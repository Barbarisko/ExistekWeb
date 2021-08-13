﻿using Interfaces;
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
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        private readonly IPublishStartup publishStartup;

        private readonly ILogger<ArticleController> logger;

        public ArticleController(ILogger<ArticleController> _logger, IPublishStartup publishStartup)
        {
            logger = _logger;
            this.publishStartup = publishStartup;
        }

        //[HttpGet]
        //public IActionResult Publish(string filename)
        //{
        //    logger.LogInformation($"Fetching article from {filename}.txt");

        //    publishStartup.Publish(filename);

        //    throw new AccessViolationException($"Exception while fetching article from {filename}.txt.");

        //    logger.LogInformation($"Returning {filename}.");

        //    return Ok(filename);
        //}


    }
}
