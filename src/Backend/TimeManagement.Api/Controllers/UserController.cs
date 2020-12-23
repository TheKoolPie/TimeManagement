using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeManagement.Api.Response;
using TimeManagement.Api.Services.Users;
using TimeManagement.BL.Services;
using TimeManagement.BL.User;

namespace TimeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ICurrentUserProvider _currentUser;
        private readonly IUserRepository _userRepository;
        public UserController(ICurrentUserProvider currentUser, IUserRepository userRepository)
        {
            _currentUser = currentUser;
            _userRepository = userRepository;
        }

        [HttpGet("overview")]
        public async Task<ActionResult<UserOverviewResponse>> GetUserOverviewOfCurrentUser()
        {
            UserOverviewResponse response = new UserOverviewResponse();
            var currentUser = await _currentUser.GetCurrentUserAsync();
            UserOverview overview = await _userRepository.GetUserOverviewAsync(currentUser.Id);
            if (overview == null)
            {
                response.IsSuccess = false;
                response.Message = $"Could not find user with id '{currentUser.Id}'";
                response.UserOverview = null;
                return NotFound(response);
            }

            response.IsSuccess = true;
            response.UserOverview = overview;
            return Ok(overview);
        }

        [HttpGet("overview/{userId}")]
        public async Task<ActionResult<UserOverviewResponse>> GetUserOverview(string userId)
        {
            UserOverviewResponse response = new UserOverviewResponse();
            var currentUser = await _currentUser.GetCurrentUserAsync();
            if (currentUser.Id != userId)
            {
                response.IsSuccess = false;
                response.Message = "Not authorized";
                response.UserOverview = null;
                return Unauthorized(response);
            }
            UserOverview overview = await _userRepository.GetUserOverviewAsync(userId);
            if (overview == null)
            {
                response.IsSuccess = false;
                response.Message = $"Could not find user with id '{userId}'";
                response.UserOverview = null;
                return NotFound(response);
            }

            response.IsSuccess = true;
            response.UserOverview = overview;
            return Ok(overview);
        }
    }
}
