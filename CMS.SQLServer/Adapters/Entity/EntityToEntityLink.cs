using System;

namespace CMS.Providers.SQL.Adapters.Entity
{
    public class EntityToEntityLink
    {
        public Guid ParentId { get; set; }
        public Guid ChildId { get; set; }
    }
}