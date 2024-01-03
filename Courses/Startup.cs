using Courses.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Courses.Startup))]
namespace Courses
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }
        public void CreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);

                ApplicationUser user = new ApplicationUser();
                user.UserName = "Sanjoli63";
                user.Email = "sanjoligo6mar@gmail.com";
                user.UserType = "Admin";
                user.Dob = new DateTime(2000, 03, 06);
               //"Sanjoli" is the password.
                var check = userManager.Create(user, "Sanjoli");

                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                    db.SaveChanges();

                }
                else
                {
                    foreach (var error in check.Errors)
                    {
                        Console.WriteLine($"Error: {error}");
                    }
                    var e = new Exception("Could not add default user");
                    foreach (var error in check.Errors)
                    {
                        e.Data.Add("IdentityError", error);
                    }
                    throw e;

                }
            }
        }
    }
}
