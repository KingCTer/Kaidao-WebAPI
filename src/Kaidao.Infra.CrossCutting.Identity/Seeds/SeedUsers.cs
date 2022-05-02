using Kaidao.Domain.Constants;
using Kaidao.Domain.IdentityEntity;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Kaidao.Infra.CrossCutting.Identity.Seeds;

public class SeedUsers
{
    public static void EnsureSeedUsers(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<AuthDbContext>();
            context!.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();


            var user1 = userMgr.FindByNameAsync("admin").Result;
            if (user1 == null)
            {
                user1 = new AppUser
                {
                    Id = "admin",
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false
                };
                var result = userMgr.CreateAsync(user1, "Admin@123$").Result;
                if (result.Succeeded)
                {
                    var user = userMgr.FindByNameAsync("admin").Result;
                    result = userMgr.AddToRoleAsync(user, IdentityConstant.Roles.Admin).Result;
                }
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("admin created");
            }
            else
            {
                Log.Debug("admin already exists");
            }

            var user2 = userMgr.FindByNameAsync("user").Result;
            if (user2 == null)
            {
                user2 = new AppUser
                {
                    Id = "user",
                    UserName = "user",
                    Email = "user@gmail.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false
                };
                var result = userMgr.CreateAsync(user2, "Admin@123$").Result;
                if (result.Succeeded)
                {
                    var user = userMgr.FindByNameAsync("user").Result;
                    result = userMgr.AddToRoleAsync(user, IdentityConstant.Roles.User).Result;
                }
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("user created");
            }
            else
            {
                Log.Debug("user already exists");
            }
        }
    }
}