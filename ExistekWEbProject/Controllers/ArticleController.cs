using BLL.ModelsNew;
using DataAccess;
using DataAccess.Entities;
using ExistekWEbProject.CustomFilters;
using ExistekWEbProject.Models;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public List<string> GetArticles([FromHeader] string directory, int numOfArticles)
        {
            var articles = publishStartup.ShowArticles(directory);
            var new_list = new List<string>();

            if (numOfArticles > articles.Count() - 1)
                return articles.ToList();

            else
            {
                for (int i = 1; i <= numOfArticles; i++)
                {
                    new_list.Add(articles.ToList()[i]);
                }
                return new_list;
            }
        }


        //implementing services        
        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult GetArticleById([FromRoute] int? id)
        {
            if (id == null)
                return NotFound();

            var article = publishContext.Articles
                .Include(a => a.ArticleTags)
                .Include(a => a.Author)
                .FirstOrDefault(a => a.Id == id);

            if (article == null || article.Author == null)
                return NotFound();

            return Json(new
            {
                Id = id,
                Name = article.Name,
                Publishdate = article.PublishDate,
                Author = article.Author.Name,
            });
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult CreateArticle([FromBody] ArticleModelInput modelInput)
        {
            if (ModelState.IsValid)
            {
                var article = articleService.AddArticleToDB(
                    modelInput.name, modelInput.authorId, modelInput.articleTags = new List<ArticleTagModel>());

                article.ArticleTags.Add(new ArticleTagModel
                {
                    Article = article,
                    Tag = new TagModel()
                    {
                        Name = "testtag"
                    }
                });

                publishContext.SaveChanges();

                return Ok(article.ToString());
            }

            return BadRequest();
        }


        // daata to test Edit
        //  "id": 6,
        //  "name": "edited",
        //  "publishDate": "2021-09-03T13:04:58.388Z",
        //  "idAuthor": 4,
        //  "author": {
        //  },
        //  "articleTags": []   }
        [HttpPost]
        public ActionResult Edit([FromQuery] int id, [FromBody] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                publishContext.Update(article);
                publishContext.SaveChanges();

                return Json(new
                {
                    Id = id,
                    Name = article.Name

                });
            }
            return BadRequest();
        }
      
        [Route("[action]/{id}")]
        [HttpPost]
        public ActionResult Delete([FromRoute] int id)
        {
            var article = publishContext.Articles.Find(id);
            publishContext.Articles.Remove(article);
            publishContext.SaveChanges();

            return Ok(publishContext.Articles.ToList());
        }
    }
}
