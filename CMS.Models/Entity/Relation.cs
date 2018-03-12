using System;
using System.Collections.Generic;

namespace CMS.Base.Models.Entity
{
    public class Relation
    {
        public string Name { get; set; }
        public Guid? RelateDefinitionId { get; set; }
        public List<Guid> RelatedEntitiesIds { get; set; }
    }
}