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
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticlePublishService articlePublishService;
        private readonly ICheckArticleService checkArticleService;
        private readonly IArticleService articleService;

        private readonly ILogger<ArticleController> logger;

        public ArticleController(ILogger<ArticleController> _logger,
            IArticlePublishService _articlePublishService, ICheckArticleService _checkArticleService)
        {
            logger = _logger;
            articlePublishService = _articlePublishService;

        }

       
    }
}
