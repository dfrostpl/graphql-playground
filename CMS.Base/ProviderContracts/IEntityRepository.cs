using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Base.Models.Definition;
using CMS.Base.Models.Entity;

namespace CMS.Base.ProviderContracts
{
    public interface IEntityRepository
    {
        Task<Entity> Single(Guid id);
        Task<List<Entity>> Many(string definitionName);
        Task<List<Entity>> Many(Guid definitionId);
        Task<Entity> Create(Definition definition);
        Task Update(Entity entity);
    }
}