using System.Linq;
using MechParser.NET.Smurfy;
using NUnit.Framework;

namespace MechParser.NET.Tests.Smurfy
{
    [TestFixture]
    public class SmurfyCsvParseTest
    {
        [Test]
        public void ParseTest()
        {
            var mechs = SmurfyImporter.ParseCsvPaste("mwomechs.csv").ToArray();

            Assert.That(mechs.Length, Is.EqualTo(701));
        }
    }
}
