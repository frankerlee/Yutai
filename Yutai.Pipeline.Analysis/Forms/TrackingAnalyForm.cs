
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public class TrackingAnalyForm : Form
	{
		private class Class6
		{
			public IFeatureLayer ifeatureLayer_0;

			public override string ToString()
			{
				return this.ifeatureLayer_0.Name;
			}
		}

		private IContainer icontainer_0 = null;

		private Label label1;

		private ComboBox LayerCom;

		private RadioButton radioButton1;

		private RadioButton radioButton2;

		private RadioButton radioButton3;

		private RadioButton radioButton4;

		private Label label2;

		private ComboBox WayCom;

		private Button SetButton;

		private Button ClearBut;

		public IMapControl3 MapControl;

		public IPipeConfig pPipeCfg;

		public IAppContext m_iApp;

		private IList<IFeature> ilist_0 = new List<IFeature>();

		private IList<IFeature> ilist_1 = new List<IFeature>();

		private IList<IFeature> ilist_2 = new List<IFeature>();

		private IList<IFeature> ilist_3 = new List<IFeature>();

		private IList<IFeature> ilist_4 = new List<IFeature>();

		public int DrawType
		{
			get
			{
				int result;
				if (this.radioButton1.Checked)
				{
					result = 1;
				}
				else if (this.radioButton2.Checked)
				{
					result = 2;
				}
				else if (this.radioButton3.Checked)
				{
					result = 3;
				}
				else if (this.radioButton4.Checked)
				{
					result = 4;
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		public IFeatureClass pSelectPointLayer
		{
			get
			{
				int selectedIndex = this.LayerCom.SelectedIndex;
				IFeatureClass result;
				if (selectedIndex < 0)
				{
					result = null;
				}
				else if (this.MapControl == null)
				{
					result = null;
				}
				else
				{
					IFeatureClass featureClass = ((TrackingAnalyForm.Class6)this.LayerCom.SelectedItem).ifeatureLayer_0.FeatureClass;
					if (featureClass == null)
					{
						result = null;
					}
					else
					{
						INetworkClass networkClass = featureClass as INetworkClass;
						if (networkClass == null)
						{
							result = null;
						}
						else
						{
							IGeometricNetwork geometricNetwork = networkClass.GeometricNetwork;
							IEnumFeatureClass enumFeatureClass = geometricNetwork.get_ClassesByType((esriFeatureType) 7);
							enumFeatureClass.Reset();
							featureClass = enumFeatureClass.Next();
							if (featureClass.AliasName.Contains("TOPO_Junctions"))
							{
								featureClass = enumFeatureClass.Next();
							}
							result = featureClass;
						}
					}
				}
				return result;
			}
		}

		public IFeatureClass pSelectLineLayer
		{
			get
			{
				int selectedIndex = this.LayerCom.SelectedIndex;
				IFeatureClass result;
				if (selectedIndex < 0)
				{
					result = null;
				}
				else if (this.MapControl == null)
				{
					result = null;
				}
				else
				{
					IFeatureClass featureClass = ((TrackingAnalyForm.Class6)this.LayerCom.SelectedItem).ifeatureLayer_0.FeatureClass;
					if (featureClass == null)
					{
						result = null;
					}
					else
					{
						INetworkClass networkClass = featureClass as INetworkClass;
						if (networkClass == null)
						{
							result = null;
						}
						else
						{
							IGeometricNetwork geometricNetwork = networkClass.GeometricNetwork;
							IEnumFeatureClass enumFeatureClass = geometricNetwork.get_ClassesByType((esriFeatureType) 8);
							enumFeatureClass.Reset();
							result = enumFeatureClass.Next();
						}
					}
				}
				return result;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.LayerCom = new ComboBox();
			this.radioButton1 = new RadioButton();
			this.radioButton2 = new RadioButton();
			this.radioButton3 = new RadioButton();
			this.radioButton4 = new RadioButton();
			this.label2 = new Label();
			this.WayCom = new ComboBox();
			this.SetButton = new Button();
			this.ClearBut = new Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 14);
			this.label1.Name = "label1";
			this.label1.Size = new Size(65, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "选择追踪层";
			this.LayerCom.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LayerCom.FormattingEnabled = true;
			this.LayerCom.Location = new System.Drawing.Point(78, 11);
			this.LayerCom.Name = "LayerCom";
			this.LayerCom.Size = new Size(113, 20);
			this.LayerCom.TabIndex = 1;
			this.LayerCom.SelectedIndexChanged += new EventHandler(this.LayerCom_SelectedIndexChanged);
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(203, 1);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new Size(59, 16);
			this.radioButton1.TabIndex = 2;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "点标志";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(203, 23);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(59, 16);
			this.radioButton2.TabIndex = 3;
			this.radioButton2.Text = "线标志";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(268, 1);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new Size(59, 16);
			this.radioButton3.TabIndex = 4;
			this.radioButton3.Text = "点阻断";
			this.radioButton3.UseVisualStyleBackColor = true;
			this.radioButton4.AutoSize = true;
			this.radioButton4.Location = new System.Drawing.Point(268, 23);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new Size(59, 16);
			this.radioButton4.TabIndex = 5;
			this.radioButton4.Text = "线阻断";
			this.radioButton4.UseVisualStyleBackColor = true;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(370, 14);
			this.label2.Name = "label2";
			this.label2.Size = new Size(53, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "追踪方式";
			this.WayCom.DropDownStyle = ComboBoxStyle.DropDownList;
			this.WayCom.FormattingEnabled = true;
			this.WayCom.Items.AddRange(new object[]
			{
				"追踪上游",
				"追踪下游"
			});
			this.WayCom.Location = new System.Drawing.Point(425, 11);
			this.WayCom.Name = "WayCom";
			this.WayCom.Size = new Size(85, 20);
			this.WayCom.TabIndex = 7;
			this.SetButton.Location = new System.Drawing.Point(516, 8);
			this.SetButton.Name = "SetButton";
			this.SetButton.Size = new Size(64, 23);
			this.SetButton.TabIndex = 8;
			this.SetButton.Text = "处理";
			this.SetButton.UseVisualStyleBackColor = true;
			this.SetButton.Click += new EventHandler(this.SetButton_Click);
			this.ClearBut.Location = new System.Drawing.Point(329, 9);
			this.ClearBut.Name = "ClearBut";
			this.ClearBut.Size = new Size(39, 23);
			this.ClearBut.TabIndex = 9;
			this.ClearBut.Text = "清除";
			this.ClearBut.UseVisualStyleBackColor = true;
			this.ClearBut.Click += new EventHandler(this.ClearBut_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(587, 49);
			base.Controls.Add(this.ClearBut);
			base.Controls.Add(this.SetButton);
			base.Controls.Add(this.WayCom);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.radioButton4);
			base.Controls.Add(this.radioButton3);
			base.Controls.Add(this.radioButton2);
			base.Controls.Add(this.radioButton1);
			base.Controls.Add(this.LayerCom);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TrackingAnalyForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "流向追踪";
			base.Load += new EventHandler(this.TrackingAnalyForm_Load);
			base.HelpRequested += new HelpEventHandler(this.TrackingAnalyForm_HelpRequested);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public TrackingAnalyForm()
		{
			this.InitializeComponent();
		}

		public void AddJunctionFlag(IFeature PointFeature)
		{
			this.ilist_0.Add(PointFeature);
		}

		public void AddEdgeFlag(IFeature PointFeature)
		{
			this.ilist_1.Add(PointFeature);
		}

		public void AddJunctionBarrierFlag(IFeature PointFeature)
		{
			this.ilist_2.Add(PointFeature);
		}

		public void AddEdgeBarrierFlag(IFeature PointFeature)
		{
			this.ilist_3.Add(PointFeature);
		}

		private void TrackingAnalyForm_Load(object obj, EventArgs eventArgs)
		{
			this.WayCom.SelectedIndex = 0;
			this.method_3();
		}

		private void method_0(ILayer layer)
		{
			if (layer is IFeatureLayer)
			{
				this.method_2((IFeatureLayer)layer);
			}
			else if (layer is IGroupLayer)
			{
				this.method_1((IGroupLayer)layer);
			}
		}

		private void method_1(IGroupLayer groupLayer)
		{
			ICompositeLayer compositeLayer = (ICompositeLayer)groupLayer;
			if (compositeLayer != null)
			{
				int count = compositeLayer.Count;
				for (int i = 0; i < count; i++)
				{
					ILayer layer = compositeLayer.get_Layer(i);
					this.method_0(layer);
				}
			}
		}

		private void method_2(IFeatureLayer featureLayer)
		{
			if (featureLayer != null && featureLayer.FeatureClass != null)
			{
				string aliasName = featureLayer.FeatureClass.AliasName;
				if (this.pPipeCfg.IsPipeLine(aliasName))
				{
					TrackingAnalyForm.Class6 @class = new TrackingAnalyForm.Class6();
					@class.ifeatureLayer_0 = featureLayer;
					this.LayerCom.Items.Add(@class);
				}
			}
		}

		private void method_3()
		{
			int layerCount = this.m_iApp.FocusMap.LayerCount;
			if (this.MapControl != null)
			{
				this.LayerCom.Items.Clear();
				for (int i = 0; i < layerCount; i++)
				{
				    ILayer layer = this.m_iApp.FocusMap.Layer[i];
					this.method_0(layer);
				}
				if (this.LayerCom.Items.Count > 0)
				{
					this.LayerCom.SelectedIndex = 0;
				}
			}
		}

		private void LayerCom_SelectedIndexChanged(object obj, EventArgs eventArgs)
		{
			this.ilist_0.Clear();
			this.ilist_1.Clear();
			this.ilist_2.Clear();
			this.ilist_3.Clear();
			this.ilist_4.Clear();
		}

		private void SetButton_Click(object obj, EventArgs eventArgs)
		{
			int count = this.ilist_0.Count;
			int count2 = this.ilist_1.Count;
			int count3 = this.ilist_2.Count;
			int count4 = this.ilist_3.Count;
			if (count < 1 && count2 < 1)
			{
				MessageBox.Show("请用户选择要分析的点或线!");
			}
			else
			{
				IEdgeFlag[] array = new EdgeFlag[count2];
				IJunctionFlag[] array2 = new JunctionFlag[count];
				INetwork network = null;
				INetworkClass networkClass = null;
				if (count > 0)
				{
					for (int i = 0; i < count; i++)
					{
						IFeature feature = this.ilist_0[i];
						networkClass = (feature.Class as INetworkClass);
						network = networkClass.GeometricNetwork.Network;
						INetElements netElements = network as INetElements;
						INetFlag netFlag = new JunctionFlag() as INetFlag;
						ISimpleJunctionFeature simpleJunctionFeature = feature as ISimpleJunctionFeature;
						int userClassID;
						int userID;
						int userSubID;
						netElements.QueryIDs(simpleJunctionFeature.EID, (esriElementType) 1, out userClassID, out userID, out userSubID);
						netFlag.UserClassID=(userClassID);
						netFlag.UserID=(userID);
						netFlag.UserSubID=(userSubID);
						IJunctionFlag junctionFlag = netFlag as IJunctionFlag;
						array2[i] = junctionFlag;
					}
				}
				if (count2 > 0)
				{
					for (int j = 0; j < count2; j++)
					{
						IFeature feature2 = this.ilist_1[j];
						networkClass = (feature2.Class as INetworkClass);
						network = networkClass.GeometricNetwork.Network;
						INetElements netElements2 = network as INetElements;
						INetFlag netFlag2 = new EdgeFlag() as INetFlag;
						ISimpleEdgeFeature simpleEdgeFeature = feature2 as ISimpleEdgeFeature;
						int userClassID2;
						int userID2;
						int userSubID2;
						netElements2.QueryIDs(simpleEdgeFeature.EID, (esriElementType) 2, out userClassID2, out userID2, out userSubID2);
						netFlag2.UserClassID=(userClassID2);
						netFlag2.UserID=(userID2);
						netFlag2.UserSubID=(userSubID2);
						IEdgeFlag edgeFlag = netFlag2 as IEdgeFlag;
						array[j] = edgeFlag;
					}
				}
				ITraceFlowSolverGEN traceFlowSolverGEN = new TraceFlowSolver() as ITraceFlowSolverGEN;
				INetSolver netSolver = traceFlowSolverGEN as INetSolver;
				INetElementBarriersGEN netElementBarriersGEN = new NetElementBarriers();
				netElementBarriersGEN.Network=(network);
				netElementBarriersGEN.ElementType=(esriElementType) (1);
				int[] array3 = new int[count3];
				int num = 0;
				if (count3 > 0)
				{
					for (int k = 0; k < count3; k++)
					{
						IFeature feature3 = this.ilist_2[k];
						networkClass = (feature3.Class as INetworkClass);
						network = networkClass.GeometricNetwork.Network;
						INetElements netElements3 = network as INetElements;
						new EdgeFlag();
						ISimpleJunctionFeature simpleJunctionFeature2 = feature3 as ISimpleJunctionFeature;
						int num2;
						int num3;
						netElements3.QueryIDs(simpleJunctionFeature2.EID, (esriElementType) 1, out num, out num2, out num3);
						array3[k] = num2;
					}
					netElementBarriersGEN.SetBarriers(num, ref array3);
					netSolver.set_ElementBarriers((esriElementType) 1, (INetElementBarriers)netElementBarriersGEN);
				}
				INetElementBarriersGEN netElementBarriersGEN2 = new NetElementBarriers();
				netElementBarriersGEN2.Network=(network);
				netElementBarriersGEN2.ElementType=(esriElementType) (2);
				int[] array4 = new int[count4];
				if (count4 > 0)
				{
					for (int l = 0; l < count4; l++)
					{
						IFeature feature4 = this.ilist_3[l];
						networkClass = (feature4.Class as INetworkClass);
						network = networkClass.GeometricNetwork.Network;
						INetElements netElements4 = network as INetElements;
						new EdgeFlag();
						ISimpleEdgeFeature simpleEdgeFeature2 = feature4 as ISimpleEdgeFeature;
						int num4;
						int num5;
						netElements4.QueryIDs(simpleEdgeFeature2.EID, (esriElementType) 2, out num, out num4, out num5);
						array4[l] = num4;
					}
					netElementBarriersGEN2.SetBarriers(num, ref array4);
					netSolver.set_ElementBarriers((esriElementType) 2, (INetElementBarriers)netElementBarriersGEN2);
				}
				netSolver.SourceNetwork=(network);
				if (count > 0)
				{
					traceFlowSolverGEN.PutJunctionOrigins(ref array2);
				}
				if (count2 > 0)
				{
					traceFlowSolverGEN.PutEdgeOrigins(ref array);
				}
				IEnumNetEID enumNetEID = null;
				IEnumNetEID enumNetEID2 = null;
				object[] array5 = new object[1];
				if (this.WayCom.SelectedIndex == 0)
				{
					traceFlowSolverGEN.FindSource(0, (esriShortestPathObjFn) 1, out enumNetEID, out enumNetEID2, count + count2, ref array5);
				}
				if (this.WayCom.SelectedIndex == 1)
				{
					traceFlowSolverGEN.FindSource((esriFlowMethod) 1, (esriShortestPathObjFn) 1, out enumNetEID, out enumNetEID2, count + count2, ref array5);
				}
				IPolyline polyline = new Polyline() as IPolyline;
				IGeometryCollection geometryCollection = polyline as IGeometryCollection;
			    ISpatialReference spatialReference = this.m_iApp.FocusMap.SpatialReference;
				IEIDHelper iEIDHelper = new EIDHelper();
				iEIDHelper.GeometricNetwork=(networkClass.GeometricNetwork);
				iEIDHelper.OutputSpatialReference=(spatialReference);
				iEIDHelper.ReturnGeometries=(true);
				iEIDHelper.ReturnFeatures=(true);
				int selectedIndex = this.LayerCom.SelectedIndex;
				if (selectedIndex >= 0 && this.MapControl != null)
				{
					this.LayerCom.SelectedItem.ToString();
					IFeatureLayer ifeatureLayer_ = ((TrackingAnalyForm.Class6)this.LayerCom.SelectedItem).ifeatureLayer_0;
					if (ifeatureLayer_ != null)
					{
						IFeatureSelection featureSelection = (IFeatureSelection)ifeatureLayer_;
						featureSelection.Clear();
						if (enumNetEID2 != null)
						{
							IEnumEIDInfo enumEIDInfo = iEIDHelper.CreateEnumEIDInfo(enumNetEID2);
							int count5 = enumEIDInfo.Count;
							enumEIDInfo.Reset();
							for (int m = 0; m < count5; m++)
							{
								IEIDInfo iEIDInfo = enumEIDInfo.Next();
								featureSelection.Add(iEIDInfo.Feature);
								IGeometry geometry = iEIDInfo.Geometry;
								geometryCollection.AddGeometryCollection(geometry as IGeometryCollection);
							}
						}
						featureSelection.SelectionSet.Refresh();
					    IActiveView activeView = this.m_iApp.ActiveView;
						activeView.Refresh();
						CMapOperator.ShowFeatureWithWink(this.m_iApp.ActiveView.ScreenDisplay, polyline);
					}
				}
			}
		}

		private void ClearBut_Click(object obj, EventArgs eventArgs)
		{
			this.ilist_0.Clear();
			this.ilist_1.Clear();
			this.ilist_2.Clear();
			this.ilist_3.Clear();
			this.ilist_4.Clear();
		    IActiveView activeView = this.m_iApp.ActiveView;

            activeView.Refresh();
		}

		private void TrackingAnalyForm_HelpRequested(object obj, HelpEventArgs helpEventArgs)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "追踪分析";
			Help.ShowHelp(this, url, HelpNavigator.KeywordIndex, parameter);
		}
	}
}
