using System;
using System.Threading.Tasks;
using LmmPlanner.Data.TheocData;
using LmmPlanner.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LmmPlanner.Data;

public class MyWriterContext : DbContext
{

    private AppSettings appSettings;

    public virtual DbSet<LmmScheduleWrite> LmmSchedules { get; set; } = null!;
    public virtual DbSet<LmmAssignmentWrite> LmmAssignments { get; set; } = null!;
    public virtual DbSet<UnavailableWrite> Unavailables { get; set; } = null!;
    public MyWriterContext(AppSettings appSettings)
    {
        this.appSettings = appSettings;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string path = appSettings.LmmConnectionString;
            optionsBuilder
            .EnableSensitiveDataLogging(true)
            .LogTo(Console.Write)
            .UseSqlite($"data source={path}");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<LmmScheduleWrite>(entity =>
            {
                entity.ToTable("lmm_schedule");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.Roworder)
                    .HasColumnType("int")
                    .HasColumnName("roworder");

                entity.Property(e => e.Source).HasColumnName("source");

                entity.Property(e => e.StudyNumber)
                    .HasColumnType("int")
                    .HasColumnName("study_number");

                entity.Property(e => e.TalkId)
                    .HasColumnType("INT")
                    .HasColumnName("talk_id");

                entity.Property(e => e.Theme).HasColumnName("theme");

                entity.Property(e => e.Time)
                    .HasColumnType("INT")
                    .HasColumnName("time");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<LmmAssignmentWrite>(entity =>
            {
                entity.ToTable("lmm_assignment");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AssigneeId)
                    .HasColumnType("INT")
                    .HasColumnName("assignee_id")
                    .HasDefaultValueSql("- 1");

                entity.Property(e => e.AssistantId)
                    .HasColumnType("INT")
                    .HasColumnName("assistant_id")
                    .HasDefaultValueSql("- 1");

                entity.Property(e => e.Classnumber)
                    .HasColumnType("INT")
                    .HasColumnName("classnumber")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Completed)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("completed")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.LmmScheduleId)
                    .HasColumnType("INT")
                    .HasColumnName("lmm_schedule_id")
                    .HasDefaultValueSql("- 1");

                entity.Property(e => e.Note)
                    .HasColumnType("VARCHAR (255)")
                    .HasColumnName("note");

                entity.Property(e => e.Setting)
                    .HasColumnType("VARCHAR(200)")
                    .HasColumnName("setting");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Timing)
                    .HasColumnType("VARCHAR (255)")
                    .HasColumnName("timing");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.VolunteerId)
                    .HasColumnType("INT")
                    .HasColumnName("volunteer_id")
                    .HasDefaultValueSql("- 1");
            });

            modelBuilder.Entity<UnavailableWrite>(entity =>
            {
                entity.ToTable("unavailables");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.EndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("end_date");

                entity.Property(e => e.PersonId)
                    .HasColumnType("INT")
                    .HasColumnName("person_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("start_date");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });
    }
    public async Task<TaskResult> SaveChangesAsync()
    {
        try
        {
            await base.SaveChangesAsync();
            return new() { Success = true };
        }
        catch (System.Exception ex)
        {
            return new(ex.ToString());
        }
    }
}
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

    public async Task<TaskResult> SaveChangesAsync()
    {
        try
        {
            await base.SaveChangesAsync();
            return new() { Success = true };
        }
        catch (System.Exception ex)
        {
            return new(ex.ToString());
        }
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

        modelBuilder.Entity<LmmAssignment>().Property(p => p.Date).HasColumnType("date");
        modelBuilder.Entity<LmmSchedule>().Property(p => p.Date).HasColumnType("date");
        // modelBuilder.Entity<LmmSchedule>().Property(p => p.Date)
        // .HasConversion<DateOnlyConverter, DateOnlyComparer>()
        // .HasColumnType("date");

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
