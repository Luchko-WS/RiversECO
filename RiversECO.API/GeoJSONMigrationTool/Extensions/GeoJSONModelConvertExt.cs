using System;
using System.Collections.Generic;
using RiversECO.Models;
using GeoJSONMigrationTool.Models;
using GeoJSONMigrationTool.Models.River;
using GeoJSONMigrationTool.Models.Lake;

namespace GeoJSONMigrationTool.Extensions
{
    public static class GeoJSONModelConvertExt
    {
        public static List<WaterObject> MapToWaterObjects(this GeoJSONFileModel<RiverFeatureModel> geoJsonObject)
        {
            var resultList = new List<WaterObject>();
            foreach (var feature in geoJsonObject.Features)
            {
                try
                {
                    var waterObject = new WaterObject()
                    {
                        Id = feature.Properties.WaterObjectId ?? Guid.NewGuid(),
                        Name = feature.Properties.Name_ukr,
                        CreatedOn = DateTime.UtcNow,
                        Code = feature.Properties.Code,
                        Type = WaterObjectType.River
                    };

                    feature.Properties.WaterObjectId = waterObject.Id;
                    resultList.Add(waterObject);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occured while mapping the feature {feature.Properties.Name_ukr}. Exception: {ex}");
                }
            }

            return resultList;
        }

        public static List<WaterObject> MapToWaterObjects(this GeoJSONFileModel<LakeFeatureModel> geoJsonObject)
        {
            var resultList = new List<WaterObject>();
            foreach (var feature in geoJsonObject.Features)
            {
                try
                {
                    var waterObject = new WaterObject()
                    {
                        Id = feature.Properties.WaterObjectId ?? Guid.NewGuid(),
                        Name = feature.Properties.Name_ukr,
                        CreatedOn = DateTime.UtcNow,
                        Code = feature.Properties.Code,
                        Type = WaterObjectType.Lake
                    };

                    feature.Properties.WaterObjectId = waterObject.Id;
                    resultList.Add(waterObject);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occured while mapping the feature {feature.Properties.Name_ukr}. Exception: {ex}");
                }
            }

            return resultList;
        }
    }
}
