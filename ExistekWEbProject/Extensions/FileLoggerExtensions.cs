using ExistekWEbProject.CustomLogger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.Extensions
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, LoggingOptions options, ColoredConsoleLoggerConfiguration colorconfig)
        {
            factory.AddProvider(new PublishLoggerProvider(options, colorconfig));
            return factory;
        }

        public static ILoggerFactory AddColoredConsoleLogger(this ILoggerFactory loggerFactory, LoggingOptions options, ColoredConsoleLoggerConfiguration colorconfig)
        {
            loggerFactory.AddProvider(new PublishLoggerProvider(options, colorconfig));
            return loggerFactory;
        }

        public static ILoggerFactory AddColoredConsoleLogger(this ILoggerFactory loggerFactory, ColoredConsoleLoggerConfiguration config)
        {
            var config3 = new ColoredConsoleLoggerConfiguration();
            return loggerFactory.AddColoredConsoleLogger(config3);
        }

        public static ILoggerFactory AddColoredConsoleLogger(this ILoggerFactory loggerFactory, Action<ColoredConsoleLoggerConfiguration> configure)
        {
            var config = new ColoredConsoleLoggerConfiguration();
            configure(config);
            return loggerFactory.AddColoredConsoleLogger(config);
        }
    }
}
