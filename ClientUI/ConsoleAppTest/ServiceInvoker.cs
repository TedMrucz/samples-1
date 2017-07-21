using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
	public class ServiceInvoker : IServiceInvoker
	{
		private readonly Uri rootUri;

		public ServiceInvoker(string rootUri = @"http://localhost:8080/api/")
		{
			this.rootUri = new Uri(rootUri);
		}

		public async Task<T> Get<T>(object parameters = null, [CallerMemberName]string action = null)
		{
			var path = action;

			if (parameters != null)
			{
				var properties = parameters.GetType().GetProperties();
				var pairs = properties.Select(p => new { Name = p.Name, Value = p.GetValue(parameters) }).Select(p => String.Concat(p.Name, "=", p.Value));
				path += "?" + String.Join("&", pairs);
			}

			using (var client = new HttpClient())
			using (var result = await client.GetAsync(new Uri(this.rootUri, path)))
			{
				result.EnsureSuccessStatusCode();
				return await result.Content.ReadAsAsync<T>();
			}
		}

		public async Task<int> Post<T>(T @object, object parameters = null, [CallerMemberName] string action = null)
		{
			var path = action;

			if (parameters != null)
			{
				var properties = parameters.GetType().GetProperties();
				var pairs = properties.Select(p => new { Name = p.Name, Value = p.GetValue(parameters) }).Select(p => String.Concat(p.Name, "=", p.Value));
				path += "?" + String.Join("&", pairs);
			}

			using (var client = new HttpClient())
			using (var result = await client.PostAsJsonAsync(new Uri(this.rootUri, path), @object))
			{
				result.EnsureSuccessStatusCode();
				return await result.Content.ReadAsAsync<int>();
			}
		}
	}
}
