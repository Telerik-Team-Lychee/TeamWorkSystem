namespace TWS.Services.Tests
{
	using System;

	using TWS.Models;
	using TWS.Models.Enumerations;

	public static class Entities
	{
		public static Message GetValidMessage()
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

		public static User GetValidUser()
		{
			var user = new User()
			{
				FirstName = "Pesho",
				LastName = "Peshev",
				IsOnline = false
			};

			return user;
		}

		public static TeamWork GetValidTeamWork()
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

		public static Assignment GetValidAssignment()
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

		public static TeamWorkRequest GetValidTeamWorkRequest()
		{
			var teamWorkRequest = new TeamWorkRequest()
			{
				Message = "Add me",
				SentBy = GetValidUser(),
				TeamWork = GetValidTeamWork()
			};

			return teamWorkRequest;
		}

		public static Resource GetValidResource()
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