using System;

namespace RiversECO.Dtos.Responses
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public string City { get; set; }
    }
}
