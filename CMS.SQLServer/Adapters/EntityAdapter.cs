using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Providers.SQLServer.Adapters
{
    internal class EntityAdapter : SqlServerEntityBase
    {
        public DefinitionAdapter Definition { get; set; }
        public Guid? DefinitionId { get; set; }
    }
}
