using System;
using Newtonsoft.Json;

namespace GeoJSONMigrationTool.Models
{
    public class RiverFeatureProperties
    {
        public Guid? DbId { get; set; }

        [JsonProperty("NAME_UKR")]
        public string NameUkr { get; set; }

        [JsonProperty("FLOW_TO")]
        public string FlowTo { get; set; }

        [JsonProperty("TYPE_CODE")]
        public string TypeCode { get; set; }

        [JsonProperty("TYPE_NAME1")]
        public string TypeName { get; set; }

        [JsonProperty("LENGTH_km")]
        public double? LengthKm { get; set; }

        [JsonProperty("CATEGORY")]
        public string Category { get; set; }

        [JsonProperty("CODE_SWB")]
        public string CodeSwb { get; set; }

        [JsonProperty("note_ukr")]
        public string NoteUkr { get; set; }

        public override string ToString()
        {
            return $"[{NameUkr} | {CodeSwb}]";
        }
    }
}