using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrettyThings.Menu;

namespace PrettyThings.data.planning
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

        public SortedSet<StationToStationProfit> Hops { get; private set; }
        //public List<StationToStationProfit> Hops { get; private set; }

        public override String ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} :: ", Weight());
            foreach (var hop in Hops)
            {
                sb.AppendFormat("{0}/{1} - {2}:{3} >> ", hop.BuyFromStation.ParentSystem.Name, hop.BuyFromStation.Name, 
                    hop.BuyFromListing.Commodity.Name, hop.ProfitAmount());
            }
            return sb.ToString();
        }

        public String ToTsv()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}\t", Weight());
            foreach (var hop in Hops)
            {
                sb.AppendFormat("{0}/{1}\t{2}\t{3}\t", hop.BuyFromStation.ParentSystem.Name, hop.BuyFromStation.Name,
                    hop.BuyFromListing.Commodity.Name, hop.ProfitAmount());
            }
            return sb.ToString();
        }
        
        public Loop()
        {
            //Hops = new List<StationToStationProfit>();
            Hops = new SortedSet<StationToStationProfit>();
        }

        public void RemoveLast()
        {
            if (Hops.Count > 0)
            {
                Hops.Remove(Hops.Last());
            }
            //Hops.RemoveAt(Hops.Count-1);
        }

        public AddHopResponse AddHop(StationToStationProfit hop, List<Loop> localLoops )
        {
            if (Hops.Count > 0)
            {
                if (Hops.Contains(hop))
                {
                    return AddHopResponse.Exists;
                }
                var lastHop = Hops.Last();

                if (hop.BuyFromStation.ParentSystemId != lastHop.SellAtStation.ParentSystemId ||
                    hop.BuyFromStation != lastHop.SellAtStation)
                {
                    return AddHopResponse.Invalid;
                }

                for (var index = 1; index < Hops.Count; index++)
                {
                    if (Hops.ElementAt(index).BuyFromStation.Id == hop.SellAtStation.Id)
                    {
                        return AddHopResponse.StationExists;
                    }
                }
                for(int i = 0, len = localLoops.Count; i < len; i++)
                {
                    if (localLoops[i].SamePath(this, hop))
                    {
                        return AddHopResponse.MatchesExistingLoop;
                    }
                }
            }
            Hops.Add(hop);

            var firstHop = Hops.First();
            return (firstHop.BuyFromStation.ParentSystemId == hop.SellAtStation.ParentSystemId && firstHop.BuyFromStation == hop.SellAtStation) 
                ? AddHopResponse.LoopComplete 
                : AddHopResponse.Added;
        }

        public bool SamePath(Loop loop, StationToStationProfit currentHop)
        {
            if (Hops.Count != loop.Hops.Count + 1)
            {
                return false;//If it's not the same length; not the same path
            }


            var lastHop = Hops.Last();
            //If the last buy/sell don't match; we can't be the same loop
            if (lastHop.BuyFromStation.ParentSystemId != currentHop.BuyFromStation.ParentSystemId ||
                lastHop.BuyFromStation != currentHop.BuyFromStation ||
                lastHop.SellAtStation.ParentSystemId != currentHop.SellAtStation.ParentSystemId ||
                lastHop.SellAtStation != currentHop.SellAtStation)
            {
                return false;
            }


            //If the rest of the path doesn't match; we can't be the same
            for (var index = 0; index < Hops.Count-1; index++)
            {
                //If we're not in the same system or same station; we're not the same path
                var hopsEle = Hops.ElementAt(index);
                var loopEle = loop.Hops.ElementAt(index);
                if (hopsEle.BuyFromStation.ParentSystemId != loopEle.BuyFromStation.ParentSystemId ||
                    hopsEle.BuyFromStation != loopEle.BuyFromStation)
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
            var profit = Hops.Aggregate<StationToStationProfit, decimal>(0, 
                (current, stationToStationProfit) => current + stationToStationProfit.ProfitAmount());
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
