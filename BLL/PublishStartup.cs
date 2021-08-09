using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PublishStartup : IPublishStartup
    {
        private readonly IArticlePublishService articlePublishService;
        private readonly ICheckArticleService checkArticleService;
        private readonly IArticleService articleService;


        public PublishStartup(IArticlePublishService _articlePublishService, ICheckArticleService _checkArticleService, IArticleService articleService)
        {
            articlePublishService = _articlePublishService;
            checkArticleService = _checkArticleService;
            this.articleService = articleService;
        }

        public void Publish(string filepath)
        {
            try
            {
                //тут я запуталась с кастами к типам, короче оно не работеат, и мозг уже тоже не работает

                var articles = articlePublishService.PublishArticle(
                    articleService.CreateArticle(filepath, "author", articleService.GetText(filepath)));

                foreach (var a in articles)
                {
                    if ((object)a is Article art)
                    {
                        Console.WriteLine($"{art.Name} \n {art.Publishdate} \n {art.Author} \n {art.Text.Text}");
                        Checks();
                    }
                }

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Checks()
        {
            if(!checkArticleService.HasHeading() || !checkArticleService.HasAuthor()) throw new ArgumentNullException("no name provided");
            checkArticleService.IsOfSetVolume(66);
        }
    }
}
