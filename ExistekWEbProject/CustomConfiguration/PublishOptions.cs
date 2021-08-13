using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject
{
    //this publish options will be applied, if no article is created
    //Configurations will be inserted here

    //Створити власну конфігурацію і застосувати її в проекті через проекцію на клас
    //Вимоги до конфігурації:
    //- повинні бути групування
    //- повинно підтримувати різні типи даних
    //Якщо хтось розробить власний провайдер конфігурації, то це дасть йому додаткові бонуси.
    // Цей провайдер повинен суттєво відрізнятись від того, що ми розглядали. 
    public class PublishOptions
    {
        private string filename;
        private string author;
        private DateTime publishDate;
        private InfoOptions infoConfig;

        public string Filename { get => filename; set => filename = value; }
        public string Author { get => author; set => author = value; }
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }
        public InfoOptions InfoConfig { get => infoConfig; set => infoConfig = value; }
    }

    public class InfoOptions
    {
        private string testText;

        public string TestText { get => testText; set => testText = value; }
    }
}
