namespace TWS.RestApi.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using TWS.Data;
    using TWS.Models;
    using TWS.RestApi.Infrastructure;
    using TWS.RestApi.Models;

    public class RequestController : BaseApiController
    {
        private IUserIdProvider userIdProvider;

        public RequestController(ITwsData data, IUserIdProvider userIdProvider)
            : base(data)
        {
            this.userIdProvider = userIdProvider;
        }

        [HttpPost]
        public IHttpActionResult Send(RequestModel request)
        {
            var teamwork = this.data.TeamWorks.Find(request.TeamWorkId);
            if (teamwork == null)
            {
                return BadRequest("Teamwork does not exist - invalid id");
            }

            var currentUserId = this.userIdProvider.GetUserId();

            var newRequest = new TeamWorkRequest() 
            { 
                Message = request.Message, 
                SentById = currentUserId, 
                TeamWork = teamwork
            };

            this.data.TeamWorkRequests.Add(newRequest);
            this.data.SaveChanges();

            request.Id = newRequest.Id;
            return Ok(request);
        }

        [HttpGet]
        public IHttpActionResult ByTeamwork(int id)
        {
            var existingTeamwork = this.data
              .TeamWorks
              .Find(id);

            if (existingTeamwork == null)
            {
                return BadRequest("Teamwork does not exist - invalid id");
            }

            var requests = existingTeamwork
                .Requests
                .AsQueryable()
                .Select(RequestModel.FromRequest);

            return Ok(requests);
        }

        [HttpPost]
        public IHttpActionResult Accept(int id, int teamworkId)
        {
            var existingTeamwork = this.data
              .TeamWorks
              .Find(id);

            if (existingTeamwork == null)
            {
                return BadRequest("Teamwork does not exist - invalid id");
            }

            var currentRequest = existingTeamwork
                .Requests
                .FirstOrDefault(r => r.Id == id);

            if (currentRequest == null)
            {
                return BadRequest("Given request does not exist");
            }

            var currentUserId = this.userIdProvider.GetUserId();
            if (!existingTeamwork.Users.Any(u => u.Id == currentUserId))
            {
                return BadRequest("You don't have permissios to the given teamwork.");
            }

            var userToBeJoin = this.data.Users.Find(currentRequest.SentById);
            existingTeamwork.Users.Add(userToBeJoin);

            this.data.TeamWorkRequests.Delete(currentRequest);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Reject(int id, int teamworkId)
        {
            var existingTeamwork = this.data
              .TeamWorks
              .Find(id);

            if (existingTeamwork == null)
            {
                return BadRequest("Teamwork does not exist - invalid id");
            }

            var currentRequest = existingTeamwork
                .Requests
                .FirstOrDefault(r => r.Id == id);

            if (currentRequest == null)
            {
                return BadRequest("Given request does not exist");
            }

            var currentUserId = this.userIdProvider.GetUserId();
            if (!existingTeamwork.Users.Any(u => u.Id == currentUserId))
            {
                return BadRequest("You don't have permissios to the given teamwork.");
            }

            this.data.TeamWorkRequests.Delete(currentRequest);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
