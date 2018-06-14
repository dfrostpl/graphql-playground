using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CMS.Base.Helpers;
using CMS.Providers.SQL.Adapters.Definition;
using CMS.Providers.SQL.Adapters.Entity;
using CMS.Providers.SQL.Configuration;
using CMS.Providers.SQL.Exceptions;
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

        public async Task<DefinitionAdapter> QueryDefinitionByIdAsync(Guid id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var queryConfig = _configuration.Queries.FindByName(Constants.Queries.GetDefinitionById);
                    Guard.NotNull(queryConfig, new SqlProviderConfigurationException($"Query configuration for {Constants.Queries.GetDefinitionById} not found"));
                    var definitionIdParam = queryConfig.Parameters[Constants.Parameters.DefinitionId]?.ToObject<string>();
                    var procedureNameParam = queryConfig.Parameters[Constants.Parameters.ProcedureName]?.ToObject<string>();
                    Guard.NotEmptyString(definitionIdParam, new SqlProviderConfigurationException($"Query {Constants.Queries.GetDefinitionById} has wrong configuration for {Constants.Parameters.DefinitionId} parameter."));
                    Guard.NotEmptyString(procedureNameParam, new SqlProviderConfigurationException($"Query {Constants.Queries.GetDefinitionById} has wrong configuration for {Constants.Parameters.ProcedureName} parameter."));
                    var parameters = new DynamicParameters();
                    parameters.Add(definitionIdParam, id.ToString());
                    var definition = connection.Query<DefinitionAdapter>(procedureNameParam, parameters, commandType:CommandType.StoredProcedure).FirstOrDefault();
                    return definition == null ? null : await LoadDefinitionAsync(connection, definition).ConfigureAwait(false);
                }
            }
            catch (Exception)
            {
                //logging
                throw;
            }
        }

        public async Task<DefinitionAdapter> QueryDefinitionByNameAsync(string name)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var queryConfig = _configuration.Queries.FindByName(Constants.Queries.GetDefinitionByName);
                    Guard.NotNull(queryConfig, new SqlProviderConfigurationException($"Query configuration for {Constants.Queries.GetDefinitionByName} not found"));
                    var definitionNameParam = queryConfig.Parameters[Constants.Parameters.DefinitionName]?.ToObject<string>();
                    var procedureNameParam = queryConfig.Parameters[Constants.Parameters.ProcedureName]?.ToObject<string>();
                    Guard.NotEmptyString(definitionNameParam, new SqlProviderConfigurationException($"Query {Constants.Queries.GetDefinitionByName} has wrong configuration for {Constants.Parameters.DefinitionName} parameter."));
                    Guard.NotEmptyString(procedureNameParam, new SqlProviderConfigurationException($"Query {Constants.Queries.GetDefinitionByName} has wrong configuration for {Constants.Parameters.ProcedureName} parameter."));
                    var parameters = new DynamicParameters();
                    parameters.Add(definitionNameParam, name);
                    var definition = connection.Query<DefinitionAdapter>(procedureNameParam, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return definition == null ? null : await LoadDefinitionAsync(connection, definition).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                //logging
                throw;
            }
        }

        public async Task<List<DefinitionAdapter>> QueryDefinitionsByIdAsync(Guid[] ids)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var definitions = (await connection.QueryAsync<DefinitionAdapter>(DefinitionQueries.DefinitionsByIdsQuery, new {Ids = ids.Select(i => i.ToString()).Distinct()})
                        .ConfigureAwait(false)).ToList();
                    foreach (var definitionAdapter in definitions)
                        await LoadDefinitionAsync(connection, definitionAdapter).ConfigureAwait(false);
                    return definitions;
                }
            }
            catch (Exception ex)
            {
                //logging
                throw;
            }
        }

        public async Task<List<DefinitionAdapter>> QueryAllDefinitionsAsync()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return (await connection.QueryAsync<DefinitionAdapter>(DefinitionQueries.AllDefinitions).ConfigureAwait(false)).ToList();
                }
            }
            catch (Exception ex)
            {
                //logging
                throw;
            }
        }

        public async Task<EntityAdapter> QueryEntityByIdAsync(Guid id, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var entity = await connection.QueryFirstOrDefaultAsync<EntityAdapter>(EntityQueries.EntityByIdQuery, new {Id = id.ToString()}).ConfigureAwait(false);
                    return entity == null ? null : await LoadEntityAsync(connection, entity, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                //logging
                throw;
            }
        }

        public async Task<List<EntityAdapter>> QueryEntitiesByIdsAsync(Guid[] ids, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var entities = (await connection.QueryAsync<EntityAdapter>(EntityQueries.EntitiesByIdsQuery, new {Ids = ids.Select(i => i.ToString()).Distinct()})
                            .ConfigureAwait(false))
                        .ToList();
                    foreach (var entityAdapter in entities)
                        await LoadEntityAsync(connection, entityAdapter, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
                    return entities;
                }
            }
            catch (Exception ex)
            {
                //logging
                throw;
            }
        }

        public async Task<List<EntityAdapter>> QueryEntitiesByDefinitionIdAsync(Guid definitionId, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var entities = (await connection.QueryAsync<EntityAdapter>(EntityQueries.EntitiesByDefinitionIdQuery, new {DefinitionId = definitionId.ToString()})
                        .ConfigureAwait(false)).ToList();
                    foreach (var entityAdapter in entities)
                        await LoadEntityAsync(connection, entityAdapter, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
                    return entities;
                }
            }
            catch (Exception ex)
            {
                //logging
                throw;
            }
        }

        private async Task<DefinitionAdapter> LoadDefinitionAsync(SqlConnection connection, DefinitionAdapter definition)
        {
            definition.Properties = (await connection.QueryAsync<PropertyDefinitionAdapter>(DefinitionQueries.PropertyDefinitionsByDefinitionIdQuery,
                new {DefinitionId = definition.Id.ToString()}).ConfigureAwait(false))?.ToList();
            definition.Relations = (await connection.QueryAsync<RelationDefinitionAdapter>(DefinitionQueries.RelationDefinitionsByDefinitionIdQuery,
                new {DefinitionId = definition.Id.ToString()}).ConfigureAwait(false))?.ToList();
            return definition;
        }

        private async Task<EntityAdapter> LoadEntityAsync(SqlConnection connection, EntityAdapter entity, string[] propertiesToLoad = null, string[] relationsToLoad = null)
        {
            await entity.LoadPropertiesAsync(connection, propertiesToLoad).ConfigureAwait(false);
            await entity.LoadRelationsAsync(connection, relationsToLoad).ConfigureAwait(false);
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