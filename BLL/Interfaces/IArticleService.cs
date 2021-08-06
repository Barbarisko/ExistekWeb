using BLL;
using System;
using System.IO;

namespace Interfaces
{
    public interface IArticleService
    {
        void SaveArticleInfo(Type type);
        string GetText(string articleName);
        Article CreateArticle(string name, string author, string text);
    }
}
