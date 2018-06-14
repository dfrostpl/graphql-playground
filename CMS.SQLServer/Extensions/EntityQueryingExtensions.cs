using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CMS.Providers.SQL.Adapters.Entity;
using CMS.Providers.SQL.Queries;
using Dapper;

namespace CMS.Providers.SQL.Extensions
{
    public static class EntityQueryingExtensions
    {
        public static async Task LoadPropertiesAsync(this EntityAdapter entity, SqlConnection connection, string[] propertiesToLoad = null)
        {
            if (propertiesToLoad != null && propertiesToLoad.Any())
                entity.Properties = (await connection.QueryAsync<PropertyAdapter>(EntityQueries.FilteredPropertiesByEntityIdQuery,
                        new {EntityId = entity.Id.ToString(), Names = propertiesToLoad.Distinct()})
                    .ConfigureAwait(false))?.ToList();
            else
                entity.Properties = (await connection.QueryAsync<PropertyAdapter>(EntityQueries.PropertiesByEntityIdQuery,
                    new {EntityId = entity.Id.ToString()}).ConfigureAwait(false))?.ToList();
        }

        public static async Task LoadRelationsAsync(this EntityAdapter entity, SqlConnection connection, string[] relationsToLoad = null)
        {
            if (relationsToLoad != null && relationsToLoad.Any())
                entity.Relations = (await connection
                    .QueryAsync<RelationAdapter>(EntityQueries.FilteredRelationsByEntityIdQuery, new {EntityId = entity.Id.ToString(), Names = relationsToLoad.Distinct()})
                    .ConfigureAwait(false))?.ToList();
            else
                entity.Relations = (await connection.QueryAsync<RelationAdapter>(EntityQueries.RelationsByEntityIdQuery, new {EntityId = entity.Id.ToString()}).ConfigureAwait(false))
                    .ToList();
        }
    }
}