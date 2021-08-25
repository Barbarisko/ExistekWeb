using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Entities
{
    public partial class Author : BaseEntity
    {
        public Author()
        {
            Articles = new HashSet<Article>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? JoinDate { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
