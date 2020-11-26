using AmdarisInternship.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace AmdarisInternship.API
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User()
                {
                    UserName = "admin",
                    Email = "eugenciolacu@gmail.com",

                    FirstName = "Eugeniu",
                    LastName = "Ciolacu",
                    Skype = null,
                    AvatarExtension = null,
                    AvatarName = null,
                    Avatar = null,
                    MentroId = null
                };

                await userManager.CreateAsync(user, "Admin@123");
            }
        }
    }
}
