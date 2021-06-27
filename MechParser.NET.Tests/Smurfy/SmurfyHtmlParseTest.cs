using System.IO;
using System.Linq;
using AngleSharp.Html.Parser;
using MechParser.NET.Extensions;
using MechParser.NET.Smurfy;
using NUnit.Framework;

namespace MechParser.NET.Tests.Smurfy
{
    [TestFixture]
    public class SmurfyHtmlParseTest
    {
        [Test]
        public void ParseUrlTest()
        {
            var stream = File.OpenRead("mechs");
            var html = GZipExtensions.Decompress(stream);
            var document = new HtmlParser().ParseDocument(html);
            var mechs = SmurfyHtmlImporter.ParseDocument(document).ToArray();

            Assert.That(mechs.Length, Is.EqualTo(695));
        }
    }
}
