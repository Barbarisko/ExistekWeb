using System;
using System.Collections.Generic;

#nullable disable

namespace BLL.ModelsNew
{
    public partial class AuthorModel : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? JoinDate { get; set; }

        public List<ArticleModel> Articles { get; set; }
    }
}
