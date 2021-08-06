using Interfaces;
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
    internal class ArticleService: IArticleService
    {
        private IInfoService infoService;
        Dictionary<string, string> details;

        public ArticleService(IInfoService _infoService)
        {
            infoService = _infoService;
        }

        public string GetText(string articleName)
        {
            var path = $"{articleName}.txt";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"no file with name {articleName}");
            }
            var textForArticle = File.ReadAllText(path);
            return textForArticle;
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
                if((object)p.PropertyType is IInfo info)
                {
                    infoService.AddInfo(GetText(Convert.ToString(type.GetProperty("Name"))));
                }
                details.Add($"{p.PropertyType} {p.Name}", Convert.ToString(p.GetValue(p)));
                Console.WriteLine($"{p.PropertyType} {p.Name}");
            }

            Console.WriteLine("Detailes saved");
        }
    }
}
