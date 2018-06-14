using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Base.Models.Definition;

namespace CMS.Base.Abstractions
{
    public interface IDefinitionRepository
    {
        Task<Definition> SingleAsync(Guid id);
        Task<Definition> SingleAsync(string name);
        Task<List<Definition>> ManyAsync();
        //Task<Guid> UpdateAsync(Definition definition);
    }
}