using BLL;
using DataAccess;
using ExistekWEbProject.CustomLogger;
using ExistekWEbProject.Extensions;
using Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //adding db context
            services.AddDbContext<PublishContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("localhost"));
            });
            
            //the extension method used here
            services.AddCustomServices();
            services.AddSampleConfigs(Configuration);
            services.AddCustomRouting();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExistekWEbProject", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            SetLogging(loggerFactory);
            //app.ConfigureExceptionHandler(defaultLogger);

            //custom route for ArticleController 
            //app.ConfigurePublishRoute();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExistekWEbProject v1"));
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //custom middleware used here
            //app.UsePublishMiddleware(options =>
            //{
            //    options.Filename = "article";
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SetLogging(ILoggerFactory loggerFactory)
        {
            var debuglogpath = Configuration.GetSection("FileLog:DebugFileLog").Get<LoggingOptions>();
            var errorlogpath = Configuration.GetSection("FileLog:ErrorFileLog").Get<LoggingOptions>();
            var warninglogpath = Configuration.GetSection("FileLog:WarningFileLog").Get<LoggingOptions>();
            var infologpath = Configuration.GetSection("FileLog:InfoFileLog").Get<LoggingOptions>();

            //defining file for debug style logging
            loggerFactory.AddFile(debuglogpath, new ColoredConsoleLoggerConfiguration
            {
                LogLevel = LogLevel.Debug,
                Color = ConsoleColor.Blue
            });
            var defaultLogger = loggerFactory.CreateLogger("DebugLogger");

            //defining file for error style logging
            loggerFactory.AddFile(errorlogpath, new ColoredConsoleLoggerConfiguration
            {
                LogLevel = LogLevel.Error,
                Color = ConsoleColor.Red
            });
            var errorLogger = loggerFactory.CreateLogger("ErrorLogger");
            

            //defining file for warning style logging
            loggerFactory.AddFile(warninglogpath, new ColoredConsoleLoggerConfiguration
            {
                LogLevel = LogLevel.Warning,
                Color = ConsoleColor.DarkYellow
            });
            var warningLogger = loggerFactory.CreateLogger("WarningLogger");
           

            //defining file for info style logging
            loggerFactory.AddFile(infologpath, new ColoredConsoleLoggerConfiguration
            {
                LogLevel = LogLevel.Information,
                Color = ConsoleColor.Green
            });
            var infoLogger = loggerFactory.CreateLogger("InfoLogger");

            //sample logging
            defaultLogger.LogDebug("Processing request {0}", debuglogpath.FileName);
            errorLogger.LogError("Processing request {0}", errorlogpath.FileName);
            warningLogger.LogWarning("Processing request {0}", warninglogpath.FileName);
            infoLogger.LogInformation("Processing request {0}", infologpath.FileName);
        }       
    }
}
