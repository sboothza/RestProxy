using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace General
{
	public static class Extensions
	{
		public static HttpMethod StringToMethod(this string method) =>
			method.ToLower() switch
			{
				"get" => HttpMethod.Get,
				"put" => HttpMethod.Put,
				"post" => HttpMethod.Post,
				"delete" => HttpMethod.Delete,
				_ => HttpMethod.Get,
			};

		public static IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetCollection(
			this HttpResponseHeaders headers)
		{
			var headerList = headers.AsEnumerable();
			return headerList;
		}
	}
}
