using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using Yutai.Services.Helpers;
using Yutai.Shared;
using Yutai.Shared.Log;

namespace Yutai.Services.Concrete
{
    public class LogEntry : ILogEntry
    {
        private static int _newIndex = 0;

        public LogEntry(string msg, LogLevel level)
        {
            Index = Interlocked.Increment(ref _newIndex);
            msg = msg.TrimEnd(Environment.NewLine.ToCharArray());
            Message = msg;
            Level = level;
        }

        public LogEntry(string msg, LogLevel level, Exception ex)
            : this(msg, level)
        {
            Exception = ex;
            TimeStamp = DateTime.Now;
        }

        [DisplayName(" ")]
        public Bitmap Image { get; set; }

        [DisplayName(" ")]
        public int Index { get; set; }

        [Browsable(false)]
        public bool Displayed { get; set; }

        public LogLevel Level { get; set; }

        [Browsable(false)]
        public string Message { get; private set; }

        [Browsable(false)]
        public Exception Exception { get; private set; }

        [DisplayName("Time")]
        public DateTime TimeStamp { get; private set; }

        public string ToLine()
        {
            return string.Format("{0}: {1}" + Environment.NewLine, Level, Message);
        }

        [DisplayName("Message")]
        public string DetailedMessage
        {
            get { return GetCompleteDescription(false); }
        }

        public string GetCompleteDescription(bool innerExceptions)
        {
            string s = Message;

            if (Exception != null)
            {
                if (innerExceptions)
                {
                    s += Environment.NewLine + Environment.NewLine + Exception.ExceptionToLogString();
                }
                else
                {
                    s += Environment.NewLine + Exception.Message + Environment.NewLine + Exception.StackTrace;
                }
            }

            return s;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Level.ToString().ToUpper(), Message);
        }
    }
}