using RiversECO.Dtos.JsonConverters;
using RiversECO.Models;
using System;
using System.Text.Json.Serialization;

namespace RiversECO.Dtos.Requests
{
    public class CreateReviewRequestDto
    {
        public string Name { get; set; }

        public string Comment { get; set; }

        public string CreatedBy { get; set; }

        public bool IsAnonymous { get; set; }

        public string CriteriaName { get; set; }

        public Guid? WaterObjectId { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<Influence?>))]
        public Influence? Influence { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<ReferenceType?>))]
        public ReferenceType? ReferenceType { get; set; }

        public string Reference { get; set; }
    }
}
