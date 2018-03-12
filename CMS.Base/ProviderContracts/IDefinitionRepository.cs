using System;
using System.Collections.Generic;
using CMS.Base.Models.Definition;

namespace CMS.Base.ProviderContracts
{
    public interface IDefinitionRepository
    {
        Definition Single(Guid id);
        Definition Single(string name);
        IEnumerable<Definition> Many();
        Definition Create(string name);
        void Update(Definition definition);
    }
}