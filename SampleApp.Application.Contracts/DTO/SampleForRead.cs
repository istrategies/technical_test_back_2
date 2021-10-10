using System;
using System.Collections.Generic;

namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO to retrieve Sample information
    /// </summary>
    public class SampleForRead
    {
        // No se desea devolver el identificador
        //public Guid SampleId { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public DateTimeOffset Created { get; set; }

        public IEnumerable<SubSampleForRead> SubSamples { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Id: {Code} - Name: {Name}";
        }
    }
}
