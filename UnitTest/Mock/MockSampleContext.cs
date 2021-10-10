using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Entities;
using SampleApp.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace UnitTest.Mock
{
     public class MockSampleContext : SampleContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Custom extension to fill the data for the test purpose
            SeedData(modelBuilder);

            modelBuilder.Entity<SubSample>().HasOne(x => x.SampleRef).WithMany(x => x.SubSamples).HasForeignKey(x => x.SampleId);
            modelBuilder.Entity<Sample>().HasMany(x => x.SubSamples).WithOne(x => x.SampleRef);

            // No llamar al base para que carge los datos desde el Test Unitario
            //base.OnModelCreating(modelBuilder);
        }


        /// <summary>
        /// Fills the database. TESTING DBLess purpose
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void SeedData(ModelBuilder modelBuilder)
        {
            List<Sample> samples = JsonSerializer.Deserialize<List<Sample>>(ReadResource("UnitTest.SampleSeed.Samples.json"));
            List<SubSample> subSamples = JsonSerializer.Deserialize<List<SubSample>>(ReadResource("UnitTest.SampleSeed.SubSamples.json"));

            modelBuilder.Entity<Sample>().HasData(samples);
            modelBuilder.Entity<SubSample>().HasData(subSamples);
        }
        private string ReadResource(string name)
        {
            // Determine path
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = name;
            // Format: "{Namespace}.{Folder}.{filename}.{Extension}"
            resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(name));

            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
