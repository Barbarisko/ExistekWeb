using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
   public  class ArticleOld
    {
        private int id;
        private string name;
        private string author;
        private DateTime publishdate;
        private InfoOld text;
        public int Id { get => id; set => id = value; }
        public InfoOld Text { get => text; set => text = value; }

        public string Name { get => name; set => name = value; }
        public string Author { get => author; set => author = value; }
        public DateTime Publishdate { get => publishdate; }

        public ArticleOld()
        {

        }
    }

    public class InfoOld 
    {
        private uint numOfSigns = 0;
        private string text;

        public uint NumOfSigns
        {
            get { return numOfSigns; }
            set { numOfSigns = value; }
        }
        public string Text { get => text; set => text = value; }

        //public InfoOld() { }
        //public InfoOld(string text)
        //{
        //    Text = text;
        //    Set_numOfSigns(text);
        //    Console.WriteLine($"num of signs in text = {numOfSigns }");
        //}

        private void Set_numOfSigns(string? text)
        {
            numOfSigns = Convert.ToUInt32(text.Trim().Count<char>());
        }

    }
}
