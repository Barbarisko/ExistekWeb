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
        private ILogger<ArticleService> logger;

        Dictionary<string, string> details;
        public Article article;
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
                
            }

            var textForArticle = File.ReadAllText(path);
            return textForArticle;
        }

        public Article CreateArticle(string name, string author, string text)
        {

            article = new Article { 
                Name = name, 
                Author = author, 
                Text = new Info(text) 
            };
            logger.LogDebug($"{article.Text.NumOfSigns}") ;
            return article;
        }
        
        public void SaveArticleInfo(Type type, object obj)
        {
            details = new Dictionary<string, string>();

            var properties = type.GetProperties();

            if (type == null)
            {
                throw new ArgumentNullException("no article to save");
            }

            foreach (var p in properties)
            {
                if ((object)p.PropertyType is IInfo info)
                {
                    infoService.AddInfo(GetText(Convert.ToString(type.GetProperty("Name"))));                    
                }        
                details.Add($"{p.PropertyType} {p.Name}", Convert.ToString(p.GetValue(obj)));
                logger.LogDebug($"{p.PropertyType} {p.Name}");       
            }
            logger.LogDebug("Detailes saved");
        }
    }
}
