using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Providers.SQL.Adapters.Definition;
using CMS.Providers.SQL.Adapters.Entity;

namespace CMS.Providers.SQL.Context
{
    public interface ISqlContext
    {
        Task<DefinitionAdapter> QueryDefinitionById(Guid id);
        Task<DefinitionAdapter> QueryDefinitionByName(string name);
        Task<List<DefinitionAdapter>> QueryDefinitionsById(Guid[] ids);
        Task<List<DefinitionAdapter>> QueryAllDefinitions();

        Task<EntityAdapter> QueryEntityById(Guid id, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<EntityAdapter>> QueryEntitiesByIds(Guid[] ids, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<EntityAdapter>> QueryEntitiesByDefinitionId(Guid definitionId, string[] propertiesToLoad = null, string[] relationsToLoad = null);
    }
}