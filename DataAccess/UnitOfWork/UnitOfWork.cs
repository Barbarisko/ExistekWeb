using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly PublishContext _context;
		public IRepository<Article> ArticleRepository { get; }
		public IRepository<Author> AuthorRepository { get; }
		public IRepository<Tag> TagRepository { get; }
		public IRepository<Text> TextRepository { get; }
		public IRepository<ArticleTag> ArticleTagRepository { get; }

        public UnitOfWork(PublishContext context, IRepository<Article> articleRepository, IRepository<Author> authorRepository, IRepository<Tag> tagRepository, IRepository<Text> textRepository, IRepository<ArticleTag> articleTagRepository)
        {
            _context = context;
            ArticleRepository = articleRepository;
            AuthorRepository = authorRepository;
            TagRepository = tagRepository;
            TextRepository = textRepository;
            ArticleTagRepository = articleTagRepository;
        }

        public void Save()
		{
			_context.SaveChanges();
		}
	}
}
