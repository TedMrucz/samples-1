using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
	public class CallerCentreService
    {
		private readonly IServiceInvoker service;

		public CallerCentreService()
		{
			this.service = new ServiceInvoker("http://localhost:8080/api/CallerCentre/");
		}

		public Task<string> Hello(string name) => this.service.Get<string>(new { name = name });

		public Task<IList<User>> GetUsers() => this.service.Get<IList<User>>();
		public Task<IList<Message>> GetMessages() => this.service.Get<IList<Message>>();

	}
}
