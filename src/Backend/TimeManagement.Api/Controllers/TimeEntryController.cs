using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManagement.Api.Exceptions;
using TimeManagement.Api.Extensions;
using TimeManagement.Api.Models.Entries;
using TimeManagement.Api.Response;
using TimeManagement.Api.Services.Users;
using TimeManagement.BL.Entries;
using TimeManagement.BL.Services;

namespace TimeManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
        private readonly ICurrentUserProvider _userRepository;
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly ILogger<TimeEntryController> _logger;
        public TimeEntryController(ICurrentUserProvider userRepository,
            ITimeEntryRepository timeEntryRepository,
            ILogger<TimeEntryController> logger)
        {
            _userRepository = userRepository;
            _timeEntryRepository = timeEntryRepository;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<TimeEntryResponse>> GetAllOfUserAsync(string userId)
        {
            return await GetAllOfUserAsync(userId, 0, 0, 0);
        }
        [HttpGet("{userId}/{month}")]
        public async Task<ActionResult<TimeEntryResponse>> GetAllOfUserAsync(string userId, int month)
        {
            return await GetAllOfUserAsync(userId, month, 0, 0);
        }
        [HttpGet("{userId}/{month}/{day}")]
        public async Task<ActionResult<TimeEntryResponse>> GetAllOfUserAsync(string userId, int month, int day)
        {
            return await GetAllOfUserAsync(userId, month, day, 0);
        }
        [HttpGet("{userId}/{month}/{day}/{year}")]
        public async Task<ActionResult<TimeEntryResponse>> GetAllOfUserAsync(string userId, int month, int day, int year)
        {
            var result = new TimeEntryResponse();

            var currentUser = await _userRepository.GetCurrentUserAsync();
            if (userId != currentUser.Id)
            {
                result.IsSuccess = false;
                result.Message = $"User not authorized to access resources owned by '{userId}'";
                return Unauthorized(result);
            }
            try
            {
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
            }
            catch (PersistencyException e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
                return Conflict(result);
            }
            result.IsSuccess = result.Entries != null;
            _logger.LogDebug($"Data was successfully read: '{result.IsSuccess}'");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TimeEntryResponse>> CreateEntryAsync([FromBody] TimeEntryModel model)
        {
            var result = new TimeEntryResponse();
            var currentUser = await _userRepository.GetCurrentUserAsync();
            if (currentUser.Id != model.UserId)
            {
                result.IsSuccess = false;
                result.Message = $"Provided user cannot create entries for user '{model.UserId}'";
                return Unauthorized(result);
            }

            var dbEntry = new TimeEntry
            {
                UserId = model.UserId,
                Date = model.Date,
                Time = model.Time,
                EntryType = model.EntryType
            };
            try
            {
                var created = await _timeEntryRepository.CreateAsync(dbEntry);
                result.IsSuccess = true;
                result.Entries = new List<TimeEntry> { created };
            }
            catch (PersistencyException e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
                return Conflict(result);
            }

            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TimeEntryResponse>> UpdateEntryAsync(string id, [FromBody] TimeEntryModel model)
        {
            var result = new TimeEntryResponse();
            var currentUser = await _userRepository.GetCurrentUserAsync();
            if (currentUser.Id != model.UserId)
            {
                result.IsSuccess = false;
                result.Message = $"Provided user cannot update entries for user '{model.UserId}'";
                return Unauthorized(result);
            }
            try
            {
                var updatedItem = await _timeEntryRepository.UpdateAsync(id, model.ToDbEntity());
                result.IsSuccess = true;
                result.Entries = new List<TimeEntry> { updatedItem };
            }
            catch (TimeEntryNotFoundException e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
                return NotFound(result);
            }
            catch (PersistencyException e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
                return Conflict(result);
            }

            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<TimeEntryResponse>> DeleteOfUserAsync(string userId)
        {
            return await DeleteOfUserAsync(userId, 0, 0, 0);
        }
        [HttpDelete("{userId}/{month}")]
        public async Task<ActionResult<TimeEntryResponse>> DeleteOfUserAsync(string userId, int month)
        {
            return await DeleteOfUserAsync(userId, month, 0, 0);
        }
        [HttpDelete("{userId}/{month}/{day}")]
        public async Task<ActionResult<TimeEntryResponse>> DeleteOfUserAsync(string userId, int month, int day)
        {
            return await DeleteOfUserAsync(userId, month, day, 0);
        }
        [HttpDelete("{userId}/{month}/{day}/{year}")]
        public async Task<ActionResult<TimeEntryResponse>> DeleteOfUserAsync(string userId, int month, int day, int year)
        {
            var result = new TimeEntryResponse();
            var currentUser = await _userRepository.GetCurrentUserAsync();
            if (userId != currentUser.Id)
            {
                result.IsSuccess = false;
                result.Message = $"User not authorized to access resources owned by '{userId}'";
                return Unauthorized(result);
            }
            try
            {
                if (month > 0)
                {
                    _logger.LogDebug($"Found value for month '{month}'");
                    if (day > 0)
                    {
                        _logger.LogDebug($"Found value for day '{day}'");
                        if (year > 0)
                        {
                            _logger.LogDebug($"Found value for year '{year}'");
                            result.IsSuccess = await _timeEntryRepository.DeleteOfUserAsync(userId, month, day, year);
                        }
                        else
                        {
                            result.IsSuccess = await _timeEntryRepository.DeleteOfUserAsync(userId, month, day);
                        }
                    }
                    else
                    {
                        result.IsSuccess = await _timeEntryRepository.DeleteOfUserAsync(userId, month);
                    }
                }
                else
                {
                    result.IsSuccess = await _timeEntryRepository.DeleteOfUserAsync(userId);

                }
            }
            catch (PersistencyException e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
                return Conflict(result);
            }
            _logger.LogDebug($"Data was successfully deleted: '{result.IsSuccess}'");
            return Ok(result);
        }
    }
}
