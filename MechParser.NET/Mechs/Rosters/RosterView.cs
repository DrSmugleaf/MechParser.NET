using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MechParser.NET.Mechs.Rosters
{
    public class RosterView : IRoster
    {
        public RosterView(IEnumerable<Mech> mechs)
        {
            Mechs = mechs;
        }

        public IEnumerable<Mech> Mechs { get; }

        public IEnumerator<Mech> GetEnumerator()
        {
            return Mechs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Mechs).GetEnumerator();
        }

        public IEnumerable<string> GetModels()
        {
            return Mechs.Select(m => m.Model).Distinct();
        }

        public IRoster OfModel(string model)
        {
            return new RosterView(Mechs.Where(m => m.Model == model));
        }

        public IRoster OfTonnage(TonnageClass tonnage)
        {
            return new RosterView(Mechs.Where(m => m.TonnageClass == tonnage));
        }
    }
}
