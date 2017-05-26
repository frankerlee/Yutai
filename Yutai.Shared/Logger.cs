using Yutai.Shared.Log;

namespace Yutai.Shared
{
    public class Logger
    {
        private static ILoggingService _logger;

        public static ILoggingService Current
        {
            get { return _logger; }
            internal set { _logger = value; }
        }
    }
}
