using Microsoft.AspNetCore.Identity;

namespace Thunders.TechTest.ApiService.Repositories.Seed
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Define as roles necessárias
            string[] roles = new[] { "Internal", "Dealership" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Cria o usuário admin, se não existir
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var adminUser = new IdentityUser { UserName = "admin", Email = "admin@thunders.com" };
                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Internal");
                }
            }

            // Cria o usuário padrão, se não existir
            if (await userManager.FindByNameAsync("user") == null)
            {
                var normalUser = new IdentityUser { UserName = "user", Email = "user@thunders.com" };
                var result = await userManager.CreateAsync(normalUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "Dealership");
                }
            }
        }
    }
}
