using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiversECO.Models
{
    public class WaterObject : ModelBase
    {
        [Required]
        public WaterObjectType Type { get; set; }

        [Required]
        public string CodeSwb { get; set; }

        public string TypeCode { get; set; }

        public string TypeName { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
