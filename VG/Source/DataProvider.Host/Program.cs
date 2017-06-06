using System;
using Microsoft.Owin.Hosting;

namespace DataProvider.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			using (WebApp.Start<Startup>("http://localhost:5000"))
			{
				Console.WriteLine("Self-host running... press any key to exit.");
				Console.ReadLine();
			}
		}
	}
}
