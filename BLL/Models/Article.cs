using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
 //   Створити сервіси і підключити їх використовуючи Dependency injection
//Сервіси не повинні містити ніякої логіки але потрібно додати методи які умовно виконують певну дію.
//Серед сервісів повинні бути:
//- сервіс, який зберігає інформацію про статтю, яку потрібно опублікувати
//- сервіс, який вносить контент в статтю(наприклад з якогось файлу на диску)
//- сервіс, який виконує перевірку статті по якимось критеріям.
//- сервіс, який опубліковує статтю
//- сервіс, який міститиме логіку по виконанню всього процесу публікації статті.
//З контроллера запустити публікацію статті.

    //Подумайте який LifeTime(Transient, Scoped, Singleton)  краще використати для того чи іншого сервісу
    internal class Article
    {
        private string name;

        private string author;
        private DateTime publishdate;
        private Info text;
        public Info Text { get => text; set => text = value; }
        public string Name { get => name; set => name = value; }
        public string Author { get => author; set => author = value; }
        public DateTime Publishdate { get => publishdate;}

        public Article()
        {

        }

        public Article(string _name, string _author)
        {
            name = _name;
            author = _author;
            publishdate = DateTime.Now;
        }

    }
}
