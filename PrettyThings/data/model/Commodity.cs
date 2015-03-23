using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SQLite;

namespace PrettyThings.data.model
{
    public class Commodity : BaseModelDal<Commodity>
    {
        public static void Init(SQLiteConnection conn)
        {
            conn.CreateTable<Commodity>();
        }

        public static void InsertOrReplace(Commodity commodity)
        {
            if (commodity.Id <= 0){ return; }
            var result1 = SystemDatabase.Connection.InsertOrReplace(commodity);
            var result2 = SystemDatabase.Connection.InsertOrReplace(commodity._category);
            var result3 = result1 + result2;
        }

        public override void InsertOrReplace()
        {
            InsertOrReplace(this);
        }

        public static Commodity Get(string name)
        {
            return SystemDatabase.Connection.Table<Commodity>().FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// TURN INTO UPDATE W/O id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="averagePrice"></param>
        /// <param name="categoryId"></param>
        public static void Update(long id, string name, int averagePrice, long categoryId)
        {
            var c = new Commodity()
            {
                Id = id,
                Name = name,
                AveragePrice = averagePrice,
                CommodityCategoryId = categoryId
            };

            SystemDatabase.Connection.InsertOrReplace(c);
        }

        [PrimaryKey]
        public long Id { get; set; }

        public String Name { get; set; }

        [JsonProperty(PropertyName = "average_price")]
        public int? AveragePrice { get; set; }

        [JsonProperty(PropertyName = "category_id")]
        public long CommodityCategoryId { get; set; }

        [JsonProperty(PropertyName = "category")]
        private CommodityCategory _category = null;

        [Ignore] 
        public CommodityCategory Category
        {
            get {
                if (_category != null) return _category;

                var result =
                    SystemDatabase.Connection.Table<CommodityCategory>().Where(x => x.Id == CommodityCategoryId);
                if (result.Any())
                {
                    _category = result.First();
                }
                return _category;
            }
        }

        public override String ToString()
        {
            return String.Format("[Name={0}] [RowId={1}] [AveragePrice={2}] [CategoryId={3}] [CommodityCateogy={4}]",
                Name, Id, AveragePrice, CommodityCategoryId, Category);
        }
    }
}

