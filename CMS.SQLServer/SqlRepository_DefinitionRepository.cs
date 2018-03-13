using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Base.Models.Definition;
using CMS.Base.ProviderContracts;

namespace CMS.Providers.SQL
{
    public partial class SqlRepository : IDefinitionRepository
    {
        async Task<Definition> IDefinitionRepository.Single(Guid id)
        {
            var definitionRecord = await _context.QueryDefinitionById(id).ConfigureAwait(false);
            return _mapper.Map<Definition>(definitionRecord);
        }

        async Task<Definition> IDefinitionRepository.Single(string name)
        {
            var definitionRecord = await _context.QueryDefinitionByName(name).ConfigureAwait(false);
            return _mapper.Map<Definition>(definitionRecord);
        }

        async Task<List<Definition>> IDefinitionRepository.Many()
        {
            var definitionRecords = await _context.QueryAllDefinitions().ConfigureAwait(false);
            return definitionRecords.Select(d=>_mapper.Map<Definition>(d)).ToList();
        }

        async Task<Definition> IDefinitionRepository.Create(string name)
        {
            var definition = new Definition
            {
                Name = name,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Id = Guid.NewGuid()
            };
            await Context.Definitions.AddAsync(definition).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return definition;
        }

        async Task<Guid> IDefinitionRepository.Update(Definition definition)
        {
            var dbDefinition = await Definitions.Single(definition.Id).ConfigureAwait(false);
            dbDefinition.Name = definition.Name;
            dbDefinition.Properties = definition.Properties;
            dbDefinition.Relations = definition.Relations;
            Context.Definitions.Update(definition);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return dbDefinition.Id;
        }

        public virtual IDefinitionRepository Definitions => this;
    }
}