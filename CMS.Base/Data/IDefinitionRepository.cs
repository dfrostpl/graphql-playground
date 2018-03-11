using System;
using System.Collections.Generic;
using CMS.Models.Definition;

namespace CMS.Base.Data
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