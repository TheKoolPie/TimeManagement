using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManagementApi.Context.Authentication;

namespace TimeManagement.Api.Services.Users
{
    public interface ICurrentUserProvider
    {
        Task<ApplicationUser> GetCurrentUserAsync();
    }
}
