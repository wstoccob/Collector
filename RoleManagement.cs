using Microsoft.AspNetCore.Identity;

namespace Collector;

public static class RoleManagement
{
    public static async Task RoleCreate(RoleManager<IdentityRole> roleManager)
    {
        var roleNames = new[] { "User", "Admin" };

        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}