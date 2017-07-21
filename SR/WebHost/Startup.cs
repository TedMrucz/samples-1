using CallCentre.Data;
using CallCentre.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebHost.SignalR;

namespace WebHost
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddEntityFrameworkSqlServer()
				.AddDbContext<DataContext>((serviceProvider, options) => options.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=CallCentre;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
					.UseInternalServiceProvider(serviceProvider)); 

			services.AddScoped<ITicker, Ticker>();
			services.AddScoped<ICalculator, Calculator>();

			services.AddSignalR(options =>
			{
				options.Hubs.EnableDetailedErrors = true;
			});

			services.AddIdentity<User, IdentityRole<int>>()
			   .AddEntityFrameworkStores<DataContext, int>()
			   .AddDefaultTokenProviders();

			services.AddAuthorization();
			services.AddMvc(options =>
			{
				options.Filters.Add(new RequireHttpsAttribute());
			});
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseIdentity();
			app.UseWebSockets();
			app.UseSignalR();
			app.UseMvc(routes => routes.MapRoute(name: "default", template: "api/{controller}/{action}"));
		}
	}
}
