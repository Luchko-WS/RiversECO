using System.Threading.Tasks;
using RiversECO.Contracts;

namespace RiversECO.PlainTextExtractors
{
    public class PlainTextExtractor : IPlainTextExtractor
    {
        private string _content;

        public PlainTextExtractor() { }

        public PlainTextExtractor(string content)
        {
            LoadText(content);
        }

        public void LoadText(string content)
        {
            _content = content;
        }

        public string ExtractPlainText()
        {
            return _content;
        }

        public Task<string> ExtractPlainTextAsync()
        {
            return Task.FromResult(_content);
        }
    }
}
