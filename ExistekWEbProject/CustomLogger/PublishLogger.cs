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

        private readonly ColoredConsoleLoggerConfiguration _config;
        private readonly string _name;

        public PublishLogger(PublishLoggerProvider provider, ColoredConsoleLoggerConfiguration config, string name)
        {
            _provider = provider;
            _config = config;
            _name = name;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                var filePath = Path.Combine(_provider._options.FolderPath, _provider._options.FileName);

                var logLine = formatter(state, exception) + Environment.NewLine;

                lock (_lock)
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = _config.Color;
                    Console.WriteLine($"{logLevel.ToString()} - {eventId.Id} - {_name} - {formatter(state, exception)}");
                    Console.ForegroundColor = color;

                    File.AppendAllText(filePath, logLine);
                }                
            }                
        }
    }
}
