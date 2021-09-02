using BLL;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace ExistekWEbProject.CustomFilters
{
    //    - Cтворити фільтр, який буде перехоплювати якийсь конкретний Exception(бажано створити власний)
    //Фільтр повинен логувати цей Exception і повертати клієнту щось типу "Цей запит не може бути оброблено".

    public class DirectoryExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        private readonly ILogger<DirectoryExceptionFilter> logger;

        public DirectoryExceptionFilter(ILogger<DirectoryExceptionFilter> logger)
        {
            this.logger = logger;
        }
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is NotExistingDirectoryException)
                {
                    var res = context.Exception.Message;

                    HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(res),
                        ReasonPhrase = res
                    };

                    context.Response = response;

                    logger.LogError(context.Exception, "Exception handled");
                }
            }
        }
    }
}
