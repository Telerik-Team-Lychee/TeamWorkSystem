namespace TWS.Data
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;

	using TWS.Models;

	public interface ITwsDbContext : IDisposable
	{
		IDbSet<TeamWork> Teamworks { get; set; }

		IDbSet<Resource> Resources { get; set; }

		IDbSet<Message> Messages { get; set; }

		IDbSet<Assignment> Assignments { get; set; }

		IDbSet<TeamWorkRequest> TeamworkRequests { get; set; }

		int SaveChanges();

		DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

		IDbSet<T> Set<T>() where T : class;
	}
}