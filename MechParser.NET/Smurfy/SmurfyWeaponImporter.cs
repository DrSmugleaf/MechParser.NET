using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using MechParser.NET.Mechs;
using MechParser.NET.Mechs.Slots;
using MechParser.NET.Mechs.Weapons;

namespace MechParser.NET.Smurfy
{
    public static class SmurfyWeaponImporter
    {
        public static IEnumerable<Weapon> ParseDocument(IDocument document)
        {
            var rows = document.Body.SelectNodes("/html/body/div[3]/table[1]/tbody//tr").Cast<IHtmlElement>();

            HardpointType? type = null;
            Faction faction = 0;

            foreach (var element in rows)
            {
                if (element is not IHtmlTableRowElement row ||
                    row.QuerySelector("td > span")?.GetAttribute("title") == null)
                {
                    if (element.QuerySelector("th") is { } header &&
                        header.ClassList.Contains("tablehead"))
                    {
                        type = header.QuerySelector("h4")?.TextContent.ToLowerInvariant() switch
                        {
                            "ballistic weapons" => HardpointType.Ballistic,
                            "energy weapons" => HardpointType.Energy,
                            "missile based weapons" => HardpointType.Missile,
                            "support and utility weapons" => null,
                            _ => throw new ArgumentOutOfRangeException()
                        };

                        faction = header.QuerySelector("small")?.TextContent.ToLowerInvariant() switch
                        {
                            "innersphere" => Faction.InnerSphere,
                            "clan" => Faction.Clan,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                    }

                    continue;
                }

                var weaponRow = new SmurfyWeaponRow(row);

                var name = weaponRow.ParseName();
                var damage = weaponRow.ParseDamage();
                var heat = weaponRow.ParseHeat();
                var cooldown = weaponRow.ParseCooldown();
                var minimumRange = weaponRow.ParseMinimumRange();
                var optimalRange = weaponRow.ParseOptimalRange();
                var maxRange = weaponRow.ParseMaxRange();
                var slots = weaponRow.ParseSlots();
                var tons = weaponRow.ParseTons();
                var speed = weaponRow.ParseSpeed();
                var ammoPerTon = weaponRow.ParseAmmoPerTon();
                var dps = weaponRow.ParseDps();
                var dph = weaponRow.ParseDph();
                var dpsT = weaponRow.ParseDpsT();
                var hps = weaponRow.ParseHps();
                var impulse = weaponRow.ParseImpulse();
                var health = weaponRow.ParseHealth();
                var cBillsCost = weaponRow.ParseCBillsCost();

                var actualType = type ?? name.ToLowerInvariant() switch
                {
                    "ams" or "laser ams" or "c-ams" or "c-laser ams" => HardpointType.Ams,
                    "narc" or "c-narc" => HardpointType.Missile,
                    "tag" or "c-light tag" or "c-tag" => HardpointType.Energy,
                    _ => throw new ArgumentOutOfRangeException()
                };

                yield return new Weapon(
                    actualType,
                    faction,
                    name,
                    damage,
                    heat,
                    cooldown,
                    minimumRange,
                    optimalRange,
                    maxRange,
                    slots,
                    tons,
                    speed,
                    ammoPerTon,
                    dps,
                    dph,
                    dpsT,
                    hps,
                    impulse,
                    health,
                    cBillsCost);
            }
        }

        public static IEnumerable<Weapon> ParseHtmlUrl(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync(url).Result;

            return ParseDocument(document);
        }

        public static async IAsyncEnumerable<Weapon> ParseHtmlUrlAsync(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            foreach (var mech in ParseDocument(document))
            {
                yield return mech;
            }
        }
    }
}
