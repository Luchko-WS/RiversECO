using System;
using System.Text;
using RiversECO.Models;
using GeoJSONMigrationTool.Helpers;
using GeoJSONMigrationTool.Models;

namespace GeoJSONMigrationTool.Extensions
{
    public static class ConvertsionExt
    {
        public static WaterObject MapToWaterObject(this RiverFeatureProperties feature)
        {
            try
            {
                var waterObject = new WaterObject()
                {
                    Id = Guid.NewGuid(),
                    Name = feature.NameUkr,
                    CodeSwb = feature.CodeSwb,
                    CreatedOn = DateTime.UtcNow,
                    Category = feature.Category,
                    TypeName = feature.TypeName,
                    TypeCode = feature.TypeCode,
                    Note = feature.NoteUkr,
                    Description = GetRiverDescription(feature),
                    Type = WaterObjectType.River
                };

                return waterObject;
            }
            catch (Exception exception)
            {
                ConsoleLogger.WriteError($"Exception occured while mapping the river feature {feature}.", exception);
                throw exception;
            }
        }

        private static string GetRiverDescription(RiverFeatureProperties riverProperties)
        {
            try
            {
                var descriptionBuilder = new StringBuilder();

                if (riverProperties.LengthKm.HasValue)
                {
                    descriptionBuilder.Append($"Довжина (км): {riverProperties.LengthKm}.");
                }

                if (!string.IsNullOrEmpty(riverProperties.FlowTo))
                {
                    descriptionBuilder.Append($" Впадає в: { riverProperties.FlowTo}.");
                }

                return descriptionBuilder.ToString();
            }
            catch (Exception exception)
            {
                ConsoleLogger.WriteWarning($"Could not generate a description for river {riverProperties}.", exception);
                return string.Empty;
            }
        }

        public static WaterObject MapToWaterObject(this LakeFeatureProperties feature)
        {
            try
            {
                var waterObject = new WaterObject()
                {
                    Id = Guid.NewGuid(),
                    Name = feature.NameUkr,
                    CodeSwb = feature.CodeSwb,
                    CreatedOn = DateTime.UtcNow,
                    Category = feature.Category,
                    TypeName = feature.TypeName,
                    TypeCode = feature.TypeCode,
                    Description = GetLakeDescription(feature),
                    Type = WaterObjectType.Lake
                };

                return waterObject;
            }
            catch (Exception exception)
            {
                ConsoleLogger.WriteError($"Exception occured while mapping the lake feature {feature}.", exception);
                throw exception;
            }
        }

        private static string GetLakeDescription(LakeFeatureProperties lakeProperties)
        {
            try
            {
                var descriptionBuilder = new StringBuilder();

                if (lakeProperties.Area.HasValue)
                {
                    descriptionBuilder.Append($"Площа: {lakeProperties.Area}.");
                }

                return descriptionBuilder.ToString();
            }
            catch (Exception exception)
            {
                ConsoleLogger.WriteWarning($"Could not generate a description for lake {lakeProperties}.", exception);
                return string.Empty;
            }
        }

        public static Feature<OutputFeatureProperties, RiverFeatureGeometry> MapToOutputFeature(
            this Feature<RiverFeatureProperties, RiverFeatureGeometry> riverFeature)
        {
            var newFeature = new Feature<OutputFeatureProperties, RiverFeatureGeometry>
            {
                Type = riverFeature.Type,
                Geometry = riverFeature.Geometry,
                Properties = new OutputFeatureProperties
                {
                    NameUkr = riverFeature.Properties.NameUkr,
                    WaterObjectId = riverFeature.Properties.DbId
                }
            };

            return newFeature;
        }

        public static Feature<OutputFeatureProperties, LakeFeatureGeometry> MapToOutputFeature(
            this Feature<LakeFeatureProperties, LakeFeatureGeometry> lakeFeature)
        {
            var newFeature = new Feature<OutputFeatureProperties, LakeFeatureGeometry>
            {
                Type = lakeFeature.Type,
                Geometry = lakeFeature.Geometry,
                Properties = new OutputFeatureProperties
                {
                    NameUkr = lakeFeature.Properties.NameUkr,
                    WaterObjectId = lakeFeature.Properties.DbId
                }
            };

            return newFeature;
        }
    }
}
