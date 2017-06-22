using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.PipeConfig;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class SimpleQueryByJdxzUI : Form
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

		private string strDX;

		private IField myfieldDX;

		private QueryResult resultDlg;

		public object mainform;

		private List<string> DXArray = new List<string>();

		private IContainer components = null;

		private ComboBox LayerBox;

		private Label lable;

		private Button CloseBut;

		private Button QueryBut;

		private GroupBox groupBox2;

		private Button NoneBut;

		private Button AllBut;

		private CheckedListBox ValueBox;

		private GroupBox groupBox1;

		private TextBox SqlBox;

		private Button RevBut;

		private CheckBox GeometrySet;

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

		public SimpleQueryByJdxzUI()
		{
			this.InitializeComponent();
		}

		private void SimpleQueryByJdxzUI_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		public void AutoFlash()
		{
			this.FillLayerBox();
			if (!this.ValidateField())
			{
				MessageBox.Show("配置文件有误，请检查！");
			}
			else
			{
				this.FillValueBox();
			}
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
				if (this.pPipeCfg.IsPipePoint(aliasName))
				{
					SimpleQueryByJdxzUI.LayerboxItem layerboxItem = new SimpleQueryByJdxzUI.LayerboxItem();
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
				ILayer ipLay = m_context.FocusMap.get_Layer(i);
				this.AddLayer(ipLay);
			}
			if (this.LayerBox.Items.Count > 0)
			{
				this.LayerBox.SelectedIndex = 0;
			}
		}

		private bool ValidateField()
		{
			int selectedIndex = this.LayerBox.SelectedIndex;
			bool result;
			if (selectedIndex < 0)
			{
				result = false;
			}
			else
			{
				this.SelectLayer = null;
				if (this.MapControl == null)
				{
					result = false;
				}
				else
				{
					this.SelectLayer = ((SimpleQueryByJdxzUI.LayerboxItem)this.LayerBox.SelectedItem).m_pPipeLayer;
					if (this.SelectLayer == null)
					{
						result = false;
					}
					else
					{
						this.myfields = this.SelectLayer.FeatureClass.Fields;
						this.strDX = this.pPipeCfg.GetPointTableFieldName("点性");
						if (this.myfields.FindField(this.strDX) < 0)
						{
							this.QueryBut.Enabled = false;
							result = false;
						}
						else
						{
							this.QueryBut.Enabled = true;
							result = true;
						}
					}
				}
			}
			return result;
		}

		private void FillValueBox()
		{
			if (this.myfields != null)
			{
				int num = this.myfields.FindField(this.strDX);
				if (num >= 0)
				{
					this.myfieldDX = this.myfields.get_Field(num);
					IFeatureClass featureClass = this.SelectLayer.FeatureClass;
					IQueryFilter queryFilter = new QueryFilter();
					IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
					IFeature feature = featureCursor.NextFeature();
					this.ValueBox.Items.Clear();
					while (feature != null)
					{
						object obj = feature.get_Value(num);
						if (obj is DBNull)
						{
							feature = featureCursor.NextFeature();
						}
						else
						{
							string text = obj.ToString();
							if (text.Length == 0)
							{
								feature = featureCursor.NextFeature();
							}
							else
							{
								if (!this.ValueBox.Items.Contains(text))
								{
									this.ValueBox.Items.Add(text);
								}
								feature = featureCursor.NextFeature();
							}
						}
					}
				}
			}
		}

		private void LayerBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.DXArray.Clear();
			this.SqlBox.Text = "";
			if (!this.ValidateField())
			{
				MessageBox.Show("配置文件有误，点性字段不匹配,请检查！");
			}
			else
			{
				this.FillValueBox();
			}
		}

		private void AllBut_Click(object sender, EventArgs e)
		{
			this.DXArray.Clear();
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				this.DXArray.Add(this.ValueBox.Items[i].ToString());
				this.ValueBox.SetItemChecked(i, true);
			}
			string text = "";
			foreach (string current in this.DXArray)
			{
				text += current;
				text += " ";
			}
			this.SqlBox.Text = text;
		}

		private void NoneBut_Click(object sender, EventArgs e)
		{
			this.DXArray.Clear();
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				this.ValueBox.SetItemChecked(i, false);
			}
			this.SqlBox.Text = "";
		}

		private void RevBut_Click(object sender, EventArgs e)
		{
			this.DXArray.Clear();
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				if (this.ValueBox.GetItemChecked(i))
				{
					this.ValueBox.SetItemChecked(i, false);
				}
				else
				{
					this.DXArray.Add(this.ValueBox.Items[i].ToString());
					this.ValueBox.SetItemChecked(i, true);
				}
			}
			string text = "";
			foreach (string current in this.DXArray)
			{
				text += current;
				text += " ";
			}
			this.SqlBox.Text = text;
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

		private void ValueBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = this.ValueBox.SelectedIndex;
			string text = "";
			string item = this.ValueBox.SelectedItem.ToString();
			if (this.ValueBox.GetItemChecked(selectedIndex))
			{
				if (!this.DXArray.Contains(item))
				{
					this.DXArray.Add(item);
				}
			}
			else if (this.DXArray.Contains(item))
			{
				this.DXArray.Remove(item);
			}
			foreach (string current in this.DXArray)
			{
				text += current;
				text += " ";
			}
			this.SqlBox.Text = text;
		}

		private void QueryBut_Click(object sender, EventArgs e)
		{
			IFeatureClass featureClass = this.SelectLayer.FeatureClass;
			ISpatialFilter spatialFilter = new SpatialFilter();
			string text = "";
			int count = this.DXArray.Count;
			int num = 1;
			foreach (string current in this.DXArray)
			{
				text += this.strDX;
				text += " = '";
				text += current;
				text += "'";
				if (num < count)
				{
					text += " OR ";
				}
				num++;
			}
			spatialFilter.WhereClause=text;
			if (this.GeometrySet.Checked)
			{
				if (this.m_ipGeo != null)
				{
					spatialFilter.Geometry=(this.m_ipGeo);
				}
				spatialFilter.SpatialRel=(esriSpatialRelEnum) (1);
			}
			IFeatureCursor pCursor = featureClass.Search(spatialFilter, false);
			this.m_iApp.SetResult(pCursor, (IFeatureSelection)this.SelectLayer);
		}

		private void SimpleQueryByJdxzUI_VisibleChanged(object sender, EventArgs e)
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
            axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
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

		private void SimpleQueryByJdxzUI_HelpRequested(object sender, HelpEventArgs hlpevent)
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
			this.LayerBox = new ComboBox();
			this.lable = new Label();
			this.CloseBut = new Button();
			this.QueryBut = new Button();
			this.groupBox2 = new GroupBox();
			this.RevBut = new Button();
			this.NoneBut = new Button();
			this.AllBut = new Button();
			this.ValueBox = new CheckedListBox();
			this.groupBox1 = new GroupBox();
			this.SqlBox = new TextBox();
			this.GeometrySet = new CheckBox();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.LayerBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(54, 12);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 11;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(5, 15);
			this.lable.Name = "lable";
			this.lable.Size = new Size(41, 12);
			this.lable.TabIndex = 10;
			this.lable.Text = "管点层";
			this.CloseBut.DialogResult = DialogResult.Cancel;
			this.CloseBut.Location = new System.Drawing.Point(321, 236);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(67, 25);
			this.CloseBut.TabIndex = 9;
			this.CloseBut.Text = "关闭(&C)";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			this.QueryBut.Location = new System.Drawing.Point(321, 25);
			this.QueryBut.Name = "QueryBut";
			this.QueryBut.Size = new Size(67, 25);
			this.QueryBut.TabIndex = 8;
			this.QueryBut.Text = "查询(&Q)";
			this.QueryBut.UseVisualStyleBackColor = true;
			this.QueryBut.Click += new EventHandler(this.QueryBut_Click);
			this.groupBox2.Controls.Add(this.RevBut);
			this.groupBox2.Controls.Add(this.NoneBut);
			this.groupBox2.Controls.Add(this.AllBut);
			this.groupBox2.Controls.Add(this.ValueBox);
			this.groupBox2.Location = new System.Drawing.Point(5, 96);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(298, 191);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "选择查询对象";
			this.RevBut.Location = new System.Drawing.Point(208, 140);
			this.RevBut.Name = "RevBut";
			this.RevBut.Size = new Size(76, 23);
			this.RevBut.TabIndex = 3;
			this.RevBut.Text = "反选(&I)";
			this.RevBut.UseVisualStyleBackColor = true;
			this.RevBut.Click += new EventHandler(this.RevBut_Click);
			this.NoneBut.Location = new System.Drawing.Point(208, 95);
			this.NoneBut.Name = "NoneBut";
			this.NoneBut.Size = new Size(76, 23);
			this.NoneBut.TabIndex = 2;
			this.NoneBut.Text = "全不选(&N)";
			this.NoneBut.UseVisualStyleBackColor = true;
			this.NoneBut.Click += new EventHandler(this.NoneBut_Click);
			this.AllBut.Location = new System.Drawing.Point(208, 50);
			this.AllBut.Name = "AllBut";
			this.AllBut.Size = new Size(76, 23);
			this.AllBut.TabIndex = 1;
			this.AllBut.Text = "全选(&A)";
			this.AllBut.UseVisualStyleBackColor = true;
			this.AllBut.Click += new EventHandler(this.AllBut_Click);
			this.ValueBox.CheckOnClick = true;
			this.ValueBox.FormattingEnabled = true;
			this.ValueBox.Items.AddRange(new object[]
			{
				"sdfsfsfs",
				"sdsdf",
				"sfdsdf"
			});
			this.ValueBox.Location = new System.Drawing.Point(10, 17);
			this.ValueBox.Name = "ValueBox";
			this.ValueBox.Size = new Size(166, 164);
			this.ValueBox.Sorted = true;
			this.ValueBox.TabIndex = 0;
			this.ValueBox.SelectedIndexChanged += new EventHandler(this.ValueBox_SelectedIndexChanged);
			this.groupBox1.Controls.Add(this.SqlBox);
			this.groupBox1.Location = new System.Drawing.Point(0, 43);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(303, 41);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "查询条件";
			this.SqlBox.Location = new System.Drawing.Point(10, 13);
			this.SqlBox.Name = "SqlBox";
			this.SqlBox.ReadOnly = true;
			this.SqlBox.Size = new Size(287, 21);
			this.SqlBox.TabIndex = 0;
			this.GeometrySet.AutoSize = true;
			this.GeometrySet.Location = new System.Drawing.Point(213, 14);
			this.GeometrySet.Name = "GeometrySet";
			this.GeometrySet.Size = new Size(72, 16);
			this.GeometrySet.TabIndex = 12;
			this.GeometrySet.Text = "空间范围";
			this.GeometrySet.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(395, 297);
			base.Controls.Add(this.GeometrySet);
			base.Controls.Add(this.LayerBox);
			base.Controls.Add(this.lable);
			base.Controls.Add(this.CloseBut);
			base.Controls.Add(this.QueryBut);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "SimpleQueryByJdxzUI";
			base.ShowInTaskbar = false;
			this.Text = "快速查询－按点性";
			base.Load += new EventHandler(this.SimpleQueryByJdxzUI_Load);
			base.VisibleChanged += new EventHandler(this.SimpleQueryByJdxzUI_VisibleChanged);
			base.HelpRequested += new HelpEventHandler(this.SimpleQueryByJdxzUI_HelpRequested);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
