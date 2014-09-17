namespace TWS.RestApi.Controllers
{
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TWS.Data;
using TWS.Models;
using TWS.RestApi.Infrastructure;
    public class MessageController : BaseApiController
    {
        private IUserIdProvider userIDProvider;
        public MessageController(ITwsData data, IUserIdProvider userIdProvider)
            : base(data)
        {
            this.userIDProvider = userIdProvider;
        }

        public MessageController()
            :this(new TwsData(), new AspNetUserIdProvider())
        {
            
        }

        [HttpPost]
        public IHttpActionResult Create(Message message)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            this.data.Messages.Add(message);
            this.data.SaveChanges();

            return Ok(message);
        }

        [HttpGet]
        public IHttpActionResult All(int teamWorkId)
        {
            var messages = this.data.Messages.All().Where(m => m.TeamWorkId == teamWorkId);

            return Ok(messages);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var message = this.data.Messages.All().FirstOrDefault(m => m.Id == id);

            if (message == null)
            {
                return BadRequest("Invalid Id - no such message existing!");
            }

            return Ok(message);
        }
    }
}
