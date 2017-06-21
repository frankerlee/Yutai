using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;


namespace Yutai.Pipeline.Analysis.Forms
{

	public class BufferAnalyseDlg : XtraForm
	{
		private double double_0;

		public IAppContext m_app;

		public ArrayList m_alCheckedLayerNames;

		public ArrayList m_alCheckedLayer;

		public IGeometry m_pDrawGeo;

		public IGeometry m_pBufferGeo;

		private ResultDialog resultDialog_0;

		public bool bDrawRed;

		private IContainer icontainer_0 = null;

		private Label label1;

		private TextBox txBoxRadius;

		private Button btnAnalyse;

		private Button btnClose;

		private GroupBox 分析对象选择;

		private CheckedListBox chkLstLayers;

		private RadioButton radBtnOther;

		private RadioButton radBtnLn;

		private RadioButton radBtnPt;

		private Button button1;

		private Button btnSelectNone;

		private Button btnConvertSelect;

		private Button btnSelectAll;

		private CheckBox bGeo;

		public double Radius
		{
			get
			{
				return this.double_0;
			}
			set
			{
				this.double_0 = value;
			}
		}

		public bool SelectGeometry
		{
			get
			{
				return this.bGeo.Checked;
			}
		}

		public BufferAnalyseDlg()
		{
			this.InitializeComponent();
		}

		public void AddName(ILayer pLayer)
		{
			try
			{
				if (pLayer != null)
				{
					IFeatureLayer featureLayer = pLayer as IFeatureLayer;
					CheckListFeatureLayerItem checkListFeatureLayerItem = new CheckListFeatureLayerItem()
					{
						m_pFeatureLayer = featureLayer
					};
					IFeatureClass featureClass = featureLayer.FeatureClass;
					if (featureLayer.FeatureClass.FeatureType != (esriFeatureType) 11)
					{
						if ((!this.radBtnPt.Checked ? false : this.m_app.PipeConfig.IsPipePoint(featureClass.AliasName)))
						{
							this.chkLstLayers.Items.Add(checkListFeatureLayerItem);
						}
						if ((!this.radBtnLn.Checked ? false : this.m_app.PipeConfig.IsPipeLine(featureClass.AliasName)))
						{
							this.chkLstLayers.Items.Add(checkListFeatureLayerItem);
						}
						if ((!this.radBtnOther.Checked || this.m_app.PipeConfig.IsPipeLine(featureClass.AliasName) ? false : !this.m_app.PipeConfig.IsPipePoint(featureClass.AliasName)))
						{
							this.chkLstLayers.Items.Add(checkListFeatureLayerItem);
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void bGeo_CheckedChanged(object obj, EventArgs eventArg)
		{
			if (!this.bGeo.Checked)
			{
				this.m_pDrawGeo = null;
				this.m_app.ActiveView.PartialRefresh((esriViewDrawPhase)32, null, null);
			}
		}

		private void bGeo_Click(object obj, EventArgs eventArg)
		{
		}

		private void btnAnalyse_Click(object obj, EventArgs eventArg)
		{
			double num = Convert.ToDouble(this.txBoxRadius.Text.Trim());
			CommonUtils.DeleteAllElements(this.m_app.FocusMap);
			this.method_0(num);
			this.bDrawRed = false;
		}

		private void btnClose_Click(object obj, EventArgs eventArg)
		{
			base.Visible = false;
			CommonUtils.DeleteAllElements(this.m_app.FocusMap);
			base.Close();
		}

		private void btnConvertSelect_Click(object obj, EventArgs eventArg)
		{
			int count = this.chkLstLayers.Items.Count;
			for (int i = 0; i < count; i++)
			{
				bool itemChecked = this.chkLstLayers.GetItemChecked(i);
				this.chkLstLayers.SetItemChecked(i, !itemChecked);
			}
		}

		private void btnSelectAll_Click(object obj, EventArgs eventArg)
		{
			int count = this.chkLstLayers.Items.Count;
			for (int i = 0; i < count; i++)
			{
				this.chkLstLayers.SetItemChecked(i, true);
			}
		}

		private void btnSelectNone_Click(object obj, EventArgs eventArg)
		{
			int count = this.chkLstLayers.Items.Count;
			for (int i = 0; i < count; i++)
			{
				this.chkLstLayers.SetItemChecked(i, false);
			}
		}

		private void BufferAnalyseDlg_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
		{
			base.Visible = false;
			formClosingEventArg.Cancel = true;
			CommonUtils.DeleteAllElements(this.m_app.FocusMap);
		}

		private void BufferAnalyseDlg_Load(object obj, EventArgs eventArg)
		{
			this.method_1();
		}

		private void BufferAnalyseDlg_VisibleChanged(object obj, EventArgs eventArg)
		{
			if (!base.Visible)
			{
				IMapControlEvents2_Event _axMapControl = this.m_app.MapControl as IMapControlEvents2_Event;
				_axMapControl.OnAfterDraw-= AxMapControlOnOnAfterDraw;
            }
			else
			{
                IMapControlEvents2_Event axMapControl = this.m_app.MapControl as IMapControlEvents2_Event;
				axMapControl.OnAfterDraw+= AxMapControlOnOnAfterDraw;
			}
		}

	    private void AxMapControlOnOnAfterDraw(object display, int viewDrawPhase)
	    {
            if ((viewDrawPhase != 32 ? false : this.bDrawRed))
            {
                this.DrawSelGeometry();
            }
        }

	    private void button1_Click(object obj, EventArgs eventArg)
		{
			this.method_3();
			double num = Convert.ToDouble(this.txBoxRadius.Text.Trim());
			CommonUtils.DeleteAllElements(this.m_app.FocusMap);
			this.method_0(num);
			this.XbwqoohXht();
		}

		private void chkLstLayers_Click(object obj, EventArgs eventArg)
		{
		}

		private void chkLstLayers_ItemCheck(object obj, ItemCheckEventArgs itemCheckEventArg)
		{
			if (!this.radBtnPt.Checked && !this.radBtnLn.Checked && this.radBtnOther.Checked && itemCheckEventArg.CurrentValue != CheckState.Checked)
			{
				for (int i = 0; i < ((CheckedListBox)obj).Items.Count; i++)
				{
					((CheckedListBox)obj).SetItemChecked(i, false);
				}
				itemCheckEventArg.NewValue = CheckState.Checked;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.icontainer_0 != null))
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		public void DrawSelGeometry()
		{
            if (this.m_pDrawGeo != null)
            {
                IRgbColor rgbColor = new RgbColor();
                IRgbColor selectionCorlor = this.m_app.Config.SelectionEnvironment.DefaultColor as IRgbColor;
                rgbColor.Blue=((int)selectionCorlor.Blue);
                rgbColor.Green=((int)selectionCorlor.Green);
                rgbColor.Red=((int)selectionCorlor.Red);
                rgbColor.Transparency=(selectionCorlor.Transparency);
                object obj = null;
                int selectionBufferInPixels = this.m_app.Config.SelectionEnvironment.SearchTolerance;
                ISymbol symbol = null;
                switch ((int)this.m_pDrawGeo.GeometryType)
                {
                    case 1:
                        {
                            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
                            symbol = (ISymbol)simpleMarkerSymbol;
                            symbol.ROP2=(esriRasterOpCode)  (10);
                            simpleMarkerSymbol.Color=(rgbColor);
                            simpleMarkerSymbol.Size=((double)(selectionBufferInPixels + selectionBufferInPixels + selectionBufferInPixels));
                            break;
                        }
                    case 3:
                        {
                            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                            symbol = (ISymbol)simpleLineSymbol;
                            symbol.ROP2=(esriRasterOpCode) (10);
                            simpleLineSymbol.Color=(rgbColor);
                            simpleLineSymbol.Color.Transparency=(1);
                            simpleLineSymbol.Width=((double)selectionBufferInPixels);
                            break;
                        }
                    case 4:
                    case 5:
                        {
                            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
                            symbol = (ISymbol)simpleFillSymbol;
                            symbol.ROP2=(esriRasterOpCode) (10);
                            simpleFillSymbol.Color=(rgbColor);
                            simpleFillSymbol.Color.Transparency=(1);
                            break;
                        }
                }
                obj = symbol;
                this.m_app.MapControl.DrawShape(this.m_pDrawGeo, ref obj);
            }
        }

		private void EoQqirFnxu(IGeometry geometry, double num)
		{
			this.m_pBufferGeo = ((ITopologicalOperator)geometry).Buffer(num);
			CommonUtils.NewPolygonElementTran(this.m_app.FocusMap, this.m_pBufferGeo as IPolygon, false);
		    IActiveView activeView = this.m_app.ActiveView;
			activeView.PartialRefresh((esriViewDrawPhase)8, null, null);
		}

		private void hbAqYjDqsa(object obj, HelpEventArgs helpEventArg)
		{
			string str = string.Concat(Application.StartupPath, "\\帮助.chm");
			Help.ShowHelp(this, str, HelpNavigator.KeywordIndex, "缓冲分析");
		}

		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.txBoxRadius = new TextBox();
			this.btnAnalyse = new Button();
			this.btnClose = new Button();
			this.分析对象选择 = new GroupBox();
			this.radBtnOther = new RadioButton();
			this.radBtnLn = new RadioButton();
			this.radBtnPt = new RadioButton();
			this.chkLstLayers = new CheckedListBox();
			this.button1 = new Button();
			this.btnSelectNone = new Button();
			this.btnConvertSelect = new Button();
			this.btnSelectAll = new Button();
			this.bGeo = new CheckBox();
			this.分析对象选择.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(262, 257);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "缓冲区半径：";
			this.txBoxRadius.Location = new System.Drawing.Point(351, 254);
			this.txBoxRadius.Name = "txBoxRadius";
			this.txBoxRadius.Size = new System.Drawing.Size(67, 21);
			this.txBoxRadius.TabIndex = 1;
			this.txBoxRadius.Text = "10";
			this.txBoxRadius.KeyPress += new KeyPressEventHandler(this.txBoxRadius_KeyPress);
			this.btnAnalyse.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnAnalyse.Location = new System.Drawing.Point(124, 291);
			this.btnAnalyse.Name = "btnAnalyse";
			this.btnAnalyse.Size = new System.Drawing.Size(100, 23);
			this.btnAnalyse.TabIndex = 2;
			this.btnAnalyse.Text = "查看缓冲区(&O)";
			this.btnAnalyse.UseVisualStyleBackColor = true;
			this.btnAnalyse.Click += new EventHandler(this.btnAnalyse_Click);
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(365, 300);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "关闭(&C)";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.分析对象选择.Controls.Add(this.radBtnOther);
			this.分析对象选择.Controls.Add(this.radBtnLn);
			this.分析对象选择.Controls.Add(this.radBtnPt);
			this.分析对象选择.Location = new System.Drawing.Point(264, 18);
			this.分析对象选择.Name = "分析对象选择";
			this.分析对象选择.Size = new System.Drawing.Size(155, 114);
			this.分析对象选择.TabIndex = 21;
			this.分析对象选择.TabStop = false;
			this.分析对象选择.Text = "分析对象 方式";
			this.radBtnOther.AutoSize = true;
			this.radBtnOther.Location = new System.Drawing.Point(25, 78);
			this.radBtnOther.Name = "radBtnOther";
			this.radBtnOther.Size = new System.Drawing.Size(89, 16);
			this.radBtnOther.TabIndex = 21;
			this.radBtnOther.Text = "其它数据(&D)";
			this.radBtnOther.UseVisualStyleBackColor = true;
			this.radBtnOther.CheckedChanged += new EventHandler(this.radBtnOther_CheckedChanged);
			this.radBtnLn.AutoSize = true;
			this.radBtnLn.Location = new System.Drawing.Point(25, 48);
			this.radBtnLn.Name = "radBtnLn";
			this.radBtnLn.Size = new System.Drawing.Size(89, 16);
			this.radBtnLn.TabIndex = 20;
			this.radBtnLn.Text = "管线数据(&L)";
			this.radBtnLn.UseVisualStyleBackColor = true;
			this.radBtnLn.CheckedChanged += new EventHandler(this.radBtnLn_CheckedChanged);
			this.radBtnPt.AutoSize = true;
			this.radBtnPt.Checked = true;
			this.radBtnPt.Location = new System.Drawing.Point(25, 20);
			this.radBtnPt.Name = "radBtnPt";
			this.radBtnPt.Size = new System.Drawing.Size(101, 16);
			this.radBtnPt.TabIndex = 19;
			this.radBtnPt.TabStop = true;
			this.radBtnPt.Text = "管线点数据(&P)";
			this.radBtnPt.UseVisualStyleBackColor = true;
			this.radBtnPt.CheckedChanged += new EventHandler(this.radBtnPt_CheckedChanged);
			this.chkLstLayers.CheckOnClick = true;
			this.chkLstLayers.FormattingEnabled = true;
			this.chkLstLayers.Location = new System.Drawing.Point(12, 9);
			this.chkLstLayers.Name = "chkLstLayers";
			this.chkLstLayers.Size = new System.Drawing.Size(206, 260);
			this.chkLstLayers.TabIndex = 20;
			this.chkLstLayers.ItemCheck += new ItemCheckEventHandler(this.chkLstLayers_ItemCheck);
			this.chkLstLayers.Click += new EventHandler(this.chkLstLayers_Click);
			this.button1.Location = new System.Drawing.Point(229, 291);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(102, 23);
			this.button1.TabIndex = 22;
			this.button1.Text = "查看明细(&V)";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.btnSelectNone.Location = new System.Drawing.Point(263, 195);
			this.btnSelectNone.Name = "btnSelectNone";
			this.btnSelectNone.Size = new System.Drawing.Size(77, 28);
			this.btnSelectNone.TabIndex = 25;
			this.btnSelectNone.Text = "全不选(&N)";
			this.btnSelectNone.UseVisualStyleBackColor = true;
			this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
			this.btnConvertSelect.Location = new System.Drawing.Point(351, 149);
			this.btnConvertSelect.Name = "btnConvertSelect";
			this.btnConvertSelect.Size = new System.Drawing.Size(77, 28);
			this.btnConvertSelect.TabIndex = 24;
			this.btnConvertSelect.Text = "反选(&I)";
			this.btnConvertSelect.UseVisualStyleBackColor = true;
			this.btnConvertSelect.Click += new EventHandler(this.btnConvertSelect_Click);
			this.btnSelectAll.Location = new System.Drawing.Point(263, 149);
			this.btnSelectAll.Name = "btnSelectAll";
			this.btnSelectAll.Size = new System.Drawing.Size(77, 28);
			this.btnSelectAll.TabIndex = 23;
			this.btnSelectAll.Text = "全选(&A)";
			this.btnSelectAll.UseVisualStyleBackColor = true;
			this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
			this.bGeo.AutoSize = true;
			this.bGeo.Location = new System.Drawing.Point(12, 295);
			this.bGeo.Name = "bGeo";
			this.bGeo.Size = new System.Drawing.Size(96, 16);
			this.bGeo.TabIndex = 26;
			this.bGeo.Text = "绘制空间范围";
			this.bGeo.UseVisualStyleBackColor = true;
			this.bGeo.Click += new EventHandler(this.bGeo_Click);
			this.bGeo.CheckedChanged += new EventHandler(this.bGeo_CheckedChanged);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(452, 335);
			base.Controls.Add(this.bGeo);
			base.Controls.Add(this.btnSelectNone);
			base.Controls.Add(this.btnConvertSelect);
			base.Controls.Add(this.btnSelectAll);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.分析对象选择);
			base.Controls.Add(this.chkLstLayers);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnAnalyse);
			base.Controls.Add(this.txBoxRadius);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "BufferAnalyseDlg";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "缓冲区分析";
			base.TopMost = true;
			base.Load += new EventHandler(this.BufferAnalyseDlg_Load);
			base.VisibleChanged += new EventHandler(this.BufferAnalyseDlg_VisibleChanged);
			base.FormClosing += new FormClosingEventHandler(this.BufferAnalyseDlg_FormClosing);
			this.分析对象选择.ResumeLayout(false);
			this.分析对象选择.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void method_0(double num)
		{
			if (!this.bGeo.Checked)
			{
				IMap map = this.m_app.FocusMap;
				IEnumFeature featureSelection = (IEnumFeature)map.FeatureSelection;
				IFeature feature = featureSelection.Next();
				if (feature != null)
				{
					while (feature != null)
					{
						this.EoQqirFnxu(feature.Shape, num);
						feature = featureSelection.Next();
					}
				}
				else
				{
					MessageBox.Show("请确定范围！");
				}
			}
			else if (this.m_pDrawGeo != null)
			{
				this.EoQqirFnxu(this.m_pDrawGeo, num);
			}
			else
			{
				MessageBox.Show("请确定范围！");
			}
		}

		private void method_1()
		{
			this.chkLstLayers.Items.Clear();
			CommonUtils.ThrougAllLayer(this.m_app.FocusMap, new CommonUtils.DealLayer(this.AddName));
		}

		private bool method_2(string str)
		{
			bool flag;
			int count = this.m_alCheckedLayerNames.Count;
			int num = 0;
			while (true)
			{
				if (num < count)
				{
					string item = this.m_alCheckedLayerNames[num] as string;
					if (str.Trim().ToUpper() == item.Trim().ToUpper())
					{
						flag = true;
						break;
					}
					else
					{
						num++;
					}
				}
				else
				{
					flag = false;
					break;
				}
			}
			return flag;
		}

		private void method_3()
		{
			this.m_alCheckedLayer = new ArrayList();
			int count = this.chkLstLayers.CheckedItems.Count;
			this.m_alCheckedLayer.Clear();
			for (int i = 0; i < count; i++)
			{
				CheckListFeatureLayerItem item = this.chkLstLayers.CheckedItems[i] as CheckListFeatureLayerItem;
				this.m_alCheckedLayer.Add(item.m_pFeatureLayer);
			}
		}

		

		private void radBtnLn_CheckedChanged(object obj, EventArgs eventArg)
		{
			this.btnSelectAll.Enabled = true;
			this.btnConvertSelect.Enabled = true;
			this.btnSelectNone.Enabled = true;
			this.method_1();
		}

		private void radBtnOther_CheckedChanged(object obj, EventArgs eventArg)
		{
			this.btnSelectAll.Enabled = false;
			this.btnConvertSelect.Enabled = false;
			this.btnSelectNone.Enabled = false;
			this.method_1();
		}

		private void radBtnPt_CheckedChanged(object obj, EventArgs eventArg)
		{
			this.btnSelectAll.Enabled = true;
			this.btnConvertSelect.Enabled = true;
			this.btnSelectNone.Enabled = true;
			this.method_1();
		}

		private void txBoxRadius_KeyPress(object obj, KeyPressEventArgs keyPressEventArg)
		{
			//ApplicationData.CommonUtils.NumberText_KeyPress(obj, keyPressEventArg);
		}

		private void XbwqoohXht()
		{
			if (this.resultDialog_0 == null)
			{
				this.resultDialog_0 = new ResultDialog()
				{
					App = this.m_app,
					m_pBufferGeo = this.m_pBufferGeo,
					m_alLayers = this.m_alCheckedLayer
				};
				this.resultDialog_0.Show();
			}
			else if (!this.resultDialog_0.Visible)
			{
				this.resultDialog_0.m_pBufferGeo = this.m_pBufferGeo;
				this.resultDialog_0.m_alLayers = this.m_alCheckedLayer;
				this.resultDialog_0.ThrougAllLayer();
				this.resultDialog_0.Visible = true;
			}
			else
			{
				this.resultDialog_0.m_pBufferGeo = this.m_pBufferGeo;
				this.resultDialog_0.m_alLayers = this.m_alCheckedLayer;
				this.resultDialog_0.ThrougAllLayer();
			}
		}
	}
}