using System.IO;
using System.Linq;
using AngleSharp.Html.Parser;
using MechParser.NET.Extensions;
using MechParser.NET.Mechs;
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
            var mech = SmurfyMechImporter.ParseDocument(document).First();
            var json = mech.SerializeJson();
            var deserializedMech = json.DeserializeMech();

            Assert.NotNull(deserializedMech);
            Assert.That(mech.Faction, Is.EqualTo(deserializedMech!.Faction));
            Assert.That(mech.Model, Is.EqualTo(deserializedMech.Model));
            Assert.That(mech.Variant, Is.EqualTo(deserializedMech.Variant));
        }

        [Test]
        public void SerializeListTest()
        {
            var stream = File.OpenRead("mechs");
            var html = GZipExtensions.Decompress(stream);
            var document = new HtmlParser().ParseDocument(html);
            var mechs = SmurfyMechImporter.ParseDocument(document).ToArray();
            var json = mechs.SerializeJson();
            var deserializedMechs = json.DeserializeMechList();

            Assert.NotNull(deserializedMechs);
            Assert.That(mechs.Length, Is.EqualTo(deserializedMechs!.Count));
        }
    }
}
