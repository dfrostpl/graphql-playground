using Newtonsoft.Json;

namespace CMS.Providers.SQLServer.Configuration
{
    public class SqlServerConfiguration
    {
        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }
    }
}