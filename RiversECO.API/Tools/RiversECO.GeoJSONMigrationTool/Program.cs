using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RiversECO.DataContext;
using RiversECO.Models;
using RiversECO.Tools.GeoJSONMigrationTool.DataProcessing;
using RiversECO.Tools.GeoJSONMigrationTool.Helpers;

namespace RiversECO.Tools.GeoJSONMigrationTool
{
    class Program
    {
        // TODO: Move hardcoded connection string to onfiguration.
        const string CONNECTION_STRING = "Server=(localdb)\\MSSQLLocalDB;Database=RiversECO;Trusted_Connection=True;";
        const string RIVERS_DATA = "r";
        const string LAKES_DATA = "l";

        [STAThread]
        static void Main()
        {
            var dataType = PromptDataTypeInput();
            var factory = new DataContextFactory(CONNECTION_STRING);
            var dataProcessor = DataProcessorFactory.GetDataProcessor(factory, dataType);

            var fileName = SelectGeoJsonFile();
            if (string.IsNullOrEmpty(fileName))
            {
                ConsoleLogger.WriteWarning("No file is selected.");
            }

            var fileContent = File.ReadAllText(fileName);
            try
            {
                dataProcessor.ParseFileData(fileContent);
                ConsoleLogger.WriteSuccess("Imported data parsed!");

                if (PromptYesNo("Would you like to sync data with database?"))
                {
                    var rewriteWaterObjects = PromptYesNo("Rewrite existing water objects in the database?");
                    ConsoleLogger.WriteText("Starting data synchronation...");
                    dataProcessor.SyncWithDataBase(rewriteWaterObjects);
                    ConsoleLogger.WriteSuccess("The database has been updated!");
                }

                if (PromptYesNo("Would you like to write result data into JSON file?"))
                {
                    var outputFileData = dataProcessor.GetOutputFileData();

                    ConsoleLogger.WriteText("Enter file name:");
                    var filename = Console.ReadLine().Trim();

                    var settings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        Formatting = Formatting.None
                    };

                    ConsoleLogger.WriteText("Writing geo data into JSON file...");

                    var outputFileContent = JsonConvert.SerializeObject(outputFileData, settings);
                    File.WriteAllText($"{filename}.json", outputFileContent, Encoding.UTF8);
                    ConsoleLogger.WriteSuccess("The JSON file generated.");
                }
            }
            catch (Exception exception)
            {
                ConsoleLogger.WriteError("An unexpected exception occured.", exception);
            }

            ConsoleLogger.WriteSuccess("Done!");
            Console.ReadKey();
        }

        private static string SelectGeoJsonFile()
        {
            ConsoleLogger.WriteText("Selection GeoJSON file...");
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Title = $"Select GeoJSON file";
                fileDialog.Filter = "GeoJSON file|*.geojson";
                fileDialog.Multiselect = false;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = fileDialog.FileName;
                    ConsoleLogger.WriteText($"File {fileName} selected.");
                    return fileName;
                }

                return null;
            }
        }

        private static WaterObjectType PromptDataTypeInput()
        {
            do
            {
                ConsoleLogger.WriteText("What data would you like to import?");
                ConsoleLogger.WriteText("rivers — [r], lakes — [l]");

                var input = Console.ReadLine().ToLower().Trim();
                if (string.Equals(input, RIVERS_DATA, StringComparison.InvariantCultureIgnoreCase))
                {
                    return WaterObjectType.River;
                }
                else if (string.Equals(input, LAKES_DATA, StringComparison.InvariantCultureIgnoreCase))
                {
                    return WaterObjectType.Lake;
                }

                ConsoleLogger.WriteWarning("Unknown data type.");
            }
            while (true);
        }

        private static bool PromptYesNo(string message)
        {
            do
            {
                ConsoleLogger.WriteText(message);
                ConsoleLogger.WriteText("yes — [y], no — [n]");

                var input = Console.ReadLine();
                if (string.Equals(input, "y", StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }

                if (string.Equals(input, "n", StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
            }
            while (true);
        }
    }
}
