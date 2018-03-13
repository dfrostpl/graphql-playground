using System;
using CMS.Base.Models.Definition;

namespace CMS.Providers.SQL.Adapters.Definition
{
    public class RelationDefinitionAdapter : SqlEntityBase
    {
        public string Name { get; set; }
        public Guid RelatedDefinitionId { get; set; }
        public DefinitionAdapter RelatedDefinition { get; set; }
        public RelationRole Role { get; set; }
        public RelationCardinality Cardinality { get; set; }
        public Guid DefinitionId { get; set; }
    }
}