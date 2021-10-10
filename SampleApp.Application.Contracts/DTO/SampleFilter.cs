using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Application.Contracts.DTO
{
    public class SampleFilter
    {
        public DateTimeOffset StartCreated { get; set; }
        public DateTimeOffset EndCreated { get; set; }
        public int CurrentPage { get; set; }
        public int Rows { get; set; }

    }
}
