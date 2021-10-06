using System;

namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO with the SubSample information
    /// </summary>
    public class SubSample
    {
        public Guid SubSampleId { get; set; }

        public string Info { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Id: {SubSampleId} - Name: {Info}";
        }
    }
}
