using System;

namespace GeoJSONMigrationTool.Models.River
{
    public class RiverProperties
    {
        public Guid? WaterObjectId { get; set; }
        public string Name_ukr { get; set; }
        public string Code { get; set; }
        public double L_km { get; set; }
    }
}