using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrettyThings
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Initialize();
            Application.Run(new Form1());
        }

        private static void Initialize()
        {
            try
            {
                File.Copy("lib/sqlite3.dll", "sqlite3.dll", false);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("sqlite3.dll Not found. [msg={0}]", ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Exception copying sqlite3.dll. [msg={0}]", ex.Message);
            }
        }
    }
}
