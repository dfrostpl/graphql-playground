using System;

namespace CMS.Base.Models
{
    public abstract class RecordBase
    {
        public Guid Id { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}