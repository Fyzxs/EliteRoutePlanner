using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrettyThings.data;
using PrettyThings.data.model;
using PrettyThings.Menu;

namespace PrettyThings
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SystemDatabase.Initialization();
        }

        private void mniMainDebugShowSystemCount_Click(object sender, EventArgs e)
        {
            DebugItems.Instance.mniMainDebugShowSystemCount_Click(sender, e);
        }

        private void mniMainDebugShowStationCount_Click(object sender, EventArgs e)
        {
            DebugItems.Instance.mniMainDebugShowStationCount_Click(sender, e);
        }

        private void mniMainDebugShowPathCount_Click(object sender, EventArgs e)
        {
            DebugItems.Instance.mniMainDebugShowPathCount_Click(sender, e);
        }

        private void btnGenPath_Click(object sender, EventArgs e)
        {
            lock (this)
            {
                if (!btnGenPath.Enabled)
                {
                    return;
                }
                btnGenPath.Enabled = false;
            }
            btnGenPath.Enabled = !LoopGeneration.Instance.btnGenPath_Click(sender, e);
        }


        private void mniMainImportEddb_Click(object sender, EventArgs e)
        {
            EddbImport.Instance.mniMainImportEddb_Click(sender, e);
        }

        private void mniMainImportOcr_Click(object sender, EventArgs e)
        {
            OcrImport.Instance.mniMainImportOcr_Click(sender, e);
        }

        private void mniMainImportEddbCommodities_Click(object sender, EventArgs e)
        {
            EddbImport.Instance.mniMainImportEddbCommodities_Click(sender, e);
        }

        private void mniMainImportEddbStations_Click(object sender, EventArgs e)
        {
            EddbImport.Instance.mniMainImportEddbStations_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var x = SystemDatabase.Connection.DeferredQuery<Commodity>(textBox2.Text);
            

            foreach (var z in x)
            {
                textBox1.Text += "\r\n" + z;
            }


        }

        private void mniMainImportEddbInterwebs_Click(object sender, EventArgs e)
        {
            EddbImport.Instance.mniMainImportEddbInterwebs_Click(sender, e);
        }

        private void cancelLoopingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoopGeneration.Instance._loopWorker.CancelAsync();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemDatabase.Close();
            System.Windows.Forms.Application.Exit();
        }

    }
}
