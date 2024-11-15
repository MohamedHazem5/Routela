﻿using Microsoft.AspNetCore.Http;

namespace Routela.Models.DTO
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile formFile { get; set; }
    }
}
