using System;
using System.ComponentModel;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Scene.Helpers;

namespace Yutai.Plugins.Scene.Forms
{
	internal partial class frmViewSettings : System.Windows.Forms.Form
	{
		private partial class Class0
		{
			private object object_0 = null;

			public ISceneViewer SceneViewer
			{
				get
				{
					ISceneViewer result;
					if (this.object_0 is ISceneViewer)
					{
						result = (this.object_0 as ISceneViewer);
					}
					else if (this.object_0 is frmSecondaryViewer)
					{
						result = (this.object_0 as frmSecondaryViewer).SceneViewer;
					}
					else
					{
						result = null;
					}
					return result;
				}
			}

			public Class0(object object_1)
			{
				this.object_0 = object_1;
			}

			public override string ToString()
			{
				string result;
				if (this.object_0 is ISceneViewer)
				{
					result = "MainViewer";
				}
				else if (this.object_0 is frmSecondaryViewer)
				{
					result = (this.object_0 as frmSecondaryViewer).Text;
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		private IContainer icontainer_0 = null;



        

		private ISceneViewer isceneViewer_0 = null;

		private bool bool_0 = false;

		public ISceneViewer MainSceneViewer
		{
			get
			{
				return this.isceneViewer_0;
			}
			set
			{
				this.isceneViewer_0 = value;
			}
		}

	public frmViewSettings()
		{
			this.InitializeComponent();
			this.rdoProjectionType.SelectedIndexChanged+=(new EventHandler(this.rdoProjectionType_SelectedIndexChanged));
		}

		private void rdoProjectionType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				ISceneViewer sceneViewer = (this.cboViewers.SelectedItem as frmViewSettings.Class0).SceneViewer;
				sceneViewer.Camera.ProjectionType = this.rdoProjectionType.SelectedIndex + esri3DProjectionType.esriPerspectiveProjection;
				sceneViewer.Camera.Apply();
				sceneViewer.SceneGraph.RefreshViewers();
			}
		}

		private void txtTargetX_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void frmViewSettings_Load(object sender, EventArgs e)
		{
			this.cboViewers.Properties.Items.Add(new frmViewSettings.Class0(this.isceneViewer_0));
			for (int i = 0; i < ViewersManager.ViewerList.Count; i++)
			{
				this.cboViewers.Properties.Items.Add(new frmViewSettings.Class0(ViewersManager.ViewerList[i]));
			}
			this.cboViewers.SelectedIndex=(0);
			this.bool_0 = true;
		}

		private void cboViewers_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cboViewers.SelectedIndex != -1)
			{
				ISceneViewer sceneViewer = (this.cboViewers.SelectedItem as frmViewSettings.Class0).SceneViewer;
				IPoint point = sceneViewer.Camera.Observer;
				if (!point.IsEmpty)
				{
					this.txtObserverX.Text = point.X.ToString("0.###");
					this.txtObserverY.Text = point.Y.ToString("0.###");
					this.txtObserverZ.Text = point.Z.ToString("0.###");
				}
				point = sceneViewer.Camera.Target;
				this.rdoProjectionType.SelectedIndex=(sceneViewer.Camera.ProjectionType - esri3DProjectionType.esriPerspectiveProjection);
				if (!point.IsEmpty)
				{
					this.txtTargetX.Text = point.X.ToString("0.###");
					this.txtTargetY.Text = point.Y.ToString("0.###");
					this.txtTargetZ.Text = point.Z.ToString("0.###");
				}
				this.lblDistance.Text = sceneViewer.Camera.ViewingDistance.ToString("0");
				this.spinRollAngle.Value=((decimal)sceneViewer.Camera.RollAngle);
				this.spinViewFieldAngle.Value=((decimal)sceneViewer.Camera.ViewFieldAngle);
			}
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			ISceneViewer sceneViewer = (this.cboViewers.SelectedItem as frmViewSettings.Class0).SceneViewer;
			IPoint point = sceneViewer.Camera.Observer;
			if (!point.IsEmpty)
			{
				try
				{
					double num = double.Parse(this.txtObserverX.Text);
					point.X = num;
				}
				catch
				{
				}
				try
				{
					double num = double.Parse(this.txtObserverY.Text);
					point.Y = num;
				}
				catch
				{
				}
				try
				{
					double num = double.Parse(this.txtObserverZ.Text);
					point.Z = num;
				}
				catch
				{
				}
				sceneViewer.Camera.Observer = point;
				point = sceneViewer.Camera.Target;
				try
				{
					double num = double.Parse(this.txtTargetX.Text);
					point.X = num;
				}
				catch
				{
				}
				try
				{
					double num = double.Parse(this.txtTargetY.Text);
					point.Y = num;
				}
				catch
				{
				}
				try
				{
					double num = double.Parse(this.txtTargetZ.Text);
					point.Z = num;
				}
				catch
				{
				}
				sceneViewer.Camera.Target = point;
				sceneViewer.Camera.RollAngle = (double)this.spinRollAngle.Value;
				sceneViewer.Camera.ViewFieldAngle = (double)this.spinViewFieldAngle.Value;
				sceneViewer.Camera.Apply();
				sceneViewer.SceneGraph.RefreshViewers();
			}
		}
	}
}
