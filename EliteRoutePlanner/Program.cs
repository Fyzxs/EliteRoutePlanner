using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EliteRoutePlanner.Data;
using EliteRoutePlanner.Log;
using RethinkDb;
using RethinkDb.Configuration;

namespace EliteRoutePlanner
{

    class Program
    {
        private static readonly decimal MaxLightYears = new decimal(18.2);

        static void Main(string[] args)
        {
            Console.WriteLine("This is the starting");
            //run();
            doDb();
            Console.WriteLine("This is the ending");
            Console.ReadLine();
        }

        static void doDb()
        {
            var connectionFactory = ConfigurationAssembler.CreateConnectionFactory("example");
            
        }


        static void run()
        {
            Console.WriteLine("----Processing System Data----");
            LoadFromFile.ProcessSystemStationData();
            Console.WriteLine("----Processing Commodity Data----");
            LoadFromFile.ProcessOcr();

            Console.WriteLine("Systems Loaded {0}", MasterDataController.Instance.AllStarSystems.Count);

            demo4();
        }

        static void demo3()
        {
            var lines = new List<string> { StationToStationProfit.GetTsvHeader() };
            Console.WriteLine("----Finding Profitable Stations----");
            var profits = GetProfitableStationsWithin(MaxLightYears);

            foreach (var profit in profits)
            {
                Console.WriteLine(profit.ToString());
                lines.Add(profit.GetTsv());
            }
            System.IO.File.WriteAllLines("thisguy.tsv", lines.ToArray());
            Console.WriteLine("All Profits have been output to tsv");
            Console.WriteLine("Attempting Loops");

            
            Console.WriteLine("----Searching For Loops----");
            for(int currentStartIndex = 0, max = 0; currentStartIndex < profits.Count && max < 10000; currentStartIndex++, max++)
            {
                var loop = new Loop();
                loop.AddHop(profits[currentStartIndex]);
                if(FillLoop(loop, profits.GetRange(currentStartIndex + 1, profits.Count - (currentStartIndex + 1))))
                {
                    Console.Write(".");
                    if (max%500 == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine(DateTime.Now.ToShortTimeString());
                    }

                    currentStartIndex--;
                }
            }
            Console.WriteLine("Found {0} loops", MasterDataController.Instance.AllLoops.Count);

            lines.Clear();
            //MasterDataController.Instance.AllLoops.Sort();
            //MasterDataController.Instance.AllLoops.Reverse();
            lines.AddRange(MasterDataController.Instance.AllLoops.Select(loop => loop.ToTsv()));
            System.IO.File.WriteAllLines("loops.txt", lines.ToArray());
        }

        static void demo4()
        {
            var lines = new List<string> { StationToStationProfit.GetTsvHeader() };
            Console.WriteLine("----Finding Profitable Stations----");
            var profits = GetProfitableStationsWithin(MaxLightYears);

            foreach (var profit in profits)
            {
                Console.WriteLine(profit.ToString());
                lines.Add(profit.GetTsv());
            }
            System.IO.File.WriteAllLines("thisguy.tsv", lines.ToArray());
            Console.WriteLine("All Profits have been output to tsv");
            Console.WriteLine("Attempting Loops");


            Console.WriteLine("----Searching For Loops----");

            Parallel.For((int)0, (int)(profits.Count - 1), i =>
            {
                Console.WriteLine("Start Thread={0}, i={1}", Thread.CurrentThread.ManagedThreadId, i);
                var success = false;
                do
                {
                    var loop = new Loop();
                    loop.AddHop(profits[i]);
                    success = FillLoop(loop, profits.GetRange(i + 1, profits.Count - (i + 1)));
                } while (success);
                Console.WriteLine("Finish Thread={0}, i={1}", Thread.CurrentThread.ManagedThreadId, i);

            });

            /*
            for (int currentStartIndex = 0, max = 0; currentStartIndex < profits.Count && max < 10000; currentStartIndex++, max++)
            {
                var loop = new Loop();
                loop.AddHop(profits[currentStartIndex]);
                if (FillLoop(loop, profits.GetRange(currentStartIndex + 1, profits.Count - (currentStartIndex + 1))))
                {
                    Console.Write(".");
                    if (max % 500 == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine(DateTime.Now.ToShortTimeString());
                    }

                    currentStartIndex--;
                }
            }
            */
            Console.WriteLine("Found {0} loops", MasterDataController.Instance.AllLoops.Count);

            lines.Clear();
            //MasterDataController.Instance.AllLoops.Sort();
            //MasterDataController.Instance.AllLoops.Reverse();
            var sortable = MasterDataController.Instance.AllLoops.ToList();
            sortable.Sort();
            sortable.Reverse();
            lines.AddRange(sortable.Select(loop => loop.ToTsv()));
            System.IO.File.WriteAllLines("loops.txt", lines.ToArray());
            
        }

        static bool FillLoop(Loop loop, IReadOnlyList<StationToStationProfit> profits)
        {
            for (var index = 0; index < profits.Count; index++)
            {
                var response = loop.AddHop(profits[index]);
                switch (response)
                {
                    case Loop.AddHopResponse.Invalid:
                    case Loop.AddHopResponse.MatchesExistingLoop:
                    case Loop.AddHopResponse.StationExists:
                        break;
                    case Loop.AddHopResponse.LoopComplete:
                        MasterDataController.Instance.AddLoop(loop);
                        return true;
                    case Loop.AddHopResponse.Added:
                        index = -1;
                        break;
                    case Loop.AddHopResponse.Exists:
                        loop.RemoveLast();
                        break;
                }
            }
            return false;
        }

        static void demo2()
        {
            var lines = new List<string> {StationToStationProfit.GetTsvHeader()};
            var profits = GetProfitableStationsWithin(MaxLightYears);

            foreach (var profit in profits)
            {
                Console.WriteLine(profit.ToString());
                lines.Add(profit.GetTsv());
            }
            System.IO.File.WriteAllLines("thisguy.tsv", lines.ToArray());
        }

        static List<StationToStationProfit> GetProfitableStationsWithin(decimal distance)
        {
            var profits = new List<StationToStationProfit>();

            foreach (var buyingFromStation in MasterDataController.Instance.AllStations)
            {
                foreach (var sellingAtStation in MasterDataController.Instance.AllStations)
                {
                    if (!buyingFromStation.ParentSystem.JumpableSystems.ContainsValue(sellingAtStation.ParentSystem) || 
                        buyingFromStation.ParentSystem.JumpableSystems.Any(x => x.Value == sellingAtStation.ParentSystem && x.Key > distance))
                    {
                        continue;
                    }

                    var profitable = buyingFromStation.GetBestCommodityProfitableToSellAt(sellingAtStation);
                    if (profitable.Key == 0) continue;

                    profits.Add(new StationToStationProfit(profitable.Key, profitable.Value, sellingAtStation.ParentSystem.Name, sellingAtStation.Name,
                        buyingFromStation.ParentSystem.Name, buyingFromStation.Name));
                }
            }
            return profits;
        }

        static void demo()
        {
            List<string> lines = new List<string>();
            lines.Add(StationToStationProfit.GetTsvHeader());
            //var teaka = MasterDataController.Instance.GetStarSystem("Teaka");
            foreach (var teaka in MasterDataController.Instance.AllStarSystems)
            {
                var profits = teaka.GetBestProfitToStarSystemsWithin(20);

                foreach (var profit in profits)
                {
                    Console.WriteLine(profit.ToString());
                    lines.Add(profit.GetTsv());
                }
            }
            System.IO.File.WriteAllLines("thisguy.tsv", lines.ToArray());
        }

    }
}
