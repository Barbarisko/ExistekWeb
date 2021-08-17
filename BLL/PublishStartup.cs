using Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PublishStartup> logger;


        public PublishStartup(IArticlePublishService _articlePublishService, ICheckArticleService _checkArticleService, IArticleService articleService, ILogger<PublishStartup> logger)
        {
            articlePublishService = _articlePublishService;
            checkArticleService = _checkArticleService;
            this.articleService = articleService;
            this.logger = logger;
        }

        public void Publish(string filepath, uint required_volume)
        {
            try
            {
                //тут я запуталась с кастами к типам, короче оно не работaeт, и мозг уже тоже не работает

                var articles = articlePublishService.PublishArticle(
                    articleService.CreateArticle(filepath, "author", articleService.GetText(filepath)));

                articles.ToList().Add(new Article());

                foreach (var a in articles.ToList())
                {
                    if ((object)a is Article art && Checks(required_volume))
                    {
                        
                        logger.LogInformation($"{art.Name} \n {art.Publishdate} \n {art.Author} \n {art.Text.Text}");
                    }
                }

            }
            catch (Exception e)
            {
                logger.LogError($"trouble happened : {e.GetType()} \n {e.Message}");
            }
        }

        public IEnumerable<Article> ShowArticles(string directory)
        {
            try
            {
                return articlePublishService.PublishArticle(
                    articleService.CreateArticle(directory, "author", articleService.GetText(directory)));
            }
            catch
            {
                throw new NullReferenceException("no article to print");
            }
        }
        private bool Checks(uint articlevolume)
        {
            if (!checkArticleService.HasHeading() || !checkArticleService.HasAuthor() 
                || !checkArticleService.IsOfSetVolume(articlevolume)) return false;

            return true;
        }
    }
}
