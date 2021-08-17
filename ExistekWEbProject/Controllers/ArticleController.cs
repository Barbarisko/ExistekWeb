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
        [Route("[action]/{filename:}")]
        public IActionResult Publish(string filename, uint required_volume)
        {
            logger.LogInformation($"Fetching article from {filename}.txt");

            publishStartup.Publish(filename, required_volume);

            throw new AccessViolationException($"Exception while fetching article from {filename}.txt.");

            logger.LogInformation($"Returning {filename}.");

            return Ok(filename);
        }

        [HttpGet]
        [Route("publish/[action]")]
        public IEnumerable<Article> GetArticles(string directory)
        {
            var articles = publishStartup.ShowArticles(directory);
            return articles.ToList();
        }
    }
}
