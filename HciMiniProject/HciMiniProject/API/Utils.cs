using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HciMiniProject.API
{
    public static class Utils
    {
        public static List<string> GDPIntervals { get; private set; }
        public const int GDPIntervalDefaulIndex = 1;            // default = annual

        public static List<string> TreasureIntervals { get; private set; }
        public const int TreasureIntervalsDefaulIndex = 2;      // default = monthly

        public static List<string> TreasureMaturity { get; private set; }
        public const int TreasureMaturityDefaulIndex = 4;       // default = 10 years

        static Utils()
        {
            GDPIntervals = new List<string>();
            TreasureIntervals = new List<string>();
            TreasureMaturity = new List<string>();

            GDPIntervals.Add("Quaterly");
            GDPIntervals.Add("Annual");         // default

            TreasureIntervals.Add("Daily");
            TreasureIntervals.Add("Weekly");
            TreasureIntervals.Add("Monthly");   // default

            TreasureMaturity.Add("3 months");
            TreasureMaturity.Add("2 years");
            TreasureMaturity.Add("5 years");
            TreasureMaturity.Add("7 years");
            TreasureMaturity.Add("10 years");  // default
            TreasureMaturity.Add("30 years");
        }
    }
}
