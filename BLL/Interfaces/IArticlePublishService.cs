using BLL;
using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IArticlePublishService
    {
        IEnumerable<Article> PublishArticle(object obj);
    }
}
