using AutoMapper;
using BLL;
using DataAccess.Entities;
using DataAccess.Repository;
using DataAccess.UnitOfWork;
using ExistekWEbProject.CustomFilters;
using ExistekWEbProject.CustomRouting;
using Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace ExistekWEbProject
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<DirectoryExceptionFilter>();
            services.AddScoped<ValidParamsFilter>();


            services.AddScoped<IInfo, Info>();
            services.AddScoped<IArticle, BLL.Article>();

            services.AddScoped<IRepository<ArticleTag>, Repository<ArticleTag>>();
            services.AddScoped<IRepository<Author>, Repository<Author>>();
            services.AddScoped<IRepository<Tag>, Repository<Tag>>();
            services.AddScoped<IRepository<Text>, Repository<Text>>();
            services.AddScoped<IRepository<DataAccess.Entities.Article>, Repository<DataAccess.Entities.Article>>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton(new MapperConfiguration(c => c.AddProfile(new BusinessLogic.CustomMapper())).CreateMapper());

            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IArticlePublishService, AriclePublishService>();
            services.AddTransient<ICheckArticleService, CheckArticleService>();
            services.AddTransient<IInfoService, InfoService>();
            services.AddTransient<IPublishStartup, PublishStartup>();
        }
        public static void AddSampleConfigs(this IServiceCollection services, IConfiguration Configuration)
        {
            //var publishOptions = Configuration.GetSection("BasicFileConfig").Get<PublishOptions>();

            //Console.WriteLine("Basic author: " + publishOptions.Author);
            //Console.WriteLine("Basic filename: " + publishOptions.Filename);
            //Console.WriteLine("basic publishdate: " + publishOptions.PublishDate);
            //Console.WriteLine("default text: " + publishOptions.InfoConfig.TestText);

            services.Configure<PublishOptions>(Configuration.GetSection("BasicFileConfig"));
        }
        public static void AddCustomRouting(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(_ =>
            {
                _.ConstraintMap.Add("req_dir", typeof(DirectoryConstraint));
            });
        }
    }
}
