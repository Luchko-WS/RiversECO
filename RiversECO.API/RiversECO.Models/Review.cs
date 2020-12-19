using System;
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

        [Required]
        public Guid CriteriaId { get; set; }
        
        public virtual Criteria Criteria { get; set; }

        [Required]
        public Guid WaterObjectId { get; set; }

        public virtual WaterObject WaterObject { get; set; }
    }
}
