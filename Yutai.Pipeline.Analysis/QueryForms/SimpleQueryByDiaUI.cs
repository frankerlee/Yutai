using ApplicationData;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using PipeConfig;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class SimpleQueryByDiaUI : Form
	{
		private class LayerboxItem
		{
			public IFeatureLayer m_pPipeLayer;

			public override string ToString()
			{
				return this.m_pPipeLayer.Name;
			}
		}

		public IGeometry m_ipGeo;

		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipeConfig pPipeCfg;

		private IFeatureLayer SelectLayer;

		private IFields myfields;

		private string strGJ;

		private string strKG;

		private IField myfieldGJ;

		private IField myfieldKG;

		private QueryResult resultDlg;

		public object mainform;

		public ushort DrawType;

		public ushort SelectType;

		private ContextMenuStrip contextMenuStrip1;

		private IContainer components = null;

		private GroupBox groupBox1;

		private Button QueryBut;

		private Button CloseBut;

		private Label lable;

		private ComboBox LayerBox;

		private Label label2;

		private ComboBox ValueBox1;

		private Label label1;

		private ComboBox OperateBox;

		private ComboBox ValueBox2;

		private RadioButton radioButton2;

		private ComboBox ValueBox3;

		private Label label3;

		private ComboBox OperateBox2;

		private RadioButton radioButton1;

		private CheckBox GeometrySet;

		private ToolStripMenuItem MenuItemSelectType;

		private ToolStripMenuItem ByEnvelope;

		private ToolStripMenuItem ByPolygon;

		private ToolStripMenuItem ByCircle;

		private ToolStripMenuItem MenuItemDataSelectType;

		private ToolStripMenuItem CrossesSelect;

		private ToolStripMenuItem WithinSelect;

		public bool SelectGeometry
		{
			get
			{
				return this.GeometrySet.Checked;
			}
			set
			{
				this.GeometrySet.Checked = value;
			}
		}

		public SimpleQueryByDiaUI()
		{
			this.InitializeComponent();
		}

		private void SimpleQueryByDiaUI_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		public void AutoFlash()
		{
			this.OperateBox.SelectedIndex = 0;
			this.OperateBox2.SelectedIndex = 0;
			this.FillLayerBox();
			this.ValidateField();
			this.FillValueBox();
		}

		private void AddLayer(ILayer ipLay)
		{
			if (ipLay is IFeatureLayer)
			{
				this.AddFeatureLayer((IFeatureLayer)ipLay);
			}
			else if (ipLay is IGroupLayer)
			{
				this.AddGroupLayer((IGroupLayer)ipLay);
			}
		}

		private void AddGroupLayer(IGroupLayer iGLayer)
		{
			ICompositeLayer compositeLayer = (ICompositeLayer)iGLayer;
			if (compositeLayer != null)
			{
				int count = compositeLayer.Count;
				for (int i = 0; i < count; i++)
				{
					ILayer ipLay = compositeLayer.get_Layer(i);
					this.AddLayer(ipLay);
				}
			}
		}

		private void AddFeatureLayer(IFeatureLayer iFLayer)
		{
			if (iFLayer != null)
			{
				string aliasName = iFLayer.FeatureClass.AliasName;
				if (this.pPipeCfg.IsPipeLine(aliasName))
				{
					SimpleQueryByDiaUI.LayerboxItem layerboxItem = new SimpleQueryByDiaUI.LayerboxItem();
					layerboxItem.m_pPipeLayer = iFLayer;
					this.LayerBox.Items.Add(layerboxItem);
				}
			}
		}

		private void FillLayerBox()
		{
			this.LayerBox.Items.Clear();
			int layerCount = m_context.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer layer = m_context.FocusMap.get_Layer(i);
				if (layer.Valid)
				{
				}
				this.AddLayer(layer);
			}
			if (this.LayerBox.Items.Count > 0)
			{
				this.LayerBox.SelectedIndex = 0;
			}
		}

		private void FillValueBox()
		{
			if (this.myfields != null)
			{
				int num = -1;
				if (this.radioButton1.Enabled)
				{
					num = this.myfields.FindField(this.strGJ);
				}
				int num2 = -1;
				if (this.radioButton2.Enabled)
				{
					num2 = this.myfields.FindField(this.strKG);
				}
				if (num >= 0)
				{
					this.myfieldGJ = this.myfields.get_Field(num);
				}
				else
				{
					this.radioButton1.Checked = true;
				}
				if (num2 >= 0)
				{
					this.myfieldKG = this.myfields.get_Field(num2);
					if (num < 0)
					{
						this.radioButton2.Checked = true;
					}
				}
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
				IQueryFilter queryFilter = new QueryFilter();
				IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
				IFeature feature = featureCursor.NextFeature();
				this.ValueBox1.Items.Clear();
				this.ValueBox2.Items.Clear();
				this.ValueBox3.Items.Clear();
				if (num2 >= 0 || num >= 0)
				{
					while (feature != null)
					{
						if (num >= 0)
						{
							object obj = feature.get_Value(num);
							string text;
							if (obj is DBNull)
							{
								text = "0";
							}
							else
							{
								text = obj.ToString();
								if (text.Length == 0)
								{
									text = "0";
								}
							}
							if (!this.ValueBox1.Items.Contains(text))
							{
								this.ValueBox1.Items.Add(text);
								this.ValueBox2.Items.Add(text);
							}
						}
						if (num2 >= 0)
						{
							object obj = feature.get_Value(num2);
							string text;
							if (obj is DBNull)
							{
								text = "NULL";
							}
							else
							{
								text = obj.ToString();
								if (text.Length == 0)
								{
									text = "空字段值";
								}
							}
							if (!this.ValueBox3.Items.Contains(text))
							{
								this.ValueBox3.Items.Add(text);
							}
						}
						feature = featureCursor.NextFeature();
					}
					if (this.ValueBox1.Items.Count > 0)
					{
						this.ValueBox1.SelectedIndex = 0;
					}
					if (this.ValueBox2.Items.Count > 0)
					{
						this.ValueBox2.SelectedIndex = 0;
					}
					if (this.ValueBox3.Items.Count > 0)
					{
						this.ValueBox3.SelectedIndex = 0;
					}
				}
			}
		}

		private void ValidateField()
		{
			int selectedIndex = this.LayerBox.SelectedIndex;
			if (selectedIndex >= 0)
			{
				this.SelectLayer = null;
				if (this.MapControl != null)
				{
					this.SelectLayer = ((SimpleQueryByDiaUI.LayerboxItem)this.LayerBox.SelectedItem).m_pPipeLayer;
					if (this.SelectLayer != null)
					{
						this.myfields = this.SelectLayer.FeatureClass.Fields;
						this.strGJ = this.pPipeCfg.GetLineTableFieldName("管径");
						this.strKG = this.pPipeCfg.GetLineTableFieldName("断面尺寸");
						if (this.myfields.FindField(this.strGJ) < 0)
						{
							this.radioButton1.Enabled = false;
							this.OperateBox.Enabled = false;
							this.ValueBox1.Enabled = false;
							this.ValueBox2.Enabled = false;
							this.radioButton1.Checked = false;
						}
						else
						{
							this.radioButton1.Enabled = true;
							this.OperateBox.Enabled = true;
							this.ValueBox1.Enabled = true;
							this.ValueBox2.Enabled = true;
						}
						if (this.myfields.FindField(this.strKG) < 0)
						{
							this.radioButton2.Enabled = false;
							this.radioButton2.Checked = false;
							this.OperateBox2.Enabled = false;
							this.ValueBox3.Enabled = false;
						}
						else
						{
							this.radioButton2.Enabled = true;
							this.OperateBox2.Enabled = true;
							this.ValueBox3.Enabled = true;
						}
					}
				}
			}
		}

		private void OperateBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.OperateBox.SelectedItem.ToString() == "介于")
			{
				this.label2.Visible = true;
				this.ValueBox2.Visible = true;
			}
			else
			{
				this.label2.Visible = false;
				this.ValueBox2.Visible = false;
			}
		}

		private void CloseBut_Click(object sender, EventArgs e)
		{
			if (this.SelectLayer != null)
			{
				IFeatureSelection featureSelection = (IFeatureSelection)this.SelectLayer;
				featureSelection.Clear();
				featureSelection.SelectionSet.Refresh();
				IActiveView activeView = m_context.ActiveView;
				activeView.Refresh();
			}
			this.GeometrySet.Checked = false;
			base.Close();
		}

		private void LayerBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidateField();
			this.FillValueBox();
		}

		private void QueryBut_Click(object sender, EventArgs e)
		{
			if (!this.radioButton1.Checked && !this.radioButton2.Checked)
			{
				MessageBox.Show("系统设置有误/当前层不含有管径信息，请检查配置文件/数据！");
			}
			else if (!this.radioButton1.Enabled && !this.radioButton2.Enabled)
			{
				MessageBox.Show("系统设置有误/当前层不含有管径信息，请检查配置文件/数据！");
			}
			else
			{
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
				ISpatialFilter spatialFilter = new SpatialFilter();
				IFeatureCursor pCursor = null;
				string text = "";
				if (this.radioButton1.Checked)
				{
					if (this.OperateBox.SelectedItem.ToString() == "介于")
					{
						double num = Convert.ToDouble(this.ValueBox1.Text);
						double num2 = Convert.ToDouble(this.ValueBox2.Text);
						string text2;
						string text3;
						if (num > num2)
						{
							text2 = this.ValueBox1.Text;
							text3 = this.ValueBox2.Text;
						}
						else
						{
							text3 = this.ValueBox1.Text;
							text2 = this.ValueBox2.Text;
						}
						if (text3 == text2)
						{
							if (this.myfieldGJ.Type == (esriFieldType) 4)
							{
								text = this.strGJ;
								text += " = '";
								text += text3;
								text += "'";
							}
							else
							{
								text = this.strGJ;
								text += text3;
							}
						}
						else if (this.myfieldGJ.Type == (esriFieldType) 4)
						{
							text = this.strGJ;
							text += ">= '";
							text += text3;
							text += "' and ";
							text += this.strGJ;
							text += "<= '";
							text += text2;
							text += "'";
						}
						else
						{
							text = this.strGJ;
							text += ">=";
							text += text3;
							text += " and ";
							text += this.strGJ;
							text += "<=";
							text += text2;
						}
					}
					else
					{
						text += this.strGJ;
						switch (this.OperateBox.SelectedIndex)
						{
						case 0:
							text += ">";
							break;
						case 1:
							text += ">=";
							break;
						case 2:
							text += "=";
							break;
						case 3:
							text += "<>";
							break;
						case 4:
							text += "<";
							break;
						case 5:
							text += "<=";
							break;
						}
						if (this.myfieldGJ.Type == (esriFieldType) 4)
						{
							text += "'";
							text += this.ValueBox1.Text;
							text += "'";
						}
						else
						{
							text += this.ValueBox1.Text;
						}
					}
				}
				else if (this.radioButton2.Checked)
				{
					text = this.strKG;
					switch (this.OperateBox2.SelectedIndex)
					{
					case 0:
						text += "=";
						break;
					case 1:
						text += "<>";
						break;
					}
					if (this.ValueBox3.Text == "空字段值")
					{
						text += "''";
					}
					else
					{
						text += "'";
						text += this.ValueBox3.Text;
						text += "'";
					}
				}
				spatialFilter.WhereClause=text;
				try
				{
					if (this.GeometrySet.Checked && this.m_ipGeo != null)
					{
						spatialFilter.Geometry=(this.m_ipGeo);
					}
					if (this.SelectType == 0)
					{
						spatialFilter.SpatialRel=(esriSpatialRelEnum) (1);
					}
					else if (this.SelectType == 1)
					{
						spatialFilter.SpatialRel=(7);
					}
					pCursor = featureClass.Search(spatialFilter, false);
				}
				catch (Exception)
				{
					MessageBox.Show("查询值有误,请检查!");
					return;
				}
				this.m_iApp.SetResult(pCursor, (IFeatureSelection)this.SelectLayer);
			}
		}

		private void lable_Click(object sender, EventArgs e)
		{
		}

		private void ValueBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void ByEnvelope_Click(object sender, EventArgs e)
		{
			this.ByPolygon.Checked = false;
			this.ByCircle.Checked = false;
			this.ByEnvelope.Checked = true;
			this.DrawType = 0;
		}

		private void ByPolygon_Click(object sender, EventArgs e)
		{
			this.ByPolygon.Checked = true;
			this.ByCircle.Checked = false;
			this.ByEnvelope.Checked = false;
			this.DrawType = 1;
		}

		private void ByCircle_Click(object sender, EventArgs e)
		{
			this.ByPolygon.Checked = false;
			this.ByCircle.Checked = true;
			this.ByEnvelope.Checked = false;
			this.DrawType = 2;
		}

		private void CrossesSelect_Click(object sender, EventArgs e)
		{
			this.CrossesSelect.Checked = true;
			this.WithinSelect.Checked = false;
			this.SelectType = 0;
		}

		private void WithinSelect_Click(object sender, EventArgs e)
		{
			this.CrossesSelect.Checked = false;
			this.WithinSelect.Checked = true;
			this.SelectType = 1;
		}

		private void SimpleQueryByDiaUI_VisibleChanged(object sender, EventArgs e)
		{
            if (base.Visible)
            {
                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw += AxMapControlOnOnAfterDraw;
            }
            else
            {

                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
            }
        }

        private void AxMapControlOnOnAfterDraw(object display, int viewDrawPhase)
        {

            if (viewDrawPhase == 32)
            {
                this.DrawSelGeometry();
            }
        }



   
        private void SimpleQueryByDiaUI_FormClosed(object sender, FormClosedEventArgs e)
		{
            IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
            axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw

            this.OnClosed(e);
		}

		public void DrawSelGeometry()
		{
			if (this.m_ipGeo != null)
			{
                IRgbColor rgbColor = new RgbColor();
                IColor selectionCorlor = this.m_context.Config.SelectionEnvironment.DefaultColor;
                rgbColor.RGB = selectionCorlor.RGB;
                rgbColor.Transparency = selectionCorlor.Transparency;

                object obj = null;
                int selectionBufferInPixels = this.m_context.Config.SelectionEnvironment.SearchTolerance;
                ISymbol symbol = null;
				switch ((int)this.m_ipGeo.GeometryType)
				{
				case 1:
				{
					ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
					symbol = (ISymbol)simpleMarkerSymbol;
					symbol.ROP2=(esriRasterOpCode) (10);
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
				this.MapControl.DrawShape(this.m_ipGeo, ref obj);
			}
		}

		private void SimpleQueryByDiaUI_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "快速查询";
			HelpNavigator command = HelpNavigator.KeywordIndex;
			Help.ShowHelp(this, url, command, parameter);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			this.groupBox1 = new GroupBox();
			this.radioButton2 = new RadioButton();
			this.ValueBox3 = new ComboBox();
			this.label3 = new Label();
			this.OperateBox2 = new ComboBox();
			this.radioButton1 = new RadioButton();
			this.ValueBox2 = new ComboBox();
			this.label2 = new Label();
			this.ValueBox1 = new ComboBox();
			this.label1 = new Label();
			this.OperateBox = new ComboBox();
			this.QueryBut = new Button();
			this.CloseBut = new Button();
			this.lable = new Label();
			this.LayerBox = new ComboBox();
			this.GeometrySet = new CheckBox();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.MenuItemSelectType = new ToolStripMenuItem();
			this.ByEnvelope = new ToolStripMenuItem();
			this.ByPolygon = new ToolStripMenuItem();
			this.ByCircle = new ToolStripMenuItem();
			this.MenuItemDataSelectType = new ToolStripMenuItem();
			this.CrossesSelect = new ToolStripMenuItem();
			this.WithinSelect = new ToolStripMenuItem();
			this.groupBox1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.ValueBox3);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.OperateBox2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Controls.Add(this.ValueBox2);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.ValueBox1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.OperateBox);
			this.groupBox1.Location = new System.Drawing.Point(-1, 43);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(388, 85);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "查询条件";
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(6, 53);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(14, 13);
			this.radioButton2.TabIndex = 9;
			this.radioButton2.TabStop = true;
			this.radioButton2.UseVisualStyleBackColor = true;
			this.ValueBox3.FormattingEnabled = true;
			this.ValueBox3.Location = new System.Drawing.Point(171, 51);
			this.ValueBox3.Name = "ValueBox3";
			this.ValueBox3.Size = new Size(81, 20);
			this.ValueBox3.Sorted = true;
			this.ValueBox3.TabIndex = 8;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(22, 55);
			this.label3.Name = "label3";
			this.label3.Size = new Size(65, 12);
			this.label3.TabIndex = 7;
			this.label3.Text = "沟截面宽高";
			this.OperateBox2.DropDownStyle = ComboBoxStyle.DropDownList;
			this.OperateBox2.FormattingEnabled = true;
			this.OperateBox2.Items.AddRange(new object[]
			{
				"等于",
				"不等于"
			});
			this.OperateBox2.Location = new System.Drawing.Point(89, 51);
			this.OperateBox2.Name = "OperateBox2";
			this.OperateBox2.Size = new Size(76, 20);
			this.OperateBox2.TabIndex = 6;
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(6, 24);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new Size(14, 13);
			this.radioButton1.TabIndex = 5;
			this.radioButton1.TabStop = true;
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.ValueBox2.FormattingEnabled = true;
			this.ValueBox2.Location = new System.Drawing.Point(301, 23);
			this.ValueBox2.Name = "ValueBox2";
			this.ValueBox2.Size = new Size(81, 20);
			this.ValueBox2.Sorted = true;
			this.ValueBox2.TabIndex = 4;
			this.ValueBox2.Visible = false;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(267, 26);
			this.label2.Name = "label2";
			this.label2.Size = new Size(17, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "到";
			this.label2.Visible = false;
			this.ValueBox1.FormattingEnabled = true;
			this.ValueBox1.Location = new System.Drawing.Point(171, 22);
			this.ValueBox1.Name = "ValueBox1";
			this.ValueBox1.Size = new Size(81, 20);
			this.ValueBox1.Sorted = true;
			this.ValueBox1.TabIndex = 2;
			this.ValueBox1.SelectedIndexChanged += new EventHandler(this.ValueBox1_SelectedIndexChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(31, 26);
			this.label1.Name = "label1";
			this.label1.Size = new Size(29, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "管径";
			this.OperateBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.OperateBox.FormattingEnabled = true;
			this.OperateBox.Items.AddRange(new object[]
			{
				"大于",
				"大于等于",
				"等于",
				"不等于",
				"小于",
				"小于等于",
				"介于"
			});
			this.OperateBox.Location = new System.Drawing.Point(89, 22);
			this.OperateBox.Name = "OperateBox";
			this.OperateBox.Size = new Size(76, 20);
			this.OperateBox.TabIndex = 0;
			this.OperateBox.SelectedIndexChanged += new EventHandler(this.OperateBox_SelectedIndexChanged);
			this.QueryBut.Location = new System.Drawing.Point(320, 2);
			this.QueryBut.Name = "QueryBut";
			this.QueryBut.Size = new Size(67, 23);
			this.QueryBut.TabIndex = 2;
			this.QueryBut.Text = "查询(&Q)";
			this.QueryBut.UseVisualStyleBackColor = true;
			this.QueryBut.Click += new EventHandler(this.QueryBut_Click);
			this.CloseBut.DialogResult = DialogResult.Cancel;
			this.CloseBut.Location = new System.Drawing.Point(320, 27);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(67, 20);
			this.CloseBut.TabIndex = 3;
			this.CloseBut.Text = "关闭(&C)";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(4, 17);
			this.lable.Name = "lable";
			this.lable.Size = new Size(41, 12);
			this.lable.TabIndex = 4;
			this.lable.Text = "管线层";
			this.LayerBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(53, 14);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 5;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.GeometrySet.AutoSize = true;
			this.GeometrySet.Location = new System.Drawing.Point(210, 18);
			this.GeometrySet.Name = "GeometrySet";
			this.GeometrySet.Size = new Size(72, 16);
			this.GeometrySet.TabIndex = 6;
			this.GeometrySet.Text = "空间范围";
			this.GeometrySet.UseVisualStyleBackColor = true;
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.MenuItemSelectType,
				this.MenuItemDataSelectType
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(123, 48);
			this.MenuItemSelectType.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ByEnvelope,
				this.ByPolygon,
				this.ByCircle
			});
			this.MenuItemSelectType.Name = "MenuItemSelectType";
			this.MenuItemSelectType.Size = new Size(122, 22);
			this.MenuItemSelectType.Text = "选择方式";
			this.ByEnvelope.Checked = true;
			this.ByEnvelope.CheckOnClick = true;
			this.ByEnvelope.CheckState = CheckState.Checked;
			this.ByEnvelope.Name = "ByEnvelope";
			this.ByEnvelope.Size = new Size(134, 22);
			this.ByEnvelope.Text = "矩形选择";
			this.ByEnvelope.Click += new EventHandler(this.ByEnvelope_Click);
			this.ByPolygon.CheckOnClick = true;
			this.ByPolygon.Name = "ByPolygon";
			this.ByPolygon.Size = new Size(134, 22);
			this.ByPolygon.Text = "多边形选择";
			this.ByPolygon.Click += new EventHandler(this.ByPolygon_Click);
			this.ByCircle.CheckOnClick = true;
			this.ByCircle.Name = "ByCircle";
			this.ByCircle.Size = new Size(134, 22);
			this.ByCircle.Text = "圆形选择";
			this.ByCircle.Click += new EventHandler(this.ByCircle_Click);
			this.MenuItemDataSelectType.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.CrossesSelect,
				this.WithinSelect
			});
			this.MenuItemDataSelectType.Name = "MenuItemDataSelectType";
			this.MenuItemDataSelectType.Size = new Size(122, 22);
			this.MenuItemDataSelectType.Text = "数据选择";
			this.CrossesSelect.Checked = true;
			this.CrossesSelect.CheckOnClick = true;
			this.CrossesSelect.CheckState = CheckState.Checked;
			this.CrossesSelect.Name = "CrossesSelect";
			this.CrossesSelect.Size = new Size(98, 22);
			this.CrossesSelect.Text = "相交";
			this.CrossesSelect.Click += new EventHandler(this.CrossesSelect_Click);
			this.WithinSelect.Name = "WithinSelect";
			this.WithinSelect.Size = new Size(98, 22);
			this.WithinSelect.Text = "内部";
			this.WithinSelect.Click += new EventHandler(this.WithinSelect_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(388, 130);
			base.Controls.Add(this.GeometrySet);
			base.Controls.Add(this.LayerBox);
			base.Controls.Add(this.lable);
			base.Controls.Add(this.CloseBut);
			base.Controls.Add(this.QueryBut);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "SimpleQueryByDiaUI";
			base.ShowInTaskbar = false;
			this.Text = "管径查询";
			base.Load += new EventHandler(this.SimpleQueryByDiaUI_Load);
			base.VisibleChanged += new EventHandler(this.SimpleQueryByDiaUI_VisibleChanged);
			base.FormClosed += new FormClosedEventHandler(this.SimpleQueryByDiaUI_FormClosed);
			base.HelpRequested += new HelpEventHandler(this.SimpleQueryByDiaUI_HelpRequested);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
