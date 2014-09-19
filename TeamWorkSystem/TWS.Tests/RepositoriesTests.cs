namespace TWS.Repositories.Tests
{
	using System;
	using System.Data.Entity;
	using System.Transactions;
	using System.Linq;

	using TWS.Data;
	using TWS.Data.Repositories;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using TWS.Models;
	using TWS.Models.Enumerations;
	using TWS.Services.Tests;

	[TestClass]
	public class RepositoriesTests
	{
		public TwsDbContext dbContext { get; set; }

		private static TransactionScope tran;

		public IRepository<Message> MessageRepo { get; set; }

		public RepositoriesTests()
		{
			this.dbContext = new TwsDbContext();
			this.MessageRepo = new EFRepository<Message>(this.dbContext);
			this.dbContext.Database.Initialize(false);
		}

		[TestInitialize]
		public void TestInit()
		{
			tran = new TransactionScope(TransactionScopeOption.RequiresNew);
		}

		[TestCleanup]
		public void TestCleanUp()
		{
			tran.Dispose();
		}

		[TestMethod]
		public void AddAndGetItemInTeamWorkRepository()
		{
			var teamWork = Entities.GetValidTeamWork();

			var createdTeamWork = dbContext.Teamworks.Add(teamWork);
			var teamWorkInDb = dbContext.Set<TeamWork>().Find(teamWork.Id);

			Assert.IsNotNull(teamWorkInDb);
			Assert.AreEqual(teamWork.Name, teamWorkInDb.Name);
		}

		[TestMethod]
		public void AddAndGetItemInResourcesRepository()
		{
			var resource = Entities.GetValidResource();

			var createdResource = dbContext.Resources.Add(resource);
			var resourceInDb = dbContext.Set<Resource>().Find(resource.Id);

			Assert.IsNotNull(resourceInDb);
			Assert.AreEqual(resource.Name, resourceInDb.Name);
		}

		[TestMethod]
		public void AddAndGetItemInAssignmentRepository()
		{
			var assignment = Entities.GetValidAssignment();

			var createdAssignment = dbContext.Assignments.Add(assignment);
			var assignmentInDb = dbContext.Set<Assignment>().Find(assignment.Id);

			Assert.IsNotNull(assignmentInDb);
			Assert.AreEqual(assignment.Name, assignmentInDb.Name);
		}

		[TestMethod]
		public void AddAndGetItemInRequestRepository()
		{
			var request =  Entities.GetValidTeamWorkRequest();

			var createdRequest = dbContext.TeamworkRequests.Add(request);
			var requestInDb = dbContext.Set<TeamWorkRequest>().Find(request.Id);

			Assert.IsNotNull(requestInDb);
			Assert.AreEqual(request.Message, requestInDb.Message);
		}
	}
}