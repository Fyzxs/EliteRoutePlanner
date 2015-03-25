using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrettyThings.data;
using PrettyThings.data.model;

namespace PrettyThings.Menu

{
    internal class EddbImport
    {
        public static readonly EddbImport Instance = new EddbImport();

        private static ProcessFlags processFlags = ProcessFlags.None;

        [Flags]
        private enum ProcessFlags
        {
            None = 0 << 0,
            Commodities = 1 << 1,
            Stations = 1 << 2,
            Systems = 1 << 3,
            Stations_Lite = 1 << 4,
            All = ~0
        }

        private readonly BackgroundWorker _importWorker = new BackgroundWorker();
        private readonly BackgroundWorker _downloadWorker = new BackgroundWorker();
        private string _currentDownloadFile = "";


        private class ImportUserState
        {
            public string Label;
            public int Processed;
        }

        private EddbImport()
        {
            _importWorker.DoWork += ImportWorkerOnDoWork;
            _importWorker.ProgressChanged += ImportWorkerOnProgressChanged;
            _importWorker.RunWorkerCompleted += ImportWorkerOnRunWorkerCompleted;

            _importWorker.WorkerSupportsCancellation = true;
            _importWorker.WorkerReportsProgress = true;


            _downloadWorker.DoWork += DownloadWorkerOnDoWork;
            _downloadWorker.ProgressChanged += DownloadWorkerOnProgressChanged;
            _downloadWorker.RunWorkerCompleted += DownloadWorkerOnRunWorkerCompleted;
            _downloadWorker.WorkerSupportsCancellation = true;
            _downloadWorker.WorkerReportsProgress = true;
        }

        private void DownloadWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {

            var active = Form.ActiveForm as Form1;
            if (active == null)
            {
                return;
            }

            active.stsMainProgress.Visible = false;
            active.stsMainStatus.Visible = false;
            active.stsMainStatus.Text = "";

            active.mniMainImportEddb.Enabled = true;
        }

        private void DownloadWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            var active = Form.ActiveForm as Form1;
            var ius = progressChangedEventArgs.UserState as ImportUserState;

            if (active == null || ius == null)
            {
                return;
            }
            active.stsMainStatus.Text = ius.Label;
            active.stsMainProgress.Visible = ius.Processed > 0;
            active.stsMainProgress.Value = progressChangedEventArgs.ProgressPercentage;
        }

        private void DownloadWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var worker = sender as BackgroundWorker;
            if (worker == null)
            {
                doWorkEventArgs.Cancel = true;
                return;
            }

            var form = Form.ActiveForm as Form1;
            var formValid = form != null;
            if (!formValid) { return; }

            var commoditiesUrl = new Uri("http://eddb.io/archive/v2/commodities.json");
            var systemsUrl = new Uri("http://eddb.io/archive/v2/systems.json");
            var stationsUrl = new Uri("http://eddb.io/archive/v2/stations.json");


            worker.ReportProgress(0, new ImportUserState() { Label = String.Format("Starting Download of Commodities...") });
            DownloadWork(commoditiesUrl);
            worker.ReportProgress(0, new ImportUserState() { Label = String.Format("Starting Download of Systems...") });
            DownloadWork(systemsUrl);
            worker.ReportProgress(0, new ImportUserState() { Label = String.Format("Starting Download of Stations...") });
            DownloadWork(stationsUrl);
            worker.ReportProgress(0, new ImportUserState() { Label = String.Format("Downloads Finished...") });
        }

        private void DownloadWork(Uri url)
        {

            using (var client = new WebClient())
            {
                _currentDownloadFile = url.Segments.Last();
                client.DownloadProgressChanged += ClientOnDownloadProgressChanged;
                var x = client.DownloadFileTaskAsync(url, _currentDownloadFile);
                x.Wait();
            }
        }


        private void ClientOnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs downloadProgressChangedEventArgs)
        {
            //BOOOOOOOOOOOOOOOOO
            var processed =
                (int)
                    ((downloadProgressChangedEventArgs.BytesReceived*100)/
                     downloadProgressChangedEventArgs.TotalBytesToReceive);
            _downloadWorker.ReportProgress(processed, new ImportUserState() { Label = string.Format("{0} downloaded {1} of {2}", _currentDownloadFile, 
                downloadProgressChangedEventArgs.BytesReceived, downloadProgressChangedEventArgs.TotalBytesToReceive),
                                                                              Processed = processed
            });
            
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

            active.mniMainImportEddb.Enabled = true;
        }

        private static void ImportWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            var active = Form.ActiveForm as Form1;
            var ius = progressChangedEventArgs.UserState as ImportUserState;

            if (active == null || ius == null)
            {
                return;
            }
            active.stsMainStatus.Text = ius.Label;
            active.stsMainProgress.Visible = ius.Processed > 0;
            active.stsMainProgress.Value = progressChangedEventArgs.ProgressPercentage;
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
            var formValid = form != null;
            if (!formValid){return;}


            //var folder = !form.mniMainDebugUseSampleData.Checked ? "FullData" : "SampleData";
            var commodtiesPath = "commodities.json";
            var systemPath = "systems.json";
            var stationsPath = "stations.json";

            if ((processFlags & ProcessFlags.Commodities) != 0)
            {
                worker.ReportProgress(0, new ImportUserState() {Label = String.Format("Parsing Commodities...")});
                var commodities = Commodity.ParseFromJsonPath(commodtiesPath);
                worker.ReportProgress(0, new ImportUserState() { Label = String.Format("Importing Commodities...") });
                ImportCollection(worker, doWorkEventArgs, commodities);
            }

            if ((processFlags & ProcessFlags.Systems) != 0)
            {
                worker.ReportProgress(0, new ImportUserState() {Label = String.Format("Parsing Systems...")});
                var systems = StarSystem.ParseFromJsonPath(systemPath);
                worker.ReportProgress(0, new ImportUserState() {Label = String.Format("Importing Systems...")});
                ImportCollection(worker, doWorkEventArgs, systems);
            }

            if ((processFlags & ProcessFlags.Stations) != 0)
            {
                worker.ReportProgress(0, new ImportUserState() { Label = String.Format("Parsing Stations...") });
                var stations = Station.ParseFromJsonPath(stationsPath);
                worker.ReportProgress(0, new ImportUserState() { Label = String.Format("Importing Stations...") });
                ImportCollection(worker, doWorkEventArgs, stations);
            }
        }

        private void ImportCollection<T>(BackgroundWorker worker, CancelEventArgs args, IReadOnlyCollection<BaseModelDal<T>> collection)
        {
            var length = collection.Count;
            var count = 1;
            foreach(var item in collection)
            {
                if (worker.CancellationPending)
                {
                    args.Cancel = true;
                    return;
                }

                item.InsertOrReplace();
                worker.ReportProgress(((count * 100) / length), new ImportUserState() { Processed = count, Label = String.Format("{2}: {0} of {1}", count++, length, item.GetType().Name) });

            }
            
        }

        public void mniMainImportEddb_Click(object sender, EventArgs e)
        {
            processFlags = ProcessFlags.All;
            process(sender, e);
        }

        private void process(object sender, EventArgs e)
        {
            if (_importWorker.IsBusy) return;

            var active = Form.ActiveForm as Form1;
            if (active != null)
            {
                active.stsMainProgress.Visible = false;
                active.stsMainStatus.Visible = true;
                active.mniMainImportEddb.Enabled = false;
            }
            _importWorker.RunWorkerAsync();
        }

        public void mniMainImportEddbInterwebs_Click(object sender, EventArgs e)
        {
            if (_downloadWorker.IsBusy) return;

            var active = Form.ActiveForm as Form1;
            if (active != null)
            {
                active.stsMainProgress.Visible = false;
                active.stsMainStatus.Visible = true;
                active.mniMainImportEddb.Enabled = false;
            }
            _downloadWorker.RunWorkerAsync();
        }

        public void mniMainImportEddbCommodities_Click(object sender, EventArgs e)
        {
            processFlags = ProcessFlags.Commodities;
            process(sender, e);
        }

        public void mniMainImportEddbStations_Click(object sender, EventArgs e)
        {
            processFlags = ProcessFlags.Stations;// | ProcessFlags.Stations_Lite;
            process(sender, e);
        }
    }
}
