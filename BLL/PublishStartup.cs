using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PublishStartup
    {
        private readonly IArticlePublishService articlePublishService;
        private readonly ICheckArticleService checkArticleService;

        public PublishStartup(IArticlePublishService _articlePublishService, ICheckArticleService _checkArticleService)
        {
            articlePublishService = _articlePublishService;
            checkArticleService = _checkArticleService;
        }
    }
}
