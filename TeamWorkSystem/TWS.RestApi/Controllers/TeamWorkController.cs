namespace TWS.RestApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using TWS.Data;
    using TWS.RestApi.Infrastructure;
    using TWS.Models;

    public class TeamWorkController : BaseApiController
    {
        private IUserIdProvider userIdProvider;

        public TeamWorkController()
            : this(new TwsData(), new AspNetUserIdProvider())
        {
        }

        public TeamWorkController(ITwsData data, IUserIdProvider userIdProvider)
            : base(data)
        {
            this.userIdProvider = userIdProvider;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var teamworks = this.data.TeamWorks.All();
            return Ok(teamworks);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var teamwork = this.GetCurrentTeamwork(id);
            if (teamwork == null)
            {
                return BadRequest("Teamwork does not exist - invalid id");
            }

            return Ok(teamwork);
        }

        [HttpPost]
        public IHttpActionResult Create(TeamWork teamWork)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var currentUserId = this.userIdProvider.GetUserId();

            teamWork.Users.Add(new UsersTeamWorks() { Id = currentUserId, IsAdmin = true });
            this.data.TeamWorks.Add(teamWork);
            this.data.SaveChanges();

            return Ok(teamWork);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingTeamwork = this.GetCurrentTeamwork(id);
            if (existingTeamwork == null)
            {
                return BadRequest("Teamwork does not exist - invalid id");
            }

            this.data.TeamWorks.Delete(existingTeamwork);
            this.data.SaveChanges();

            return Ok();
        }

        private TeamWork GetCurrentTeamwork(int id)
        {
            return this.data.TeamWorks.Find(id);
        }
    }
}