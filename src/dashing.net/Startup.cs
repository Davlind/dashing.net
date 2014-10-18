using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(dashing.net.Startup))]

namespace dashing.net
{
	using Microsoft.AspNet.SignalR;

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var hubConfiguration =
				new HubConfiguration
				{
					EnableDetailedErrors = true
				};

			app.MapSignalR(hubConfiguration);
		}
	}
}
