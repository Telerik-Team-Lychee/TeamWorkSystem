namespace TWS.Services.IntegrationTests
{
	using System;
	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Telerik.JustMock;
	using TWS.Data.Repositories;
	using TWS.Models;
	using System.Collections.Generic;
	using TWS.Services.Tests;
	using TWS.Data;
	using System.Net;

	[TestClass]
	public class TeamWorkControllerIntegrationTests
	{
		[TestMethod]
		public void GetAllWhenOneTeamWorkShouldReturnStatusCode200AndNotNullContent()
		{
			var mockRepository = Mock.Create<ITwsData>();
			var models = new List<TeamWork>();
			models.Add(Entities.GetValidTeamWork());

			Mock.Arrange(() => mockRepository.TeamWorks.All())
				.Returns(() => models.AsQueryable());

			var server = new InMemoryHttpServer<TeamWork>("http://localhost/", mockRepository.TeamWorks);

			var response = server.CreateGetRequest("/teamwork/all");

			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			Assert.IsNotNull(response.Content);
		}
	}
}
