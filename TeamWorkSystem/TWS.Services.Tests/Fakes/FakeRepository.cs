namespace TWS.Services.Tests.Fakes
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using TWS.Data.Repositories;

	public class FakeRepository<T> : IRepository<T> where T: class
	{
		public IList<T> entities = new List<T>();

		public IQueryable<T> All()
		{
			return this.entities.AsQueryable();
		}

		public T Find(object id)
		{
			throw new System.NotImplementedException();
		}

		public void Add(T entity)
		{
			this.entities.Add(entity);
		}

		public void Update(T entity)
		{
			throw new NotImplementedException();
		}

		public T Delete(T entity)
		{
			this.entities.Remove(entity);

			return entity;
		}

		public T Delete(object id)
		{
			throw new NotImplementedException();
		}

		public int SaveChanges()
		{
			throw new NotImplementedException();
		}
	}
}
