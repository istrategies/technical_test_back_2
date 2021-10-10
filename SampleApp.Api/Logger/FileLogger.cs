using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Api.Logger
{
    public class FileLogger : ILogger, IDisposable
    {
        public FileLogger(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static StreamWriter _stream;
        private readonly IConfiguration _configuration;

        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //todos los log estan habilitados
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var fs = GetFileStream();
            fs.WriteLine(formatter(state, exception));
        }

        private StreamWriter GetFileStream()
        {
            if (_stream != null) return _stream;

            var directory = _configuration.GetValue<string>("log:folder");
            var path = Path.Combine(directory, $"log-{DateTime.UtcNow.ToString("yyyyy-MM-dd")}.log");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);


            var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

            _stream = new StreamWriter(fs);
            _stream.AutoFlush = true;
            return _stream;
        }

        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}
