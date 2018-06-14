using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Base.Models.Entity;

namespace CMS.Base.Abstractions
{
    public interface IEntityRepository
    {
        Task<Entity> SingleAsync(Guid id, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<Entity>> ManyAsync(string definitionName, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<Entity>> ManyAsync(Guid definitionId, string[] propertiesToLoad = null, string[] relationsToLoad = null);
        Task<List<Entity>> ManyAsync(Guid definitionId, string[] propertiesToLoad = null, string[] relationsToLoad = null, params Guid[] ids);
        Task<List<Entity>> ManyAsync(Guid definitionId, IEnumerable<Guid> ids, string[] propertiesToLoad = null, string[] relationsToLoad = null);
    }
}