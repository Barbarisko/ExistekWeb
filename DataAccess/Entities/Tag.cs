using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Entities
{
    public partial class Tag : BaseEntity
    {
        public Tag()
        {
            ArticleTags = new HashSet<ArticleTag>();
        }

        public string Name { get; set; }

        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
