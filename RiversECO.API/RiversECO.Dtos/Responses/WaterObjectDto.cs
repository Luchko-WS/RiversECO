namespace RiversECO.Dtos.Responses
{
    public class WaterObjectDto : EntityDto
    {
        public string Type { get; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
