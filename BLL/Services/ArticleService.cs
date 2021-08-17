using Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //Створити сервіси і підключити їх використовуючи Dependency injection
//Сервіси не повинні містити ніякої логіки але потрібно додати методи які умовно виконують певну дію.
//Серед сервісів повинні бути:
//- сервіс, який зберігає інформацію про статтю, яку потрібно опублікувати

    //LifeTime - Scoped
    public class ArticleService: IArticleService
    {
        private IInfoService infoService;
        Dictionary<string, string> details;
        public Article article;
        private ILogger<ArticleService> logger;
        public ArticleService(IInfoService _infoService, ILogger<ArticleService> logger)
        {
            infoService = _infoService;
            this.logger = logger;
        }

        public string GetText(string articleName)
        {
            var path = $"{articleName}.txt";

            if (!File.Exists(path))
            {
                File.Create(path);
                throw new FileNotFoundException($"no file with name {articleName}");
            }
            var textForArticle = File.ReadAllText(path);
            return textForArticle;
        }

        public Article CreateArticle(string name, string author, string text)
        {

            article = new Article { Name = name, Author = author, Text = new Info { Text = text} };

            return article;
        }
        
        public void SaveArticleInfo(Type type)
        {
            details = new Dictionary<string, string>();

            var properties = type.GetProperties();

            if (type == null)
            {
                throw new ArgumentNullException("no article to save");
            }

            foreach (var p in properties)
            {
                logger.LogDebug(Convert.ToString(type.GetProperty(p.Name)));

                if ((object)p.PropertyType is Info info)
                {
                    infoService.AddInfo(GetText(Convert.ToString(type.GetProperty("Name"))));
                    
                }        
                details.Add($"{p.PropertyType} {p.Name}", Convert.ToString(p.GetValue(p)));
                logger.LogDebug($"{p.PropertyType} {p.Name}");       
            }
            logger.LogDebug("Detailes saved");
        }
    }
}
