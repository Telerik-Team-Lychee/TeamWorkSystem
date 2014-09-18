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

		public IDbSet<Message> Messages { get; set; }

		public IDbSet<Assignment> Assignments { get; set; }

		public IDbSet<TeamWorkRequest> TeamWorkRequests { get; set; }

        public IDbSet<UsersTeamWorks> UsersTeamworks { get; set; }

		public static TwsDbContext Create()
		{
			return new TwsDbContext();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TeamWorkRequest>()
				.HasRequired(user => user.SentBy)
				.WithMany(request => request.TeamWorkRequests)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Message>()
				.HasRequired(user => user.SentBy)
				.WithMany(message => message.Messages)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Resource>()
				.HasRequired(user => user.UploadedBy)
				.WithMany(resource => resource.Resources)
				.WillCascadeOnDelete(false);

			base.OnModelCreating(modelBuilder);
		}
	}
}