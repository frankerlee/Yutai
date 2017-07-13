using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Scene.Commands.View;
using Yutai.Plugins.Scene.Helpers;

namespace Yutai.Plugins.Scene.Forms
{
	internal partial class frmFlyByProps : System.Windows.Forms.Form
	{
		private const double dMaxRollAngle = 5.0;

		private const double dTurnStep = 5.0;
        private IScenePlugin _plugin;
        private ISceneGraph isceneGraph_0;
        private ISceneViewer isceneViewer_0;
        private ICamera icamera_0;
        private IGraphicsContainer3D igraphicsContainer3D_0;
        private IPointCollection ipointCollection_0;
        private IPointCollection ipointCollection_1;
        private ISurface isurface_0;
        private IPoint ipoint_0;
        private IMultiPatch imultiPatch_0;
        private SortedList sortedList_0;
        private bool bool_0;
        private bool bool_1;
        private double double_0;
        private short short_0;
        private string string_0;
        private string string_1;
        private double double_1;
        private double double_2;
        private int int_0;
        private int int_1;
        private bool bool_2;
        private FlyByUtils.FlyByDirection flyByDirection_0;
        private FlyByUtils.FlyByLoop flyByLoop_0;

        private IAppContext _context;
      






        public IScenePlugin Application
		{
			set
			{
				this._plugin = value;
				if (this._plugin != null)
				{
					this.isceneGraph_0 = this._plugin.SceneGraph;
					this.isceneViewer_0 = this.isceneGraph_0.ActiveViewer;
					this.icamera_0 = this.isceneViewer_0.Camera;
					this.isceneGraph_0.IsRecordingMessageEnabled = false;
				}
			}
		}

		public IPoint StaticLoc
		{
			set
			{
				this.ipoint_0 = value;
				this.method_5();
			}
		}


	    public frmFlyByProps()
	    {
            this.InitializeComponent();
            this.bool_0 = false;
        }
        public frmFlyByProps(IAppContext context,IScenePlugin plugin)
		{
			this.InitializeComponent();
			this.bool_0 = false;
		    _context = context;
		    _plugin = plugin;
		}

	private void cboOffsetType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				this.UpdatePathShape();
			}
		}

		private void chkOffset_CheckStateChanged(object sender, EventArgs e)
		{
			bool @checked = this.chkOffset.Checked;
			this.sldTarOffset.Enabled = @checked;
			this.lblTarOffset.Enabled = @checked;
			this.cboMaxOffset.Enabled = @checked;
			this.lblMaxOffset.Enabled = @checked;
			this.cboOffsetType.Enabled = @checked;
			this.UpdatePathShape();
		}

		public void Init(IPointCollection ipointCollection_2)
		{
			this.ipointCollection_0 = ((ipointCollection_2 as IClone).Clone() as IPointCollection);
			Utils3D.MakeZMAware(this.ipointCollection_0 as IGeometry, true);
			this.ipointCollection_1 = ((ipointCollection_2 as IClone).Clone() as IPointCollection);
			Utils3D.MakeZMAware(this.ipointCollection_1 as IGeometry, true);
			this.bool_1 = false;
		}

		private void frmFlyByProps_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			System.Windows.Forms.Keys keyCode = e.KeyCode;
			bool arg_0D_0 = e.Shift;
			if (keyCode == System.Windows.Forms.Keys.Escape && this.tmrAnimation.Enabled)
			{
				this.method_21();
			}
		}

		private void frmFlyByProps_Load(object sender, EventArgs e)
		{
			this.tmrAnimation.Interval = 1000;
			this.tmrAnimation.Enabled = false;
			this.sldFrameDelay.Value = 1;
			this.lblFrameDelay.Text = "画面延迟: 1.0 秒";
			if (this._plugin != null)
			{
				this.isurface_0 = SurfaceInfo.GetSurfaceFromLayer(this._plugin.CurrentLayer);
				SurfaceInfo.GetCurrentSurfaceMax(this._plugin.CurrentLayer, out this.double_1, out this.double_2);
				if (this.double_1 < 3.0)
				{
					this.string_0 = "0.000000";
				}
				else
				{
					this.string_0 = "###0.00";
				}
				this.cboOffsetType.Items.Clear();
				this.cboOffsetType.Items.Add("相对于表面");
				this.cboOffsetType.Items.Add("相对于锚定点集");
				this.cboOffsetType.Items.Add("固定高度");
				this.cboOffsetType.Text = "固定高度";
				this.cboRollFactor.Items.Clear();
				this.cboRollFactor.Items.Add("<无>");
				this.cboRollFactor.Items.Add("1.00");
				this.cboRollFactor.Items.Add("1.25");
				this.cboRollFactor.Items.Add("1.50");
				this.cboRollFactor.Items.Add("1.75");
				this.cboRollFactor.Items.Add("2.00");
				this.cboRollFactor.Text = "1.00";
				this.cboOutputImgsType.Items.Clear();
				this.cboOutputImgsType.Items.Add("JPG");
				this.cboOutputImgsType.Items.Add("BMP");
				this.cboOutputImgsType.Text = "BMP";
				this.cboOutputImgsEvery.Items.Clear();
				this.cboOutputImgsEvery.Items.Add("<无>");
				this.cboOutputImgsEvery.Items.Add("1个间隔");
				this.cboOutputImgsEvery.Items.Add("2个间隔");
				this.cboOutputImgsEvery.Items.Add("3个间隔");
				this.cboOutputImgsEvery.Items.Add("4个间隔");
				this.cboOutputImgsEvery.Items.Add("5个间隔");
				this.cboOutputImgsEvery.Text = "<无>";
				this.cboStepSize.Items.Clear();
				this.cboStepSize.Items.Add(this.double_1 / 50.0);
				this.cboStepSize.Items.Add(this.double_1 / 75.0);
				this.cboStepSize.Items.Add(this.double_1 / 100.0);
				this.cboStepSize.Items.Add(this.double_1 / 150.0);
				this.cboStepSize.Items.Add(this.double_1 / 300.0);
				this.cboStepSize.SelectedIndex = 3;
				if (this.double_2 < 3.0)
				{
					this.string_1 = "0.000000";
				}
				else
				{
					this.string_1 = "######0.00";
				}
				this.cboMaxOffset.Items.Clear();
				this.cboMaxOffset.Items.Add(this.double_2 / 5.0);
				this.cboMaxOffset.Items.Add(this.double_2 / 3.0);
				this.cboMaxOffset.Items.Add(this.double_2);
				this.cboMaxOffset.Items.Add(this.double_2 * 3.0);
				this.cboMaxOffset.Items.Add(this.double_2 * 5.0);
				this.cboMaxOffset.SelectedIndex = 3;
				this.sldTarOffset.Maximum = (int)this.double_2 * 3;
				this.sldTarOffset.TickFrequency = this.sldTarOffset.Maximum / 10;
				this.sldTarOffset.Value = (int)this.double_2;
				this.lblTarOffset.Text = "竖直偏移：" + this.sldTarOffset.Value.ToString(this.string_1);
				this.sldInclination.Value = 5;
				this.lblInclinationR.Text = "5";
				this.cboPointStep.Text = "2";
				this.optNo.Checked = true;
				this.flyByLoop_0 = FlyByUtils.FlyByLoop.FLYBY_LOOP_NO;
				this.optStaticTarget.Checked = true;
				this.chkShowPath.Checked = true;
				this.chkSmooth.Checked = true;
				this.chkOffset.Checked = true;
				this.UpdatePathShape();
				this.int_0 = 0;
				this.int_1 = this.ipointCollection_0.PointCount - 1;
				this.Refresh();
				this.txtOutputImgsPath.Text = System.Windows.Forms.Application.StartupPath + "\\";
				this.txtOutputImgsPrefix.Text = DateTime.Today.ToString("mmmdd_");
				this.sortedList_0 = new SortedList();
				this.optForward.Checked = true;
				this.flyByDirection_0 = FlyByUtils.FlyByDirection.FLYBY_DIR_FORWARD;
				this.short_0 = 2;
				IGraphicsSelection graphicsSelection = this._plugin.Scene.BasicGraphicsLayer as IGraphicsSelection;
				graphicsSelection.UnselectAllElements();
				this.igraphicsContainer3D_0 = Utils3D.Add3DGraphicsLayer("Background", this._plugin.SceneGraph);
				this.isceneGraph_0.SetOwnerFaceCulling(this.igraphicsContainer3D_0, esri3DFaceCulling.esriFaceCullingBack);
				this.isceneGraph_0.SetOwnerLightingOption(this.igraphicsContainer3D_0, false);
				this.bool_0 = true;
			}
		}

		private void frmFlyByProps_Closing(object sender, CancelEventArgs e)
		{
			bool arg_06_0 = e.Cancel;
			if (this.tmrAnimation.Enabled)
			{
				this.method_21();
			}
			e.Cancel = false;
		}

		private void frmFlyByProps_Closed(object sender, EventArgs e)
		{
			this.method_21();
			if (this._plugin != null)
			{
				FlyByUtils.DeleteFlyByElement(this._plugin.SceneGraph, FlyByUtils.FlyByElementType.FLYBY_ALL, true);
			}
			if (this.igraphicsContainer3D_0 != null)
			{
				this.igraphicsContainer3D_0.DeleteAllElements();
				if (this.isceneGraph_0 != null)
				{
					this.isceneGraph_0.Scene.DeleteLayer(this.igraphicsContainer3D_0 as ILayer);
					this.isceneGraph_0.IsRecordingMessageEnabled = true;
				}
			}
			FlyByUtils.bStillDrawing = true;
			FlyByUtils.bDrawStatic = false;
			this.ipoint_0 = null;
			this.bool_0 = false;
		}

		private void cboStepSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				this.method_21();
				this.UpdatePathShape();
			}
		}

		private void cboStepSize_TextChanged(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				this.method_21();
				this.UpdatePathShape();
			}
		}

		private void cboPointStep_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.method_5();
		}

		private void cboMaxOffset_Leave(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				this.method_0();
			}
		}

		private void cboMaxOffset_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			System.Windows.Forms.Keys keyCode = e.KeyCode;
			bool arg_0D_0 = e.Shift;
			if (this.bool_0 && keyCode == System.Windows.Forms.Keys.Return)
			{
				this.method_0();
			}
		}

		private void cboMaxOffset_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				this.method_0();
			}
		}

		private void method_0()
		{
			if (this.bool_0)
			{
				double num;
				try
				{
					num = double.Parse(this.cboMaxOffset.Text);
				}
				catch
				{
					num = 0.0;
				}
				this.sldTarOffset.Maximum = (int)num;
				this.sldTarOffset.TickFrequency = this.sldTarOffset.Maximum / 10;
				this.sldTarOffset.Refresh();
				this.lblTarOffset.Text = "竖直偏移: " + this.sldTarOffset.Value.ToString(this.string_1);
				this.lblTarOffset.Refresh();
			}
		}

		private void chkSmooth_CheckStateChanged(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				this.method_21();
				this.UpdatePathShape();
			}
		}

		public void UpdatePathShape()
		{
			try
			{
				this.double_0 = Convert.ToDouble(this.cboStepSize.Text);
				this.cboStepSize.SelectedIndex = this.cboStepSize.Items.Count - 1;
			}
			catch
			{
			}
			IPolyline polyline = (this.ipointCollection_1 as IClone).Clone() as IPolyline;
			Utils3D.MakeZMAware(polyline, true);
			if (this.chkSmooth.Checked)
			{
				polyline.Smooth(0.0);
			}
			polyline.Densify(this.double_0, 0.0);
			object obj = this.double_0;
			if (this.chkOffset.Checked)
			{
				double num = Convert.ToDouble(this.sldTarOffset.Value);
				string text = this.cboOffsetType.Text;
				if (text != null)
				{
					if (!(text == "相对于表面"))
					{
						if (!(text == "相对于锚定点集"))
						{
							if (text == "固定高度")
							{
								Utils3D.MakeConstantZ(polyline, num);
							}
						}
						else
						{
							Utils3D.MakeOffsetZ(polyline, num);
						}
					}
					else
					{
						this.isurface_0 = SurfaceInfo.GetSurfaceFromLayer(this._plugin.CurrentLayer);
						IGeometry geometry;
						this.isurface_0.InterpolateShape(polyline, out geometry, ref obj);
						polyline = (geometry as IPolyline);
						Utils3D.MakeOffsetZ(polyline, num);
					}
				}
			}
			this.ipointCollection_0 = ((polyline as IClone).Clone() as IPointCollection);
			Utils3D.MakeZMAware(this.ipointCollection_0 as IGeometry, true);
			this.int_1 = this.ipointCollection_0.PointCount - 1;
			this.method_1();
			if (this.chkShowPath.CheckState == System.Windows.Forms.CheckState.Checked)
			{
				FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, this.ipointCollection_0 as IGeometry, FlyByUtils.FlyByElementType.FLYBY_PATH, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, true);
			}
		}

		private void method_1()
		{
			int selectedIndex = this.lstPoints.SelectedIndex;
			this.lstPoints.Items.Clear();
			IEnumVertex enumVertices = this.ipointCollection_0.EnumVertices;
			enumVertices.Reset();
			IPoint point;
			int num;
			int num2;
			enumVertices.Next(out point, out num, out num2);
			while (point != null)
			{
				string str = string.Concat(new string[]
				{
					point.X.ToString(this.string_0),
					", ",
					point.Y.ToString(this.string_0),
					", ",
					point.Z.ToString(this.string_0)
				});
				this.lstPoints.Items.Add(num.ToString("00") + num2.ToString("-000") + ": " + str);
				enumVertices.Next(out point, out num, out num2);
			}
			this.lblPntCount.Text = "点数：" + this.lstPoints.Items.Count.ToString();
			if (selectedIndex < this.lstPoints.Items.Count)
			{
				this.lstPoints.SelectedIndex = selectedIndex;
			}
			this.chkShowPath_CheckStateChanged(this.chkShowPath, new EventArgs());
		}

		private void chkStatic_CheckStateChanged(object sender, EventArgs e)
		{
			if (this.chkStatic.CheckState == System.Windows.Forms.CheckState.Checked)
			{
				FlyByUtils.bDrawStatic = true;
				if (this.optStaticTarget.Checked)
				{
					FlyByUtils.DeleteFlyByElement(this._plugin.SceneGraph, FlyByUtils.FlyByElementType.FLYBY_TARGET, true);
				}
				else
				{
					FlyByUtils.DeleteFlyByElement(this._plugin.SceneGraph, FlyByUtils.FlyByElementType.FLYBY_OBSERVER, true);
				}
				if (this.ipoint_0 == null)
				{
					ICommand command = new ToolSceneDrawStaticLoc(_context,_plugin);
					this._plugin.SetCurrentTool (command as ITool);
					command.OnCreate(this._plugin.Hook);
					command.OnClick();
				}
			}
			else
			{
				FlyByUtils.bDrawStatic = false;
				FlyByUtils.DeleteFlyByElement(this._plugin.SceneGraph, FlyByUtils.FlyByElementType.FLYBY_STATIC, false);
			}
			this.method_5();
		}

		private void chkShowPath_CheckStateChanged(object sender, EventArgs e)
		{
		}

		private void method_2(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			System.Windows.Forms.Keys keyCode = e.KeyCode;
			bool arg_0D_0 = e.Shift;
			if (keyCode == System.Windows.Forms.Keys.Escape && this.tmrAnimation.Enabled)
			{
				this.method_21();
			}
		}

		private void cmdAnim_Click(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				if (this.tmrAnimation.Enabled)
				{
					this.method_21();
					this.isceneGraph_0.IsRecordingMessageEnabled = true;
				}
				else
				{
					this.isceneGraph_0.IsRecordingMessageEnabled = false;
					this.cmdDone.Enabled = false;
					this.cmdPlayImages.Enabled = false;
					this.cmdDeleteImages.Enabled = false;
					this.cmdBrowse.Enabled = false;
					this.cmdAnim.Text = "停止飞行";
					this.cmdAnim.Refresh();
					if (this.optNo.Checked)
					{
						this.flyByLoop_0 = FlyByUtils.FlyByLoop.FLYBY_LOOP_NO;
					}
					else if (this.optReturn.Checked)
					{
						this.flyByLoop_0 = FlyByUtils.FlyByLoop.FLYBY_LOOP_ONCE;
					}
					else
					{
						this.flyByLoop_0 = FlyByUtils.FlyByLoop.FLYBY_LOOP_CONTINUOUS;
					}
					this.tmrAnimation.Enabled = true;
				}
			}
		}

		private void method_3()
		{
			if (this.bool_0)
			{
				IMessageDispatcher messageDispatcher = new MessageDispatcher();
				messageDispatcher.CancelOnEscPress = true;
				this.bool_2 = false;
				this.icamera_0.RollAngle = 0.0;
				short num = 1;
				while ((double)num <= 36.0)
				{
					this.icamera_0.Rotate(5.0);
					Thread.Sleep(this.sldFrameDelay.Value * 100);
					this.isceneViewer_0.Redraw(true);
					this.method_25();
					object obj;
					messageDispatcher.Dispatch(base.Handle.ToInt32(), false, out obj);
					if (this.bool_2)
					{
						num = 36;
					}
					messageDispatcher.Dispatch((this._plugin.Hook as ISceneControlDefault).hWnd, false, out obj);
					if (this.bool_2)
					{
						num = 36;
					}
					System.Windows.Forms.Application.DoEvents();
					num += 1;
				}
			}
		}

		private void method_4()
		{
			if (this.bool_0)
			{
				if (!this.chkStatic.Checked && this.lstPoints.SelectedIndex != -1)
				{
					if (this.tmrAnimation.Enabled)
					{
						this.tmrAnimation.Enabled = false;
						this.method_3();
						this.tmrAnimation.Enabled = true;
					}
					else
					{
						this.method_3();
					}
				}
				this.bool_1 = true;
				switch (this.flyByDirection_0)
				{
				case FlyByUtils.FlyByDirection.FLYBY_DIR_BACKWARDS:
					this.flyByDirection_0 = FlyByUtils.FlyByDirection.FLYBY_DIR_FORWARD;
					this.optForward.Checked = true;
					break;
				case FlyByUtils.FlyByDirection.FLYBY_DIR_FORWARD:
					this.flyByDirection_0 = FlyByUtils.FlyByDirection.FLYBY_DIR_BACKWARDS;
					this.optBackward.Checked = true;
					break;
				}
				this.bool_1 = false;
				this.lstPoints.SelectedIndex = this.method_7();
				this.lstPoints.Refresh();
			}
		}

		private void method_5()
		{
			if (this.bool_0 && this.method_9())
			{
				if (this.chkStatic.Checked && this.ipoint_0 != null)
				{
					IPoint point;
					IPoint point2;
					if (this.optStaticTarget.Checked)
					{
						point = this.ipoint_0;
						point2 = this.ipointCollection_0.get_Point(this.int_0);
					}
					else
					{
						point = this.ipointCollection_0.get_Point(this.method_6());
						point2 = this.ipoint_0;
					}
					point.Z *= this.isceneGraph_0.Scene.ExaggerationFactor;
					this.icamera_0.Target = point;
					point2.Z *= this.isceneGraph_0.Scene.ExaggerationFactor;
					this.icamera_0.Observer = point2;
					this.icamera_0.RollAngle = 0.0;
				}
				else
				{
					IPoint point = this.ipointCollection_0.get_Point(this.method_6());
					point.Z *= this.isceneGraph_0.Scene.ExaggerationFactor;
					this.icamera_0.Target = point;
					IPoint point2 = this.ipointCollection_0.get_Point(this.int_0);
					point2.Z *= this.isceneGraph_0.Scene.ExaggerationFactor;
					this.icamera_0.Observer = point2;
					this.icamera_0.RollAngle = this.method_12();
					if (this.sldInclination.Value > 0)
					{
						this.icamera_0.PropertiesChanged();
						this.icamera_0.Inclination = (double)this.sldInclination.Value;
					}
				}
				this.lblAzimuthR.Text = this.icamera_0.Azimuth.ToString(" " + this.string_0);
				this.lblRollAngleR.Text = this.icamera_0.RollAngle.ToString(" " + this.string_0);
				if (this.chkShowPath.CheckState == System.Windows.Forms.CheckState.Checked)
				{
					FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, this.icamera_0.Observer, FlyByUtils.FlyByElementType.FLYBY_OBSERVER, System.Drawing.Color.Red, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Red, false);
					FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, this.icamera_0.Target, FlyByUtils.FlyByElementType.FLYBY_TARGET, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, false);
				}
				this.isceneGraph_0.RefreshViewers();
			}
		}

		private int method_6()
		{
			double num = 0.0;
			try
			{
				num = double.Parse(this.cboPointStep.Text);
			}
			catch
			{
				this.cboPointStep.Text = "2";
				num = 2.0;
			}
			int num2 = this.int_0 + (int)(num * (double)this.flyByDirection_0);
			switch (this.flyByDirection_0)
			{
			case FlyByUtils.FlyByDirection.FLYBY_DIR_BACKWARDS:
				if (num2 < 0)
				{
					num2 = 0;
				}
				break;
			case FlyByUtils.FlyByDirection.FLYBY_DIR_FORWARD:
				if (num2 > this.int_1)
				{
					num2 = this.int_1;
				}
				break;
			}
			return num2;
		}

		private int method_7()
		{
			return (int)(this.int_0 + this.flyByDirection_0);
		}

		private int method_8()
		{
			return this.int_0 - (int)this.flyByDirection_0;
		}

		private bool method_9()
		{
			bool result = true;
			switch (this.flyByDirection_0)
			{
			case FlyByUtils.FlyByDirection.FLYBY_DIR_BACKWARDS:
				if (this.method_7() <= 0)
				{
					result = false;
				}
				break;
			case FlyByUtils.FlyByDirection.FLYBY_DIR_FORWARD:
				if (this.method_7() >= this.int_1)
				{
					result = false;
				}
				break;
			}
			return result;
		}

		private bool method_10()
		{
			bool result = true;
			switch (this.flyByDirection_0)
			{
			case FlyByUtils.FlyByDirection.FLYBY_DIR_BACKWARDS:
				if (this.method_8() >= this.int_1)
				{
					result = false;
				}
				break;
			case FlyByUtils.FlyByDirection.FLYBY_DIR_FORWARD:
				if (this.method_8() <= 0)
				{
					result = false;
				}
				break;
			}
			return result;
		}

		private bool method_11()
		{
			return this.method_10() && this.method_9();
		}

		private double method_12()
		{
			double num = 0.0;
			if (this.method_11())
			{
				IVector3D vector3D = new Vector3D() as IVector3D;
				vector3D.ConstructDifference(this.ipointCollection_0.get_Point(this.method_7()), this.ipointCollection_0.get_Point(this.int_0));
				IVector3D vector3D2 = new Vector3D() as IVector3D;
				vector3D2.ConstructDifference(this.ipointCollection_0.get_Point(this.int_0), this.ipointCollection_0.get_Point(this.method_8()));
				if (this.cboRollFactor.Text == "<无>")
				{
					num = 0.0;
				}
				else
				{
					num = Utils3D.RadiansToDegrees(vector3D.Azimuth) - Utils3D.RadiansToDegrees(vector3D2.Azimuth);
					double num2 = Convert.ToDouble(this.cboRollFactor.Text);
					num *= num2;
					if (num > 5.0)
					{
						num = 5.0;
					}
					else if (num < -5.0)
					{
						num = -5.0;
					}
				}
			}
			return num;
		}

		private void method_13(ref short short_1, ref short short_2, ref float float_0, ref float float_1)
		{
		}

		private void method_14(ref short short_1, ref short short_2, ref float float_0, ref float float_1)
		{
			tagRECT tagRECT_;
			tagRECT_.bottom = 10;
			tagRECT_.left = 10;
			tagRECT_.top = 10;
			tagRECT_.right = 10;
			basColor.SelColor(System.Drawing.Color.Red.ToArgb(), base.Handle.ToInt32(), tagRECT_);
			if (this.chkShowPath.CheckState == System.Windows.Forms.CheckState.Checked && FlyByUtils.pObserverElem != null)
			{
				FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, FlyByUtils.pObserverElem.Geometry, FlyByUtils.FlyByElementType.FLYBY_OBSERVER, System.Drawing.Color.Red, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Red, true);
			}
		}

		private void method_15(ref short short_1, ref short short_2, ref float float_0, ref float float_1)
		{
			tagRECT tagRECT_;
			tagRECT_.bottom = 10;
			tagRECT_.left = 10;
			tagRECT_.top = 10;
			tagRECT_.right = 10;
			basColor.SelColor(System.Drawing.Color.Red.ToArgb(), base.Handle.ToInt32(), tagRECT_);
			if (this.chkShowPath.CheckState == System.Windows.Forms.CheckState.Checked && FlyByUtils.pTargetElem != null)
			{
				FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, FlyByUtils.pTargetElem.Geometry, FlyByUtils.FlyByElementType.FLYBY_TARGET, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, true);
			}
		}

		private void method_16(ref short short_1, ref short short_2, ref float float_0, ref float float_1)
		{
			tagRECT tagRECT_;
			tagRECT_.bottom = 10;
			tagRECT_.left = 10;
			tagRECT_.top = 10;
			tagRECT_.right = 10;
			basColor.SelColor(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red), base.Handle.ToInt32(), tagRECT_);
			if (this.chkShowPath.CheckState == System.Windows.Forms.CheckState.Checked && FlyByUtils.pPathElem != null)
			{
				FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, FlyByUtils.pPathElem.Geometry, FlyByUtils.FlyByElementType.FLYBY_PATH, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, true);
			}
		}

		private void lstPoints_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.bool_0 && this.lstPoints.SelectedItems.Count != 0)
			{
				this.int_0 = this.lstPoints.SelectedIndex;
				this.method_5();
			}
		}

		private void lstPoints_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			System.Windows.Forms.Keys keyCode = e.KeyCode;
			bool arg_0D_0 = e.Shift;
			if (keyCode == System.Windows.Forms.Keys.Escape)
			{
				this.method_21();
			}
		}

		private void optBackward_CheckedChanged(object sender, EventArgs e)
		{
			if (this.optBackward.Checked && !this.bool_1)
			{
				this.method_4();
				this.lstPoints_SelectedIndexChanged(this.lstPoints, new EventArgs());
			}
		}

		private void optForward_CheckedChanged(object sender, EventArgs e)
		{
			if (this.optForward.Checked && !this.bool_1)
			{
				this.method_4();
				this.lstPoints_SelectedIndexChanged(this.lstPoints, new EventArgs());
			}
		}

		private void optNo_CheckedChanged(object sender, EventArgs e)
		{
			if (this.optNo.Checked)
			{
				this.flyByLoop_0 = FlyByUtils.FlyByLoop.FLYBY_LOOP_NO;
			}
		}

		private void optReturn_CheckedChanged(object sender, EventArgs e)
		{
			if (this.optReturn.Checked)
			{
				this.flyByLoop_0 = FlyByUtils.FlyByLoop.FLYBY_LOOP_ONCE;
			}
		}

		private void optContinuos_CheckedChanged(object sender, EventArgs e)
		{
			if (this.optContinuos.Checked)
			{
				this.flyByLoop_0 = FlyByUtils.FlyByLoop.FLYBY_LOOP_CONTINUOUS;
			}
		}

		private void sldFrameDelay_Scroll(object sender, EventArgs e)
		{
			try
			{
				if (this.sldFrameDelay.Value == 1)
				{
					this.tmrAnimation.Interval = 1;
				}
				else if (this.sldFrameDelay.Value == 2)
				{
					this.tmrAnimation.Interval = 50;
				}
				else if (this.sldFrameDelay.Value == 3)
				{
					this.tmrAnimation.Interval = 150;
				}
				else
				{
					this.tmrAnimation.Interval = this.sldFrameDelay.Value * 100;
				}
				double num = (double)(this.sldFrameDelay.Value / 10);
				this.lblFrameDelay.Text = "画面延迟：" + num.ToString("##0.0") + " seconds.";
				this.lblFrameDelay.Refresh();
			}
			catch (Exception)
			{
			}
		}

		private void method_17()
		{
			this.sldFrameDelay.Value = this.sldFrameDelay.Value - this.sldFrameDelay.SmallChange;
			this.sldFrameDelay_Scroll(this.sldFrameDelay, new EventArgs());
		}

		private void method_18()
		{
			this.sldFrameDelay.Value = this.sldFrameDelay.Value + this.sldFrameDelay.SmallChange;
			this.sldFrameDelay_Scroll(this.sldFrameDelay, new EventArgs());
		}

		private void sldInclination_Scroll(object sender, EventArgs e)
		{
			if (this.sldInclination.Value == 0)
			{
				this.lblInclinationR.ForeColor = System.Drawing.Color.Red;
				this.lblNoInclination.Visible = true;
			}
			else
			{
				this.lblInclinationR.ForeColor = System.Drawing.Color.Blue;
				this.lblNoInclination.Visible = false;
			}
			this.lblInclinationR.Text = this.sldInclination.Value.ToString();
			if (this.lstPoints.SelectedIndex != -1)
			{
				this.method_5();
			}
		}

		private void method_19(object sender, EventArgs e)
		{
			this.lblTarOffset.Text = "竖直偏移：" + this.sldTarOffset.Value.ToString(this.string_1);
			this.lblTarOffset.Refresh();
		}

		private void cmdDone_Click(object sender, EventArgs e)
		{
			base.Hide();
			base.Close();
		}

		private void method_20(object sender, EventArgs e)
		{
			switch (this.TabControl1.SelectedIndex)
			{
			case 0:
				this.fraPath.BringToFront();
				break;
			case 1:
				this.fraAnimation.BringToFront();
				break;
			case 2:
				this.fraCamera.BringToFront();
				break;
			case 3:
				this.fraOutput.BringToFront();
				break;
			}
			this.cmdAnim.Focus();
		}

		private void method_21()
		{
			this.bool_2 = true;
			this.tmrAnimation.Enabled = false;
			this.cmdAnim.Text = "开始飞行";
			this.cmdDone.Enabled = true;
			this.cmdPlayImages.Enabled = true;
			this.cmdDeleteImages.Enabled = true;
			this.cmdBrowse.Enabled = true;
			this.method_22();
		}

		private void method_22()
		{
			if (this.icamera_0 != null)
			{
				this.icamera_0.RollAngle = 0.0;
			}
			Utils3D.RefreshApp(this._plugin.SceneGraph);
		}

		private void method_23()
		{
			if (this.bool_0)
			{
				this.bool_1 = true;
				if (this.method_9())
				{
					this.lstPoints.SelectedIndex = this.method_7();
					this.lstPoints.Refresh();
				}
				else if (this.flyByLoop_0 == FlyByUtils.FlyByLoop.FLYBY_LOOP_NO)
				{
					this.method_21();
				}
				else
				{
					this.method_4();
					if (this.flyByLoop_0 == FlyByUtils.FlyByLoop.FLYBY_LOOP_ONCE)
					{
						this.flyByLoop_0 = FlyByUtils.FlyByLoop.FLYBY_LOOP_NO;
						this.optNo.Checked = true;
					}
				}
				this.bool_1 = false;
			}
		}

		private void tmrAnimation_Tick(object sender, EventArgs e)
		{
			try
			{
				if (!this.bool_1)
				{
					this.method_23();
					this.method_25();
				}
				IMessageDispatcher messageDispatcher = new MessageDispatcher();
				messageDispatcher.CancelOnEscPress = true;
				object obj;
				messageDispatcher.Dispatch(base.Handle.ToInt32(), false, out obj);
				messageDispatcher.Dispatch((this._plugin.Hook as ISceneControlDefault).hWnd, false, out obj);
			}
			catch
			{
			}
		}

		private void txtOutputImgsPath_TextChanged(object sender, EventArgs e)
		{
			this.method_24();
		}

		private void txtOutputImgsPrefix_TextChanged(object sender, EventArgs e)
		{
			this.method_24();
		}

		private void cboOutputImgsType_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.method_24();
		}

		private void method_24()
		{
			if (Directory.Exists(this.txtOutputImgsPath.Text))
			{
				this.flstOutputImgs.Visible = true;
			}
			else
			{
				this.flstOutputImgs.Visible = false;
			}
		}

		private void method_25()
		{
			this.bool_1 = true;
			if (this.cboOutputImgsEvery.Text == "<无>")
			{
				this.bool_1 = false;
			}
			else
			{
				short num;
				try
				{
					num = short.Parse(this.cboOutputImgsEvery.Text.Substring(0, 1));
				}
				catch
				{
					return;
				}
				if ((double)(this.short_0 % num) == 0.0)
				{
					esri3DOutputImageType type;
					if (this.cboOutputImgsType.Text.Trim() == "JPG")
					{
						type = esri3DOutputImageType.JPEG;
					}
					else
					{
						type = esri3DOutputImageType.BMP;
					}
					string nextAvailableFilename = this.GetNextAvailableFilename(this.cboOutputImgsType.Text);
					this.isceneViewer_0.GetScreenShot(type, nextAvailableFilename);
					this.flstOutputImgs.Items.Add(nextAvailableFilename);
					this.flstOutputImgs.SelectedIndex = this.flstOutputImgs.Items.Count - 1;
					this.method_26();
				}
				this.short_0 += 1;
				this.bool_1 = false;
			}
		}

		private void flstOutputImgs_SelectedIndexChanged(object sender, EventArgs e)
		{
			string filename = this.flstOutputImgs.SelectedItem.ToString();
			try
			{
				this.imgPreview.Image = System.Drawing.Image.FromFile(filename);
				this.imgPreview.Refresh();
			}
			catch
			{
			}
		}

		private void cmdPlayImages_Click(object sender, EventArgs e)
		{
			this.cmdDeleteImages.Enabled = false;
			this.cmdBrowse.Enabled = false;
			this.cmdAnim.Enabled = false;
			IMessageDispatcher messageDispatcher = new MessageDispatcher();
			messageDispatcher.CancelOnEscPress = true;
			this.bool_2 = false;
			int selectedIndex = this.flstOutputImgs.SelectedIndex;
			for (int i = selectedIndex; i <= this.flstOutputImgs.Items.Count - 1; i++)
			{
				this.flstOutputImgs.SelectedIndex = i;
				object obj;
				messageDispatcher.Dispatch(base.Handle.ToInt32(), false, out obj);
				if (this.bool_2)
				{
					break;
				}
				messageDispatcher.Dispatch((this._plugin.Hook as ISceneControlDefault).hWnd, false, out obj);
				if (this.bool_2)
				{
					break;
				}
			}
			this.flstOutputImgs.SelectedIndex = selectedIndex;
			this.cmdDeleteImages.Enabled = true;
			this.cmdBrowse.Enabled = true;
			this.cmdAnim.Enabled = true;
		}

		private void cmdDeleteImages_Click(object sender, EventArgs e)
		{
			string path = string.Concat(new string[]
			{
				this.txtOutputImgsPath.Text,
				"\\",
				this.txtOutputImgsPrefix.Text,
				"*.",
				this.cboOutputImgsType.Text
			});
			File.Delete(path);
			this.imgPreview.Image = null;
			this.lblFileSize.Text = "文件大小: 0 Kb.";
			this.lblFreeSpace.Text = "当前可用空间: 0 Kb.";
			this.lblFreeSpaceAfter.Text = "估计当前循环后可用空间: 0 Kb.";
			this.flstOutputImgs.Refresh();
		}

		private void method_26()
		{
		}

		public string GetNextAvailableFilename(string string_2)
		{
			short num = 0;
			string text = string.Concat(new string[]
			{
				this.txtOutputImgsPath.Text,
				"\\",
				this.txtOutputImgsPrefix.Text,
				num.ToString("0000"),
				".",
				string_2
			});
			while (File.Exists(text))
			{
				num += 1;
				text = string.Concat(new string[]
				{
					this.txtOutputImgsPath.Text,
					"\\",
					this.txtOutputImgsPrefix.Text,
					num.ToString("0000"),
					".",
					string_2
				});
			}
			return text;
		}

		private void cmdBrowse_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string selectedPath = folderBrowserDialog.SelectedPath;
				this.txtOutputImgsPath.Text = selectedPath;
			}
		}

		private void flstTextures_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.method_27();
			}
			catch
			{
			}
		}

		private void method_27()
		{
			if (this.flstTextures.Items.Count != 0)
			{
				if (this.flstTextures.SelectedIndex == -1)
				{
					this.flstTextures.SelectedIndex = 0;
				}
				string filename = this.flstTextures.SelectedItem.ToString();
				this.imgBackground.Image = System.Drawing.Image.FromFile(filename);
				if (this.igraphicsContainer3D_0 != null)
				{
					this.igraphicsContainer3D_0.DeleteAllElements();
				}
				if (this.chkBackground.CheckState == System.Windows.Forms.CheckState.Checked)
				{
					this.ShowBackground();
				}
			}
		}

		public void ShowBackground()
		{
			if (this.isceneGraph_0 != null && this.flstTextures.Items.Count != 0)
			{
				if (this.imultiPatch_0 == null)
				{
					IEnvelope2 envelope = this.isceneGraph_0.Scene.Extent as IEnvelope2;
					double xMin = envelope.XMin;
					double yMin = envelope.YMin;
					double zMin = envelope.ZMin;
					double xMax = envelope.XMax;
					double yMax = envelope.YMax;
					double zMax = envelope.ZMax;
					IPoint point = new Point();
					point.X = xMin + envelope.Width * 0.5;
					point.Y = yMin + envelope.Height * 0.5;
					point.Z = zMin + (zMax - zMin) * 0.5;
					double double_ = Math.Sqrt(Math.Pow(xMax - xMin, 2.0) + Math.Pow(yMax - yMin, 2.0)) * 0.75;
					this.imultiPatch_0 = this.SpherePatch(0.0, 360.0, -15.0, 35.0, point, double_);
				}
				string text = this.flstTextures.SelectedItem.ToString();
				IPictureFillSymbol pictureFillSymbol;
				if (this.sortedList_0[text] != null)
				{
					pictureFillSymbol = (this.sortedList_0[text] as IPictureFillSymbol);
				}
				else
				{
					pictureFillSymbol = new PictureFillSymbol();
					pictureFillSymbol.CreateFillSymbolFromFile(esriIPictureType.esriIPictureBitmap, text);
					this.sortedList_0.Add(text, pictureFillSymbol);
				}
				IElement element = new MultiPatchElement();
				IFillShapeElement fillShapeElement = element as IFillShapeElement;
				fillShapeElement.Symbol = pictureFillSymbol;
				element.Geometry = this.imultiPatch_0;
				IElementProperties elementProperties = element as IElementProperties;
				elementProperties.Name = "_BACKDROP_";
				this.igraphicsContainer3D_0.AddElement(element);
				this.isceneGraph_0.RefreshViewers();
			}
		}

		public IMultiPatch SpherePatch(double double_3, double double_4, double double_5, double double_6, IPoint ipoint_1, double double_7)
		{
			object value = Missing.Value;
			double num = 36.0;
			double num2 = (double_4 - double_3) / num;
			double num3 = (double_6 - double_5) / (num / 2.0);
			double num4 = double_4 - double_3;
			double num5 = double_6 - double_5;
			IMultiPatch multiPatch = new MultiPatch() as IMultiPatch;
			IGeometryCollection geometryCollection = multiPatch as IGeometryCollection;
			IVector3D vector3D = new Vector3D() as IVector3D;
			IEncode3DProperties encode3DProperties = new GeometryEnvironment() as IEncode3DProperties;
			for (double num6 = double_3; num6 <= double_4 - num2; num6 += num2)
			{
				IPointCollection pointCollection = new TriangleStrip();
				for (double num7 = double_5; num7 <= double_6; num7 += num3)
				{
					double num8 = Utils3D.DegreesToRadians(num6);
					double inclination = Utils3D.DegreesToRadians(num7);
					vector3D.PolarSet(-num8, inclination, double_7);
					IPoint point = new Point();
					point.X = ipoint_1.X + vector3D.XComponent;
					point.Y = ipoint_1.Y + vector3D.YComponent;
					point.Z = ipoint_1.Z + vector3D.ZComponent;
					double textureS = (num6 - double_3) / num4;
					double textureT = (double_6 - num7) / num5;
					double m = 0.0;
					encode3DProperties.PackTexture2D(textureS, textureT, out m);
					vector3D.Normalize();
					encode3DProperties.PackNormal(vector3D, out m);
					point.M = m;
					pointCollection.AddPoint(point, ref value, ref value);
					if (num7 != -90.0 && num7 != 90.0)
					{
						num8 = Utils3D.DegreesToRadians(num6 + num2);
						inclination = Utils3D.DegreesToRadians(num7);
						vector3D.PolarSet(-num8, inclination, double_7);
						point = new Point();
						point.X = ipoint_1.X + vector3D.XComponent;
						point.Y = ipoint_1.Y + vector3D.YComponent;
						point.Z = ipoint_1.Z + vector3D.ZComponent;
						textureS = (num6 + num2 - double_3) / num4;
						textureT = (double_6 - num7) / num5;
						m = 0.0;
						encode3DProperties.PackTexture2D(textureS, textureT, out m);
						vector3D.Normalize();
						encode3DProperties.PackNormal(vector3D, out m);
						point.M = m;
						pointCollection.AddPoint(point, ref value, ref value);
					}
				}
				geometryCollection.AddGeometry(pointCollection as IGeometry, ref value, ref value);
			}
			IZAware iZAware = multiPatch as IZAware;
			iZAware.ZAware = true;
			IMAware iMAware = multiPatch as IMAware;
			iMAware.MAware = true;
			return multiPatch;
		}

		private void sldTarOffset_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.UpdatePathShape();
		}

		private void chkBackground_CheckedChanged(object sender, EventArgs e)
		{
			this.btnSelectBackGround.Enabled = this.chkBackground.Checked;
			this.flstTextures.Enabled = this.chkBackground.Checked;
			this.method_27();
		}

		private void method_28(object sender, EventArgs e)
		{
		}

		private void btnSelectBackGround_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
			openFileDialog.Filter = "JPEG文件交换格式(*.jpg;*jpeg)|*.jpg;*jpeg|Windows位图(*.bmp)|*.bmp|tiff(*.tif)|*.tif|GIF(*.gif)|*.gif";
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.flstTextures.Items.Add(openFileDialog.FileName);
			}
		}

		private void method_29(object sender, EventArgs e)
		{
		}

		private void chkShowPath_CheckedChanged(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				if (this.chkShowPath.Checked)
				{
					FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, this._plugin.Camera.Observer, FlyByUtils.FlyByElementType.FLYBY_OBSERVER, System.Drawing.Color.Red, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Red, true);
					FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, this._plugin.Camera.Target, FlyByUtils.FlyByElementType.FLYBY_TARGET, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, true);
					FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, this.ipointCollection_0 as IGeometry, FlyByUtils.FlyByElementType.FLYBY_PATH, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, true);
				}
				else
				{
					FlyByUtils.DeleteFlyByElement(this._plugin.SceneGraph, FlyByUtils.FlyByElementType.FLYBY_PATH, false);
					FlyByUtils.DeleteFlyByElement(this._plugin.SceneGraph, FlyByUtils.FlyByElementType.FLYBY_TARGET, false);
					FlyByUtils.DeleteFlyByElement(this._plugin.SceneGraph, FlyByUtils.FlyByElementType.FLYBY_OBSERVER, true);
				}
			}
		}
	}
}
