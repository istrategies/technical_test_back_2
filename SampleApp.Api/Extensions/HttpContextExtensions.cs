using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SampleApp.Api.Filters
{
    public static class HttpContextExtensions
    {
        public static async Task WriteLog(this HttpContext context, ILogger log)
        {
            if (context?.Request == null) return;

            var stream = context.Request.Body;
            var headers = context.Request.Headers?.Select(x => $"{x.Key} --> {x.Value}") ?? new List<string>();
            var ip = context.Connection?.RemoteIpAddress;

            var reader = new StreamReader(stream);
            if (stream.CanSeek)
                stream.Seek(0, SeekOrigin.Begin);

            var requestBodyAsString = await reader.ReadToEndAsync();

            log.LogInformation("Headers: {0}: ", string.Join(" -- ", headers));
            log.LogInformation("Body: {0}: ", requestBodyAsString);
            log.LogInformation("IP: {0}: ", ip);

            if (stream.CanSeek)
                stream.Seek(0, SeekOrigin.Begin);
        }
    }
}