using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO with the SubSample information
    /// </summary>
    public class SubSample
    {
        [Required(ErrorMessage = "SubSampleId is required.")]
        public Guid SubSampleId { get; set; }

        [MaxLength(128, ErrorMessage = "MaxLength of info is exceeded.")]
        public string Info { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Id: {SubSampleId} - Name: {Info}";
        }
    }
}
