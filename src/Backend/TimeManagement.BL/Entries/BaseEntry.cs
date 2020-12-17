using System;

namespace TimeManagement.BL.Entries
{
    public class BaseEntry
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastModifierId { get; set; }
        public DateTime LastModified { get; set; }

        public BaseEntry()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            LastModified = DateTime.Now;
        }

        public BaseEntry(BaseEntry e)
        {
            Id = e.Id;
            UserId = e.UserId;
            Date = e.Date;
            CreatorId = e.CreatorId;
            CreatedAt = e.CreatedAt;
            LastModifierId = e.LastModifierId;
            LastModified = e.LastModified;
        }
    }
}
