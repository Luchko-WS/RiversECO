using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using RiversECO.Models;

namespace RiversECO.DataContext.Seed
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                CreateRoles(roleManager);

                var adminUser = new User { UserName = "admin" };
                var result = userManager.CreateAsync(adminUser, "Passw0rd").Result;
                if (result.Succeeded)
                {
                    var admin = userManager.FindByNameAsync(adminUser.UserName).Result;
                    userManager.AddToRolesAsync(admin, new[] { "Admin" }).Wait();
                }
            }
        }

        private static void CreateRoles(RoleManager<Role> roleManager)
        {
            var roles = new List<Role>
            {
                new Role { Name = "Member" },
                new Role { Name = "Admin" }
            };

            foreach (var role in roles)
            {
                roleManager.CreateAsync(role).Wait();
            }
        }
    }
}