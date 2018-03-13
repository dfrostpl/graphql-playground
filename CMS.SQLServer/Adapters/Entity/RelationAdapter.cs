using System;
using System.Collections.Generic;
using CMS.Base.Models.Definition;
using CMS.Providers.SQL.Adapters.Definition;

namespace CMS.Providers.SQL.Adapters.Entity
{
    public class RelationAdapter : SqlEntityBase
    {
        public string Name { get; set; }
        public Guid RelatedDefinitionId { get; set; }
        public DefinitionAdapter RelatedDefinition { get; set; }
        public RelationRole Role { get; set; }
        public RelationCardinality Cardinality { get; set; }
        public List<EntityToEntityLink> RelatedEntitiesIds { get; set; } = new List<EntityToEntityLink>();
    }
}