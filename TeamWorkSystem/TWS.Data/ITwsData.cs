namespace TWS.Data
{
	using TWS.Data.Repositories;
	using TWS.Models;

	public interface ITwsData
	{
		IRepository<Message> Messages { get; }

		IRepository<TeamWork> TeamWorks { get; }

		IRepository<User> Users { get; }

		IRepository<Resource> Resources { get; }

		IRepository<Assignment> Assignments { get; }

		IRepository<TeamWorkRequest> TeamWorkRequests { get; }

		int SaveChanges();
	}
}