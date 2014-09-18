namespace TWS.RestApi.Controllers
{
    using System;
	using System.Linq;
	using System.Web.Http;
	using System.Web.Http.Cors;

	using TWS.Data;
	using TWS.Models;
    using TWS.Models.Enumerations;
	using TWS.RestApi.Infrastructure;
    using TWS.RestApi.Models;

	[EnableCors("*", "*", "*")]
	public class AssignmentController : BaseApiController
	{
		private IUserIdProvider userIdProvider;

		public AssignmentController(ITwsData data)//, IUserIdProvider userIdProvider)
			: base(data)
		{
			//this.userIdProvider = userIdProvider;
		}
        [EnableCors("*", "*", "*")]
		[HttpGet]
		public IHttpActionResult ByTeamwork(int id)
		{
			var existingTeamwork = this.data
			   .TeamWorks
			   .All()
			   .FirstOrDefault(t => t.Id == id);

			if (existingTeamwork == null)
			{
				return BadRequest("Teamwork does not exist - invalid id");
			}

            var assignments = existingTeamwork
                .Assignments
                .AsQueryable()
                .Select(AssignmentModel.FromAssignment);

			return Ok(assignments);
		}

		[HttpPost]
		public IHttpActionResult Create(int id, AssignmentModel assignment)
		{
			if (!this.ModelState.IsValid)
			{
				return BadRequest(this.ModelState);
			}

			var teamwork = this.data.TeamWorks.Find(assignment.Id);
			if (teamwork == null)
			{
				return BadRequest("Teamwork does not exist - invalid id");
			}

            var newAssignment = new Assignment
            {
                Name = assignment.Name,
                Description = assignment.Description,
                Priority = assignment.Priority
            };

            this.data.Assignments.Add(newAssignment);
			this.data.SaveChanges();

            assignment.Id = newAssignment.Id;
			return Ok(assignment);
		}

		[HttpPut]
		public IHttpActionResult Put(int id, AssignmentModel assignment)
		{
			if (!this.ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existingAssignment = this.GetCurrentAssignment(id);
			if (existingAssignment == null)
			{
				return BadRequest("Assignment does not exist.");
			}

            var newAssignment = new Assignment
            {
                Name = assignment.Name,
                Description = assignment.Description,
                Priority = assignment.Priority,
                Status = (AssignmentStatus)Enum.Parse(typeof(AssignmentStatus), assignment.Status)
            };

            existingAssignment = newAssignment;
			this.data.SaveChanges();

			return Ok();
		}

		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			var existingAssignment = this.GetCurrentAssignment(id);
			if (existingAssignment == null)
			{
				return BadRequest("Assignment does not exist - invalid id");
			}

			this.data.Assignments.Delete(existingAssignment);
			this.data.SaveChanges();

			return Ok();
		}

		private Assignment GetCurrentAssignment(int id)
		{
			return this.data.Assignments.Find(id);
		}
	}
}
