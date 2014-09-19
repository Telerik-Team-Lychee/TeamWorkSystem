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
	using TWS.RestApi.Infrastructure;
	using TWS.RestApi.Models;

	[TestClass]
	public class TeamWorkControllerTests
	{
		[TestMethod]
		public void AddWhenTeamWorkIsValidShouldAddTeamwork()
		{
			var repository = Mock.Create<ITwsData>();

			var teamWorkEntity = Entities.GetValidTeamWork();

			IList<TeamWork> teamWorkEntities = new List<TeamWork>();
			teamWorkEntities.Add(teamWorkEntity);
			Mock.Arrange(() => repository.TeamWorks.All())
				.Returns(() => teamWorkEntities.AsQueryable());

			var userProvider = Mock.Create<IUserIdProvider>();

			var controller = new TeamWorkController(repository, userProvider);

			var teamWorkModels = controller.All();

			var negotiatedResult = teamWorkModels as OkNegotiatedContentResult<IQueryable<TeamworkModel>>;
			Assert.IsNotNull(negotiatedResult);
			Assert.AreEqual<string>(teamWorkEntity.Name, negotiatedResult.Content.FirstOrDefault().Name);
		}
	}
}