using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManagement.Api.Context.TimeManagement;
using TimeManagement.BL.Entries;
using TimeManagement.BL.Services;

namespace TimeManagement.Api.Services.Entries
{
    public class TimeEntryRepositoryDb : ITimeEntryRepository
    {
        private readonly TimeManagementDbContext _context;
        private readonly ILogger<TimeEntryRepositoryDb> _logger;
        public TimeEntryRepositoryDb(TimeManagementDbContext context, ILogger<TimeEntryRepositoryDb> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TimeEntry> CreateAsync(TimeEntry entry)
        {
            TimeEntry createdEntry = null;
            var existingEntry = await _context.TimeEntries
                .FirstOrDefaultAsync(e => e.UserId == entry.UserId && e.Date.Date == entry.Date && e.EntryType == entry.EntryType);
            if (existingEntry == null)
            {
                createdEntry = new TimeEntry
                {
                    UserId = entry.UserId,
                    Date = entry.Date,
                    CreatedAt = DateTime.Now,
                    LastModified = DateTime.Now,
                    Time = entry.Time,
                    EntryType = entry.EntryType
                };

                _context.TimeEntries.Add(createdEntry);
                if (!(await SaveChangesAsync()))
                {
                    return null;
                }
            }
            return createdEntry;
        }

        public async Task<bool> DeleteAsync(string entryId)
        {
            var entry = await GetAsync(entryId);
            _context.TimeEntries.Remove(entry);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteOfUserAsync(string userId)
        {
            var entries = await GetAllOfUserAsync(userId);
            _context.TimeEntries.RemoveRange(entries);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteOfUserAsync(string userId, int month)
        {
            var entries = await GetAllOfUserAsync(userId, month);
            _context.TimeEntries.RemoveRange(entries);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteOfUserAsync(string userId, int month, int day)
        {
            var entries = await GetAllOfUserAsync(userId, month, day);
            _context.TimeEntries.RemoveRange(entries);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteOfUserAsync(string userId, int month, int day, int year)
        {
            var entries = await GetAllOfUserAsync(userId, month, day, year);
            _context.TimeEntries.RemoveRange(entries);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<TimeEntry>> GetAllAsync()
        {
            return await _context.TimeEntries.ToListAsync();
        }

        public async Task<IEnumerable<TimeEntry>> GetAllOfUserAsync(string userId)
        {
            return await _context.TimeEntries
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeEntry>> GetAllOfUserAsync(string userId, int month)
        {
            return await _context.TimeEntries
                .Where(e => e.UserId == userId)
                .Where(e => e.Date.Month == month)
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeEntry>> GetAllOfUserAsync(string userId, int month, int day)
        {
            return await _context.TimeEntries
                .Where(e => e.UserId == userId)
                .Where(e => e.Date.Month == month)
                .Where(e => e.Date.Day == day)
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeEntry>> GetAllOfUserAsync(string userId, int month, int day, int year)
        {
            return await _context.TimeEntries
                .Where(e => e.UserId == userId)
                .Where(e => e.Date.Month == month)
                .Where(e => e.Date.Day == day)
                .Where(e => e.Date.Year == year)
                .ToListAsync();
        }

        public async Task<TimeEntry> GetAsync(string entryId)
        {
            return await _context.TimeEntries.FirstOrDefaultAsync(e => e.Id == entryId);
        }

        public async Task<TimeEntry> UpdateAsync(string entryId, TimeEntry entry)
        {
            var dbEntry = await GetAsync(entryId);
            dbEntry.Time = entry.Time;
            dbEntry.LastModified = DateTime.Now;
            dbEntry.LastModifierId = entry.LastModifierId;

            _context.TimeEntries.Update(dbEntry);
            await SaveChangesAsync();

            return dbEntry;
        }

        private async Task<bool> SaveChangesAsync()
        {
            bool result = false;
            try
            {
                await _context.SaveChangesAsync();
                result = true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(e, "Could not save changes to context");
                result = false;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, "Could not save changes to context");
                result = false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not save changes to context");
                throw;
            }
            return result;
        }
    }
}
