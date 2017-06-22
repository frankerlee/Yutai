
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Yutai.PipeConfig;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class SimpleStatMyshUI : Form
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

		private GroupBox groupBox4;

		private DataGridView dataGridView1;

		private GroupBox groupBox3;

		private Button button4;

		private Button button3;

		private Button button2;

		private GroupBox groupBox2;

		private ListBox listBox1;

		private GroupBox groupBox1;

		private Button RevBut;

		private Button NoneBut;

		private Button AllBut;

		private CheckedListBox checkedListBox1;

		private Button CalButton;

		private Button button1;

		private Button button5;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column4;

		private Label label1;

		private NumericUpDown numericUpDown1;

		private Label label2;

		private Button InsertBut;

		private CheckBox GeometrySet;

		public IGeometry m_ipGeo;

		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipeConfig pPipeCfg;

		private IField myfield;

		private IFeatureLayer SelectLayer;

		private IFields myfields;

		private IList<IPoint> DXArray = new List<IPoint>();

		private DataTable Sumtable = new DataTable();

		private double minNum;

		private double maxNum;

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
			this.groupBox4 = new GroupBox();
			this.label1 = new Label();
			this.dataGridView1 = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.groupBox3 = new GroupBox();
			this.InsertBut = new Button();
			this.button5 = new Button();
			this.button4 = new Button();
			this.button3 = new Button();
			this.button2 = new Button();
			this.groupBox2 = new GroupBox();
			this.label2 = new Label();
			this.numericUpDown1 = new NumericUpDown();
			this.listBox1 = new ListBox();
			this.groupBox1 = new GroupBox();
			this.RevBut = new Button();
			this.NoneBut = new Button();
			this.AllBut = new Button();
			this.checkedListBox1 = new CheckedListBox();
			this.CalButton = new Button();
			this.button1 = new Button();
			this.GeometrySet = new CheckBox();
			this.groupBox4.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.numericUpDown1).BeginInit();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.dataGridView1);
			this.groupBox4.Location = new System.Drawing.Point(182, 117);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new Size(139, 180);
			this.groupBox4.TabIndex = 25;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "统计范围设置";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 157);
			this.label1.Name = "label1";
			this.label1.Size = new Size(107, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "下限=<统计值<上限";
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.BackgroundColor = SystemColors.ActiveCaptionText;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Column1,
				this.Column4
			});
			this.dataGridView1.GridColor = SystemColors.ControlText;
			this.dataGridView1.Location = new System.Drawing.Point(8, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new Size(125, 142);
			this.dataGridView1.TabIndex = 4;
			this.Column1.Frozen = true;
			this.Column1.HeaderText = "下限";
			this.Column1.MaxInputLength = 6;
			this.Column1.MinimumWidth = 6;
			this.Column1.Name = "Column1";
			this.Column1.Width = 60;
			this.Column4.HeaderText = "上限";
			this.Column4.MaxInputLength = 6;
			this.Column4.Name = "Column4";
			this.Column4.Width = 60;
			this.groupBox3.Controls.Add(this.InsertBut);
			this.groupBox3.Controls.Add(this.button5);
			this.groupBox3.Controls.Add(this.button4);
			this.groupBox3.Controls.Add(this.button3);
			this.groupBox3.Controls.Add(this.button2);
			this.groupBox3.Location = new System.Drawing.Point(98, 117);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(78, 180);
			this.groupBox3.TabIndex = 24;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "操作";
			this.InsertBut.Location = new System.Drawing.Point(3, 148);
			this.InsertBut.Name = "InsertBut";
			this.InsertBut.Size = new Size(69, 23);
			this.InsertBut.TabIndex = 28;
			this.InsertBut.Text = "插入行";
			this.InsertBut.UseVisualStyleBackColor = true;
			this.InsertBut.Click += new EventHandler(this.InsertBut_Click);
			this.button5.Location = new System.Drawing.Point(3, 84);
			this.button5.Name = "button5";
			this.button5.Size = new Size(69, 23);
			this.button5.TabIndex = 27;
			this.button5.Text = "添加行";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new EventHandler(this.button5_Click);
			this.button4.Location = new System.Drawing.Point(3, 116);
			this.button4.Name = "button4";
			this.button4.Size = new Size(69, 23);
			this.button4.TabIndex = 26;
			this.button4.Text = "删除行";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new EventHandler(this.button4_Click);
			this.button3.Location = new System.Drawing.Point(3, 52);
			this.button3.Name = "button3";
			this.button3.Size = new Size(69, 23);
			this.button3.TabIndex = 25;
			this.button3.Text = "添加上限";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.button2.Location = new System.Drawing.Point(3, 20);
			this.button2.Name = "button2";
			this.button2.Size = new Size(69, 23);
			this.button2.TabIndex = 21;
			this.button2.Text = "添加下限";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.numericUpDown1);
			this.groupBox2.Controls.Add(this.listBox1);
			this.groupBox2.Location = new System.Drawing.Point(9, 117);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(83, 180);
			this.groupBox2.TabIndex = 23;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "埋深范围";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 136);
			this.label2.Name = "label2";
			this.label2.Size = new Size(59, 12);
			this.label2.TabIndex = 29;
			this.label2.Text = "埋深分段:";
			this.numericUpDown1.Location = new System.Drawing.Point(8, 153);
			int[] array = new int[4];
			array[0] = 20;
			this.numericUpDown1.Maximum = new decimal(array);
			int[] array2 = new int[4];
			array2[0] = 1;
			this.numericUpDown1.Minimum = new decimal(array2);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new Size(71, 21);
			this.numericUpDown1.TabIndex = 28;
			this.numericUpDown1.TextAlign = HorizontalAlignment.Center;
			int[] array3 = new int[4];
			array3[0] = 5;
			this.numericUpDown1.Value = new decimal(array3);
			this.numericUpDown1.ValueChanged += new EventHandler(this.numericUpDown1_ValueChanged);
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(6, 20);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new Size(71, 112);
			this.listBox1.Sorted = true;
			this.listBox1.TabIndex = 17;
			this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
			this.groupBox1.Controls.Add(this.RevBut);
			this.groupBox1.Controls.Add(this.NoneBut);
			this.groupBox1.Controls.Add(this.AllBut);
			this.groupBox1.Controls.Add(this.checkedListBox1);
			this.groupBox1.Location = new System.Drawing.Point(9, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(312, 108);
			this.groupBox1.TabIndex = 22;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "管线数据层列表";
			this.RevBut.Location = new System.Drawing.Point(228, 74);
			this.RevBut.Name = "RevBut";
			this.RevBut.Size = new Size(76, 23);
			this.RevBut.TabIndex = 6;
			this.RevBut.Text = "反选(&I)";
			this.RevBut.UseVisualStyleBackColor = true;
			this.RevBut.Click += new EventHandler(this.RevBut_Click);
			this.NoneBut.Location = new System.Drawing.Point(229, 45);
			this.NoneBut.Name = "NoneBut";
			this.NoneBut.Size = new Size(76, 23);
			this.NoneBut.TabIndex = 5;
			this.NoneBut.Text = "全不选(&N)";
			this.NoneBut.UseVisualStyleBackColor = true;
			this.NoneBut.Click += new EventHandler(this.NoneBut_Click);
			this.AllBut.Location = new System.Drawing.Point(229, 16);
			this.AllBut.Name = "AllBut";
			this.AllBut.Size = new Size(76, 23);
			this.AllBut.TabIndex = 4;
			this.AllBut.Text = "全选(&A)";
			this.AllBut.UseVisualStyleBackColor = true;
			this.AllBut.Click += new EventHandler(this.AllBut_Click);
			this.checkedListBox1.CheckOnClick = true;
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.Location = new System.Drawing.Point(6, 16);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new Size(208, 84);
			this.checkedListBox1.Sorted = true;
			this.checkedListBox1.TabIndex = 2;
			this.checkedListBox1.SelectedIndexChanged += new EventHandler(this.checkedListBox1_SelectedIndexChanged);
			this.CalButton.Location = new System.Drawing.Point(101, 303);
			this.CalButton.Name = "CalButton";
			this.CalButton.Size = new Size(71, 23);
			this.CalButton.TabIndex = 26;
			this.CalButton.Text = "确定(&Q)";
			this.CalButton.UseVisualStyleBackColor = true;
			this.CalButton.Click += new EventHandler(this.CalButton_Click);
			this.button1.Location = new System.Drawing.Point(188, 303);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 23);
			this.button1.TabIndex = 27;
			this.button1.Text = "关闭(&G)";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.GeometrySet.AutoSize = true;
			this.GeometrySet.Location = new System.Drawing.Point(17, 307);
			this.GeometrySet.Name = "GeometrySet";
			this.GeometrySet.Size = new Size(72, 16);
			this.GeometrySet.TabIndex = 28;
			this.GeometrySet.Text = "空间范围";
			this.GeometrySet.UseVisualStyleBackColor = true;
			this.GeometrySet.Click += new EventHandler(this.GeometrySet_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(329, 328);
			base.Controls.Add(this.GeometrySet);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.CalButton);
			base.Controls.Add(this.groupBox4);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "SimpleStatMyshUI";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "埋深分段统计";
			base.Load += new EventHandler(this.SimpleStatMyshUI_Load);
			base.VisibleChanged += new EventHandler(this.SimpleStatMyshUI_VisibleChanged);
			base.HelpRequested += new HelpEventHandler(this.SimpleStatMyshUI_HelpRequested);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)this.numericUpDown1).EndInit();
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public SimpleStatMyshUI()
		{
			this.InitializeComponent();
		}

		private void SimpleStatMyshUI_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		public void AutoFlash()
		{
			this.checkedListBox1.Items.Clear();
			int layerCount = m_context.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer ipLay = m_context.FocusMap.get_Layer(i);
				this.AddLayer(ipLay);
			}
			if (this.checkedListBox1.Items.Count > 0)
			{
				this.checkedListBox1.SelectedIndex = 0;
			}
		}

		public void FillValue()
		{
			if (this.myfields != null)
			{
				int num = this.myfields.FindField("埋深");
				this.myfield = this.myfields.get_Field(num);
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
				IQueryFilter queryFilter = new QueryFilter();
				IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
				IFeature feature = featureCursor.NextFeature();
				this.listBox1.Items.Clear();
				while (feature != null)
				{
					object obj = feature.get_Value(num);
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
					if (!this.listBox1.Items.Contains(text))
					{
						this.listBox1.Items.Add(text);
					}
					feature = featureCursor.NextFeature();
				}
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
				if (this.pPipeCfg.IsPipeLine(aliasName))
				{
					SimpleStatMyshUI.LayerboxItem layerboxItem = new SimpleStatMyshUI.LayerboxItem();
					layerboxItem.m_pPipeLayer = iFLayer;
					this.checkedListBox1.Items.Add(layerboxItem);
				}
			}
		}

		private bool ColumnEqual(object A, object B)
		{
			return (A == DBNull.Value && B == DBNull.Value) || (A != DBNull.Value && B != DBNull.Value && A.Equals(B));
		}

		private void CalButton_Click(object sender, EventArgs e)
		{
			int count = this.checkedListBox1.CheckedItems.Count;
			if (count == 0)
			{
				MessageBox.Show("请选定需要统计的管线");
			}
			else
			{
				int rowCount = this.dataGridView1.RowCount;
				if (rowCount <= 0)
				{
					MessageBox.Show("请确定上下限的值，其值不能为空");
				}
				else if (this.dataGridView1[0, 0].Value == null && this.dataGridView1[1, 0].Value == null)
				{
					MessageBox.Show("没有确定埋深的范围");
				}
				else
				{
					DataTable dataTable = new DataTable();
					dataTable.Columns.Clear();
					if (!dataTable.Columns.Contains("层名"))
					{
						dataTable.Columns.Add("层名", typeof(string));
					}
					if (!dataTable.Columns.Contains("统计范围"))
					{
						dataTable.Columns.Add("统计范围", typeof(string));
					}
					if (!dataTable.Columns.Contains("个数"))
					{
						dataTable.Columns.Add("个数", typeof(int));
					}
					int count2 = this.dataGridView1.Rows.Count;
					for (int i = 0; i < count; i++)
					{
						for (int j = 0; j < count2; j++)
						{
							this.DXArray.Clear();
							string text = (string)this.dataGridView1[0, j].Value;
							string text2 = (string)this.dataGridView1[1, j].Value;
							if (text == null || text2 == null)
							{
								MessageBox.Show("请确定上下限的值，其值不能为空");
								return;
							}
							double num = 0.0;
							double num2 = 0.0;
							try
							{
								num = Convert.ToDouble(text);
								num2 = Convert.ToDouble(text2);
							}
							catch (Exception)
							{
								MessageBox.Show("请确定上下限的值是否输入有误");
								return;
							}
							int num3 = 0;
							int num4 = 0;
							IFeatureLayer pPipeLayer = ((SimpleStatMyshUI.LayerboxItem)this.checkedListBox1.CheckedItems[i]).m_pPipeLayer;
							ISpatialFilter spatialFilter = new SpatialFilter();
							if (this.GeometrySet.Checked)
							{
								if (this.m_ipGeo != null)
								{
									spatialFilter.Geometry=(this.m_ipGeo);
								}
								spatialFilter.SpatialRel=(esriSpatialRelEnum) (1);
							}
							IFeatureCursor featureCursor = pPipeLayer.FeatureClass.Search(spatialFilter, false);
							IFeature feature = featureCursor.NextFeature();
							string name = pPipeLayer.Name;
							while (feature != null)
							{
								if (feature.Shape== null)
								{
									feature = featureCursor.NextFeature();
								}
								else
								{
									IPolyline polyline = (IPolyline)feature.Shape;
									IPointCollection pointCollection = (IPointCollection)polyline;
									IPoint point = pointCollection.get_Point(0);
									IPoint point2 = pointCollection.get_Point(1);
									double m = point.M;
									double m2 = point2.M;
									if (m >= num && m < num2 && !this.DXArray.Contains(point))
									{
										this.DXArray.Add(point);
										num3++;
									}
									if (m2 >= num && m2 < num2 && !this.DXArray.Contains(point2))
									{
										this.DXArray.Add(point2);
										num4++;
									}
									feature = featureCursor.NextFeature();
								}
							}
							int num5 = num3 + num4;
							object obj = text + "-" + text2;
							dataTable.Rows.Add(new object[]
							{
								name,
								obj,
								num5
							});
						}
					}
					new ClassCollectResultForm
					{
						nType = 0,
						ResultTable = dataTable
					}.ShowDialog();
				}
			}
		}

		private void AllBut_Click(object sender, EventArgs e)
		{
			this.minNum = 1000.0;
			this.maxNum = -1000.0;
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
				this.checkedListBox1.SetItemChecked(i, true);
			}
			this.listBox1.Items.Clear();
			int count = this.checkedListBox1.CheckedItems.Count;
			if (count != 0)
			{
				for (int j = 0; j < count; j++)
				{
					IFeatureLayer pPipeLayer = ((SimpleStatMyshUI.LayerboxItem)this.checkedListBox1.CheckedItems[j]).m_pPipeLayer;
					this.FillFieldValuesToListBox(pPipeLayer, this.listBox1);
				}
			}
			this.all();
		}

		public void all()
		{
			this.listBox1.Items.Clear();
			double num = Convert.ToDouble(this.numericUpDown1.Value);
			if (this.maxNum - this.minNum < 0.0001)
			{
				this.listBox1.Items.Add(this.maxNum);
			}
			else
			{
				double num2 = (this.maxNum - this.minNum) / num;
				int num3 = 0;
				while ((double)num3 < num + 1.0)
				{
					string text = (this.minNum + (double)num3 * num2).ToString("f2");
					if (!this.listBox1.Items.Contains(text))
					{
						this.listBox1.Items.Add(text);
					}
					num3++;
				}
			}
		}

		private void FillFieldValuesToListBox(IFeatureLayer pFeaLay, ListBox lbVal)
		{
			IFeatureClass featureClass = pFeaLay.FeatureClass;
			IQueryFilter queryFilter = new QueryFilter();
			IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
			IFeature feature = featureCursor.NextFeature();
			int num = featureClass.Fields.FindField("起点埋深");
			int num2 = featureClass.Fields.FindField("终点埋深");
			if (num != -1 && num2 != -1)
			{
				double num3 = 2147483647.0;
				double num4 = -2147483648.0;
				this.minNum = 2147483647.0;
				this.maxNum = -2147483648.0;
				while (feature != null)
				{
					try
					{
						object obj = feature.get_Value(num).ToString();
						object obj2 = feature.get_Value(num2).ToString();
						if (obj == null || Convert.IsDBNull(obj))
						{
							continue;
						}
						if (obj2 == null || Convert.IsDBNull(obj2))
						{
							continue;
						}
						double num5 = Convert.ToDouble(obj);
						double num6 = Convert.ToDouble(obj2);
						num3 = ((num3 < num5) ? num3 : num5);
						num3 = ((num3 < num6) ? num3 : num6);
						num4 = ((num4 > num5) ? num4 : num5);
						num4 = ((num4 > num6) ? num4 : num6);
						feature = featureCursor.NextFeature();
					}
					catch (Exception)
					{
						return;
					}
					if (num3 < this.minNum)
					{
						this.minNum = num3;
					}
					if (num4 > this.maxNum)
					{
						this.maxNum = num4;
					}
				}
				this.all();
			}
		}

		private void NoneBut_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
				this.checkedListBox1.SetItemChecked(i, false);
			}
			this.listBox1.Items.Clear();
		}

		private void RevBut_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
				if (this.checkedListBox1.GetItemChecked(i))
				{
					this.checkedListBox1.SetItemChecked(i, false);
				}
				else
				{
					this.checkedListBox1.SetItemChecked(i, true);
				}
			}
			this.listBox1.Items.Clear();
			int count = this.checkedListBox1.CheckedItems.Count;
			if (count != 0)
			{
				for (int j = 0; j < count; j++)
				{
					IFeatureLayer pPipeLayer = ((SimpleStatMyshUI.LayerboxItem)this.checkedListBox1.CheckedItems[j]).m_pPipeLayer;
					this.FillFieldValuesToListBox(pPipeLayer, this.listBox1);
				}
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.Rows.Count == 0)
			{
				MessageBox.Show("行数已为空");
			}
			else
			{
				this.dataGridView1.Rows.RemoveAt(this.dataGridView1.CurrentRow.Index);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (this.listBox1.Items.Count == 0)
			{
				MessageBox.Show("请选定要统计的管线");
			}
			else if (this.listBox1.SelectedItems.Count == 0)
			{
				MessageBox.Show("请选定需要的埋深");
			}
			else
			{
				int count = this.dataGridView1.Rows.Count;
				if (count < 1)
				{
					MessageBox.Show("请用户添加行");
				}
				else
				{
					string value = this.listBox1.SelectedItem.ToString();
					double num = Convert.ToDouble(value);
					int index = this.dataGridView1.CurrentRow.Index;
					double num2 = Convert.ToDouble(this.dataGridView1[0, index].Value);
					if (num2 > num)
					{
						MessageBox.Show("下限值不应大于上限值");
					}
					else
					{
						this.dataGridView1.CurrentRow.Cells[1].Value = value;
					}
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (this.listBox1.Items.Count == 0)
			{
				MessageBox.Show("请选定要统计的管线");
			}
			else if (this.listBox1.SelectedItems.Count == 0)
			{
				MessageBox.Show("请选定需要的埋深");
			}
			else
			{
				int count = this.dataGridView1.Rows.Count;
				if (count < 1)
				{
					MessageBox.Show("请用户添加行");
				}
				else
				{
					string value = this.listBox1.SelectedItem.ToString();
					double num = Convert.ToDouble(value);
					int index = this.dataGridView1.CurrentRow.Index;
					double num2 = Convert.ToDouble(this.dataGridView1[1, index].Value);
					if (num2 < num && num2 != 0.0)
					{
						MessageBox.Show("下限值不应大于上限值");
					}
					else
					{
						this.dataGridView1.CurrentRow.Cells[0].Value = value;
					}
				}
			}
		}

		private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.listBox1.Items.Clear();
			int count = this.checkedListBox1.CheckedItems.Count;
			if (count != 0)
			{
				for (int i = 0; i < count; i++)
				{
					IFeatureLayer pPipeLayer = ((SimpleStatMyshUI.LayerboxItem)this.checkedListBox1.CheckedItems[i]).m_pPipeLayer;
					this.FillFieldValuesToListBox(pPipeLayer, this.listBox1);
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.dataGridView1.Rows.Clear();
			this.GeometrySet.Checked = false;
			base.Close();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			this.dataGridView1.Rows.Add();
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dataGridView1.Rows.Count == 0)
			{
				this.dataGridView1.Rows.Add();
			}
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			this.all();
		}

		private void InsertBut_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.Rows.Count == 0)
			{
				this.dataGridView1.Rows.Add();
			}
			else
			{
				this.dataGridView1.Rows.Insert(this.dataGridView1.CurrentRow.Index, new object[0]);
			}
		}

		private void SimpleStatMyshUI_VisibleChanged(object sender, EventArgs e)
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

		private void GeometrySet_Click(object sender, EventArgs e)
		{
			if (!this.GeometrySet.Checked)
			{
				this.m_ipGeo = null;
				m_context.ActiveView.Refresh();
			}
		}

		private void SimpleStatMyshUI_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "分段统计";
			HelpNavigator command = HelpNavigator.KeywordIndex;
			Help.ShowHelp(this, url, command, parameter);
		}
	}
}
