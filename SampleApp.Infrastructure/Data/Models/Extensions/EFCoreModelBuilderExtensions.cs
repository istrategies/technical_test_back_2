using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace SampleApp.Infrastructure.Data.Models.Extensions
{
    /// <summary>
    /// EntitiFrameworkCore ModelBuilder extensions
    /// </summary>
    public static class EFCoreModelBuilderExtensions
    {
        /// <summary>
        /// Fills the database. TESTING DBLess purpose
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            List<Sample> samples = JsonSerializer.Deserialize<List<Sample>>(File.ReadAllText(Path.Combine(root, Constants.SAMPLE_SEED_PATH)));
            List<SubSample> subSamples = JsonSerializer.Deserialize<List<SubSample>>(File.ReadAllText(Path.Combine(root, Constants.SUBSAMPLE_SEED_PATH)));

            modelBuilder.Entity<Sample>().HasData(samples);
            modelBuilder.Entity<SubSample>().HasData(subSamples);
        }
    }  
}
