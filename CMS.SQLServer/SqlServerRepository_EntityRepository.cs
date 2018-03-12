using System;
using System.Collections.Generic;
using System.Linq;
using CMS.Base.Models.Definition;
using CMS.Base.Models.Entity;
using CMS.Base.ProviderContracts;

namespace CMS.Providers.SQLServer
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
                entity.Properties.Add(new Property
                {
                    Name = definitionProperty.Name,
                    Type = definitionProperty.Type
                });
            }
            foreach (var definitionRelation in definition.Relations)
            {
                entity.Relations.Add(new Relation
                {
                    Name = definitionRelation.Name,
                    RelateDefinitionId = definitionRelation.RelatedDefinitionId
                });
            }
            _entities.Add(entity);
            return entity;
        }

        void IEntityRepository.Update(Entity entity)
        {
            var dbEntity = Entities.Single(entity.Id);
            dbEntity.Properties = entity.Properties;
            dbEntity.Relations = entity.Relations;
        }
    }
}
