using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagement.Api.Exceptions
{
    public class TimeEntryNotFoundException : Exception
    {
        public TimeEntryNotFoundException(string id) : base($"Could not find time entry with id '{id}'") { }
        public TimeEntryNotFoundException(string id, Exception inner):base($"Could not find time entry with id '{id}'", inner) { }
    }
}
