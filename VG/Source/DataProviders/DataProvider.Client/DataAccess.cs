using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;

namespace DataProvider.Client
{
    public class DataAccess
    {
		public DataAccess()
		{ }
		const string svcBaseAddress = "http://localhost:5000/api/ef/";
		protected async Task<R> Get<R>(object[] parameters, string documentType, string action)
		{
			var path = new StringBuilder(action);

			foreach (var param in parameters)
			{
				path.Append('/');
				if (param is DateTime)
					path.AppendFormat("{0:dd-MMM-yyyy}", param);
				else
					path.Append(param);
			}

			using (var client = new HttpClient())
			{
				using (var response = await client.GetAsync(svcBaseAddress + path))
				{
					response.EnsureSuccessStatusCode();
					string result = await response.Content.ReadAsStringAsync();

					return JsonConvert.DeserializeObject<R>(result);
				}
			}
		}

		protected Task<R> Get<R>(string documentType, [CallerMemberName] string action = null) => Get<R>(new object[0], documentType, action);

		protected Task<R> Get<R, T1>(T1 param1, string documentType, [CallerMemberName] string action = null) => Get<R>(new object[] { param1 }, documentType, action);

		protected Task<R> Get<R, T1, T2>(T1 param1, T2 param2, string documentType, [CallerMemberName] string action = null) => Get<R>(new object[] { param1, param2 }, documentType, action);

		protected Task<R> Get<R, T1, T2, T3>(T1 param1, T2 param2, T3 param3, string documentType, [CallerMemberName] string action = null) => Get<R>(new object[] { param1, param2, param3 }, documentType, action);

		protected Task<R> Get<R, T1, T2, T3, T4>(T1 param1, T2 param2, T3 param3, T4 param4, string documentType, [CallerMemberName] string action = null) => Get<R>(new object[] { param1, param2, param3, param4 }, documentType, action);


		protected async Task<int> Post<T>(T @object, string documentType, string parameters = null, [CallerMemberName] string action = null)
		{
			var path = action;
			if (!String.IsNullOrEmpty(parameters))
				path += String.Concat("/", parameters);

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var content = JsonConvert.SerializeObject(@object);

				using (var result = await client.PostAsJsonAsync<string>(svcBaseAddress + path, @content))
				{
					result.EnsureSuccessStatusCode();
					return 0;
				}
			}
		}
	}
}
