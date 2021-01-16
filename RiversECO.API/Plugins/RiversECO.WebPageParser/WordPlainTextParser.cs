using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Spire.Doc;
using Spire.Doc.Documents;

namespace RiversECO.Plugins.WebPageParser
{
    public class WordPlainTextParser : IPlainTextParser
    {
        private Stream _fileStream;

        public WordPlainTextParser() { }

        public WordPlainTextParser(Stream fileStream)
        {
            LoadWordDocument(fileStream);
        }

        public void LoadWordDocument(Stream fileStream)
        {
            _fileStream = fileStream;
        }

        public string GetPlainText()
        {
            if (_fileStream == null)
            {
                throw new Exception("Please load a Word document file first.");
            }

            var document = new Document();
            document.LoadFromStream(_fileStream, FileFormat.Auto);

            var sb = new StringBuilder();
            foreach (Section section in document.Sections)
            {
                foreach (Paragraph paragraph in section.Paragraphs)
                {
                    sb.AppendLine(paragraph.Text);
                }
            }

            return sb.ToString();
        }

        public Task<string> GetPlainTextAsync()
        {
            return Task.Factory.StartNew(GetPlainText);
        }
    }
}
