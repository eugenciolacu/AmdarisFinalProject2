using System.Linq;
using System.Threading.Tasks;
using AmdarisInternship.Domain;
using AmdarisInternship.Domain.Auth;
using AmdarisInternship.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace AmdarisInternship.API
{
    public class Seed
    {
        public static async Task SeedInitialData (UserManager<User> userManager, RoleManager<Role> roleManager, AppDbContext appDbContext)
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
                    UserName = "eugenciolacu@gmail.com",
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

                var lector = new User()
                {
                    UserName = "eugenciolacu@yahoo.com",
                    Email = "eugenciolacu@yahoo.com",

                    FirstName = "LecturerFirstName",
                    LastName = "LecturerLastName",
                    Skype = null,
                    AvatarExtension = null,
                    AvatarName = null,
                    Avatar = null,
                    MentroId = null
                };

                await userManager.CreateAsync(lector, "Admin@123");

                if (await roleManager.RoleExistsAsync(UserRoles.Lecturer))
                {
                    await userManager.AddToRoleAsync(lector, UserRoles.Lecturer);
                }

                var mentor = new User()
                {
                    UserName = "ion@yahoo.com",
                    Email = "ion@yahoo.com",

                    FirstName = "Ion",
                    LastName = "IonIon",
                    Skype = null,
                    AvatarExtension = null,
                    AvatarName = null,
                    Avatar = null,
                    MentroId = null
                };

                await userManager.CreateAsync(mentor, "Admin@123");

                if (await roleManager.RoleExistsAsync(UserRoles.Mentor))
                {
                    await userManager.AddToRoleAsync(mentor, UserRoles.Mentor);
                }

                var intern = new User()
                {
                    UserName = "intern@yahoo.com",
                    Email = "intern@yahoo.com",

                    FirstName = "InternFirstName",
                    LastName = "InternLastName",
                    Skype = null,
                    AvatarExtension = null,
                    AvatarName = null,
                    Avatar = null,
                    MentroId = mentor.Id
                };

                await userManager.CreateAsync(intern, "Admin@123");

                if (await roleManager.RoleExistsAsync(UserRoles.Intern))
                {
                    await userManager.AddToRoleAsync(intern, UserRoles.Intern);
                }
            }

            if (!appDbContext.Modules.Any())
            {
                var module = new Module()
                {
                    Name = "C#"
                };

                appDbContext.Modules.Add(module);
                appDbContext.SaveChanges();

                var moduleGrading1 = new ModuleGrading()
                {
                    Name = "Quiz 1",
                    Weight = 0.5f,
                    Module = module,
                };

                var moduleGrading2 = new ModuleGrading()
                {
                    Name = "Quiz 2",
                    Weight = 0.5f,
                    Module = module,
                };

                appDbContext.ModuleGradings.Add(moduleGrading1);
                appDbContext.ModuleGradings.Add(moduleGrading2);
                appDbContext.SaveChanges();
            }

            if (!appDbContext.Promotions.Any())
            {
                var promotion = new Promotion()
                {
                    Name = "Winter 2021",
                    StartDate = new System.DateTime(2021, 1, 1),
                    EndDate = new System.DateTime(2021, 3, 30)
                };

                appDbContext.Promotions.Add(promotion);
                appDbContext.SaveChanges();

                var promotionModule = new PromotionModule()
                {
                    Module = appDbContext.Modules.FirstOrDefault(x => x.Name == "C#"),
                    Promotion = promotion
                };

                appDbContext.PromotionModules.Add(promotionModule);
                appDbContext.SaveChanges();

                var userpromotion = new UserPromotion()
                {
                    UserId = appDbContext.Users.FirstOrDefault(x => x.UserName == "intern@yahoo.com").Id,
                    PromotionId = promotion.Id                    
                };

                appDbContext.UserPromotions.Add(userpromotion);
                appDbContext.SaveChanges();
            }

            if (!appDbContext.Lessons.Any())
            {
                var lesson1 = new Lesson()
                {
                    Name = "Intership intro",
                    Description = "Introduction",
                    StartTime = new System.DateTime(2021, 1, 2, 9, 0, 0),
                    EndTime = new System.DateTime(2021, 1, 2, 10, 0, 0),
                    LecturerId = appDbContext.Users.FirstOrDefault(x => x.UserName == "eugenciolacu@yahoo.com").Id,
                    ModuleId = appDbContext.Modules.FirstOrDefault(x => x.Name == "C#").Id,
                    PromotionId = appDbContext.Promotions.FirstOrDefault(x => x.Name == "Winter 2021").Id
                };

                appDbContext.Lessons.Add(lesson1);

                var lesson2 = new Lesson()
                {
                    Name = "Second lesson",
                    Description = "Second lesson description",
                    StartTime = new System.DateTime(2021, 1, 3, 9, 0, 0),
                    EndTime = new System.DateTime(2021, 1, 3, 10, 0, 0),
                    LecturerId = appDbContext.Users.FirstOrDefault(x => x.UserName == "eugenciolacu@yahoo.com").Id,
                    ModuleId = appDbContext.Modules.FirstOrDefault(x => x.Name == "C#").Id,
                    PromotionId = appDbContext.Promotions.FirstOrDefault(x => x.Name == "Winter 2021").Id
                };

                appDbContext.Lessons.Add(lesson2);

                appDbContext.SaveChanges();
            }

        }
    }
}
