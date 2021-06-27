using System.IO;
using System.Linq;
using AngleSharp.Html.Parser;
using MechParser.NET.Extensions;
using MechParser.NET.Smurfy;
using NUnit.Framework;

namespace MechParser.NET.Tests.Smurfy
{
    [TestFixture]
    public class SmurfyWeaponImportTests
    {
        [Test]
        public void ParseFileTest()
        {
            var stream = File.OpenRead("weapons");
            var html = GZipExtensions.Decompress(stream);
            var document = new HtmlParser().ParseDocument(html);
            var weapons = SmurfyWeaponImporter.ParseDocument(document).ToArray();

            Assert.That(weapons.Count, Is.EqualTo(118));
        }
    }
}
