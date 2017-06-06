using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;

namespace DataProvider.Host
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.Map("/api", api =>
			{
				var config = new HttpConfiguration();
				config.MapHttpAttributeRoutes();
				config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
				config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
				config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
				api.UseWebApi(config);
			});
		}
	}
}
