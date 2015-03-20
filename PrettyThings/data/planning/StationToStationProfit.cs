using System;
using System.Linq.Expressions;
using PrettyThings.data.model;

namespace PrettyThings.data.planning
{
    public class StationToStationProfit : IComparable
    {
        public Station SellAtStation { get; private set; }
        public StationListing SellAtListing { get; private set; }
        public Station BuyFromStation { get; private set; }
        public StationListing BuyFromListing { get; private set; }

        public StationToStationProfit(Station sellAtStation, StationListing sellAtListing,
            Station buyFromStation, StationListing buyFromListing)
        {
            SellAtStation = sellAtStation;
            SellAtListing = sellAtListing;
            BuyFromStation = buyFromStation;
            BuyFromListing = buyFromListing;
        }

        public int ProfitAmount()
        {
            try
            {
                if (SellAtListing != null && SellAtListing.SellPrice > 0 &&
                    BuyFromListing != null && BuyFromListing.BuyPrice > 0 &&
                    SellAtListing.CommodityId == BuyFromListing.CommodityId)
                {
                    return SellAtListing.SellPrice - BuyFromListing.BuyPrice;
                }
            }
            catch (Exception ignored)
            {
                /* no op */
            }

            return 0;
        }

        public override string ToString()
        {
            return String.Format("Profit {0} :: {1} from {2}/{3} sell at {4}/{5}", 
                ProfitAmount(), BuyFromListing.Commodity.Name, 
                BuyFromStation.ParentSystem.Name, BuyFromStation.Name, 
                SellAtStation.ParentSystem.Name, SellAtStation.Name);
        }


        public string GetTsv()
        {
            return String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                ProfitAmount(), BuyFromListing.Commodity.Name,
                BuyFromStation.ParentSystem.Name, BuyFromStation.Name,
                SellAtStation.ParentSystem.Name, SellAtStation.Name);
        }

        public static string GetTsvHeader()
        {

            return String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", "ProfitAmount", "CommodityName", "BuyFromStarSystem",
                "BuyFromStation", "SellAtStarSystem", "SellAtStation");
        }


        public int CompareTo(object obj)
        {
            var rhs = obj as StationToStationProfit;
            if (rhs == null)
            {
                return 1;
            }

            var result = 0;
            if ((result = ProfitAmount().CompareTo(rhs.ProfitAmount())) == 0)
            {
                result = BuyFromStation.Id.CompareTo(rhs.BuyFromStation.Id);
            }

            return result;
        }
    }
}
