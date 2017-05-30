using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Yutai.Catalog
{
	internal class State
	{
		public ManualResetEvent ManualEvent
		{
			get;
			set;
		}

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
	}
}