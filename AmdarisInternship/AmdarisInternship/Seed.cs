using System.Linq;
using System.Threading.Tasks;
using AmdarisInternship.Domain.Auth;
using Microsoft.AspNetCore.Identity;

namespace AmdarisInternship.API
{
    public class Seed
    {
        public static async Task SeedInitialData (UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Administrator))
                await roleManager.CreateAsync(new Role(UserRoles.Administrator));
            if (!await roleManager.RoleExistsAsync(UserRoles.Lecturer))
                await roleManager.CreateAsync(new Role(UserRoles.Lecturer));
            if (!await roleManager.RoleExistsAsync(UserRoles.Mentor))
                await roleManager.CreateAsync(new Role(UserRoles.Mentor));
            if (!await roleManager.RoleExistsAsync(UserRoles.Intern))
                await roleManager.CreateAsync(new Role(UserRoles.Intern));

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

                if (await roleManager.RoleExistsAsync(UserRoles.Administrator))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Administrator);
                }
            }
        }
    }
}
