using System;
using System.ComponentModel.DataAnnotations;
using TimeManagement.BL.Enums;

namespace TimeManagement.Api.Models.Entries
{
    public class TimeEntryModel
    {
        [Required(ErrorMessage = "User id is needed")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Target date for the entry is needed")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Target time for the entry is needed")]
        public TimeSpan Time { get; set; }
        [Required(ErrorMessage = "Entry type is needed")]
        public TimeEntryType EntryType { get; set; }
    }
}
