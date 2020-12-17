using System.Collections.Generic;
using System.Threading.Tasks;
using TimeManagement.BL.Entries;

namespace TimeManagement.BL.Services
{
    public interface ITimeEntryRepository
    {
        Task<IEnumerable<TimeEntry>> GetAllAsync();
        Task<IEnumerable<TimeEntry>> GetAllOfUserAsync(string userId);
        Task<IEnumerable<TimeEntry>> GetAllOfUserAsync(string userId, int month);
        Task<IEnumerable<TimeEntry>> GetAllOfUserAsync(string userId, int month, int day);
        Task<IEnumerable<TimeEntry>> GetAllOfUserAsync(string userId, int month, int day, int year);
        Task<TimeEntry> GetAsync(string entryId);
        Task<TimeEntry> CreateAsync(TimeEntry entry);
        Task<TimeEntry> UpdateAsync(string entryId, TimeEntry entry);
        Task<bool> DeleteAsync(string entryId);
        Task<bool> DeleteOfUserAsync(string userId);
        Task<bool> DeleteOfUserAsync(string userId, int month);
        Task<bool> DeleteOfUserAsync(string userId, int month, int day);
        Task<bool> DeleteOfUserAsync(string userId, int month, int day, int year);
    }
}
