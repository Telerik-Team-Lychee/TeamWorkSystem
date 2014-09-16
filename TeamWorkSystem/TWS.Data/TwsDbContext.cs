namespace TWS.Data
{
	using Microsoft.AspNet.Identity.EntityFramework;
	using System.Data.Entity;

	using TWS.Data.Migrations;
	using TWS.Models;

	public class TwsDbContext : IdentityDbContext<User>
	{
		public TwsDbContext()
			: base("TeamWorkSystemConnection", throwIfV1Schema: false)
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<TwsDbContext, Configuration>());
		}

		public IDbSet<TeamWork> TeamWorks { get; set; }

		public IDbSet<Resource> Resources { get; set; }

		public IDbSet<User> Users { get; set; }

		public IDbSet<Message> Messages { get; set; }

		public IDbSet<TeamWorkRequest> Request { get; set; }

		public IDbSet<Assignment> Assignments { get; set; }

		public IDbSet<TeamWorkRequest> TeamWorkRequests { get; set; }

		public static TwsDbContext Create()
		{
			return new TwsDbContext();
		}
	}
}