using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace General
{
    public class Map
    {
        private Regex _regex;

        public ObjectId Id { get; set; }
        public string Site { get; set; }
        public string Route { get; set; }
        public string RoutePrefix { get; set; }

        public Regex Regex
        {
            get => _regex ??= new Regex(Route);
            set => _regex = value;
        }

        public List<Remap> Remaps { get; set; }
        public Map()
        {
            Remaps = new List<Remap>();
        }

        public Map(IConfigurationSection configurationSection)
        {
            Site = configurationSection["site"];
            Route = configurationSection["route"];
            RoutePrefix = configurationSection["routePrefix"];
            Regex = new Regex(configurationSection["route"]);

            var remapSection = configurationSection.GetSection("remaps");

            var tempRemaps = remapSection.GetChildren()
                .Select(remap => new Remap(remap))
                .ToList();
            tempRemaps.Sort();
            Remaps = new List<Remap>(tempRemaps);
        }

        public Remap GetVersion(int version)
        {
            var map = Remaps.FirstOrDefault(m => m.Version == version);
            return map ?? Remaps[0];
        }
    }


}
