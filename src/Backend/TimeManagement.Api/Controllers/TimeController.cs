using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TimeManagement.Api.Models.Entries;
using TimeManagement.Api.Response;
using TimeManagement.Api.Services.Users;
using TimeManagement.BL.Services;

namespace TimeManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly ICurrentUserProvider _currentUser;
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly ILogger<TimeController> _logger;
        public TimeController(ICurrentUserProvider currentUser,
            ITimeEntryRepository timeEntryRepository,
            ILogger<TimeController> logger)
        {
            _currentUser = currentUser;
            _timeEntryRepository = timeEntryRepository;
            _logger = logger;
        }

        [HttpGet("Entries/{userId}")]
        public async Task<ActionResult<TimeEntryResponse>> GetAllOfUserAsync(string userId)
        {
            return await GetAllOfUserAsync(userId, 0, 0, 0);
        }

        [HttpGet("Entries/{userId}/{month}")]
        public async Task<ActionResult<TimeEntryResponse>> GetAllOfUserAsync(string userId, int month)
        {
            return await GetAllOfUserAsync(userId, month, 0, 0);
        }
        [HttpGet("Entries/{userId}/{month}/{day}")]
        public async Task<ActionResult<TimeEntryResponse>> GetAllOfUserAsync(string userId, int month, int day)
        {
            return await GetAllOfUserAsync(userId, month, day, 0);
        }
        [HttpGet("Entries/{userId}/{month}/{day}/{year}")]
        public async Task<ActionResult<TimeEntryResponse>> GetAllOfUserAsync(string userId, int month, int day, int year)
        {
            var result = new TimeEntryResponse();

            var currentUser = await _currentUser.GetCurrentUserAsync();
            if (userId != currentUser.Id)
            {
                result.IsSuccess = false;
                result.Message = $"User not authorized to access resources owned by '{userId}'";
                return Unauthorized(result);
            }
            if (month > 0)
            {
                _logger.LogDebug($"Found value for month '{month}'");
                if (day > 0)
                {
                    _logger.LogDebug($"Found value for day '{day}'");
                    if (year > 0)
                    {
                        _logger.LogDebug($"Found value for year '{year}'");
                        result.Entries = await _timeEntryRepository.GetAllOfUserAsync(userId, month, day, year);
                    }
                    else
                    {
                        result.Entries = await _timeEntryRepository.GetAllOfUserAsync(userId, month, day);
                    }
                }
                else
                {
                    result.Entries = await _timeEntryRepository.GetAllOfUserAsync(userId, month);
                }
            }
            else
            {
                result.Entries = await _timeEntryRepository.GetAllOfUserAsync(userId);

            }
            result.IsSuccess = result.Entries != null;
            _logger.LogDebug($"Data was successfully read: '{result.IsSuccess}'");
            return Ok(result);
        }
    }
}
