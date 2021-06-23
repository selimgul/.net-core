using System;
using System.Reflection.Emit;

namespace Basics
{
    public class ConsoleLogService : ILog
    {
        public void info(String msg){
            Console.WriteLine($"ConsoleLogService: {msg}");
        }
    }
}