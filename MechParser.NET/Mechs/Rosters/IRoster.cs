using System.Collections.Generic;

namespace MechParser.NET.Mechs.Rosters
{
    public interface IRoster : IEnumerable<Mech>
    {
        IEnumerable<string> GetModels();

        IRoster OfModel(string model);

        IRoster OfTonnage(TonnageClass tonnage);
    }
}
