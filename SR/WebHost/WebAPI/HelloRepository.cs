
using System.Collections.Generic;
using System.Linq;

namespace WebHost.WebAPI
{
	public class HelloRepository : IHelloRepository
	{
		static List<Hello> hellos = new List<Hello> {
			new Hello { Id = 1, Text = "Hi", Name = "Ted" },
			new Hello { Id = 2, Text = "Hello", Name = "Administrator" }
		};

		public IEnumerable<Hello> Get()
		{
			return hellos;
		}

		public Hello Get(int id)
		{
			return (hellos.FirstOrDefault(d => d.Id == id));
		}
	}
}