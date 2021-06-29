using System.Collections.Generic;
using MechParser.NET.Mechs.Rosters;

namespace MechParser.NET.Mechs
{
    public static class MechEnumerableExtensions
    {
        public static Roster ToRoster(this IEnumerable<Mech> mechs)
        {
            return new(mechs);
        }
    }
}
