using System;

namespace Yutai.Api.Legend.Events
{
    public class SingleTargetEventArgs : EventArgs
    {
        public bool Handled { get; set; }
    }
}