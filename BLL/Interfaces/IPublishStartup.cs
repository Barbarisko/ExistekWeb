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
        void Publish(string filepath, uint required_volume);
        IEnumerable<string> ShowArticles(string directory);
    }
}
