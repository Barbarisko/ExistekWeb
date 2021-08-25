using BLL;
using ExistekWEbProject.CustomFilters;
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
    }
}
