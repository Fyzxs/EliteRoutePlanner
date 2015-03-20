using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteRoutePlanner.Log
{
    class MyLog
    {
        public static void E(string msgFormat, params object []args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msgFormat, args);
            Console.ResetColor();
        }
    }
}
