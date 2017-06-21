using ApplicationData;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using PipeConfig;
using RandomColor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.Forms
{
	public class VerDistDlg : XtraForm
	{
		private IContainer icontainer_0 = null;

		private Button btAnalyse;

		private TextBox tbBufferRadius;

		private Label label1;

		private DataGridView dataGridView1;

		private Timer timer_0;

		private Button btClose;

		private DataGridViewTextBoxColumn 序号;

		private DataGridViewTextBoxColumn PipeDataset;

		private DataGridViewTextBoxColumn FID;

		private DataGridViewTextBoxColumn verDist;

		private DataGridViewTextBoxColumn stanDist;

		private CHitAnalyse chitAnalyse_0 = new CHitAnalyse();

		public IProjectFrameworkApp m_app;

		public CommonDistAnalyse m_commonDistAls;

		private IPolyline ipolyline_0;

		public int m_nTimerCount;

		public IGeometry m_pFlashGeo;

		public VerDistDlg(IProjectFrameworkApp pApp)
		{
			this.InitializeComponent();
			this.m_commonDistAls = new CommonDistAnalyse()
			{
				m_nAnalyseType = DistAnalyseType.emVerDist
			};
			this.m_app = pApp;
			this.m_commonDistAls.PipeConfig = pApp.PipeConfig;
			this.m_nTimerCount = 0;
		}

		private void btAnalyse_Click(object obj, EventArgs eventArg)
		{
			double num = 0;
			if (this.tbBufferRadius.Text != "")
			{
				num = Convert.ToDouble(this.tbBufferRadius.Text);
			}
			this.btAnalyse.Enabled = false;
			this.Cursor = Cursors.WaitCursor;
			try
			{
				this.chitAnalyse_0.BufferDistance = num;
				this.chitAnalyse_0.Analyse_Vertical();
				List<CHitAnalyse.CItem> items = this.chitAnalyse_0.Items;
				this.dataGridView1.Rows.Clear();
				DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle()
				{
					BackColor = Color.Red
				};
				if (items.Count > 0)
				{
					this.dataGridView1.Rows.Add(items.Count);
					for (int i = 0; i < items.Count; i++)
					{
						CHitAnalyse.CItem item = items[i];
						DataGridViewRow str = this.dataGridView1.Rows[i];
						str.Tag = item._pClass;
						int num1 = i + 1;
						str.Cells[0].Value = num1.ToString();
						str.Cells[1].Value = item._sKind;
						str.Cells[2].Value = item._OID.ToString();
						str.Cells[3].Value = item._dVerDistance.ToString("0.000");
						if (item._dVerBase > 0.001)
						{
							str.Cells[4].Value = item._dVerBase.ToString("0.000");
						}
						if (item._dVerDistance < item._dVerBase)
						{
							str.Cells[3].Style = dataGridViewCellStyle;
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
			this.Cursor = Cursors.Default;
			this.btAnalyse.Enabled = true;
		}

		private void btClose_Click(object obj, EventArgs eventArg)
		{
			base.Visible = false;
		}

		private void dataGridView1_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
		{
			int rowIndex = dataGridViewCellEventArg.RowIndex;
			if (rowIndex >= 0)
			{
				int num = Convert.ToInt32(this.dataGridView1[2, rowIndex].Value);
				DataGridViewRow item = this.dataGridView1.Rows[dataGridViewCellEventArg.RowIndex];
				IFeatureClass tag = (IFeatureClass)item.Tag;
				if (tag != null)
				{
					IFeature feature = tag.GetFeature(num);
					if (feature != null)
					{
						this.m_pFlashGeo = feature.Shape;
					}
				}
				CommonUtils.ScaleToTwoGeo(this.m_app.FocusMap, this.m_commonDistAls.m_pBaseLine, this.m_pFlashGeo);
				this.timer_0.Start();
				this.timer_0.Interval = 300;
				this.m_app.FocusMap.get_ActiveView().Refresh();
				this.m_nTimerCount = 0;
				this.FlashDstItem();
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

		public void FlashDstItem()
		{
			IMapControl3 mapControl = this.m_app.FocusMap;
			Color randColor = (new RandomColor.CRandomColor()).GetRandColor();
			ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
			IRgbColor rgbColorClass = new RgbColor();
			rgbColorClass.Red=((int)randColor.R);
			rgbColorClass.Green=((int)randColor.G);
			rgbColorClass.Blue=((int)randColor.B);
			simpleLineSymbolClass.Color=(rgbColorClass);
			simpleLineSymbolClass.Width=(5);
			object obj = simpleLineSymbolClass;
			ISimpleLineSymbol simpleLineSymbolClass1 = new SimpleLineSymbol();
			IRgbColor rgbColorClass1 = new RgbColor();
			rgbColorClass1.Red=(255);
			rgbColorClass1.Green=(0);
			rgbColorClass1.Blue=(0);
			simpleLineSymbolClass1.Color=(rgbColorClass1);
			simpleLineSymbolClass1.Width=(5);
			object obj1 = simpleLineSymbolClass1;
			try
			{
				mapControl.DrawShape(this.m_pFlashGeo, ref obj);
				if (this.m_commonDistAls.m_pBaseLine != null)
				{
					mapControl.DrawShape(this.m_commonDistAls.m_pBaseLine, ref obj1);
				}
			}
			catch
			{
			}
		}

		public void GetBaseLine()
		{
			string str;
			string str1;
			string str2;
			this.timer_0.Stop();
			this.dataGridView1.Rows.Clear();
			IMap map = this.m_app.FocusMap.get_Map();
			IEnumFeature featureSelection = (IEnumFeature)map.FeatureSelection;
			featureSelection.Reset();
			IFeature feature = featureSelection.Next();
			if ((feature == null ? false : feature.FeatureType == 8))
			{
				CommonUtils.GetSmpClassName(feature.Class.AliasName);
				if (this.m_app.PipeConfig.IsPipeLine(feature.Class.AliasName))
				{
					IGeometry shape = feature.Shape;
					if (shape.GeometryType == 3)
					{
						this.ipolyline_0 = CommonUtils.GetPolylineDeepCopy((IPolyline)shape);
						this.m_commonDistAls.m_pFeature = feature;
						this.m_commonDistAls.m_pBaseLine = this.ipolyline_0;
						this.m_commonDistAls.m_strLayerName = feature.Class.AliasName;
						int num = feature.Fields.FindField("埋设方式");
						str = (num == -1 ? "" : this.method_0(feature.get_Value(num)));
						this.m_commonDistAls.m_strBuryKind = str;
						int num1 = feature.Fields.FindField(this.m_app.PipeConfig.get_Diameter());
						str1 = (num1 == -1 ? "" : this.method_0(feature.get_Value(num1)));
						num1 = feature.Fields.FindField(this.m_app.PipeConfig.get_Section_Size());
						str2 = (num1 == -1 ? "" : this.method_0(feature.get_Value(num1)));
						string str3 = "";
						if (str1 != "")
						{
							str3 = str1;
						}
						if (str2 != "")
						{
							str3 = str2;
						}
						this.m_commonDistAls.m_dDiameter = this.m_commonDistAls.GetDiameterFromString(str3.Trim());
						IEdgeFeature edgeFeature = (IEdgeFeature)feature;
						this.m_commonDistAls.m_nBaseLineFromID = edgeFeature.FromJunctionEID;
						this.m_commonDistAls.m_nBaseLineToID = edgeFeature.ToJunctionEID;
						this.btAnalyse.Enabled = this.m_commonDistAls.m_pBaseLine != null;
						this.chitAnalyse_0.PipeLayer_Class = feature.Class as IFeatureClass;
						this.chitAnalyse_0.BaseLine_OID = feature.OID;
					}
					else
					{
						MessageBox.Show("所选择的管线多于一条，或者不是管线！");
					}
				}
				else
				{
					this.m_commonDistAls.m_pBaseLine = null;
					this.btAnalyse.Enabled = false;
					this.m_app.FocusMap.get_Map().ClearSelection();
					this.m_app.FocusMap.get_ActiveView().Refresh();
				}
			}
			else
			{
				this.m_commonDistAls.m_pBaseLine = null;
				this.btAnalyse.Enabled = false;
				this.m_app.FocusMap.get_Map().ClearSelection();
				this.m_app.FocusMap.get_ActiveView().Refresh();
			}
		}

		public void InitDistAnalyseDlg()
		{
			this.m_app.FocusMap.get_Map().ClearSelection();
			this.m_app.FocusMap.get_ActiveView().Refresh();
			this.dataGridView1.Rows.Clear();
			this.btAnalyse.Enabled = false;
		}

		private void InitializeComponent()
		{
			this.icontainer_0 = new System.ComponentModel.Container();
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle white = new DataGridViewCellStyle();
			this.btAnalyse = new Button();
			this.tbBufferRadius = new TextBox();
			this.label1 = new Label();
			this.dataGridView1 = new DataGridView();
			this.序号 = new DataGridViewTextBoxColumn();
			this.PipeDataset = new DataGridViewTextBoxColumn();
			this.FID = new DataGridViewTextBoxColumn();
			this.verDist = new DataGridViewTextBoxColumn();
			this.stanDist = new DataGridViewTextBoxColumn();
			this.timer_0 = new Timer(this.icontainer_0);
			this.btClose = new Button();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.btAnalyse.Enabled = false;
			this.btAnalyse.Location = new System.Drawing.Point(215, 188);
			this.btAnalyse.Name = "btAnalyse";
			this.btAnalyse.Size = new System.Drawing.Size(75, 23);
			this.btAnalyse.TabIndex = 7;
			this.btAnalyse.Text = "分析(&A)";
			this.btAnalyse.UseVisualStyleBackColor = true;
			this.btAnalyse.Click += new EventHandler(this.btAnalyse_Click);
			this.tbBufferRadius.Location = new System.Drawing.Point(104, 185);
			this.tbBufferRadius.Name = "tbBufferRadius";
			this.tbBufferRadius.Size = new System.Drawing.Size(99, 21);
			this.tbBufferRadius.TabIndex = 6;
			this.tbBufferRadius.Text = "20";
			this.tbBufferRadius.KeyPress += new KeyPressEventHandler(this.tbBufferRadius_KeyPress);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 188);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "分析半径(米)：";
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView1.ColumnHeadersHeight = 22;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.序号, this.PipeDataset, this.FID, this.verDist, this.stanDist });
			
			this.dataGridView1.Location = new System.Drawing.Point(12, 16);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridView1.RowTemplate.Height = 20;
			this.dataGridView1.Size = new System.Drawing.Size(359, 163);
			this.dataGridView1.TabIndex = 4;
			this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
			this.序号.HeaderText = "序号";
			this.序号.Name = "序号";
			this.序号.ReadOnly = true;
			this.序号.Width = 40;
			this.PipeDataset.HeaderText = "管线性质";
			this.PipeDataset.Name = "PipeDataset";
			this.PipeDataset.ReadOnly = true;
			this.PipeDataset.Width = 80;
			this.FID.HeaderText = "编号";
			this.FID.Name = "FID";
			this.FID.ReadOnly = true;
			this.FID.Width = 35;
			this.verDist.HeaderText = "垂直净距(米)";
			this.verDist.Name = "verDist";
			this.verDist.ReadOnly = true;
			white.Alignment = DataGridViewContentAlignment.MiddleCenter;
			white.BackColor = Color.White;
			white.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			white.ForeColor = SystemColors.WindowText;
			white.SelectionBackColor = SystemColors.Highlight;
			white.SelectionForeColor = SystemColors.HighlightText;
			white.WrapMode = DataGridViewTriState.True;
			this.stanDist.DefaultCellStyle = white;
			this.stanDist.HeaderText = "标准";
			this.stanDist.Name = "stanDist";
			this.stanDist.ReadOnly = true;
			this.timer_0.Interval = 500;
			this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
			this.btClose.Location = new System.Drawing.Point(296, 188);
			this.btClose.Name = "btClose";
			this.btClose.Size = new System.Drawing.Size(75, 23);
			this.btClose.TabIndex = 22;
			this.btClose.Text = "关闭(&C)";
			this.btClose.UseVisualStyleBackColor = true;
			this.btClose.Click += new EventHandler(this.btClose_Click);
			base.AcceptButton = this.btAnalyse;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(373, 224);
			base.Controls.Add(this.btClose);
			base.Controls.Add(this.btAnalyse);
			base.Controls.Add(this.tbBufferRadius);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.dataGridView1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "VerDistDlg";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "垂直净距分析";
			base.TopMost = true;
			base.FormClosing += new FormClosingEventHandler(this.VerDistDlg_FormClosing);
			base.FormClosed += new FormClosedEventHandler(this.VerDistDlg_FormClosed);
			base.Load += new EventHandler(this.VerDistDlg_Load);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private string method_0(object obj)
		{
			return ((Convert.IsDBNull(obj) ? false : obj != null) ? obj.ToString() : "");
		}

		private void method_1(object obj, KeyPressEventArgs keyPressEventArg)
		{
			MessageBox.Show("dataGridViewBase_KeyPress!");
		}

		private void method_2(object obj, KeyEventArgs keyEventArg)
		{
			MessageBox.Show("KeyDownEvent!");
		}

		private void method_3(object obj, HelpEventArgs helpEventArg)
		{
			string str = string.Concat(Application.StartupPath, "\\帮助.chm");
			Help.ShowHelp(this, str, HelpNavigator.KeywordIndex, "垂直净距分析");
		}

		private void tbBufferRadius_KeyPress(object obj, KeyPressEventArgs keyPressEventArg)
		{
			char keyChar = keyPressEventArg.KeyChar;
			if (keyChar != '\b')
			{
				switch (keyChar)
				{
					case '.':
					{
						if ((this.tbBufferRadius.Text.IndexOf('.') != -1 ? false : this.tbBufferRadius.SelectionStart != 0))
						{
							break;
						}
						keyPressEventArg.KeyChar = '\0';
						break;
					}
					case '/':
					{
						keyPressEventArg.KeyChar = '\0';
						break;
					}
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					{
						break;
					}
					default:
					{
						goto case '/';
					}
				}
			}
		}

		private void timer_0_Tick(object obj, EventArgs eventArg)
		{
			if ((!base.Visible ? true : this.m_nTimerCount > 20))
			{
				this.m_nTimerCount = 0;
				this.timer_0.Stop();
				IActiveView activeView = this.m_app.FocusMap.get_ActiveView();
				activeView.PartialRefresh(8, null, null);
			}
			this.FlashDstItem();
			VerDistDlg mNTimerCount = this;
			mNTimerCount.m_nTimerCount = mNTimerCount.m_nTimerCount + 1;
		}

		private void VerDistDlg_FormClosed(object obj, FormClosedEventArgs formClosedEventArg)
		{
		}

		private void VerDistDlg_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
		{
			base.Visible = false;
			formClosingEventArg.Cancel = true;
		}

		private void VerDistDlg_Load(object obj, EventArgs eventArg)
		{
			this.chitAnalyse_0.m_app = this.m_app;
			this.chitAnalyse_0.IMap = this.m_app.FocusMap.get_Map();
		}
	}
}