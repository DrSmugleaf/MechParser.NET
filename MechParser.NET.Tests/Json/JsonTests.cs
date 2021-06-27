using System.IO;
using System.Linq;
using AngleSharp.Html.Parser;
using MechParser.NET.Extensions;
using MechParser.NET.Json;
using MechParser.NET.Smurfy;
using NUnit.Framework;

namespace MechParser.NET.Tests.Json
{
    [TestFixture]
    public class JsonTests
    {
        [Test]
        public void SerializeSingleTest()
        {
            var stream = File.OpenRead("mechs");
            var html = GZipExtensions.Decompress(stream);
            var document = new HtmlParser().ParseDocument(html);
            var mech = SmurfyHtmlImporter.ParseDocument(document).First();
            var json = mech.SerializeJson();
            var deserializedMech = json.DeserializeSingleMech();

            Assert.NotNull(deserializedMech);
            Assert.That(mech.Faction, Is.EqualTo(deserializedMech!.Faction));
            Assert.That(mech.Variant, Is.EqualTo(deserializedMech.Variant));
            Assert.That(mech.Model, Is.EqualTo(deserializedMech.Model));
        }

        [Test]
        public void SerializeListTest()
        {
            var stream = File.OpenRead("mechs");
            var html = GZipExtensions.Decompress(stream);
            var document = new HtmlParser().ParseDocument(html);
            var mechs = SmurfyHtmlImporter.ParseDocument(document).ToArray();
            var json = mechs.SerializeJson();
            var deserializedMechs = json.DeserializeList();

            Assert.NotNull(deserializedMechs);
            Assert.That(mechs.Length, Is.EqualTo(deserializedMechs!.Count));
        }
    }
}
