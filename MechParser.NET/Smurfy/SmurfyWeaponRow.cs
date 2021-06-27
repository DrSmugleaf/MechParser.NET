using AngleSharp.Html.Dom;

namespace MechParser.NET.Smurfy
{
    public class SmurfyWeaponRow
    {
        public SmurfyWeaponRow(IHtmlTableRowElement row)
        {
            Name = row.Cells[0];
            Damage = row.Cells[1];
            Heat = row.Cells[2];
            Cooldown = row.Cells[3];
            Range = row.Cells[4];
            MaxRange = row.Cells[5];
            Slots = row.Cells[6];
            Tons = row.Cells[7];
            Speed = row.Cells[8];
            AmmoPerTon = row.Cells[9];
            DamagePerSecond = row.Cells[10];
            DamagePerHeat = row.Cells[11];
            DpsPerTon = row.Cells[12];
            HeatPerSecond = row.Cells[13];
            Impulse = row.Cells[14];
            Health = row.Cells[15];
            Costs = row.Cells[16];
        }

        private IHtmlTableCellElement Name { get; }

        private IHtmlTableCellElement Damage { get; }

        private IHtmlTableCellElement Heat { get; }

        private IHtmlTableCellElement Cooldown { get; }

        private IHtmlTableCellElement Range { get; }

        private IHtmlTableCellElement MaxRange { get; }

        private IHtmlTableCellElement Slots { get; }

        private IHtmlTableCellElement Tons { get; }

        private IHtmlTableCellElement Speed { get; }

        private IHtmlTableCellElement AmmoPerTon { get; }

        private IHtmlTableCellElement DamagePerSecond { get; }

        private IHtmlTableCellElement DamagePerHeat { get; }

        private IHtmlTableCellElement DpsPerTon { get; }

        private IHtmlTableCellElement HeatPerSecond { get; }

        private IHtmlTableCellElement Impulse { get; }

        private IHtmlTableCellElement Health { get; }

        private IHtmlTableCellElement Costs { get; }

        public string ParseName()
        {
            return Name.TextContent.Trim();
        }

        public double ParseDamage()
        {
            var str = Damage.TextContent.Trim();
            return str.Length == 0 ? 0 : double.Parse(str);
        }

        public double ParseHeat()
        {
            var str = Heat.TextContent.Trim();
            return str == "-" ? 0 : double.Parse(str);
        }

        public double ParseCooldown()
        {
            var str = Cooldown.TextContent.Trim().Replace(",", "");

            if (str == "-")
            {
                return 0;
            }

            if (str.Contains('+'))
            {
                str = str.Split('+')[0];
            }

            return double.Parse(str);
        }

        public double ParseChargeTime()
        {
            var str = Cooldown.TextContent.Trim().Replace(",", "");

            if (str.Contains('+'))
            {
                return double.Parse(str.Split('+')[1]);
            }

            return 0;
        }

        public int ParseMinimumRange()
        {
            var str = Range.TextContent.Trim().Replace(",", "");

            if (str.Contains('-'))
            {
                return int.Parse(str.Split('-')[0]);
            }

            return int.Parse(str);
        }

        public int ParseOptimalRange()
        {
            var str = Range.TextContent.Trim().Replace(",", "");

            if (str.Contains('-'))
            {
                return int.Parse(str.Split('-')[1]);
            }

            return int.Parse(str);
        }

        public int ParseMaxRange()
        {
            return int.Parse(MaxRange.TextContent.Trim().Replace(",", ""));
        }

        public int ParseSlots()
        {
            return int.Parse(Slots.TextContent.Trim());
        }

        public double ParseTons()
        {
            return double.Parse(Tons.TextContent.Trim());
        }

        public int? ParseSpeed()
        {
            var str = Speed.TextContent.Trim().Replace(",", "");
            return str == "-" ? null : int.Parse(str);
        }

        public double? ParseAmmoPerTon()
        {
            var str = AmmoPerTon.TextContent.Trim().Replace(",", "");
            return str == "-" ? null : double.Parse(str);
        }

        public double ParseDps()
        {
            var str = DamagePerSecond.TextContent.Trim();
            return str == "-" ? 0 : double.Parse(str);
        }

        public double ParseDph()
        {
            var str = DamagePerHeat.TextContent.Trim();
            return str == "-" ? 0 : double.Parse(str);
        }

        public double ParseDpsT()
        {
            var str = DpsPerTon.TextContent.Trim();
            return str == "-" ? 0 : double.Parse(str);
        }

        public double ParseHps()
        {
            var str = HeatPerSecond.TextContent.Trim();
            return str == "-" ? 0 : double.Parse(str);
        }

        public double ParseImpulse()
        {
            var str = Impulse.TextContent.Trim();
            return str == "-" ? 0 : double.Parse(str);
        }

        public double ParseHealth()
        {
            return double.Parse(Health.TextContent.Trim());
        }

        public int? ParseCBillsCost()
        {
            var str = Costs.TextContent.Trim().Replace(",", "");
            return str.ToLowerInvariant() == "n/a" ? null : int.Parse(str);
        }
    }
}
