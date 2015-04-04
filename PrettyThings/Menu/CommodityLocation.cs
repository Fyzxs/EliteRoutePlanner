using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrettyThings.data;
using PrettyThings.data.model;

namespace PrettyThings.Menu
{
    internal class CommodityLocation
    {
        public static readonly CommodityLocation Instance = new CommodityLocation();

        internal readonly BackgroundWorker _loopWorker = new BackgroundWorker();

        private CommodityLocation()
        {
            _loopWorker.DoWork += LoopWorkerOnDoWork;
            _loopWorker.ProgressChanged += LoopWorkerOnProgressChanged;

            _loopWorker.WorkerSupportsCancellation = true;
            _loopWorker.WorkerReportsProgress = true;
        }

        private static void LoopWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {

            var worker = sender as BackgroundWorker;
            var args = doWorkEventArgs.Argument as LoopWorkerArgs;
            if (worker == null || args == null)
            {
                doWorkEventArgs.Cancel = true;
                return;
            }

            worker.ReportProgress(0, new LoopWorkerProgressArgs()
            {
                Text =
                    string.Format("[{3}] Starting Commodity Finding for {0} around '{1}' in a '{2} ly' bubble", args.Commodity, args.SystemName, args.Distance, DateTime.Now.ToLongTimeString())
            });

            var centralSystem = SystemDatabase.Connection.Table<StarSystem>().FirstOrDefault(x => x.Name.Equals(args.SystemName));
            if (centralSystem == null)
            {
                worker.ReportProgress(0, new LoopWorkerProgressArgs()
                {
                    Text = string.Format("Central System {0} Not Found", args.SystemName)
                });
                doWorkEventArgs.Cancel = true;
                return;
            }

            var temp = new List<HackStorage>();

            var stationCollection = centralSystem.GetStarSystemsCloserThan(Decimal.ToDouble(args.Distance));
            foreach (var starSystem in stationCollection)
            {
                foreach (var station in starSystem.Stations)
                {
                    foreach (var stationListing in station.Listings.Where(stationListing => stationListing.Commodity.Name == args.Commodity && stationListing.SellPrice > 0))
                    {
                        temp.Add(new HackStorage() {SellPrice = stationListing.SellPrice, 
                            Text = string.Format("{0}/{1} has commodity {2} for {3} per unit", starSystem.Name, station.Name, args.Commodity, stationListing.SellPrice)});
                    }
                }
            }

            
            temp.Sort();
            foreach (var hack in temp)
            {
                worker.ReportProgress(0, new LoopWorkerProgressArgs()
                {
                    Text = hack.Text
                });
            }
        }

        private class HackStorage : IComparable<HackStorage>
        {
            public int SellPrice;
            public String Text;

            public int CompareTo(HackStorage other)
            {
                return SellPrice.CompareTo(other.SellPrice);
            }
        }

        private void LoopWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            var active = Form.ActiveForm as Form1;
            if (active == null)
            {
                return;
            }
            if (progressChangedEventArgs.UserState is LoopWorkerProgressArgs)
            {
                var args = (LoopWorkerProgressArgs)progressChangedEventArgs.UserState;
                
                ((TextBox)active.tabControls.GetControl(1).Controls.Find("txtCmdLocResults", false)[0]).Text += args.Text + "\r\n";
            }
        }


        public bool btnFindCommodity_Click(object sender, EventArgs eventArgs)
        {
            if (_loopWorker.IsBusy) return false;

            var active = Form.ActiveForm as Form1;
            if (active == null)
            {
                return false;
            }

            active.stsMainProgress.Visible = false;
            active.stsMainStatus.Visible = true;
            _loopWorker.RunWorkerAsync(new LoopWorkerArgs()
            {
                Distance = ((NumericUpDown)active.tabControls.GetControl(1).Controls.Find("numCmdLocDistance", false)[0]).Value,
                SystemName = ((ComboBox)active.tabControls.GetControl(1).Controls.Find("cboCmdLocSystem", false)[0]).Text,
                Commodity = ((ComboBox)active.tabControls.GetControl(1).Controls.Find("cboCmdLocCommodity", false)[0]).Text,
            });

            return true;
        }

        private class LoopWorkerArgs
        {
            public string SystemName;
            public decimal Distance;
            public string Commodity;

        }
        private class LoopWorkerProgressArgs
        {
            public string Text;
        }
    }
}
