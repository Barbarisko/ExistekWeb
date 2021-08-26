using System;
using System.Collections.Generic;

#nullable disable

namespace BLL.ModelsNew
{
    public partial class TagModel : BaseEntity
    {
        public string Name { get; set; }

        public List<ArticleTagModel> ArticleTags { get; set; }
    }
}
