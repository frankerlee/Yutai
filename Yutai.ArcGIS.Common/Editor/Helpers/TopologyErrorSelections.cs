using System.Collections;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
	public class TopologyErrorSelections
	{
		private IActiveView iactiveView_0 = null;

		private IList ilist_0 = new ArrayList();

		public IActiveView ActiveView
		{
			set
			{
				this.iactiveView_0 = value;
				(this.iactiveView_0 as IActiveViewEvents_Event).AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(this.method_0);
			}
		}

		public int Count
		{
			get
			{
				return this.ilist_0.Count;
			}
		}

		public TopologyError this[int int_0]
		{
			get
			{
				return this.ilist_0[int_0] as TopologyError;
			}
		}

		public TopologyErrorSelections()
		{
		}

		public void Add(TopologyError topologyError_0)
		{
			this.ilist_0.Add(topologyError_0);
		}

		public void Clear()
		{
			this.ilist_0.Clear();
		}

		private void method_0(IDisplay idisplay_0, esriViewDrawPhase esriViewDrawPhase_0)
		{
			for (int i = 0; i < this.ilist_0.Count; i++)
			{
				(this.ilist_0[i] as TopologyError).Draw(idisplay_0 as IScreenDisplay);
			}
		}

		public void Remove(ITopologyErrorFeature itopologyErrorFeature_0)
		{
			int num = 0;
			while (true)
			{
				if (num >= this.ilist_0.Count)
				{
					break;
				}
				else if ((this.ilist_0[num] as TopologyError).TopologyErrorFeature == itopologyErrorFeature_0)
				{
					this.ilist_0.RemoveAt(num);
					break;
				}
				else
				{
					num++;
				}
			}
		}
	}
}