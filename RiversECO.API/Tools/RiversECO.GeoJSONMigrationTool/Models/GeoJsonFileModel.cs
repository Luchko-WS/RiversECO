namespace RiversECO.Tools.GeoJSONMigrationTool.Models
{
    public class GeoJsonFileModel<TPropertiesModel, TGeometryModel>
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public Crs Crs { get; set; }
        public Feature<TPropertiesModel, TGeometryModel>[] Features { get; set; }
    }
}
