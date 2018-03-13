using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CMS.Providers.SQL.Adapters.Entity
{
    public class PropertyAdapter : SqlEntityBase
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        [Column("Value")]
        public string SerializedValue { get; set; }

        [NotMapped]
        public object Value
        {
            get => string.IsNullOrEmpty(SerializedValue) ? null : JsonConvert.DeserializeObject(SerializedValue);
            set => SerializedValue = value == null ? string.Empty : JsonConvert.SerializeObject(value);
        }
    }
}