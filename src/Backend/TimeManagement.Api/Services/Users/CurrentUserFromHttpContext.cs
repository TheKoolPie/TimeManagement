using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManagementApi.Context.Authentication;

namespace TimeManagement.Api.Services.Users
{
    public class CurrentUserFromHttpContext : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CurrentUserFromHttpContext> _logger;
        public CurrentUserFromHttpContext(IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager, ILogger<CurrentUserFromHttpContext> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            ApplicationUser dbUser = null;
            string userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                dbUser = await _userManager.FindByNameAsync(userName);
                if(dbUser == null)
                {
                    dbUser = await _userManager.FindByEmailAsync(userName);
                }
            }
            return dbUser;
        }
    }
}
