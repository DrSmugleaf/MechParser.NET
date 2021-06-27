using System.Collections.Generic;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using MechParser.NET.Mechs;

namespace MechParser.NET.Smurfy
{
    public static class SmurfyHtmlImporter
    {
        public static IEnumerable<Mech> ParseDocument(IDocument document)
        {
            var rows = document.Body.SelectNodes("/html/body/div[3]/table[1]//tr");

            var variant = string.Empty;
            var tonnage = 0;

            foreach (var element in rows)
            {
                if (element is not IHtmlTableRowElement row ||
                    row.ClassList.Contains("hidden"))
                {
                    continue;
                }

                if (row.GetAttribute("data-mechfilter-faction") == null)
                {
                    var family = row.QuerySelector("th.mechs_family")?.TextContent.Trim();

                    if (family != null)
                    {
                        var openingParenthesis = family.LastIndexOf('(');
                        var closingParenthesis = family.LastIndexOf(')');

                        variant = family.Substring(0, openingParenthesis - 1);

                        var tonnageString = family.Substring(openingParenthesis + 1, closingParenthesis - openingParenthesis - 1);
                        tonnage = int.Parse(tonnageString);
                    }

                    continue;
                }

                var mechRow = new SmurfyMechRow(row);
                var faction = mechRow.ParseFaction();
                var model = mechRow.ParseModel();
                var parts = mechRow.ParseParts();
                var jumpJets = mechRow.ParseJumpJets();
                var ecm = mechRow.ParseEcm();
                var masc = mechRow.ParseMasc();
                var (minimumEngine, maximumEngine) = mechRow.ParseEngineRange();
                var defaultEngine = mechRow.ParseDefaultEngine();
                var hardpoints = mechRow.ParseHardpoints();
                var (torsoYaw, torsoPitch, armYaw, armPitch) = mechRow.ParseTwist();
                var (mcCost, cBillsCost) = mechRow.ParseCost();

                yield return new Mech(
                    faction,
                    variant,
                    model,
                    tonnage,
                    parts,
                    jumpJets,
                    ecm,
                    masc,
                    minimumEngine,
                    maximumEngine,
                    defaultEngine,
                    hardpoints,
                    torsoYaw,
                    torsoPitch,
                    armYaw,
                    armPitch,
                    mcCost,
                    cBillsCost);
            }
        }

        public static IEnumerable<Mech> ParseHtml(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync(url).Result;

            return ParseDocument(document);
        }

        public static async IAsyncEnumerable<Mech> ParseHtmlAsync(string url)
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
