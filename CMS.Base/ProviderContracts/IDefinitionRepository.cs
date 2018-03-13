using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Base.Models.Definition;

namespace CMS.Base.ProviderContracts
{
    public interface IDefinitionRepository
    {
        Task<Definition> Single(Guid id);
        Task<Definition> Single(string name);
        Task<List<Definition>> Many();
        Task<Definition> Create(string name);
        Task<Guid> Update(Definition definition);
    }
}