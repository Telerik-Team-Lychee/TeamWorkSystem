namespace TWS.Tests
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

	[TestClass]
	public class RepositoriesTests
	{
		public ITwsDbContext dbContext { get; set; }

		private static TransactionScope tran;

		public IRepository<Message> MessageRepo { get; set; }

		public RepositoriesTests()
        {
			this.dbContext = new TwsDbContext();
			this.MessageRepo = new EFRepository<Message>(this.dbContext);
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
			var teamWork = GetValidTeamWork();

			var createdTeamWork = dbContext.Teamworks.Add(teamWork);
			var teamWorkInDb = dbContext.Set<TeamWork>().Find(teamWork.Id);

			Assert.IsNotNull(teamWorkInDb);
			Assert.AreEqual(teamWork.Name, teamWorkInDb.Name);
		}

		[TestMethod]
		public void AddAndGetItemInMessageRepository()
		{
			var message = GetValidMessage();

			var createdMessage = dbContext.Messages.Add(message);
			var messageInDb = dbContext.Set<Message>().Find(message.Id);

			Assert.IsNotNull(messageInDb);
			Assert.AreEqual(message.Text, messageInDb.Text);
		}

		[TestMethod]
		public void AddAndGetItemInAssignmentRepository()
		{
			var assignment = GetValidAssignment();

			var createdAssignment = dbContext.Assignments.Add(assignment);
			var assignmentInDb = dbContext.Set<Assignment>().Find(assignment.Id);

			Assert.IsNotNull(assignmentInDb);
			Assert.AreEqual(assignment.Name, assignmentInDb.Name);
		}

		[TestMethod]
		public void AddAndGetItemInRequestRepository()
		{
			var request = GetValidTeamWorkRequest();

			var createdRequest = dbContext.TeamworkRequests.Add(request);
			var requestInDb = dbContext.Set<TeamWorkRequest>().Find(request.Id);

			Assert.IsNotNull(requestInDb);
			Assert.AreEqual(request.Message, requestInDb.Message);
		}

		private Message GetValidMessage()
		{
			var message = new Message()
			{
				PostDate = DateTime.Now,
				TeamWork = GetValidTeamWork(),
				SentBy = GetValidUser(),
				Text = "What's going on fellas."
			};

			return message;
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

		private Assignment GetValidAssignment()
		{
			var assignment = new Assignment()
			{
				Priority = 10,
				Status = AssignmentStatus.Assigned,
				Name = "Issue64",
				Description = "Answer 42",
			};

			return assignment;
		}

		private TeamWorkRequest GetValidTeamWorkRequest()
		{
			var teamWorkRequest = new TeamWorkRequest()
			{
				Message = "Add me",
				SentBy = GetValidUser(),
				TeamWork = GetValidTeamWork()
			};

			return teamWorkRequest;
		}

		private Resource GetValidResource()
		{
			var resource = new Resource()
			{
				Name = "Solution",
				UploadedBy = GetValidUser(),
				Data = new byte[] { 1, 21, 2, 1, 2 }
			};

			return resource;
		}
	}
}