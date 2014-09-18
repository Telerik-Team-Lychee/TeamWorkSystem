namespace TWS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using TWS.Models;
    using TWS.Models.Enumerations;

    internal sealed class Configuration : DbMigrationsConfiguration<TwsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TwsDbContext context)
        {
            if (context.UsersTeamworks.Any())
            {
                return;
            }

            this.SeedUsers(context);
            this.SeedTeamworks(context);
            this.SeedAssignments(context);
            this.SeedMessages(context);
        }

        private void SeedUsers(TwsDbContext context)
        {
            for (int i = 1; i <= 6; i++)
            {
                string email = "user" + i + "@asd.bg";
                context.Users.AddOrUpdate(
                    new User
                    {
                        Email = email,
                        UserName = email,
                        FirstName = "Gosho " + i,
                        LastName = "Peshov " + i,
                        PasswordHash = "asd".GetHashCode().ToString()
                    });

            }

            context.SaveChanges();
        }

        private void SeedTeamworks(TwsDbContext context)
        {
            for (int i = 1; i <= 6; i++)
            {
                context.TeamWorks.AddOrUpdate(
                    new TeamWork
                    {
                        Name = "Teamwork" + i,
                        Description = "Description" + i,
                        GitHubLink = "Github" + i,
                        EndDate = DateTime.Now.AddDays(i),
                        Category = (Category)(i % 6),
                    });
            }

            context.SaveChanges();
        }

        private void SeedAssignments(TwsDbContext context)
        {
            for (int i = 1; i <= 6; i++)
            {
                context.Assignments.AddOrUpdate(
                    new Assignment
                    {
                       Name = "Assignment" + i,
                       Priority = i,
                       Status = AssignmentStatus.Assigned,
                       TeamWork = context.TeamWorks.First(),
                    });
            }

            context.SaveChanges();
        }

        private void SeedMessages(TwsDbContext context)
        {
            for (int i = 1; i <= 6; i++)
            {
                context.Messages.AddOrUpdate(
                    new Message
                    {
                        Text = "Some text" + i,
                        SentBy = context.Users.First(),
                        TeamWork = context.TeamWorks.First()
                    });
            }

            context.SaveChanges();
        }
    }
}
