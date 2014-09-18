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
		private IUserIdProvider userIDProvider;
		public MessageController(ITwsData data, IUserIdProvider userIdProvider)
			: base(data)
		{
			this.userIDProvider = userIdProvider;
		}

		[HttpPost]
		public IHttpActionResult Create(MessageModel messageModel)
		{
			if (!this.ModelState.IsValid)
			{
				return BadRequest(this.ModelState);
			}

			var newMessage = new Message()
			{
				Id = messageModel.Id,
				Text = messageModel.Text,
				PostDate = messageModel.PostDate,
				TeamWorkId = messageModel.TeamWorkId,
				SentBy = messageModel.SentById

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