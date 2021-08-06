using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICheckArticleService
    {
        bool HasHeading();
        bool HasAuthor();
        bool HasText();
        bool IsOfSetVolume(uint volume);
        bool ContainsWords(string[] words);
    }
}
