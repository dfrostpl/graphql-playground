using System;
using System.Collections.Generic;
using CMS.Base.Models.Definition;
using CMS.Base.Models.Entity;

namespace CMS.Base.ProviderContracts
{
    public interface IEntityRepository
    {
        Entity Single(Guid id);
        IEnumerable<Entity> Many(string definitionName);
        IEnumerable<Entity> Many(Guid definitionId);
        Entity Create(Definition definition);
        void Update(Entity entity);
    }
}