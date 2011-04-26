using System.IO;
using BoxInformation.Interfaces;

namespace BoxInformation.Logging
{
    public class TextFileLogger:ILogger
    {
        private string logPath;

        public TextFileLogger(string logPath)
        {
            this.logPath = logPath;
        }

        public void Log(string message)
        {
            using (StreamWriter logWriter = new StreamWriter(logPath))
            {
                logWriter.AutoFlush = true;
                logWriter.WriteLine(message);
            }
        }
    }
}