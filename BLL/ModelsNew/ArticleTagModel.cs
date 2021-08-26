using System;
using System.Collections.Generic;

#nullable disable

namespace BLL.ModelsNew
{
    public partial class ArticleTagModel : BaseEntity
    {
        public int? IdArticle { get; set; }
        public int? IdTag { get; set; }

        public ArticleModel Article { get; set; }
        public TagModel Tag { get; set; }
    }
}
