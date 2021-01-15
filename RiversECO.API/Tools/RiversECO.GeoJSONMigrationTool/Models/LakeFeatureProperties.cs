using System;
using Newtonsoft.Json;

namespace RiversECO.Tools.GeoJSONMigrationTool.Models
{
    public class LakeFeatureProperties
    {
        public Guid? DbId { get; set; }

        [JsonProperty("NAME_UKR")]
        public string NameUkr { get; set; }

        [JsonProperty("CODE_SWB")]
        public string CodeSwb { get; set; }

        [JsonProperty("TYPE_CODE")]
        public string TypeCode { get; set; }

        [JsonProperty("TYPE_NAME")]
        public string TypeName { get; set; }

        [JsonProperty("CATEGORY")]
        public string Category { get; set; }

        [JsonProperty("AREA")]
        public double? Area { get; set; }

        public override string ToString()
        {
            return $"[{NameUkr} | {CodeSwb}]";
        }
    }
}
