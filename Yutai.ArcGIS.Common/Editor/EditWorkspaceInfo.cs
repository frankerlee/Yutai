using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Editor
{
	public class EditWorkspaceInfo
	{
		private IWorkspace iworkspace_0 = null;

		private IArray iarray_0 = new Array();

		public IArray LayerArray
		{
			get
			{
				return this.iarray_0;
			}
		}

		public IWorkspace Workspace
		{
			get
			{
				return this.iworkspace_0;
			}
		}

		public EditWorkspaceInfo(IWorkspace iworkspace_1)
		{
			this.iworkspace_0 = iworkspace_1;
		}
	}
}