﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;
using RiversECO.Tools.GeoJSONMigrationTool.Models;
using RiversECO.Tools.GeoJSONMigrationTool.Extensions;

namespace RiversECO.Tools.GeoJSONMigrationTool.DataProcessing
{
    internal class LakesDataProcessor : IDataProcessor
    {
        private RiversECO.DataContext.DataContext _dataContext;
        private GeoJsonFileModel<LakeFeatureProperties, LakeFeatureGeometry> _fileModel;

        public LakesDataProcessor(IDesignTimeDbContextFactory<RiversECO.DataContext.DataContext> contextFactory)
        {
            _dataContext = contextFactory.CreateDbContext(new string[0]);
        }

        public void ParseFileData(string json)
        {
            _fileModel = JsonConvert
                    .DeserializeObject<GeoJsonFileModel<LakeFeatureProperties, LakeFeatureGeometry>>(json);
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
            var newFileModel = new GeoJsonFileModel<OutputFeatureProperties, LakeFeatureGeometry>();
            newFileModel.Crs = _fileModel.Crs;
            newFileModel.Name = _fileModel.Name;
            newFileModel.Type = _fileModel.Type;

            var newFeatures = new List<Feature<OutputFeatureProperties, LakeFeatureGeometry>>();
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
