namespace RiversECO.Models
{
    public abstract class WaterObject : ModelBase
    {
        public abstract string Type { get; }
        public string Code { get; set; }
        public string Geometry { get; set; }
    }
}
