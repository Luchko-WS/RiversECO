﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RiversECO.Models
{
    public abstract class ModelBase
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
