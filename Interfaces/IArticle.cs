using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{ //   Створити сервіси і підключити їх використовуючи Dependency injection
  //Сервіси не повинні містити ніякої логіки але потрібно додати методи які умовно виконують певну дію.
  //Серед сервісів повинні бути:
  //- сервіс, який зберігає інформацію про статтю, яку потрібно опублікувати
  //- сервіс, який вносить контент в статтю(наприклад з якогось файлу на диску)
  //- сервіс, який виконує перевірку статті по якимось критеріям.
  //- сервіс, який опубліковує статтю
  //- сервіс, який міститиме логіку по виконанню всього процесу публікації статті.
  //З контроллера запустити публікацію статті.

    //Подумайте який LifeTime(Transient, Scoped, Singleton)  краще використати для того чи іншого сервісу
    public interface IArticle
    {
        int Id { get; set ; }
        IInfo Text { get; set; }
        public string Name { get; set ; }
        public string Author { get ; set ; }
        public DateTime Publishdate { get ; }
    }

    public interface IInfo
    {
        uint NumOfSigns { get; set; }
        string? Text { get ; set ; }
    }
}
