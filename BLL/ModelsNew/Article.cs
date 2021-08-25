using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Entities
{
    public partial class Article:BaseEntity
    {
        public Article()
        {
            ArticleTags = new HashSet<ArticleTag>();
        }

        public string Name { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? IdAuthor { get; set; }

        public Author Author { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
