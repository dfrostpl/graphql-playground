using System;

namespace CMS.Providers.SQLServer.Adapters
{
    internal abstract class SqlServerEntityBase
    {
        public Guid Id { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}