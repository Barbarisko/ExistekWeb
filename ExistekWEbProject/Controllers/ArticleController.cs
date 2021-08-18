using BLL;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.Controllers
{
    [ApiController]
    [Route("publish")]
    public class ArticleController : Controller
    {
        private readonly IPublishStartup publishStartup;

        private readonly ILogger<ArticleController> logger;

        public ArticleController(ILogger<ArticleController> _logger, IPublishStartup publishStartup)
        {
            logger = _logger;
            this.publishStartup = publishStartup;
        }

        [HttpGet]
        [Route("[action]/{filename:alpha:maxlength(15)}&{required_volume:min(10)}")]
        //https://localhost:5001/publish/Publish/article&18 - ok
        //https://localhost:5001/publish/Publish/artice&2 - error

        public IActionResult Publish(string filename, uint required_volume)
        {
            logger.LogInformation($"Fetching article from {filename}.txt");
            try
            {
                //if (publishStartup.Checks(required_volume))
                //{
                    publishStartup.Publish(filename);
                    logger.LogInformation($"Returning {filename}.");
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
        [Route("[action]/{dateofpublish:datetime}")]
        //https://localhost:5001/publish/Publish/34567?filename=article - 404
        //https://localhost:5001/publish/Publish/01.03.2001?filename=article - ok
        public IActionResult Publish(string filename, DateTime dateofpublish)
        {
            logger.LogInformation($"Fetching article from {filename}.txt");
            try
            {
                publishStartup.Publish(filename);
                logger.LogInformation($"Returning {filename}.");
                return Ok($"{ filename} successfully published on {dateofpublish}");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return NotFound($"{ filename} not found");

            }

        }

        [HttpGet]
        [Route("[action]/{directory:req_dir(Articles)}")]
        //https://localhost:5001/publish/GetArticles/C%3A%5CUsers%5Chelen%5Csource%5Crepos%5CExistekWEbProject%5CExistekWEbProject%5CArticles - ok
        //https://localhost:5001/publish/GetArticles/C%3A%5CUsers%5Chelen%5Csource%5Crepos%5CExistekWEbProject%5CExistekWEbProject%5Carticles - 404
        //
        public IEnumerable<string> GetArticles(string directory)
        {
            try
            {
                var articles = publishStartup.ShowArticles(directory);
                return articles.ToList();
            }
            catch (NotExistingDirectoryException e)
            {
                logger.LogDebug(e.Message);
                return new List<string>();
            }
        }
        [HttpGet]
        [Route("[action]/{numOfArticles:max(3)}")]
        //https://localhost:5001/publish/GetArticles/0?directory=C%3A%5CUsers%5Chelen%5Csource%5Crepos%5CExistekWEbProject%5CExistekWEbProject%5CArticles - ok
        //https://localhost:5001/publish/GetArticles/456?directory=C%3A%5CUsers%5Chelen%5Csource%5Crepos%5CExistekWEbProject%5CExistekWEbProject%5CArticles - error
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
    }
}
