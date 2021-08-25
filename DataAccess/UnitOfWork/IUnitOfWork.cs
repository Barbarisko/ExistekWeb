using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
	public interface IUnitOfWork
	{
		IRepository<Article> ArticleRepository { get; }
		IRepository<Author> AuthorRepository { get; }
		IRepository<Tag> TagRepository { get; }
		IRepository<Text> TextRepository { get; }
		IRepository<ArticleTag> ArticleTagRepository { get; }
		

		void Save();
	}
}
