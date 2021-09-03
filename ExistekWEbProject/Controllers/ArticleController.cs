using BLL;
using BLL.ModelsNew;
using DataAccess;
using ExistekWEbProject.CustomFilters;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExistekWEbProject.Controllers
{
    [ApiController]
    [Route("publish")]
    public class ArticleController : Controller
    {
        private readonly IPublishStartup publishStartup;
        private readonly IArticleService articleService;

        private PublishContext publishContext;
        private readonly ILogger<ArticleController> logger;

        public ArticleController(ILogger<ArticleController> _logger, IPublishStartup publishStartup,
            PublishContext publishContext, IArticleService articleService)
        {
            logger = _logger;
            this.publishStartup = publishStartup;
            this.publishContext = publishContext;
            this.articleService = articleService;
        }

        [HttpGet]    
        [Route("[action]")]
        public IActionResult Publish(string filename, uint required_volume)
        {
            logger.LogInformation($"Fetching article from {filename}.txt");
            try
            {
                //if (publishStartup.Checks(required_volume))
                //{
                    publishStartup.Publish(filename);
                    logger.LogDebug($"Returning {filename}.");
                return Ok($"{ filename} successfully published");

                //}
                //else throw new AccessViolationException($"Exception while fetching article from {filename}.txt.");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return NotFound($"{ filename} not found");
            }
        }


        [HttpGet]
        [Route("[action]")]
        [ServiceFilter(typeof(DirectoryExceptionFilter))]
        public List<string> GetArticles(string directory, int numOfArticles)
        {
            var articles = publishStartup.ShowArticles(directory);
            var new_list = new List<string>();

            if (numOfArticles > articles.Count())
                return articles.ToList();

            else
            {
                for (int i = 0; i <= numOfArticles; i++)
                {
                    new_list.Add(articles.ToList()[i]);
                }
                return new_list;
            }
        }

        //implementing services
        [HttpPost]
        public ActionResult CreateArticle()
        {
            var id = articleService.AddArticleToDB("test", 1, 
                new List<ArticleTagModel>() {
                    new ArticleTagModel() { IdArticle=1, IdTag = 2} 
                });
            return Json(new { ArticleId =  id});
        }

    }
}
