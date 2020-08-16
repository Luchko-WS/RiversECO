using System;

namespace RiversECO.Dtos.Requests
{
    public class UpdateCriteriaRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
