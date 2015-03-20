using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrettyThings.data;
using PrettyThings.data.model;

namespace PrettyThings.Menu
{
    public class OcrImport
    {
        private const int OcrHeaderSystem = 0;
        private const int OcrHeaderStation = 1;
        private const int OcrHeaderCommodity = 2;
        private const int OcrHeaderSell = 3;
        private const int OcrHeaderBuy = 4;
        private const int OcrHeaderDemand = 5;
        private const char OcrSplitter = ';';
        public static readonly OcrImport Instance = new OcrImport();

        private readonly BackgroundWorker _importWorker = new BackgroundWorker();


        private class ImportUserState
        {
            public string Label;
        }

        private OcrImport()
        {
            _importWorker.DoWork += ImportWorkerOnDoWork;
            _importWorker.ProgressChanged += ImportWorkerOnProgressChanged;
            _importWorker.RunWorkerCompleted += ImportWorkerOnRunWorkerCompleted;

            _importWorker.WorkerSupportsCancellation = true;
            _importWorker.WorkerReportsProgress = true;
        }

        private void ImportWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            var active = Form.ActiveForm as Form1;

            if (active == null)
            {
                return;
            }

            active.stsMainProgress.Visible = false;
            active.stsMainStatus.Visible = false;
            active.stsMainStatus.Text = "";
        }

        private void ImportWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            var active = Form.ActiveForm as Form1;
            var ius = progressChangedEventArgs.UserState as ImportUserState;

            if (active == null || ius == null)
            {
                return;
            }
            active.stsMainStatus.Text = ius.Label;
        }

        private void ImportWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {

            var worker = sender as BackgroundWorker;
            if (worker == null)
            {
                doWorkEventArgs.Cancel = true;
                return;
            }
            var form = Form.ActiveForm as Form1;

            const string commodtiesPath = "batch.csv";

            worker.ReportProgress(0, new ImportUserState() { Label = String.Format("Importing OCR Commodity Data...") });

            ImportFromOcr(worker, commodtiesPath);
        }


        public static void ImportFromOcr(BackgroundWorker workder, string path)
        {
            string line;

            var file = new StreamReader(path);
            file.ReadLine();//Skip Headers
            StarSystem starSystem = null;
            Station station = null;
            var missingStations = new List<string>();
            var badCommodities = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                var values = line.Split(OcrSplitter);
                var systemName = values[OcrHeaderSystem];
                var stationName = values[OcrHeaderStation];
                var commodityName = values[OcrHeaderCommodity];
                var sell = Int32.Parse(values[OcrHeaderSell]);
                var buy = 0; Int32.TryParse(values[OcrHeaderBuy], out buy);
                var demand = 0; Int32.TryParse(values[OcrHeaderDemand], out demand);

                if (starSystem == null || starSystem.Name != systemName)
                {
                    if (!StarSystem.Has(systemName))
                    {
                        continue;
                    }
                    starSystem = StarSystem.Get(systemName);
                }
                if (station == null || station.Name != stationName)
                {
                    if (!Station.Has(stationName))
                    {
                        //station = new Station {ParentSystemId = starSystem.RowId, Name = stationName};
                        //Station.InsertOrReplace(station);
                        var info = String.Format("{0}/{1}", systemName, stationName);
                        if (!missingStations.Contains(info))
                        {
                            Console.WriteLine("Station [{0}/{1}] doesn't exist in EDDN Data - Go Fix that.", systemName,
                                stationName);
                            missingStations.Add(info);
                        }
                        continue;
                    }
                    station = Station.Get(stationName);
                }

                var item = station.Listings.FirstOrDefault(x => x.Commodity.Name == commodityName);

                if (item == null)
                {

                    var c = Commodity.Get(commodityName);
                    if (c == null)
                    {
                        if (!badCommodities.Contains(commodityName))
                        {
                            badCommodities.Add(commodityName);
                            Console.WriteLine("Unable to process [commodityName={0}]", commodityName);
                        }
                        continue;
                    }
                    item = new StationListing
                    {
                        BuyPrice = buy,
                        SellPrice = sell,
                        CommodityId = c.Id,
                        StationId = station.Id,
                        Demand = demand
                    };
                }
                else
                {
                    item.BuyPrice = buy;
                    item.SellPrice = sell;
                    item.Demand = demand;

                }
                StationListing.InsertOrReplace(item);
                station.purgeListings();


                workder.ReportProgress(0, new ImportUserState() { Label = String.Format("{1}/{2} - Imported: {0}", commodityName, systemName, stationName) });
            }

            file.Close();

            foreach (var missingStation in missingStations)
            {
                Console.WriteLine("MISSING STATION:::" + missingStation);
            }
        }

        public void mniMainImportOcr_Click(object sender, EventArgs eventArgs)
        {
            if (_importWorker.IsBusy) return;

            var active = Form.ActiveForm as Form1;
            if (active != null)
            {
                active.stsMainProgress.Visible = false;
                active.stsMainStatus.Visible = true;
            }

            _importWorker.RunWorkerAsync();
        }
    }
}
