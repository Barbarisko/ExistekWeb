using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Web.Http;

namespace ExistekWEbProject.CustomFilters
{
    //    - Cтворити фільтр, який буде перехоплювати якийсь конкретний Exception(бажано створити власний)
    //Фільтр повинен логувати цей Exception і повертати клієнту щось типу "Цей запит не може бути оброблено".

    public class DirectoryExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        private readonly ILogger<DirectoryExceptionFilter> logger;

        public DirectoryExceptionFilter(ILogger<DirectoryExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is NotExistingDirectoryException exception)
                {
                    context.Result = new ObjectResult(exception.Message)
                    {
                        StatusCode = (int?)HttpStatusCode.NotFound,
                    };
                    context.ExceptionHandled = true;

                    logger.LogError($"Exception '{context.Exception.Message}' handled");

                }
            }
        }
        
        //!-- Здесь надо работать с using System.Net.Http;, неудобно с Asp.net core, конфликты --!

        //public void OnException(ExceptionContext context)
        //{
        //    if (context.Exception != null)
        //    {
        //        logger.LogError(context.Exception, "happening");

        //        if (context.Exception is NotExistingDirectoryException)
        //        {
        //            var res = context.Exception.Message;

        //            //var response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.NotFound)
        //            //{
        //            //    Content = new System.Net.Http.StringContent(string.Format("{0}. Try correcting it.", res)),
        //            //    ReasonPhrase = "Directory Not Found"
        //            //};
        //            context.ExceptionHandled = true;
        //            logger.LogError($"Exception {res} handled");

        //            //throw new System.Web.Http.HttpResponseException(response);
        //        }
        //    }
        //}
    }
}
