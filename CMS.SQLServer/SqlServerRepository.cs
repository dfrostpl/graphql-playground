using CMS.Base.ProviderContracts;
using CMS.Providers.SQLServer.Context;

namespace CMS.Providers.SQLServer
{
    public partial class SqlServerRepository : IRepository
    {
        protected SqlServerContext Context;

        public SqlServerRepository(SqlServerContext context)
        {
            Context = context;
        }
    }
}