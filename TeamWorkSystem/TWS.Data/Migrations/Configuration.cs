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
            if (context.TeamWorks.Any())
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
                        Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
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
                var teamwork = context.TeamWorks.First();

                context.Assignments.AddOrUpdate(
                    new Assignment
                    {
                       Name = "Assignment" + i,
                       Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                       Priority = i,
                       TeamWork = teamwork,
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
