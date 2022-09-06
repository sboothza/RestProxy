using System;
using Microsoft.Extensions.Configuration;

namespace General
{
    public class Remap : IComparable
    {
        public int Version { get; set; }
        public string RemapUri { get; set; }
        public string RemapPath { get; set; }

        public Remap()
        {
        }

        public Remap(IConfigurationSection configurationSection)
        {
            Version = Convert.ToInt32(configurationSection["version"]);
            RemapUri = configurationSection["remapUri"];
            RemapPath = configurationSection["remapPath"];
        }

        public int CompareTo(object obj)
        {
            if (obj is Remap remap)
                return Version.CompareTo(remap.Version);
            throw new ArgumentException("obj is not remap");
        }
    }
}
