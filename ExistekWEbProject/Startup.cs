using BLL;
using ExistekWEbProject.CustomLogger;
using ExistekWEbProject.Extensions;
using Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //the extension method used here
            services.AddCustomServices();
            //var publishOptions = Configuration.GetSection("BasicFileConfig").Get<PublishOptions>();

            //Console.WriteLine("Basic author: " + publishOptions.Author);
            //Console.WriteLine("Basic filename: " + publishOptions.Filename);
            //Console.WriteLine("basic publishdate: " + publishOptions.PublishDate);
            //Console.WriteLine("default text: " + publishOptions.InfoConfig.TestText);

            services.Configure<PublishOptions>(Configuration.GetSection("BasicFileConfig"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExistekWEbProject", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var loggingOptions = Configuration.GetSection("FileLog:BasicFileLog").Get<LoggingOptions>();
            var errorlogpath = Configuration.GetSection("FileLog:ErrorFileLog").Get<LoggingOptions>();

            //var loggerFactory = LoggerFactory.Create(builder =>
            //{
            //    builder.AddProvider(new PublishLoggerProvider(loggingOptions));
            //});
            //var logger1 = loggerFactory.CreateLogger("Custom Logger");

            
            loggerFactory.AddFile(loggingOptions);
            var defaultLogger = loggerFactory.CreateLogger("DefaultLogger");


            loggerFactory.AddFile(errorlogpath);
            var errorLogger = loggerFactory.CreateLogger("ErrorLogger");
            errorLogger.LogInformation("Processing request {0}", errorlogpath.FileName);

            //app.ConfigureExceptionHandler(defaultLogger);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExistekWEbProject v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            //custom middleware used here
            app.UsePublishMiddleware(options =>
            {
                options.Filename = "article1";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
