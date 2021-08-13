using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.CustomLogger
{
    public class PublishLogger : ILogger
    {
        private readonly PublishLoggerProvider _provider;

        private static object _lock = new object();

        public PublishLogger(PublishLoggerProvider provider)
        {
            _provider = provider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            
            var filePath = Path.Combine(_provider.Options.FolderPath, _provider.Options.FileName);

            var logLine = formatter(state, exception) + Environment.NewLine;

            lock (_lock)
            {
                File.AppendAllText(filePath, logLine);
            }
        }
    }
}
