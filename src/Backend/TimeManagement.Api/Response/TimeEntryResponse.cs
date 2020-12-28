using System.Collections.Generic;
using TimeManagement.Api.Models.Entries;

namespace TimeManagement.Api.Response
{
    public class TimeEntryResponse : BaseResponse
    {
        public IEnumerable<TimeEntryModel> Entries { get; set; }
    }
}
