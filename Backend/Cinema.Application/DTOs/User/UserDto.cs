using System.ComponentModel.DataAnnotations;
using Cinema.Domain.Enums;
namespace Cinema.Application.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        public Role Role { get; set; }
    }
}
