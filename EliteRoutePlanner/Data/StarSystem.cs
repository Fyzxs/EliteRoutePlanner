using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteRoutePlanner.Data
{
    public class StarSystem
    {
        public string Name { get; private set; }
        public List<Station> Stations { get; private set; }
        public Dictionary<decimal, StarSystem> JumpableSystems { get; private set; }

        public StarSystem(string name)
        {
            Name = name;
            Stations = new List<Station>();
            JumpableSystems = new Dictionary<decimal, StarSystem>();
        }

        public void AddStation(string name, decimal distance)
        {
            Station s = new Station(name, distance, this);
            Stations.Add(s);
            MasterDataController.Instance.AddStation(s);
        }

        public Station GetStation(string name)
        {
            return Stations.Find(x => x.Name == name);
        }

        public bool HasStation(string name)
        {
            return Stations.Any(x => x.Name == name);
        }

        public bool HasJumpSystem(string name)
        {
            return JumpableSystems.Values.Any(x => x.Name == name);
        }

        public void AddJumpSystem(string name, decimal distance)
        {
            if (HasJumpSystem(name))
            {
                return;
            }
            while (true)
            {
                try
                {
                    JumpableSystems.Add(distance, MasterDataController.Instance.GetStarSystem(name));
                    return;
                }
                catch (ArgumentException)
                {
                    distance += (decimal) 0.01;
                }
            }
        }


        public IEnumerable<StarSystem> GetStarSystemsCloserThan(decimal distance)
        {
            return from x in JumpableSystems where x.Key < distance select x.Value;
        }
        
        public List<StationToStationProfit> GetBestProfitToStarSystemsWithin(decimal distance)
        {
            var profits = new List<StationToStationProfit>();

            foreach (var sellToSystem in GetStarSystemsCloserThan(distance))//System Touching
            {
                int profit = 0;
                string commodity = null;
                Station buyStation = null;
                Station sellStation = null;
                foreach (var sellToStation in sellToSystem.Stations)//Get stations from sell_SYSTEM
                {
                    foreach (var buyFromStation in Stations)//this system's stations
                    {
                        var profitable = buyFromStation.GetBestCommodityProfitableToSellAt(sellToStation);
                        if (profitable.Key == 0) continue;

                        var max = profitable.Key;
                        if (max > profit)
                        {
                            profit = max;
                            commodity = profitable.Value;
                            buyStation = buyFromStation;
                            sellStation = sellToStation;
                        }
                    }
                    if (profit > 0 && commodity != null && buyStation != null && sellStation != null)
                    {
                        profits.Add(new StationToStationProfit(profit, commodity, sellToSystem.Name, sellStation.Name,
                            this.Name, buyStation.Name));
                    }
                }
            }

            return profits;
        }
    }
}