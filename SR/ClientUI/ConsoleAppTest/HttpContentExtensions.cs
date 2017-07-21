using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
	internal static class HttpContentExtensions
	{
		public static async Task<T> ReadAsAsync<T>(this HttpContent content)
		{
			if (typeof(T) == typeof(string))
			{
				var result = await content.ReadAsStringAsync();
				return (T)(object)result;
			}

			using (var stream = await content.ReadAsStreamAsync())
			using (var reader = new StreamReader(stream))
			using (var jsonReader = new JsonTextReader(reader))
			{
				var serializer = new JsonSerializer();
				return serializer.Deserialize<T>(jsonReader);
			}
		}

		public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, Uri uri, object @object)
		{
			using (var stream = new MemoryStream())
			using (var writer = new StreamWriter(stream))
			using (var jsonWriter = new JsonTextWriter(writer))
			using (var content = new StreamContent(stream))
			{
				var serializer = new JsonSerializer();
				serializer.Serialize(jsonWriter, @object);
				return await client.PostAsync(uri, content);
			}
		}
	}
}
