using System;
using System.ComponentModel.DataAnnotations;
using TimeManagement.BL.Enums;

namespace TimeManagement.Api.Models.Entries
{
    public class TimeEntryModel
    {
        [Required(ErrorMessage = "Target date for the entry is needed")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Hours must be set")]
        [Range(0, 24, ErrorMessage = "Hours can only be between 0 and 24")]
        public int Hours { get; set; }
        [Required(ErrorMessage = "Minutes must be set")]
        [Range(0, 60, ErrorMessage = "Minutes can only be between 0 and 60")]
        public int Minutes { get; set; }
        [Required(ErrorMessage = "Seconds must be set")]
        [Range(0, 60, ErrorMessage = "Minutes can only be between 0 and 60")]
        public int Seconds { get; set; }

        [Required(ErrorMessage = "Entry type is needed")]
        public TimeEntryType EntryType { get; set; }
    }
}
