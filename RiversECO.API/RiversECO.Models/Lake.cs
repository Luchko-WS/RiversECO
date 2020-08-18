using System.ComponentModel.DataAnnotations.Schema;

namespace RiversECO.Models
{
    public class Lake : WaterObject
    {
        public string Code { get; set; }
        public double Area { get; set; }

        [NotMapped]
        public override string Type => "Lake";
    }
}
