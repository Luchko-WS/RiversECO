using Microsoft.EntityFrameworkCore.Design;
using RiversECO.Models;

namespace RiversECO.Tools.GeoJSONMigrationTool.DataProcessing
{
    internal static class DataProcessorFactory
    {
        public static IDataProcessor GetDataProcessor(
            IDesignTimeDbContextFactory<RiversECO.DataContext.DataContext> contextFactory,
            WaterObjectType objectType)
        {
            switch (objectType)
            {
                case WaterObjectType.River:
                    return new RiversDataProcessor(contextFactory);
                case WaterObjectType.Lake:
                    return new LakesDataProcessor(contextFactory);
                default:
                    return null;
            }
        }
    }
}
