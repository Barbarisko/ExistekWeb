using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IArticlePublishService
    {
        IEnumerable<Type> PublishArticle(object obj);
    }
}
