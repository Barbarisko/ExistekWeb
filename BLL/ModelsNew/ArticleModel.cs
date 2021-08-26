using System;
using System.Collections.Generic;

#nullable disable

namespace BLL.ModelsNew
{
    public partial class ArticleModel:BaseEntity
    {
        public string Name { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? IdAuthor { get; set; }

        public AuthorModel Author { get; set; }
        public List<ArticleTagModel> ArticleTags { get; set; }
    }
}
