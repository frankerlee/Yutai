using System;

namespace Yutai.Plugins.Events
{
    public class PluginMessageEventArgs : EventArgs
    {
        public PluginMessageEventArgs(string message)
        {
            if (message == null) throw new ArgumentNullException("message");
            Message = message;
        }

        public string Message { get; private set; }
    }
}