using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace os
{
    public static class WriteMessage
    {
        public static void writeError(string error) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(error);
        }
        public static void writeInfo(string info)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("[Info]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(info);
        }
        public static void writeOK(string ok)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[OK]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ok);
        }
    }
}
