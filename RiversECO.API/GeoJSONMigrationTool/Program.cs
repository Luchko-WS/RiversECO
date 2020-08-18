using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;
using RiversECO.DataContext;
using RiversECO.Models;
using GeoJSONMigrationTool.Extensions;
using GeoJSONMigrationTool.Models;
using GeoJSONMigrationTool.Models.Lake;
using GeoJSONMigrationTool.Models.River;

namespace GeoJSONMigrationTool
{
    class Program
    {
        // TODO: Move hardcoded onnection string to onfiguration.
        const string CONNECTION_STRING = "Server=(localdb)\\MSSQLLocalDB;Database=RiversECO;Trusted_Connection=True;";

        [STAThread]
        static void Main()
        {
            Console.WriteLine("What data would you like to import (rivers, lakes)?");
            var dataType = Console.ReadLine().ToLower().Trim();
            if (dataType != "rivers" && dataType != "lakes")
            {
                Console.WriteLine("Unknown data type. Exiting from application...");
                return;
            }

            Console.WriteLine("Selection GeoJSON file...");
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Title = $"Select GeoJSON file which contains {dataType} data";
                fileDialog.Filter = "GeoJSON file|*.geojson";
                fileDialog.Multiselect = false;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    Console.WriteLine("Processing imported data...");
                    var fileContent = File.ReadAllText(fileDialog.FileName);

                    if (dataType == "rivers")
                    {
                        var geoJsonObject = JsonConvert
                            .DeserializeObject<GeoJSONFileModel<RiverFeatureModel>>(fileContent);
                        var rivers = geoJsonObject.MapToRivers();
                        SeedDatabase(rivers);
                    }
                    else
                    {
                        var geoJsonObject = JsonConvert
                            .DeserializeObject<GeoJSONFileModel<LakeFeatureModel>>(fileContent);
                        var lakes = geoJsonObject.MapToLakes();
                        SeedDatabase(lakes);
                    }
                }
            }

            Console.WriteLine("Done!");
        }

        private static void SeedDatabase<T>(List<T> waterObjects) where T: WaterObject
        {
            IDesignTimeDbContextFactory<DataContext> factory = new DataContextFactory(CONNECTION_STRING);
            var context = factory.CreateDbContext(new string[0]);

            Console.WriteLine("Updating database...");
            foreach (var waterObject in waterObjects)
            {
                try
                {
                    context.Add(waterObject);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occured while processing the feature {waterObject.Name}. Exception: {ex}");
                }
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured while updating DB. Exception: {ex}");
            }
        }
    }
}
