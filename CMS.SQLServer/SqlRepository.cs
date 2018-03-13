using AutoMapper;
using CMS.Base.ProviderContracts;
using CMS.Providers.SQL.Configuration;
using CMS.Providers.SQL.Context;

namespace CMS.Providers.SQL
{
    public partial class SqlRepository : IRepository
    {
        private readonly ISqlContext _context;
        private readonly IMapper _mapper;

        public SqlRepository(ISqlContext context, SqlMapperConfiguration mapperConfiguration)
        {
            _context = context;
            _mapper = new Mapper(mapperConfiguration.Configuration);
        }
    }
}