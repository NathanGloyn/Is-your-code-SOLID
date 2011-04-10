using System.Diagnostics;

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