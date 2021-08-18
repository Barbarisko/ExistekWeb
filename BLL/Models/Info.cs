using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Info:IInfo
    {
        private uint numOfSigns;
        private string text;

        public uint NumOfSigns { get { return numOfSigns; } 
                                 set { numOfSigns = value; } }
        public string? Text { get => text; set => text = value; }

        public Info()
        {
            Console.WriteLine($"num of signs in text = {numOfSigns }");
        }

        private void Set_numOfSigns(string? text)
        {
            numOfSigns = Convert.ToUInt32(text.Trim().Count<char>());
        }

    }
}
