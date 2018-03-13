using System.Collections.Generic;

namespace CMS.Providers.SQL.Adapters.Definition
{
    public class DefinitionAdapter : SqlEntityBase
    {
        public string Name { get; set; }
        public List<PropertyDefinitionAdapter> Properties { get; set; }
        public List<RelationDefinitionAdapter> Relations { get; set; }
    }
}