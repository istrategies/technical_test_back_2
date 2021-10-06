using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Entities;
using SampleApp.Infrastructure.Data.Models.Extensions;

namespace SampleApp.Infrastructure.Data.Models
{
    /// <summary>
    /// Sample EF Model
    /// </summary>
    public class SampleContext : DbContext
    {
        public SampleContext() { }

        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // The context is configured to work in memory mode (DBLess)
            options.UseInMemoryDatabase("MemoryDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Custom extension to fill the data for the test purpose
            modelBuilder.SeedData();

            modelBuilder.Entity<SubSample>().HasOne(x => x.SampleRef).WithMany(x => x.SubSamples).HasForeignKey(x => x.SampleId);
            modelBuilder.Entity<Sample>().HasMany(x => x.SubSamples).WithOne(x => x.SampleRef);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Sample> Sample { get; set; }
        public DbSet<SubSample> SubSample { get; set; }

    }
}
