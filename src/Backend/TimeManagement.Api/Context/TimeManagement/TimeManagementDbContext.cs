using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TimeManagement.BL.Entries;

namespace TimeManagement.Api.Context.TimeManagement
{
    public class TimeManagementDbContext : DbContext
    {
        public DbSet<TimeEntry> TimeEntries { get; set; }

        public TimeManagementDbContext() : base() { }
        public TimeManagementDbContext(DbContextOptions<TimeManagementDbContext> o) : base(o) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
