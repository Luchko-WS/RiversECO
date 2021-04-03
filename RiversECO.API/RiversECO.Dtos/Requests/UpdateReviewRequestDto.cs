using System;
using System.Text.Json.Serialization;
using RiversECO.Dtos.JsonConverters;
using RiversECO.Models;

namespace RiversECO.Dtos.Requests
{
    public class UpdateReviewRequestDto
    {
        public Guid Id { get; set; }
        
        public string Comment { get; set; }
        
        public string ModifiedBy { get; set; }
        
        public bool IsAnonymous { get; set; }
        
        public string CriteriaName { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<Influence?>))]
        public Influence? Influence { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<ReferenceType?>))]
        public ReferenceType? ReferenceType { get; set; }
        
        public string Reference { get; set; }
    }
}
