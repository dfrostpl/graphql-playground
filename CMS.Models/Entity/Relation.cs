﻿using System;
using System.Collections.Generic;
using CMS.Base.Models.Definition;

namespace CMS.Base.Models.Entity
{
    public class Relation
    {
        public string Name { get; set; }
        public Guid RelatedDefinitionId { get; set; }
        public RelationRole Role { get; set; }
        public RelationCardinality Cardinality { get; set; }
        public List<Guid> ParentIds { get; set; } = new List<Guid>();
        public List<Guid> ChildIds { get; set; } = new List<Guid>();
    }
}