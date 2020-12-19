using System;

namespace RiversECO.Dtos.Requests
{
    public class UpdateReviewRequestDto
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public string ModifiedBy { get; set; }
        public string CriteriaName { get; set; }
    }
}
