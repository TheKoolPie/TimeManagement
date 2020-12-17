using System.Collections.Generic;
using TimeManagement.BL.Entries;

namespace TimeManagement.Api.Response
{
    public class TimeEntryResponse : BaseResponse
    {
        public IEnumerable<TimeEntry> Entries { get; set; }
    }
}
