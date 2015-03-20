using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteRoutePlanner.Data
{
    public class MasterDataController
    {
        public static MasterDataController Instance = new MasterDataController();

        public List<StarSystem> AllStarSystems = new List<StarSystem>();
        public List<Station> AllStations = new List<Station>();
        public BlockingCollection<Loop> AllLoops = new BlockingCollection<Loop>();

        public void AddLoop(Loop loop)
        {
            AllLoops.Add(loop);
        }

        public bool HasStation(Station station)
        {
            return AllStations.Contains(station);
        }
        public void AddStation(Station station)
        {
            if (HasStation(station)) return;
            AllStations.Add(station);
        }

        public bool HasStarSystem(string name)
        {
            return AllStarSystems.Any(x => x.Name == name);
        }

        public StarSystem GetStarSystem(string name)
        {
            return AllStarSystems.First(x => x.Name == name);
        }

        public void AddStarSystem(string name)
        {
            if (HasStarSystem(name)) return;

            AllStarSystems.Add(new StarSystem(name));
        }
    }
}
