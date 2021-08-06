using Interfaces;
using System;

namespace BLL
{

    //    Створити сервіси і підключити їх використовуючи Dependency injection
    //Сервіси не повинні містити ніякої логіки але потрібно додати методи які умовно виконують певну дію.
    //Серед сервісів повинні бути:
    //- сервіс, який вносить контент в статтю(наприклад з якогось файлу на диску)

    //Подумайте який LifeTime(Transient, Scoped, Singleton)  краще використати для того чи іншого сервісу
      public class InfoService: IInfoService
    {
        private IArticle article;
        public InfoService(IArticle _article)
        {
            article = _article;
        }
        public void AddInfo(string newText)
        {
            if(article.Text.Text == null || article.Text.NumOfSigns == 0) 
                article.Text.Text = newText;
            else
            {
                article.Text.Text += $"\n {newText}"; 
            }

            Console.WriteLine("Written to article");
        }



    }
}
