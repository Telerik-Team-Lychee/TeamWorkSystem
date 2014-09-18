namespace TWS.RestApi.Controllers
{
	using System.Linq;
	using System.Web.Http;
	using System.Web.Http.Cors;

	using TWS.Data;
	using TWS.Models;
	using TWS.RestApi.Infrastructure;

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

			var assignments = existingTeamwork.Assignments;
			return Ok(assignments);
		}

		[HttpPost]
		public IHttpActionResult Create(int id, Assignment assignment)
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

			this.data.Assignments.Add(assignment);
			this.data.SaveChanges();

			return Ok(assignment);
		}

		[HttpPut]
		public IHttpActionResult Put(int id, Assignment assignment)
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

			existingAssignment = assignment;
			this.data.SaveChanges();

			return Ok(existingAssignment);
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
