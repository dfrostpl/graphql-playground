using System;
using System.Collections.Generic;
using System.Linq;
using CMS.Base.Data;
using CMS.Models.Definition;
using CMS.Models.Entity;

namespace CMS.SQLServer
{
    public partial class SqlServerRepository : IEntityRepository
    {
        public virtual IEntityRepository Entities => this;

        private readonly List<Entity> _entities = new List<Entity>();
        Entity IEntityRepository.Single(Guid id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        IEnumerable<Entity> IEntityRepository.Many(string definitionName)
        {
            var definition = Definitions.Single(definitionName);
            return Entities.Many(definition.Id);
        }

        IEnumerable<Entity> IEntityRepository.Many(Guid definitionId)
        {
            return _entities.Where(e => e.DefinitionId == definitionId);
        }

        Entity IEntityRepository.Create(Definition definition)
        {
            var entity = new Entity
            {
                CreatedAt = DateTime.Now,
                DefinitionId = definition.Id,
                ModifiedAt = DateTime.Now,
                Id = Guid.NewGuid()
            };
            foreach (var definitionProperty in definition.Properties)
            {
                entity.Properties.Add(new EntityMember
                {
                    Name = definitionProperty.Name,
                    Type = definitionProperty.Type
                });
            }
            _entities.Add(entity);
            return entity;
        }

        void IEntityRepository.Update(Entity entity)
        {
            var dbEntity = Entities.Single(entity.Id);
            dbEntity.Properties = entity.Properties;
        }
    }
}
