namespace TWS.RestApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using TWS.Data;
    using TWS.Models;
    using TWS.RestApi.Infrastructure;
    using TWS.RestApi.Models;

    public class TeamWorkController : BaseApiController
    {
        private IUserIdProvider userIdProvider;

        public TeamWorkController(ITwsData data, IUserIdProvider userIdProvider)
            : base(data)
        {
            this.userIdProvider = userIdProvider;
        }

        [HttpGet]
        public IQueryable<TeamworkModel> All()
        {
            var teamworks = this.data
                .TeamWorks
                .All()
                .Select(TeamworkModel.FromTeamwork);

            return teamworks;
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var teamwork = this.GetCurrentTeamworkModel(id);
            if (teamwork == null)
            {
                return BadRequest("Teamwork does not exist - invalid id");
            }

            return Ok(teamwork);
        }

        [HttpPost]
        public IHttpActionResult Create(TeamworkModel teamWork)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var currentUserId = this.userIdProvider.GetUserId();

            var newTeamwork = new TeamWork()
            {
                Name = teamWork.Name,
                Description = teamWork.Description,
                GitHubLink = teamWork.GitHubLink,
                EndDate = teamWork.EndDate,
                Category = teamWork.Category
            };

            newTeamwork.Users.Add(new UsersTeamWorks() { Id = currentUserId, IsAdmin = true });
            this.data.TeamWorks.Add(newTeamwork);
            this.data.SaveChanges();

            teamWork.Id = newTeamwork.Id;
            return Ok(teamWork);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingTeamwork = this.data.TeamWorks.Find(id);
            if (existingTeamwork == null)
            {
                return BadRequest("Teamwork does not exist - invalid id");
            }

            this.data.TeamWorks.Delete(existingTeamwork);
            this.data.SaveChanges();

            return Ok();
        }

        private TeamworkModel GetCurrentTeamworkModel(int id)
        {
            return this.data.TeamWorks
                .All()
                .Where(t => t.Id == id)
                .Select(TeamworkModel.FromTeamwork)
                .FirstOrDefault();
        }
    }
}