namespace GeoJSONMigrationTool.Models.Lake
{
    public class LakeFeatureModel
    {
        public string Type { get; set; }
        public LakeProperties Properties { get; set; }
        public LakeFeatureGeometry Geometry { get; set; }
    }
}