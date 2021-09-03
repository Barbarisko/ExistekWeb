using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExistekWEbProject.CustomFilters
{
    //- Створити Фільтр, який буде перевіряти валідність запиту і потім дозволяти його виконання.
    //Валідація повинна бути у вигляді простої арифметичної операції.
    //З куків брати аргументи для операції, виконати операцію і результат порівняти з відповіддю в хедері
    //Приклад: взяти з куків значення з ключами arg1 та arg2 виконати додавання і порівняти з хедером MathAddResult
    //Ключі для куків і для хедера потрібно взяти з конфігурації.
    public class ValidParamsFilter: IActionFilter
        , IValidatableObject
    {
        public ILogger<ValidParamsFilter> Logger { get; set; }
        public int Prop11 { get => Prop1; set => Prop1 = value; }
        public int Prop21 { get => Prop2; set => Prop2 = value; }
        private int Result { get; set; }

        private int Prop1;
        private int Prop2;

        IConfiguration configuration;

        public ValidParamsFilter(ILogger<ValidParamsFilter> logger, IConfiguration configuration)
        {
            Logger = logger;
            this.configuration = configuration;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Logger.LogInformation("ValidParamsFilter :  OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                var cookie1 = context.HttpContext.Request.Cookies[configuration.GetValue<string>("KeysForRequest:Prop1")];
                Prop1 = Convert.ToInt32(cookie1);

                var cookie2 = context.HttpContext.Request.Cookies[configuration.GetValue<string>("KeysForRequest:Prop2")];
                Prop1 = Convert.ToInt32(cookie2);

                var header_result = context.HttpContext.Request.Headers[configuration.GetValue<string>("KeysForRequest:Result")];
                Result = Convert.ToInt32(header_result);

                Logger.LogWarning($"Got params to validate : {Prop11} {Prop21}");
                if ((this.Prop1+ this.Prop2)==Result)
                {
                    context.Result = new ObjectResult(context.HttpContext.Response.Body)
                    {
                        StatusCode = (int?)HttpStatusCode.OK,
                    };
                }
                else
                {
                    context.Result = new ObjectResult(context.HttpContext.Response.Body)
                    {
                        StatusCode = (int?)HttpStatusCode.BadRequest,
                    };
                }
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (this.Prop1 < this.Prop2)
            {
                yield return new ValidationResult("Prop1 must be larger than Prop2");
            }
        }
    }
}
