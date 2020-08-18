using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RiversECO.Models;
using GeoJSONMigrationTool.Models;
using GeoJSONMigrationTool.Models.River;
using GeoJSONMigrationTool.Models.Lake;

namespace GeoJSONMigrationTool.Extensions
{
    public static class GeoJSONModelConvertExt
    {
        public static List<River> MapToRivers(this GeoJSONFileModel<RiverFeatureModel> geoJsonObject)
        {
            var resultList = new List<River>();
            foreach (var feature in geoJsonObject.Features)
            {
                try
                {
                    var waterObject = new River()
                    {
                        Id = Guid.NewGuid(),
                        Name = feature.Properties.Name_ukr,
                        CreatedOn = DateTime.UtcNow,
                        Code = feature.Properties.Code,
                        LengthKm = feature.Properties.L_km,
                        Geometry = JsonConvert.SerializeObject(feature.Geometry.Coordinates)
                    };

                    resultList.Add(waterObject);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occured while mapping the feature {feature.Properties.Name_ukr}. Exception: {ex}");
                }
            }

            return resultList;
        }

        public static List<Lake> MapToLakes(this GeoJSONFileModel<LakeFeatureModel> geoJsonObject)
        {
            var resultList = new List<Lake>();
            foreach (var feature in geoJsonObject.Features)
            {
                try
                {
                    var waterObject = new Lake()
                    {
                        Id = Guid.NewGuid(),
                        Name = feature.Properties.Name_ukr,
                        CreatedOn = DateTime.UtcNow,
                        Code = feature.Properties.Code,
                        Area = feature.Properties.Area,
                        Geometry = JsonConvert.SerializeObject(feature.Geometry.Coordinates)
                    };

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
