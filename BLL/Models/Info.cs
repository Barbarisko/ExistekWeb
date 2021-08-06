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
                                 set { numOfSigns = Convert.ToUInt32(Text.Trim().Count<char>()); } }
        public string? Text { get => text; set => text = value; }

    }
}
