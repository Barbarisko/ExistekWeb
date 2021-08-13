using ExistekWEbProject.CustomLogger;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.Extensions
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, LoggingOptions options)
        {
            factory.AddProvider(new PublishLoggerProvider(options));
            return factory;
        }
    }
}
