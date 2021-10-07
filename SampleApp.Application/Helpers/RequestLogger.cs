using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Application.Helpers
{
    public class RequestLogger : IRequestLogger
    {
        private bool disposedValue;
        public string Path { get; }

        public RequestLogger(string path)
        {
            Path = path;
        }

        public async void Log(string message, HttpContext context)
        {
            var _logFile = File.Open($"{Path}_{DateTime.Now.ToString("yyMMdd")}.txt", FileMode.Append);
            var _logWriter = new StreamWriter(_logFile);

            _logWriter.WriteLine($"[{ DateTime.Now.ToString()}] - {message} from {GetIpAddressFromRequest(context)}");
            _logWriter.WriteLine();

            var headers = GetHeadersFromRequest(context);
            foreach (var header in headers)
            {
                _logWriter.WriteLine($"{header.Key} {header.Value}");
            }
            _logWriter.WriteLine();
            _logWriter.WriteLine(await GetBodyFromRequest(context));
            _logWriter.WriteLine();
            _logWriter.Close();
            _logFile.Close();
            return;

        }

        public string GetIpAddressFromRequest(HttpContext context)
        {
            string clientIpAddr = context.Connection.RemoteIpAddress.ToString();
            if (clientIpAddr == "::1") return "127.0.0.1";
            return clientIpAddr;
        }

        public IHeaderDictionary GetHeadersFromRequest(HttpContext context)
        {
            return context.Request.Headers;
        }

        public async Task<string> GetBodyFromRequest(HttpContext context)
        {         
            if (!context.Request.Body.CanSeek)
            {
                context.Request.EnableBuffering();
            }

            context.Request.Body.Position = 0;

            var reader = new StreamReader(context.Request.Body, Encoding.UTF8);

            var body = await reader.ReadToEndAsync().ConfigureAwait(false);

            context.Request.Body.Position = 0;

            return body;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }
                disposedValue = true;
            }
        }


        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}
