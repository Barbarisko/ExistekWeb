using BLL;
using ExistekWEbProject.CustomLogger;
using Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IInfo, Info>();
            services.AddScoped<IArticle, Article>();

            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IArticlePublishService, AriclePublishService>();
            services.AddTransient<ICheckArticleService, CheckArticleService>();
            services.AddTransient<IInfoService, InfoService>();
            services.AddTransient<IPublishStartup, PublishStartup>();
        }
        public static void AddSampleConfigs(this IServiceCollection services, IConfiguration Configuration)
        {
            var publishOptions = Configuration.GetSection("BasicFileConfig").Get<PublishOptions>();

            Console.WriteLine("Basic author: " + publishOptions.Author);
            Console.WriteLine("Basic filename: " + publishOptions.Filename);
            Console.WriteLine("basic publishdate: " + publishOptions.PublishDate);
            Console.WriteLine("default text: " + publishOptions.InfoConfig.TestText);

            services.Configure<PublishOptions>(Configuration.GetSection("BasicFileConfig"));
        }     
        //public static void AddCustomRouting(this IServiceCollection services)
        //{
        //    services.Configure<RouteOptions>(_ =>
        //    {
        //        _.ConstraintMap.Add("cust", typeof(CustomRouteConstrain));
        //    });
        //}       
    }
}
