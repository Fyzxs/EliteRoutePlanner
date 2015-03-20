using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteRoutePlanner.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EliteRoutePlanner.Data
{
    public class LoadFromFile
    {
        private const int OcrHeaderSystem = 0;
        private const int OcrHeaderStation = 1;
        private const int OcrHeaderCommodity = 2;
        private const int OcrHeaderSell = 3;
        private const int OcrHeaderBuy = 4;
        private const char OcrSplitter = ';';

        private const string JsonSystems = "systems";
        private const string JsonSystemStationName = "name";
        private const string JsonSystemStations = "stations";
        private const string JsonSystemStationsName = "name";
        private const string JsonSystemStationsDistance = "distance";
        private const string JsonSystemSystems = "systems";
        private const string JsonSystemSystemsName = "name";
        private const string JsonSystemSystemsDistance = "distance";
        /*
            {
                "systems": [
                    {
                        "name": "SYSTEM_NAME",
                        "stations": [
                            {
                                "name": "STATION_NAME",
                                "distance": 12345
                            }
                        ],
                        "systems": [
                            {
                                "name": "SYSTEM_NAME",
                                "distance": 12345
                            }
                        ]
                    }
                ]
            }
         */
        public static void ProcessSystemStationData()
        {
            var mdc = MasterDataController.Instance;
            var systemFiles = Directory.GetFiles(Environment.CurrentDirectory, "system.json");

            foreach (var filePath in systemFiles)
            {
                var file = new StreamReader(filePath);
                var data = JObject.Parse(file.ReadToEnd());

                var systemsArray = (JArray) data.GetValue(JsonSystems);
                foreach (var arr in systemsArray)
                {
                    ProcessSystem(arr);
                }
            }
        }

        private static void ProcessSystem(JToken system)
        {
            var systemName = system.Value<string>(JsonSystemStationName);
            if (!MasterDataController.Instance.HasStarSystem(systemName))
            {
                MasterDataController.Instance.AddStarSystem(systemName);
            }

            var starSystem = MasterDataController.Instance.GetStarSystem(systemName);

            ProcessSystemStations(starSystem, (JArray)system.SelectToken(JsonSystemStations));
            ProcessSystemSystems(starSystem, (JArray)system.SelectToken(JsonSystemSystems));
        }

        private static void ProcessSystemStations(StarSystem starSystem, JArray stations)
        {
            foreach (var station in stations)
            {
                var stationName = station.Value<string>(JsonSystemStationsName);
                if (!starSystem.HasStation(stationName))
                {
                    var stationDistance = station.Value<int>(JsonSystemStationsDistance);
                    starSystem.AddStation(stationName, stationDistance);
                }
            }
        }

        private static void ProcessSystemSystems(StarSystem starSystem, JArray systems)
        {
            foreach (var system in systems)
            {
                var systemName = system.Value<string>(JsonSystemSystemsName);
                if (starSystem.HasJumpSystem(systemName)) { continue; }
                
                if (!MasterDataController.Instance.HasStarSystem(systemName))
                {
                    MasterDataController.Instance.AddStarSystem(systemName);
                }
                var stationDistance = system.Value<decimal>(JsonSystemSystemsDistance);
                if (stationDistance >= 999){continue;}
                starSystem.AddJumpSystem(systemName, stationDistance);
            }
        }

        public static void ProcessOcr()
        {
            var mdc = MasterDataController.Instance;
            var ocrFiles = Directory.GetFiles(Environment.CurrentDirectory, "*.csv");
            foreach (var filePath in ocrFiles)
            {
                string line;

                var file = new StreamReader(filePath);
                file.ReadLine();//Skip Headers
                StarSystem starSystem = null;
                Station systemStation = null;
                while ((line = file.ReadLine()) != null)
                {
                    var values = line.Split(OcrSplitter);
                    var systemName = values[OcrHeaderSystem];
                    var stationName = values[OcrHeaderStation];
                    var commodityName = values[OcrHeaderCommodity];
                    var sell = int.Parse(values[OcrHeaderSell]);
                    int buy = 0; int.TryParse(values[OcrHeaderBuy], out buy);

                    if (starSystem == null || starSystem.Name != systemName)
                    {
                        if (!mdc.HasStarSystem(systemName))
                        {
                            MyLog.E("Do Not Have System Information for {0}", systemName);
                            continue;
                        }
                        starSystem = mdc.GetStarSystem(systemName);
                    }

                    if (systemStation == null || systemStation.Name != stationName)
                    {
                        if (!starSystem.HasStation(stationName))
                        {
                            MyLog.E("Do Not Have System/Station Information for {0}/{1}", systemName, stationName);
                            continue;
                        }
                        systemStation = starSystem.GetStation(stationName);
                    }

                    if (!systemStation.HasCommodity(commodityName)) { systemStation.AddCommodity(commodityName, sell, buy);}

                    var commodity = systemStation.GetCommodity(commodityName);
                    commodity.SellFor = sell;
                    commodity.BuyFor = buy;
                }

                file.Close();
            }
        }
    }
}
