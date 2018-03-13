using System;
using Autofac;
using Awesome.Data.Sql.Builder.Renderers;
using CMS.Base.ProviderContracts;
using CMS.Providers.SQL.Configuration;
using CMS.Providers.SQL.Context;
using Microsoft.Extensions.Configuration;

namespace CMS.Providers.SQL
{
    public class SqlProviderModule : Module
    {
        private readonly IConfiguration _configuration;

        public SqlProviderModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var sqlConfiguration = new SqlConfiguration();
            _configuration.GetSection("sql").Bind(sqlConfiguration);
            builder.RegisterType<SqlMapperConfiguration>().AsSelf();
            builder.RegisterInstance(sqlConfiguration).SingleInstance().AsSelf();
            builder.RegisterType<SqlContext>().SingleInstance().As<ISqlContext>();
            builder.RegisterType<SqlRepository>().SingleInstance().As<IRepository>();
            builder.Register<ISqlRenderer>(c =>
            {
                switch (sqlConfiguration.Provider)
                {
                    case Constants.SupportedProviders.MSSQL:
                        return new SqlServerSqlRenderer();
                    default:
                        throw new Exception("Provider not supported");
                }
            });
        }
    }
}