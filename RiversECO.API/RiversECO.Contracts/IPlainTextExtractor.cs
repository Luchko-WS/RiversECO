using System.Threading.Tasks;

namespace RiversECO.Contracts
{
    public interface IPlainTextExtractor
    {
        string ExtractPlainText();
        Task<string> ExtractPlainTextAsync();
    }
}
