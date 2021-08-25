using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Entities
{
    public partial class ArticleTag : BaseEntity
    {
        public int? IdArticle { get; set; }
        public int? IdTag { get; set; }

        public Article Article { get; set; }
        public Tag Tag { get; set; }
    }
}
