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
                Time = model.Time,
                EntryType = model.EntryType
            };
        }
    }
}
