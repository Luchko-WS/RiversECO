using System;
using System.Net.Http;
using System.Threading.Tasks;
using RiversECO.Contracts;

namespace RiversECO.PlainTextExtractors
{
    public class UrlExtractor : IPlainTextExtractor
    {
        private Uri _uri;

        public UrlExtractor() { }

        public UrlExtractor(Uri uri)
        {
            SetUri(uri);
        }

        public void SetUri(Uri uri)
        {
            _uri = uri;
        }

        public string ExtractPlainText()
        {
            return ExtractPlainTextAsync().Result;
        }

        public async Task<string> ExtractPlainTextAsync()
        {
            if (_uri == null)
            {
                throw new Exception("Please set Uri first.");
            }

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _uri);
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("An error ocured during a sending request.");
                }

                var parser = await GetPlainTextParser(response);
                return parser.ExtractPlainText();
            }
        }

        private async Task<IPlainTextExtractor> GetPlainTextParser(HttpResponseMessage response)
        {
            var contentType = response.Content.Headers.ContentType;
            switch (contentType.MediaType)
            {
                // web page (htm, html)
                case "text/html":
                    var html = await response.Content.ReadAsStringAsync();
                    return new HtmlExtractor(html);
                // pdf
                case "application/pdf":
                    var pdfDocStream = await response.Content.ReadAsStreamAsync();
                    return new PdfExtractor(pdfDocStream);
                // word
                case "application/msword":
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    var wordDocStream = await response.Content.ReadAsStreamAsync();
                    return new WordExtractor(wordDocStream);
                // excel
                case "application/vnd.ms-excel":
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    throw new NotImplementedException();
                //json, xml, csv, txt
                case "application/json":
                case "application/xml":
                case "text/xml":
                case "text/csv":
                case "text/plain":
                    var text = await response.Content.ReadAsStringAsync();
                    return new PlainTextExtractor(text);
                default:
                    throw new Exception($"Unsupported content type {contentType.MediaType}.");
            }
        }
    }
}
