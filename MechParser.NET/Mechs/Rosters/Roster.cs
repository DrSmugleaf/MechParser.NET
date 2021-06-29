using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MechParser.NET.Mechs.Rosters
{
    public class Roster : IRoster
    {
        public Roster(IEnumerable<Mech> mechs)
        {
            Mechs = new SortedSet<Mech>(mechs);
            Models = new HashSet<string>(Mechs.Select(m => m.Model));
        }

        private SortedSet<Mech> Mechs { get; }

        private HashSet<string> Models { get; }

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
            return Models;
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
