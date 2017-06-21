using ApplicationData;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using PipeConfig;
using RandomColor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.Pipeline.Analysis.Forms
{
	public class GForm0 : XtraForm
	{
		private TabControl QfyEqiOwEk;

		public IProjectFrameworkApp m_app;

		private List<IFeatureLayer> list_0 = new List<IFeatureLayer>();

		private List<IFeatureLayer> list_1 = new List<IFeatureLayer>();

		private CHitAnalyse chitAnalyse_0 = new CHitAnalyse();

		public CommonDistAnalyse m_commonDistAls;

		private IPolyline ipolyline_0;

		public int m_nTimerCount;

		public IGeometry m_pFlashGeo;

		private List<IFeatureLayer> list_2 = new List<IFeatureLayer>();

		private string string_0 = "管线性质";

		private string string_1 = "管径";

		private string string_2 = "沟截面宽高";

		private IPolyline ipolyline_1;

		private IFeatureClass ifeatureClass_0;

		private IFeature ifeature_0;

		private double double_0;

		private double double_1;

		private int int_0;

		private List<CHitAnalyse.CItem> list_3 = new List<CHitAnalyse.CItem>();

		private List<CHitAnalyse.CItem> list_4 = new List<CHitAnalyse.CItem>();

		private List<CHitAnalyse.CItem> list_5 = new List<CHitAnalyse.CItem>();

		private IFeatureClass ifeatureClass_1;

		private Dictionary<string, string> dictionary_0 = new Dictionary<string, string>();

		private int int_1;

		private int int_2;

		private IContainer icontainer_0 = null;

		private GroupBox groupBox1;

		private Button btnClear;

		private TextBox textBoxLoadCADName;

		private Button button_0;

		private TextBox tbBufferRadius;

		private Label label1;

		private Button btClose;

		private Button btAnalyse;

		private GroupBox groupBox2;

		private TabPage tabPageHor;

		private TabPage ejsEghpDqe;

		private TabPage tabPageRollCenter;

		private TabPage tabPageApprove;

		private DataGridView dataGridViewSelectItem;

		private Timer timer_0;

		private DataGridView dataGridViewVer;

		private DataGridViewTextBoxColumn 序号;

		private DataGridViewTextBoxColumn PipeDataset;

		private DataGridViewTextBoxColumn FID;

		private DataGridViewTextBoxColumn verDist;

		private DataGridViewTextBoxColumn stanDist;

		private DataGridView dataGridViewHor;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn hrzDist;

		private DataGridViewTextBoxColumn UwxEkbyfru;

		private DataGridView dataGridViewCenterLine;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private DataGridViewTextBoxColumn ColumnIndex;

		private DataGridViewTextBoxColumn FeatueName;

		private DataGridViewTextBoxColumn krepayTghI;

		private DataGridViewTextBoxColumn editHeightStartPoint;

		private DataGridViewTextBoxColumn editHeightEndPoint;

		private DataGridViewComboBoxColumn comboLineType;

		private DataGridViewComboBoxCell dataGridViewComboBoxCell_0;

		public GForm0(IProjectFrameworkApp pApp)
		{
			this.InitializeComponent();
			this.m_commonDistAls = new CommonDistAnalyse()
			{
				m_nAnalyseType = DistAnalyseType.emVerDist
			};
			this.m_app = pApp;
			this.m_commonDistAls.PipeConfig = pApp.PipeConfig;
			this.m_nTimerCount = 0;
			this.dataGridViewSelectItem.Columns[0].ReadOnly = true;
			this.dataGridViewSelectItem.Columns[1].ReadOnly = true;
			this.dataGridViewSelectItem.Columns[2].ReadOnly = true;
			this.method_16();
		}

		public void AddFeatureLayer(IFeatureLayer iFLayer, List<IFeatureLayer> pListLayers)
		{
			try
			{
				if (iFLayer != null)
				{
					IFeatureClass featureClass = iFLayer.FeatureClass;
					if ((featureClass.ShapeType == 13 ? true : featureClass.ShapeType == 3))
					{
						if ((featureClass.AliasName == "SY_ZX_L" ? true : featureClass.AliasName == "SDE.SY_ZX_L"))
						{
							this.ifeatureClass_1 = featureClass;
						}
						INetworkClass networkClass = featureClass as INetworkClass;
						if ((networkClass == null ? false : networkClass.GeometricNetwork != null))
						{
							pListLayers.Add(iFLayer);
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		public void AddGroupLayer(IGroupLayer iGLayer, List<IFeatureLayer> pListLayers)
		{
			ICompositeLayer compositeLayer = (ICompositeLayer)iGLayer;
			if (compositeLayer != null)
			{
				int count = compositeLayer.Count;
				for (int i = 0; i < count; i++)
				{
					this.AddLayer(compositeLayer.get_Layer(i), pListLayers);
				}
			}
		}

		public void AddLayer(ILayer ipLay, List<IFeatureLayer> pListLayers)
		{
			if (ipLay is IFeatureLayer)
			{
				this.AddFeatureLayer((IFeatureLayer)ipLay, pListLayers);
			}
			else if (ipLay is IGroupLayer)
			{
				this.AddGroupLayer((IGroupLayer)ipLay, pListLayers);
			}
		}

		public void AddName(ILayer pLayer)
		{
			try
			{
				if (pLayer != null)
				{
					IFeatureLayer featureLayer = pLayer as IFeatureLayer;
					IFeatureClass featureClass = featureLayer.FeatureClass;
					if (featureLayer.FeatureClass.FeatureType != 11 && this.m_app.PipeConfig.IsPipeLine(featureClass.AliasName))
					{
						this.dictionary_0.Add(featureLayer.Name, featureClass.AliasName);
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btAnalyse_Click(object obj, EventArgs eventArg)
		{
			try
			{
				IPoint fromPoint = this.ipolyline_0.FromPoint;
				IPoint toPoint = this.ipolyline_0.ToPoint;
				fromPoint.Z=((double)this.int_1);
				toPoint.Z=((double)this.int_2);
				this.ipolyline_0.FromPoint=(fromPoint);
				this.ipolyline_0.ToPoint=(toPoint);
				this.method_0();
				MessageBox.Show("分析完成！");
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void btClose_Click(object obj, EventArgs eventArg)
		{
			base.Close();
		}

		private void btnClear_Click(object obj, EventArgs eventArg)
		{
			this.method_10();
			this.InitDistAnalyseDlg();
		}

		private void dataGridViewCenterLine_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
		{
			int rowIndex = dataGridViewCellEventArg.RowIndex;
			if (rowIndex >= 0)
			{
				try
				{
					int num = Convert.ToInt32(this.dataGridViewCenterLine[2, rowIndex].Value);
					DataGridViewRow item = this.dataGridViewCenterLine.Rows[dataGridViewCellEventArg.RowIndex];
					IFeatureClass tag = (IFeatureClass)item.Tag;
					if (tag != null)
					{
						IFeature feature = tag.GetFeature(num);
						if (feature != null)
						{
							this.m_pFlashGeo = feature.Shape;
						}
					}
					this.timer_0.Start();
					this.timer_0.Interval = 300;
					this.m_app.FocusMap.get_ActiveView().Refresh();
					this.m_nTimerCount = 0;
					this.FlashDstItem();
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}

		private void dataGridViewHor_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
		{
			int rowIndex = dataGridViewCellEventArg.RowIndex;
			if (rowIndex >= 0)
			{
				try
				{
					int num = Convert.ToInt32(this.dataGridViewHor[2, rowIndex].Value);
					DataGridViewRow item = this.dataGridViewHor.Rows[dataGridViewCellEventArg.RowIndex];
					IFeatureClass tag = (IFeatureClass)item.Tag;
					if (tag != null)
					{
						IFeature feature = tag.GetFeature(num);
						if (feature != null)
						{
							this.m_pFlashGeo = feature.Shape;
						}
					}
					this.timer_0.Start();
					this.timer_0.Interval = 300;
					this.m_app.FocusMap.get_ActiveView().Refresh();
					this.m_nTimerCount = 0;
					this.FlashDstItem();
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}

		private void dataGridViewSelectItem_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
		{
			int rowIndex = dataGridViewCellEventArg.RowIndex;
			if (rowIndex >= 0)
			{
				try
				{
					int num = Convert.ToInt32(this.dataGridViewSelectItem[2, rowIndex].Value);
					DataGridViewRow item = this.dataGridViewSelectItem.Rows[dataGridViewCellEventArg.RowIndex];
					IFeatureLayer tag = (IFeatureLayer)item.Tag;
					IFeatureClass featureClass = tag.FeatureClass;
					if (featureClass != null)
					{
						IFeature feature = featureClass.GetFeature(num);
						if (feature != null)
						{
							this.m_pFlashGeo = feature.Shape;
							IMap map = this.m_app.FocusMap.get_Map();
							map.ClearSelection();
							map.SelectFeature(tag, feature);
							this.GetBaseLine();
							((IActiveView)map).PartialRefresh(4, null, null);
							CommonUtils.ScaleToTwoGeo(this.m_app.FocusMap, this.ipolyline_0, this.m_pFlashGeo);
						}
					}
					this.m_app.FocusMap.get_ActiveView().Refresh();
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}

		private void dataGridViewSelectItem_CellEndEdit(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
		{
			DataGridViewRow item = this.dataGridViewSelectItem.Rows[dataGridViewCellEventArg.RowIndex];
			this.dataGridViewComboBoxCell_0 = (DataGridViewComboBoxCell)item.Cells["comboLineType"];
			this.int_1 = Convert.ToInt16(item.Cells["editHeightStartPoint"].Value);
			this.int_2 = Convert.ToInt16(item.Cells["editHeightEndPoint"].Value);
		}

		private void dataGridViewVer_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
		{
			int rowIndex = dataGridViewCellEventArg.RowIndex;
			if (rowIndex >= 0)
			{
				try
				{
					int num = Convert.ToInt32(this.dataGridViewVer[2, rowIndex].Value);
					DataGridViewRow item = this.dataGridViewVer.Rows[dataGridViewCellEventArg.RowIndex];
					IFeatureClass tag = (IFeatureClass)item.Tag;
					if (tag != null)
					{
						IFeature feature = tag.GetFeature(num);
						if (feature != null)
						{
							this.m_pFlashGeo = feature.Shape;
						}
					}
					this.timer_0.Start();
					this.timer_0.Interval = 300;
					this.m_app.FocusMap.get_ActiveView().Refresh();
					this.m_nTimerCount = 0;
					this.FlashDstItem();
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
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
			IRgbColorrgbColorClass = new RgbColor();
			rgbColorClass.Red=((int)randColor.R);
			rgbColorClass.Green=((int)randColor.G);
			rgbColorClass.Blue=((int)randColor.B);
			simpleLineSymbolClass.Color=(rgbColorClass);
			simpleLineSymbolClass.Width=(5);
			object obj = simpleLineSymbolClass;
			ISimpleLineSymbol simpleLineSymbolClass1 = new SimpleLineSymbol();
			IRgbColorrgbColorClass1 = new RgbColor();
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
					mapControl.DrawShape(this.ipolyline_0, ref obj1);
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
			IMap map = this.m_app.FocusMap.get_Map();
			IFeature feature = ((IEnumFeature)map.FeatureSelection).Next();
			if (feature != null)
			{
				CommonUtils.GetSmpClassName(feature.Class.AliasName);
				if ((this.m_app.PipeConfig.IsPipeLine(feature.Class.AliasName) ? true : !(feature.Class.AliasName != "Polyline")))
				{
					IGeometry shape = feature.Shape;
					if (shape.GeometryType == 3)
					{
						this.ipolyline_0 = CommonUtils.GetPolylineDeepCopy((IPolyline)shape);
						this.m_commonDistAls.m_pFeature = feature;
						this.m_commonDistAls.m_pBaseLine = this.ipolyline_0;
						this.m_commonDistAls.m_strLayerName = feature.Class.AliasName;
						int num = feature.Fields.FindField("埋设方式");
						str = (num == -1 ? "" : this.method_11(feature.get_Value(num)));
						this.m_commonDistAls.m_strBuryKind = str;
						int num1 = feature.Fields.FindField(this.m_app.PipeConfig.get_Diameter());
						str1 = (num1 == -1 ? "" : this.method_11(feature.get_Value(num1)));
						num1 = feature.Fields.FindField(this.m_app.PipeConfig.get_Section_Size());
						str2 = (num1 == -1 ? "" : this.method_11(feature.get_Value(num1)));
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
						this.m_commonDistAls.m_nBaseLineFromID = -1;
						this.m_commonDistAls.m_nBaseLineToID = -1;
						this.btAnalyse.Enabled = this.m_commonDistAls.m_pBaseLine != null;
						this.ifeature_0 = feature;
						this.ifeatureClass_0 = feature.Class as IFeatureClass;
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

		private void GForm0_FormClosed(object obj, FormClosedEventArgs formClosedEventArg)
		{
		}

		private void GForm0_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
		{
			base.Visible = false;
			formClosingEventArg.Cancel = true;
			this.method_10();
			this.InitDistAnalyseDlg();
		}

		private void GForm0_HelpRequested(object obj, HelpEventArgs helpEventArg)
		{
			string str = string.Concat(Application.StartupPath, "\\帮助.chm");
			Help.ShowHelp(this, str, HelpNavigator.KeywordIndex, "审批辅助分析");
		}

		private void GForm0_Load(object obj, EventArgs eventArg)
		{
			this.LoadMapLayer();
		}

		public void InitDistAnalyseDlg()
		{
			this.m_app.FocusMap.get_Map().ClearSelection();
			this.m_app.FocusMap.get_ActiveView().Refresh();
			this.dataGridViewVer.Rows.Clear();
			this.dataGridViewHor.Rows.Clear();
			this.dataGridViewSelectItem.Rows.Clear();
			this.dataGridViewCenterLine.Rows.Clear();
			this.btAnalyse.Enabled = false;
			this.list_0.Clear();
			this.list_1.Clear();
			this.textBoxLoadCADName.Text = "";
		}

		private void InitializeComponent()
		{
			this.icontainer_0 = new System.ComponentModel.Container();
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle control = new DataGridViewCellStyle();
			DataGridViewCellStyle window = new DataGridViewCellStyle();
			DataGridViewCellStyle font = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle controlText = new DataGridViewCellStyle();
			DataGridViewCellStyle windowText = new DataGridViewCellStyle();
			DataGridViewCellStyle white = new DataGridViewCellStyle();
			DataGridViewCellStyle highlight = new DataGridViewCellStyle();
			DataGridViewCellStyle highlightText = new DataGridViewCellStyle();
			DataGridViewCellStyle control1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.groupBox1 = new GroupBox();
			this.tbBufferRadius = new TextBox();
			this.label1 = new Label();
			this.btClose = new Button();
			this.btAnalyse = new Button();
			this.btnClear = new Button();
			this.textBoxLoadCADName = new TextBox();
			this.button_0 = new Button();
			this.groupBox2 = new GroupBox();
			this.QfyEqiOwEk = new TabControl();
			this.tabPageApprove = new TabPage();
			this.dataGridViewSelectItem = new DataGridView();
			this.ColumnIndex = new DataGridViewTextBoxColumn();
			this.FeatueName = new DataGridViewTextBoxColumn();
			this.krepayTghI = new DataGridViewTextBoxColumn();
			this.editHeightStartPoint = new DataGridViewTextBoxColumn();
			this.editHeightEndPoint = new DataGridViewTextBoxColumn();
			this.comboLineType = new DataGridViewComboBoxColumn();
			this.tabPageHor = new TabPage();
			this.dataGridViewHor = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.hrzDist = new DataGridViewTextBoxColumn();
			this.UwxEkbyfru = new DataGridViewTextBoxColumn();
			this.ejsEghpDqe = new TabPage();
			this.dataGridViewVer = new DataGridView();
			this.序号 = new DataGridViewTextBoxColumn();
			this.PipeDataset = new DataGridViewTextBoxColumn();
			this.FID = new DataGridViewTextBoxColumn();
			this.verDist = new DataGridViewTextBoxColumn();
			this.stanDist = new DataGridViewTextBoxColumn();
			this.tabPageRollCenter = new TabPage();
			this.dataGridViewCenterLine = new DataGridView();
			this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
			this.timer_0 = new Timer(this.icontainer_0);
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.QfyEqiOwEk.SuspendLayout();
			this.tabPageApprove.SuspendLayout();
			((ISupportInitialize)this.dataGridViewSelectItem).BeginInit();
			this.tabPageHor.SuspendLayout();
			((ISupportInitialize)this.dataGridViewHor).BeginInit();
			this.ejsEghpDqe.SuspendLayout();
			((ISupportInitialize)this.dataGridViewVer).BeginInit();
			this.tabPageRollCenter.SuspendLayout();
			((ISupportInitialize)this.dataGridViewCenterLine).BeginInit();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.tbBufferRadius);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.btClose);
			this.groupBox1.Controls.Add(this.btAnalyse);
			this.groupBox1.Controls.Add(this.btnClear);
			this.groupBox1.Controls.Add(this.textBoxLoadCADName);
			this.groupBox1.Controls.Add(this.button_0);
			this.groupBox1.Dock = DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(553, 87);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "报批数据";
			this.tbBufferRadius.Location = new System.Drawing.Point(105, 51);
			this.tbBufferRadius.Name = "tbBufferRadius";
			this.tbBufferRadius.Size = new System.Drawing.Size(99, 21);
			this.tbBufferRadius.TabIndex = 29;
			this.tbBufferRadius.Text = "20";
			this.tbBufferRadius.WordWrap = false;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 12);
			this.label1.TabIndex = 28;
			this.label1.Text = "分析半径(米)：";
			this.btClose.Location = new System.Drawing.Point(469, 49);
			this.btClose.Name = "btClose";
			this.btClose.Size = new System.Drawing.Size(75, 23);
			this.btClose.TabIndex = 27;
			this.btClose.Text = "关闭(&C)";
			this.btClose.UseVisualStyleBackColor = true;
			this.btClose.Click += new EventHandler(this.btClose_Click);
			this.btAnalyse.Enabled = false;
			this.btAnalyse.Location = new System.Drawing.Point(210, 49);
			this.btAnalyse.Name = "btAnalyse";
			this.btAnalyse.Size = new System.Drawing.Size(75, 23);
			this.btAnalyse.TabIndex = 26;
			this.btAnalyse.Text = "分析(&A)";
			this.btAnalyse.UseVisualStyleBackColor = true;
			this.btAnalyse.Click += new EventHandler(this.btAnalyse_Click);
			this.btnClear.Location = new System.Drawing.Point(388, 49);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "清除";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new EventHandler(this.btnClear_Click);
			this.textBoxLoadCADName.AcceptsReturn = true;
			this.textBoxLoadCADName.Location = new System.Drawing.Point(12, 22);
			this.textBoxLoadCADName.Name = "textBoxLoadCADName";
			this.textBoxLoadCADName.ReadOnly = true;
			this.textBoxLoadCADName.Size = new System.Drawing.Size(532, 21);
			this.textBoxLoadCADName.TabIndex = 1;
			this.button_0.Location = new System.Drawing.Point(291, 49);
			this.button_0.Name = "btnLoadCAD";
			this.button_0.Size = new System.Drawing.Size(91, 23);
			this.button_0.TabIndex = 0;
			this.button_0.Text = "导入报批数据";
			this.button_0.UseVisualStyleBackColor = true;
			this.button_0.Click += new EventHandler(this.qsTayfWbAn);
			this.groupBox2.Controls.Add(this.QfyEqiOwEk);
			this.groupBox2.Dock = DockStyle.Bottom;
			this.groupBox2.Location = new System.Drawing.Point(0, 93);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(553, 302);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "分析结果";
			this.QfyEqiOwEk.Controls.Add(this.tabPageApprove);
			this.QfyEqiOwEk.Controls.Add(this.tabPageHor);
			this.QfyEqiOwEk.Controls.Add(this.ejsEghpDqe);
			this.QfyEqiOwEk.Controls.Add(this.tabPageRollCenter);
			this.QfyEqiOwEk.Dock = DockStyle.Fill;
			this.QfyEqiOwEk.Location = new System.Drawing.Point(3, 17);
			this.QfyEqiOwEk.Name = "tabControl1";
			this.QfyEqiOwEk.SelectedIndex = 0;
			this.QfyEqiOwEk.Size = new System.Drawing.Size(547, 282);
			this.QfyEqiOwEk.TabIndex = 0;
			this.tabPageApprove.Controls.Add(this.dataGridViewSelectItem);
			this.tabPageApprove.Location = new System.Drawing.Point(4, 22);
			this.tabPageApprove.Name = "tabPageApprove";
			this.tabPageApprove.Size = new System.Drawing.Size(539, 256);
			this.tabPageApprove.TabIndex = 3;
			this.tabPageApprove.Text = "导入管线";
			this.tabPageApprove.UseVisualStyleBackColor = true;
			this.dataGridViewSelectItem.AllowUserToAddRows = false;
			this.dataGridViewSelectItem.AllowUserToDeleteRows = false;
			this.dataGridViewSelectItem.AllowUserToResizeRows = false;
			this.dataGridViewSelectItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dataGridViewSelectItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridViewSelectItem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridViewSelectItem.Columns.AddRange(new DataGridViewColumn[] { this.ColumnIndex, this.FeatueName, this.krepayTghI, this.editHeightStartPoint, this.editHeightEndPoint, this.comboLineType });
			
			this.dataGridViewSelectItem.Dock = DockStyle.Fill;
			this.dataGridViewSelectItem.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewSelectItem.MultiSelect = false;
			this.dataGridViewSelectItem.Name = "dataGridViewSelectItem";
			this.dataGridViewSelectItem.RowHeadersVisible = false;
			this.dataGridViewSelectItem.RowTemplate.Height = 23;
			this.dataGridViewSelectItem.SelectionMode = DataGridViewSelectionMode.CellSelect;
			this.dataGridViewSelectItem.Size = new System.Drawing.Size(539, 256);
			this.dataGridViewSelectItem.TabIndex = 7;
			this.dataGridViewSelectItem.CellClick += new DataGridViewCellEventHandler(this.dataGridViewSelectItem_CellClick);
			this.dataGridViewSelectItem.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridViewSelectItem_CellEndEdit);
			this.ColumnIndex.HeaderText = "序号";
			this.ColumnIndex.Name = "ColumnIndex";
			this.FeatueName.HeaderText = "元素类";
			this.FeatueName.Name = "FeatueName";
			this.krepayTghI.HeaderText = "编号";
			this.krepayTghI.Name = "OID";
			this.editHeightStartPoint.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.editHeightStartPoint.HeaderText = "起点高程(可编辑)";
			this.editHeightStartPoint.Name = "editHeightStartPoint";
			this.editHeightStartPoint.Width = 126;
			this.editHeightEndPoint.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.editHeightEndPoint.HeaderText = "终点高程(可编辑)";
			this.editHeightEndPoint.Name = "editHeightEndPoint";
			this.editHeightEndPoint.Width = 126;
			this.comboLineType.HeaderText = "管线类型";
			this.comboLineType.Name = "comboLineType";
			this.comboLineType.Resizable = DataGridViewTriState.True;
			this.comboLineType.SortMode = DataGridViewColumnSortMode.Automatic;
			this.tabPageHor.Controls.Add(this.dataGridViewHor);
			this.tabPageHor.Location = new System.Drawing.Point(4, 22);
			this.tabPageHor.Name = "tabPageHor";
			this.tabPageHor.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageHor.Size = new System.Drawing.Size(539, 256);
			this.tabPageHor.TabIndex = 0;
			this.tabPageHor.Text = "水平净距";
			this.tabPageHor.UseVisualStyleBackColor = true;
			this.dataGridViewHor.AllowUserToAddRows = false;
			this.dataGridViewHor.AllowUserToDeleteRows = false;
			this.dataGridViewHor.AllowUserToResizeRows = false;
			this.dataGridViewHor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			control.Alignment = DataGridViewContentAlignment.MiddleCenter;
			control.BackColor = SystemColors.Control;
			control.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			control.ForeColor = SystemColors.WindowText;
			control.SelectionBackColor = SystemColors.Highlight;
			control.SelectionForeColor = SystemColors.HighlightText;
			control.WrapMode = DataGridViewTriState.True;
			this.dataGridViewHor.ColumnHeadersDefaultCellStyle = control;
			this.dataGridViewHor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewHor.Columns.AddRange(new DataGridViewColumn[] { this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.hrzDist, this.UwxEkbyfru });
			
			window.Alignment = DataGridViewContentAlignment.MiddleCenter;
			window.BackColor = SystemColors.Window;
			window.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			window.ForeColor = SystemColors.ControlText;
			window.SelectionBackColor = SystemColors.Highlight;
			window.SelectionForeColor = SystemColors.HighlightText;
			window.WrapMode = DataGridViewTriState.False;
			this.dataGridViewHor.DefaultCellStyle = window;
			this.dataGridViewHor.Dock = DockStyle.Fill;
			this.dataGridViewHor.Location = new System.Drawing.Point(3, 3);
			this.dataGridViewHor.MultiSelect = false;
			this.dataGridViewHor.Name = "dataGridViewHor";
			this.dataGridViewHor.ReadOnly = true;
			font.Alignment = DataGridViewContentAlignment.MiddleCenter;
			font.BackColor = SystemColors.Control;
			font.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			font.ForeColor = SystemColors.WindowText;
			font.SelectionBackColor = SystemColors.Highlight;
			font.SelectionForeColor = SystemColors.HighlightText;
			font.WrapMode = DataGridViewTriState.True;
			this.dataGridViewHor.RowHeadersDefaultCellStyle = font;
			this.dataGridViewHor.RowHeadersVisible = false;
			this.dataGridViewHor.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridViewHor.RowTemplate.Height = 20;
			this.dataGridViewHor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewHor.Size = new System.Drawing.Size(533, 250);
			this.dataGridViewHor.TabIndex = 27;
			this.dataGridViewHor.CellClick += new DataGridViewCellEventHandler(this.dataGridViewHor_CellClick);
			this.dataGridViewTextBoxColumn1.HeaderText = "序号";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewTextBoxColumn2.HeaderText = "管线性质";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewTextBoxColumn3.HeaderText = "编号";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.hrzDist.DefaultCellStyle = dataGridViewCellStyle3;
			this.hrzDist.HeaderText = "水平净距(米)";
			this.hrzDist.Name = "hrzDist";
			this.hrzDist.ReadOnly = true;
			controlText.Alignment = DataGridViewContentAlignment.MiddleCenter;
			controlText.BackColor = SystemColors.Window;
			controlText.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			controlText.ForeColor = SystemColors.ControlText;
			controlText.SelectionBackColor = SystemColors.Highlight;
			controlText.SelectionForeColor = SystemColors.HighlightText;
			controlText.WrapMode = DataGridViewTriState.False;
			this.UwxEkbyfru.DefaultCellStyle = controlText;
			this.UwxEkbyfru.HeaderText = "标准";
			this.UwxEkbyfru.Name = "dataGridViewTextBoxColumn4";
			this.UwxEkbyfru.ReadOnly = true;
			this.ejsEghpDqe.Controls.Add(this.dataGridViewVer);
			this.ejsEghpDqe.Location = new System.Drawing.Point(4, 22);
			this.ejsEghpDqe.Name = "tabPageVer";
			this.ejsEghpDqe.Padding = new System.Windows.Forms.Padding(3);
			this.ejsEghpDqe.Size = new System.Drawing.Size(539, 256);
			this.ejsEghpDqe.TabIndex = 1;
			this.ejsEghpDqe.Text = "垂直净距";
			this.ejsEghpDqe.UseVisualStyleBackColor = true;
			this.dataGridViewVer.AllowUserToAddRows = false;
			this.dataGridViewVer.AllowUserToDeleteRows = false;
			this.dataGridViewVer.AllowUserToResizeRows = false;
			this.dataGridViewVer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			windowText.Alignment = DataGridViewContentAlignment.MiddleCenter;
			windowText.BackColor = SystemColors.Control;
			windowText.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			windowText.ForeColor = SystemColors.WindowText;
			windowText.SelectionBackColor = SystemColors.Highlight;
			windowText.SelectionForeColor = SystemColors.HighlightText;
			windowText.WrapMode = DataGridViewTriState.True;
			this.dataGridViewVer.ColumnHeadersDefaultCellStyle = windowText;
			this.dataGridViewVer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewVer.Columns.AddRange(new DataGridViewColumn[] { this.序号, this.PipeDataset, this.FID, this.verDist, this.stanDist });
			
			this.dataGridViewVer.Dock = DockStyle.Fill;
			this.dataGridViewVer.Location = new System.Drawing.Point(3, 3);
			this.dataGridViewVer.MultiSelect = false;
			this.dataGridViewVer.Name = "dataGridViewVer";
			this.dataGridViewVer.ReadOnly = true;
			this.dataGridViewVer.RowHeadersVisible = false;
			this.dataGridViewVer.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewVer.RowTemplate.Height = 20;
			this.dataGridViewVer.Size = new System.Drawing.Size(533, 250);
			this.dataGridViewVer.TabIndex = 6;
			this.dataGridViewVer.CellClick += new DataGridViewCellEventHandler(this.dataGridViewVer_CellClick);
			this.序号.HeaderText = "序号";
			this.序号.Name = "序号";
			this.序号.ReadOnly = true;
			this.PipeDataset.HeaderText = "管线性质";
			this.PipeDataset.Name = "PipeDataset";
			this.PipeDataset.ReadOnly = true;
			this.FID.HeaderText = "编号";
			this.FID.Name = "FID";
			this.FID.ReadOnly = true;
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
			this.tabPageRollCenter.Controls.Add(this.dataGridViewCenterLine);
			this.tabPageRollCenter.Location = new System.Drawing.Point(4, 22);
			this.tabPageRollCenter.Name = "tabPageRollCenter";
			this.tabPageRollCenter.Size = new System.Drawing.Size(539, 256);
			this.tabPageRollCenter.TabIndex = 2;
			this.tabPageRollCenter.Text = "道路中心线";
			this.tabPageRollCenter.UseVisualStyleBackColor = true;
			this.dataGridViewCenterLine.AllowUserToAddRows = false;
			this.dataGridViewCenterLine.AllowUserToDeleteRows = false;
			this.dataGridViewCenterLine.AllowUserToResizeRows = false;
			this.dataGridViewCenterLine.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			highlight.Alignment = DataGridViewContentAlignment.MiddleCenter;
			highlight.BackColor = SystemColors.Control;
			highlight.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			highlight.ForeColor = SystemColors.WindowText;
			highlight.SelectionBackColor = SystemColors.Highlight;
			highlight.SelectionForeColor = SystemColors.HighlightText;
			highlight.WrapMode = DataGridViewTriState.True;
			this.dataGridViewCenterLine.ColumnHeadersDefaultCellStyle = highlight;
			this.dataGridViewCenterLine.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCenterLine.Columns.AddRange(new DataGridViewColumn[] { this.dataGridViewTextBoxColumn5, this.dataGridViewTextBoxColumn6, this.dataGridViewTextBoxColumn7, this.dataGridViewTextBoxColumn8 });
			
			highlightText.Alignment = DataGridViewContentAlignment.MiddleCenter;
			highlightText.BackColor = SystemColors.Window;
			highlightText.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			highlightText.ForeColor = SystemColors.ControlText;
			highlightText.SelectionBackColor = SystemColors.Highlight;
			highlightText.SelectionForeColor = SystemColors.HighlightText;
			highlightText.WrapMode = DataGridViewTriState.False;
			this.dataGridViewCenterLine.DefaultCellStyle = highlightText;
			this.dataGridViewCenterLine.Dock = DockStyle.Fill;
			this.dataGridViewCenterLine.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewCenterLine.MultiSelect = false;
			this.dataGridViewCenterLine.Name = "dataGridViewCenterLine";
			this.dataGridViewCenterLine.ReadOnly = true;
			control1.Alignment = DataGridViewContentAlignment.MiddleCenter;
			control1.BackColor = SystemColors.Control;
			control1.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			control1.ForeColor = SystemColors.WindowText;
			control1.SelectionBackColor = SystemColors.Highlight;
			control1.SelectionForeColor = SystemColors.HighlightText;
			control1.WrapMode = DataGridViewTriState.True;
			this.dataGridViewCenterLine.RowHeadersDefaultCellStyle = control1;
			this.dataGridViewCenterLine.RowHeadersVisible = false;
			this.dataGridViewCenterLine.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridViewCenterLine.RowTemplate.Height = 20;
			this.dataGridViewCenterLine.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewCenterLine.Size = new System.Drawing.Size(539, 256);
			this.dataGridViewCenterLine.TabIndex = 28;
			this.dataGridViewCenterLine.CellClick += new DataGridViewCellEventHandler(this.dataGridViewCenterLine_CellClick);
			this.dataGridViewTextBoxColumn5.HeaderText = "序号";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewTextBoxColumn6.HeaderText = "道路名称";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridViewTextBoxColumn7.HeaderText = "编号";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle6;
			this.dataGridViewTextBoxColumn8.HeaderText = "水平净距(米)";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			this.timer_0.Interval = 500;
			this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(553, 395);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormApproveFromCAD";
			base.ShowIcon = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "审批辅助分析";
			base.FormClosing += new FormClosingEventHandler(this.GForm0_FormClosing);
			base.FormClosed += new FormClosedEventHandler(this.GForm0_FormClosed);
			base.Load += new EventHandler(this.GForm0_Load);
			base.HelpRequested += new HelpEventHandler(this.GForm0_HelpRequested);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.QfyEqiOwEk.ResumeLayout(false);
			this.tabPageApprove.ResumeLayout(false);
			((ISupportInitialize)this.dataGridViewSelectItem).EndInit();
			this.tabPageHor.ResumeLayout(false);
			((ISupportInitialize)this.dataGridViewHor).EndInit();
			this.ejsEghpDqe.ResumeLayout(false);
			((ISupportInitialize)this.dataGridViewVer).EndInit();
			this.tabPageRollCenter.ResumeLayout(false);
			((ISupportInitialize)this.dataGridViewCenterLine).EndInit();
			base.ResumeLayout(false);
		}

		public void LoadMapLayer()
		{
			this.list_2.Clear();
			this.ifeatureClass_1 = null;
			if ((this.m_app == null || this.m_app.FocusMap == null ? false : this.m_app.FocusMap.get_Map() != null))
			{
				for (int i = 0; i < this.m_app.FocusMap.get_Map().LayerCount; i++)
				{
					ILayer layer = this.m_app.FocusMap.get_Map().get_Layer(i);
					this.AddLayer(layer, this.list_2);
				}
			}
			this.string_0 = this.m_app.PipeConfig.get_Kind();
			this.string_1 = this.m_app.PipeConfig.get_Diameter();
			this.string_2 = this.m_app.PipeConfig.get_Section_Size();
			this.method_12();
		}

		private void method_0()
		{
			CommonUtils.m_pProjectFrameWorkApp = this.m_app;
			this.ipolyline_1 = this.method_13(this.ipolyline_0);
			ITopologicalOperator ipolyline0 = (ITopologicalOperator)this.ipolyline_0;
			double num = Convert.ToDouble(this.tbBufferRadius.Text);
			if (num > 0)
			{
				IGeometry geometry = ipolyline0.Buffer(num);
				ISpatialFilter spatialFilterClass = new SpatialFilter();
				spatialFilterClass.Geometry=(geometry);
				spatialFilterClass.SpatialRel=(1);
				if (this.list_2.Count >= 1)
				{
					this.list_4.Clear();
					this.list_3.Clear();
					this.list_5.Clear();
					foreach (IFeatureLayer list2 in this.list_2)
					{
						if (!list2.Visible)
						{
							continue;
						}
						IFeatureClass featureClass = list2.FeatureClass;
						if ((featureClass.AliasName == this.ifeatureClass_0.AliasName ? true : featureClass.ShapeType != 3))
						{
							continue;
						}
						spatialFilterClass.Geometry=Field(featureClass.ShapeFieldName);
						this.method_1(featureClass.Search(spatialFilterClass, false));
						this.method_2();
						this.method_3(featureClass.Search(spatialFilterClass, false));
						this.method_4();
					}
					if (this.ifeatureClass_1 != null)
					{
						spatialFilterClass.Geometry=Field(this.ifeatureClass_1.ShapeFieldName);
						this.method_8(this.ifeatureClass_1.Search(spatialFilterClass, false));
						this.method_9();
					}
				}
				else
				{
					MessageBox.Show("当前地图不可用!");
				}
			}
			else
			{
				MessageBox.Show("分析半径不能为空!");
			}
		}

		private void method_1(IFeatureCursor featureCursor)
		{
			double double1 = this.double_1 * 0.0005;
			IProximityOperator ipolyline1 = (IProximityOperator)this.ipolyline_1;
			IFeature feature = featureCursor.NextFeature();
			int num = -1;
			int num1 = -1;
			int num2 = -1;
			if (feature != null)
			{
				num = feature.Fields.FindField(this.string_1);
				num1 = feature.Fields.FindField(this.string_2);
				num2 = feature.Fields.FindField(this.string_0);
			}
			if (num2 >= 0)
			{
				if ((num >= 0 ? true : num1 >= 0))
				{
					while (feature != null)
					{
						IPolyline polyline = this.method_13(feature.Shape as IPolyline);
						double num3 = 0;
						double num4 = this.method_14(feature, num, num1, out num3);
						if (num4 < 10)
						{
							num4 = 10;
						}
						num4 = num4 * 0.0005;
						double num5 = ipolyline1.ReturnDistance(polyline);
						if (num5 >= 0.001)
						{
							num5 = num5 - double1 - num4;
							if (num5 < 0.001)
							{
								num5 = 0;
							}
						}
						else
						{
							num5 = 0;
						}
						string str = feature.get_Value(num2).ToString();
						CHitAnalyse.CItem cItem = new CHitAnalyse.CItem()
						{
							_OID = feature.OID,
							_sKind = str,
							_dHorDistance = num5,
							_dHorBase = 0,
							_pClass = (IFeatureClass)feature.Class
						};
						string value = this.dataGridViewComboBoxCell_0.Value as string;
						if (this.dictionary_0.ContainsKey(value))
						{
							value = this.dictionary_0[value].ToString();
						}
						cItem._dHorBase = (double)CommonUtils.GetPipeLineAlarmHrzDistByFeatureClassName2(CommonUtils.GetSmpClassName(value), CommonUtils.GetSmpClassName(feature.Class.AliasName), this.ifeature_0, feature);
						this.list_3.Add(cItem);
						feature = featureCursor.NextFeature();
					}
				}
			}
		}

		private void method_10()
		{
			if ((this.list_1 == null ? false : this.list_1.Count > 0))
			{
				for (int i = 0; i < this.list_1.Count; i++)
				{
					this.m_app.FocusMap.get_Map().DeleteLayer(this.list_1[i]);
				}
			}
		}

		private string method_11(object obj)
		{
			return ((Convert.IsDBNull(obj) ? false : obj != null) ? obj.ToString() : "");
		}

		private void method_12()
		{
			this.ipolyline_0 = null;
			if (this.ifeature_0 != null)
			{
				int num = this.ifeature_0.Fields.FindField(this.string_1);
				int num1 = this.ifeature_0.Fields.FindField(this.string_2);
				if (this.ifeature_0.Fields.FindField(this.string_0) >= 0)
				{
					if ((num >= 0 ? true : num1 >= 0))
					{
						this.double_1 = this.method_14(this.ifeature_0, num, num1, out this.double_0);
						if (this.double_1 < 1)
						{
							this.double_1 = 10;
						}
						if (this.double_0 < 1)
						{
							this.double_0 = this.double_1;
						}
						this.int_0 = this.m_app.PipeConfig.getLineConfig_HeightFlag(CommonUtils.GetSmpClassName(this.ifeatureClass_0.AliasName));
					}
				}
			}
		}

		private IPolyline method_13(IPolyline polyline)
		{
			object missing = Type.Missing;
			IPolyline polylineClass = new Polyline();
			IPointCollection pointCollection = (IPointCollection)polylineClass;
			IPointCollection pointCollection1 = (IPointCollection)polyline;
			for (int i = 0; i <= pointCollection1.PointCount - 1; i++)
			{
				IPoint point = pointCollection1.get_Point(i);
				PointClass pointClass = new Point();
				pointClass.X=(point.X);
				pointClass.Y=(point.Y);
				pointClass.Z=(0);
				pointCollection.AddPoint(pointClass, ref missing, ref missing);
			}
			return polylineClass;
		}

		private double method_14(IFeature feature, int num, int num, out double double_2)
		{
			double_2 = 0;
			double num1 = 0;
			object value = null;
			if (num > 0)
			{
				value = feature.get_Value(num);
				num1 = ((value == null || Convert.IsDBNull(value) ? false : Regex.IsMatch(value.ToString(), "^\\d+$")) ? Convert.ToDouble(value) : 0);
			}
			if (num1 >= 1)
			{
				double_2 = num1;
			}
			else
			{
				num1 = (!Convert.IsDBNull(value) ? this.method_15(feature, num, out double_2) : this.method_15(feature, num, out double_2));
			}
			return num1;
		}

		private double method_15(IFeature feature, int num, out double double_2)
		{
			double num1 = 0;
			double_2 = 0;
			string str = "";
			if (num > 0)
			{
				object value = feature.get_Value(num);
				if (!Convert.IsDBNull(value))
				{
					str = Convert.ToString(value);
				}
				if ((str == null ? false : str.Length >= 1))
				{
					string[] strArrays = str.Split(new char[] { 'x', 'X', 'Х', '×' });
					num1 = Convert.ToDouble(strArrays[0]);
					double_2 = Convert.ToDouble(strArrays[1]);
				}
			}
			return num1;
		}

		private void method_16()
		{
			this.dictionary_0.Clear();
			CommonUtils.ThrougAllLayer(this.m_app.FocusMap.get_Map(), new CommonUtils.DealLayer(this.AddName));
		}

		private void method_2()
		{
			List<CHitAnalyse.CItem> list3 = this.list_3;
			this.dataGridViewHor.Rows.Clear();
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle()
			{
				BackColor = Color.Red
			};
			if (list3.Count > 0)
			{
				this.dataGridViewHor.Rows.Add(list3.Count);
				for (int i = 0; i < list3.Count; i++)
				{
					CHitAnalyse.CItem item = list3[i];
					DataGridViewRow str = this.dataGridViewHor.Rows[i];
					str.Tag = item._pClass;
					int num = i + 1;
					str.Cells[0].Value = num.ToString();
					str.Cells[1].Value = item._sKind;
					str.Cells[2].Value = item._OID.ToString();
					str.Cells[3].Value = item._dHorDistance.ToString("0.000");
					if (item._dHorBase > 0.001)
					{
						str.Cells[4].Value = item._dHorBase.ToString("0.000");
					}
					if (item._dHorDistance < item._dHorBase)
					{
						str.Cells[3].Style = dataGridViewCellStyle;
					}
				}
			}
		}

		private void method_3(IFeatureCursor featureCursor)
		{
			double double0 = this.double_0 * 0.0005;
			ITopologicalOperator ipolyline1 = (ITopologicalOperator)this.ipolyline_1;
			IFeature feature = featureCursor.NextFeature();
			int num = -1;
			int num1 = -1;
			int num2 = -1;
			if (feature != null)
			{
				num = feature.Fields.FindField(this.string_1);
				num1 = feature.Fields.FindField(this.string_2);
				num2 = feature.Fields.FindField(this.string_0);
			}
			if (num2 >= 0)
			{
				if ((num >= 0 ? true : num1 >= 0))
				{
					int lineConfigHeightFlag = this.m_app.PipeConfig.getLineConfig_HeightFlag(CommonUtils.GetSmpClassName(feature.Class.AliasName));
					while (feature != null)
					{
						IPolyline shape = feature.Shape as IPolyline;
						IPolyline polyline = this.method_13(shape);
						double num3 = 0;
						this.method_14(feature, num, num1, out num3);
						if (num3 < 10)
						{
							num3 = 10;
						}
						num3 = num3 * 0.0005;
						IGeometry geometry = ipolyline1.Intersect(polyline, 1);
						if (geometry != null)
						{
							IPoint point = null;
							if (geometry is IPoint)
							{
								point = (IPoint)geometry;
							}
							else if (geometry is IMultipoint)
							{
								IPointCollection pointCollection = (IPointCollection)geometry;
								if (pointCollection.PointCount > 0)
								{
									point = pointCollection.get_Point(0);
								}
							}
							if (point != null)
							{
								int num4 = this.method_5(this.ipolyline_1, point);
								double num5 = this.method_6(this.ipolyline_0, point, num4);
								if (this.int_0 == 0)
								{
									num5 = num5 - double0;
								}
								else if (2 == this.int_0)
								{
									num5 = num5 + double0;
								}
								int num6 = this.method_5(polyline, point);
								double num7 = this.method_6(shape, point, num6);
								if (lineConfigHeightFlag == 0)
								{
									num7 = num7 - num3;
								}
								else if (2 == lineConfigHeightFlag)
								{
									num7 = num7 + num3;
								}
								double num8 = Math.Abs(num5 - num7);
								num8 = num8 - double0 - num3;
								if (num8 < 0.001)
								{
									num8 = 0;
								}
								string str = feature.get_Value(num2).ToString();
								CHitAnalyse.CItem cItem = new CHitAnalyse.CItem()
								{
									_OID = feature.OID,
									_sKind = str,
									_dVerDistance = num8,
									_dVerBase = 0,
									_pClass = (IFeatureClass)feature.Class
								};
								string str1 = this.method_7(feature);
								string str2 = this.method_7(this.ifeature_0);
								string value = this.dataGridViewComboBoxCell_0.Value as string;
								if (this.dictionary_0.ContainsKey(value))
								{
									value = this.dictionary_0[value].ToString();
								}
								cItem._dVerBase = (double)CommonUtils.GetPipeLineAlarmVerDistByFeatureClassName(CommonUtils.GetSmpClassName(value), CommonUtils.GetSmpClassName(feature.Class.AliasName), str2, str1);
								this.list_4.Add(cItem);
							}
						}
						feature = featureCursor.NextFeature();
					}
				}
			}
		}

		private void method_4()
		{
			List<CHitAnalyse.CItem> list4 = this.list_4;
			this.dataGridViewVer.Rows.Clear();
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle()
			{
				BackColor = Color.Red
			};
			if (list4.Count > 0)
			{
				this.dataGridViewVer.Rows.Add(list4.Count);
				for (int i = 0; i < list4.Count; i++)
				{
					CHitAnalyse.CItem item = list4[i];
					DataGridViewRow str = this.dataGridViewVer.Rows[i];
					str.Tag = item._pClass;
					int num = i + 1;
					str.Cells[0].Value = num.ToString();
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

		private int method_5(IPolyline polyline, IPoint point)
		{
			int num;
			int num1 = -1;
			IPointCollection pointCollection = (IPointCollection)polyline;
			if (pointCollection.PointCount != 2)
			{
				ISegmentCollection segmentCollection = (ISegmentCollection)polyline;
				int pointCount = pointCollection.PointCount;
				bool[] flagArray = new bool[pointCount];
				bool[] flagArray1 = new bool[2];
				for (int i = 0; i < pointCount; i++)
				{
					IPoint point1 = pointCollection.get_Point(i);
					if (point.X < point1.X)
					{
						flagArray[i] = false;
					}
					else if ((point.X != point1.X ? false : point.Y == point1.Y))
					{
						num = i;
						return num;
					}
					else
					{
						flagArray[i] = true;
					}
				}
				for (int j = 0; j < pointCount - 1; j++)
				{
					if (flagArray[j] != flagArray[j + 1])
					{
						IPoint point2 = pointCollection.get_Point(j);
						IPoint point3 = pointCollection.get_Point(j + 1);
						if (point.Y < point2.Y)
						{
							flagArray1[0] = false;
						}
						else if ((point.Y != point2.Y ? false : point.Y == point3.Y))
						{
							num = j;
							return num;
						}
						else
						{
							flagArray1[0] = true;
						}
						if (point.Y >= point3.Y)
						{
							flagArray1[1] = true;
						}
						else
						{
							flagArray1[1] = false;
						}
						if (flagArray1[0] != flagArray1[1] && Math.Abs(((IProximityOperator)segmentCollection.get_Segment(j)).ReturnDistance(point)) < 0.001)
						{
							num = j;
							return num;
						}
					}
				}
				if (num1 < 0)
				{
					for (int k = 0; k < pointCount - 1; k++)
					{
						if (flagArray[k] == flagArray[k + 1])
						{
							IPoint point4 = pointCollection.get_Point(k);
							IPoint point5 = pointCollection.get_Point(k + 1);
							if ((point.X != point4.X ? false : point.X == point5.X))
							{
								num = k;
								return num;
							}
						}
					}
				}
				num = num1;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		private double method_6(IPolyline polyline, IPoint point, int num)
		{
			double z;
			IPointCollection pointCollection = (IPointCollection)polyline;
			IPoint point1 = pointCollection.get_Point(num);
			IPoint point2 = pointCollection.get_Point(num + 1);
			double x = point2.X - point1.X;
			double y = point2.Y - point1.Y;
			double z1 = point2.Z - point1.Z;
			double x1 = point.X - point1.X;
			double y1 = point.Y - point1.Y;
			double num1 = Math.Sqrt(x * x + y * y);
			double num2 = Math.Sqrt(x1 * x1 + y1 * y1);
			if (num1 >= 0.001)
			{
				double num3 = num2 / num1;
				z = num3 * z1 + point1.Z;
			}
			else
			{
				z = point1.Z;
			}
			return z;
		}

		private string method_7(IFeature feature)
		{
			string str;
			string str1;
			if (feature != null)
			{
				int num = feature.Fields.FindField("埋设方式");
				str1 = (num == -1 ? "" : CommonUtils.ObjToString(feature.get_Value(num)));
				str = str1;
			}
			else
			{
				str = "";
			}
			return str;
		}

		private void method_8(IFeatureCursor featureCursor)
		{
			double double1 = this.double_1 * 0.0005;
			IProximityOperator ipolyline1 = (IProximityOperator)this.ipolyline_1;
			IFeature feature = featureCursor.NextFeature();
			int num = -1;
			if (feature != null)
			{
				num = feature.Fields.FindField("名称");
			}
			if (num >= 0)
			{
				while (feature != null)
				{
					IPolyline shape = feature.Shape as IPolyline;
					double num1 = ipolyline1.ReturnDistance(this.method_13(shape));
					if (num1 >= 0.001)
					{
						num1 = num1 - double1;
						if (num1 < 0.001)
						{
							num1 = 0;
						}
					}
					else
					{
						num1 = 0;
					}
					string str = feature.get_Value(num).ToString();
					CHitAnalyse.CItem cItem = new CHitAnalyse.CItem()
					{
						_OID = feature.OID,
						_sKind = str,
						_dHorDistance = num1,
						_dHorBase = 0,
						_pClass = (IFeatureClass)feature.Class
					};
					this.list_5.Add(cItem);
					feature = featureCursor.NextFeature();
				}
			}
		}

		private void method_9()
		{
			List<CHitAnalyse.CItem> list5 = this.list_5;
			this.dataGridViewCenterLine.Rows.Clear();
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle()
			{
				BackColor = Color.Red
			};
			if (list5.Count > 0)
			{
				this.dataGridViewCenterLine.Rows.Add(list5.Count);
				for (int i = 0; i < list5.Count; i++)
				{
					CHitAnalyse.CItem item = list5[i];
					DataGridViewRow str = this.dataGridViewCenterLine.Rows[i];
					str.Tag = item._pClass;
					int num = i + 1;
					str.Cells[0].Value = num.ToString();
					str.Cells[1].Value = item._sKind;
					str.Cells[2].Value = item._OID.ToString();
					str.Cells[3].Value = item._dHorDistance.ToString("0.000");
					if (item._dVerDistance < 0.001)
					{
						str.Cells[3].Style = dataGridViewCellStyle;
					}
				}
			}
		}

		private void qsTayfWbAn(object obj, EventArgs eventArg)
		{
			IFeatureLayer cadFeatureLayerClass;
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = "CAD文件(*.dwg;*.dxf)|*.dwg;*.dxf"
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				IWorkspaceFactory cadWorkspaceFactoryClass = new CadWorkspaceFactory();
				string fileName = openFileDialog.FileName;
				int num = fileName.LastIndexOf('\\');
				fileName = fileName.Substring(0, num);
				IWorkspace workspace = cadWorkspaceFactoryClass.OpenFromFile(fileName, this.m_app.FocusMap.get_hWnd());
				if (workspace != null)
				{
					IFeatureWorkspace featureWorkspace = workspace as IFeatureWorkspace;
					string str = openFileDialog.FileName.Substring(num + 1);
					IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(str);
					if (featureDataset != null)
					{
						IFeatureClassContainer featureClassContainer = featureDataset as IFeatureClassContainer;
						long classCount = (long)featureClassContainer.ClassCount;
						this.textBoxLoadCADName.Text = openFileDialog.FileName;
						this.list_0.Clear();
						for (int i = 0; (long)i < classCount; i++)
						{
							IFeatureClass @class = featureClassContainer.get_Class(i);
							if (@class != null)
							{
								if (@class.FeatureType != 12)
								{
									cadFeatureLayerClass = new CadFeatureLayer();
								}
								else
								{
									cadFeatureLayerClass = new CadAnnotationLayer();
								}
								cadFeatureLayerClass.Name=(@class.AliasName);
								cadFeatureLayerClass.set_FeatureClass(@class);
								cadFeatureLayerClass.set_Selectable(true);
								this.m_app.FocusMap.AddLayer(cadFeatureLayerClass, i);
								if (cadFeatureLayerClass.FeatureClass.ShapeType == 3)
								{
									this.list_0.Add(cadFeatureLayerClass);
								}
								this.list_1.Add(cadFeatureLayerClass);
							}
						}
						int num1 = 0;
						this.dataGridViewSelectItem.Rows.Clear();
						for (int j = 0; j < this.list_0.Count; j++)
						{
							IFeatureLayer item = this.list_0[j];
							IFeatureClass featureClass = item.FeatureClass;
							int num2 = featureClass.FeatureCount(null);
							if (num2 > 0)
							{
								IFeatureCursor featureCursor = featureClass.Search(null, false);
								this.dataGridViewSelectItem.Rows.Add(num2);
								for (int k = 0; k < num2; k++)
								{
									IFeature feature = featureCursor.NextFeature();
									DataGridViewRow name = this.dataGridViewSelectItem.Rows[k + num1];
									name.Tag = item;
									name.Cells[0].Value = k + num1 + 1;
									name.Cells[1].Value = item.Name;
									name.Cells[2].Value = feature.OID;
									name.Cells[3].Value = 0;
									name.Cells[4].Value = 0;
									DataGridViewComboBoxCell dataGridViewComboBoxCell = (DataGridViewComboBoxCell)name.Cells[5];
									IEnumerator enumerator = this.dictionary_0.Keys.GetEnumerator();
									enumerator.Reset();
									for (int l = 0; l < this.dictionary_0.Keys.Count; l++)
									{
										enumerator.MoveNext();
										string str1 = enumerator.Current.ToString();
										dataGridViewComboBoxCell.Items.Add(str1);
									}
								}
								num1 = num1 + num2;
							}
						}
						this.m_app.FocusMap.get_ActiveView().Refresh();
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
			GForm0 mNTimerCount = this;
			mNTimerCount.m_nTimerCount = mNTimerCount.m_nTimerCount + 1;
		}
	}
}