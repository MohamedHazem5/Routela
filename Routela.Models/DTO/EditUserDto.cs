﻿using Microsoft.AspNetCore.Http;

namespace Routela.Models.DTO
{
    public class EditUserDto
    {

        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public IFormFile formFile { get; set; }

    }
}
