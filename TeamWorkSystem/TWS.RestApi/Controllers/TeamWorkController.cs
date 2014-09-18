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
    using TWS.Models.Enumerations;

    public class TeamWorkController : BaseApiController
    {
        private IUserIdProvider userIdProvider;

        public TeamWorkController(ITwsData data, IUserIdProvider userIdProvider)
            : base(data)
        {
            this.userIdProvider = userIdProvider;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var teamworks = this.data
                .TeamWorks
                .All()
                .Select(TeamworkModel.FromTeamwork);

            return Ok(teamworks);
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

        /// <summary>
        /// Get all values in the Category enumeration
        /// </summary>
        /// <returns>The enum Category's values</returns>
        [HttpGet]
        public IHttpActionResult GetCategories()
        {
            return Ok(Enum.GetNames(typeof(Category)));
        }

        /// <summary>
        /// Get all users that participate in the current teamwork
        /// </summary>
        /// <param name="id">Id of the specific teamwork</param>
        /// <returns>Collection of all contributors</returns>
        [HttpGet]
        public IHttpActionResult GetAllContributersById(int id)
        {
            var teamwork = this.data.TeamWorks.Find(id);
            if (teamwork == null)
            {
                return BadRequest("Teamwork does not exist - invalid id");
            }

            var teamworkUsers = teamwork
                .Users
                .Select(u =>
                    new
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        IsOnline = u.IsOnline
                    });

            return Ok(teamworkUsers);
        }

        /// <summary>
        /// Get all resource data by given teamwork id
        /// </summary>
        /// <param name="id">Id of the teamwork</param>
        /// <returns>Collection of resources for the teamwork</returns>
        [HttpGet]
        public IHttpActionResult GetAllResourcesById(int id)
        {
            var teamwork = this.data.TeamWorks.Find(id);
            if (teamwork == null)
            {
                return BadRequest("Teamwork does not exist - invalid id");
            }

            var resources = teamwork
                .Resources
                .Select(res =>
                    new
                    {
                        ResourceName = res.Name,
                        UploadedBy = res.UploadedBy.UserName,
                        Data = res.Data,
                    });

            return Ok(resources);
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
                Category = (Category)Enum.Parse(typeof(Category), teamWork.Category)
            };

            newTeamwork.Users.Add(this.data.Users.Find(currentUserId));
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