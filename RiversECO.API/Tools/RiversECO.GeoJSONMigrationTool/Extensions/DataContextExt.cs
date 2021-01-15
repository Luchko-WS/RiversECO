using System;
using System.Linq;
using RiversECO.Models;

namespace RiversECO.Tools.GeoJSONMigrationTool.Extensions
{
    public static class DataContextExt
    {
        public static Guid AddOrUpdateWaterObject(
            this RiversECO.DataContext.DataContext dataContext,
            WaterObject waterObject,
            bool rewriteWaterObjects)
        {
            var existingWaterObject = dataContext.WaterObjects
                .SingleOrDefault(x => waterObject.CodeSwb == x.CodeSwb);

            if (existingWaterObject != null)
            {
                waterObject.Id = existingWaterObject.Id;
                if (rewriteWaterObjects)
                {
                    existingWaterObject.Category = waterObject.Category;
                    existingWaterObject.Description = waterObject.Description;
                    existingWaterObject.ModifiedOn = DateTime.UtcNow;
                    existingWaterObject.Note = waterObject.Note;
                    existingWaterObject.Type = waterObject.Type;
                    existingWaterObject.TypeCode = waterObject.TypeCode;
                    existingWaterObject.TypeName = waterObject.TypeName;
                }
            }
            else
            {
                dataContext.Add(waterObject);
            }

            return waterObject.Id;
        }
    }
}
