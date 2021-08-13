using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.CustomLogger
{
    public class PublishLoggerProvider : ILoggerProvider
    {
        public readonly LoggingOptions Options;

        public PublishLoggerProvider(LoggingOptions options)
        {
            Options = options;

            if (!Directory.Exists(Options.FolderPath))
            {
                Directory.CreateDirectory(Options.FolderPath);
                //File.Create(Options.FolderPath + newfile);
            }
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new PublishLogger(this);
        }
    }
}
