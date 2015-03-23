using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrettyThings.data.model;
using SQLite;

namespace PrettyThings.data
{
    public class SystemDatabase
    {
        private static readonly string PathToMemory = ":memory:";
        private static readonly string PathToFile = Path.Combine(Path.Combine(Environment.CurrentDirectory, "sample.sqlite"));

        public static readonly SQLiteConnection MemoryConnection = new SQLiteConnection(PathToMemory);
        public static readonly SQLiteConnection FileConnection = new SQLiteConnection(PathToFile);

        public static SQLiteConnection Connection
        {
            get { return MemoryConnection; }
        }

        public static void Initialization()
        {
            MiniInitialization(FileConnection);
            CopyDataToMemory();
            
        }

        private static void MiniInitialization(SQLiteConnection connection)
        {
            CommodityCategory.Init(connection);
            Commodity.Init(connection);
            StarSystem.Init(connection);
            Station.Init(connection);
            StationListing.Init(connection);
        }

        public static void CopyDataToMemory()
        {
            IntPtr backup = SQLite3.BackupInit(MemoryConnection.Handle, "main", FileConnection.Handle, "main");
            Console.WriteLine("[BackUp=" + backup + "] [extdErrorCode=" + SQLite3.ExtendedErrCode(MemoryConnection.Handle) + "]");
            SQLite3.BackupStep(backup, -1);
            SQLite3.BackupFinish(backup);
        }
        public static void CopyDataToDisk()
        {
            IntPtr backup = SQLite3.BackupInit(FileConnection.Handle, "main", MemoryConnection.Handle, "main");
            Console.WriteLine("[BackUp=" + backup + "] [extdErrorCode=" + SQLite3.ExtendedErrCode(MemoryConnection.Handle) + "]");
            SQLite3.BackupStep(backup, -1);
            SQLite3.BackupFinish(backup);
        }

        public static void Close()
        {
            CopyDataToDisk();
            MemoryConnection.Close();
            FileConnection.Close();
        }
    }
}
