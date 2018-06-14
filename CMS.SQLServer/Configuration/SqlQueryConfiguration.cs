using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMS.Providers.SQL.Configuration
{
    public class SqlQueryConfiguration
    {
        [JsonProperty("name")]

        public string Name { get; set; }

        [JsonProperty("parameters")]

        public JObject Parameters { get; set; }
    }
}