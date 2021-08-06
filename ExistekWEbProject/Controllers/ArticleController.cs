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
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticlePublishService articlePublishService;
        private readonly ICheckArticleService checkArticleService;

        private readonly ILogger<ArticleController> logger;

        public ArticleController(ILogger<ArticleController> _logger,
            IArticlePublishService _articlePublishService, ICheckArticleService _checkArticleService)
        {
            logger = _logger;
        }

        [HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new 
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        //[HttpPost]
        //public IEnumerable<WeatherForecast> Get()
        //{

        //}



        private ActionResult testResult(int id)
        {
            return Json(new
            {
                //// user input
                //input = id,
                //// just so there's different content in the response
                //when = DateTime.Now,
                //// type of request
                //req = this.Request.HttpMethod,
                //// differentiate calls in response, for matching up
                //call = new StackTrace().GetFrame(1).GetMethod().Name
            },
                            JsonRequestBehavior.AllowGet);
        }
        public ActionResult Test(int id)
        {
            return testResult(id);
        }
        [HttpGet]
        public ActionResult TestGetOnly(int id)
        {
            return testResult(id);
        }
        [HttpPost]
        public ActionResult TestPostOnly(int id)
        {
            return testResult(id);
        }
        [HttpPost, HttpGet]
        public ActionResult TestBoth(int id)
        {
            return testResult(id);

        }
    }
}
