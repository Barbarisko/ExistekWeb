using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    internal class Article:IArticle
    {
        private int id;
        private string name;
        private string author;
        private DateTime publishdate;
        private IInfo text; 
        public int Id { get => id; set => id = value; }
        public IInfo Text { get => text; set => text = value; }
        public string Name { get => name; set => name = value; }
        public string Author { get => author; set => author = value; }
        public DateTime Publishdate { get => publishdate;}

        public Article()
        {

        }

        public Article(string _name, string _author)
        {
            id += 1;
            name = _name;
            author = _author;
            publishdate = DateTime.Now;
        }

    }
}
