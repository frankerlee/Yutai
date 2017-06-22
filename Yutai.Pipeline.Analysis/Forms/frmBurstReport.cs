using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.NetworkAnalysis;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public class frmBurstReport : Form
	{
		private struct Struct0
		{
			public IGeometricNetwork igeometricNetwork_0;

			public int int_0;

			public IPoint ipoint_0;

			public double double_0;
		}

		private struct Struct1
		{
			public IFeatureLayer ifeatureLayer_0;

			public IFeatureLayer ifeatureLayer_1;

			public IFeature ifeature_0;

			public ArrayList arrayList_0;

			public ArrayList arrayList_1;

			public Struct1(IFeatureLayer featureLayer, IFeatureLayer featureLayer2, IFeature feature, ArrayList arrayList, ArrayList arrayList2)
			{
				this.ifeatureLayer_0 = featureLayer;
				this.ifeatureLayer_1 = featureLayer2;
				this.ifeature_0 = feature;
				this.arrayList_0 = arrayList;
				this.arrayList_1 = arrayList2;
			}
		}

		private IContainer icontainer_0 = null;

		private Button btnCancel;

		private TreeView treeView1;

		private GroupBox gIlaRpybmq;

		private Label lblPickPipeInfo;

		private ListBox listOutBarriers;

		private Button btnDelThroughBarrier;

		private Button btnStartAnalysis;

		private GroupBox groupBox2;

		private RadioButton radioUpAndDown;

		private RadioButton radioDown;

		private RadioButton radioUp;

		private GroupBox groupBox3;

		private ListBox listFieldValues;

		private Button btnDelFieldValue;

		private Button btnAddFieldValue;

		private RadioButton btnAddThroughValve;

		private RadioButton btnPickBrokePipe;

		private ComboBox comboBox1;

		private Label label1;

		private CheckBox checkReflushView;

		private Button ClearGra;

		private ListView listView1;

		private Timer timer_0;

		public IAppContext m_iApp;

		private IPoint ipoint_0;

		private frmBurstReport.Struct0 struct0_0 = default(frmBurstReport.Struct0);

		private frmBurstReport.Struct1 struct1_0 = new frmBurstReport.Struct1(null, null, null, new ArrayList(), new ArrayList());

		private IElement ielement_0;

		public int m_nTimerCounter;

		private IGeometry BuoawIbkuD;

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
			this.icontainer_0 = new Container();
			this.btnCancel = new Button();
			this.listView1 = new ListView();
			this.treeView1 = new TreeView();
			this.gIlaRpybmq = new GroupBox();
			this.lblPickPipeInfo = new Label();
			this.listOutBarriers = new ListBox();
			this.btnDelThroughBarrier = new Button();
			this.btnStartAnalysis = new Button();
			this.groupBox2 = new GroupBox();
			this.radioUpAndDown = new RadioButton();
			this.radioDown = new RadioButton();
			this.radioUp = new RadioButton();
			this.groupBox3 = new GroupBox();
			this.label1 = new Label();
			this.comboBox1 = new ComboBox();
			this.listFieldValues = new ListBox();
			this.btnDelFieldValue = new Button();
			this.btnAddFieldValue = new Button();
			this.btnAddThroughValve = new RadioButton();
			this.btnPickBrokePipe = new RadioButton();
			this.timer_0 = new Timer(this.icontainer_0);
			this.checkReflushView = new CheckBox();
			this.ClearGra = new Button();
			this.gIlaRpybmq.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			base.SuspendLayout();
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(326, 254);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "退出(&X)";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(176, 20);
			this.listView1.Name = "listView1";
			this.listView1.Size = new Size(203, 192);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = View.Details;
			this.treeView1.FullRowSelect = true;
			this.treeView1.Location = new System.Drawing.Point(7, 20);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new Size(168, 192);
			this.treeView1.TabIndex = 1;
			this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
			this.treeView1.MouseClick += new MouseEventHandler(this.treeView1_MouseClick);
			this.treeView1.MouseDoubleClick += new MouseEventHandler(this.treeView1_MouseDoubleClick);
			this.gIlaRpybmq.Controls.Add(this.treeView1);
			this.gIlaRpybmq.Controls.Add(this.listView1);
			this.gIlaRpybmq.Location = new System.Drawing.Point(12, 283);
			this.gIlaRpybmq.Name = "groupBox1";
			this.gIlaRpybmq.Size = new Size(389, 219);
			this.gIlaRpybmq.TabIndex = 0;
			this.gIlaRpybmq.TabStop = false;
			this.gIlaRpybmq.Text = "分析结果";
			this.lblPickPipeInfo.BorderStyle = BorderStyle.Fixed3D;
			this.lblPickPipeInfo.Location = new System.Drawing.Point(82, 9);
			this.lblPickPipeInfo.Name = "lblPickPipeInfo";
			this.lblPickPipeInfo.Size = new Size(319, 26);
			this.lblPickPipeInfo.TabIndex = 6;
			this.listOutBarriers.FormattingEnabled = true;
			this.listOutBarriers.ItemHeight = 12;
			this.listOutBarriers.Location = new System.Drawing.Point(235, 67);
			this.listOutBarriers.Name = "listOutBarriers";
			this.listOutBarriers.Size = new Size(166, 64);
			this.listOutBarriers.TabIndex = 8;
			this.btnDelThroughBarrier.Location = new System.Drawing.Point(377, 38);
			this.btnDelThroughBarrier.Name = "btnDelThroughBarrier";
			this.btnDelThroughBarrier.Size = new Size(24, 23);
			this.btnDelThroughBarrier.TabIndex = 9;
			this.btnDelThroughBarrier.Text = "×";
			this.btnDelThroughBarrier.UseVisualStyleBackColor = true;
			this.btnDelThroughBarrier.Click += new EventHandler(this.btnDelThroughBarrier_Click);
			this.btnStartAnalysis.Location = new System.Drawing.Point(12, 254);
			this.btnStartAnalysis.Name = "btnStartAnalysis";
			this.btnStartAnalysis.Size = new Size(75, 23);
			this.btnStartAnalysis.TabIndex = 10;
			this.btnStartAnalysis.Text = "分析";
			this.btnStartAnalysis.UseVisualStyleBackColor = true;
			this.btnStartAnalysis.Click += new EventHandler(this.btnStartAnalysis_Click);
			this.groupBox2.Controls.Add(this.radioUpAndDown);
			this.groupBox2.Controls.Add(this.radioDown);
			this.groupBox2.Controls.Add(this.radioUp);
			this.groupBox2.Location = new System.Drawing.Point(228, 147);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(173, 97);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "查找方向设置";
			this.radioUpAndDown.AutoSize = true;
			this.radioUpAndDown.Location = new System.Drawing.Point(7, 67);
			this.radioUpAndDown.Name = "radioUpAndDown";
			this.radioUpAndDown.Size = new Size(71, 16);
			this.radioUpAndDown.TabIndex = 2;
			this.radioUpAndDown.TabStop = true;
			this.radioUpAndDown.Text = "查找联通";
			this.radioUpAndDown.UseVisualStyleBackColor = true;
			this.radioDown.AutoSize = true;
			this.radioDown.Location = new System.Drawing.Point(7, 44);
			this.radioDown.Name = "radioDown";
			this.radioDown.Size = new Size(71, 16);
			this.radioDown.TabIndex = 1;
			this.radioDown.TabStop = true;
			this.radioDown.Text = "查找下游";
			this.radioDown.UseVisualStyleBackColor = true;
			this.radioUp.AutoSize = true;
			this.radioUp.Location = new System.Drawing.Point(7, 21);
			this.radioUp.Name = "radioUp";
			this.radioUp.Size = new Size(71, 16);
			this.radioUp.TabIndex = 0;
			this.radioUp.TabStop = true;
			this.radioUp.Text = "查找上游";
			this.radioUp.UseVisualStyleBackColor = true;
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.comboBox1);
			this.groupBox3.Controls.Add(this.listFieldValues);
			this.groupBox3.Controls.Add(this.btnDelFieldValue);
			this.groupBox3.Controls.Add(this.btnAddFieldValue);
			this.groupBox3.Location = new System.Drawing.Point(13, 47);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(208, 197);
			this.groupBox3.TabIndex = 14;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "查找内容设置";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 18);
			this.label1.Name = "label1";
			this.label1.Size = new Size(41, 12);
			this.label1.TabIndex = 11;
			this.label1.Text = "label1";
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(10, 162);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new Size(163, 20);
			this.comboBox1.TabIndex = 10;
			this.listFieldValues.FormattingEnabled = true;
			this.listFieldValues.ItemHeight = 12;
			this.listFieldValues.Location = new System.Drawing.Point(8, 39);
			this.listFieldValues.Name = "listFieldValues";
			this.listFieldValues.Size = new Size(167, 112);
			this.listFieldValues.TabIndex = 2;
			this.btnDelFieldValue.Location = new System.Drawing.Point(178, 128);
			this.btnDelFieldValue.Name = "btnDelFieldValue";
			this.btnDelFieldValue.Size = new Size(24, 23);
			this.btnDelFieldValue.TabIndex = 9;
			this.btnDelFieldValue.Text = "×";
			this.btnDelFieldValue.UseVisualStyleBackColor = true;
			this.btnDelFieldValue.Click += new EventHandler(this.btnDelFieldValue_Click);
			this.btnAddFieldValue.Location = new System.Drawing.Point(178, 157);
			this.btnAddFieldValue.Name = "btnAddFieldValue";
			this.btnAddFieldValue.Size = new Size(24, 23);
			this.btnAddFieldValue.TabIndex = 9;
			this.btnAddFieldValue.Text = "+";
			this.btnAddFieldValue.UseVisualStyleBackColor = true;
			this.btnAddFieldValue.Click += new EventHandler(this.btnAddFieldValue_Click);
			this.btnAddThroughValve.Appearance = Appearance.Button;
			this.btnAddThroughValve.AutoSize = true;
			this.btnAddThroughValve.Location = new System.Drawing.Point(235, 38);
			this.btnAddThroughValve.Name = "btnAddThroughValve";
			this.btnAddThroughValve.Size = new Size(87, 22);
			this.btnAddThroughValve.TabIndex = 10;
			this.btnAddThroughValve.Text = "拾取穿越阀门";
			this.btnAddThroughValve.TextAlign = ContentAlignment.MiddleCenter;
			this.btnAddThroughValve.UseVisualStyleBackColor = true;
			this.btnAddThroughValve.CheckedChanged += new EventHandler(this.btnAddThroughValve_CheckedChanged);
			this.btnPickBrokePipe.Appearance = Appearance.Button;
			this.btnPickBrokePipe.AutoSize = true;
			this.btnPickBrokePipe.Checked = true;
			this.btnPickBrokePipe.Location = new System.Drawing.Point(13, 13);
			this.btnPickBrokePipe.Name = "btnPickBrokePipe";
			this.btnPickBrokePipe.Size = new Size(63, 22);
			this.btnPickBrokePipe.TabIndex = 16;
			this.btnPickBrokePipe.TabStop = true;
			this.btnPickBrokePipe.Text = "拾取爆管";
			this.btnPickBrokePipe.UseVisualStyleBackColor = true;
			this.btnPickBrokePipe.CheckedChanged += new EventHandler(this.btnPickBrokePipe_CheckedChanged);
			this.btnPickBrokePipe.Click += new EventHandler(this.btnPickBrokePipe_Click);
			this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
			this.checkReflushView.AutoSize = true;
			this.checkReflushView.Checked = true;
			this.checkReflushView.CheckState = CheckState.Checked;
			this.checkReflushView.Location = new System.Drawing.Point(202, 258);
			this.checkReflushView.Name = "checkReflushView";
			this.checkReflushView.Size = new Size(120, 16);
			this.checkReflushView.TabIndex = 18;
			this.checkReflushView.Text = "退出清除分析结果";
			this.checkReflushView.UseVisualStyleBackColor = true;
			this.ClearGra.Location = new System.Drawing.Point(93, 254);
			this.ClearGra.Name = "ClearGra";
			this.ClearGra.Size = new Size(75, 23);
			this.ClearGra.TabIndex = 10;
			this.ClearGra.Text = "清除结果";
			this.ClearGra.UseVisualStyleBackColor = true;
			this.ClearGra.Click += new EventHandler(this.ClearGra_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(408, 511);
			base.Controls.Add(this.checkReflushView);
			base.Controls.Add(this.btnDelThroughBarrier);
			base.Controls.Add(this.listOutBarriers);
			base.Controls.Add(this.btnAddThroughValve);
			base.Controls.Add(this.btnPickBrokePipe);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.ClearGra);
			base.Controls.Add(this.btnStartAnalysis);
			base.Controls.Add(this.lblPickPipeInfo);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.gIlaRpybmq);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmBurstReport";
			base.ShowIcon = false;
			this.Text = "关阀分析";
			base.TopMost = true;
			base.FormClosed += new FormClosedEventHandler(this.frmBurstReport_FormClosed);
			base.Load += new EventHandler(this.frmBurstReport_Load);
			base.HelpRequested += new HelpEventHandler(this.frmBurstReport_HelpRequested);
			this.gIlaRpybmq.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public IQueryFilter GetBarrierQuery()
		{
		    IQueryFilter queryFilter = new QueryFilter();
			ArrayList arrayList = new ArrayList();
			frmBurstReport.GetValues(arrayList);
			for (int i = 0; i < this.listFieldValues.Items.Count; i++)
			{
				if (i > 0)
				{
					IQueryFilter queryFilter2 = queryFilter;
					IQueryFilter expr_22 = queryFilter2;
					expr_22.WhereClause=(expr_22.WhereClause + " or ");
				}
				IQueryFilter queryFilter3 = queryFilter;
				IQueryFilter expr_3C = queryFilter3;
				expr_3C.WhereClause=(expr_3C.WhereClause + this.label1.Text);
				IQueryFilter queryFilter4 = queryFilter;
				queryFilter4.WhereClause=(queryFilter4.WhereClause + "='" + this.listFieldValues.Items[i].ToString() + "'");
			}
			arrayList.Clear();
			return queryFilter;
		}

		public esriFlowMethod GetFindMethod()
		{
			esriFlowMethod result;
			if (this.radioDown.Checked)
			{
				result = (esriFlowMethod) 1;
			}
			else if (this.radioUp.Checked)
			{
				result = 0;
			}
			else
			{
				result = (esriFlowMethod) 2;
			}
			return result;
		}

		public ISelectionSetBarriers GetSelectionSetBarries()
		{
			int featureClassID = this.struct1_0.ifeatureLayer_1.FeatureClass.FeatureClassID;
		    ISelectionSetBarriers selectionSetBarriers = new SelectionSetBarriers();
			IFeatureSelection featureSelection = (IFeatureSelection)this.struct1_0.ifeatureLayer_1;
			IQueryFilter barrierQuery = this.GetBarrierQuery();
			featureSelection.SelectFeatures(barrierQuery, 0, false);
			IEnumIDs iDs = featureSelection.SelectionSet.IDs;
			iDs.Reset();
			for (int i = iDs.Next(); i > 0; i = iDs.Next())
			{
				if (this.listOutBarriers.Items.IndexOf(i.ToString()) == -1)
				{
					selectionSetBarriers.Add(featureClassID, i);
				}
			}
			return selectionSetBarriers;
		}

		public ITraceFlowSolver GetTraceFlowSolver()
		{
			INetElements netElements = (INetElements)this.struct0_0.igeometricNetwork_0.Network;
			int userClassID = 0;
			int userID = 0;
			int userSubID = 0;
			netElements.QueryIDs(this.struct0_0.int_0, (esriElementType) 2, out userClassID, out userID, out userSubID);
		    IEdgeFlag edgeFlag = new EdgeFlag();
			((INetFlag)edgeFlag).UserClassID=(userClassID);
			((INetFlag)edgeFlag).UserID=(userID);
			((INetFlag)edgeFlag).UserSubID=(userSubID);
		    ITraceFlowSolver traceFlowSolver = (ITraceFlowSolver) new TraceFlowSolver();
			((INetSolver)traceFlowSolver).SourceNetwork=(this.struct0_0.igeometricNetwork_0.Network);
			((INetSolver)traceFlowSolver).SelectionSetBarriers=(this.GetSelectionSetBarries());
			traceFlowSolver.PutEdgeOrigins(1, ref edgeFlag);
			return traceFlowSolver;
		}

		public frmBurstReport()
		{
			this.InitializeComponent();
		}

		private void frmBurstReport_Load(object obj, EventArgs eventArgs)
		{
			ArrayList arrayList = new ArrayList();
			CMapOperator.GetMapILayers(m_iApp.FocusMap, null, arrayList);
			string pointTableFieldName = m_iApp.PipeConfig.GetPointTableFieldName("点性");
			this.label1.Text = pointTableFieldName;
			for (int i = 0; i < arrayList.Count; i++)
			{
				IFeatureLayer featureLayer = arrayList[i] as IFeatureLayer;
				if (featureLayer != null && featureLayer.FeatureClass != null && m_iApp.PipeConfig.IsPipePoint(featureLayer.FeatureClass.AliasName))
				{
					IFeatureClass featureClass = featureLayer.FeatureClass;
					int num = featureClass.Fields.FindField(pointTableFieldName);
					if (num != -1)
					{
						ArrayList uVByQueryDef = this.GetUVByQueryDef(featureLayer, pointTableFieldName);
						for (int j = 0; j < uVByQueryDef.Count; j++)
						{
							string text = uVByQueryDef[j].ToString();
							if (text.Trim() != "" && !this.comboBox1.Items.Contains(text))
							{
								this.comboBox1.Items.Add(text);
							}
							this.comboBox1.Items.Add(text);
						}
					}
				}
			}
			if (this.comboBox1.Items.Count > 0)
			{
				this.comboBox1.SelectedIndex = 0;
			}
			this.label1.Text = pointTableFieldName;
			ArrayList arrayList2 = new ArrayList();
			frmBurstReport.GetValues(arrayList2);
			for (int j = 0; j < arrayList2.Count; j++)
			{
				this.listFieldValues.Items.Add(arrayList2[j].ToString());
			}
			if (this.listFieldValues.Items.Count == 0)
			{
				this.listFieldValues.Items.Add("阀门");
				this.listFieldValues.Items.Add("阀门井");
			}
			this.radioUpAndDown.Checked = true;
		}

		public ArrayList GetUVByQueryDef(IFeatureLayer pFeatureLayer, string strField)
		{
			IDataset dataset = (IDataset)pFeatureLayer.FeatureClass;
			IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)dataset.Workspace;
			IQueryDef queryDef = featureWorkspace.CreateQueryDef();
			queryDef.Tables=(dataset.Name);
			queryDef.SubFields=("DISTINCT(" + strField + ")");
			ICursor cursor = queryDef.Evaluate();
			IRow row = cursor.NextRow();
			ArrayList arrayList = new ArrayList();
			while (row != null)
			{
				object obj = row.get_Value(0);
				arrayList.Add(obj.ToString());
				row = cursor.NextRow();
			}
			return arrayList;
		}

		public void SetMousePoint(int x, int y)
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				IPoint pMousePoint = m_iApp.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
				this.lblPickPipeInfo.Text = "";
				if (this.btnPickBrokePipe.Checked)
				{
					this.listOutBarriers.Items.Clear();
					if (this.PickBrokePipe(pMousePoint))
					{
						this.method_0();
						this.btnStartAnalysis_Click(null, null);
					}
				}
				else if (this.btnAddThroughValve.Checked)
				{
					this.PickThroughValve(pMousePoint);
				}
			}
			catch
			{
			}
			this.Cursor = Cursors.Default;
		}

		public bool PickBrokePipe(IPoint _pMousePoint)
		{
			this.method_1();
			this.ipoint_0 = _pMousePoint;
			bool flag = false;
			double num = 2147483647.0;
			ArrayList arrayList = new ArrayList();
			frmBurstReport.GetMapVisibleILayers(m_iApp.FocusMap, null, arrayList);
			for (int i = 0; i < arrayList.Count; i++)
			{
				IFeatureLayer featureLayer = arrayList[i] as IFeatureLayer;
				if (featureLayer != null && featureLayer.FeatureClass != null && featureLayer.Visible && m_iApp.PipeConfig.IsPipeLine(featureLayer.FeatureClass.AliasName))
				{
					IFeatureDataset featureDataset = featureLayer.FeatureClass.FeatureDataset;
					IFeatureClassContainer featureClassContainer = featureDataset as IFeatureClassContainer;
					IFeatureClass featureClass = featureClassContainer.get_Class(1);
					if (featureClass != null && featureClass is INetworkClass)
					{
						IGeometricNetwork geometricNetwork = ((INetworkClass)featureClass).GeometricNetwork;
						IPointToEID pointToEIDClass = new PointToEID();
						pointToEIDClass.SourceMap=(m_iApp.FocusMap);
						pointToEIDClass.GeometricNetwork=(geometricNetwork);
						pointToEIDClass.SnapTolerance=(m_iApp.ActiveView.Extent.Width / 200.0);
						int int_ = 0;
						IPoint point = null;
						double num2 = 0;
						pointToEIDClass.GetNearestEdge(this.ipoint_0, out int_, out point, out num2);
						if (point != null && num > num2)
						{
							num = num2;
							this.struct0_0.igeometricNetwork_0 = geometricNetwork;
							this.struct0_0.double_0 = num2;
							this.struct0_0.int_0 = int_;
							this.struct0_0.ipoint_0 = point;
							flag = true;
						}
					}
				}
			}
			arrayList.Clear();
			if (flag)
			{
				this.method_3();
			}
			return flag;
		}

		private void method_0()
		{
            int num;
            int num1;
            int num2;
            IFeatureClassContainer featureDataset = (IFeatureClassContainer)this.struct0_0.igeometricNetwork_0.FeatureDataset;
            IFeatureClass featureClass = null;
            IFeatureClass featureClass1 = null;
            int num3 = 0;
            while (true)
            {
                if (num3 < featureDataset.ClassCount)
                {
                    IFeatureClass @class = featureDataset.Class[num3];
                    if ((@class.ShapeType != esriGeometryType.esriGeometryPoint ? false : !@class.AliasName.Contains("Junctions")))
                    {
                        featureClass = @class;
                        break;
                    }
                    else
                    {
                        num3++;
                    }
                }
                else
                {
                    break;
                }
            }
            int num4 = 0;
            while (true)
            {
                if (num4 < featureDataset.ClassCount)
                {
                    IFeatureClass class1 = featureDataset.Class[num4];
                    if (class1.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        featureClass1 = class1;
                        break;
                    }
                    else
                    {
                        num4++;
                    }
                }
                else
                {
                    break;
                }
            }
            this.struct1_0.ifeatureLayer_0 = CMapOperator.GetILayerByAliasName(m_iApp.FocusMap, null, featureClass1.AliasName) as IFeatureLayer;
            this.struct1_0.ifeatureLayer_1 = CMapOperator.GetILayerByAliasName(m_iApp.FocusMap, null, featureClass.AliasName) as IFeatureLayer;
            INetElements network = (INetElements)this.struct0_0.igeometricNetwork_0.Network;
            network.QueryIDs(this.struct0_0.int_0, esriElementType.esriETEdge, out num, out num1, out num2);
            this.struct1_0.ifeature_0 = featureClass1.GetFeature(num1);
            Label label = this.lblPickPipeInfo;
            string[] name = new string[] { this.struct1_0.ifeatureLayer_0.Name, ",", featureClass1.OIDFieldName, "=", num1.ToString() };
            label.Text = string.Concat(name);
            string.Format("\r\n爆管点位置({0:f2},{1:f2},{2:f2})", this.struct0_0.ipoint_0.X, this.struct0_0.ipoint_0.Y, this.struct0_0.ipoint_0.Z);
        }

		private void method_1()
		{
			this.ipoint_0 = null;
			this.struct0_0.igeometricNetwork_0 = null;
			this.struct0_0.ipoint_0 = null;
			this.struct0_0.int_0 = 0;
			this.struct0_0.double_0 = 2147483647.0;
			this.struct1_0.arrayList_1.Clear();
			this.struct1_0.ifeature_0 = null;
			this.struct1_0.ifeatureLayer_0 = null;
			this.struct1_0.ifeatureLayer_1 = null;
			this.struct1_0.arrayList_0.Clear();
			this.treeView1.Nodes.Clear();
			this.listView1.Items.Clear();
		}

		public static void GetMapVisibleILayers(IMap XMap, ILayer ilayer_0, ArrayList Layers)
		{
			if (ilayer_0 == null)
			{
				for (int i = 0; i < XMap.LayerCount; i++)
				{
					ILayer layer = XMap.get_Layer(i);
					if (layer.Visible)
					{
						if (layer is IGroupLayer)
						{
							frmBurstReport.GetMapVisibleILayers(XMap, layer, Layers);
						}
						else
						{
							Layers.Add(layer);
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < ((ICompositeLayer)ilayer_0).Count; j++)
				{
					ILayer layer2 = ((ICompositeLayer)ilayer_0).get_Layer(j);
					if (layer2.Visible)
					{
						if (layer2 is IGroupLayer)
						{
							frmBurstReport.GetMapVisibleILayers(XMap, layer2, Layers);
						}
						else
						{
							Layers.Add(layer2);
						}
					}
				}
			}
		}

		private void method_2(IFeature feature)
		{
			if (feature != null)
			{
				this.listView1.Clear();
				this.listView1.Columns.Add("属性信息", 100, HorizontalAlignment.Center);
				this.listView1.Columns.Add("数值", 140, HorizontalAlignment.Left);
				for (int i = 0; i < feature.Fields.FieldCount; i++)
				{
					string name = feature.Fields.get_Field(i).Name;
					string text = feature.get_Value(i).ToString();
					ListViewItem listViewItem = this.listView1.Items.Add(name);
					listViewItem.SubItems.Add(text);
				}
			}
		}

		private void btnAddFieldValue_Click(object obj, EventArgs eventArgs)
		{
			if (this.listFieldValues.Items.IndexOf(this.comboBox1.Text) == -1)
			{
				this.listFieldValues.Items.Add(this.comboBox1.Text);
			}
		}

		private void btnDelFieldValue_Click(object obj, EventArgs eventArgs)
		{
			if (this.listFieldValues.SelectedItem != null)
			{
				this.listFieldValues.Items.Remove(this.listFieldValues.SelectedItem);
			}
		}

		private void treeView1_MouseClick(object obj, MouseEventArgs mouseEventArgs)
		{
			try
			{
				if (mouseEventArgs.Button != MouseButtons.Right)
				{
					TreeViewHitTestInfo treeViewHitTestInfo = this.treeView1.HitTest(mouseEventArgs.X, mouseEventArgs.Y);
					if (treeViewHitTestInfo != null)
					{
						if (treeViewHitTestInfo.Node.Parent == null)
						{
							if (treeViewHitTestInfo.Node.Nodes.Count > 0)
							{
								this.treeView1.SelectedNode = treeViewHitTestInfo.Node.Nodes[0];
							}
						}
						else if (treeViewHitTestInfo.Node.Parent.Text.Trim() == "爆管的线路")
						{
							this.method_2(this.struct1_0.ifeature_0);
							this.BuoawIbkuD = this.struct1_0.ifeature_0.Shape;
							this.ScaleToGeo(m_iApp.ActiveView, this.BuoawIbkuD);
							this.timer_0.Start();
							this.m_nTimerCounter = 0;
							m_iApp.ActiveView.Refresh();
						}
						else if (treeViewHitTestInfo.Node.Parent.Text.Trim() == "受影响的阀门")
						{
							for (int i = 0; i <= this.struct1_0.arrayList_0.Count - 1; i++)
							{
								IFeature feature = (IFeature)this.struct1_0.arrayList_0[i];
								if (feature.OID == (int)Convert.ToInt16(treeViewHitTestInfo.Node.Text))
								{
									this.BuoawIbkuD = feature.Shape;
									this.method_2(feature);
									this.ScaleToGeo(m_iApp.ActiveView , this.BuoawIbkuD);
									this.timer_0.Start();
									this.m_nTimerCounter = 0;
									m_iApp.ActiveView.Refresh();
									break;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void ScaleToGeo(IActiveView pView, IGeometry pGeo)
		{
			if (pGeo.GeometryType == (esriGeometryType) 1)
			{
				IEnvelope envelope = pGeo.Envelope;
			    IEnvelope extent = pView.Extent;
				double width = extent.Width;
				double height = extent.Height;
				envelope.Expand(width / 2.0, height / 2.0, false);
				pView.Extent=(envelope);
			}
			else
			{
				IEnvelope envelope2 = pGeo.Envelope;
				envelope2.Expand(3.0, 3.0, true);
                pView.Extent=(envelope2);
			}
		}

		private void treeView1_AfterSelect(object obj, TreeViewEventArgs treeViewEventArgs)
		{
		}

		public void PickThroughValve(IPoint _pMousePoint)
		{
			IPointToEID pointToEIDClass = new PointToEID();
			pointToEIDClass.GeometricNetwork=(this.struct0_0.igeometricNetwork_0);
			pointToEIDClass.SourceMap=(m_iApp.FocusMap);
			pointToEIDClass.SnapTolerance=(m_iApp.ActiveView.Extent.Width / 200.0);
			int num = 0;
			IPoint point = null;
			pointToEIDClass.GetNearestJunction(_pMousePoint, out num, out point);
			if (point != null)
			{
				INetElements netElements = (INetElements)this.struct0_0.igeometricNetwork_0.Network;
				int num2;
				int num3;
				int num4;
				netElements.QueryIDs(num, (esriElementType) 1, out num2, out num3, out num4);
				IFeature feature = this.struct1_0.ifeatureLayer_1.FeatureClass.GetFeature(num3);
				this.listOutBarriers.Items.Add(feature.OID.ToString());
			}
		}

		private void btnDelThroughBarrier_Click(object obj, EventArgs eventArgs)
		{
			if (this.listOutBarriers.SelectedItem != null)
			{
				this.listOutBarriers.Items.Remove(this.listOutBarriers.SelectedItem);
			}
		}

		private void btnStartAnalysis_Click(object obj, EventArgs eventArgs)
		{
			if (this.struct0_0.igeometricNetwork_0 != null)
			{
				this.struct1_0.arrayList_1.Clear();
				this.struct1_0.arrayList_0.Clear();
				INetElements netElements = (INetElements)this.struct0_0.igeometricNetwork_0.Network;
				ITraceFlowSolver traceFlowSolver = this.GetTraceFlowSolver();
				IEnumNetEID enumNetEID;
				IEnumNetEID enumNetEID2;
				traceFlowSolver.FindFlowEndElements(this.GetFindMethod(), 0, out enumNetEID, out enumNetEID2);
				int num;
				int num2;
				int num3;
				for (int i = enumNetEID.Next(); i > 0; i = enumNetEID.Next())
				{
					netElements.QueryIDs(i, (esriElementType) 1, out num, out num2, out num3);
					IFeature feature = this.struct1_0.ifeatureLayer_1.FeatureClass.GetFeature(num2);
					int num4 = feature.Fields.FindField(this.label1.Text);
					if (num4 == -1)
					{
						return;
					}
					if (this.listFieldValues.Items.IndexOf(feature.get_Value(num4)) != -1)
					{
						this.struct1_0.arrayList_0.Add(feature);
					}
				}
				traceFlowSolver.FindFlowElements(this.GetFindMethod(), (esriFlowElements) 1, out enumNetEID, out enumNetEID2);
				for (int i = enumNetEID2.Next(); i > 0; i = enumNetEID2.Next())
				{
					netElements.QueryIDs(i, (esriElementType) 2, out num, out num2, out num3);
					IFeature feature2 = this.struct1_0.ifeatureLayer_0.FeatureClass.GetFeature(num2);
					this.struct1_0.arrayList_1.Add(feature2);
				}
				m_iApp.FocusMap.ClearSelection();
				this.method_4();
				this.method_5();
				this.method_7();
			}
		}

		private void treeView1_MouseDoubleClick(object obj, MouseEventArgs mouseEventArgs)
		{
			TreeViewHitTestInfo treeViewHitTestInfo = this.treeView1.HitTest(mouseEventArgs.X, mouseEventArgs.Y);
			if (treeViewHitTestInfo != null && treeViewHitTestInfo.Node.Parent != null)
			{
				IFeature feature = (IFeature)treeViewHitTestInfo.Node.Tag;
				this.listOutBarriers.Items.Add(feature.OID.ToString());
				this.btnStartAnalysis_Click(null, null);
			}
		}

		private void method_3()
		{
			IMarkerSymbol markerSymbol = new SimpleMarkerSymbol();
			IMarkerSymbol arg_2B_0 = markerSymbol;
			IRgbColor rgbColorClass = new RgbColor();
			rgbColorClass.Red=(255);
			rgbColorClass.Green=(0);
			rgbColorClass.Blue=(255);
			arg_2B_0.Color=(rgbColorClass);
			markerSymbol.Size=(15.0);
			IScreenDisplay screenDisplay = m_iApp.ActiveView.ScreenDisplay;
			screenDisplay.StartDrawing(screenDisplay.hDC, 0);
			screenDisplay.SetSymbol((ISymbol)markerSymbol);
			screenDisplay.DrawPoint(this.struct0_0.ipoint_0);
			screenDisplay.FinishDrawing();
		}

		private void method_4()
		{
			IGeometryCollection geometryCollection = new GeometryBag() as IGeometryCollection;
			for (int i = 0; i < this.struct1_0.arrayList_1.Count; i++)
			{
				object missing = Type.Missing;
				IFeature feature = (IFeature)this.struct1_0.arrayList_1[i];
				geometryCollection.AddGeometry(feature.ShapeCopy, ref missing, ref missing);
			}
			if (this.struct1_0.arrayList_1.Count > 0)
			{
				IRgbColor rgbColor = new RgbColor();
				rgbColor.Red=(255);
				rgbColor.Green=(0);
				rgbColor.Blue=(255);
				IGraphicsContainer graphicsContainer = (IGraphicsContainer)m_iApp.ActiveView;
				for (int j = 0; j < geometryCollection.GeometryCount; j++)
				{
					ILineElement lineElement = new LineElement() as ILineElement;
					IElement element = (IElement)lineElement;
					element.Geometry=(geometryCollection.get_Geometry(j));
					ILineElement arg_F5_0 = lineElement;
					ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
					simpleLineSymbolClass.Color=(rgbColor);
					simpleLineSymbolClass.Width=(10.0);
					simpleLineSymbolClass.Style=(0);
					arg_F5_0.Symbol=(simpleLineSymbolClass);
					graphicsContainer.AddElement(element, 0);
				}
				IEnvelope envelope = ((IGeometry)geometryCollection).Envelope;
				envelope.Expand(1.2, 1.2, true);
				m_iApp.ActiveView.Extent=(envelope);
			}
		}

		private void method_5()
		{
			IGeometryCollection geometryCollection = new Multipoint() as IGeometryCollection;
			for (int i = 0; i < this.struct1_0.arrayList_0.Count; i++)
			{
				object missing = Type.Missing;
				IFeature feature = (IFeature)this.struct1_0.arrayList_0[i];
				geometryCollection.AddGeometry(feature.ShapeCopy, ref missing, ref missing);
			}
			if (this.struct1_0.arrayList_0.Count > 0)
			{
				IGraphicsContainer graphicsContainer = (IGraphicsContainer)m_iApp.ActiveView;
				IMarkerSymbol markerSymbol = this.method_6();
				markerSymbol.Size=(26.0);
				for (int j = 0; j < geometryCollection.GeometryCount; j++)
				{
					IMarkerElement markerElement = new MarkerElement() as IMarkerElement;
					IElement element = (IElement)markerElement;
					element.Geometry=(geometryCollection.get_Geometry(j));
					markerElement.Symbol=(markerSymbol);
					graphicsContainer.AddElement(element, 0);
				}
				m_iApp.ActiveView.PartialRefresh((esriViewDrawPhase) 8, null, null);
			}
		}

		private IMarkerSymbol method_6()
		{
            IMarkerSymbol markerSymbol;
            IStyleGallery serverStyleGalleryClass = new ServerStyleGallery();
            IMarkerSymbol item = null;
            bool flag = false;
            string str = string.Concat(Application.StartupPath, "\\Style\\ESRI.ServerStyle");
            if (!File.Exists(str))
            {
                markerSymbol = null;
            }
            else
            {
                string str1 = str;
                serverStyleGalleryClass.Clear();
                (serverStyleGalleryClass as IStyleGalleryStorage).AddFile(str1);
                IEnumStyleGalleryItem items = serverStyleGalleryClass.Items["Marker Symbols", str1, "Default"];
                items.Reset();
                IStyleGalleryItem styleGalleryItem = items.Next();
                while (true)
                {
                    if (styleGalleryItem == null)
                    {
                        break;
                    }
                    else if (styleGalleryItem.Name == "Star 5")
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        styleGalleryItem = items.Next();
                    }
                }
                if (flag)
                {
                    item = (IMarkerSymbol)styleGalleryItem.Item;
                }
                item.Size = 19;
                markerSymbol = item;
            }
            return markerSymbol;
        }

		private void method_7()
		{
			this.treeView1.Nodes.Clear();
			TreeNode treeNode = this.treeView1.Nodes.Add("爆管的线路");
			TreeNode treeNode2 = treeNode.Nodes.Add(this.struct1_0.ifeature_0.OID.ToString());
			treeNode2.Tag = this.struct1_0.ifeature_0;
			this.treeView1.SelectedNode = treeNode2;
			treeNode.Expand();
			TreeNode treeNode3 = this.treeView1.Nodes.Add("受影响的阀门");
			for (int i = 0; i < this.struct1_0.arrayList_0.Count; i++)
			{
				IFeature feature = (IFeature)this.struct1_0.arrayList_0[i];
				TreeNode treeNode4 = treeNode3.Nodes.Add(feature.OID.ToString());
				treeNode4.Tag = feature;
			}
			treeNode3.Expand();
			treeNode.EnsureVisible();
		}

		private void btnCancel_Click(object obj, EventArgs eventArgs)
		{
			string str = "";
			for (int i = 0; i < this.listFieldValues.Items.Count; i++)
			{
				str = str + this.listFieldValues.Items[i].ToString() + "/";
			}
			((IMapControlEvents2_Event)m_iApp.MapControl).OnMouseDown-= OnOnMouseDown;
		}

	    private void OnOnMouseDown(int button, int shift, int i, int i1, double mapX, double mapY)
	    {
            if ((button != 1 ? false :m_iApp.MapControl.CurrentTool == null))
            {
                this.SetMousePoint(i, i1);
            }
        }

	    protected override void OnClosing(CancelEventArgs e)
		{
		}

		public static void GetValues(ArrayList Values)
		{
			Values.Clear();
			string text = CRegOperator.GetRegistryKey().GetValue("节点性质字段值", "").ToString();
			for (int num = text.IndexOf("/"); num != -1; num = text.IndexOf("/"))
			{
				Values.Add(text.Substring(0, num));
				text = text.Substring(num + 1, text.Length - num - 1);
			}
		}

		private void btnAddThroughValve_CheckedChanged(object obj, EventArgs eventArgs)
		{
		}

		private void timer_0_Tick(object obj, EventArgs eventArgs)
		{
			this.m_nTimerCounter++;
			if (this.m_nTimerCounter > 9)
			{
				this.timer_0.Stop();
				this.m_nTimerCounter = 0;
			}
			else
			{
				this.FlashDstItem();
			}
		}

		public void FlashDstItem()
		{
			IMapControl3 mapControl = m_iApp.MapControl as IMapControl3;
			CRandomColor cRandomColor = new CRandomColor();
			Color randColor = cRandomColor.GetRandColor();
			ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
			IRgbColor rgbColor = new RgbColor();
			rgbColor.Red=((int)randColor.R);
			rgbColor.Green=((int)randColor.G);
			rgbColor.Blue=((int)randColor.B);
			simpleLineSymbol.Color=(rgbColor);
			simpleLineSymbol.Width=(5.0);
			object obj = simpleLineSymbol;
			ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol();
			simpleMarkerSymbolClass.Color=(rgbColor);
			simpleMarkerSymbolClass.Size=(10.0);
			simpleMarkerSymbolClass.Style=(0);
			object obj2 = simpleMarkerSymbolClass;
			try
			{
				if (this.BuoawIbkuD.GeometryType == (esriGeometryType) 1)
				{
					mapControl.DrawShape(this.BuoawIbkuD, ref obj2);
				}
				if (this.BuoawIbkuD.GeometryType == (esriGeometryType) 3)
				{
					mapControl.DrawShape(this.BuoawIbkuD, ref obj);
				}
			}
			catch
			{
			}
		}

		private void frmBurstReport_FormClosed(object obj, FormClosedEventArgs formClosedEventArgs)
		{
			string str = "";
			for (int i = 0; i < this.listFieldValues.Items.Count; i++)
			{
				str = str + this.listFieldValues.Items[i].ToString() + "/";
			}
			if (this.checkReflushView.Checked)
			{
			    IGraphicsContainer graphicsContainer = (IGraphicsContainer) m_iApp.ActiveView;
				graphicsContainer.DeleteAllElements();
			}
		    m_iApp.MapControl.CurrentTool = null;
			m_iApp.ActiveView.PartialRefresh((esriViewDrawPhase) 8, null, null);
		}

		private void frmBurstReport_HelpRequested(object obj, HelpEventArgs helpEventArgs)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "关阀分析";
			Help.ShowHelp(this, url, HelpNavigator.KeywordIndex, parameter);
		}

		private void btnPickBrokePipe_Click(object obj, EventArgs eventArgs)
		{
            ((IMapControlEvents2_Event)m_iApp.MapControl).OnMouseDown += OnOnMouseDown;
        }

		private void method_8(object obj, IMapControlEvents2_OnMouseDownEvent mapControlEvents2_OnMouseDownEvent)
		{
			if (mapControlEvents2_OnMouseDownEvent.button == 1 && m_iApp.MapControl.CurrentTool == null)
			{
				this.SetMousePoint(mapControlEvents2_OnMouseDownEvent.x, mapControlEvents2_OnMouseDownEvent.y);
			}
		}

		private void ClearGra_Click(object obj, EventArgs eventArgs)
		{
		    IGraphicsContainer graphicsContainer = (IGraphicsContainer) m_iApp.ActiveView;
			graphicsContainer.DeleteAllElements();
		}

		private void btnPickBrokePipe_CheckedChanged(object obj, EventArgs eventArgs)
		{
		}
	}
}
