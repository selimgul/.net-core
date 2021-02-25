using System.Diagnostics;

namespace DotnetCore.Services
{
    public class Logger : ILogger
    {
        public void Log(string msg)
        {
            Debug.WriteLine(msg);
        }
    }
}
