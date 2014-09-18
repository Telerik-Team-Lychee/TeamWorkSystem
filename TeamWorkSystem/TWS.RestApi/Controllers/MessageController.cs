namespace TWS.RestApi.Controllers
{
	using System.Linq;
	using System.Web.Http;

	using TWS.Data;
	using TWS.Models;
	using TWS.RestApi.Infrastructure;
	using TWS.RestApi.Models;

	public class MessageController : BaseApiController
	{
		private IUserIdProvider userIdProvider;

		public MessageController(ITwsData data, IUserIdProvider userIdProvider)
			: base(data)
		{
			this.userIdProvider = userIdProvider;
		}

		[HttpPost]
		public IHttpActionResult Create(MessageModel messageModel)
		{
			if (!this.ModelState.IsValid)
			{
				return BadRequest(this.ModelState);
			}

			var currentUserId = this.userIdProvider.GetUserId();

			var newMessage = new Message()
			{
				Text = messageModel.Text,
				TeamWorkId = messageModel.TeamWorkId,
				SentById = currentUserId
			};

			this.data.Messages.Add(newMessage);
			this.data.SaveChanges();

			messageModel.Id = newMessage.Id;

			return Ok(messageModel);
		}

		[HttpGet]
		public IQueryable<MessageModel> All(int id)
		{
			var messages = this.data
				.Messages
				.All()
				.Where(m => m.TeamWorkId == id)
				.Select(MessageModel.FromMessage);

			return messages;
		}

		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			var message = this.data.Messages.All().FirstOrDefault(m => m.Id == id);

			if (message == null)
			{
				return BadRequest("Invalid Id - no such message existing!");
			}

			this.data.SaveChanges();

			return Ok(message);
		}
	}
}