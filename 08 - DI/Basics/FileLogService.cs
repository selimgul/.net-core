using System;
using System.Reflection.Emit;

namespace Basics
{
    public class FileLogService : ILog
    {
        public void info(String msg){
            Console.WriteLine($"FileLogService: {msg}");
        }
    }
}