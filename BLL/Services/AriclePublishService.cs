using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
//    Створити сервіси і підключити їх використовуючи Dependency injection
//Сервіси не повинні містити ніякої логіки але потрібно додати методи які умовно виконують певну дію.
//Серед сервісів повинні бути:

//- сервіс, який опубліковує статтю
// LifeTime(Transient, Scoped, Singleton)  краще використати для того чи іншого сервісу
    public class AriclePublishService : IArticlePublishService
    {
        private readonly IArticleService articleService;
        IEnumerable<Article> articles;
        public AriclePublishService(IArticleService _articleService)
        {
            articleService = _articleService;
        }

        

        //"publishes" to list of articles
        public IEnumerable<Article> PublishArticle(Article obj)
        {
            articleService.SaveArticleInfo(obj.GetType());

            articles = new List<Article>();
            articles.ToList().Add(obj);
            //articles.ToList().Add(obj.GetType());
            return articles;
        }
    }
}
