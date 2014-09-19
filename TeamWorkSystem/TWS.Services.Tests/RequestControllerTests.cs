namespace TWS.Services.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Http.Results;

	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Telerik.JustMock;
	using TWS.Data;
	using TWS.Models;
	using TWS.RestApi.Controllers;
	using TWS.RestApi.Models;
	using TWS.RestApi.Infrastructure;

	[TestClass]
	public class RequestControllerTests
	{
		[TestMethod]
		public void AddWhenRequestIsValidShouldAddRequest()
		{
			var repository = Mock.Create<ITwsData>();

			var requestEntity = Entities.GetValidTeamWorkRequest();
			var teamWorkEntity = Entities.GetValidTeamWork();
			IList<TeamWork> teamworkEntities = new List<TeamWork>();
			teamworkEntities.Add(teamWorkEntity);

			var requestEntities = new List<TeamWorkRequest>();
			requestEntities.Add(requestEntity);
			Mock.Arrange(() => repository.TeamWorkRequests.All())
				.Returns(() => requestEntities.AsQueryable());
			Mock.Arrange(() => repository.TeamWorks.Find(0))
				.Returns(() => teamworkEntities.FirstOrDefault());

			var userProvider = Mock.Create<IUserIdProvider>();

			var controller = new RequestController(repository, userProvider);
			var requestModels = controller.ByTeamwork(0);
			var negotiatedResult = requestModels as OkNegotiatedContentResult<IQueryable<RequestModel>>;
			Assert.IsNotNull(negotiatedResult);
			Assert.AreEqual<string>(requestEntity.Message, negotiatedResult.Content.FirstOrDefault().Message);
		}
	}
}