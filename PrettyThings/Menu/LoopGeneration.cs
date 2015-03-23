using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrettyThings.data;
using PrettyThings.data.model;
using PrettyThings.data.planning;

namespace PrettyThings.Menu
{
    internal class LoopGeneration
    {
        public static readonly LoopGeneration Instance = new LoopGeneration();

        internal readonly BackgroundWorker _loopWorker = new BackgroundWorker();

        public BlockingCollection<List<Loop>> CollectionLoops = new BlockingCollection<List<Loop>>();

        private LoopGeneration()
        {
            _loopWorker.DoWork += LoopWorkerOnDoWork;
            _loopWorker.ProgressChanged += LoopWorkerOnProgressChanged;
            _loopWorker.RunWorkerCompleted += LoopWorkerOnRunWorkerCompleted;

            _loopWorker.WorkerSupportsCancellation = true;
            _loopWorker.WorkerReportsProgress = true;
        }

        private void LoopWorkerOnRunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {

            var active = Form.ActiveForm as Form1;
            if (active == null)
            {
                return;
            }

            active.btnGenPath.Enabled = true;
            active.textBox1.Text += "\r\n" + "[" + DateTime.Now.ToLongTimeString() + "] Path finding Complete";
            active.stsMainStatus.Text = "";
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
                if (args.doTextBox)
                {
                    active.textBox1.Text += "\r\n" + args.Text;
                }
                else
                {
                    active.stsMainStatus.Text = args.Text;
                }
            }
           
        }
        private class Item
        {
            public Loop.AddHopResponse Response;
            public int Value;
            public Item(Loop.AddHopResponse response)
            {
                Response = response;
            }
            public override string ToString()
            {
                return Response.ToString() + " :: " + Value;
            }
        }

        private class LoopWorkerArgs
        {
            public string SystemName;
            public double Distance;
        }

        private class LoopWorkerProgressArgs
        {
            public bool doTextBox;
            public string Text;
        }

        private class LoopWorkerCounterArgs
        {
            public Loop.AddHopResponse Response;
        }

        private void LoopWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
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
                doTextBox =  true,
                Text =
                    string.Format("[{2}] Starting Path Finding for '{0}' in a '{1} ly' bubble", args.SystemName, args.Distance, DateTime.Now.ToLongTimeString())
            });

            var centralSystem = SystemDatabase.Connection.Table<StarSystem>().FirstOrDefault(x => x.Name.Equals(args.SystemName));
            if (centralSystem == null)
            {
                worker.ReportProgress(0, new LoopWorkerProgressArgs()
                {doTextBox =  true,
                    Text = string.Format("Central System {0} Not Found", args.SystemName)
                });
                doWorkEventArgs.Cancel = true;
                return;
            }
            var stationCollection = centralSystem.GetProfitableStationsWithin(args.Distance);
            stationCollection.Sort();
            stationCollection.Reverse();
            worker.ReportProgress(0, new LoopWorkerProgressArgs()
            {doTextBox =  true,
                Text = string.Format("Found {0} stations with profitable hops", stationCollection.Count)
            });

#if DEBUG
            for(int i = 0; i < stationCollection.Count; i++)
#else
            Parallel.For((int) 0, (int)(stationCollection.Count - 1), i =>
#endif
            {
                var localLoops = new List<Loop>(200);
                if (worker.CancellationPending)
                {
                    doWorkEventArgs.Cancel = true;
                    return;
                }

                var success = false;
                do
                {
                    var loop = new Loop();
                    loop.AddHop(stationCollection[i], localLoops);
                    success = FillLoop(loop, stationCollection.GetRange(i + 1, stationCollection.Count - (i + 1)), localLoops, worker);
                    
                    worker.ReportProgress(0, new LoopWorkerProgressArgs()
                    {
                        doTextBox =  false,
                        Text = string.Format("Found {0,7} paths running thread [{1,3}] [sucess={2,5}] [{3,2}]", localLoops.Count, Thread.CurrentThread.ManagedThreadId, success, i)
                    });
                    if (worker.CancellationPending)
                    {
                        doWorkEventArgs.Cancel = true;
                        success = false;
                    }
                } while (success);

                Instance.CollectionLoops.Add(localLoops);
#if DEBUG
            }
#else
            });
#endif
            
            var allLoops = new List<Loop>();
            foreach (var collectionLoop in CollectionLoops)
            {
                allLoops.AddRange(collectionLoop);
            }
            allLoops.Sort();
            allLoops.Reverse();

            worker.ReportProgress(0, new LoopWorkerProgressArgs()
            {
                doTextBox = true,
                Text = string.Format("Found {0} paths", allLoops.Count)
            });

            var sortedFile = new StreamWriter("sortedFile.tsv", false);
            foreach (var loop in allLoops)
            {
                sortedFile.WriteLine(loop.ToTsv());
            }

            sortedFile.Close();

        }


        static bool FillLoop(Loop loop, IReadOnlyList<StationToStationProfit> profits, List<Loop> localLoops, BackgroundWorker worker )
        {
            for (var index = 0; index < profits.Count; index++)
            {
                var response = loop.AddHop(profits[index], localLoops);
                switch (response)
                {
                    case Loop.AddHopResponse.Invalid:
                    case Loop.AddHopResponse.MatchesExistingLoop:
                    case Loop.AddHopResponse.StationExists:
                        break;
                    case Loop.AddHopResponse.TooLong:
                        //localLoops.Add(loop);
                        return false;
                    case Loop.AddHopResponse.LoopComplete:
                        localLoops.Add(loop);
#if DEBUG
                         worker.ReportProgress(0, new LoopWorkerProgressArgs()
                        {
                            doTextBox =  true,
                            Text = string.Format("Found path running thread [{0,3}] [{1}]",Thread.CurrentThread.ManagedThreadId, loop.ToString())
                        });
#endif
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

        internal bool btnGenPath_Click(object sender, EventArgs e)
        {
            if (_loopWorker.IsBusy) return false;

            var active = Form.ActiveForm as Form1;
            if (active == null)
            {
                return false;
            }
            
            active.stsMainProgress.Visible = false;
            active.stsMainStatus.Visible = true;

            _loopWorker.RunWorkerAsync(new LoopWorkerArgs(){Distance = Convert.ToDouble(active.numDistance.Value), SystemName = active.txtStartingSystem.Text});

            return true;
        }
    }
}