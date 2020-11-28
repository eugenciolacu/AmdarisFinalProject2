using System;
using System.ComponentModel.DataAnnotations;

namespace AmdarisInternship.API.Dtos.Account
{
    public class UserForRegisterDto
    {
        // AspNetUsers
        //[Required(ErrorMessage = "User name is required")]
        //public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string UserRole { get; set; }

        // Users
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        public string Skype { get; set; }

        public string AvatarExtension { get; set; }

        public string AvatarName { get; set; }

        public byte[] Avatar { get; set; }

        public Nullable<int> MentroId { get; set; }
    }
}
