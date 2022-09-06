using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace General
{
    public class Maps
    {
        private readonly List<Map> _mappings;

        public Maps(IConfiguration configuration)
        {
            _mappings = new List<Map>();
            var mappings = configuration.GetSection("Mappings");
            mappings.GetChildren()
                .ToList()
                .ForEach(map => _mappings.Add(new Map(map)));
        }

        public Maps(string connectionString)
        {
            var url = MongoUrl.Create(connectionString);
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(url.DatabaseName);
            var collection = db.GetCollection<Map>("Mappings");
            _mappings = collection.Find(_ => true).ToList();
        }

        public MapModel GetMapForPrefix(string route, int version = 0)
        {
            foreach (var map in _mappings)
            {
                if (route.StartsWith(map.RoutePrefix))
                {
                    var model = new MapModel(map, version);
                    return model;
                }
            }

            return null;
        }

        public MapModel GetMapForRegex(string route, int version = 0)
        {
            foreach (var map in _mappings)
            {
                if (map.Regex.IsMatch(route))
                {
                    Console.WriteLine($"mapped to {map.Site}");
                    var model = new MapModel(map, version);
                    return model;
                }
            }

            return null;
        }
    }
}
