using System;
using System.ComponentModel;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.Plugins.Scene.Forms
{
	internal partial class frmSecondaryViewer : System.Windows.Forms.Form
	{
		private IContainer icontainer_0 = null;


		private IScenePlugin iapplication_0 = null;

		private IScene iscene_0 = null;

		private ITool itool_0 = null;

		public IScene MainScene
		{
			get
			{
				return this.iscene_0;
			}
			set
			{
				this.iscene_0 = value;
				(this.iscene_0 as ESRI.ArcGIS.Analyst3D.IActiveViewEvents_Event).ContentsCleared += method_3;
				(this.iscene_0 as ESRI.ArcGIS.Analyst3D.IActiveViewEvents_Event).ItemAdded += method_2;
				(this.iscene_0 as ESRI.ArcGIS.Analyst3D.IActiveViewEvents_Event).ItemDeleted += method_1;
			}
		}

		public IScenePlugin Application
		{
			set
			{
				this.iapplication_0 = value;
				if (this.iapplication_0 != null)
				{
				//	(this.iapplication_0 as IApplicationEvents).OnCurrentToolChanged += new OnCurrentToolChangedHandler(this.method_0);
				}
			}
		}

		public ISceneViewer SceneViewer
		{
			get
			{
				return this.axSceneControl1.SceneViewer;
			}
		}

		public ITool CurrentTool
		{
			set
			{
				this.itool_0 = value;
			}
		}

	private void method_0(object object_0)
		{
			try
			{
				if (this.iapplication_0.CurrentTool != null)
				{
					ICommand command = TypeDescriptor.CreateInstance(null, this.iapplication_0.CurrentTool.GetType(), null, null) as ICommand;
					command.OnCreate(this.axSceneControl1.Object);
					(this.axSceneControl1.Object as ISceneControlDefault).CurrentTool = (command as ITool);
				}
				else
				{
					(this.axSceneControl1.Object as ISceneControlDefault).CurrentTool = null;
				}
			}
			catch
			{
			}
		}

		private void method_1(object object_0)
		{
			if (object_0 is ILayer)
			{
				this.axSceneControl1.Scene.DeleteLayer(object_0 as ILayer);
			}
		}

		private void method_2(object object_0)
		{
			if (object_0 is ILayer)
			{
				this.axSceneControl1.Scene.AddLayer(object_0 as ILayer, false);
			}
		}

		private void method_3()
		{
			this.axSceneControl1.Scene.ClearLayers();
		}

		public frmSecondaryViewer()
		{
			this.InitializeComponent();
		}

		private void frmSecondaryViewer_Load(object sender, EventArgs e)
		{
			this.axSceneControl1.SceneViewer.Caption = this.Text;
			if (this.iscene_0 != null)
			{
				for (int i = this.iscene_0.LayerCount - 1; i >= 0; i--)
				{
					this.axSceneControl1.Scene.AddLayer(this.iscene_0.get_Layer(i), false);
				}
				if (this.itool_0 != null)
				{
					ICommand command = TypeDescriptor.CreateInstance(null, this.itool_0.GetType(), null, null) as ICommand;
					command.OnCreate(this.axSceneControl1.Object);
					(this.axSceneControl1.Object as ISceneControlDefault).CurrentTool = (command as ITool);
				}
				else
				{
					(this.axSceneControl1.Object as ISceneControlDefault).CurrentTool = null;
				}
			}
		}
	}
}
