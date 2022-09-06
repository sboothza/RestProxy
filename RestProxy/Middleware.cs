using System;
using System.IO.Pipelines;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using General;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

namespace RestProxy
{
	public class Middleware
	{
		private readonly Maps _maps;

		public Middleware(IConfiguration configuration, RequestDelegate next)
		{
			var connectionString = configuration.GetConnectionString("Cache");
			_maps = new Maps(connectionString);
		}

		public async Task Invoke(HttpContext context)
		{
			var route = context.Request.Path.ToString();

			int version = 0;
			if (context.Request.Headers.ContainsKey("version"))
				version = Convert.ToInt32(context.Request.Headers["version"]);

			var currentMap = _maps.GetMapForPrefix(route, version);

			if (currentMap != null)
			{
				var client = new HttpClient
				{
					BaseAddress = new Uri(currentMap.RemapUri)
				};
				var method = context.Request.Method.StringToMethod();

				var message = new HttpRequestMessage(method, route.Replace(currentMap.RoutePrefix, currentMap.RemapPath))
				{
					Content = new StreamContent(context.Request.Body)
				};

				message.Content.Headers.ContentType = new MediaTypeHeaderValue(context.Request.ContentType ?? "application/json");
				var response = await client.SendAsync(message);
				context.Response.StatusCode = (int)response.StatusCode;
				context.Response.ContentType = response.Content.Headers.ContentType.MediaType;

				await StreamPipeExtensions.CopyToAsync(await response.Content.ReadAsStreamAsync(), context.Response.BodyWriter);
			}
			else
			{
				context.Response.StatusCode = 200;
				await context.Response.WriteAsync("nothing");
			}
		}
	}
}
