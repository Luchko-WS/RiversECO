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
using System.Text;
using Newtonsoft.Json.Serialization;

namespace GeoJSONMigrationTool
{
    class Program
    {
        // TODO: Move hardcoded onnection string to onfiguration.
        const string CONNECTION_STRING = "Server=(localdb)\\MSSQLLocalDB;Database=RiversECO;Trusted_Connection=True;";
        const string RIVERS_DATA = "r";
        const string LAKES_DATA = "l";

        [STAThread]
        static void Main()
        {
            Console.WriteLine("What data would you like to import (rivers [r], lakes [l])?");
            var dataType = Console.ReadLine().ToLower().Trim();
            if (dataType != RIVERS_DATA && dataType != LAKES_DATA)
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

                    if (dataType == RIVERS_DATA)
                    {
                        var geoJsonObject = JsonConvert
                            .DeserializeObject<GeoJSONFileModel<RiverFeatureModel>>(fileContent);
                        var rivers = geoJsonObject.MapToWaterObjects();
                        SeedDatabase(rivers);
                        WriteJsonFile(geoJsonObject);
                    }
                    else
                    {
                        var geoJsonObject = JsonConvert
                            .DeserializeObject<GeoJSONFileModel<LakeFeatureModel>>(fileContent);
                        var lakes = geoJsonObject.MapToWaterObjects();
                        SeedDatabase(lakes);
                        WriteJsonFile(geoJsonObject);
                    }
                }
            }

            Console.WriteLine("Done!");
        }

        private static void SeedDatabase(List<WaterObject> waterObjects)
        {
            if (PromptYesNo("Would you like to update database?"))
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

                Console.WriteLine("Database has been updated");
            }
        }

        private static void WriteJsonFile(object data)
        {
            if (PromptYesNo("Would you like to write result data into JSON file?"))
            {
                Console.WriteLine("Enter file name:");
                var filename = Console.ReadLine().Trim();

                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.None
                };

                Console.WriteLine("Writing geo data into JSON file...");
                var fileContent = JsonConvert.SerializeObject(data, settings);
                File.WriteAllText($"{filename}.json", fileContent, Encoding.UTF8);
                Console.WriteLine("JSON file generated.");
            }
        }

        private static bool PromptYesNo(string message)
        {
            Console.WriteLine($"{message} (y/n)");
            return Console.ReadLine().ToLower() == "y";
        }
    }
}
