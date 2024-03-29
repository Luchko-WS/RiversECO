﻿using System;

namespace RiversECO.Dtos.Responses
{
    public class EntityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
