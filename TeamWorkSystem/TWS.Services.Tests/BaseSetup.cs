namespace TWS.Services.Tests
{
	using System.Net.Http;
	using System.Web.Http;
	using System.Web.Http.Controllers;
	using System.Web.Http.Hosting;
	using System.Web.Http.Routing;

	public class BaseSetup
	{
		public void SetupController(ApiController controller, string routeValue)
		{
			var config = new HttpConfiguration();
			var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/" + routeValue);
			var route = config.Routes.MapHttpRoute(
				name: "Default",
				routeTemplate: "{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			var routeData = new HttpRouteData(route);
			routeData.Values.Add("id", RouteParameter.Optional);
			routeData.Values.Add("controller", routeValue);
			controller.ControllerContext = new HttpControllerContext(config, routeData, request);
			controller.Request = request;
			controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
			controller.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
		}
	}
}