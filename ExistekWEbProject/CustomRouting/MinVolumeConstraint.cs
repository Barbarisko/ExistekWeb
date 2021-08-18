using Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace ExistekWEbProject.CustomRouting
{
    
    //check for auth rights
    //and article volume
    public class MinVolumeConstraint : IHttpRouteConstraint
    {
        private readonly IArticle article;

        public MinVolumeConstraint(IArticle article)
        {
            this.article = article;
        }

        public bool Match(HttpRequestMessage? request, IHttpRoute? route,
            string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object volume_value;
            if (values.TryGetValue(parameterName, out volume_value) && volume_value != null)
            {
                uint req_value;
                if (volume_value is uint)
                {
                    req_value = (uint)volume_value;
                    return req_value != 0;
                }

                string valueString = Convert.ToString(volume_value, CultureInfo.InvariantCulture);
                if (UInt32.TryParse(valueString, NumberStyles.Number,
                    CultureInfo.InvariantCulture, out req_value))
                {
                    return req_value != article.Text.NumOfSigns;
                }
            }
            return false;
        }

        //todo
        private bool AuthCheck() {
            return false;
        }
    }

}