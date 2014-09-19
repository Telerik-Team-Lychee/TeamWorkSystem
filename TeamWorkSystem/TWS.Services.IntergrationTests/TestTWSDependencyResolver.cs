namespace TWS.Services.IntegrationTests
{
	using System;
	using System.Collections.Generic;
	using System.Web.Http.Dependencies;

	using Telerik.JustMock;
	using TWS.Data;
	using TWS.Data.Repositories;
	using TWS.RestApi.Controllers;
	using TWS.RestApi.Infrastructure;

	public class TestTWSDependencyResolver<T> : IDependencyResolver where T : class
	{
		private IRepository<T> repository;

		public IRepository<T> Repository
		{
			get
			{
				return this.repository;
			}
			set
			{
				this.repository = value;
			}
		}

		public IDependencyScope BeginScope()
		{
			return this;
		}

		public object GetService(Type serviceType)
		{
			var repository = Mock.Create<ITwsData>();
			var userProvider = Mock.Create<IUserIdProvider>();

			if (serviceType == typeof(TeamWorkController))
			{
				return new TeamWorkController(repository, userProvider);
			}
			else if (serviceType == typeof(AssignmentController))
			{
				return new AssignmentController(repository);
			}
			else if (serviceType == typeof(RequestController))
			{
				return new RequestController(repository, userProvider);
			}
			else if (serviceType == typeof(MessageController))
			{
				return new MessageController(repository, userProvider);
			}

			return null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return new List<object>();
		}

		public void Dispose()
		{

		}
	}
}
