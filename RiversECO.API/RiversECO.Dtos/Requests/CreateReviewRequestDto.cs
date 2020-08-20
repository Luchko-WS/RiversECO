namespace RiversECO.Dtos.Requests
{
    public class CreateReviewRequestDto
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public ReviewCriteriaDto[] Criterias { get; set; }
    }
}
