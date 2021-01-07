using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Design;
using RiversECO.DataContext;
using GeoJSONMigrationTool.Extensions;
using GeoJSONMigrationTool.Models;
using System.Collections.Generic;

namespace GeoJSONMigrationTool.DataProcessing
{
    internal class RiversDataProcessor : IDataProcessor
    {
        private DataContext _dataContext;
        private GeoJsonFileModel<RiverFeatureProperties, RiverFeatureGeometry> _fileModel;

        public RiversDataProcessor(IDesignTimeDbContextFactory<DataContext> contextFactory)
        {
            _dataContext = contextFactory.CreateDbContext(new string[0]);
        }

        public void ParseFileData(string json)
        {
            _fileModel = JsonConvert
                    .DeserializeObject<GeoJsonFileModel<RiverFeatureProperties, RiverFeatureGeometry>>(json);
        }

        public void SyncWithDataBase(bool rewriteWaterObjects)
        {
            var features = _fileModel.Features;
            foreach (var feature in features)
            {
                var waterObject = feature.Properties.MapToWaterObject();
                var waterObjectId = _dataContext.AddOrUpdateWaterObject(waterObject, rewriteWaterObjects);
                feature.Properties.DbId = waterObjectId;
                _dataContext.SaveChanges();
            }

            _dataContext.SaveChanges();
        }

        public object GetOutputFileData()
        {
            var newFileModel = new GeoJsonFileModel<OutputFeatureProperties, RiverFeatureGeometry>();
            newFileModel.Crs = _fileModel.Crs;
            newFileModel.Name = _fileModel.Name;
            newFileModel.Type = _fileModel.Type;

            var newFeatures = new List<Feature<OutputFeatureProperties, RiverFeatureGeometry>>();
            foreach (var feature in _fileModel.Features)
            {
                var newFeature = feature.MapToOutputFeature();
                newFeatures.Add(newFeature);
            }
            
            newFileModel.Features = newFeatures.ToArray();
            return newFileModel;
        }
    }
}
