using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace RiversECO.Plugins.WebPageParser
{
    public class PdfPlainTextParser : IPlainTextParser
    {
        private Stream _fileStream;

        public PdfPlainTextParser() { }

        public PdfPlainTextParser(Stream fileStream)
        {
            LoadPdf(fileStream);
        }

        public void LoadPdf(Stream fileStream)
        {
            _fileStream = fileStream;
        }

        public string GetPlainText()
        {
            if (_fileStream == null)
            {
                throw new Exception("Please load a PDF file first.");
            }

            var sb = new StringBuilder();
            using (var reader = new PdfReader(_fileStream))
            {
                using (var document = new PdfDocument(reader))
                {
                    var pagesCount = document.GetNumberOfPages();
                    for (var pageNumber = 1; pageNumber <= pagesCount; pageNumber++)
                    {
                        var page = document.GetPage(pageNumber);
                        var textContent = PdfTextExtractor.GetTextFromPage(page);
                        sb.AppendLine(textContent);
                    }
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
