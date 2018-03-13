using System;

namespace CMS.Providers.SQL.Adapters
{
    public abstract class SqlEntityBase
    {
        public Guid Id { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}