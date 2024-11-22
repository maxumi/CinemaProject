using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cinema.Domain.Enums;
namespace Cinema.Application.DTOs.User
{
    public class CreateUserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
