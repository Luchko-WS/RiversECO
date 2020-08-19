using System;

namespace GeoJSONMigrationTool.Models.Lake
{
    public class LakeProperties
    {
        public Guid? WaterObjectId { get; set; }
        public string Name_ukr { get; set; }
        public string Code { get; set; }
        public double Area { get; set; }
    }
}
