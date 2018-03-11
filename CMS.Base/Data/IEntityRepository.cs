using System;
using System.Collections.Generic;
using CMS.Models.Definition;
using CMS.Models.Entity;

namespace CMS.Base.Data
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