using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.CustomLogger
{
    public class PublishLoggerProvider : ILoggerProvider
    {
        public readonly LoggingOptions _options;
        private readonly ColoredConsoleLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, PublishLogger> _loggers = new ConcurrentDictionary<string, PublishLogger>();
        public PublishLoggerProvider(LoggingOptions options, ColoredConsoleLoggerConfiguration config)
        {
            _options = options;

            if (!Directory.Exists(_options.FolderPath))
            {
                Directory.CreateDirectory(_options.FolderPath);
                //File.Create(Options.FolderPath + newfile);
            }
            _config = config;
        }

        public void Dispose()
        {
            _loggers.Clear();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new PublishLogger(this, _config, name));
        }
    }
}
