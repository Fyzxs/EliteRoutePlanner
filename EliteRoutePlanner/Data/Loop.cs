using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteRoutePlanner.Data
{
    public class Loop : IComparable
    {
        public enum AddHopResponse
        {
            Invalid,
            Exists,
            StationExists,
            MatchesExistingLoop,
            Added,
            LoopComplete
        }

        public List<StationToStationProfit> Hops { get; private set; }

        public override String ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} :: ", Weight());
            foreach (var hop in Hops)
            {
                sb.AppendFormat("{0}/{1} - {2}:{3} >> ", hop.BuyFromStarSystem, hop.BuyFromStation, hop.CommodityName, hop.ProfitAmount);
            }
            return sb.ToString();
        }

        public String ToTsv()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}\t", Weight());
            foreach (var hop in Hops)
            {
                sb.AppendFormat("{0}/{1}\t{2}\t{3}\t", hop.BuyFromStarSystem, hop.BuyFromStation, hop.CommodityName, hop.ProfitAmount);
            }
            return sb.ToString();
        }

        public Loop()
        {
            Hops = new List<StationToStationProfit>();
        }

        public void RemoveLast()
        {
            Hops.RemoveAt(Hops.Count-1);
        }

        public AddHopResponse AddHop(StationToStationProfit hop)
        {
            if (Hops.Any())
            {
                if (Hops.Contains(hop)) { return AddHopResponse.Exists;}
                var lastHop = Hops.Last();
                if (Hops.Contains(hop) ||
                    hop.BuyFromStarSystem != lastHop.SellAtStarSystem ||
                    hop.BuyFromStation != lastHop.SellAtStation)
                {
                    return AddHopResponse.Invalid;
                }

                for (var index = 1; index < Hops.Count; index++)
                {
                    if (Hops[index].BuyFromStation == hop.SellAtStation)
                    {
                        return AddHopResponse.StationExists;
                    }
                }

                if (MasterDataController.Instance.AllLoops.Any(x => x.SamePath(this, hop)))
                {
                    return AddHopResponse.MatchesExistingLoop;
                }
            }
            Hops.Add(hop);

            var firstHop = Hops[0];
            return (firstHop.BuyFromStarSystem == hop.SellAtStarSystem && firstHop.BuyFromStation == hop.SellAtStation) ? AddHopResponse.LoopComplete : AddHopResponse.Added;
        }

        public bool SamePath(Loop loop, StationToStationProfit lastHop)
        {
            if (Hops.Count != loop.Hops.Count + 1)
            {
                return false;//If it's not the same length; not the same path
            }

            //If the last buy/sell don't match; we can't be the same loop
            if (Hops.Last().BuyFromStarSystem != lastHop.BuyFromStarSystem ||
                Hops.Last().BuyFromStation != lastHop.BuyFromStation ||
                Hops.Last().SellAtStarSystem != lastHop.SellAtStarSystem ||
                Hops.Last().SellAtStation != lastHop.SellAtStation)
            {
                return false;
            }


            //If the rest of the path doesn't match; we can't be the same
            for (var index = 0; index < Hops.Count-1; index++)
            {
                //If we're not in the same system or same station; we're not the same path
                if (Hops[index].BuyFromStarSystem != loop.Hops[index].BuyFromStarSystem ||
                    Hops[index].BuyFromStation != loop.Hops[index].BuyFromStation)
                {
                    //There is only ONE commodity for any Given A->B
                    //Only the Buy needs to be checked because if the sell is different; the next Buy will be as well 
                    //OOOOORRRRR... other parts are so broken it doesn't matter.
                    return false;
                }
            }

            //Apparently there's nothing saying we're a different path
            return true;
        }

        public decimal Weight()
        {
            var profit = Hops.Aggregate<StationToStationProfit, decimal>(0, (current, stationToStationProfit) => current + stationToStationProfit.ProfitAmount);
            return Decimal.Round(profit/Hops.Count, 2);
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Loop) || ((Loop)obj).Weight() > Weight())
            {
                return -1;
            }

            if (Weight() > ((Loop) obj).Weight())
            {
                return 1;
            }

            //Weight is same
            if (Hops.Count > ((Loop)obj).Hops.Count)
            {
                return 1;
            }

            return Hops.Count < ((Loop)obj).Hops.Count ? -1 : 0;
        }
    }
}
