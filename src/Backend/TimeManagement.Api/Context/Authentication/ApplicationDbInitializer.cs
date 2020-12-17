using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManagement.Api.Settings;
using TimeManagementApi.Context.Authentication;

namespace TimeManagement.Api.Context.Authentication
{
    public static class ApplicationDbInitializer
    {
        public static void SeedAdminUser(UserManager<ApplicationUser> userManager, AdminSettings settings)
        {
            if (settings != null)
            {
                var existing = userManager.FindByNameAsync(settings.UserName).Result;
                if (existing == null)
                {
                    existing = new ApplicationUser
                    {
                        UserName = settings.UserName,
                        Email = settings.Email
                    };
                    var result = userManager.CreateAsync(existing, settings.Password).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception("Could not create admin user");
                    }
                }
            }
        }
    }
}
