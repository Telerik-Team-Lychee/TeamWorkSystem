namespace TWS.RestApi.Models
 {
     using System;
     using System.ComponentModel.DataAnnotations;
     using System.Linq.Expressions;

     using TWS.Models;

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
                     TeamWorkId = m.TeamWorkId,
                     SentBy = m.SentBy.UserName,
                     PostDate = m.PostDate
                 };
             }
         }

         public int Id { get; set; }

         [MinLength(1)]
         public string Text { get; set; }

         public int TeamWorkId { get; set; }

         public string SentBy { get; set; }

         public DateTime PostDate { get; set; }
     }
 }