using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EliteRoutePlanner.Data
{
    public class Station
    {
        public string Name { get; private set; }
        public List<Commodity> Commodities { get; private set; }
        public decimal Distance { get; private set; }
        public StarSystem ParentSystem { get; private set; }

        public Station(string name, decimal distance, StarSystem parentSystem)
        {
            Name = name;
            Distance = distance;
            Commodities = new List<Commodity>();
            ParentSystem = parentSystem;
        }

        public IEnumerable<Commodity> GetPurchasable()
        {
            return Commodities.Where(x => x.BuyFor > 0);
        }

        public IEnumerable<Commodity> GetSellable()
        {
            return Commodities.Where(x => x.SellFor > 0);
        }

        public bool HasCommodity(string name)
        {
            return Commodities.Any(x => x.Name == name);
        }

        public void AddCommodity(string name, int sell, int buy)
        {
            Commodities.Add(new Commodity(name, sell, buy));
        }

        public Commodity GetCommodity(string name)
        {
            return Commodities.Find(x => x.Name == name);
        }

        public KeyValuePair<int, string> GetBestCommodityProfitableToSellAt(Station sellAtStation)
        {

            var profit = 0;
            var commodity = String.Empty;
            foreach (var buyFrom in GetPurchasable())
            {
                foreach (var sellAt in sellAtStation.GetSellable())
                {
                    if (sellAt.Name == buyFrom.Name && sellAt.SellFor > buyFrom.BuyFor)
                    {
                        var p = sellAt.SellFor - buyFrom.BuyFor;
                        if (p > profit)
                        {
                            profit = p;
                            commodity = sellAt.Name;
                        }
                    }
                }
            }

            return String.IsNullOrEmpty(commodity) ? new KeyValuePair<int, string>(0, "NOTHING") : new KeyValuePair<int, string>(profit, commodity);
        }
    }
}
