using System.Linq;
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
            var mechs = SmurfyHtmlImporter.ParseHtml("https://mwo.smurfy-net.de/").ToArray();

            Assert.That(mechs.Length, Is.EqualTo(695));
        }
    }
}
