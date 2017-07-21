
using System.Collections.Generic;

namespace WebHost.WebAPI
{
	public interface IHelloRepository
	{
		IEnumerable<Hello> Get();
		Hello Get(int id);
	}
}