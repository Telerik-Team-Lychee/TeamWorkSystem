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

	[TestClass]
	public class AssignmentControllerTests
	{
		[TestMethod]
		public void AddWhenMessageIsValidShouldAddMessage()
		{
			var repository = Mock.Create<ITwsData>();

			var assignmentEntity = Entities.GetValidAssignment();
			var teamWorkEntity = Entities.GetValidTeamWork();

			teamWorkEntity.Assignments.Add(assignmentEntity);
			IList<TeamWork> teamworkEntities = new List<TeamWork>();
			teamworkEntities.Add(teamWorkEntity);
			IList<Assignment> assignmentEntities = new List<Assignment>();
			assignmentEntities.Add(assignmentEntity);

			Mock.Arrange(() => repository.Assignments.All())
				.Returns(() => assignmentEntities.AsQueryable());
			Mock.Arrange(() => repository.TeamWorks.All())
				.Returns(() => teamworkEntities.AsQueryable());

			var controller = new AssignmentController(repository);

			var assignmentModels = controller.ByTeamwork(0);
			var negotiatedResult = assignmentModels as OkNegotiatedContentResult<IQueryable<AssignmentModel>>;
			Assert.IsNotNull(negotiatedResult);
			Assert.AreEqual<string>(assignmentEntity.Name, negotiatedResult.Content.FirstOrDefault().Name);
		}
	}
}