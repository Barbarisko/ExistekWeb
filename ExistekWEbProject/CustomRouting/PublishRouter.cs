using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.CustomRouting
{
    public class PublishRouter : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public async Task RouteAsync(RouteContext context)
        {
            var pathValue = context.HttpContext.Request.Path.Value;
            var req_pathValue = "/publish"; //put to configs

            if (string.Equals(pathValue, req_pathValue))
            {
                context.Handler = async ctx =>
                {
                    await ctx.Response.WriteAsync("Do not use any link except '/publish'.");
                };
            }

            await Task.CompletedTask;
        }
    }
}
