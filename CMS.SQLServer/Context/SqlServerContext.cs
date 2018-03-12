using CMS.Base.Models.Definition;
using CMS.Base.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace CMS.Providers.SQLServer.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions options) : base(options) { }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        
    }
}