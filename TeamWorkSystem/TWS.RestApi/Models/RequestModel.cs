namespace TWS.RestApi.Models
{
    using System;
    using System.Linq.Expressions;

    using TWS.Models;

    public class RequestModel
    {
        public static Expression<Func<TeamWorkRequest, RequestModel>> FromRequest
        {
            get
            {
                return r => new RequestModel
                {
                    Id = r.Id,
                    Message = r.Message,
                    TeamWorkId = r.TeamWorkId,
                    SentBy = r.SentBy.UserName
                };
            }
        }

        public int Id { get; set; }

        public string Message { get; set; }

        public virtual string SentBy { get; set; }

        public int TeamWorkId { get; set; }
    }
}