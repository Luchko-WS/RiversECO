using System.Threading.Tasks;

namespace RiversECO.Plugins.WebPageParser
{
    public interface IPlainTextParser
    {
        string GetPlainText();
        Task<string> GetPlainTextAsync();
    }
}
