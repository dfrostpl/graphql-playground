using System.Collections.Generic;

namespace CMS.Models.Definition
{
    public class Definition : DbObject
    {
        public string Name { get; set; }
        public List<DefinitionMember> Properties { get; set; } = new List<DefinitionMember>();
    }
}