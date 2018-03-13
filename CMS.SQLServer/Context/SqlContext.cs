using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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

        public SqlContext(SqlConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DefinitionAdapter> QueryDefinitionById(Guid id)
        {
            using (var connection = GetConnection())
            {
                var definition = await connection.QueryFirstOrDefaultAsync<DefinitionAdapter>(DefinitionQueries.DefinitionByIdQuery, new {Id = id.ToString()})
                    .ConfigureAwait(false);
                return definition == null ? null : await LoadDefinition(connection, definition).ConfigureAwait(false);
            }
        }

        public async Task<DefinitionAdapter> QueryDefinitionByName(string name)
        {
            using (var connection = GetConnection())
            {
                var definition = await connection.QueryFirstOrDefaultAsync<DefinitionAdapter>(DefinitionQueries.DefinitionByNameQuery, new {name}).ConfigureAwait(false);
                return definition == null ? null : await LoadDefinition(connection, definition).ConfigureAwait(false);
            }
        }

        public async Task<List<DefinitionAdapter>> QueryDefinitionsById(Guid[] ids)
        {
            using (var connection = GetConnection())
            {
                var definitions = (await connection.QueryAsync<DefinitionAdapter>(DefinitionQueries.DefinitionsByIdsQuery, new {Ids = ids.Select(i => i.ToString()).Distinct()})
                    .ConfigureAwait(false)).ToList();
                foreach (var definitionAdapter in definitions)
                    await LoadDefinition(connection, definitionAdapter).ConfigureAwait(false);
                return definitions;
            }
        }

        public async Task<List<DefinitionAdapter>> QueryAllDefinitions()
        {
            using (var connection = GetConnection())
            {
                return (await connection.QueryAsync<DefinitionAdapter>(DefinitionQueries.AllDefinitions).ConfigureAwait(false)).ToList();
            }
        }

        public async Task<EntityAdapter> QueryEntityById(Guid id, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
            using (var connection = GetConnection())
            {
                var entity = await connection.QueryFirstOrDefaultAsync<EntityAdapter>(EntityQueries.EntityByIdQuery, new {Id = id.ToString()}).ConfigureAwait(false);
                return entity == null ? null : await LoadEntity(connection, entity, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
            }
        }

        public async Task<List<EntityAdapter>> QueryEntitiesByIds(Guid[] ids, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
            using (var connection = GetConnection())
            {
                var entities = (await connection.QueryAsync<EntityAdapter>(EntityQueries.EntitiesByIdsQuery, new {Ids = ids.Select(i => i.ToString()).Distinct()})
                        .ConfigureAwait(false))
                    .ToList();
                foreach (var entityAdapter in entities)
                    await LoadEntity(connection, entityAdapter, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
                return entities;
            }
        }

        public async Task<List<EntityAdapter>> QueryEntitiesByDefinitionId(Guid definitionId, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
            using (var connection = GetConnection())
            {
                var entities = (await connection.QueryAsync<EntityAdapter>(EntityQueries.EntitiesByDefinitionIdQuery, new {DefinitionId = definitionId.ToString()})
                    .ConfigureAwait(false)).ToList();
                foreach (var entityAdapter in entities)
                    await LoadEntity(connection, entityAdapter, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
                return entities;
            }
        }

        private async Task<DefinitionAdapter> LoadDefinition(SqlConnection connection, DefinitionAdapter definition)
        {
            definition.Properties = (await connection.QueryAsync<PropertyDefinitionAdapter>(DefinitionQueries.PropertyDefinitionsByDefinitionIdQuery,
                new {DefinitionId = definition.Id.ToString()}).ConfigureAwait(false))?.ToList();
            definition.Relations = (await connection.QueryAsync<RelationDefinitionAdapter>(DefinitionQueries.RelationDefinitionsByDefinitionIdQuery,
                new {DefinitionId = definition.Id.ToString()}).ConfigureAwait(false))?.ToList();
            return definition;
        }

        private async Task<EntityAdapter> LoadEntity(SqlConnection connection, EntityAdapter entity, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
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