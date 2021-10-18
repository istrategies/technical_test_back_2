using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO for the Sample creation
    /// </summary>
    public class SampleForCreate
    {
        [JsonIgnore]
        public Guid SampleId { get; set; }

        public string Name { get; set; }

        public List<SubSample> SubSamples { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Name: {Name} - SubSample: {SubSamples.Count()} items.";
        }
    }
}
