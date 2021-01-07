namespace GeoJSONMigrationTool.DataProcessing
{
    internal interface IDataProcessor
    {
        void ParseFileData(string json);
        object GetOutputFileData();
        void SyncWithDataBase(bool rewriteWaterObjects);
    }
}
