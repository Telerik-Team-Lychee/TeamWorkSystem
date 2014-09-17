using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TWS.Models;

namespace TWS.RestApi.Models
{
    public class MessageModel
    {
        public static Expression<Func<Message, MessageModel>> FromMessage
        {
            get
            {
                return m => new MessageModel
                {
                    Id = m.Id,
                    Text = m.Text,
                    PostDate = m.PostDate,
                    TeamWorkId = m.TeamWorkId,
                    SentBy = m.SentBy
                };
            }
        }

        public int Id { get; set; }

        [MinLength(1)]
        public string Text { get; set; }

        [Required]
        public DateTime PostDate { get; set; }

        public int TeamWorkId { get; set; }

        public User SentBy { get; set; }
    }
}