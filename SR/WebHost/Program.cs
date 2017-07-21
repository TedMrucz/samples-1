using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace WebHost
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Running...");

			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseUrls(@"http://*:8080")
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}