using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.CustomFilters
{
    //    - Cтворити фільтр, який буде перехоплювати якийсь конкретний Exception(бажано створити власний)
    //Фільтр повинен логувати цей Exception і повертати клієнту щось типу "Цей запит не може бути оброблено".

    public class DirectoryExceptionFilter : ActionFilterAttribute/*, IExceptionFilter*/
    {
        private readonly ILogger<DirectoryExceptionFilter> logger;

        public DirectoryExceptionFilter(ILogger<DirectoryExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string message = $"\n {context.ActionDescriptor.DisplayName} -> " +
                            $"{context.Controller} -> " +
                            $"{this.GetType().GetMethod("OnActionExecuting").Name}\t- {DateTime.Now} ";
            logger.LogInformation(message);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string message = $"\n {context.ActionDescriptor.DisplayName} -> " +
                                       $"{context.Controller} -> " +
                                       $"{this.GetType().GetMethod("OnActionExecuted").Name}\t- {DateTime.Now} ";
            logger.LogInformation(message);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            string message = $"\n {context.RouteData.Values["controller"]} -> " +
                            $"{context.RouteData.Values["action"]} -> " +
                            $"{this.GetType().GetMethod("OnResultExecuting").Name}\t- {DateTime.Now} ";
            logger.LogInformation(message);
        }
        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                logger.LogError(context.Exception, "Exception handled");
                context.ExceptionHandled = true;

               Console.WriteLine("smth happened");
            }

            logger.LogInformation("DirectoryExceptionFilter.OnException");
        }
    }
}
