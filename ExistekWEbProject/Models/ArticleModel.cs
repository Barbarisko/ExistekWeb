using BLL;
using BLL.ModelsNew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.Models
{
    public class ArticleModelInput
    {
        public string name { get; set; }
        public int authorId { get; set; }

        public List<ArticleTagModel> articleTags { get; set; }

    }
}
