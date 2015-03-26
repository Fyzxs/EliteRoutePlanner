using System;
using System.Linq.Expressions;
using PrettyThings.data.model;

namespace PrettyThings.data.planning
{
    public class StationToStationProfit : IComparable<StationToStationProfit>
    {
        public Station SellAtStation { get; private set; }
        public StationListing SellAtListing { get; private set; }
        public Station BuyFromStation { get; private set; }
        public StationListing BuyFromListing { get; private set; }
        public decimal PerUnitProfit { get; private set; }
        public decimal NumberOfUnits { get; private set; }
        public decimal ExpectedProfit { get; private set; }

        public StationToStationProfit(Station sellAtStation, StationListing sellAtListing,
            Station buyFromStation, StationListing buyFromListing, decimal perUnitProfit, decimal numberOfUnits, decimal expectedProfit)
        {
            SellAtStation = sellAtStation;
            SellAtListing = sellAtListing;
            BuyFromStation = buyFromStation;
            BuyFromListing = buyFromListing;
            PerUnitProfit = perUnitProfit;
            NumberOfUnits = numberOfUnits;
            ExpectedProfit = expectedProfit;
        }

        public decimal ProfitAmount()
        {
            return ExpectedProfit;

            try
            {
                if (SellAtListing != null && SellAtListing.SellPrice > 0 &&
                    BuyFromListing != null && BuyFromListing.BuyPrice > 0 &&
                    SellAtListing.CommodityId == BuyFromListing.CommodityId)
                {
                    return SellAtListing.SellPrice - BuyFromListing.BuyPrice;
                }
            }
            catch (Exception)
            {
                /* no op */
            }

            return 0;
        }

        public override string ToString()
        {
            return String.Format("Avg Profit {0} :: {1} from {2}/{3} for {4}/{5} sell at {6}/{7}", 
                ProfitAmount(), BuyFromListing.Commodity.Name, 
                BuyFromStation.ParentSystem.Name, BuyFromStation.Name, PerUnitProfit, BuyFromListing.BuyPrice,
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


        public int CompareTo(StationToStationProfit obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var isMatch = this.BuyFromStation.Id == obj.BuyFromStation.Id &&
                this.BuyFromListing.CommodityId == obj.BuyFromListing.CommodityId &&
                this.SellAtStation.Id == obj.SellAtStation.Id;
                
            if(isMatch)
            {
                return 0;
            }

            var result = 0;
            if ((result = ProfitAmount().CompareTo(obj.ProfitAmount())) == 0)
            {
                var shortCircuit = ((result = BuyFromStation.Id.CompareTo(obj.BuyFromStation.Id)) != 0) ||
                                   ((result = SellAtStation.Id.CompareTo(obj.SellAtStation.Id)) != 0) ||
                                   ((result = BuyFromListing.CommodityId.CompareTo(obj.BuyFromListing.CommodityId)) != 0);
            }

            return result;
        }
    }
}
