using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Application.Contracts.DTO
{
    public class SubSampleForRead
    {
        public Guid SampleId { get; set; }
        public string SampleName { get; set; }
        public DateTimeOffset SampleCreated { get; set; }
        public Guid SubSampleId { get; set; }
        public string SubSampleInfo { get; set; }
    }
}
