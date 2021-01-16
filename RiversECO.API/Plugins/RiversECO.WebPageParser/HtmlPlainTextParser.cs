using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace RiversECO.Plugins.WebPageParser
{
    public class HtmlPlainTextParser : IPlainTextParser
    {
        private HtmlDocument _htmlDoc;

        public HtmlPlainTextParser() { }

        public HtmlPlainTextParser(string html)
        {
            LoadHtml(html);
        }

        public void LoadHtml(string html)
        {
            _htmlDoc = new HtmlDocument();
            _htmlDoc.LoadHtml(html);
        }

        public string GetPlainText()
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

            var resultText = string.Join('\n', lines);
            return resultText;
        }

        public Task<string> GetPlainTextAsync()
        {
            return Task.Factory.StartNew(GetPlainText);
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
