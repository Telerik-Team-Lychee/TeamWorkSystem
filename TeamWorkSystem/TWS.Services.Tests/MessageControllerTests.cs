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
		[TestMethod]
		public void AddWhenMessageIsValidShouldAddMessage()
		{
			var repository = Mock.Create<ITwsData>();

			var messageEntity = Entities.GetValidMessage();

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
	}
}