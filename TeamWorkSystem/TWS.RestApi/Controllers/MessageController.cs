﻿namespace TWS.RestApi.Controllers
{
    using System;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
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

        public MessageController()
            :this(new TwsData(), new AspNetUserIdProvider())
        {
            
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
                SentBy = messageModel.SentBy

            };
            this.data.Messages.Add(newMessage);
            this.data.SaveChanges();

            messageModel.Id = newMessage.Id;

            return Ok(messageModel);
        }

        [HttpGet]
        public IQueryable<MessageModel> All(int teamWorkId)
        {
            var messages = this.data.Messages.All().Select(MessageModel.FromMessage).Where(m => m.TeamWorkId == teamWorkId);

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
