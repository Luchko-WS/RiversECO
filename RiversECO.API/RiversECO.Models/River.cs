using System.ComponentModel.DataAnnotations.Schema;

namespace RiversECO.Models
{
    public class River : WaterObject
    {
        public string Code { get; set; }
        public double LengthKm { get; set; }

        [NotMapped]
        public override string Type => "River";
    }
}
