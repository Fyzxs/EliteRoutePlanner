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
using Newtonsoft.Json.Linq;

namespace PrettyThings.data.model
{
    public class Station : BaseModelDal<Station>
    {

        public static new Station[] ParseFromJsonPath(string jsonPath)
        {
            var stations = new List<Station>();
            var textReader = new StreamReader(jsonPath);

            using (var jsonReader = new JsonTextReader(textReader))
            {
                jsonReader.Read();
                try
                {
                    var sb = new StringBuilder();
                    var ctr = 0;
                    var haveStation = false;
                    while (jsonReader.Read())
                    {
                        switch (jsonReader.TokenType)
                        {
                            case JsonToken.StartObject:
                                ctr++;
                                sb.Append("{");
                                break;
                            case JsonToken.EndObject:
                                haveStation = --ctr == 0;
                                sb.Append("}");
                                if (!haveStation)
                                {
                                    sb.Append(",");
                                }
                                break;
                            case JsonToken.StartArray:
                                sb.Append("[");
                                break;
                            case JsonToken.EndArray:
                                sb.Append("],");
                                break;
                            case JsonToken.PropertyName:
                                sb.Append("\"" + jsonReader.Value.ToString() + "\":");
                                break;
                            case  JsonToken.String:
                                sb.Append("\"" + jsonReader.Value.ToString() + "\",");
                                break;
                            case JsonToken.Integer:
                            case JsonToken.Float:
                                sb.Append(jsonReader.Value.ToString() + ",");
                                break;
                            case JsonToken.Null:
                                sb.Append("null,");
                                break;
                            default:
                                Console.WriteLine("vroom");
                            
                            break;
                        }
                        if (haveStation)
                        {
                            try
                            {
                                var station = JsonConvert.DeserializeObject<Station>(sb.ToString());
                                stations.Add(station);
                                haveStation = false;
                                sb.Clear();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return stations.ToArray();
        }
        public static void Init(SQLiteConnection conn)
        {
            conn.CreateTable<Station>();
        }

        public static void InsertOrReplace(Station station)
        {
            if (station.Id <= 0) { return; }
            if (ProcessListing(station))
            {
                var result = SystemDatabase.Connection.InsertOrReplace(station);
                result *= result;
                foreach (var listing in station.Listings)
                {
                    //Console.WriteLine(listing);
                    StationListing.InsertOrReplace(listing);
                }
            }
        }

        public static bool ProcessListing(Station station)
        {
            var maxDistance = 40;
            var x = -21;
            var y = -63;
            var z = 52;
            var xAdded = x + maxDistance;
            var xSubed = x - maxDistance;
            var yAdded = y + maxDistance;
            var ySubed = y - maxDistance;
            var zAdded = z + maxDistance;
            var zSubed = z - maxDistance;

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
        public StationListing[] _listings;
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