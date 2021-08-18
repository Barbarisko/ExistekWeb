using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExistekWEbProject.CustomRouting
{
    public class DirectoryConstraint : IRouteConstraint
    {
        private readonly string directory;

        public DirectoryConstraint(string directory)
        {
            this.directory = directory;
        }

        public bool Match(HttpContext httpContext, IRouter route, string parameterName, 
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                string req_value;
                if (value is string)
                {
                    req_value = (string)value;

                    string pattern = @"[A-Z]{1}";
                    Regex regex = new Regex(pattern);

                    foreach (var val in req_value.Split(@"\W|"))
                    {
                        if(regex.IsMatch(val))
                            return req_value.Contains(directory);
                    }
                }
            }
            return false;
           
        }
    }
}
