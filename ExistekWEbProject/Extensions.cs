using BLL;
using Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject
{
    public static class Extensions
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

        public static IApplicationBuilder UsePublishMiddleware(this IApplicationBuilder app, string filename)
        {
            return app.UseMiddleware<PublishMiddleware>(filename);
        }
    }
}
