using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Base.Models.Definition;
using CMS.Base.Models.Entity;
using CMS.Base.ProviderContracts;
using Microsoft.EntityFrameworkCore;

namespace CMS.Providers.SQLServer
{
    public partial class SqlServerRepository : IEntityRepository
    {
        public virtual IEntityRepository Entities => this;

        Task<Entity> IEntityRepository.Single(Guid id)
        {
            return Context.Entities.Include(e=>e.Properties).Include(e=>e.Relations).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        Task<List<Entity>> IEntityRepository.Many(string definitionName)
        {
            var definition = Definitions.Single(definitionName);
            return Entities.Many(definition.Id);
        }

        Task<List<Entity>> IEntityRepository.Many(Guid definitionId)
        {
            return Context.Entities.AsNoTracking().Include(e=>e.Properties).Include(e=>e.Relations).Where(e => e.DefinitionId == definitionId).ToListAsync();
        }

        async Task<Entity> IEntityRepository.Create(Definition definition)
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
            await Context.Entities.AddAsync(entity).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        async Task IEntityRepository.Update(Entity entity)
        {
            var dbEntity = await Entities.Single(entity.Id).ConfigureAwait(false);
            dbEntity.Properties = entity.Properties;
            dbEntity.Relations = entity.Relations;
        }
    }
}
