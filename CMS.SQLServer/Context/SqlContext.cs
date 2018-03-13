using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Awesome.Data.Sql.Builder;
using Awesome.Data.Sql.Builder.Renderers;
using Awesome.Data.Sql.Builder.Select;
using CMS.Providers.SQL.Adapters.Definition;
using CMS.Providers.SQL.Adapters.Entity;
using CMS.Providers.SQL.Configuration;
using CMS.Providers.SQL.Extensions;
using CMS.Providers.SQL.Queries;
using Dapper;

namespace CMS.Providers.SQL.Context
{
    public class SqlContext : ISqlContext
    {
        private readonly SqlConfiguration _configuration;
        private readonly ISqlRenderer _renderer;
        public SqlContext(SqlConfiguration configuration, ISqlRenderer renderer)
        {
            _configuration = configuration;
            _renderer = renderer;
        }

        public async Task<DefinitionAdapter> DefinitionById(Guid id)
        {
            using (var connection = GetConnection())
            {
                //TODO: validate
                var query = new SelectStatement(new [] {"*"}).From("Definitions").Limit(1).Where("Id = @Id").ToSql(_renderer);
                var definition = await connection.QueryFirstOrDefaultAsync<DefinitionAdapter>(query, new {Id = id.ToString()}).ConfigureAwait(false);
                return definition == null ? null : await LoadDefinition(connection, definition).ConfigureAwait(false);
            }
        }

        public async Task<DefinitionAdapter> DefinitionByName(string name)
        {
            using (var connection = GetConnection())
            {
                //TODO: validate
                var query = new SelectStatement(new[]{"*"}).From("Definitions").Limit(1).Where("Name = @Name").ToSql(_renderer);
                var definition = await connection.QueryFirstOrDefaultAsync<DefinitionAdapter>(query, new {Name = name}).ConfigureAwait(false);
                return definition == null ? null : await LoadDefinition(connection, definition).ConfigureAwait(false);
            }
        }

        public async Task<List<DefinitionAdapter>> DefinitionsById(Guid[] ids)
        {
            using (var connection = GetConnection())
            {
                //TODO: validate
                var query = new SelectStatement(new[]{"*"}).From("Definitions").Where("Id IN @Ids").ToSql(_renderer);
                var idsToRequest = ids.Select(i => i.ToString()).Distinct();
                var definitions = (await connection.QueryAsync<DefinitionAdapter>(query, new {Ids = idsToRequest}).ConfigureAwait(false)).ToList();
                foreach (var definitionAdapter in definitions)
                    await LoadDefinition(connection, definitionAdapter).ConfigureAwait(false);
                return definitions;
            }
        }

        public async Task<List<DefinitionAdapter>> AllDefinitions()
        {
            using (var connection = GetConnection())
            {
                var query = new SelectStatement(new[]{"*"}).From("Definitions").ToSql(_renderer);
                return (await connection.QueryAsync<DefinitionAdapter>(query).ConfigureAwait(false)).ToList();
            }
        }

        public async Task<EntityAdapter> EntityById(Guid id, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
            using (var connection = GetConnection())
            {
                var query = new SelectStatement(new[]{"*"}).From("Entities").Where("Id = @Id").ToSql(_renderer);
                var entity = await connection.QueryFirstOrDefaultAsync<EntityAdapter>(query, new {Id = id.ToString()}).ConfigureAwait(false);
                return entity == null ? null : await LoadEntity(connection, entity, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
            }
        }

        public async Task<List<EntityAdapter>> EntitiesByIds(Guid[] ids, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
            using (var connection = GetConnection())
            {
                var query = new SelectStatement(new[]{"*"}).From("Entities").Where("Id IN @ids").ToSql(_renderer);
                var idsToRequest = ids.Select(i => i.ToString()).Distinct();
                var entities = (await connection.QueryAsync<EntityAdapter>(query, new {Ids = idsToRequest }).ConfigureAwait(false)).ToList();
                foreach (var entityAdapter in entities)
                    await LoadEntity(connection, entityAdapter, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
                return entities;
            }
        }

        public async Task<List<EntityAdapter>> EntitiesByDefinitionId(Guid definitionId, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
            using (var connection = GetConnection())
            {
                var query = new SelectStatement(new[]{"*"}).From("Entities").Where("DefinitionId = @DefinitionId").ToSql(_renderer);
                var entities = (await connection.QueryAsync<EntityAdapter>(query, new {DefinitionId = definitionId.ToString()}).ConfigureAwait(false)).ToList();
                foreach (var entityAdapter in entities)
                    await LoadEntity(connection, entityAdapter, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
                return entities;
            }
        }

        private async Task<DefinitionAdapter> LoadDefinition(SqlConnection connection, DefinitionAdapter definition)
        {
            //TODO: use OneToMany Dapper loader
            var propertiesQuery = new SelectStatement(new[]{"*"}).From("PropertyDefinitions").Where("DefinitionId = @DefinitionId").ToSql(_renderer);
            definition.Properties = (await connection.QueryAsync<PropertyDefinitionAdapter>(propertiesQuery, new {DefinitionId = definition.Id.ToString()}).ConfigureAwait(false))?.ToList();
            var relationsQuery = new SelectStatement(new []{"*"}).From("RelationDefinitions").Where("DefinitionId = @DefinitionId").ToSql(_renderer);
            definition.Relations = (await connection.QueryAsync<RelationDefinitionAdapter>(DefinitionQueries.RelationDefinitionsByDefinitionIdQuery, new {DefinitionId = definition.Id.ToString()}).ConfigureAwait(false))?.ToList();
            return definition;
        }

        private async Task<EntityAdapter> LoadEntity(SqlConnection connection, EntityAdapter entity, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
            //TODO: use OneToMany Dapper loader
            await entity.LoadProperties(connection, propertiesToLoad).ConfigureAwait(false);
            await entity.LoadRelations(connection, relationsToLoad).ConfigureAwait(false);
            return entity;
        }

        private SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_configuration.ConnectionString);
            connection.Open();
            return connection;
        }
    }
}