namespace General
{
    public class MapModel
    {
        public string Site { get; set; }
        public string Route { get; set; }
        public string RoutePrefix { get; set; }
        public string RemapUri { get; set; }
        public string RemapPath { get; set; }

        public MapModel()
        {
        }

        public MapModel(Map map, int version)
        {
            var remap = map.GetVersion(version);
            Site = map.Site;
            Route = map.Route;
            RoutePrefix = map.RoutePrefix;
            RemapUri = remap.RemapUri;
            RemapPath = remap.RemapPath;
        }
    }
}
