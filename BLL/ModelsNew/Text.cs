using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Entities
{
    public partial class Text
    {
        public int IdArticle { get; set; }
        public int? ReaderRating { get; set; }
        public bool? AdultOnly { get; set; }
        public string Data { get; set; }

        public Article Article { get; set; }
    }
}
