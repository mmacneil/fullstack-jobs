using FullStackJobs.GraphQL.Core.Domain.Entities;
using FullStackJobs.GraphQL.Core.Domain.Values;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackJobs.GraphQL.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>(ConfigureJob);
            modelBuilder.Entity<Employer>(ConfigureEmployer);
            modelBuilder.Entity<Applicant>(ConfigureApplicant);
            modelBuilder.Entity<Tag>(ConfigureTag);
            modelBuilder.Entity<JobApplicant>(ConfigureJobApplicant);
        }

        public static void ConfigureJob(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");
            builder.Property(c => c.Status).HasConversion<string>().HasDefaultValue(Status.Draft);

            var tagsNavigation = builder.Metadata.FindNavigation(nameof(Job.Tags));
            tagsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var jobApplicantsNavigation = builder.Metadata.FindNavigation(nameof(Job.JobApplicants));
            jobApplicantsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        public static void ConfigureJobApplicant(EntityTypeBuilder<JobApplicant> builder)
        {
            /* The EF Core Team are planning on removing the need for a join entity at some point.
              https://github.com/aspnet/EntityFramework/issues/1368 */

            builder.ToTable("JobApplicants");
            builder.HasKey(ja => new { ja.JobId, ja.ApplicantId });
            builder.HasOne(ja => ja.Job)
                .WithMany(j => j.JobApplicants)
                .HasForeignKey(ja => ja.JobId);

            builder.HasOne(ja => ja.Applicant)
                .WithMany(a => a.JobApplicants)
                .HasForeignKey(ja => ja.ApplicantId);
        }

        public static void ConfigureEmployer(EntityTypeBuilder<Employer> builder)
        {
            builder.ToTable("Employers");
        }

        public static void ConfigureApplicant(EntityTypeBuilder<Applicant> builder)
        {
            builder.ToTable("Applicants");
        }

        public static void ConfigureTag(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");
        }

        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuitInfo()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified)))
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.UtcNow;
                }
                ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
