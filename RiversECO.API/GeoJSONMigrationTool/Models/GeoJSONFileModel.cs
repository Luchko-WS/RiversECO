namespace GeoJSONMigrationTool.Models
{
    public class GeoJSONFileModel<TFeatureModel>
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public TFeatureModel[] Features { get; set; }
    }
}
