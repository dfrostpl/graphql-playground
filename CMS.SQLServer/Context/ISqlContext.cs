using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Providers.SQL.Adapters.Definition;
using CMS.Providers.SQL.Adapters.Entity;

namespace CMS.Providers.SQL.Context
{
    public interface ISqlContext
    {
        Task<DefinitionAdapter> QueryDefinitionByIdAsync(Guid id);
        Task<DefinitionAdapter> QueryDefinitionByNameAsync(string name);
        Task<List<DefinitionAdapter>> QueryDefinitionsByIdAsync(Guid[] ids);
        Task<List<DefinitionAdapter>> QueryAllDefinitionsAsync();

        Task<EntityAdapter> QueryEntityByIdAsync(Guid id, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<EntityAdapter>> QueryEntitiesByIdsAsync(Guid[] ids, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<EntityAdapter>> QueryEntitiesByDefinitionIdAsync(Guid definitionId, string[] propertiesToLoad = null, string[] relationsToLoad = null);
    }
}