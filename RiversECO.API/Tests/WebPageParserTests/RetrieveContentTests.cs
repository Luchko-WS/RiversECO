using System;
using System.Text;
using NUnit.Framework;
using RiversECO.Plugins.WebPageParser;

namespace WebPageParserTests
{
    public class RetrieveContentTests
    {
        [SetUp]
        public void Setup()
        {
            // ATTENTION: 
            // 1. install System.Text.Encoding.CodePages NuGet
            // 2. register encoding provider to API before using parsers.
            // see: https://docs.microsoft.com/en-us/dotnet/api/system.text.codepagesencodingprovider?redirectedfrom=MSDN&view=net-5.0#Anchor_4
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Test]
        public void GetTextFromHtml()
        {
            var uri = new Uri("https://docs.microsoft.com/en-us/dotnet/api/system.net.webclient.downloadstring?view=net-5.0");
            var parser = new WebPageParser();
            parser.SetUri(uri);

            var content = parser.GetPlainText();

            Assert.IsFalse(string.IsNullOrEmpty(content));
        }

        [Test]
        public void GetTextFromPdfUri()
        {
            var uri = new Uri("http://www.africau.edu/images/default/sample.pdf");
            var parser = new WebPageParser();
            parser.SetUri(uri);

            var content = parser.GetPlainText();

            Assert.IsFalse(string.IsNullOrEmpty(content));
        }

        [Test]
        public void GetTextFromWordUri()
        {
            var uri = new Uri("http://iiswc.org/iiswc2012/sample.doc");
            var parser = new WebPageParser();
            parser.SetUri(uri);

            var content = parser.GetPlainText();

            Assert.IsFalse(string.IsNullOrEmpty(content));
        }
    }
}