using System.ComponentModel.DataAnnotations;

namespace RiversECO.Models
{
    public class Review : ModelBase
    {
        [Required]
        public string Comment { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Criterias { get; set; }
    }
}
