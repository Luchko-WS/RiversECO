namespace GeoJSONMigrationTool.Models.River
{
    public class RiverFeatureModel
    {
        public string Type { get; set; }
        public RiverProperties Properties { get; set; }
        public RiverFeatureGeometry Geometry { get; set; }
    }
}