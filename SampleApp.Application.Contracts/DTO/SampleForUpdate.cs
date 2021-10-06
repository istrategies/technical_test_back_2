using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO to update an existing Sample
    /// </summary>
    public class SampleForUpdate
    {
        public Guid SampleId { get; set; }

        public string Name { get; set; }

        public List<SubSample> SubSamples { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Id: {SampleId} - Name: {Name} - SubSample: {SubSamples.Count} items.";
        }
    }
}
