using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO to retrieve Sample information
    /// </summary>
    public class SampleForRead
    {
        [JsonIgnore]
        public Guid SampleId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset Created { get; set; }

        public IEnumerable<SubSample> SubSamples { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Id: {SampleId} - Name: {Name}";
        }
    }
}
