using System;
using System.ComponentModel.DataAnnotations;

namespace RiversECO.Models
{
    public class Review : ModelBase
    {
        [Required]
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
        
        public bool IsAnonymous { get; set; }

        [Required]
        public Guid CriteriaId { get; set; }
        
        public virtual Criteria Criteria { get; set; }

        [Required]
        public Guid WaterObjectId { get; set; }

        public virtual WaterObject WaterObject { get; set; }

        [Required]
        public Influence Influence { get; set; }

        [Required]
        public ReferenceType ReferenceType { get; set; }

        [Required]
        public string Reference { get; set; }

        public string Comment { get; set; }

        public double Certainty { get; set; }

        public ReviewStatus Status { get; set; }
    }
}
