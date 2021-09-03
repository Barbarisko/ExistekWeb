using Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        IConfiguration configuration;


        public PublishStartup(IArticlePublishService _articlePublishService, ICheckArticleService _checkArticleService, IArticleService articleService, ILogger<PublishStartup> logger, IConfiguration configuration)
        {
            articlePublishService = _articlePublishService;
            checkArticleService = _checkArticleService;
            this.articleService = articleService;
            this.logger = logger;
            this.configuration = configuration;
        }

        public void Publish(string filepath)
        {
            try
            {
                //тут я запуталась с кастами к типам, короче оно не работaeт, и мозг уже тоже не работает
                List<Article> articles = articlePublishService.PublishArticle(
                    articleService.CreateArticle(filepath, "author", articleService.GetText(filepath))).ToList();

                foreach (var a in articles.ToList())
                {
                    if ((object)a is Article art)
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
            var path = configuration.GetValue<string>("ArticleDirCnfg:Path");
            var ext = configuration.GetValue<string>("ArticleDirCnfg:Extension");

            if (directory != path)
                throw new NotExistingDirectoryException($"This directory is invalid: {directory}");

            var a = Directory.GetFiles(path, ext)
                .Select(Path.GetFileName);
            foreach (var b in a)
                logger.LogWarning($"{b} \n");

            return a;
        }
        public bool Checks(uint articlevolume)
        {
            if (!checkArticleService.HasHeading() || !checkArticleService.HasAuthor() 
                || !checkArticleService.IsOfSetVolume(articlevolume)) return false;

            return true;
        }
    }
}
