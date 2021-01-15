using System;
using Newtonsoft.Json;

namespace RiversECO.Tools.GeoJSONMigrationTool.Models
{
    public class OutputFeatureProperties
    {
        [JsonProperty("waterObjectId")]
        public Guid? WaterObjectId { get; set; }

        [JsonProperty("name_ukr")]
        public string NameUkr { get; set; }
    }
}