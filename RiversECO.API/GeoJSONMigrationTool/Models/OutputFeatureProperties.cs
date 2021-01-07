using System;
using Newtonsoft.Json;

namespace GeoJSONMigrationTool.Models
{
    public class OutputFeatureProperties
    {
        [JsonProperty("waterObjectId")]
        public Guid? WaterObjectId { get; set; }

        [JsonProperty("name_ukr")]
        public string NameUkr { get; set; }
    }
}