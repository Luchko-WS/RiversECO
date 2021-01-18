using System;

namespace RiversECO.Dtos.Requests
{
    public class CreateReviewRequestDto
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public string CriteriaName { get; set; }
        public Guid WaterObjectId { get; set; }
        public int? Influence { get; set; }
        public int? GlobalInfluence { get; set; }
        public string Reference { get; set; }
    }
}
