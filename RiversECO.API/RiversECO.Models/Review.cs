using System;
using System.ComponentModel.DataAnnotations;

namespace RiversECO.Models
{
    public class Review : ModelBase
    {

        [Required]
        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }

        [Required]
        public Guid CriteriaId { get; set; }
        
        public virtual Criteria Criteria { get; set; }

        [Required]
        public Guid WaterObjectId { get; set; }

        public virtual WaterObject WaterObject { get; set; }

        public int? Influence { get; set; }

        public int? GlobalInfluence { get; set; }

        [Required]
        public string Reference { get; set; }

        public string Comment { get; set; }

        public ReviewStatus Status { get; set; }
    }
}
