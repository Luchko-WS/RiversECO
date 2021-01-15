using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RiversECO.Plugins.WebPageParser
{
    public class WebPageParser : IPlainTextParser
    {
        private Uri _uri;
        
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
            if (contentType.MediaType == "text/html")
            {
                var content = await response.Content.ReadAsStringAsync();
                var parser = new HtmlPlainTextParser();
                parser.LoadHtml(content);
                return parser;
            }

            throw new Exception($"Unsupported content type {contentType.MediaType}");
        }

        private void ValidateState()
        {

        }
    }
}
