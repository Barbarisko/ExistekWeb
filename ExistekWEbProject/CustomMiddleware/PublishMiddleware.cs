using Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject
{
    public class PublishMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly PublishOptions options;


        public PublishMiddleware(RequestDelegate _requestDelegate, PublishOptions options)
        {
            this.requestDelegate = _requestDelegate;
            this.options = options;
        }

        public async Task InvokeAsync(HttpContext context, IPublishStartup _publishStartup)
        {
            //var requiredFilename = context.Request.Query["filename"];
            //if (!string.IsNullOrWhiteSpace(requiredFilename))
            //{
            //    _publishStartup.Publish(requiredFilename);

            //    await context.Response.WriteAsync($"File  {requiredFilename} published");
            //}

            //await requestDelegate(context);


            if (!context.Request.Headers.ContainsKey("filename"))
            {
                await context.Response.WriteAsync("'filename' does not exist");
                return;
            }

            var requiredFilename = context.Request.Headers["filename"];

            if (requiredFilename == options.Filename)
            {
                _publishStartup.Publish(requiredFilename);

                await context.Response.WriteAsync($"File  {requiredFilename} published");
                await requestDelegate.Invoke(context);
            }
            else
            {
                await context.Response.WriteAsync("'filename' is not valid");
            }
        }


    }
}
