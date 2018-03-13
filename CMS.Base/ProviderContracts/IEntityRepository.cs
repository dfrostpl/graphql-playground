using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Base.Models.Definition;
using CMS.Base.Models.Entity;

namespace CMS.Base.ProviderContracts
{
    public interface IEntityRepository
    {
        Task<Entity> Single(Guid id, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<Entity>> Many(string definitionName, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<Entity>> Many(Guid definitionId, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<Entity>> Many(Guid definitionId, string[] propertiesToLoad = null, string[] relationsToLoad = null, params Guid[] ids);
        Task<List<Entity>> Many(Guid definitionId, IEnumerable<Guid> ids, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<Entity> Create(Definition definition);
        Task Update(Entity entity);
    }
}