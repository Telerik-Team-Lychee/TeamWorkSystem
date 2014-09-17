using System.Reflection;
using System.Web.Http;

using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using TWS.Data;
using TWS.RestApi.Infrastructure;

[assembly: OwinStartup(typeof(TWS.RestApi.Startup))]

namespace TWS.RestApi
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
			app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
		}
		private static StandardKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			kernel.Load(Assembly.GetExecutingAssembly());
			RegisterMappings(kernel);

			return kernel;
		}

		private static void RegisterMappings(StandardKernel kernal)
		{
			kernal.Bind<ITwsData>().To<TwsData>()
				.WithConstructorArgument("context", context => new TwsDbContext());

			kernal.Bind<IUserIdProvider>().To<AspNetUserIdProvider>();
		}
	}
}