namespace TWS.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<TwsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TwsDbContext context)
        {
            this.SeedUsers(context);
            this.SeedTeamworks(context);
            this.SeedAssignments(context);
            this.SeedMessages(context);
        }

        private void SeedUsers(TwsDbContext context)
        {

        }

        private void SeedAssignments(TwsDbContext context)
        {

        }

        private void SeedMessages(TwsDbContext context)
        {

        }

        private void SeedTeamworks(TwsDbContext context)
        {

        }
    }
}
