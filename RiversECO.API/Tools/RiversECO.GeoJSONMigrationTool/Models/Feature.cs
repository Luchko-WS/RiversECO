namespace RiversECO.Tools.GeoJSONMigrationTool.Models
{
    public class Feature<TPropertiesModel, TGeometryModel>
    {
        public string Type { get; set; }
        public TPropertiesModel Properties { get; set; }
        public TGeometryModel Geometry { get; set; }
    }
}