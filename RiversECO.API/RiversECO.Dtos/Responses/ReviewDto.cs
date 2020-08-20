using System;

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
        public CriteriaDto[] Criterias { get; set; }
    }
}
