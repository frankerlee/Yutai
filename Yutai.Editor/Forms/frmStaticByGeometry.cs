using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.Plugins.Editor.Forms
{
	///  <summary>
	/// 几何统计分析窗体
	///  </summary>
	public class frmStaticByGeometry : Form
	{
		private Label label1;

		private ComboBoxEdit cboLayers;

		private Label label3;

		private GroupBox groupBox1;

		private SimpleButton btnDelete;

		private SimpleButton btnSelectField;

		private ComboBoxEdit cboFields;

		private Label label4;

		private EditListView listView1;

		private LVColumnHeader columnHeader1;

		private LVColumnHeader columnHeader2;

		private ComboBoxEdit cboClassField;

		private SimpleButton btnCancel;

		private SimpleButton btnOK;

		private ListView listView2;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private IMap m_pFocusMap = null;

		private IPolygon m_pPolygon = null;

		public IMap FocusMap
		{
			set
			{
				this.m_pFocusMap = value;
			}
		}

		public IPolygon Polygon
		{
			set
			{
				this.m_pPolygon = value;
			}
		}

		public frmStaticByGeometry()
		{
			this.InitializeComponent();
		}

		private void AddGroupLayerToComboBox(ICompositeLayer pCompositeLayer)
		{
			IFeatureLayer featureLayer = null;
			for (int i = 0; i < pCompositeLayer.Count; i++)
			{
				ILayer layer = pCompositeLayer.Layer[i];
				if (layer is IGroupLayer)
				{
					this.AddGroupLayerToComboBox(layer as ICompositeLayer);
				}
				else if (layer is IFeatureLayer)
				{
					featureLayer = layer as IFeatureLayer;
					this.cboLayers.Properties.Items.Add(new ObjectWrapEx(featureLayer));
				}
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			for (int i = this.listView1.SelectedItems.Count - 1; i >= 0; i--)
			{
				this.listView1.Items.Remove(this.listView1.SelectedItems[i]);
			}
			this.btnDelete.Enabled = false;
			this.EnableUnEnableOKButton();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			string str;
			int i;
			this.listView2.Clear();
			IList arrayLists = new ArrayList();
			for (i = 0; i < this.listView1.Items.Count; i++)
			{
				str = string.Concat(this.listView1.Items[i].SubItems[0].Text, "|", this.listView1.Items[i].SubItems[1].Text);
				if (arrayLists.IndexOf(str) == -1)
				{
					arrayLists.Add(str);
				}
			}
			string[] strArrays = new string[arrayLists.Count];
			string[] strArrays1 = new string[arrayLists.Count];
			if (this.cboClassField.SelectedIndex > 0)
			{
				this.listView2.Columns.Add(this.cboClassField.Text, 120, HorizontalAlignment.Left);
			}
			for (i = 0; i < arrayLists.Count; i++)
			{
				str = arrayLists[i].ToString();
				string[] strArrays2 = str.Split(new char[] { '|' });
				strArrays[i] = strArrays2[0];
				strArrays1[i] = strArrays2[1];
				this.listView2.Columns.Add(string.Concat(strArrays2[1], "_", strArrays2[0]), 120, HorizontalAlignment.Left);
			}
			object obj = (this.cboLayers.SelectedItem as ObjectWrap).Object;
			ITable featureClass = null;
			if (obj is IFeatureLayer)
			{
				featureClass = (obj as IFeatureLayer).FeatureClass as ITable;
			}
			else if (obj is IGxDataset)
			{
				featureClass = (obj as IGxDataset).Dataset as ITable;
			}
			if (featureClass != null)
			{
				IList lists = null;
				lists = (this.cboClassField.SelectedIndex != 0 ? this.FeatureClassOverlapStatistics(featureClass, this.cboClassField.Text, this.m_pPolygon, strArrays, strArrays1) : this.FeatureClassOverlapStatistics(featureClass, this.m_pPolygon, strArrays, strArrays1));
				for (i = 0; i < lists.Count; i++)
				{
					ListViewItem listViewItem = new ListViewItem((string[])lists[i]);
					this.listView2.Items.Add(listViewItem);
				}
			}
			else
			{
				this.btnOK.Enabled = true;
			}
		}

		private void btnSelectField_Click(object sender, EventArgs e)
		{
			string[] text = new string[] { this.cboFields.Text, "SUM" };
			ListViewItem listViewItem = new ListViewItem(text);
			this.listView1.Items.Add(listViewItem);
			this.EnableUnEnableOKButton();
		}

		private void cboClassField_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cboFields.SelectedIndex != -1)
			{
				this.btnSelectField.Enabled = true;
			}
			else
			{
				this.btnSelectField.Enabled = false;
			}
		}

        private void cboLayers_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.cboClassField.Properties.Items.Clear();
            this.cboClassField.Properties.Items.Add("<无>");
            this.EnableUnEnableOKButton();
            if (this.cboLayers.SelectedIndex != -1)
            {
                object @object = (this.cboLayers.SelectedItem as ObjectWrap).Object;
                IFields fields;
                if (@object is IFeatureLayer)
                {
                    fields = (@object as IFeatureLayer).FeatureClass.Fields;
                }
                else
                {
                    if (!(@object is IGxDataset))
                    {
                        return;
                    }
                    fields = ((@object as IGxDataset).Dataset as ITable).Fields;
                }
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    esriFieldType type = field.Type;
                    if (type == esriFieldType.esriFieldTypeInteger || type == esriFieldType.esriFieldTypeSmallInteger || type == esriFieldType.esriFieldTypeString)
                    {
                        this.cboClassField.Properties.Items.Add(field.Name);
                    }
                    if (type == esriFieldType.esriFieldTypeInteger || type == esriFieldType.esriFieldTypeSmallInteger || type == esriFieldType.esriFieldTypeDouble || type == esriFieldType.esriFieldTypeSingle)
                    {
                        this.cboFields.Properties.Items.Add(field.Name);
                    }
                }
                if (this.cboClassField.Properties.Items.Count > 0)
                {
                    this.cboClassField.SelectedIndex = 0;
                }
                if (this.cboFields.Properties.Items.Count > 0)
                {
                    this.cboFields.SelectedIndex = 0;
                }
            }
        }


        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private void EnableUnEnableOKButton()
		{
			if (this.listView1.Items.Count <= 0)
			{
				this.btnOK.Enabled = false;
			}
			else
			{
				this.btnOK.Enabled = true;
			}
		}

		/// <summary>
		/// 矢量数据分区分类统计
		/// </summary>
		/// <param name="pSFeatClass">源要素类，必须为面要素类</param>
		/// <param name="zoneField">分区字段，整形或字符字段，该字段需要添加到统计结果表字段中</param>
		/// <param name="classField">分类统计字段，整形或字符字段</param>
		/// <param name="classFieldType">分类统计字段类型，0 -整形 1- 字符字段</param>/// 
		/// <param name="FieldNames">统计字段列表，为数字字段</param>
		/// <param name="StatisticsTypes">
		/// 统计方法列表，和统计字段列表相对应
		/// 可用的统计方法字串如下：MIN, MAX, SUM, STD 最小值、最大值、总和、标准差
		/// </param>
		public IList FeatureClassOverlapStatistics(ITable pTable, string classField, IPolygon pPolygon, string[] FieldNames, string[] StatisticsTypes)
		{
			IList lists;
			IList arrayLists = new ArrayList();
			try
			{
				if (classField.Length <= 0)
				{
					lists = this.FeatureClassOverlapStatistics(pTable, pPolygon, FieldNames, StatisticsTypes);
					return lists;
				}
				else
				{
					IList arrayLists1 = new ArrayList();
					esriFieldType type = pTable.Fields.Field[pTable.FindField(classField)].Type;
					this.GetUniqueValues(pTable, classField, arrayLists1);
					IDataStatistics dataStatisticsClass = null;
					ISpatialFilter spatialFilterClass = new SpatialFilter();
					string str = "";
					spatialFilterClass.Geometry = pPolygon;
					spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
					for (int i = 0; i < arrayLists1.Count; i++)
					{
						str = (type != esriFieldType.esriFieldTypeString ? string.Concat(classField, " = ", arrayLists1[i].ToString()) : string.Concat(classField, " = '", arrayLists1[i].ToString(), "'"));
						spatialFilterClass.WhereClause = str;
						ICursor cursor = null;
						string[] strArrays = new string[(int)FieldNames.Length + 1];
						for (int j = 0; j < (int)FieldNames.Length; j++)
						{
							strArrays[j + 1] = "0";
						}
						strArrays[0] = arrayLists1[i].ToString();
						for (int k = 0; k < (int)FieldNames.Length; k++)
						{
							cursor = pTable.Search(spatialFilterClass, true);
							dataStatisticsClass = new DataStatistics()
							{
								Field = FieldNames[k],
								Cursor = cursor
							};
							IStatisticsResults statistics = dataStatisticsClass.Statistics;
							if (statistics.Count > 0)
							{
								double sum = 0;
								string statisticsTypes = StatisticsTypes[k];
								if (statisticsTypes != null)
								{
									if (statisticsTypes == "SUM")
									{
										sum = statistics.Sum;
									}
									else if (statisticsTypes == "MAX")
									{
										sum = statistics.Maximum;
									}
									else if (statisticsTypes == "MIN")
									{
										sum = statistics.Minimum;
									}
									else if (statisticsTypes == "STD")
									{
										sum = statistics.StandardDeviation;
									}
									else if (statisticsTypes == "MEAN")
									{
										sum = statistics.Mean;
									}
								}
								strArrays[k + 1] = sum.ToString();
							}
							statistics = null;
							Marshal.ReleaseComObject(cursor);
							cursor = null;
							Marshal.ReleaseComObject(dataStatisticsClass);
							dataStatisticsClass = null;
							GC.Collect();
						}
						arrayLists.Add(strArrays);
					}
					lists = arrayLists;
					return lists;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.ToString());
			}
			lists = null;
			return lists;
		}

		/// <summary>
		/// 矢量数据叠加分类统计
		/// </summary>
		/// <param name="pSFeatClass">源要素类，必须为面要素类</param>
		/// <param name="zoneField">分区字段，整形或字符字段，该字段需要添加到统计结果表字段中</param>
		/// <param name="pTFeatClass">目标字段</param>
		/// <param name="FieldNames">统计字段列表，为数字字段</param>
		/// <param name="StatisticsTypes">
		/// 统计方法列表，和统计字段列表相对应
		/// 可用的统计方法字串如下：MIN, MAX, SUM, STD MEAN 最小值、最大值、总和、标准差, 平均值
		/// </param>
		public IList FeatureClassOverlapStatistics(ITable pSTable, IPolygon pPolygon, string[] FieldNames, string[] StatisticsTypes)
		{
			IList arrayLists = new ArrayList();
			string[] str = new string[(int)FieldNames.Length];
			for (int i = 0; i < (int)FieldNames.Length; i++)
			{
				str[i] = "0";
			}
			ICursor cursor = null;
			IDataStatistics dataStatisticsClass = null;
			ISpatialFilter spatialFilterClass = new SpatialFilter()
			{
				Geometry = pPolygon,
				SpatialRel = esriSpatialRelEnum.esriSpatialRelContains
			};
			for (int j = 0; j < (int)FieldNames.Length; j++)
			{
				if (dataStatisticsClass != null)
				{
					Marshal.ReleaseComObject(dataStatisticsClass);
					dataStatisticsClass = null;
				}
				dataStatisticsClass = new DataStatistics()
				{
					Field = FieldNames[j]
				};
				cursor = pSTable.Search(spatialFilterClass, true);
				dataStatisticsClass.Cursor = cursor;
				IStatisticsResults statistics = dataStatisticsClass.Statistics;
				if (statistics.Count > 0)
				{
					double sum = 0;
					string statisticsTypes = StatisticsTypes[j];
					if (statisticsTypes != null)
					{
						if (statisticsTypes == "SUM")
						{
							sum = statistics.Sum;
						}
						else if (statisticsTypes == "MAX")
						{
							sum = statistics.Maximum;
						}
						else if (statisticsTypes == "MIN")
						{
							sum = statistics.Minimum;
						}
						else if (statisticsTypes == "STD")
						{
							sum = statistics.StandardDeviation;
						}
						else if (statisticsTypes == "MEAN")
						{
							sum = statistics.Mean;
						}
					}
					str[j] = sum.ToString();
				}
				Marshal.ReleaseComObject(cursor);
				cursor = null;
				statistics = null;
			}
			arrayLists.Add(str);
			return arrayLists;
		}

		private void frmStaticByGeometry_Load(object sender, EventArgs e)
		{
			this.Init();
		}

		public bool GetUniqueValues(ITable pTable, string sFieldName, IList Items)
		{
			bool flag;
			try
			{
				ICursor cursor = pTable.Search(null, false);
				IDataStatistics dataStatisticsClass = new DataStatistics()
				{
					Field = sFieldName,
					Cursor = cursor
				};
				IEnumerator uniqueValues = dataStatisticsClass.UniqueValues;
				uniqueValues.Reset();
				while (uniqueValues.MoveNext())
				{
					Items.Add(uniqueValues.Current);
				}
				Marshal.ReleaseComObject(dataStatisticsClass);
				dataStatisticsClass = null;
				Marshal.ReleaseComObject(cursor);
				cursor = null;
				flag = true;
				return flag;
			}
			catch (Exception exception)
			{
			}
			flag = false;
			return flag;
		}

		private void Init()
		{
			string[] strArrays = new string[] { "SUM", "MEAN", "MAX", "MIN", "STD" };
			this.listView1.SetColumn(1, ListViewColumnStyle.ComboBox);
			this.listView1.BoundListToColumn(1, strArrays);
			if (this.m_pFocusMap != null)
			{
				for (int i = 0; i < this.m_pFocusMap.LayerCount; i++)
				{
					ILayer layer = this.m_pFocusMap.Layer[i];
					if (layer is IGroupLayer)
					{
						this.AddGroupLayerToComboBox(layer as ICompositeLayer);
					}
					else if (layer is IFeatureLayer)
					{
						IFeatureLayer featureLayer = layer as IFeatureLayer;
						this.cboLayers.Properties.Items.Add(new ObjectWrapEx(featureLayer));
					}
				}
			}
		}

		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStaticByGeometry));
			this.label1 = new Label();
			this.cboLayers = new ComboBoxEdit();
			this.label3 = new Label();
			this.groupBox1 = new GroupBox();
			this.btnDelete = new SimpleButton();
			this.btnSelectField = new SimpleButton();
			this.cboFields = new ComboBoxEdit();
			this.label4 = new Label();
			this.listView1 = new EditListView();
			this.columnHeader1 = new LVColumnHeader();
			this.columnHeader2 = new LVColumnHeader();
			this.cboClassField = new ComboBoxEdit();
			this.btnCancel = new SimpleButton();
			this.btnOK = new SimpleButton();
			this.listView2 = new ListView();
			((ISupportInitialize)this.cboLayers.Properties).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.cboFields.Properties).BeginInit();
			((ISupportInitialize)this.cboClassField.Properties).BeginInit();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "图层";
			this.cboLayers.EditValue = "";
			this.cboLayers.Location = new System.Drawing.Point(64, 8);
			this.cboLayers.Name = "cboLayers";
            this.cboLayers.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
			this.cboLayers.Size = new System.Drawing.Size(224, 21);
			this.cboLayers.TabIndex = 1;
			this.cboLayers.SelectedIndexChanged += new EventHandler(this.cboLayers_SelectedIndexChanged);
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 10;
			this.label3.Text = "分类字段";
			this.groupBox1.Controls.Add(this.btnDelete);
			this.groupBox1.Controls.Add(this.btnSelectField);
			this.groupBox1.Controls.Add(this.cboFields);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.listView1);
			this.groupBox1.Location = new System.Drawing.Point(8, 72);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(296, 152);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "统计字段";
			this.btnDelete.Enabled = false;
			this.btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
			this.btnDelete.Location = new System.Drawing.Point(256, 88);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(24, 24);
			this.btnDelete.TabIndex = 13;
			this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
			this.btnSelectField.Enabled = false;
			this.btnSelectField.Image = (Image)resources.GetObject("btnSelectField.Image");
			this.btnSelectField.Location = new System.Drawing.Point(256, 56);
			this.btnSelectField.Name = "btnSelectField";
			this.btnSelectField.Size = new System.Drawing.Size(24, 24);
			this.btnSelectField.TabIndex = 12;
			this.btnSelectField.Click += new EventHandler(this.btnSelectField_Click);
			this.cboFields.EditValue = "";
			this.cboFields.Location = new System.Drawing.Point(56, 24);
			this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
			this.cboFields.Size = new System.Drawing.Size(192, 21);
			this.cboFields.TabIndex = 11;
			this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 12);
			this.label4.TabIndex = 10;
			this.label4.Text = "字段";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
			this.listView1.ComboBoxBgColor = Color.White;
			this.listView1.ComboBoxFont = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.listView1.EditBgColor = Color.White;
			this.listView1.EditFont = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 56);
			this.listView1.LockRowCount = 0;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(240, 88);
			this.listView1.TabIndex = 9;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = View.Details;
			this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
			this.columnHeader1.ColumnStyle = ListViewColumnStyle.ReadOnly;
			this.columnHeader1.Text = "字段";
			this.columnHeader1.Width = 109;
			this.columnHeader2.ColumnStyle = ListViewColumnStyle.ReadOnly;
			this.columnHeader2.Text = "统计类型";
			this.columnHeader2.Width = 125;
			this.cboClassField.EditValue = "";
			this.cboClassField.Location = new System.Drawing.Point(64, 40);
			this.cboClassField.Name = "cboClassField";
			 this.cboClassField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            
			this.cboClassField.Size = new System.Drawing.Size(224, 21);
			this.cboClassField.TabIndex = 9;
			this.cboClassField.SelectedIndexChanged += new EventHandler(this.cboClassField_SelectedIndexChanged);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(264, 344);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(48, 24);
			this.btnCancel.TabIndex = 22;
			this.btnCancel.Text = "关闭";
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(32, 344);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(48, 24);
			this.btnOK.TabIndex = 21;
			this.btnOK.Text = "统计";
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.listView2.Location = new System.Drawing.Point(8, 232);
			this.listView2.Name = "listView2";
			this.listView2.Size = new System.Drawing.Size(296, 96);
			this.listView2.TabIndex = 23;
			this.listView2.UseCompatibleStateImageBehavior = false;
			this.listView2.View = View.Details;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			base.ClientSize = new System.Drawing.Size(320, 373);
			base.Controls.Add(this.listView2);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.cboClassField);
			base.Controls.Add(this.cboLayers);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmStaticByGeometry";
			this.Text = "统计";
			base.Load += new EventHandler(this.frmStaticByGeometry_Load);
			((ISupportInitialize)this.cboLayers.Properties).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.cboFields.Properties).EndInit();
			((ISupportInitialize)this.cboClassField.Properties).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count <= 0)
			{
				this.btnDelete.Enabled = false;
			}
			else
			{
				this.btnDelete.Enabled = true;
			}
		}
	}
}