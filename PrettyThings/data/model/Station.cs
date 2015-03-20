using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrettyThings.data.model;
using PrettyThings.data.planning;
using SQLite;

namespace PrettyThings.data.model
{
    public class Station : BaseModelDal<Station>
    {
        public static void Init(SQLiteConnection conn)
        {
            conn.CreateTable<Station>();
        }

        public static void InsertOrReplace(Station station)
        {
            if (station.Id <= 0) { return; }
            SystemDatabase.Connection.InsertOrReplace(station);
            if (ProcessListing(station))
            {
                foreach (var listing in station.Listings)
                {
                    //Console.WriteLine(listing);
                    StationListing.InsertOrReplace(listing);
                }
            }
        }

        public static bool ProcessListing(Station station)
        {
            var x = -20.65625;
            var y = -62.28125;
            var z = 51.875;
            var maxDistance = 40;

            var sys = station.ParentSystem;
            return Math.Sqrt(
                Math.Pow(sys.x - x, 2) +
                Math.Pow(sys.y - y, 2) +
                Math.Pow(sys.z - z, 2)) <= maxDistance;

        }

        public override void InsertOrReplace()
        {
            InsertOrReplace(this);
        }

        public static bool Has(string name)
        {
            return SystemDatabase.Connection.Table<Station>().Any(x => x.Name == name);
        }

        public static Station Get(string name)
        {
            return SystemDatabase.Connection.Table<Station>().FirstOrDefault(x => x.Name == name);
        }

        [PrimaryKey]
        public long Id { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "has_blackmarket")]
        public bool? HasBlackmarket { get; set; }

        [JsonProperty(PropertyName = "max_landing_pad_size")]
        public int? MaxLandingPadSize { get; set; }

        [JsonProperty(PropertyName = "distance_to_star")]
        public float? DistanceToStar { get; set; }

        [JsonProperty(PropertyName = "system_id")]
        public long ParentSystemId { get; set; }

        [JsonProperty(PropertyName = "listings")]
        private StationListing[] _listings;
        [Ignore]
        public StationListing[] Listings
        {
            get
            {
                lock (this)
                {
                    return _listings ??
                           (_listings =
                               SystemDatabase.Connection.Table<StationListing>().Where(x => x.StationId == Id).ToArray());
                }
            }
        }

        internal void purgeListings()
        {
            lock (this)
            {
                _listings = null;
            }
        }

        private StarSystem _parentSystem = null;
        [Ignore]
        public StarSystem ParentSystem
        {
            get
            {
                return _parentSystem ??
                       (_parentSystem = SystemDatabase.Connection.Table<StarSystem>().First(x => x.Id == ParentSystemId));
            }
        }


        public IEnumerable<StationListing> GetPurchaseable()
        {
            return Listings.Where(x => x.BuyPrice > 0);
        }

        public StationListing GetSellable(long commodityId)
        {
            return Listings.FirstOrDefault(x => x.SellPrice > 0 && x.CommodityId == commodityId);
        }

        public StationToStationProfit GetMostProfitableToSellAt(Station sellAtStation)
        {
            StationToStationProfit profit = null;
            foreach (var buyListing in GetPurchaseable())
            {
                var sellListing = sellAtStation.GetSellable(buyListing.CommodityId);
                if (sellListing == null || sellListing.SellPrice <= buyListing.BuyPrice) continue;

                if (profit == null)
                {
                    profit = new StationToStationProfit(sellAtStation, sellListing, this, buyListing);
                    continue;
                }

                var p = sellListing.SellPrice - buyListing.BuyPrice;
                if (p < profit.ProfitAmount() || 
                    (  p == profit.ProfitAmount() && 
                       sellListing.SellPrice < profit.SellAtListing.SellPrice))
                    continue;

                profit = new StationToStationProfit(sellAtStation, sellListing, this, buyListing);
            }

            return profit;
        }

        public override string ToString()
        {
            return string.Format("[Name={0}] [RowId={1}] [Blackmarket={2}] [MaxLanding={3}]", Name, Id, HasBlackmarket,
                MaxLandingPadSize);
        }
    }
}