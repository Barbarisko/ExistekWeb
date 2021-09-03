using BLL;
using BLL.ModelsNew;
using System;
using System.Collections.Generic;
using System.IO;

namespace Interfaces
{
    public interface IArticleService
    {
        void SaveArticleInfo(Type type, object obj);
        string GetText(string articleName);
        Article CreateArticle(string name, string author, string text);
        ArticleModel AddArticleToDB(string name, int authorID, List<ArticleTagModel> tags);
    }
}
