using System.ComponentModel.DataAnnotations;

namespace RiversECO.Models
{
    public class WaterObject : ModelBase
    {
        [Required]
        public WaterObjectType Type { get; set; }
        public string Code { get; set; }
    }
}
