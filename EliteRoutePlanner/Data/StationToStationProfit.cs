using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteRoutePlanner.Data
{
    public class StationToStationProfit : IComparable
    {
        public int ProfitAmount { get; private set; }
        public string CommodityName { get; private set; }
        public string SellAtStarSystem { get; private set; }
        public string SellAtStation { get; private set; }
        public string BuyFromStarSystem { get; private set; }
        public string BuyFromStation { get; private set; }

        public StationToStationProfit(int profitAmount, string commodityName, string sellAtStarSystem, string sellAtStation,
            string buyFromStarSystem, string buyFromStation)
        {
            ProfitAmount = profitAmount;
            CommodityName = commodityName;
            SellAtStarSystem = sellAtStarSystem;
            SellAtStation = sellAtStation;
            BuyFromStarSystem = buyFromStarSystem;
            BuyFromStation = buyFromStation;
        }

        public override string ToString()
        {
            return String.Format("Profit {0} :: {1} from {2}/{3} sell at {4}/{5}", ProfitAmount,
                CommodityName, BuyFromStarSystem, BuyFromStation, SellAtStarSystem, SellAtStation);
        }

        public string GetTsv()
        {
            return String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", ProfitAmount, CommodityName, BuyFromStarSystem,
                BuyFromStation, SellAtStarSystem, SellAtStation);
        }

        public static string GetTsvHeader()
        {

            return String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", "ProfitAmount", "CommodityName", "BuyFromStarSystem",
                "BuyFromStation", "SellAtStarSystem", "SellAtStation");
        }

        public int CompareTo(object obj)
        {
            if (!(obj is StationToStationProfit) || ProfitAmount > ((StationToStationProfit)obj).ProfitAmount)
            {
                return 1;
            }

            if (ProfitAmount < ((StationToStationProfit) obj).ProfitAmount)
            {
                return -1;
            }

            return String.Compare(BuyFromStation, ((StationToStationProfit) obj).BuyFromStation, StringComparison.Ordinal);
        }
    }
}
