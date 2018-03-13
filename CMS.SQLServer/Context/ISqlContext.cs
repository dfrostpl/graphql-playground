using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Providers.SQL.Adapters.Definition;
using CMS.Providers.SQL.Adapters.Entity;

namespace CMS.Providers.SQL.Context
{
    public interface ISqlContext
    {
        Task<DefinitionAdapter> DefinitionById(Guid id);
        Task<DefinitionAdapter> DefinitionByName(string name);
        Task<List<DefinitionAdapter>> DefinitionsById(Guid[] ids);
        Task<List<DefinitionAdapter>> AllDefinitions();

        Task<EntityAdapter> EntityById(Guid id, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<EntityAdapter>> EntitiesByIds(Guid[] ids, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<EntityAdapter>> EntitiesByDefinitionId(Guid definitionId, string[] propertiesToLoad = null, string[] relationsToLoad = null);
    }
}