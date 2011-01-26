using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace BoxInformation.Logging
{
    public class TextFileLogger:ILogger
    {
        private StreamWriter logWriter;

        public TextFileLogger(string logPath)
        {
            logWriter = new StreamWriter(logPath);
            logWriter.AutoFlush = true;
        }

        public void Log(string message)
        {
            logWriter.WriteLine(message);
        }
    }
}