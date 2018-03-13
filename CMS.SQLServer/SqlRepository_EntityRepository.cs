using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Base.Models.Definition;
using CMS.Base.Models.Entity;
using CMS.Base.ProviderContracts;

namespace CMS.Providers.SQL
{
    public partial class SqlRepository : IEntityRepository
    {
        public virtual IEntityRepository Entities => this;

        async Task<Entity> IEntityRepository.Single(Guid id, string[] propertiesToLoad, string[] relationsToLoad)
        {
            var entityRecord = await _context.QueryEntityById(id, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
            return _mapper.Map<Entity>(entityRecord);
        }

        async Task<List<Entity>> IEntityRepository.Many(string definitionName, string[] propertiesToLoad, string[] relationsToLoad)
        {
            var definition = await Definitions.Single(definitionName).ConfigureAwait(false);
            return await Entities.Many(definition.Id, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
        }

        async Task<List<Entity>> IEntityRepository.Many(Guid definitionId, string[] propertiesToLoad, string[] relationsToLoad)
        {
            var entities = await _context.QueryEntitiesByDefinitionId(definitionId, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
            return entities.Select(e => _mapper.Map<Entity>(e)).ToList();
        }

        async Task<List<Entity>> IEntityRepository.Many(Guid definitionId, string[] propertiesToLoad, string[] relationsToLoad, params Guid[] ids)
        {
            var entityRecords = await _context.QueryEntitiesByIds(ids, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
            return entityRecords.Where(e => e.DefinitionId == definitionId).Select(e => _mapper.Map<Entity>(e)).ToList();
        }

        async Task<List<Entity>> IEntityRepository.Many(Guid definitionId, IEnumerable<Guid> ids, string[] propertiesToLoad, string[] relationsToLoad)
        {
            var entityRecords = await _context.QueryEntitiesByIds(ids.ToArray(), propertiesToLoad, relationsToLoad).ConfigureAwait(false);
            return entityRecords.Where(e => e.DefinitionId == definitionId).Select(e => _mapper.Map<Entity>(e)).ToList();
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
                    RelatedDefinitionId = definitionRelation.RelatedDefinitionId
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
            Context.Entities.Update(dbEntity);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }
        
    }
}
