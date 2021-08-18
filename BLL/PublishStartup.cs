using Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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

        public IEnumerable<string> ShowArticles(string directory)
        {

            //get this to configs
            var path = "C:\\Users\\helen\\source\\repos\\ExistekWEbProject\\ExistekWEbProject\\Articles";
            var ext = "*.txt";
            if (directory != path)
                //throw new NotExistingDirectoryException("This directory is invalid: ", directory);
                Console.WriteLine("vcgfcgrxrx");

            var a = Directory.GetFiles(path, ext)
                                  .Select(Path.GetFileName);
            foreach (var b in a) logger.LogWarning($"{b} \n");

            return a;
        }
        private bool Checks(uint articlevolume)
        {
            if (!checkArticleService.HasHeading() || !checkArticleService.HasAuthor() 
                || !checkArticleService.IsOfSetVolume(articlevolume)) return false;

            return true;
        }
    }
}
