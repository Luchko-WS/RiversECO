using System;
using System.Text.Json.Serialization;
using RiversECO.Dtos.Common;
using RiversECO.Models;

namespace RiversECO.Dtos.Responses
{
    public class ReviewDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsAnonymous { get; set; }

        public string Comment { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public ReviewCriteriaDto Criteria { get; set; }

        public WaterObjectDto WaterObject { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Influence Influence { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ReferenceType ReferenceType { get; set; }

        public string Reference { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ReviewStatus Status { get; set; }

        public double Certainty { get; set; }
    }
}
