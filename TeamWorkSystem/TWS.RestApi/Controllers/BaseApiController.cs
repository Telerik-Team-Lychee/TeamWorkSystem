namespace TWS.RestApi.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using TWS.Data;

    //[Authorize]
    //[EnableCors("*", "*", "*", SupportsCredentials = true)]
    public abstract class BaseApiController : ApiController
    {
        protected ITwsData data;

        protected BaseApiController(ITwsData data)
        {
            this.data = data;
        }
    }
}