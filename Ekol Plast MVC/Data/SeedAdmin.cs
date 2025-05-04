using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Ekol_Plast_MVC.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Define the admin role
            string adminRole = "Admin";

            // Check if the role already exists
            var roleExist = await roleManager.RoleExistsAsync(adminRole);
            if (!roleExist)
            {
                // Create the role if it doesn't exist
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Check if the admin user already exists
            var adminUser = await userManager.FindByEmailAsync("elvirbf4@gmail.com");

            if (adminUser == null)
            {
                // Create the admin user with a plain text password
                adminUser = new IdentityUser
                {
                    UserName = "elvirbf4@gmail.com",
                    Email = "elvirbf4@gmail.com"
                };

                // This will automatically hash the password internally
                var result = await userManager.CreateAsync(adminUser, "Titanforce1!");
                if (result.Succeeded)
                {
                    // Add the user to the admin role
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        }

    }
}
