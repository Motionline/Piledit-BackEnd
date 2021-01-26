namespace PileditBackendServer.Models
{
    public class ComponentBlockBase
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string TopUuid { get; set; }
        public string ParentUuid { get; set; }
    }

    public class MovieLoadingBlock : ComponentBlockBase
    {
        public string MaterialPath { get; set; }
    }
}
