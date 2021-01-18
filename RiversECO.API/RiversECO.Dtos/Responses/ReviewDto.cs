using System;
using System.Collections.Generic;
using RiversECO.Dtos.Common;

namespace RiversECO.Dtos.Responses
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public ReviewCriteriaDto Criteria { get; set; }
        public WaterObjectDto WaterObject { get; set; }
        public int? Influence { get; set; }
        public int? GlobalInfluence { get; set; }
        public string Reference { get; set; }
    }
}
