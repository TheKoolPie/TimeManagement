using System.Threading.Tasks;
using TimeManagement.BL.User;

namespace TimeManagement.BL.Services
{
    public interface IUserRepository
    {
        Task<UserOverview> GetUserOverviewAsync(string userId);
    }
}
