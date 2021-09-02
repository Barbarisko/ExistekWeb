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
using System.Threading.Tasks;

namespace ExistekWEbProject.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("publish")]
    public class ArticleController : Controller
    {
        private readonly IPublishStartup publishStartup;
        private readonly IArticleService articleService;

        private PublishContext publishContext;
        private readonly ILogger<ArticleController> logger;

        public ArticleController(ILogger<ArticleController> _logger, IPublishStartup publishStartup, 
            PublishContext publishContext)
        {
            logger = _logger;
            this.publishStartup = publishStartup;
            this.publishContext = publishContext;
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
        [TypeFilter(typeof(DirectoryExceptionFilter))]
        [Route("[action]")]
        public List<string> GetArticles(string directory, int numOfArticles)
        {
            try
            {
                var articles = publishStartup.ShowArticles(directory);
                var new_list = new List<string>();
                for (int i = 0; i<=numOfArticles; i++)
                {
                    new_list.Add(articles.ToList()[i]);
                }
                return new_list;
            }
            catch (NotExistingDirectoryException e)
            {
                logger.LogDebug(e.Message);
                return new List<string>();
            }
        }

        //implementing services
        [HttpPost]
        public ActionResult CreateArticle()
        {
            var id = articleService.AddArticleToDB("test", 1, new List<ArticleTagModel>() { new ArticleTagModel() { IdArticle=1, IdTag = 2} });
            return Json(new { ArticleId =  id});
        }

        [HttpGet]
        [Route("[action]")]
        [System.Web.Http.Description.ResponseType(typeof(Article))]
        public System.Web.Http.IHttpActionResult Get(int id)
        {
            var emp = publishContext.OldArticle.Find(id);

            if (emp == null)
            {
                throw new NotExistingDirectoryException("Record Not Found, It may be removed");
            }
            return (System.Web.Http.IHttpActionResult)Ok(emp);
        }

    }
}
