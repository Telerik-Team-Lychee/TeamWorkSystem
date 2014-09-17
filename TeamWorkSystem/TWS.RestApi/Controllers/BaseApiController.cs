namespace TWS.RestApi.Controllers
{
    using System.Web.Http;

    using TWS.Data;

    
    public abstract class BaseApiController : ApiController
    {
        protected ITwsData data;

        protected BaseApiController(ITwsData data)
        {
            this.data = data;
        }
    }
}