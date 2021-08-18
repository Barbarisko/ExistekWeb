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
        [Route("[action]")]
        //[Route("[action]/{filename:alpha:maxlength(7)}&&{required_volume:int}")]
        public IActionResult Publish(string filename, uint required_volume)
        {
            //logger.LogInformation($"Fetching article from {filename}.txt");

            if (publishStartup.Checks(required_volume))
                publishStartup.Publish(filename);
            else Console.WriteLine("pizdets");

            //throw new AccessViolationException($"Exception while fetching article from {filename}.txt.");
            //logger.LogInformation($"Returning {filename}.");

            return Ok($"{ filename} successfully published");
        }

        [HttpGet]
        [Route("[action]/{directory:maxlength(200)}")]
        public IEnumerable<string> GetArticles(string directory)
        {
            try {
                var articles = publishStartup.ShowArticles(directory);
            return articles.ToList(); 
            }
            catch(NotExistingDirectoryException e)
            {
                logger.LogDebug(e.Message);
                return new List<string>();
            }
        }
    }
}
