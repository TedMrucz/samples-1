using System;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Running...");

			var service = new CallerCentreService();

			var line = service.Hello("Ted").Result;

			var list1 = service.GetUsers().Result;
			var list2 = service.GetMessages().Result;

			Console.WriteLine(line);

			Console.ReadKey();
		}
    }
}