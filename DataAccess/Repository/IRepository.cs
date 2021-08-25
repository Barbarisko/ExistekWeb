﻿using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
	public interface IRepository<TEntity> where TEntity : IBaseEntity
	{
		IEnumerable<TEntity> GetAll();
		void Add(TEntity entity);
		TEntity Get(int id);
		void Update(TEntity entity);
		void Delete(int id);
	}
}
