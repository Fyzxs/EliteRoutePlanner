using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteRoutePlanner.Data
{
    public class Commodity
    {
        public string Name { get; set; }
        public int SellFor { get; set; }
        public int BuyFor { get; set; }

        public Commodity(string name, int sellFor, int buyFor)
        {
            Name = name;
            SellFor = sellFor;
            BuyFor = buyFor;
        }
    }
}
