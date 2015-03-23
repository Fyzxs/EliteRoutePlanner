using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrettyThings.data.planning;
using SQLite;

namespace PrettyThings.data.model
{
    public class StarSystem : BaseModelDal<StarSystem>
    {
        public static void Init(SQLiteConnection conn)
        {
            conn.CreateTable<StarSystem>();
        }

        public static void InsertOrReplace(StarSystem system)
        {
            
            if (system.Id <= 0) { return; }
            var result = SystemDatabase.Connection.InsertOrReplace(system);
            result *= result;
        }

        public static bool Has(string starSystemName)
        {
            return SystemDatabase.Connection.Table<StarSystem>().Any(x => x.Name == starSystemName);
        }

        public static StarSystem Get(string name)
        {
            return SystemDatabase.Connection.Table<StarSystem>().FirstOrDefault(x => x.Name == name);
        }
        public override void InsertOrReplace()
        {
            InsertOrReplace(this);
        }

        [PrimaryKey]
        public long Id { get; set; }

        public string Name { get; set; }

        public float x { get; set; }

        public float y { get; set; }

        public float z { get; set; }

        private Station[] _stations;
        [Ignore]
        public Station[] Stations
        {
            get
            {
                return _stations ??
                       (_stations = SystemDatabase.Connection.Table<Station>().Where(s => s.ParentSystemId == Id).ToArray());
            }
        }
        public List<StationToStationProfit> GetProfitableStationsWithin(double distance)
        {
            var profits = new List<StationToStationProfit>();
            var closeSystems = GetStarSystemsCloserThan(distance);
            var allStations = new List<Station>();
            foreach (var system in closeSystems)
            {
                allStations.AddRange(system.Stations);
            }
            foreach (var buyingFromStation in allStations)
            {
                foreach (var sellingAtStation in allStations)
                {
                    if (buyingFromStation.Id == sellingAtStation.Id) { continue; }

                    if (!buyingFromStation.ParentSystem.CanJumpTo(sellingAtStation.ParentSystem, distance))
                    {
                        continue;
                    }

                    var profitable = buyingFromStation.GetMostProfitableToSellAt(sellingAtStation);
                    if (profitable == null)
                    {
                        continue;
                    }

                    profits.Add(profitable);
                }
            }
            return profits;
        }

        private bool CanJumpTo(StarSystem sys, double distance)
        {
            return Math.Sqrt(
                Math.Pow(sys.x - x, 2) +
                Math.Pow(sys.y - y, 2) +
                Math.Pow(sys.z - z, 2)) <= distance;
        }
        
        public IEnumerable<StarSystem> GetStarSystemsCloserThan(double distance)
        {/*
            Math.Sqrt(
                    Math.Pow(sys.x - x, 2) +
                    Math.Pow(sys.y - y, 2) +
                    Math.Pow(sys.z - z, 2)
                    */
            var xMin = x - distance;
            var xMax = x + distance;
            var yMin = y - distance;
            var yMax = y + distance;
            var zMin = z - distance;
            var zMax = z + distance;
            var resultSet = SystemDatabase.Connection.Table<StarSystem>().Where(sys =>
                sys.x >= xMin && sys.x <= xMax &&
                sys.y >= yMin && sys.y <= yMax &&
                sys.z >= zMin && sys.z <= zMax
                ).ToList().Where(sys => Math.Sqrt(
                    Math.Pow(sys.x - x, 2) +
                    Math.Pow(sys.y - y, 2) +
                    Math.Pow(sys.z - z, 2)) <= distance);
            return resultSet;
        }

        public override string ToString()
        {
            return String.Format("[Name={0}] [RowId={1}][{2},{3},{4}] [Stations={5}]", Name, Id, x, y, x, Stations.Length);
        }
    }
}
