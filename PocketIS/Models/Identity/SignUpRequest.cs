﻿namespace PocketIS.Models.Identity
{
    public class SignUpRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
