using System;

namespace SampleApp.Application.Contracts.DTO
{
    public class FilterSampleForRead
    {
        // No se desea devolver el identificador
        //public Guid SampleId { get; set; }
        public string SampleCode { get; set; }
        public string SampleName { get; set; }
        public DateTimeOffset SampleCreated { get; set; }
        public string SubSampleCode { get; set; }

        public string SubSampleInfo { get; set; }
    }
}
