namespace TWS.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using TWS.Models;
    using TWS.Data.Migrations;

    public class TwsDbContext : IdentityDbContext<User>
    {
        public TwsDbContext()
            : base("TeamWorkSystemConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TwsDbContext, Configuration>());
        }

        public static TwsDbContext Create()
        {
            return new TwsDbContext();
        }
    }
}
