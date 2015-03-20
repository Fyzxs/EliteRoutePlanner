using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PrettyThings.data.model
{
    public class CommodityCategory
    {
        public static void Init(SQLiteConnection conn)
        {
            conn.CreateTable<CommodityCategory>();
        }

        [PrimaryKey]
        public long Id { get; set; }

        public String Name { get; set; }

        public override string ToString()
        {
            return String.Format("[RowId={0}] [Name={1}]", Id, Name);
        }
    }
}
