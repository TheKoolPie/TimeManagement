using TimeManagement.Api.Models.Entries;
using TimeManagement.BL.Entries;

namespace TimeManagement.Api.Extensions
{
    public static class ModelDbEntityConverter
    {
        public static TimeEntry ToDbEntity(this TimeEntryModel model)
        {
            return new TimeEntry
            {
                Date = model.Date,
                Time = new System.TimeSpan(model.Hours, model.Minutes, model.Seconds),
                EntryType = model.EntryType
            };
        }
        public static TimeEntryModel ToResponseModel(this TimeEntry dbEntry)
        {
            return new TimeEntryModel
            {
                Date = dbEntry.Date,
                EntryType = dbEntry.EntryType,
                Hours = dbEntry.Time.Hours,
                Minutes = dbEntry.Time.Minutes,
                Seconds = dbEntry.Time.Seconds
            };
        }
    }
}
