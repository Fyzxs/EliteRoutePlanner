using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;

namespace PrettyThings.data.model
{
    public class StationListing
    {
        public static void Init(SQLiteConnection conn)
        {
            conn.CreateTable<StationListing>();
        }

        public static int StartId = -1;

        /*public static int GetNextId()
        {
            if (StartId < 0)
            {
                StartId = SystemDatabase.Connection.Table<StationListing>().Count() + 1;
            }

            return StartId++;
        }*/

        public static void InsertOrReplace(StationListing listing)
        {
            /*if (listing.RowId == 0)
            {
                listing.RowId = 1;
            }*/
            /*if(listing.RowId <= 0)
            {
                listing.RowId = StartId++;
            }
            if (listing.StationId == 0 || listing.CommodityId == 0 || listing.RowId == 0)
            {
                Console.WriteLine("Unable to process {0}", listing);
                return;
            }*/
            var result = SystemDatabase.Connection.InsertOrReplace(listing);
            result *= result;

        }

/*
        [AutoIncrement, PrimaryKey]
        public int RowId { get; set; }
*/

        private string _key = null;

        [PrimaryKey]
        public string ItemKey
        {
            get { return (_key ?? (_key = string.Format("{0}_{1}", StationId, CommodityId))); }
            set { _key = value; }
        }

        [JsonProperty(PropertyName = "station_id")]
        public long StationId { get; set; }

        [JsonProperty(PropertyName = "commodity_id")]
        public long CommodityId { get; set; }

        [JsonProperty(PropertyName = "buy_price")]
        public int BuyPrice { get; set; }

        [JsonProperty(PropertyName = "sell_price")]
        public int SellPrice { get; set; }

        [JsonProperty(PropertyName = "demand")]
        public int Demand { get; set; }

        [JsonProperty(PropertyName = "supply")]
        public int Supply { get; set; }

        private Commodity _commodity;

        [Ignore]
        public Commodity Commodity
        {
            get
            {
                return _commodity ??
                       (_commodity = SystemDatabase.Connection.Table<Commodity>().FirstOrDefault(x => x.Id == CommodityId));
            }
        }

        private Station _station;

        [JsonIgnore]
        public Station Station
        {
            get
            {
                return _station ??
                       (_station = SystemDatabase.Connection.Table<Station>().FirstOrDefault(x => x.Id == StationId));
            }
        }

        public override string ToString()
        {
            return String.Format("[commodity={0}] [id={1}] [buy={2}] [sell={3}] [demand={4}] [stationId={5}]", (Commodity == null ? -1 : Commodity.Id), ItemKey, BuyPrice,SellPrice, Demand, StationId);
        }
    }
}
