using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IPublishStartup
    {
        void Publish(string filepath);
        bool Checks(uint articlevolume);
        IEnumerable<string> ShowArticles(string directory);

    }
}
