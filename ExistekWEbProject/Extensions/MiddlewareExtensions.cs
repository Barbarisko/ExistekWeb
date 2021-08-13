using ExistekWEbProject.CustomLogger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExistekWEbProject.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UsePublishMiddleware(this IApplicationBuilder app, Action<PublishOptions> configureOptions)
        {
            var options = new PublishOptions();
            configureOptions(options);

            return app.UseMiddleware<PublishMiddleware>(options);
        }
        //public static IApplicationBuilder UseLoggerMiddleware(this IApplicationBuilder app, ILoggerFactory loggerFactory, IConfiguration Configuration)
        //{
        //    //var loggerFactory = LoggerFactory.Create(builder =>
        //    //{
        //    //    builder.AddProvider(new PublishLoggerProvider(loggingOptions));
        //    //});
        //    //var logger1 = loggerFactory.CreateLogger("Custom Logger");

        //    var loggingOptions = Configuration.GetSection("FileLog:BasicFileLog").Get<LoggingOptions>();
        //    var errorlogpath = Configuration.GetSection("FileLog:ExceptionFileLog").Get<LoggingOptions>(); 

        //    loggerFactory.AddFile(loggingOptions);
        //    var defaultLogger = loggerFactory.CreateLogger("DefaultLogger");
        //    defaultLogger.LogInformation("Processing request {0}", loggingOptions.FileName);

        //    loggerFactory.AddFile(errorlogpath);
        //    var errorLogger = loggerFactory.CreateLogger("ExceptionLogger");
        //    errorLogger.LogInformation("Processing request {0}", errorlogpath.FileName);

        //    //app.ConfigureExceptionHandler(defaultLogger);

        //    return app.UseMiddleware<PublishMiddleware>();
        //}


        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
        
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
