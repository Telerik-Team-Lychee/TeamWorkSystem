namespace TWS.Services.Tests
{
	using System;
	using System.Web.Http;
	using System.Net.Http;
	using System.Linq;

	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System.Web.Http.Routing;
	using System.Web.Http.Controllers;
	using System.Web.Http.Hosting;
	using Telerik.JustMock;
	using TWS.Data.Repositories;
	using TWS.Models;
	using TWS.RestApi.Models;
	using TWS.RestApi.Controllers;
	using TWS.Services.Tests.Fakes;
	using TWS.Data;
	using TWS.RestApi.Infrastructure;
	using System.Collections.Generic;
	using TWS.Models.Enumerations;

	[TestClass]
	public class MessageControllerTests
	{
		private void SetupController(ApiController controller)
		{
			var config = new HttpConfiguration();
			var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/message");
			var route = config.Routes.MapHttpRoute(
				name: "Default",
				routeTemplate: "{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			var routeData = new HttpRouteData(route);
			routeData.Values.Add("id", RouteParameter.Optional);
			routeData.Values.Add("controller", "message");
			controller.ControllerContext = new HttpControllerContext(config, routeData, request);
			controller.Request = request;
			controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
			controller.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
		}

		[TestMethod]
		public void AddWhenMessageIsValidShouldAddMessage()
		{
			var repository = Mock.Create<ITwsData>();

			var messageEntity = new Message()
			{
				Id = 1,
				Text = "What's up",
				SentBy = GetValidUser(),
				TeamWork = GetValidTeamWork()
			};

			IList<Message> messageEntities = new List<Message>();
			messageEntities.Add(messageEntity);
			Mock.Arrange(() => repository.Messages.All())
				.Returns(() => messageEntities.AsQueryable());

			var userProvider = Mock.Create<IUserIdProvider>();

			var controller = new MessageController(repository, userProvider);

			var messageModels = controller.All(0);
			Assert.IsTrue(messageModels.Count() == 1);
			Assert.AreEqual(messageEntity.Text, messageModels.First().Text);
		}

		private TeamWork GetValidTeamWork()
		{
			var teamWork = new TeamWork()
			{
				Name = "WebServices",
				Description = "Description",
				EndDate = new DateTime(2015, 2, 1),
				Category = Category.CSharp,
			};

			return teamWork;
		}
		private User GetValidUser()
		{
			var user = new User()
			{
				FirstName = "Pesho",
				LastName = "Peshev",
				IsOnline = false
			};

			return user;
		}
	}
}