using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeManagement.BL.Entries;

namespace TimeManagement.Api.Context.TimeManagement.EntityBuilders
{
    public class TimeEntryConfiguration : IEntityTypeConfiguration<TimeEntry>
    {
        public void Configure(EntityTypeBuilder<TimeEntry> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Time)
                .IsRequired();
            builder.Property(e => e.EntryType)
                .IsRequired();
            //Base
            builder.Property(e => e.UserId)
                .IsRequired();
            builder.Property(e => e.Date)
                .IsRequired();
            builder.Property(e => e.CreatorId)
                .IsRequired();
            builder.Property(e => e.CreatedAt)
                .IsRequired();
        }
    }
}
