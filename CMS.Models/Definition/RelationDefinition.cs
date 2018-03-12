using System;

namespace CMS.Base.Models.Definition
{
    public class RelationDefinition
    {
        public string Name { get; set; }
        public Guid RelatedDefinitionId { get; set; }
        public RelationshipType RelationType { get; set; }
        public RelationCardinality Cardinality { get; set; }
    }
}