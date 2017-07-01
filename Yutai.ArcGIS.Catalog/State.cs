using System.Runtime.CompilerServices;
using System.Threading;

namespace Yutai.ArcGIS.Catalog
{
    internal class State
    {
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

        public ManualResetEvent ManualEvent { get; set; }
    }
}