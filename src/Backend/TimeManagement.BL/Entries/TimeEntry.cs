using System;
using TimeManagement.BL.Enums;

namespace TimeManagement.BL.Entries
{
    public class TimeEntry : BaseEntry
    {
        public TimeSpan Time { get; set; }

        public TimeEntryType EntryType { get; set; }

        public TimeEntry() : base() { }
        public TimeEntry(TimeEntry e) : base(e)
        {
            Time = e.Time;
            EntryType = e.EntryType;
        }
    }
}
