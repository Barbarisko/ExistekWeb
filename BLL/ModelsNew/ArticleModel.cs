using BusinessLogic.CustomValidation;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable

namespace BLL.ModelsNew
{
    public partial class ArticleModel:BaseEntity
    {
        [StringCharsOnly("^[a-zA-Z0-9 ]*$")]
        public string Name { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? IdAuthor { get; set; }

        public AuthorModel Author { get; set; }
        public List<ArticleTagModel> ArticleTags { get; set; }
    }
}
