using Newtonsoft.Json;

namespace CMS.Providers.SQL.Configuration
{
    public class SqlConfiguration
    {
        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }
    }
}