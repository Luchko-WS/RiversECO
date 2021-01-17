using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RiversECO.Contracts;

namespace RiversECO.PlainTextExtractors
{
    public class HtmlExtractor : IPlainTextExtractor
    {
        private HtmlDocument _htmlDoc;

        public HtmlExtractor() { }

        public HtmlExtractor(string html)
        {
            LoadHtml(html);
        }

        public void LoadHtml(string html)
        {
            _htmlDoc = new HtmlDocument();
            _htmlDoc.LoadHtml(html);
        }

        public string ExtractPlainText()
        {
            if (_htmlDoc == null)
            {
                throw new Exception("Please load a HTML document first.");
            }

            var lines = _htmlDoc.DocumentNode.InnerText
                .Split('\n')
                .Select(CleanStringLine)
                .Where(line => !line.Equals(string.Empty))
                .ToArray();

            var resultText = string.Join("\n", lines);
            return resultText;
        }

        public Task<string> ExtractPlainTextAsync()
        {
            return Task.Factory.StartNew(ExtractPlainText);
        }

        private string CleanStringLine(string line)
        {
            return line
                .Replace("\r", string.Empty)
                .Replace("\t", string.Empty)
                .Trim();
        }
    }
}
