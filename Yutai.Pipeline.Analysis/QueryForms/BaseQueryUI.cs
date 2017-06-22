using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Interfaces;
using QueryResult = QueryStatManage.QueryResult;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class BaseQueryUI : Form
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

		private IFields myfields;

		private int LayerType;

		private IFeatureLayer SelectLayer;

		public IGeometry SelectBound;

		public ushort DrawType;

		public ushort SelectType;

		private QueryResult resultDlg;

		private string strWinText;

		private bool bSave = true;

		public object mainform;

		private ContextMenuStrip contextMenuStrip1;

		private IContainer components = null;

		private ComboBox Layerbox;

		private Label label1;

		private Button QueryButton;

		private Label label2;

		private ComboBox FieldsBox;

		private ListBox ValueBox;

		private RadioButton Equalradio;

		private RadioButton Bigradio;

		private RadioButton BigeRaido;

		private RadioButton SmallRadio;

		private RadioButton SmalelRadio;

		private RadioButton LikeRadio;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private Button CloseButton;

		private CheckBox GeometrySet;

		private RadioButton NoEqualRadio;

		private ToolStripMenuItem 选择方式ToolStripMenuItem;

		private ToolStripMenuItem 数据选择ToolStripMenuItem;

		private ToolStripMenuItem ByEnvelope;

		private ToolStripMenuItem ByPolygon;

		private ToolStripMenuItem ByCircle;

		private ToolStripMenuItem CrossesSelect;

		private ToolStripMenuItem WithinSelect;

		private Button Clearbut;

		private TextBox ValueEdit;

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

		public string WindowText
		{
			set
			{
				this.Text = value;
			}
		}

		public BaseQueryUI()
		{
			this.InitializeComponent();
		}

		~BaseQueryUI()
		{
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
				if (this.Text == "管点查询")
				{
					if (this.pPipeCfg.IsPipePoint(aliasName))
					{
						BaseQueryUI.LayerboxItem layerboxItem = new BaseQueryUI.LayerboxItem();
						layerboxItem.m_pPipeLayer = iFLayer;
						this.Layerbox.Items.Add(layerboxItem);
					}
				}
				else if (this.Text == "管线查询" && this.pPipeCfg.IsPipeLine(aliasName))
				{
					BaseQueryUI.LayerboxItem layerboxItem2 = new BaseQueryUI.LayerboxItem();
					layerboxItem2.m_pPipeLayer = iFLayer;
					this.Layerbox.Items.Add(layerboxItem2);
				}
			}
		}

		private bool Fill()
		{
			this.SelectLayer = null;
			bool result;
			if (this.MapControl == null)
			{
				result = false;
			}
			else
			{
				this.SelectLayer = ((BaseQueryUI.LayerboxItem)this.Layerbox.SelectedItem).m_pPipeLayer;
				if (this.SelectLayer == null)
				{
					result = false;
				}
				else
				{
					this.myfields = this.SelectLayer.FeatureClass.Fields;
					this.FieldsBox.Items.Clear();
					for (int i = 0; i < this.myfields.FieldCount; i++)
					{
						IField field = this.myfields.get_Field(i);
						string name = field.Name;
						if (field.Type != (esriFieldType) 6 && field.Type != (esriFieldType) 7 && !(name.ToUpper() == "ENABLED") && !(name.ToUpper() == "SHAPE.LEN"))
						{
							this.FieldsBox.Items.Add(name);
						}
					}
					if (this.FieldsBox.Items.Count > 0)
					{
						this.FieldsBox.SelectedIndex = 0;
					}
					result = true;
				}
			}
			return result;
		}

		private void FieldValue()
		{
			if (this.myfields != null)
			{
				int num = this.myfields.FindField(this.FieldsBox.SelectedItem.ToString());
				IField field = this.myfields.get_Field(num);
				if (field.Type == (esriFieldType) 4)
				{
					this.BigeRaido.Enabled = false;
					this.Bigradio.Enabled = false;
					this.SmalelRadio.Enabled = false;
					this.SmallRadio.Enabled = false;
					this.LikeRadio.Enabled = true;
				}
				else
				{
					this.BigeRaido.Enabled = true;
					this.Bigradio.Enabled = true;
					this.SmalelRadio.Enabled = true;
					this.SmallRadio.Enabled = true;
					this.LikeRadio.Enabled = false;
				}
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
				IQueryFilter queryFilter = new QueryFilter();
				IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
				IFeature feature = featureCursor.NextFeature();
				this.ValueBox.Items.Clear();
				this.ValueEdit.Text = "";
				while (feature != null)
				{
					object obj = feature.get_Value(num);
					string text;
					if (obj is DBNull)
					{
						text = "NULL";
					}
					else if (field.Type == (esriFieldType) 5)
					{
						text = Convert.ToDateTime(obj).ToShortDateString();
					}
					else
					{
						text = feature.get_Value(num).ToString();
						if (text.Length == 0)
						{
							text = "空字段值";
						}
					}
					if (!this.ValueBox.Items.Contains(text))
					{
						this.ValueBox.Items.Add(text);
					}
					if (this.ValueBox.Items.Count > 100)
					{
						break;
					}
					feature = featureCursor.NextFeature();
				}
			}
		}

		private void BaseQueryUI_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		public void AutoFlash()
		{
			this.Layerbox.Items.Clear();
			int layerCount = m_context.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer ipLay = m_context.FocusMap.get_Layer(i);
				this.AddLayer(ipLay);
			}
			if (this.Layerbox.Items.Count > 0)
			{
				this.Layerbox.SelectedIndex = 0;
				if (this.Fill())
				{
					this.FieldValue();
				}
			}
		}

		private void Layerbox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.FieldsBox.Items.Clear();
			if (this.Fill())
			{
				this.FieldValue();
			}
		}

		private void FieldsBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.FieldValue();
		}

		private void QueryButton_Click(object sender, EventArgs e)
		{
			IFeatureClass featureClass = this.SelectLayer.FeatureClass;
			ISpatialFilter spatialFilter = new SpatialFilter();
			IFeatureCursor pCursor = null;
			string text = this.ValueEdit.Text;
			if (text != "")
			{
				if (this.myfields == null)
				{
					return;
				}
				int num = this.myfields.FindField(this.FieldsBox.SelectedItem.ToString());
				IField field = this.myfields.get_Field(num);
				string text2 = this.FieldsBox.SelectedItem.ToString();
				if (text == "NULL")
				{
					if (this.Equalradio.Checked)
					{
						text2 += " IS NULL";
					}
					if (this.NoEqualRadio.Checked)
					{
						text2 = "NOT(" + text2 + " IS NULL)";
					}
					if (this.LikeRadio.Checked)
					{
						text2 += " LIKE NULL";
					}
				}
				else
				{
					if (this.Equalradio.Checked)
					{
						text2 += "=";
					}
					if (this.NoEqualRadio.Checked)
					{
						text2 += "<>";
					}
					if (this.SmallRadio.Checked)
					{
						text2 += "<";
					}
					if (this.SmalelRadio.Checked)
					{
						text2 += "<=";
					}
					if (this.Bigradio.Checked)
					{
						text2 += ">";
					}
					if (this.BigeRaido.Checked)
					{
						text2 += ">=";
					}
					if (this.LikeRadio.Checked)
					{
						text2 += " like ";
					}
					if (text == "空字段值")
					{
						text = "";
					}
					if (field.Type == (esriFieldType) 4)
					{
						if (this.LikeRadio.Checked)
						{
							IDataset dataset = this.SelectLayer.FeatureClass as IDataset;
							if (dataset.Workspace.Type == (esriWorkspaceType) 2)
							{
								text2 += "'%";
								text2 += text;
								text2 += "%'";
							}
							else
							{
								text2 += "'*";
								text2 += text;
								text2 += "*'";
							}
						}
						else
						{
							text2 += "'";
							text2 += text;
							text2 += "'";
						}
					}
					else if (field.Type == (esriFieldType) 5)
					{
						if (this.SelectLayer.DataSourceType == "SDE Feature Class")
						{
							text2 += "TO_DATE('";
							text2 += text;
							text2 += "','YYYY-MM-DD')";
						}
						if (this.SelectLayer.DataSourceType == "Personal Geodatabase Feature Class")
						{
							text2 += "#";
							text2 += text;
							text2 += "#";
						}
					}
					else
					{
						text2 += text;
					}
				}
				spatialFilter.WhereClause=(text2);
			}
			else if (MessageBox.Show("末指定属性条件,是否查询?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			if (this.GeometrySet.Checked && this.m_ipGeo != null)
			{
				spatialFilter.Geometry=(this.m_ipGeo);
			}
			if (this.SelectType == 0)
			{
				spatialFilter.SpatialRel=(esriSpatialRelEnum) (1);
			}
			if (this.SelectType == 1)
			{
				spatialFilter.SpatialRel=(esriSpatialRelEnum) (7);
			}
			try
			{
				pCursor = featureClass.Search(spatialFilter, false);
			}
			catch (Exception)
			{
				MessageBox.Show("查询值有误,请检查!");
				return;
			}
			this.m_iApp.SetResult(pCursor, (IFeatureSelection)this.SelectLayer);
		}

		private void ByEnvelope_Click(object sender, EventArgs e)
		{
			this.ByPolygon.Checked = false;
			this.ByCircle.Checked = false;
			this.ByEnvelope.Checked = true;
			this.DrawType = 0;
			IActiveView activeView = m_context.ActiveView;
			this.MapControl.Refresh((esriViewDrawPhase) 32, Type.Missing, Type.Missing);
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

		private void Clearbut_Click(object sender, EventArgs e)
		{
			this.m_ipGeo = null;
			this.MapControl.Refresh((esriViewDrawPhase) 32, null, null);
		}

		private void CloseButton_Click(object sender, EventArgs e)
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

		private void BaseQueryUI_VisibleChanged(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
				axMapControl.OnAfterDraw+= AxMapControlOnOnAfterDraw;
			}
			else
			{

                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw-= AxMapControlOnOnAfterDraw;
            }
		}

	    private void AxMapControlOnOnAfterDraw(object display, int viewDrawPhase)
	    {
           
            if (viewDrawPhase == 32)
            {
                this.DrawSelGeometry();
            }
        }

	   

		protected override void OnClosed(EventArgs e)
		{
            IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
            axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
            base.OnClosed(e);
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

		private void ValueBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValueEdit.Text = this.ValueBox.SelectedItem.ToString();
		}

		private void BaseQueryUI_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "基本查询";
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
			this.Layerbox = new ComboBox();
			this.label1 = new Label();
			this.QueryButton = new Button();
			this.label2 = new Label();
			this.FieldsBox = new ComboBox();
			this.ValueBox = new ListBox();
			this.Equalradio = new RadioButton();
			this.Bigradio = new RadioButton();
			this.BigeRaido = new RadioButton();
			this.SmallRadio = new RadioButton();
			this.SmalelRadio = new RadioButton();
			this.LikeRadio = new RadioButton();
			this.groupBox1 = new GroupBox();
			this.NoEqualRadio = new RadioButton();
			this.groupBox2 = new GroupBox();
			this.ValueEdit = new TextBox();
			this.CloseButton = new Button();
			this.GeometrySet = new CheckBox();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.选择方式ToolStripMenuItem = new ToolStripMenuItem();
			this.ByEnvelope = new ToolStripMenuItem();
			this.ByPolygon = new ToolStripMenuItem();
			this.ByCircle = new ToolStripMenuItem();
			this.数据选择ToolStripMenuItem = new ToolStripMenuItem();
			this.CrossesSelect = new ToolStripMenuItem();
			this.WithinSelect = new ToolStripMenuItem();
			this.Clearbut = new Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.Layerbox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.Layerbox.FormattingEnabled = true;
			this.Layerbox.Items.AddRange(new object[]
			{
				"油田电力点",
				"中压天然气点1"
			});
			this.Layerbox.Location = new System.Drawing.Point(58, 11);
			this.Layerbox.Name = "Layerbox";
			this.Layerbox.Size = new Size(162, 20);
			this.Layerbox.TabIndex = 0;
			this.Layerbox.SelectedIndexChanged += new EventHandler(this.Layerbox_SelectedIndexChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 16);
			this.label1.Name = "label1";
			this.label1.Size = new Size(41, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "查询层";
			this.QueryButton.Location = new System.Drawing.Point(12, 263);
			this.QueryButton.Name = "QueryButton";
			this.QueryButton.Size = new Size(75, 23);
			this.QueryButton.TabIndex = 2;
			this.QueryButton.Text = "查询(&Q)";
			this.QueryButton.UseVisualStyleBackColor = true;
			this.QueryButton.Click += new EventHandler(this.QueryButton_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 47);
			this.label2.Name = "label2";
			this.label2.Size = new Size(53, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "查询字段";
			this.FieldsBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.FieldsBox.FormattingEnabled = true;
			this.FieldsBox.Items.AddRange(new object[]
			{
				"查询字段"
			});
			this.FieldsBox.Location = new System.Drawing.Point(58, 38);
			this.FieldsBox.Name = "FieldsBox";
			this.FieldsBox.Size = new Size(162, 20);
			this.FieldsBox.TabIndex = 4;
			this.FieldsBox.SelectedIndexChanged += new EventHandler(this.FieldsBox_SelectedIndexChanged);
			this.ValueBox.FormattingEnabled = true;
			this.ValueBox.ItemHeight = 12;
			this.ValueBox.Location = new System.Drawing.Point(7, 37);
			this.ValueBox.Name = "ValueBox";
			this.ValueBox.Size = new Size(126, 112);
			this.ValueBox.Sorted = true;
			this.ValueBox.TabIndex = 5;
			this.ValueBox.SelectedIndexChanged += new EventHandler(this.ValueBox_SelectedIndexChanged);
			this.Equalradio.AutoSize = true;
			this.Equalradio.Checked = true;
			this.Equalradio.Location = new System.Drawing.Point(5, 19);
			this.Equalradio.Name = "Equalradio";
			this.Equalradio.Size = new Size(29, 16);
			this.Equalradio.TabIndex = 7;
			this.Equalradio.TabStop = true;
			this.Equalradio.Text = "=";
			this.Equalradio.UseVisualStyleBackColor = true;
			this.Bigradio.AutoSize = true;
			this.Bigradio.Location = new System.Drawing.Point(5, 55);
			this.Bigradio.Name = "Bigradio";
			this.Bigradio.Size = new Size(29, 16);
			this.Bigradio.TabIndex = 8;
			this.Bigradio.TabStop = true;
			this.Bigradio.Text = ">";
			this.Bigradio.UseVisualStyleBackColor = true;
			this.BigeRaido.AutoSize = true;
			this.BigeRaido.Location = new System.Drawing.Point(5, 73);
			this.BigeRaido.Name = "BigeRaido";
			this.BigeRaido.Size = new Size(35, 16);
			this.BigeRaido.TabIndex = 9;
			this.BigeRaido.TabStop = true;
			this.BigeRaido.Text = ">=";
			this.BigeRaido.UseVisualStyleBackColor = true;
			this.SmallRadio.AutoSize = true;
			this.SmallRadio.Location = new System.Drawing.Point(5, 91);
			this.SmallRadio.Name = "SmallRadio";
			this.SmallRadio.Size = new Size(29, 16);
			this.SmallRadio.TabIndex = 10;
			this.SmallRadio.TabStop = true;
			this.SmallRadio.Text = "<";
			this.SmallRadio.UseVisualStyleBackColor = true;
			this.SmalelRadio.AutoSize = true;
			this.SmalelRadio.Location = new System.Drawing.Point(5, 109);
			this.SmalelRadio.Name = "SmalelRadio";
			this.SmalelRadio.Size = new Size(35, 16);
			this.SmalelRadio.TabIndex = 11;
			this.SmalelRadio.TabStop = true;
			this.SmalelRadio.Text = ">=";
			this.SmalelRadio.UseVisualStyleBackColor = true;
			this.LikeRadio.AutoSize = true;
			this.LikeRadio.Location = new System.Drawing.Point(5, 127);
			this.LikeRadio.Name = "LikeRadio";
			this.LikeRadio.Size = new Size(47, 16);
			this.LikeRadio.TabIndex = 12;
			this.LikeRadio.TabStop = true;
			this.LikeRadio.Text = "相似";
			this.LikeRadio.UseVisualStyleBackColor = true;
			this.groupBox1.Controls.Add(this.NoEqualRadio);
			this.groupBox1.Controls.Add(this.LikeRadio);
			this.groupBox1.Controls.Add(this.SmalelRadio);
			this.groupBox1.Controls.Add(this.SmallRadio);
			this.groupBox1.Controls.Add(this.BigeRaido);
			this.groupBox1.Controls.Add(this.Bigradio);
			this.groupBox1.Controls.Add(this.Equalradio);
			this.groupBox1.Location = new System.Drawing.Point(1, 72);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(74, 154);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "关系符";
			this.NoEqualRadio.AutoSize = true;
			this.NoEqualRadio.Location = new System.Drawing.Point(5, 37);
			this.NoEqualRadio.Name = "NoEqualRadio";
			this.NoEqualRadio.Size = new Size(35, 16);
			this.NoEqualRadio.TabIndex = 13;
			this.NoEqualRadio.TabStop = true;
			this.NoEqualRadio.Text = "!=";
			this.NoEqualRadio.UseVisualStyleBackColor = true;
			this.groupBox2.Controls.Add(this.ValueEdit);
			this.groupBox2.Controls.Add(this.ValueBox);
			this.groupBox2.Location = new System.Drawing.Point(81, 72);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(139, 154);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "字段值";
			this.ValueEdit.Location = new System.Drawing.Point(6, 13);
			this.ValueEdit.Name = "ValueEdit";
			this.ValueEdit.Size = new Size(127, 21);
			this.ValueEdit.TabIndex = 6;
			this.CloseButton.DialogResult = DialogResult.Cancel;
			this.CloseButton.Location = new System.Drawing.Point(128, 263);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new Size(75, 23);
			this.CloseButton.TabIndex = 15;
			this.CloseButton.Text = "关闭(&C)";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new EventHandler(this.CloseButton_Click);
			this.GeometrySet.AutoSize = true;
			this.GeometrySet.ContextMenuStrip = this.contextMenuStrip1;
			this.GeometrySet.Location = new System.Drawing.Point(5, 236);
			this.GeometrySet.Name = "GeometrySet";
			this.GeometrySet.Size = new Size(72, 16);
			this.GeometrySet.TabIndex = 16;
			this.GeometrySet.Text = "空间范围";
			this.GeometrySet.UseVisualStyleBackColor = true;
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.选择方式ToolStripMenuItem,
				this.数据选择ToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(123, 48);
			this.选择方式ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ByEnvelope,
				this.ByPolygon,
				this.ByCircle
			});
			this.选择方式ToolStripMenuItem.Name = "选择方式ToolStripMenuItem";
			this.选择方式ToolStripMenuItem.Size = new Size(122, 22);
			this.选择方式ToolStripMenuItem.Text = "选择方式";
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
			this.数据选择ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.CrossesSelect,
				this.WithinSelect
			});
			this.数据选择ToolStripMenuItem.Name = "数据选择ToolStripMenuItem";
			this.数据选择ToolStripMenuItem.Size = new Size(122, 22);
			this.数据选择ToolStripMenuItem.Text = "数据选择";
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
			this.Clearbut.Location = new System.Drawing.Point(83, 230);
			this.Clearbut.Name = "Clearbut";
			this.Clearbut.Size = new Size(69, 24);
			this.Clearbut.TabIndex = 17;
			this.Clearbut.Text = "清理屏幕";
			this.Clearbut.UseVisualStyleBackColor = true;
			this.Clearbut.Visible = false;
			this.Clearbut.Click += new EventHandler(this.Clearbut_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(231, 290);
			base.Controls.Add(this.Clearbut);
			base.Controls.Add(this.GeometrySet);
			base.Controls.Add(this.CloseButton);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.FieldsBox);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.QueryButton);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.Layerbox);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "BaseQueryUI";
			base.ShowInTaskbar = false;
			this.Text = "BaseQueryUI";
			base.Load += new EventHandler(this.BaseQueryUI_Load);
			base.VisibleChanged += new EventHandler(this.BaseQueryUI_VisibleChanged);
			base.HelpRequested += new HelpEventHandler(this.BaseQueryUI_HelpRequested);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
