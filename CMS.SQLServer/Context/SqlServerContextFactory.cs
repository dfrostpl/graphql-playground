using System;
using CMS.Providers.SQLServer.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CMS.Providers.SQLServer.Context
{
    public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
    {
        private readonly SqlServerConfiguration _configuration;

        public SqlServerContextFactory()
        {
        }

        public SqlServerContextFactory(SqlServerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlServerContext CreateDbContext()
        {
            return CreateDbContext(Array.Empty<string>());
        }

        public SqlServerContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SqlServerContext>();
            //TODO: validate
            builder.UseSqlServer(_configuration.ConnectionString);
            return new SqlServerContext(builder.Options);
        }
    }
}