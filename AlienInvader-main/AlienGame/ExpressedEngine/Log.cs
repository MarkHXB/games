using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienGame.Engine
{
    public class Log
    {
        public static void Info(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[INFO]   -   {msg}  -   {DateTime.Now.Minute}:{DateTime.Now.Second}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]   -   {msg}  -   {DateTime.Now.Minute}:{DateTime.Now.Second}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Warning(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARNING]   -   {msg}  -   {DateTime.Now.Minute}:{DateTime.Now.Second}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
