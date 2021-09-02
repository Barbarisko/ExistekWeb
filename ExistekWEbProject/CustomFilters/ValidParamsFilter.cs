using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.CustomFilters
{
    //- Створити Фільтр, який буде перевіряти валідність запиту і потім дозволяти його виконання.
    //Валідація повинна бути у вигляді простої арифметичної операції.З куків брати аргументи для операції, виконати операцію і результат порівняти з відповіддю в хедері
    //Приклад: взяти з куків значення з ключами arg1 та arg2 виконати додавання і порівняти з хедером MathAddResult
    //Ключі для куків і для хедера потрібно взяти з конфігурації.
    public class ValidParamsFilter
    {
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    string message = $"\n {context.ActionDescriptor.DisplayName} -> " +
        //                    $"{context.Controller} -> " +
        //                    $"{this.GetType().GetMethod("OnActionExecuting").Name}\t- {DateTime.Now} ";
        //    logger.LogInformation(message);
        //}

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    string message = $"\n {context.ActionDescriptor.DisplayName} -> " +
        //                               $"{context.Controller} -> " +
        //                               $"{this.GetType().GetMethod("OnActionExecuted").Name}\t- {DateTime.Now} ";
        //    logger.LogInformation(message);
        //}

        //public override void OnResultExecuting(ResultExecutingContext context)
        //{
        //    string message = $"\n {context.RouteData.Values["controller"]} -> " +
        //                    $"{context.RouteData.Values["action"]} -> " +
        //                    $"{this.GetType().GetMethod("OnResultExecuting").Name}\t- {DateTime.Now} ";
        //    logger.LogInformation(message);
        //}
    }
}
