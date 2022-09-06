using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class TesterController : ControllerBase
	{
		public class TesterEntity
		{
			public string Value1 { get; set; }
			public int Value2 { get; set; }

			public TesterEntity()
			{
			}

			public TesterEntity(string v1, int v2)
			{
				Value1 = v1;
				Value2 = v2;
			}

			public override string ToString()
			{
				return $"Tester:V1:{Value1} V2:{Value2}";
			}
		}

		[HttpGet]
		public IEnumerable<TesterEntity> Get()
		{
			return new[]
			{
				new TesterEntity("value1", 1), 
				new TesterEntity("value2", 2) 
			};
		}


		[HttpGet("{id}")]
		public TesterEntity Get(int id)
		{
			return new TesterEntity("value1", id);
		}

		// POST api/<TesterController>
		[HttpPost]
		public string Post([FromBody] string value)
		{
			return $"post {value}";
		}

		// PUT api/<TesterController>/5
		[HttpPut("{id}")]
		public string Put(int id, [FromBody] string value)
		{
			return $"Put {id} {value}";
		}

		// DELETE api/<TesterController>/5
		[HttpDelete("{id}")]
		public string Delete(int id)
		{
			return $"delete {id}";
		}
	}
}
