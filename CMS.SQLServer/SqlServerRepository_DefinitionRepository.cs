using System;
using System.Collections.Generic;
using System.Linq;
using CMS.Base.Data;
using CMS.Models.Definition;

namespace CMS.SQLServer
{
    public partial class SqlServerRepository : IDefinitionRepository
    {
        public virtual IDefinitionRepository Definitions => this;

        private readonly List<Definition> _definitions = new List<Definition>();

        Definition IDefinitionRepository.Single(Guid id)
        {
            return _definitions.FirstOrDefault(d => d.Id == id);
        }

        Definition IDefinitionRepository.Single(string name)
        {
            return _definitions.FirstOrDefault(d => d.Name == name);
        }

        IEnumerable<Definition> IDefinitionRepository.Many()
        {
            return _definitions;
        }

        Definition IDefinitionRepository.Create(string name)
        {
            var definition = new Definition
            {
                Name = name,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Id = Guid.NewGuid()
            };
            _definitions.Add(definition);
            return definition;
        }

        void IDefinitionRepository.Update(Definition definition)
        {
            var dbDefinition = Definitions.Single(definition.Id);
            dbDefinition.Name = definition.Name;
            dbDefinition.Properties = definition.Properties;
        }
    }
}