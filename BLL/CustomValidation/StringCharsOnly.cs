using BLL.ModelsNew;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StringCharsOnly: ValidationAttribute
    {
        private readonly string pattern;

        public StringCharsOnly(string pattern): base()
        {
            this.pattern = pattern;
        }

        public override bool IsValid(object? value)
        {
            var article = (ArticleModel)value;
            var regexItem = new Regex(pattern);
            if (!regexItem.IsMatch(article.Name) || article == null) 
                return false;

            return true;
        }
    }
}
