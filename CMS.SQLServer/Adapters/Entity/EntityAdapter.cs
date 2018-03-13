using System;
using System.Collections.Generic;
using CMS.Providers.SQL.Adapters.Definition;

namespace CMS.Providers.SQL.Adapters.Entity
{
    public class EntityAdapter : SqlEntityBase
    {
        
        public List<PropertyAdapter> Properties = new List<PropertyAdapter>();
        public List<RelationAdapter> Relations = new List<RelationAdapter>();
        public DefinitionAdapter Definition { get; set; }
        public Guid DefinitionId { get; set; }
    }
}