using BLL.ModelsNew;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.CustomValidation
{
    [AttributeUsage(AttributeTargets.Method)]

    public class IsNotNull:ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var article = (List<string>)value;

            if (article.Count() == 0)
                return false;

            return true;
        }
    }
}
