using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Contracts;

using RestSharp;

namespace Driver
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Console.ReadLine();
			var items = Call().Result;
			items.ForEach(Console.WriteLine);
		}

		public static async Task<List<WeatherForecast>> Call()
		{
			var client = new RestClient("http://localhost:9000");
			var request = new RestRequest("weatherforecast");
			var response = await client.GetAsync<List<WeatherForecast>>(request);
			return response;
		}
	}
}
