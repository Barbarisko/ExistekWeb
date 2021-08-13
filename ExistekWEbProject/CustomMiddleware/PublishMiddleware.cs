﻿using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
        //private readonly ILogger logger;


        public PublishMiddleware(RequestDelegate _requestDelegate, PublishOptions options/*, ILogger logger*/)
        {
            this.requestDelegate = _requestDelegate;
            this.options = options;
            //this.logger = logger;
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
                //logger.LogInformation($"Fetching article from {requiredFilename}.txt");

                _publishStartup.Publish(requiredFilename);

                //throw new AccessViolationException($"Exception while fetching article from {requiredFilename}.txt.");

                //logger.LogInformation($"Returning {requiredFilename}.");

                await context.Response.WriteAsync($"File {requiredFilename} published");
                await requestDelegate.Invoke(context);
            }
            else
            {
                await context.Response.WriteAsync("'filename' is not valid");
            }
        }


    }
}
