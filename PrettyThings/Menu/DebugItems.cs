using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrettyThings;
using PrettyThings.data;
using PrettyThings.data.model;
using PrettyThings.Menu;

namespace PrettyThings.Menu
{
    internal class DebugItems
    {
        internal static DebugItems Instance = new DebugItems();

        private DebugItems()
        {
            
        }

        internal void mniMainDebugShowSystemCount_Click(object sender, EventArgs e)
        {
            var form = Form.ActiveForm as Form1;
            if (form == null) return;
            form.textBox1.Text += "\r\nSystem Count: " + SystemDatabase.Connection.Table<StarSystem>().Count();
        }

        internal void mniMainDebugShowStationCount_Click(object sender, EventArgs e)
        {
            var form = Form.ActiveForm as Form1;
            if (form == null) return;

            form.textBox1.Text += "\r\nStation Count: " + SystemDatabase.Connection.Table<Station>().Count();
        }

        internal void mniMainDebugShowPathCount_Click(object sender, EventArgs eventArgs)
        {

            var form = Form.ActiveForm as Form1;
            if (form == null) return;

            form.textBox1.Text += "\r\nPath Count: " + "DON'T HAVE";
        }
    }
}
