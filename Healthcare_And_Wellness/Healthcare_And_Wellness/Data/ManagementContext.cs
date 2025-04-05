using Healthcare_And_Wellness.Models;
using Microsoft.EntityFrameworkCore;

namespace Healthcare_And_Wellness.Data
{
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions<ManagementContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> jobs { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Applicant> applicants { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<StepLog> StepLogs { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<BmiCalculator> BmiCalculations { get; set; }
        public DbSet<Injection> Injections { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MentalArticle> MentalArticles { get; set; }
        public DbSet<GuidedActivity> GuidedActivities { get; set; }
        public DbSet<SupportPost> SupportPosts { get; set; }
        public DbSet<SelfAssessment> SelfAssessments { get; set; }
        public DbSet<SupportComment> SupportComments { get; set; }
        public DbSet<SupportReaction> SupportReactions { get; set; }
        public DbSet<SupportReport> SupportReports { get; set; }
        public DbSet<HealthRecommendation> HealthRecommendations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Password = "Admin@1234",
                ConfirmPassword = "Admin@1234",
                Name = "Administrator",
                Age = 30,
                DateOfBirth = "1994-01-01",
                Role = "Admin"
            },
            new User
            {
                Id = 2,
                Username = "member",
                Password = "Lovepreet@1234",
                ConfirmPassword = "Lovepreet@1234",
                Name = "Lovepreet Singh",
                Age = 21,
                DateOfBirth = "2003-10-26",
                Role = "Member"
            });

            modelBuilder.Entity<Applicant>().HasOne(c => c.Job).WithMany(m => m.applicants).HasForeignKey(c => c.jobID);
            modelBuilder.Entity<Job>().HasData(
               new Job()
               {
                   jobID = 1,
                   jobName = "Instructor Therapist",
                   statusJob = "Apply",
                   description = "The responsibility of the Instructor " +
               "Therapist is to deliver direct ABA (Applied Behaviour Analysis) interventions. This includes providing input into the development " +
               "and implementation of Behaviour Support Plans (BSPs), collecting baseline data, maintaining progress notes (i.e., case notes), " +
               "the recording and graphing of relevant data, parent coaching, individual and group services, as well as the preparation of teaching materials " +
               "and also working with, and mentoring volunteers."
               }
            );

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Article>().HasData(
                new Article { ArticleId = 1, Title = "Latest in Mental Wellness", Content = "Explore the newest research on mental health...", PublishedDate = DateTime.Now }
            );

            // Seed example workout plans
            modelBuilder.Entity<WorkoutPlan>().HasData(
                new WorkoutPlan
                {
                    WorkoutId = 1,
                    Title = "Full Body Strength Training",
                    Description = "A 45-minute strength training routine for beginners.",
                    Category = "Strength",
                    Duration = 45,
                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
                },
                new WorkoutPlan
                {
                    WorkoutId = 2,
                    Title = "Morning Yoga Flow",
                    Description = "A 30-minute morning yoga session to start your day.",
                    Category = "Yoga",
                    Duration = 30,
                    VideoUrl = "https://www.youtube.com/watch?v=v7AYKMP6rOE"
                }
            );

            modelBuilder.Entity<Appointment>().HasData(
                 new Appointment { Id = 1, UserId = null, AppointmentDate = DateTime.Now.AddDays(2), ServiceType = "General Checkup", Status = "Available" },
                 new Appointment { Id = 2, UserId = 2, AppointmentDate = DateTime.Now.AddDays(3), ServiceType = "Dental Cleaning", Status = "Pending" }
             );

            modelBuilder.Entity<SupportComment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupportComment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupportReaction>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupportReaction>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Reactions)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupportReport>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupportReport>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Reports)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
