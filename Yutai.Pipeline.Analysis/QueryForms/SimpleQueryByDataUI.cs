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
	public class SimpleQueryByDataUI : Form
	{
		private class LayerboxItem
		{
			public IFeatureLayer m_pPipeLayer;

			public override string ToString()
			{
				return this.m_pPipeLayer.Name;
			}
		}

		private DateTimePicker dateTimePicker1;

		private DateTimePicker dateTimePicker2;

		public IGeometry m_ipGeo;

		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipeConfig pPipeCfg;

		private IFeatureLayer SelectLayer;

		private IFields myfields;

		private QueryResult resultDlg;

		public object mainform;

		private IContainer components = null;

		private ComboBox LayerBox;

		private Label lable;

		private RadioButton radioButton1;

		private RadioButton radioButton2;

		private Label label1;

		private ComboBox OperateBox;

		private Label label2;

		private GroupBox groupBox1;

		private Button QueryBut;

		private Button CloseBut;

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

		public SimpleQueryByDataUI()
		{
			this.InitializeComponent();
		}

		private void SimpleQueryByDataUI_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		public void AutoFlash()
		{
			this.OperateBox.SelectedIndex = 0;
			this.FillLayerBox();
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
				if (this.radioButton1.Checked)
				{
					if (this.pPipeCfg.IsPipePoint(aliasName))
					{
						SimpleQueryByDataUI.LayerboxItem layerboxItem = new SimpleQueryByDataUI.LayerboxItem();
						layerboxItem.m_pPipeLayer = iFLayer;
						this.LayerBox.Items.Add(layerboxItem);
					}
				}
				else if (this.pPipeCfg.IsPipeLine(aliasName))
				{
					SimpleQueryByDataUI.LayerboxItem layerboxItem2 = new SimpleQueryByDataUI.LayerboxItem();
					layerboxItem2.m_pPipeLayer = iFLayer;
					this.LayerBox.Items.Add(layerboxItem2);
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

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			this.FillLayerBox();
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			this.FillLayerBox();
		}

		private void OperateBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.OperateBox.SelectedItem.ToString() == "介于")
			{
				this.label2.Visible = true;
				this.dateTimePicker2.Visible = true;
			}
			else
			{
				this.label2.Visible = false;
				this.dateTimePicker2.Visible = false;
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

		private void QueryBut_Click(object sender, EventArgs e)
		{
			string text = "";
			int selectedIndex = this.LayerBox.SelectedIndex;
			if (selectedIndex >= 0)
			{
				this.SelectLayer = null;
				if (this.MapControl != null)
				{
					this.SelectLayer = ((SimpleQueryByDataUI.LayerboxItem)this.LayerBox.SelectedItem).m_pPipeLayer;
					if (this.SelectLayer != null)
					{
						this.myfields = this.SelectLayer.FeatureClass.Fields;
						string text2;
						if (this.radioButton1.Checked)
						{
							text2 = this.pPipeCfg.GetPointTableFieldName("建设年代");
						}
						else
						{
							text2 = this.pPipeCfg.GetLineTableFieldName("建设年代");
						}
						int num = this.myfields.FindField(text2);
						if (num < 0)
						{
							MessageBox.Show("图层中日期字段没有找到！请检查配置文件！");
						}
						else
						{
							IField field = this.myfields.get_Field(num);
							if (field.Type == (esriFieldType) 4)
							{
								switch (this.OperateBox.SelectedIndex)
								{
								case 0:
									text = text2;
									text += "<= '";
									text += this.dateTimePicker1.Value.ToShortDateString();
									text += "'";
									break;
								case 1:
									text = text2;
									text += ">= '";
									text += this.dateTimePicker1.Value.ToShortDateString();
									text += "'";
									break;
								case 2:
									text = text2;
									text += "= '";
									text += this.dateTimePicker1.Value.ToShortDateString();
									text += "'";
									break;
								case 3:
									text = text2;
									text += ">= '";
									text += this.dateTimePicker1.Value.ToShortDateString();
									text += "'and ";
									text += text2;
									text += "<= '";
									text += this.dateTimePicker2.Value.ToShortDateString();
									text += "'";
									break;
								}
							}
							else if (field.Type == (esriFieldType) 5)
							{
								if (this.SelectLayer.DataSourceType == "Personal Geodatabase Feature Class")
								{
									switch (this.OperateBox.SelectedIndex)
									{
									case 0:
										text = text2;
										text += "<= #";
										text += this.dateTimePicker1.Value.ToShortDateString();
										text += "#";
										break;
									case 1:
										text = text2;
										text += ">= #";
										text += this.dateTimePicker1.Value.ToShortDateString();
										text += "#";
										break;
									case 2:
										text = text2;
										text += "= #";
										text += this.dateTimePicker1.Value.ToShortDateString();
										text += "#";
										break;
									case 3:
										text = text2;
										text += ">= #";
										text += this.dateTimePicker1.Value.ToShortDateString();
										text += "# and ";
										text += text2;
										text += "<= #";
										text += this.dateTimePicker2.Value.ToShortDateString();
										text += "#";
										break;
									}
								}
								if (this.SelectLayer.DataSourceType == "SDE Feature Class")
								{
									switch (this.OperateBox.SelectedIndex)
									{
									case 0:
										text = text2;
										text += "<= TO_DATE('";
										text += this.dateTimePicker1.Value.ToShortDateString();
										text += "','YYYY-MM-DD')";
										break;
									case 1:
										text = text2;
										text += ">= TO_DATE('";
										text += this.dateTimePicker1.Value.ToShortDateString();
										text += "','YYYY-MM-DD')";
										break;
									case 2:
										text = text2;
										text += "= TO_DATE('";
										text += this.dateTimePicker1.Value.ToShortDateString();
										text += "','YYYY-MM-DD')";
										break;
									case 3:
										text = text2;
										text += ">= TO_DATE('";
										text += this.dateTimePicker1.Value.ToShortDateString();
										text += "','YYYY-MM-DD') and ";
										text += text2;
										text += "<= TO_DATE('";
										text += this.dateTimePicker2.Value.ToShortDateString();
										text += "','YYYY-MM-DD')";
										break;
									}
								}
							}
							IFeatureClass featureClass = this.SelectLayer.FeatureClass;
							ISpatialFilter spatialFilter = new SpatialFilter();
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
					}
				}
			}
		}

		private void LayerBox_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void SimpleQueryByDataUI_VisibleChanged(object sender, EventArgs e)
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

		private void SimpleQueryByDataUI_HelpRequested(object sender, HelpEventArgs hlpevent)
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
			this.dateTimePicker1 = new DateTimePicker();
			this.LayerBox = new ComboBox();
			this.lable = new Label();
			this.radioButton1 = new RadioButton();
			this.radioButton2 = new RadioButton();
			this.label1 = new Label();
			this.OperateBox = new ComboBox();
			this.label2 = new Label();
			this.groupBox1 = new GroupBox();
			this.dateTimePicker2 = new DateTimePicker();
			this.QueryBut = new Button();
			this.CloseBut = new Button();
			this.GeometrySet = new CheckBox();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.dateTimePicker1.Location = new System.Drawing.Point(146, 15);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.ShowUpDown = true;
			this.dateTimePicker1.Size = new Size(112, 21);
			this.dateTimePicker1.TabIndex = 0;
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(32, 29);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 13;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(1, 33);
			this.lable.Name = "lable";
			this.lable.Size = new Size(29, 12);
			this.lable.TabIndex = 12;
			this.lable.Text = "图层";
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(12, 6);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new Size(59, 16);
			this.radioButton1.TabIndex = 14;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "管点层";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(92, 6);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(59, 16);
			this.radioButton2.TabIndex = 15;
			this.radioButton2.Text = "管线层";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(1, 18);
			this.label1.Name = "label1";
			this.label1.Size = new Size(29, 12);
			this.label1.TabIndex = 16;
			this.label1.Text = "日期";
			this.OperateBox.FormattingEnabled = true;
			this.OperateBox.Items.AddRange(new object[]
			{
				"之前",
				"之后",
				"位于",
				"介于"
			});
			this.OperateBox.Location = new System.Drawing.Point(33, 15);
			this.OperateBox.Name = "OperateBox";
			this.OperateBox.Size = new Size(98, 20);
			this.OperateBox.TabIndex = 17;
			this.OperateBox.SelectedIndexChanged += new EventHandler(this.OperateBox_SelectedIndexChanged);
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(114, 54);
			this.label2.Name = "label2";
			this.label2.Size = new Size(17, 12);
			this.label2.TabIndex = 18;
			this.label2.Text = "至";
			this.label2.Visible = false;
			this.groupBox1.Controls.Add(this.dateTimePicker2);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.OperateBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.dateTimePicker1);
			this.groupBox1.Location = new System.Drawing.Point(3, 58);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(267, 85);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "条件设定";
			this.dateTimePicker2.Location = new System.Drawing.Point(149, 49);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.ShowUpDown = true;
			this.dateTimePicker2.Size = new Size(108, 21);
			this.dateTimePicker2.TabIndex = 19;
			this.dateTimePicker2.Visible = false;
			this.QueryBut.Location = new System.Drawing.Point(260, 6);
			this.QueryBut.Name = "QueryBut";
			this.QueryBut.Size = new Size(75, 23);
			this.QueryBut.TabIndex = 20;
			this.QueryBut.Text = "查询(&Q)";
			this.QueryBut.UseVisualStyleBackColor = true;
			this.QueryBut.Click += new EventHandler(this.QueryBut_Click);
			this.CloseBut.DialogResult = DialogResult.Cancel;
			this.CloseBut.Location = new System.Drawing.Point(260, 35);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(75, 23);
			this.CloseBut.TabIndex = 21;
			this.CloseBut.Text = "关闭(&C)";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			this.GeometrySet.AutoSize = true;
			this.GeometrySet.Location = new System.Drawing.Point(172, 6);
			this.GeometrySet.Name = "GeometrySet";
			this.GeometrySet.Size = new Size(72, 16);
			this.GeometrySet.TabIndex = 22;
			this.GeometrySet.Text = "空间范围";
			this.GeometrySet.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(337, 149);
			base.Controls.Add(this.GeometrySet);
			base.Controls.Add(this.CloseBut);
			base.Controls.Add(this.QueryBut);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.radioButton2);
			base.Controls.Add(this.radioButton1);
			base.Controls.Add(this.LayerBox);
			base.Controls.Add(this.lable);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "SimpleQueryByDataUI";
			base.ShowInTaskbar = false;
			this.Text = "快速查询-按日期";
			base.Load += new EventHandler(this.SimpleQueryByDataUI_Load);
			base.VisibleChanged += new EventHandler(this.SimpleQueryByDataUI_VisibleChanged);
			base.HelpRequested += new HelpEventHandler(this.SimpleQueryByDataUI_HelpRequested);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
