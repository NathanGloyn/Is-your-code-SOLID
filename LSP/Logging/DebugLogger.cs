using System.Diagnostics;
using BoxInformation.Interfaces;

namespace BoxInformation.Logging
{
    public class DebugLogger:ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}