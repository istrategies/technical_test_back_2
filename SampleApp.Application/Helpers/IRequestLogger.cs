using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Application.Helpers
{
    public interface IRequestLogger : IDisposable
    {
        void Log(string message, HttpContext context);

        string GetIpAddressFromRequest(HttpContext context);
        IHeaderDictionary GetHeadersFromRequest(HttpContext context);
        Task<string> GetBodyFromRequest(HttpContext context);
    }
}
