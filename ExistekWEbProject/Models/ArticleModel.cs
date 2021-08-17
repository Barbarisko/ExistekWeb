using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.Models
{
    public class ArticleModel
    {
        List<Article> Articles;

        public ArticleModel(List<Article> articles)
        {
            Articles = articles;
        }
    }
}
