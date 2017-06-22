
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class RelateQueryUI : Form
	{
		private class LayerboxItem
		{
			public IFeatureLayer m_pPipeLayer;

			public override string ToString()
			{
				return this.m_pPipeLayer.Name;
			}
		}

		private IContainer components = null;

		private ComboBox cmbPipeLine;

		private ComboBox cmbPipePoint;

		private GroupBox groupBox1;

		private ListBox lstBoxPipeLineValues;

		private Label label3;

		private ComboBox cmbPipeLineFields;

		private GroupBox groupBox2;

		private ListBox lstBoxPipePointValues;

		private ComboBox cmbPipePointFields;

		private Label label4;

		private Button btnPipeLineQuery;

		private Button btnPipePointQuery;

		private Button btnClose;

		private CheckBox GeometrySet;

		public IGeometry m_ipGeo;

		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipeConfig pPipeCfg;

		private QueryResult resultDlg;

		private ArrayList m_alPipeLine = new ArrayList();

		private ArrayList m_alPipePoint = new ArrayList();

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
			this.cmbPipeLine = new ComboBox();
			this.cmbPipePoint = new ComboBox();
			this.groupBox1 = new GroupBox();
			this.lstBoxPipeLineValues = new ListBox();
			this.label3 = new Label();
			this.cmbPipeLineFields = new ComboBox();
			this.groupBox2 = new GroupBox();
			this.lstBoxPipePointValues = new ListBox();
			this.cmbPipePointFields = new ComboBox();
			this.label4 = new Label();
			this.btnPipeLineQuery = new Button();
			this.btnPipePointQuery = new Button();
			this.btnClose = new Button();
			this.GeometrySet = new CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.cmbPipeLine.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPipeLine.FormattingEnabled = true;
			this.cmbPipeLine.Location = new System.Drawing.Point(17, 27);
			this.cmbPipeLine.Name = "cmbPipeLine";
			this.cmbPipeLine.Size = new Size(121, 20);
			this.cmbPipeLine.TabIndex = 1;
			this.cmbPipeLine.SelectedIndexChanged += new EventHandler(this.cmbPipeLine_SelectedIndexChanged);
			this.cmbPipePoint.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPipePoint.FormattingEnabled = true;
			this.cmbPipePoint.Location = new System.Drawing.Point(20, 27);
			this.cmbPipePoint.Name = "cmbPipePoint";
			this.cmbPipePoint.Size = new Size(121, 20);
			this.cmbPipePoint.TabIndex = 3;
			this.cmbPipePoint.SelectedIndexChanged += new EventHandler(this.cmbPipePoint_SelectedIndexChanged);
			this.groupBox1.Controls.Add(this.lstBoxPipeLineValues);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cmbPipeLineFields);
			this.groupBox1.Controls.Add(this.cmbPipeLine);
			this.groupBox1.Location = new System.Drawing.Point(7, 10);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(165, 227);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "线层";
			this.lstBoxPipeLineValues.FormattingEnabled = true;
			this.lstBoxPipeLineValues.ItemHeight = 12;
			this.lstBoxPipeLineValues.Location = new System.Drawing.Point(17, 118);
			this.lstBoxPipeLineValues.Name = "lstBoxPipeLineValues";
			this.lstBoxPipeLineValues.Size = new Size(121, 88);
			this.lstBoxPipeLineValues.TabIndex = 4;
			this.lstBoxPipeLineValues.SelectedIndexChanged += new EventHandler(this.lstBoxPipeLineValues_SelectedIndexChanged);
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(17, 59);
			this.label3.Name = "label3";
			this.label3.Size = new Size(41, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "字段：";
			this.cmbPipeLineFields.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPipeLineFields.FormattingEnabled = true;
			this.cmbPipeLineFields.Location = new System.Drawing.Point(17, 79);
			this.cmbPipeLineFields.Name = "cmbPipeLineFields";
			this.cmbPipeLineFields.Size = new Size(121, 20);
			this.cmbPipeLineFields.TabIndex = 1;
			this.cmbPipeLineFields.SelectedIndexChanged += new EventHandler(this.cmbPipeLineFields_SelectedIndexChanged);
			this.groupBox2.Controls.Add(this.lstBoxPipePointValues);
			this.groupBox2.Controls.Add(this.cmbPipePointFields);
			this.groupBox2.Controls.Add(this.cmbPipePoint);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Location = new System.Drawing.Point(204, 10);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(167, 227);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "点层";
			this.lstBoxPipePointValues.FormattingEnabled = true;
			this.lstBoxPipePointValues.ItemHeight = 12;
			this.lstBoxPipePointValues.Location = new System.Drawing.Point(21, 118);
			this.lstBoxPipePointValues.Name = "lstBoxPipePointValues";
			this.lstBoxPipePointValues.Size = new Size(121, 88);
			this.lstBoxPipePointValues.TabIndex = 4;
			this.lstBoxPipePointValues.SelectedIndexChanged += new EventHandler(this.lstBoxPipePointValues_SelectedIndexChanged);
			this.cmbPipePointFields.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPipePointFields.FormattingEnabled = true;
			this.cmbPipePointFields.Location = new System.Drawing.Point(21, 79);
			this.cmbPipePointFields.Name = "cmbPipePointFields";
			this.cmbPipePointFields.Size = new Size(121, 20);
			this.cmbPipePointFields.TabIndex = 3;
			this.cmbPipePointFields.SelectedIndexChanged += new EventHandler(this.cmbPipePointFields_SelectedIndexChanged);
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(23, 59);
			this.label4.Name = "label4";
			this.label4.Size = new Size(41, 12);
			this.label4.TabIndex = 2;
			this.label4.Text = "字段：";
			this.btnPipeLineQuery.Location = new System.Drawing.Point(84, 256);
			this.btnPipeLineQuery.Name = "btnPipeLineQuery";
			this.btnPipeLineQuery.Size = new Size(93, 23);
			this.btnPipeLineQuery.TabIndex = 5;
			this.btnPipeLineQuery.Text = "查询管线(&L)";
			this.btnPipeLineQuery.UseVisualStyleBackColor = true;
			this.btnPipeLineQuery.Click += new EventHandler(this.btnPipeLineQuery_Click);
			this.btnPipePointQuery.Location = new System.Drawing.Point(181, 256);
			this.btnPipePointQuery.Name = "btnPipePointQuery";
			this.btnPipePointQuery.Size = new Size(93, 23);
			this.btnPipePointQuery.TabIndex = 6;
			this.btnPipePointQuery.Text = "查询管点(&P)";
			this.btnPipePointQuery.UseVisualStyleBackColor = true;
			this.btnPipePointQuery.Click += new EventHandler(this.btnPipePointQuery_Click);
			this.btnClose.DialogResult = DialogResult.OK;
			this.btnClose.Location = new System.Drawing.Point(278, 256);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new Size(93, 23);
			this.btnClose.TabIndex = 7;
			this.btnClose.Text = "关闭(&C)";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.GeometrySet.AutoSize = true;
			this.GeometrySet.Location = new System.Drawing.Point(12, 260);
			this.GeometrySet.Name = "GeometrySet";
			this.GeometrySet.Size = new Size(72, 16);
			this.GeometrySet.TabIndex = 23;
			this.GeometrySet.Text = "空间范围";
			this.GeometrySet.UseVisualStyleBackColor = true;
			this.GeometrySet.Click += new EventHandler(this.GeometrySet_Click_1);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(383, 295);
			base.Controls.Add(this.GeometrySet);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnPipePointQuery);
			base.Controls.Add(this.btnPipeLineQuery);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "RelateQueryUI";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "关联查询";
			base.Load += new EventHandler(this.RelateQueryUI_Load);
			base.VisibleChanged += new EventHandler(this.RelateQueryUI_VisibleChanged);
			base.HelpRequested += new HelpEventHandler(this.RelateQueryUI_HelpRequested);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public RelateQueryUI()
		{
			this.InitializeComponent();
		}

		public void AutoFlash()
		{
			this.FillLayers();
		}

		public void AddName(ILayer pLayer)
		{
			if (pLayer is IFeatureLayer)
			{
				IFeatureLayer featureLayer = pLayer as IFeatureLayer;
				if (this.pPipeCfg.IsPipeLine(featureLayer.FeatureClass.AliasName))
				{
					RelateQueryUI.LayerboxItem layerboxItem = new RelateQueryUI.LayerboxItem();
					layerboxItem.m_pPipeLayer = featureLayer;
					this.cmbPipeLine.Items.Add(layerboxItem);
				}
				if (this.pPipeCfg.IsPipePoint(featureLayer.FeatureClass.AliasName))
				{
					RelateQueryUI.LayerboxItem layerboxItem2 = new RelateQueryUI.LayerboxItem();
					layerboxItem2.m_pPipeLayer = featureLayer;
					this.cmbPipePoint.Items.Add(layerboxItem2);
				}
			}
		}

		private void FillLayers()
		{
			this.cmbPipeLine.Items.Clear();
			this.cmbPipePoint.Items.Clear();
			this.cmbPipeLineFields.Items.Clear();
			this.cmbPipePointFields.Items.Clear();
			CommonUtils.ThrougAllLayer(m_context.FocusMap, new CommonUtils.DealLayer(this.AddName));
			if (this.cmbPipeLine.Items.Count > 0)
			{
				this.cmbPipeLine.SelectedIndex = 0;
			}
			if (this.cmbPipePoint.Items.Count > 0)
			{
				this.cmbPipePoint.SelectedIndex = 0;
			}
		}

		private void RelateQueryUI_Load(object sender, EventArgs e)
		{
			this.FillLayers();
		}

		private void cmbPipeLine_SelectedIndexChanged(object sender, EventArgs e)
		{
			RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipeLine.SelectedItem as RelateQueryUI.LayerboxItem;
			IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
			this.FillLayerFieldsToCmb(pPipeLayer, this.cmbPipeLineFields);
			if (this.cmbPipeLine.Focused)
			{
				string text = this.cmbPipeLine.Text.Trim();
				if (text.Length >= 2)
				{
					int length = text.LastIndexOf("线");
					text = text.Substring(0, length);
					int count = this.cmbPipePoint.Items.Count;
					for (int i = 0; i < count; i++)
					{
						string text2 = this.cmbPipePoint.Items[i].ToString();
						int length2 = text2.LastIndexOf("点");
						text2 = text2.Substring(0, length2);
						if (text2.Equals(text))
						{
							this.cmbPipePoint.SelectedIndex = i;
						}
					}
				}
			}
		}

		private void cmbPipePoint_SelectedIndexChanged(object sender, EventArgs e)
		{
			RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipePoint.SelectedItem as RelateQueryUI.LayerboxItem;
			IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
			this.FillLayerFieldsToCmb(pPipeLayer, this.cmbPipePointFields);
			if (this.cmbPipePoint.Focused)
			{
				string text = this.cmbPipePoint.Text.Trim();
				if (text.Length >= 2)
				{
					int length = text.LastIndexOf("点");
					text = text.Substring(0, length);
					int count = this.cmbPipeLine.Items.Count;
					for (int i = 0; i < count; i++)
					{
						string text2 = this.cmbPipeLine.Items[i].ToString();
						int length2 = text2.LastIndexOf("线");
						text2 = text2.Substring(0, length2);
						if (text2.Equals(text))
						{
							this.cmbPipeLine.SelectedIndex = i;
						}
					}
				}
			}
		}

		private void FillLayerFieldsToCmb(IFeatureLayer pFeaLay, ComboBox cmbVal)
		{
			IFeatureClass featureClass = pFeaLay.FeatureClass;
			IFields fields = featureClass.Fields;
			int fieldCount = fields.FieldCount;
			Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$");
			cmbVal.Items.Clear();
			int num = fields.FindField(featureClass.ShapeFieldName);
			for (int i = 0; i < fieldCount; i++)
			{
				if (num != i)
				{
					string name = fields.get_Field(i).Name;
					if (regex.IsMatch(name))
					{
						cmbVal.Items.Add(name);
					}
				}
			}
			if (cmbVal.Items.Count > 0)
			{
				cmbVal.SelectedIndex = 0;
			}
		}

		private void FillFieldValuesToListBox(IFeatureLayer pFeaLay, string strFieldName, ListBox lbVal)
		{
			IFeatureClass featureClass = pFeaLay.FeatureClass;
			IQueryFilter queryFilter = new QueryFilter();
			IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
			IFeature feature = featureCursor.NextFeature();
			int num = featureClass.Fields.FindField(strFieldName);
			if (num != -1)
			{
				lbVal.Items.Clear();
				while (feature != null)
				{
					object obj = feature.get_Value(num).ToString();
					if (obj != null && !Convert.IsDBNull(obj))
					{
						string value = obj.ToString();
						if (!lbVal.Items.Contains(value))
						{
							lbVal.Items.Add(obj);
						}
						feature = featureCursor.NextFeature();
					}
				}
			}
		}

		private void cmbPipeLineFields_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cmbPipeLine.Items.Count >= 1)
			{
				RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipeLine.SelectedItem as RelateQueryUI.LayerboxItem;
				IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
				if (pPipeLayer != null)
				{
					this.FillFieldValuesToListBox(pPipeLayer, this.cmbPipeLineFields.Text.Trim(), this.lstBoxPipeLineValues);
				}
			}
		}

		private void cmbPipePointFields_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cmbPipePoint.Items.Count >= 1)
			{
				RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipePoint.SelectedItem as RelateQueryUI.LayerboxItem;
				IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
				if (pPipeLayer != null)
				{
					this.FillFieldValuesToListBox(pPipeLayer, this.cmbPipePointFields.Text.Trim(), this.lstBoxPipePointValues);
				}
			}
		}

		private void btnPipeLineQuery_Click(object sender, EventArgs e)
		{
			if (this.cmbPipeLineFields.Text == "")
			{
				MessageBox.Show("请选择管线层查询字段！");
			}
			else if (this.lstBoxPipeLineValues.Text == "")
			{
				MessageBox.Show("请指定管线层查询值！");
			}
			else if (this.lstBoxPipeLineValues.Text == "")
			{
				MessageBox.Show("请指定管点层查询值！");
			}
			else
			{
				Splash.Show();
				Splash.Status = "状态:关联查询中,请稍候...";
				this.Walk();
				RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipeLine.SelectedItem as RelateQueryUI.LayerboxItem;
				IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
				IFeatureSelection featureSelection = (IFeatureSelection)pPipeLayer;
				featureSelection.Clear();
				ISelectionSet selectionSet = featureSelection.SelectionSet;
				selectionSet.IDs.Reset();
				int count = this.m_alPipeLine.Count;
				for (int i = 0; i < count; i++)
				{
					IFeature feature = this.m_alPipeLine[i] as IFeature;
					selectionSet.Add(feature.OID);
				}
				IQueryFilter queryFilter = new QueryFilter();
				ICursor cursor;
				selectionSet.Search(queryFilter, false, out cursor);
				IFeatureCursor pFeatureCursor = cursor as IFeatureCursor;
				Splash.Close();
				if (this.resultDlg == null || this.resultDlg.IsDisposed)
				{
					this.resultDlg = new QueryResult();
					this.resultDlg.pFeatureCursor = pFeatureCursor;
					this.resultDlg.MapControl = this.MapControl;
					this.resultDlg.pFeatureSelection = featureSelection;
					this.resultDlg.Show(this);
				}
				else
				{
					this.resultDlg.pFeatureCursor = pFeatureCursor;
					this.resultDlg.pFeatureSelection = featureSelection;
					this.resultDlg.MapControl = this.MapControl;
					if (!this.resultDlg.Visible)
					{
						this.resultDlg.Show(this);
						if (this.resultDlg.WindowState == FormWindowState.Minimized)
						{
							this.resultDlg.WindowState = FormWindowState.Normal;
						}
					}
					this.resultDlg.UpdateGrid();
				}
			}
		}

		private void btnPipePointQuery_Click(object sender, EventArgs e)
		{
			if (this.cmbPipeLineFields.Text == "")
			{
				MessageBox.Show("请选择管点层查询字段！");
			}
			else if (this.lstBoxPipeLineValues.Text == "")
			{
				MessageBox.Show("请指定管线层查询值！");
			}
			else if (this.lstBoxPipeLineValues.Text == "")
			{
				MessageBox.Show("请指定管点层查询值！");
			}
			else
			{
				this.Walk();
				RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipePoint.SelectedItem as RelateQueryUI.LayerboxItem;
				IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
				IFeatureSelection featureSelection = (IFeatureSelection)pPipeLayer;
				featureSelection.Clear();
				ISelectionSet selectionSet = featureSelection.SelectionSet;
				selectionSet.IDs.Reset();
				int count = this.m_alPipePoint.Count;
				for (int i = 0; i < count; i++)
				{
					IFeature feature = this.m_alPipePoint[i] as IFeature;
					selectionSet.Add(feature.OID);
				}
				IQueryFilter queryFilter = new QueryFilter();
				ICursor cursor;
				selectionSet.Search(queryFilter, false, out cursor);
				IFeatureCursor pFeatureCursor = cursor as IFeatureCursor;
				if (this.resultDlg == null || this.resultDlg.IsDisposed)
				{
					this.resultDlg = new QueryResult();
					this.resultDlg.pFeatureCursor = pFeatureCursor;
					this.resultDlg.MapControl = this.MapControl;
					this.resultDlg.pFeatureSelection = featureSelection;
					this.resultDlg.Show();
				}
				else
				{
					this.resultDlg.pFeatureCursor = pFeatureCursor;
					this.resultDlg.MapControl = this.MapControl;
					this.resultDlg.pFeatureSelection = featureSelection;
					if (!this.resultDlg.Visible)
					{
						this.resultDlg.Show();
						if (this.resultDlg.WindowState == FormWindowState.Minimized)
						{
							this.resultDlg.WindowState = FormWindowState.Normal;
						}
					}
					this.resultDlg.UpdateGrid();
				}
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.GeometrySet.Checked = false;
			base.Close();
		}

		private void Walk()
		{
			if (this.cmbPipeLine.Items.Count >= 1)
			{
				RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipeLine.SelectedItem as RelateQueryUI.LayerboxItem;
				IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
				if (pPipeLayer != null)
				{
					IFeatureClass featureClass = pPipeLayer.FeatureClass;
					ISpatialFilter spatialFilter = new SpatialFilter();
					int num = featureClass.Fields.FindField(this.cmbPipeLineFields.Text.Trim());
					if (featureClass.Fields.get_Field(num).Type == (esriFieldType) 4)
					{
						spatialFilter.WhereClause=this.cmbPipeLineFields.Text.Trim() + " = '" + this.lstBoxPipeLineValues.Text.Trim() + "'";
					}
					else if (featureClass.Fields.get_Field(num).Type == (esriFieldType) 5)
					{
						if (pPipeLayer.DataSourceType == "Personal Geodatabase Feature Class")
						{
							spatialFilter.WhereClause=this.cmbPipeLineFields.Text.Trim() + " = #" + this.lstBoxPipeLineValues.Text.Trim() + "#";
						}
						else
						{
							spatialFilter.WhereClause=this.cmbPipeLineFields.Text.Trim() + "= TO_DATE('" + this.lstBoxPipeLineValues.Text.Trim() + "','YYYY-MM-DD')";
						}
					}
					else
					{
						spatialFilter.WhereClause=this.cmbPipeLineFields.Text.Trim() + " = " + this.lstBoxPipeLineValues.Text.Trim();
					}
					if (this.GeometrySet.Checked)
					{
						if (this.m_ipGeo != null)
						{
							spatialFilter.Geometry=(this.m_ipGeo);
						}
						spatialFilter.SpatialRel=(esriSpatialRelEnum) (1);
					}
					IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
					IFeature feature = featureCursor.NextFeature();
					int num2 = featureClass.Fields.FindField(this.cmbPipeLineFields.Text.Trim());
					if (num2 != -1)
					{
						this.m_alPipeLine.Clear();
						this.m_alPipePoint.Clear();
						while (feature != null)
						{
							if (this.JustifyPipeLine(feature))
							{
								this.m_alPipeLine.Add(feature);
							}
							feature = featureCursor.NextFeature();
						}
					}
				}
			}
		}

		private void QueryPipePoint()
		{
		}

		private bool JustifyPipeLine(IFeature pFeatureLine)
		{
			IEdgeFeature edgeFeature = null;
			if (pFeatureLine is IEdgeFeature)
			{
				edgeFeature = (pFeatureLine as IEdgeFeature);
			}
			IFeature feature = edgeFeature.FromJunctionFeature as IFeature;
			IFeature feature2 = edgeFeature.ToJunctionFeature as IFeature;
			IFields fields = feature.Fields;
			string text = this.cmbPipePointFields.Text.Trim();
			int num = fields.FindField(text);
			object obj = feature.get_Value(num);
			string a;
			if (obj == null || Convert.IsDBNull(obj))
			{
				a = "";
			}
			else
			{
				a = obj.ToString();
			}
			object obj2 = feature2.get_Value(num);
			string a2;
			if (obj2 == null || Convert.IsDBNull(obj2))
			{
				a2 = "";
			}
			else
			{
				a2 = obj2.ToString();
			}
			bool flag = a == this.lstBoxPipePointValues.Text.Trim();
			bool flag2 = a2 == this.lstBoxPipePointValues.Text.Trim();
			if (flag)
			{
				this.m_alPipePoint.Add(feature);
			}
			if (flag2)
			{
				this.m_alPipePoint.Add(feature2);
			}
			return flag || flag2;
		}

		private void lstBoxPipeLineValues_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void lstBoxPipePointValues_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void axMap_OnAfterDraw(object sender, IMapControlEvents2_OnAfterDrawEvent e)
		{
			int viewDrawPhase = e.viewDrawPhase;
			if (viewDrawPhase == 32)
			{
				this.DrawSelGeometry();
			}
		}

		private void SimpleQueryByDiaUI_FormClosed(object sender, FormClosedEventArgs e)
		{
		    IMapControlEvents2_Event axMapControl = m_context.MapControl as IMapControlEvents2_Event;
            axMapControl.OnAfterDraw-= AxMapControlOnOnAfterDraw;

            
			base.OnClosed(e);
		}

	    private void AxMapControlOnOnAfterDraw(object display, int viewDrawPhase)
	    {
            if (viewDrawPhase == 32)
            {
                this.DrawSelGeometry();
            }
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

		private void GeometrySet_Click_1(object sender, EventArgs e)
		{
			this.m_ipGeo = null;
			m_context.ActiveView.Refresh();
		}

		private void RelateQueryUI_VisibleChanged(object sender, EventArgs e)
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

		private void RelateQueryUI_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "关联查询";
			HelpNavigator command = HelpNavigator.KeywordIndex;
			Help.ShowHelp(this, url, command, parameter);
		}
	}
}
