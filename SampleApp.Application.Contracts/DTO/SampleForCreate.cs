using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO for the Sample creation
    /// </summary>
    public class SampleForCreate
    {
        [Required(ErrorMessage = "SampleId is required.")]
        public Guid SampleId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(32, ErrorMessage = "MaxLength of name is exceeded.")]
        public string Name { get; set; }

        public List<SubSample> SubSamples { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Name: {Name} - SubSample: {SubSamples.Count()} items.";
        }
    }
}
