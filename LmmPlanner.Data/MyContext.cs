using LmmPlanner.Data.TheocData;
using Microsoft.EntityFrameworkCore;

namespace LmmPlanner.Data;

public class MyContext : TheocData.theocbase_backupContext
{
    private AppSettings _appSettings;

    public MyContext(AppSettings appSettings) : base(appSettings)
    {
        _appSettings = appSettings;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Person>()
        .HasMany(p => p.AssignmentsAsMain)
        .WithOne(p => p.MainPerson)
        .HasForeignKey(p => p.AssigneeId);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.AssignmentsAsVolunteer)
            .WithOne(p => p.VolunteerPerson)
            .HasForeignKey(p => p.VolunteerId);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.AssignmentsAsAssistant)
            .WithOne(p => p.AssistantPerson)
            .HasForeignKey(p => p.AssistantId);

        modelBuilder.Entity<LmmAssignment>()
            .HasOne(p => p.LmmSchedule)
            .WithMany(p => p.Assignments)
            .HasForeignKey(p => p.LmmScheduleId);

        modelBuilder.Entity<LmmMeeting>()
            .HasOne(p => p.ChairmanPerson)
            .WithMany(p => p.AssignmentsAsChairman)
            .HasForeignKey(p => p.Chairman);
        modelBuilder.Entity<LmmMeeting>()
            .HasOne(p => p.PrayerBeginningPerson)
            .WithMany(p => p.AssignmentsAsPrayerBeginning)
            .HasForeignKey(p => p.PrayerBeginning);
        modelBuilder.Entity<LmmMeeting>()
            .HasOne(p => p.PrayerEndPerson)
            .WithMany(p => p.AssignmentsAsPrayerEnd)
            .HasForeignKey(p => p.PrayerEnd);

        modelBuilder.Entity<TalkInfo>()
            .HasMany(p => p.Assignments)
            .WithOne(p => p.TalkInfo)
            .HasForeignKey(p => p.TalkId);
    }
}
