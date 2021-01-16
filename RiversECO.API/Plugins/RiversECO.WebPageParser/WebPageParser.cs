using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RiversECO.Plugins.WebPageParser
{
    public class WebPageParser : IPlainTextParser
    {
        private Uri _uri;

        public WebPageParser() { }

        public WebPageParser(Uri uri)
        {
            SetUri(uri);
        }

        public void SetUri(Uri uri)
        {
            _uri = uri;
        }

        public string GetPlainText()
        {
            return GetPlainTextAsync().Result;
        }

        public async Task<string> GetPlainTextAsync()
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
                return parser.GetPlainText();
            }
        }

        private async Task<IPlainTextParser> GetPlainTextParser(HttpResponseMessage response)
        {
            var contentType = response.Content.Headers.ContentType;
            switch (contentType.MediaType)
            {
                // web page (htm, html)
                case "text/html":
                    var html = await response.Content.ReadAsStringAsync();
                    return new HtmlPlainTextParser(html);
                // pdf
                case "application/pdf":
                    var pdfDocStream = await response.Content.ReadAsStreamAsync();
                    return new PdfPlainTextParser(pdfDocStream);
                // word
                case "application/msword":
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    var wordDocStream = await response.Content.ReadAsStreamAsync();
                    return new WordPlainTextParser(wordDocStream);
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
                    return new PlainTextContainer(text);
                default:
                    throw new Exception($"Unsupported content type {contentType.MediaType}.");
            }
        }
    }
}
