using Newtonsoft.Json;

namespace CMS.Providers.SQL.Configuration
{
    public class SqlConfiguration
    {
        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }
    }
}