using System;
using System.Collections.Generic;

namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO to retrieve Sample information
    /// </summary>
    public class SampleForRead
    {
        public string SampleId { get; set; }

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
