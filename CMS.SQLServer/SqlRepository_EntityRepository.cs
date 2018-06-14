using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Base.Abstractions;
using CMS.Base.Models.Definition;
using CMS.Base.Models.Entity;

namespace CMS.Providers.SQL
{
    public partial class SqlRepository : IEntityRepository
    {
        public virtual IEntityRepository Entities => this;

        async Task<Entity> IEntityRepository.SingleAsync(Guid id, string[] propertiesToLoad, string[] relationsToLoad)
        {
            var entityRecord = await _context.QueryEntityByIdAsync(id, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
            return _mapper.Map<Entity>(entityRecord);
        }

        async Task<List<Entity>> IEntityRepository.ManyAsync(string definitionName, string[] propertiesToLoad, string[] relationsToLoad)
        {
            var definition = await Definitions.SingleAsync(definitionName).ConfigureAwait(false);
            return await Entities.ManyAsync(definition.Id, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
        }

        async Task<List<Entity>> IEntityRepository.ManyAsync(Guid definitionId, string[] propertiesToLoad, string[] relationsToLoad)
        {
            var entities = await _context.QueryEntitiesByDefinitionIdAsync(definitionId, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
            return entities.Select(e => _mapper.Map<Entity>(e)).ToList();
        }

        async Task<List<Entity>> IEntityRepository.ManyAsync(Guid definitionId, string[] propertiesToLoad, string[] relationsToLoad, params Guid[] ids)
        {
            var entityRecords = await _context.QueryEntitiesByIdsAsync(ids, propertiesToLoad, relationsToLoad).ConfigureAwait(false);
            return entityRecords.Where(e => e.DefinitionId == definitionId).Select(e => _mapper.Map<Entity>(e)).ToList();
        }

        async Task<List<Entity>> IEntityRepository.ManyAsync(Guid definitionId, IEnumerable<Guid> ids, string[] propertiesToLoad, string[] relationsToLoad)
        {
            var entityRecords = await _context.QueryEntitiesByIdsAsync(ids.ToArray(), propertiesToLoad, relationsToLoad).ConfigureAwait(false);
            return entityRecords.Where(e => e.DefinitionId == definitionId).Select(e => _mapper.Map<Entity>(e)).ToList();
        }

        //async Task<Entity> IEntityRepository.CreateAsync(Definition definition)
        //{
        //    var entity = new Entity
        //    {
        //        CreatedAt = DateTime.Now,
        //        DefinitionId = definition.Id,
        //        ModifiedAt = DateTime.Now,
        //        Id = Guid.NewGuid()
        //    };
        //    foreach (var definitionProperty in definition.Properties)
        //    {
        //        entity.Properties.Add(new Property
        //        {
        //            Name = definitionProperty.Name,
        //            Type = definitionProperty.Type
        //        });
        //    }
        //    foreach (var definitionRelation in definition.Relations)
        //    {
        //        entity.Relations.Add(new Relation
        //        {
        //            Name = definitionRelation.Name,
        //            RelatedDefinitionId = definitionRelation.RelatedDefinitionId
        //        });
        //    }
        //    await Context.Entities.AddAsync(entity).ConfigureAwait(false);
        //    await Context.SaveChangesAsync().ConfigureAwait(false);
        //    return entity;
        //}

        //async Task IEntityRepository.UpdateAsync(Entity entity)
        //{
        //    var dbEntity = await Entities.SingleAsync(entity.Id).ConfigureAwait(false);
        //    dbEntity.Properties = entity.Properties;
        //    dbEntity.Relations = entity.Relations;
        //    Context.Entities.Update(dbEntity);
        //    await Context.SaveChangesAsync().ConfigureAwait(false);
        //}
        
    }
}
