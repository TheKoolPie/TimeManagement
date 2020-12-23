using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TimeManagement.BL.Services;
using TimeManagement.BL.User;
using TimeManagementApi.Context.Authentication;

namespace TimeManagement.Api.Services.Users
{
    public class UserRepositoryDb : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepositoryDb(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserOverview> GetUserOverviewAsync(string userId)
        {
            UserOverview overview = null;
            var dbUser = await _userManager.FindByIdAsync(userId);
            if (dbUser != null)
            {
                overview = new UserOverview
                {
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    Username = dbUser.UserName
                };
            }
            return overview;
        }
    }
}
