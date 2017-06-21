using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public class HitAnalyseDlg : XtraForm
	{
		private IContainer icontainer_0 = null;

		private DataGridView dataGridViewBase;

		private Button btAnalyse;

		private TextBox tbBufferRadius;

		private Label label1;

		private GroupBox groupBox1;

		private RadioButton radioButton2;

		private RadioButton radioButton1;

		private TextBox tbPipeWidthOrHeight;

		private Label label2;

		private Button btSaveResult;

		private Button btClose;

		private GroupBox groupBox2;

		private DataGridView dataGridView1;

		private Timer timer_0;

		private SaveFileDialog saveFileDialog_0;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private ComboBox comboBoxGX;

		private Label label3;

		private DataGridViewTextBoxColumn 序号;

		private DataGridViewTextBoxColumn PipeDataset;

		private DataGridViewTextBoxColumn FID;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn stanHrzDist;

		private DataGridViewTextBoxColumn verDist;

		private DataGridViewTextBoxColumn stanVerDist;

		private CHitAnalyse chitAnalyse_0 = new CHitAnalyse();

		public IAppContext m_app;

		public CommonDistAnalyse m_commonDistAls;

		public int m_nTimerCount;

		public IGeometry m_pFlashGeo;

		private HitAnalyseDlg.HitAnalyseType hitAnalyseType_0;

		public HitAnalyseDlg.HitAnalyseType HitType
		{
			get
			{
				return this.hitAnalyseType_0;
			}
			set
			{
				this.hitAnalyseType_0 = value;
			}
		}

		public HitAnalyseDlg(IAppContext pApp)
		{
			this.InitializeComponent();
			this.m_commonDistAls = new CommonDistAnalyse()
			{
				m_nAnalyseType = DistAnalyseType.emHitDist
			};
			this.m_app = pApp;
			this.m_commonDistAls.PipeConfig = pApp.PipeConfig;
			this.m_nTimerCount = 0;
			this.hitAnalyseType_0 = HitAnalyseDlg.HitAnalyseType.emHitAnalyseSelect;
		}

		private void btAnalyse_Click(object obj, EventArgs eventArg)
		{
			double num = Convert.ToDouble(this.tbBufferRadius.Text);
			this.btAnalyse.Enabled = false;
			this.Cursor = Cursors.WaitCursor;
			IPolyline polyline = this.method_3();
			string text = this.tbPipeWidthOrHeight.Text;
			if (!this.radioButton2.Checked)
			{
				this.chitAnalyse_0.SetData(text, polyline);
			}
			else
			{
				if (this.comboBoxGX.SelectedIndex < 0)
				{
					MessageBox.Show("请选择管线层!");
					this.Cursor = Cursors.Default;
					this.btAnalyse.Enabled = true;
					return;
				}
				LayerInfo selectedItem = (LayerInfo)this.comboBoxGX.SelectedItem;
				this.chitAnalyse_0.SetData(text, polyline, selectedItem.FeatureLauer.FeatureClass);
			}
			try
			{
				this.chitAnalyse_0.BufferDistance = num;
				this.chitAnalyse_0.Analyse_Hit();
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
						str.Cells[0].Value = i + 1;
						str.Cells[1].Value = item._sKind;
						str.Cells[2].Value = item._OID;
						str.Cells[3].Value = item._dHorDistance.ToString("0.000");
						if (item._dHorBase > 0.001)
						{
							str.Cells[4].Value = item._dHorBase.ToString("0.000");
						}
						str.Cells[5].Value = item._dVerDistance.ToString("0.000");
						if (item._dVerBase > 0.001)
						{
							str.Cells[6].Value = item._dVerBase.ToString("0.000");
						}
						if (item._dHorDistance < item._dHorBase)
						{
							str.Cells[3].Style = dataGridViewCellStyle;
						}
						if (item._dVerDistance < item._dVerBase)
						{
							str.Cells[5].Style = dataGridViewCellStyle;
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
			base.Close();
		}

		private void btSaveResult_Click(object obj, EventArgs eventArg)
		{
			if (this.saveFileDialog_0.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string fileName = this.saveFileDialog_0.FileName;
				if (!File.Exists(fileName))
				{
					OdbcCommand odbcCommand = new OdbcCommand();
					OdbcConnection odbcConnection = new OdbcConnection()
					{
						ConnectionString = string.Concat("DRIVER=MICROSOFT EXCEL DRIVER (*.xls);FIRSTROWHASNAMES=1;READONLY=FALSE;CREATE_DB=\"", fileName, "\\;DBQ=", fileName)
					};
					odbcConnection.Open();
					odbcCommand.Connection = odbcConnection;
					odbcCommand.CommandType = CommandType.Text;
					odbcCommand.CommandText = string.Concat("CREATE TABLE HitAnalyseResult ", "(序号 varchar(10), 管线性质 varchar(40), 编号 varchar(40), 水平间距 varchar(40), 水平标准 varchar(40),  垂直间距 varchar(40), 垂直标准 varchar(40))");
					odbcCommand.ExecuteNonQuery();
					int count = this.dataGridView1.Rows.Count;
					int columnCount = this.dataGridView1.ColumnCount;
					for (int i = 0; i < count; i++)
					{
						OdbcCommand odbcCommand1 = new OdbcCommand()
						{
							Connection = odbcConnection,
							CommandType = CommandType.Text
						};
						string str = "INSERT INTO HitAnalyseResult ";
						string str1 = " VALUES(";
						for (int j = 0; j < columnCount; j++)
						{
							str1 = (j == columnCount - 1 ? string.Concat(str1, "'", this.dataGridView1[j, i].Value.ToString(), "')") : string.Concat(str1, "'", this.dataGridView1[j, i].Value.ToString(), "', "));
						}
						str = string.Concat(str, str1);
						odbcCommand1.CommandText = str;
						odbcCommand1.ExecuteNonQuery();
					}
					odbcConnection.Close();
				}
				else
				{
					MessageBox.Show("该文件已经存在,请换名保存!");
				}
			}
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
				IActiveView activeView = this.m_app.ActiveView;
				activeView.PartialRefresh((esriViewDrawPhase) 8, null, null);
				this.timer_0.Start();
				this.timer_0.Interval = 300;
				this.m_nTimerCount = 0;
				this.FlashDstItem();
			}
		}

		private void dataGridView1_ColumnHeaderMouseClick(object obj, DataGridViewCellMouseEventArgs dataGridViewCellMouseEventArg)
		{
			this.dataGridView1.Sort(this.dataGridView1.Columns[dataGridViewCellMouseEventArg.ColumnIndex], ListSortDirection.Ascending);
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
			IMapControl3 mapControl = this.m_app.MapControl as IMapControl3;
			Color randColor = (new CRandomColor()).GetRandColor();
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
				mapControl.DrawShape(this.m_commonDistAls.m_pBaseLine, ref obj1);
			}
			catch
			{
			}
		}

		private void FyUjxdliMq(object obj, EventArgs eventArg)
		{
			this.chitAnalyse_0.m_app = this.m_app;
			this.chitAnalyse_0.IMap = this.m_app.FocusMap;
			CBase cBase = new CBase();
		    int layerCount = this.m_app.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				cBase.AddLayer(this.m_app.FocusMap.get_Layer(i), cBase.m_listLayers);
			}
			foreach (IFeatureLayer mListLayer in cBase.m_listLayers)
			{
				LayerInfo layerInfo = new LayerInfo(mListLayer);
				this.comboBoxGX.Items.Add(layerInfo);
			}
			this.comboBoxGX.Enabled = this.radioButton2.Checked;
		}

		public void GetBaseLine()
		{
			string str;
			string str1;
			string str2;
			this.timer_0.Stop();
			this.dataGridView1.Rows.Clear();
		    IMap map = this.m_app.FocusMap;
			IFeature feature = ((IEnumFeature)map.FeatureSelection).Next();
			if ((feature == null ? false : feature.FeatureType == (esriFeatureType) 8))
			{
				CommonUtils.GetSmpClassName(feature.Class.AliasName);
				if (this.m_app.PipeConfig.IsPipeLine(feature.Class.AliasName))
				{
					IGeometry shape = feature.Shape;
					if (shape.GeometryType == (esriGeometryType) 3)
					{
						this.m_commonDistAls.m_pFeature = feature;
						this.m_commonDistAls.m_pBaseLine = (IPolyline)shape;
						this.m_commonDistAls.m_strLayerName = feature.Class.AliasName;
						int num = feature.Fields.FindField("埋设方式");
						str = (num == -1 ? "" : this.method_0(feature.get_Value(num)));
						this.m_commonDistAls.m_strBuryKind = str;
						int num1 = feature.Fields.FindField(this.m_app.PipeConfig.get_Diameter());
						str1 = (num1 == -1 ? "" : this.method_0(feature.get_Value(num1)));
						num1 = feature.Fields.FindField(this.m_app.PipeConfig.get_Section_Size());
						str2 = (num1 == -1 ? "" : feature.get_Value(num1).ToString());
						if (str1 != "")
						{
							this.tbPipeWidthOrHeight.Text = str1;
						}
						if (str2 != "")
						{
							this.tbPipeWidthOrHeight.Text = str2;
						}
						this.m_commonDistAls.m_dDiameter = this.m_commonDistAls.GetDiameterFromString(this.tbPipeWidthOrHeight.Text.Trim());
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
						this.tbPipeWidthOrHeight.Text = "";
					}
				}
				else
				{
					this.m_commonDistAls.m_pBaseLine = null;
					this.btAnalyse.Enabled = false;
					this.m_app.FocusMap.ClearSelection();
					this.m_app.ActiveView.Refresh();
					this.tbPipeWidthOrHeight.Text = "";
				}
			}
			else
			{
				this.m_commonDistAls.m_pBaseLine = null;
				this.btAnalyse.Enabled = false;
				this.m_app.FocusMap.ClearSelection();
				this.m_app.ActiveView.Refresh();
				this.tbPipeWidthOrHeight.Text = "";
			}
		}

		public void GetDrawLine()
		{
			IGeometry geometry = this.m_app.MapControl.TrackLine();
			CommonUtils.NewLineElement(this.m_app.FocusMap, geometry as IPolyline);
			if (geometry != null && geometry.GeometryType == (esriGeometryType) 3)
			{
				this.m_commonDistAls.m_pBaseLine = (IPolyline)geometry;
				this.m_commonDistAls.m_strLayerName = "";
				this.m_commonDistAls.m_nBaseLineFromID = -1;
				this.m_commonDistAls.m_nBaseLineToID = -1;
				this.btAnalyse.Enabled = this.m_commonDistAls.m_pBaseLine != null;
			}
		}

		private void HitAnalyseDlg_FormClosed(object obj, FormClosedEventArgs formClosedEventArg)
		{
			base.Visible = false;
		}

		private void HitAnalyseDlg_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
		{
			CommonUtils.DeleteAllElements(this.m_app.FocusMap);
			base.Visible = false;
			formClosingEventArg.Cancel = true;
		}

		public void InitDistAnalyseDlg()
		{
			this.m_app.FocusMap.ClearSelection();
			this.m_app.ActiveView.Refresh();
			this.dataGridViewBase.Rows.Clear();
			this.dataGridView1.Rows.Clear();
			this.btAnalyse.Enabled = false;
			this.hitAnalyseType_0 = HitAnalyseDlg.HitAnalyseType.emHitAnalyseSelect;
			this.radioButton1.Checked = true;
		}

		private void InitializeComponent()
		{
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewBase = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btAnalyse = new System.Windows.Forms.Button();
            this.tbBufferRadius = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxGX = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPipeWidthOrHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btSaveResult = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PipeDataset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stanHrzDist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.verDist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stanVerDist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer_0 = new System.Windows.Forms.Timer();
            this.saveFileDialog_0 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBase)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewBase
            // 
            this.dataGridViewBase.AllowUserToAddRows = false;
            this.dataGridViewBase.AllowUserToDeleteRows = false;
            this.dataGridViewBase.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewBase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewBase.ColumnHeadersHeight = 20;
            this.dataGridViewBase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewBase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridViewBase.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewBase.Location = new System.Drawing.Point(10, 44);
            this.dataGridViewBase.Name = "dataGridViewBase";
            this.dataGridViewBase.RowHeadersVisible = false;
            this.dataGridViewBase.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewBase.RowTemplate.Height = 20;
            this.dataGridViewBase.Size = new System.Drawing.Size(569, 91);
            this.dataGridViewBase.TabIndex = 14;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = "0.000";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "X坐标";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "Y坐标";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column4.HeaderText = "管线高程";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 80;
            // 
            // btAnalyse
            // 
            this.btAnalyse.Enabled = false;
            this.btAnalyse.Location = new System.Drawing.Point(69, 378);
            this.btAnalyse.Name = "btAnalyse";
            this.btAnalyse.Size = new System.Drawing.Size(87, 27);
            this.btAnalyse.TabIndex = 13;
            this.btAnalyse.Text = "分析(&A)";
            this.btAnalyse.UseVisualStyleBackColor = true;
            this.btAnalyse.Click += new System.EventHandler(this.btAnalyse_Click);
            // 
            // tbBufferRadius
            // 
            this.tbBufferRadius.Location = new System.Drawing.Point(299, 143);
            this.tbBufferRadius.Name = "tbBufferRadius";
            this.tbBufferRadius.Size = new System.Drawing.Size(52, 22);
            this.tbBufferRadius.TabIndex = 12;
            this.tbBufferRadius.Text = "5";
            this.tbBufferRadius.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBufferRadius_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(197, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 14);
            this.label1.TabIndex = 11;
            this.label1.Text = "分析半径(米)：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxGX);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbPipeWidthOrHeight);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.dataGridViewBase);
            this.groupBox1.Controls.Add(this.tbBufferRadius);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 183);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "碰撞线信息";
            // 
            // comboBoxGX
            // 
            this.comboBoxGX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGX.FormattingEnabled = true;
            this.comboBoxGX.Location = new System.Drawing.Point(409, 143);
            this.comboBoxGX.Name = "comboBoxGX";
            this.comboBoxGX.Size = new System.Drawing.Size(140, 22);
            this.comboBoxGX.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(360, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 19;
            this.label3.Text = "管线:";
            // 
            // tbPipeWidthOrHeight
            // 
            this.tbPipeWidthOrHeight.Location = new System.Drawing.Point(111, 143);
            this.tbPipeWidthOrHeight.Name = "tbPipeWidthOrHeight";
            this.tbPipeWidthOrHeight.Size = new System.Drawing.Size(74, 22);
            this.tbPipeWidthOrHeight.TabIndex = 18;
            this.tbPipeWidthOrHeight.Text = "0";
            this.tbPipeWidthOrHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPipeWidthOrHeight_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 14);
            this.label2.TabIndex = 17;
            this.label2.Text = "规格(毫米):";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(155, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(167, 18);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "绘制管段(坐标高程可修改)";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(36, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(73, 18);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "选择管段";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // btSaveResult
            // 
            this.btSaveResult.Location = new System.Drawing.Point(187, 378);
            this.btSaveResult.Name = "btSaveResult";
            this.btSaveResult.Size = new System.Drawing.Size(87, 27);
            this.btSaveResult.TabIndex = 19;
            this.btSaveResult.Text = "保存结果(&S)";
            this.btSaveResult.UseVisualStyleBackColor = true;
            this.btSaveResult.Click += new System.EventHandler(this.btSaveResult_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(304, 378);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(87, 27);
            this.btClose.TabIndex = 20;
            this.btClose.Text = "关闭(&C)";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(14, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(587, 169);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "碰撞结果";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeight = 20;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序号,
            this.PipeDataset,
            this.FID,
            this.Column5,
            this.stanHrzDist,
            this.verDist,
            this.stanVerDist});
            this.dataGridView1.Location = new System.Drawing.Point(10, 23);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.Size = new System.Drawing.Size(569, 139);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            // 
            // 序号
            // 
            this.序号.HeaderText = "序号";
            this.序号.Name = "序号";
            this.序号.ReadOnly = true;
            this.序号.Width = 40;
            // 
            // PipeDataset
            // 
            this.PipeDataset.HeaderText = "管线性质";
            this.PipeDataset.Name = "PipeDataset";
            this.PipeDataset.ReadOnly = true;
            this.PipeDataset.Width = 80;
            // 
            // FID
            // 
            this.FID.HeaderText = "编号";
            this.FID.Name = "FID";
            this.FID.ReadOnly = true;
            this.FID.Width = 35;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "水平净距";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 60;
            // 
            // stanHrzDist
            // 
            this.stanHrzDist.HeaderText = "水平标准";
            this.stanHrzDist.Name = "stanHrzDist";
            this.stanHrzDist.ReadOnly = true;
            this.stanHrzDist.Width = 60;
            // 
            // verDist
            // 
            this.verDist.HeaderText = "垂直净距";
            this.verDist.Name = "verDist";
            this.verDist.ReadOnly = true;
            this.verDist.Width = 60;
            // 
            // stanVerDist
            // 
            this.stanVerDist.HeaderText = "垂直标准";
            this.stanVerDist.Name = "stanVerDist";
            this.stanVerDist.ReadOnly = true;
            this.stanVerDist.Width = 60;
            // 
            // timer_0
            // 
            this.timer_0.Interval = 500;
            this.timer_0.Tick += new System.EventHandler(this.timer_0_Tick);
            // 
            // saveFileDialog_0
            // 
            this.saveFileDialog_0.DefaultExt = "xls";
            this.saveFileDialog_0.FileName = "hitanalyse1";
            this.saveFileDialog_0.Filter = "Excel文件(*.xls)|*.xls|所有文件(*.*)|*.*";
            this.saveFileDialog_0.OverwritePrompt = false;
            this.saveFileDialog_0.RestoreDirectory = true;
            this.saveFileDialog_0.Title = "保存";
            // 
            // HitAnalyseDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 418);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btSaveResult);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btAnalyse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HitAnalyseDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "碰撞分析";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HitAnalyseDlg_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HitAnalyseDlg_FormClosed);
            this.Load += new System.EventHandler(this.FyUjxdliMq);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBase)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

		}

		private string method_0(object obj)
		{
			return ((Convert.IsDBNull(obj) ? false : obj != null) ? obj.ToString() : "");
		}

		private void method_1(object obj, EventArgs eventArg)
		{
		}

		private void method_2()
		{
			IPointCollection polylineClass = new Polyline();
			int rowCount = this.dataGridViewBase.RowCount;
			for (int i = 0; i < rowCount; i++)
			{
				IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
				pointClass.X=(Convert.ToDouble(this.dataGridViewBase[1, i].Value.ToString()));
				pointClass.Y=(Convert.ToDouble(this.dataGridViewBase[2, i].Value.ToString()));
				pointClass.Z=(Convert.ToDouble(this.dataGridViewBase[3, i].Value.ToString()));
				object missing = Type.Missing;
				polylineClass.AddPoint(pointClass, ref missing, ref missing);
			}
			this.m_commonDistAls.m_pBaseLine = (IPolyline)polylineClass;
			this.method_4(this.m_commonDistAls.m_pBaseLine);
			this.m_commonDistAls.m_dDiameter = this.m_commonDistAls.GetDiameterFromString(this.tbPipeWidthOrHeight.Text.Trim());
		}

		private IPolyline method_3()
		{
			IPointCollection polylineClass = new Polyline();
			int rowCount = this.dataGridViewBase.RowCount;
			for (int i = 0; i < rowCount; i++)
			{
				IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
				pointClass.X=(Convert.ToDouble(this.dataGridViewBase[1, i].Value.ToString()));
				pointClass.Y=(Convert.ToDouble(this.dataGridViewBase[2, i].Value.ToString()));
				pointClass.Z=(Convert.ToDouble(this.dataGridViewBase[3, i].Value.ToString()));
				object missing = Type.Missing;
				polylineClass.AddPoint(pointClass, ref missing, ref missing);
			}
			return polylineClass as IPolyline;
		}

		private void method_4(IGeometry geometry)
		{
			IPointCollection pointCollection = (IPointCollection)geometry;
			int pointCount = pointCollection.PointCount;
			if (pointCount != 0)
			{
				for (int i = 0; i < pointCount; i++)
				{
					pointCollection.get_Point(i);
				}
			}
		}

		private void method_5()
		{
			this.dataGridViewBase.Rows.Clear();
			this.dataGridView1.Rows.Clear();
			this.m_nTimerCount = 0;
			this.timer_0.Stop();
			this.m_app.ActiveView.Refresh();
		}

		private void method_6(object obj, HelpEventArgs helpEventArg)
		{
			string str = string.Concat(Application.StartupPath, "\\帮助.chm");
			Help.ShowHelp(this, str, HelpNavigator.KeywordIndex, "碰撞分析");
		}

		private void radioButton1_CheckedChanged(object obj, EventArgs eventArg)
		{
			this.dataGridViewBase.EditMode = DataGridViewEditMode.EditProgrammatically;
			this.hitAnalyseType_0 = HitAnalyseDlg.HitAnalyseType.emHitAnalyseSelect;
			this.method_5();
			this.btAnalyse.Enabled = false;
		}

		private void radioButton2_CheckedChanged(object obj, EventArgs eventArg)
		{
			this.dataGridViewBase.EditMode = DataGridViewEditMode.EditOnEnter;
			this.hitAnalyseType_0 = HitAnalyseDlg.HitAnalyseType.emHitAnalyseDraw;
			this.m_app.FocusMap.ClearSelection();
			this.comboBoxGX.Enabled = this.radioButton2.Checked;
			this.method_5();
			this.btAnalyse.Enabled = false;
		}

		public void RefreshBaseLineGrid()
		{
			if (this.m_commonDistAls.m_pBaseLine != null)
			{
				IPointCollection mPBaseLine = (IPointCollection)this.m_commonDistAls.m_pBaseLine;
				int pointCount = mPBaseLine.PointCount;
				if (pointCount != 0)
				{
					this.dataGridViewBase.Rows.Clear();
					for (int i = 0; i < pointCount; i++)
					{
						IPoint point = mPBaseLine.get_Point(i);
						double x = point.X;
						double y = point.Y;
						double z = point.Z - point.M;
						this.dataGridViewBase.Rows.Add(new object[] { "" });
						this.dataGridViewBase[0, i].Value = i + 1;
						this.dataGridViewBase[1, i].Value = x.ToString("f3");
						this.dataGridViewBase[2, i].Value = y.ToString("f3");
						this.dataGridViewBase[3, i].Value = z.ToString("f3");
						if (this.hitAnalyseType_0 == HitAnalyseDlg.HitAnalyseType.emHitAnalyseDraw)
						{
							this.dataGridViewBase[3, i].Value = "0.000";
						}
					}
				}
			}
			else
			{
				this.dataGridViewBase.Rows.Clear();
			}
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

		private void tbPipeWidthOrHeight_KeyPress(object obj, KeyPressEventArgs keyPressEventArg)
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
				IActiveView activeView = this.m_app.ActiveView;
				activeView.PartialRefresh((esriViewDrawPhase) 8, null, null);
			}
			this.FlashDstItem();
			HitAnalyseDlg mNTimerCount = this;
			mNTimerCount.m_nTimerCount = mNTimerCount.m_nTimerCount + 1;
		}

		public enum HitAnalyseType
		{
			emHitAnalyseSelect,
			emHitAnalyseDraw
		}
	}
}