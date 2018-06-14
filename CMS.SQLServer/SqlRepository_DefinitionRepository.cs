using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Base.Abstractions;
using CMS.Base.Models.Definition;

namespace CMS.Providers.SQL
{
    public partial class SqlRepository : IDefinitionRepository
    {
        async Task<Definition> IDefinitionRepository.SingleAsync(Guid id)
        {
            var definitionRecord = await _context.QueryDefinitionByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<Definition>(definitionRecord);
        }

        async Task<Definition> IDefinitionRepository.SingleAsync(string name)
        {
            var definitionRecord = await _context.QueryDefinitionByNameAsync(name).ConfigureAwait(false);
            return _mapper.Map<Definition>(definitionRecord);
        }

        async Task<List<Definition>> IDefinitionRepository.ManyAsync()
        {
            var definitionRecords = await _context.QueryAllDefinitionsAsync().ConfigureAwait(false);
            return definitionRecords.Select(d=>_mapper.Map<Definition>(d)).ToList();
        }

        //async Task<Definition> IDefinitionRepository.CreateAsync(string name)
        //{
        //    var definition = new Definition
        //    {
        //        Name = name,
        //        CreatedAt = DateTime.Now,
        //        ModifiedAt = DateTime.Now,
        //        Id = Guid.NewGuid()
        //    };
        //    await Context.Definitions.AddAsync(definition).ConfigureAwait(false);
        //    await Context.SaveChangesAsync().ConfigureAwait(false);
        //    return definition;
        //}

        //async Task<Guid> IDefinitionRepository.UpdateAsync(Definition definition)
        //{
        //    var dbDefinition = await Definitions.SingleAsync(definition.Id).ConfigureAwait(false);
        //    dbDefinition.Name = definition.Name;
        //    dbDefinition.Properties = definition.Properties;
        //    dbDefinition.Relations = definition.Relations;
        //    Context.Definitions.Update(definition);
        //    await Context.SaveChangesAsync().ConfigureAwait(false);
        //    return dbDefinition.Id;
        //}

        public virtual IDefinitionRepository Definitions => this;
    }
}