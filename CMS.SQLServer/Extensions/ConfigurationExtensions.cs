using System.Collections.Generic;
using System.Linq;
using CMS.Providers.SQL.Configuration;

namespace CMS.Providers.SQL.Extensions
{
    public static class ConfigurationExtensions
    {
        public static SqlQueryConfiguration FindByName(this List<SqlQueryConfiguration> configurations, string name)
        {
            return configurations.FirstOrDefault(c => c.Name.Equals(name));
        }
    }
}