using System.Linq;
using MechParser.NET.Mechs.Parts;
using MechParser.NET.Mechs.Slots;
using MechParser.NET.Smurfy;
using NUnit.Framework;

namespace MechParser.NET.Tests.Smurfy
{
    [TestFixture]
    public class SmurfyMissileParseTest
    {
        [Test]
        [TestCase("1 M (10)", 1)]
        [TestCase("1 M (6)", 1)]
        [TestCase("2 M (10x2)", 2)]
        [TestCase("2 M (10, 6)", 2)]
        [TestCase("2 M (6x2)", 2)]
        [TestCase("2 M (6, 10)", 2)]
        public void MissileParseTest(string text, int missileAmount)
        {
            var missiles = SmurfyCsvImporter
                .ParsePart(PartType.Center, text)
                .Slots
                .Count(s => s.Type == ModuleType.Missile);

            Assert.That(missiles, Is.EqualTo(missileAmount));
        }
    }
}
