using System;
using NUnit.Framework;
using RiversECO.Plugins.WebPageParser;

namespace WebPageParserTests
{
    public class RetrieveContentTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetWebPageHtml()
        {
            var uri = new Uri("https://docs.microsoft.com/en-us/dotnet/api/system.net.webclient.downloadstring?view=net-5.0");
            var parser = new WebPageParser();
            parser.SetUri(uri);

            var content = parser.GetPlainText();

            Assert.IsFalse(string.IsNullOrEmpty(content));
        }
    }
}