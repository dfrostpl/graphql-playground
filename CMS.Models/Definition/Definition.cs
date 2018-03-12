using System.Collections.Generic;

namespace CMS.Base.Models.Definition
{
    public class Definition : DbObject
    {
        public string Name { get; set; }
        public List<PropertyDefinition> Properties { get; set; } = new List<PropertyDefinition>();
        public List<RelationDefinition> Relations { get; set; } = new List<RelationDefinition>();
    }
}