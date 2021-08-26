using System;
using System.Collections.Generic;

#nullable disable

namespace BLL.ModelsNew
{
    public partial class TextModel
    {
        public int IdArticle { get; set; }
        public int? ReaderRating { get; set; }
        public bool? AdultOnly { get; set; }
        public string Data { get; set; }

        public ArticleModel Article { get; set; }
    }
}
