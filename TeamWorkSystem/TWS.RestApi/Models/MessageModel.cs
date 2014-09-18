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
                     PostDate = m.PostDate,
                     TeamWorkId = m.TeamWorkId,
                     SentBy = m.SentBy.UserName
                 };
             }
         }

         public int Id { get; set; }

         [MinLength(1)]
         public string Text { get; set; }

         [Required]
         public DateTime PostDate { get; set; }

         public int TeamWorkId { get; set; }

         public string SentBy { get; set; }
     }
 }