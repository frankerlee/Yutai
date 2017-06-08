namespace JLK.Catalog
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class State
    {
        [CompilerGenerated]
        private ManualResetEvent manualResetEvent_0;

        public State(ManualResetEvent manualResetEvent_1)
        {
            this.ManualEvent = manualResetEvent_1;
        }

        public void Reset()
        {
            if (this.ManualEvent != null)
            {
                this.ManualEvent.Reset();
            }
        }

        public void Set()
        {
            if (this.ManualEvent != null)
            {
                this.ManualEvent.Set();
            }
        }

        public ManualResetEvent ManualEvent
        {
            [CompilerGenerated]
            get
            {
                return this.manualResetEvent_0;
            }
            [CompilerGenerated]
            set
            {
                this.manualResetEvent_0 = value;
            }
        }
    }
}

