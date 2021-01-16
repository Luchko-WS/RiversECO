using System.Threading.Tasks;

namespace RiversECO.Plugins.WebPageParser
{
    public class PlainTextContainer : IPlainTextParser
    {
        private string _content;

        public PlainTextContainer() { }

        public PlainTextContainer(string content)
        {
            LoadText(content);
        }

        public void LoadText(string content)
        {
            _content = content;
        }

        public string GetPlainText()
        {
            return _content;
        }

        public Task<string> GetPlainTextAsync()
        {
            return Task.FromResult(_content);
        }
    }
}
