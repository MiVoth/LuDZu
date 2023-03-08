using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LmmPlanner.Data.TheocData
{
    public partial class theocbase_backupContext : DbContext
    {
        private AppSettings appSettings;

        public theocbase_backupContext(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        // public theocbase_backupContext(DbContextOptions<theocbase_backupContext> options)
        //     : base(options)
        // {
        // }

        public virtual DbSet<BiblestudyMeeting> BiblestudyMeetings { get; set; } = null!;
        public virtual DbSet<Cityterritory> Cityterritories { get; set; } = null!;
        public virtual DbSet<Congregation> Congregations { get; set; } = null!;
        public virtual DbSet<Congregationmeetingtime> Congregationmeetingtimes { get; set; } = null!;
        public virtual DbSet<EReminder> EReminders { get; set; } = null!;
        public virtual DbSet<Exception> Exceptions { get; set; } = null!;
        public virtual DbSet<Family> Families { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<LmmAssignment> LmmAssignments { get; set; } = null!;
        public virtual DbSet<LmmMeeting> LmmMeetings { get; set; } = null!;
        public virtual DbSet<LmmSchedule> LmmSchedules { get; set; } = null!;
        public virtual DbSet<LmmScheduleAssist> LmmScheduleAssists { get; set; } = null!;
        public virtual DbSet<LmmStudy> LmmStudies { get; set; } = null!;
        public virtual DbSet<LmmWorkbookregex> LmmWorkbookregices { get; set; } = null!;
        public virtual DbSet<Locale> Locales { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<Outgoing> Outgoings { get; set; } = null!;
        public virtual DbSet<Person> Persons { get; set; } = null!;
        public virtual DbSet<PersonidlmmhistoryMt> PersonidlmmhistoryMts { get; set; } = null!;
        public virtual DbSet<Personmidweek> Personmidweeks { get; set; } = null!;
        public virtual DbSet<PersonschoolhistoryMt> PersonschoolhistoryMts { get; set; } = null!;
        public virtual DbSet<Publicmeeting> Publicmeetings { get; set; } = null!;
        public virtual DbSet<Publicmeetinghistory> Publicmeetinghistories { get; set; } = null!;
        public virtual DbSet<Publictalk> Publictalks { get; set; } = null!;
        public virtual DbSet<PublictalksTodolist> PublictalksTodolists { get; set; } = null!;
        public virtual DbSet<School> Schools { get; set; } = null!;
        public virtual DbSet<SchoolBreak> SchoolBreaks { get; set; } = null!;
        public virtual DbSet<SchoolSchedule> SchoolSchedules { get; set; } = null!;
        public virtual DbSet<SchoolSetting> SchoolSettings { get; set; } = null!;
        public virtual DbSet<Schoolhistory> Schoolhistories { get; set; } = null!;
        public virtual DbSet<SchoolhistoryMt> SchoolhistoryMts { get; set; } = null!;
        public virtual DbSet<ServiceMeeting> ServiceMeetings { get; set; } = null!;
        public virtual DbSet<ServiceProgram> ServicePrograms { get; set; } = null!;
        public virtual DbSet<Servicemeetinghistory> Servicemeetinghistories { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<Song> Songs { get; set; } = null!;
        public virtual DbSet<SpeakerPublictalk> SpeakerPublictalks { get; set; } = null!;
        public virtual DbSet<StudentStudy> StudentStudies { get; set; } = null!;
        public virtual DbSet<Study> Studies { get; set; } = null!;
        public virtual DbSet<TalkInfo> TalkInfos { get; set; } = null!;
        public virtual DbSet<Territory> Territories { get; set; } = null!;
        public virtual DbSet<Territory1> Territories1 { get; set; } = null!;
        public virtual DbSet<TerritoryAddress> TerritoryAddresses { get; set; } = null!;
        public virtual DbSet<TerritoryAddresstype> TerritoryAddresstypes { get; set; } = null!;
        public virtual DbSet<TerritoryAssignment> TerritoryAssignments { get; set; } = null!;
        public virtual DbSet<TerritoryCity> TerritoryCities { get; set; } = null!;
        public virtual DbSet<TerritoryStreet> TerritoryStreets { get; set; } = null!;
        public virtual DbSet<TerritoryStreettype> TerritoryStreettypes { get; set; } = null!;
        public virtual DbSet<TerritoryType> TerritoryTypes { get; set; } = null!;
        public virtual DbSet<Territoryaddress1> Territoryaddresses1 { get; set; } = null!;
        public virtual DbSet<Territoryassignment1> Territoryassignments1 { get; set; } = null!;
        public virtual DbSet<Territorystreet1> Territorystreets1 { get; set; } = null!;
        public virtual DbSet<TheocbaseSequence> TheocbaseSequences { get; set; } = null!;
        public virtual DbSet<Unavailable> Unavailables { get; set; } = null!;
        public virtual DbSet<WtArticleDate> WtArticleDates { get; set; } = null!;

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
            modelBuilder.Entity<BiblestudyMeeting>(entity =>
            {
                entity.ToTable("biblestudyMeeting");

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

                entity.Property(e => e.Material)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("material");

                entity.Property(e => e.PersonId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("person_id");

                entity.Property(e => e.PrayerId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("prayer_id");

                entity.Property(e => e.ReaderId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("reader_id");

                entity.Property(e => e.Song)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("song");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Cityterritory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cityterritory");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LangId).HasColumnName("lang_id");

                entity.Property(e => e.Locality).HasColumnName("locality");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.Remark)
                    .HasColumnType("VARCHAR")
                    .HasColumnName("remark");

                entity.Property(e => e.TerritoryNumber).HasColumnName("territory_number");

                entity.Property(e => e.TimeStamp).HasColumnName("time_stamp");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.WktGeometry)
                    .HasColumnType("VARCHAR")
                    .HasColumnName("wkt_geometry");
            });

            modelBuilder.Entity<Congregation>(entity =>
            {
                entity.ToTable("congregations");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Address)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("address");

                entity.Property(e => e.Circuit)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("circuit");

                entity.Property(e => e.Info)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("info");

                entity.Property(e => e.Meeting1Time)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("meeting1_time");

                entity.Property(e => e.Name)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("name");

                entity.Property(e => e.TalkcoordinatorId)
                    .HasColumnType("INT")
                    .HasColumnName("talkcoordinator_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Congregationmeetingtime>(entity =>
            {
                entity.ToTable("congregationmeetingtimes");

                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CongregationId)
                    .HasColumnType("int")
                    .HasColumnName("congregation_id");

                entity.Property(e => e.MtgDay)
                    .HasColumnType("int")
                    .HasColumnName("mtg_day");

                entity.Property(e => e.MtgTime)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("mtg_time");

                entity.Property(e => e.MtgYear)
                    .HasColumnType("int")
                    .HasColumnName("mtg_year");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<EReminder>(entity =>
            {
                entity.ToTable("e_reminder");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Counter)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("counter");

                entity.Property(e => e.ObjectId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("object_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Type)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("type");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("user_id");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Exception>(entity =>
            {
                entity.ToTable("exceptions");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Cbsday)
                    .HasColumnType("INT")
                    .HasColumnName("cbsday");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.Date2)
                    .HasColumnType("DATE")
                    .HasColumnName("date2");

                entity.Property(e => e.Desc).HasColumnName("desc");

                entity.Property(e => e.Publicmeetingday)
                    .HasColumnType("INT")
                    .HasColumnName("publicmeetingday");

                entity.Property(e => e.Schoolday)
                    .HasColumnType("INT")
                    .HasColumnName("schoolday");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Type)
                    .HasColumnType("INT")
                    .HasColumnName("type");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.ToTable("families");

                entity.HasIndex(e => new { e.PersonId, e.FamilyHead }, "pk")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FamilyHead).HasColumnName("family_head");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("languages");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Code)
                    .HasColumnType("VARCHAR (16)")
                    .HasColumnName("code");

                entity.Property(e => e.Desc)
                    .HasColumnType("VARCHAR (255)")
                    .HasColumnName("desc");

                entity.Property(e => e.Language1)
                    .HasColumnType("VARCHAR (255)")
                    .HasColumnName("language");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<LmmAssignment>(entity =>
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

            modelBuilder.Entity<LmmMeeting>(entity =>
            {
                entity.ToTable("lmm_meeting");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.BibleReading).HasColumnName("bible_reading");

                entity.Property(e => e.Chairman)
                    .HasColumnType("INT")
                    .HasColumnName("chairman");

                entity.Property(e => e.ClosingComments)
                    .HasColumnType("text")
                    .HasColumnName("closing_comments");

                entity.Property(e => e.Counselor2)
                    .HasColumnType("INT")
                    .HasColumnName("counselor2")
                    .HasDefaultValueSql("-1");

                entity.Property(e => e.Counselor3)
                    .HasColumnType("INT")
                    .HasColumnName("counselor3")
                    .HasDefaultValueSql("-1");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.OpeningComments)
                    .HasColumnType("text")
                    .HasColumnName("opening_comments");

                entity.Property(e => e.PrayerBeginning)
                    .HasColumnType("INT")
                    .HasColumnName("prayer_beginning")
                    .HasDefaultValueSql("- 1");

                entity.Property(e => e.PrayerEnd)
                    .HasColumnType("INT")
                    .HasColumnName("prayer_end")
                    .HasDefaultValueSql("- 1");

                entity.Property(e => e.SongBeginning)
                    .HasColumnType("INT")
                    .HasColumnName("song_beginning");

                entity.Property(e => e.SongEnd)
                    .HasColumnType("INT")
                    .HasColumnName("song_end");

                entity.Property(e => e.SongMiddle)
                    .HasColumnType("INT")
                    .HasColumnName("song_middle");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<LmmSchedule>(entity =>
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

            modelBuilder.Entity<LmmScheduleAssist>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("lmm_schedule_assist");

                entity.HasIndex(e => new { e.Date, e.Position }, "lmm_schedule_assist_pk")
                    .IsUnique();

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.Position)
                    .HasColumnType("int")
                    .HasColumnName("position");

                entity.Property(e => e.TalkId)
                    .HasColumnType("int")
                    .HasColumnName("talk_id");
            });

            modelBuilder.Entity<LmmStudy>(entity =>
            {
                entity.ToTable("lmm_studies");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Lang).HasColumnName("lang");

                entity.Property(e => e.StudyName).HasColumnName("study_name");

                entity.Property(e => e.StudyNumber)
                    .HasColumnType("INT")
                    .HasColumnName("study_number");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<LmmWorkbookregex>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("lmm_workbookregex");

                entity.Property(e => e.Key)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("key");

                entity.Property(e => e.Lang)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("lang");

                entity.Property(e => e.Names)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("names");

                entity.Property(e => e.Value)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Locale>(entity =>
            {
                entity.ToTable("locales");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Code)
                    .HasColumnType("VARCHAR ( 16 )")
                    .HasColumnName("code");

                entity.Property(e => e.GeneralRangeformat)
                    .HasColumnType("VARCHAR ( 255 )")
                    .HasColumnName("general_rangeformat");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("INT")
                    .HasColumnName("language_id");

                entity.Property(e => e.SamemonthRangeformat)
                    .HasColumnType("VARCHAR ( 255 )")
                    .HasColumnName("samemonth_rangeformat");

                entity.Property(e => e.SameyearRangeformat)
                    .HasColumnType("VARCHAR ( 255 )")
                    .HasColumnName("sameyear_rangeformat");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("notes");

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

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TypeId)
                    .HasColumnType("INT")
                    .HasColumnName("type_id");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Outgoing>(entity =>
            {
                entity.ToTable("outgoing");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.CongregationId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("congregation_id");

                entity.Property(e => e.Date)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("date");

                entity.Property(e => e.SpeakerId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("speaker_id");

                entity.Property(e => e.ThemeId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("theme_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("persons");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.CongregationId)
                    .HasColumnType("INT")
                    .HasColumnName("congregation_id");

                entity.Property(e => e.Elder)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("elder")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Email)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender)
                    .HasColumnType("CHAR(1)")
                    .HasColumnName("gender")
                    .HasDefaultValueSql("B");

                entity.Property(e => e.Info)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("info");

                entity.Property(e => e.Lastname)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("lastname");

                entity.Property(e => e.Mobile)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("mobile");

                entity.Property(e => e.Phone)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("phone");

                entity.Property(e => e.Publisher)
                    .HasColumnType("INT")
                    .HasColumnName("publisher")
                    .HasDefaultValueSql("2");

                entity.Property(e => e.Servant)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("servant")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Usefor)
                    .HasColumnType("INT")
                    .HasColumnName("usefor");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<PersonidlmmhistoryMt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("personidlmmhistory_mt");

                entity.Property(e => e.CapacityId).HasColumnName("capacity_id");

                entity.Property(e => e.Classnumber)
                    .HasColumnType("INT")
                    .HasColumnName("classnumber");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TalkId)
                    .HasColumnType("INT")
                    .HasColumnName("talk_id");
            });

            modelBuilder.Entity<Personmidweek>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("personmidweek");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.Mtype).HasColumnName("mtype");

                entity.Property(e => e.Part).HasColumnName("part");

                entity.Property(e => e.PersonId)
                    .HasColumnType("INT")
                    .HasColumnName("person_id");
            });

            modelBuilder.Entity<PersonschoolhistoryMt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("personschoolhistory_mt");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active");

                entity.Property(e => e.AssistantId)
                    .HasColumnType("INT")
                    .HasColumnName("assistant_id");

                entity.Property(e => e.Classnumber)
                    .HasColumnType("INT")
                    .HasColumnName("classnumber");

                entity.Property(e => e.CongregationId)
                    .HasColumnType("INT")
                    .HasColumnName("congregation_id");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.Firstname)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender)
                    .HasColumnType("CHAR(1)")
                    .HasColumnName("gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Lastname)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("lastname");

                entity.Property(e => e.Number)
                    .HasColumnType("INT")
                    .HasColumnName("number");

                entity.Property(e => e.Servant)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("servant");

                entity.Property(e => e.StudentId)
                    .HasColumnType("INT")
                    .HasColumnName("student_id");

                entity.Property(e => e.Usefor)
                    .HasColumnType("INT")
                    .HasColumnName("usefor");

                entity.Property(e => e.VolunteerId)
                    .HasColumnType("INT")
                    .HasColumnName("volunteer_id");
            });

            modelBuilder.Entity<Publicmeeting>(entity =>
            {
                entity.ToTable("publicmeeting");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.ChairmanId)
                    .HasColumnType("INT")
                    .HasColumnName("chairman_id");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.FinalPrayerId)
                    .HasColumnType("int")
                    .HasColumnName("final_prayer_id");

                entity.Property(e => e.FinalTalk).HasColumnName("final_talk");

                entity.Property(e => e.HospitalityId)
                    .HasColumnType("int")
                    .HasColumnName("hospitality_id");

                entity.Property(e => e.OpeningPrayerId)
                    .HasColumnType("int")
                    .HasColumnName("opening_prayer_id");

                entity.Property(e => e.SongPt)
                    .HasColumnType("INT")
                    .HasColumnName("song_pt");

                entity.Property(e => e.SongWtEnd)
                    .HasColumnType("INT")
                    .HasColumnName("song_wt_end");

                entity.Property(e => e.SongWtStart)
                    .HasColumnType("INT")
                    .HasColumnName("song_wt_start");

                entity.Property(e => e.SpeakerId)
                    .HasColumnType("INT")
                    .HasColumnName("speaker_id");

                entity.Property(e => e.ThemeId)
                    .HasColumnType("INT")
                    .HasColumnName("theme_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.WtConductorId)
                    .HasColumnType("INT")
                    .HasColumnName("wt_conductor_id");

                entity.Property(e => e.WtSource).HasColumnName("wt_source");

                entity.Property(e => e.WtTheme)
                    .HasColumnType("text")
                    .HasColumnName("wt_theme");

                entity.Property(e => e.WtreaderId)
                    .HasColumnType("INT")
                    .HasColumnName("wtreader_id");
            });

            modelBuilder.Entity<Publicmeetinghistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("publicmeetinghistory");

                entity.Property(e => e.ChairmanId)
                    .HasColumnType("INT")
                    .HasColumnName("chairman_id");

                entity.Property(e => e.FinalPrayerId)
                    .HasColumnType("int")
                    .HasColumnName("final_prayer_id");

                entity.Property(e => e.FinalTalk).HasColumnName("final_talk");

                entity.Property(e => e.HospitalityId)
                    .HasColumnType("int")
                    .HasColumnName("hospitality_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MtgDate).HasColumnName("mtg_date");

                entity.Property(e => e.SongPt)
                    .HasColumnType("INT")
                    .HasColumnName("song_pt");

                entity.Property(e => e.SongWtEnd)
                    .HasColumnType("INT")
                    .HasColumnName("song_wt_end");

                entity.Property(e => e.SongWtStart)
                    .HasColumnType("INT")
                    .HasColumnName("song_wt_start");

                entity.Property(e => e.SpeakerId)
                    .HasColumnType("INT")
                    .HasColumnName("speaker_id");

                entity.Property(e => e.Theme).HasColumnName("theme");

                entity.Property(e => e.ThemeId)
                    .HasColumnType("INT")
                    .HasColumnName("theme_id");

                entity.Property(e => e.ThemeNumber)
                    .HasColumnType("INT")
                    .HasColumnName("theme_number");

                entity.Property(e => e.Weekof)
                    .HasColumnType("DATE")
                    .HasColumnName("weekof");

                entity.Property(e => e.WtConductorId)
                    .HasColumnType("INT")
                    .HasColumnName("wt_conductor_id");

                entity.Property(e => e.WtSource).HasColumnName("wt_source");

                entity.Property(e => e.WtTheme)
                    .HasColumnType("text")
                    .HasColumnName("wt_theme");

                entity.Property(e => e.WtreaderId)
                    .HasColumnType("INT")
                    .HasColumnName("wtreader_id");
            });

            modelBuilder.Entity<Publictalk>(entity =>
            {
                entity.ToTable("publictalks");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.DiscontinueDate)
                    .HasColumnType("DATE")
                    .HasColumnName("discontinue_date");

                entity.Property(e => e.LangId)
                    .HasColumnType("INT")
                    .HasColumnName("lang_id");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("DATE")
                    .HasColumnName("release_date");

                entity.Property(e => e.Revision).HasColumnName("revision");

                entity.Property(e => e.ThemeName).HasColumnName("theme_name");

                entity.Property(e => e.ThemeNumber)
                    .HasColumnType("INT")
                    .HasColumnName("theme_number");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<PublictalksTodolist>(entity =>
            {
                entity.ToTable("publictalks_todolist");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Congregation).HasColumnName("congregation");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Inout).HasColumnName("inout");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.Speaker).HasColumnName("speaker");

                entity.Property(e => e.Theme).HasColumnName("theme");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("school");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.AssistantId)
                    .HasColumnType("INT")
                    .HasColumnName("assistant_id")
                    .HasDefaultValueSql("-1");

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

                entity.Property(e => e.Note)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("note");

                entity.Property(e => e.SchoolScheduleId)
                    .HasColumnType("INT")
                    .HasColumnName("school_schedule_id");

                entity.Property(e => e.SettingId)
                    .HasColumnType("INT")
                    .HasColumnName("setting_id");

                entity.Property(e => e.StudentId)
                    .HasColumnType("INT")
                    .HasColumnName("student_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Timing)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("timing");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.VolunteerId)
                    .HasColumnType("INT")
                    .HasColumnName("volunteer_id")
                    .HasDefaultValueSql("-1");
            });

            modelBuilder.Entity<SchoolBreak>(entity =>
            {
                entity.ToTable("school_break");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Classnumber)
                    .HasColumnType("INT")
                    .HasColumnName("classnumber");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<SchoolSchedule>(entity =>
            {
                entity.ToTable("school_schedule");

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

                entity.Property(e => e.Number)
                    .HasColumnType("INT")
                    .HasColumnName("number");

                entity.Property(e => e.Onlybrothers)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("onlybrothers")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Source).HasColumnName("source");

                entity.Property(e => e.Theme).HasColumnName("theme");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<SchoolSetting>(entity =>
            {
                entity.ToTable("school_settings");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Brothers)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("brothers")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .HasColumnType("INT")
                    .HasColumnName("name");

                entity.Property(e => e.Number)
                    .HasColumnType("INT")
                    .HasColumnName("number");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Schoolhistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("schoolhistory");

                entity.Property(e => e.AssistantId)
                    .HasColumnType("INT")
                    .HasColumnName("assistant_id");

                entity.Property(e => e.Classnumber)
                    .HasColumnType("INT")
                    .HasColumnName("classnumber");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.Number)
                    .HasColumnType("INT")
                    .HasColumnName("number");

                entity.Property(e => e.StudentId)
                    .HasColumnType("INT")
                    .HasColumnName("student_id");

                entity.Property(e => e.VolunteerId)
                    .HasColumnType("INT")
                    .HasColumnName("volunteer_id");
            });

            modelBuilder.Entity<SchoolhistoryMt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("schoolhistory_mt");

                entity.Property(e => e.AssistantId)
                    .HasColumnType("INT")
                    .HasColumnName("assistant_id");

                entity.Property(e => e.Classnumber)
                    .HasColumnType("INT")
                    .HasColumnName("classnumber");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.Number)
                    .HasColumnType("INT")
                    .HasColumnName("number");

                entity.Property(e => e.StudentId)
                    .HasColumnType("INT")
                    .HasColumnName("student_id");

                entity.Property(e => e.VolunteerId)
                    .HasColumnType("INT")
                    .HasColumnName("volunteer_id");
            });

            modelBuilder.Entity<ServiceMeeting>(entity =>
            {
                entity.ToTable("serviceMeeting");

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

                entity.Property(e => e.PrayerId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("prayer_id");

                entity.Property(e => e.SongEnd)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("song_end");

                entity.Property(e => e.SongMiddle)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("song_middle");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<ServiceProgram>(entity =>
            {
                entity.ToTable("serviceProgram");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.MeetingId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("meeting_id");

                entity.Property(e => e.PersonId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("person_id");

                entity.Property(e => e.ProgramId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("program_id");

                entity.Property(e => e.Theme)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("theme");

                entity.Property(e => e.Time)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("time");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Servicemeetinghistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("servicemeetinghistory");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("date");

                entity.Property(e => e.PersonId)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("person_id");

                entity.Property(e => e.Type)
                    .HasColumnType("NUMERIC")
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("settings");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("song");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.LangId).HasColumnName("lang_id");

                entity.Property(e => e.SongNumber).HasColumnName("song_number");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<SpeakerPublictalk>(entity =>
            {
                entity.ToTable("speaker_publictalks");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.LangId)
                    .HasColumnType("INT")
                    .HasColumnName("lang_id");

                entity.Property(e => e.SpeakerId)
                    .HasColumnType("INT")
                    .HasColumnName("speaker_id");

                entity.Property(e => e.ThemeId)
                    .HasColumnType("INT")
                    .HasColumnName("theme_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<StudentStudy>(entity =>
            {
                entity.ToTable("student_studies");

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

                entity.Property(e => e.Exercises)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("exercises")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.StartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("start_date");

                entity.Property(e => e.StudentId)
                    .HasColumnType("INT")
                    .HasColumnName("student_id");

                entity.Property(e => e.StudyNumber)
                    .HasColumnType("INT")
                    .HasColumnName("study_number");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Study>(entity =>
            {
                entity.ToTable("studies");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Demonstration)
                    .HasColumnType("INT")
                    .HasColumnName("demonstration");

                entity.Property(e => e.Discource)
                    .HasColumnType("INT")
                    .HasColumnName("discource");

                entity.Property(e => e.Reading)
                    .HasColumnType("INT")
                    .HasColumnName("reading");

                entity.Property(e => e.StudyName).HasColumnName("study_name");

                entity.Property(e => e.StudyNumber)
                    .HasColumnType("INT")
                    .HasColumnName("study_number");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<TalkInfo>(entity =>
            {
                entity.HasKey(e => e.Talkid);

                entity.ToTable("talk_info");

                entity.Property(e => e.Talkid)
                    .HasColumnType("int")
                    .ValueGeneratedNever()
                    .HasColumnName("talkid");

                entity.Property(e => e.CanCounsel)
                    .HasColumnType("bit")
                    .HasColumnName("can_counsel");

                entity.Property(e => e.Meeting)
                    .HasColumnType("int")
                    .HasColumnName("meeting");

                entity.Property(e => e.MeetingSection)
                    .HasColumnType("int")
                    .HasColumnName("meeting_section");
            });

            modelBuilder.Entity<Territory>(entity =>
            {
                entity.ToTable("territory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.LangId).HasColumnName("lang_id");

                entity.Property(e => e.Locality).HasColumnName("locality");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.Remark)
                    .HasColumnType("VARCHAR")
                    .HasColumnName("remark");

                entity.Property(e => e.TerritoryNumber).HasColumnName("territory_number");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.WktGeometry)
                    .HasColumnType("VARCHAR")
                    .HasColumnName("wkt_geometry");
            });

            modelBuilder.Entity<Territory1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("territories");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active");

                entity.Property(e => e.CheckedoutDate)
                    .HasColumnType("DATE")
                    .HasColumnName("checkedout_date");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LangId).HasColumnName("lang_id");

                entity.Property(e => e.LastworkedDate).HasColumnName("lastworked_date");

                entity.Property(e => e.Locality).HasColumnName("locality");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.Publisher).HasColumnName("publisher");

                entity.Property(e => e.Remark)
                    .HasColumnType("VARCHAR")
                    .HasColumnName("remark");

                entity.Property(e => e.TerritoryNumber).HasColumnName("territory_number");

                entity.Property(e => e.TimeStamp).HasColumnName("time_stamp");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.TypeName).HasColumnName("type_name");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.WktGeometry)
                    .HasColumnType("VARCHAR")
                    .HasColumnName("wkt_geometry");

                entity.Property(e => e.Workedthrough).HasColumnName("workedthrough");
            });

            modelBuilder.Entity<TerritoryAddress>(entity =>
            {
                entity.ToTable("territory_address");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.AddresstypeNumber).HasColumnName("addresstype_number");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Country).HasColumnName("country");

                entity.Property(e => e.County).HasColumnName("county");

                entity.Property(e => e.District).HasColumnName("district");

                entity.Property(e => e.Housenumber).HasColumnName("housenumber");

                entity.Property(e => e.LangId).HasColumnName("lang_id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Postalcode).HasColumnName("postalcode");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Street).HasColumnName("street");

                entity.Property(e => e.TerritoryId).HasColumnName("territory_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.WktGeometry).HasColumnName("wkt_geometry");
            });

            modelBuilder.Entity<TerritoryAddresstype>(entity =>
            {
                entity.ToTable("territory_addresstype");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.AddresstypeName).HasColumnName("addresstype_name");

                entity.Property(e => e.AddresstypeNumber).HasColumnName("addresstype_number");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.LangId).HasColumnName("lang_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<TerritoryAssignment>(entity =>
            {
                entity.ToTable("territory_assignment");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.CheckedbackinDate)
                    .HasColumnType("DATE")
                    .HasColumnName("checkedbackin_date");

                entity.Property(e => e.CheckedoutDate)
                    .HasColumnType("DATE")
                    .HasColumnName("checkedout_date");

                entity.Property(e => e.LangId).HasColumnName("lang_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.TerritoryId).HasColumnName("territory_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<TerritoryCity>(entity =>
            {
                entity.ToTable("territory_city");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.LangId)
                    .HasColumnType("INT")
                    .HasColumnName("lang_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.WktGeometry)
                    .HasColumnType("VARCHAR")
                    .HasColumnName("wkt_geometry");
            });

            modelBuilder.Entity<TerritoryStreet>(entity =>
            {
                entity.ToTable("territory_street");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.FromNumber).HasColumnName("from_number");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.StreetName).HasColumnName("street_name");

                entity.Property(e => e.StreettypeId).HasColumnName("streettype_id");

                entity.Property(e => e.TerritoryId).HasColumnName("territory_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ToNumber).HasColumnName("to_number");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.WktGeometry).HasColumnName("wkt_geometry");
            });

            modelBuilder.Entity<TerritoryStreettype>(entity =>
            {
                entity.ToTable("territory_streettype");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.StreettypeName).HasColumnName("streettype_name");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<TerritoryType>(entity =>
            {
                entity.ToTable("territory_type");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.LangId)
                    .HasColumnType("INT")
                    .HasColumnName("lang_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("INT")
                    .HasColumnName("time_stamp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TypeName).HasColumnName("type_name");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Territoryaddress1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("territoryaddresses");

                entity.Property(e => e.AddresstypeName).HasColumnName("addresstype_name");

                entity.Property(e => e.AddresstypeNumber).HasColumnName("addresstype_number");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.Country).HasColumnName("country");

                entity.Property(e => e.County).HasColumnName("county");

                entity.Property(e => e.District).HasColumnName("district");

                entity.Property(e => e.Housenumber).HasColumnName("housenumber");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LangId).HasColumnName("lang_id");

                entity.Property(e => e.Locality).HasColumnName("locality");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Postalcode).HasColumnName("postalcode");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Street).HasColumnName("street");

                entity.Property(e => e.TerritoryId).HasColumnName("territory_id");

                entity.Property(e => e.TerritoryNumber).HasColumnName("territory_number");

                entity.Property(e => e.WktGeometry).HasColumnName("wkt_geometry");
            });

            modelBuilder.Entity<Territoryassignment1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("territoryassignments");

                entity.Property(e => e.Active)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("active");

                entity.Property(e => e.CheckedbackinDate)
                    .HasColumnType("DATE")
                    .HasColumnName("checkedbackin_date");

                entity.Property(e => e.CheckedoutDate)
                    .HasColumnType("DATE")
                    .HasColumnName("checkedout_date");

                entity.Property(e => e.Firstname)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("firstname");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LangId).HasColumnName("lang_id");

                entity.Property(e => e.Lastname)
                    .HasColumnType("VARCHAR(255)")
                    .HasColumnName("lastname");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.TerritoryId).HasColumnName("territory_id");
            });

            modelBuilder.Entity<Territorystreet1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("territorystreets");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.FromNumber).HasColumnName("from_number");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Locality).HasColumnName("locality");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.StreetName).HasColumnName("street_name");

                entity.Property(e => e.StreettypeId).HasColumnName("streettype_id");

                entity.Property(e => e.StreettypeName).HasColumnName("streettype_name");

                entity.Property(e => e.TerritoryId).HasColumnName("territory_id");

                entity.Property(e => e.TerritoryNumber).HasColumnName("territory_number");

                entity.Property(e => e.ToNumber).HasColumnName("to_number");

                entity.Property(e => e.WktGeometry).HasColumnName("wkt_geometry");
            });

            modelBuilder.Entity<TheocbaseSequence>(entity =>
            {
                entity.ToTable("theocbase_sequence");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Seq).HasColumnName("seq");
            });

            modelBuilder.Entity<Unavailable>(entity =>
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

            modelBuilder.Entity<WtArticleDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("wt_article_dates");

                entity.HasIndex(e => new { e.Yr, e.Article }, "yrarticle")
                    .IsUnique();

                entity.Property(e => e.Article)
                    .HasColumnType("INT")
                    .HasColumnName("article");

                entity.Property(e => e.Dt)
                    .HasColumnType("DATE")
                    .HasColumnName("dt");

                entity.Property(e => e.Yr)
                    .HasColumnType("INT")
                    .HasColumnName("yr");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
