using System;

namespace Yutai.Plugins.Events
{
    public class StringValueChangedEventArgs : EventArgs
    {
        public StringValueChangedEventArgs(string newValue)
        {
            NewValue = newValue;
        }

        public string NewValue { get; private set; }
    }
}