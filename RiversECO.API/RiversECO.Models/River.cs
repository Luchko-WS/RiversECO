using System.ComponentModel.DataAnnotations.Schema;

namespace RiversECO.Models
{
    public class River : WaterObject
    {
        public double LengthKm { get; set; }

        [NotMapped]
        public override string Type => "River";
    }
}
