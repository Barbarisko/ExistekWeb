using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.CustomRouting
{
    public class DateTimeInlineConstraint : IRouteConstraint
    {
        private readonly DateTime publishdate_watermark;
        private readonly string sign_watermark;

        public DateTimeInlineConstraint(DateTime publishdate_watermark, string sign_watermark)
        {
            this.publishdate_watermark = publishdate_watermark;
            this.sign_watermark = sign_watermark;
        }

        public bool Match(HttpContext? httpContext, IRouter? route, 
                          string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var value = (string)values[routeKey];

            return value.Contains(sign_watermark) && publishdate_watermark <= DateTime.Now;
        }
    }
}
