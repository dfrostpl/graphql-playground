using System.Collections.Generic;
using Newtonsoft.Json;

namespace CMS.Providers.SQL.Configuration
{
    public class SqlConfiguration
    {
        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }
        [JsonProperty("queries")]
        public List<SqlQueryConfiguration> Queries { get; set; } = new List<SqlQueryConfiguration>();
    }
}