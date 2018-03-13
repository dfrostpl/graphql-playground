using System;

namespace CMS.Providers.SQL.Adapters.Definition
{
    public class PropertyDefinitionAdapter : SqlEntityBase
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public Guid DefinitionId { get; set; }
    }
}