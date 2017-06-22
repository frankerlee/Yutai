using ApplicationData;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using PipeConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class SimpleQueryByAddressUI1 : Form
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

		private GroupBox groupBox2;

		private Button RevBut;

		private Button NoneBut;

		private Button AllBut;

		private CheckedListBox ValueBox;

		private TextBox FieldBox;

		private CheckBox BlurCheck;

		private Button CloseBut;

		private Button QueryBut;

		private RadioButton radioButton2;

		private RadioButton radioButton1;

		private ComboBox LayerBox;

		private Label lable;

		private ComboBox FieldValueBox;

		private Button WipeBut;

		private Button FillAllBut;

		public IGeometry m_ipGeo;

		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipeConfig pPipeCfg;

		private IFeatureLayer SelectLayer;

		private IFields myfields;

		private QueryResult resultDlg;

		private string FindField = "";

		private string FindField1 = "";

		public object mainform;

		private bool bControlEvent;

		private bool bUnWipe = true;

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
			this.groupBox2 = new GroupBox();
			this.FillAllBut = new Button();
			this.WipeBut = new Button();
			this.RevBut = new Button();
			this.NoneBut = new Button();
			this.AllBut = new Button();
			this.ValueBox = new CheckedListBox();
			this.FieldBox = new TextBox();
			this.BlurCheck = new CheckBox();
			this.CloseBut = new Button();
			this.QueryBut = new Button();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.LayerBox = new ComboBox();
			this.lable = new Label();
			this.FieldValueBox = new ComboBox();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.FillAllBut);
			this.groupBox2.Controls.Add(this.WipeBut);
			this.groupBox2.Controls.Add(this.RevBut);
			this.groupBox2.Controls.Add(this.NoneBut);
			this.groupBox2.Controls.Add(this.AllBut);
			this.groupBox2.Controls.Add(this.ValueBox);
			this.groupBox2.Location = new System.Drawing.Point(3, 101);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(298, 191);
			this.groupBox2.TabIndex = 41;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "选择查询对象：点性";
			this.FillAllBut.Location = new System.Drawing.Point(208, 162);
			this.FillAllBut.Name = "FillAllBut";
			this.FillAllBut.Size = new Size(84, 23);
			this.FillAllBut.TabIndex = 5;
			this.FillAllBut.Text = "显示全部(&S)";
			this.FillAllBut.UseVisualStyleBackColor = true;
			this.FillAllBut.Click += new EventHandler(this.FillAllBut_Click);
			this.WipeBut.Location = new System.Drawing.Point(209, 137);
			this.WipeBut.Name = "WipeBut";
			this.WipeBut.Size = new Size(83, 23);
			this.WipeBut.TabIndex = 4;
			this.WipeBut.Text = "滤除多余(&W)";
			this.WipeBut.UseVisualStyleBackColor = true;
			this.WipeBut.Click += new EventHandler(this.WipeBut_Click);
			this.RevBut.Location = new System.Drawing.Point(209, 81);
			this.RevBut.Name = "RevBut";
			this.RevBut.Size = new Size(83, 23);
			this.RevBut.TabIndex = 3;
			this.RevBut.Text = "反选(&I)";
			this.RevBut.UseVisualStyleBackColor = true;
			this.RevBut.Click += new EventHandler(this.RevBut_Click);
			this.NoneBut.Location = new System.Drawing.Point(209, 52);
			this.NoneBut.Name = "NoneBut";
			this.NoneBut.Size = new Size(83, 23);
			this.NoneBut.TabIndex = 2;
			this.NoneBut.Text = "全不选(&N)";
			this.NoneBut.UseVisualStyleBackColor = true;
			this.NoneBut.Click += new EventHandler(this.NoneBut_Click);
			this.AllBut.Location = new System.Drawing.Point(209, 24);
			this.AllBut.Name = "AllBut";
			this.AllBut.Size = new Size(83, 23);
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
			this.FieldBox.BorderStyle = BorderStyle.None;
			this.FieldBox.Location = new System.Drawing.Point(4, 71);
			this.FieldBox.Name = "FieldBox";
			this.FieldBox.ReadOnly = true;
			this.FieldBox.Size = new Size(55, 14);
			this.FieldBox.TabIndex = 39;
			this.FieldBox.Text = "FieldBox";
			this.BlurCheck.AutoSize = true;
			this.BlurCheck.Location = new System.Drawing.Point(321, 276);
			this.BlurCheck.Name = "BlurCheck";
			this.BlurCheck.Size = new Size(72, 16);
			this.BlurCheck.TabIndex = 38;
			this.BlurCheck.Text = "模糊查询";
			this.BlurCheck.UseVisualStyleBackColor = true;
			this.BlurCheck.CheckedChanged += new EventHandler(this.BlurCheck_CheckedChanged);
			this.CloseBut.DialogResult = DialogResult.Cancel;
			this.CloseBut.Location = new System.Drawing.Point(316, 39);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(75, 23);
			this.CloseBut.TabIndex = 37;
			this.CloseBut.Text = "关闭(&C)";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			this.QueryBut.Location = new System.Drawing.Point(316, 10);
			this.QueryBut.Name = "QueryBut";
			this.QueryBut.Size = new Size(75, 23);
			this.QueryBut.TabIndex = 36;
			this.QueryBut.Text = "查询(&Q)";
			this.QueryBut.UseVisualStyleBackColor = true;
			this.QueryBut.Click += new EventHandler(this.QueryBut_Click);
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(131, 10);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(59, 16);
			this.radioButton2.TabIndex = 35;
			this.radioButton2.Text = "管线层";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(12, 10);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new Size(59, 16);
			this.radioButton1.TabIndex = 34;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "管点层";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.LayerBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(32, 33);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 33;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(1, 37);
			this.lable.Name = "lable";
			this.lable.Size = new Size(29, 12);
			this.lable.TabIndex = 32;
			this.lable.Text = "图层";
			this.FieldValueBox.FormattingEnabled = true;
			this.FieldValueBox.Location = new System.Drawing.Point(63, 68);
			this.FieldValueBox.Name = "FieldValueBox";
			this.FieldValueBox.Size = new Size(237, 20);
			this.FieldValueBox.TabIndex = 42;
			this.FieldValueBox.SelectedIndexChanged += new EventHandler(this.FieldValueBox_SelectedIndexChanged);
			this.FieldValueBox.TextUpdate += new EventHandler(this.FieldValueBox_TextUpdate);
			this.FieldValueBox.TextChanged += new EventHandler(this.FieldValueBox_TextChanged);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(403, 310);
			base.Controls.Add(this.FieldValueBox);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.FieldBox);
			base.Controls.Add(this.BlurCheck);
			base.Controls.Add(this.CloseBut);
			base.Controls.Add(this.QueryBut);
			base.Controls.Add(this.radioButton2);
			base.Controls.Add(this.radioButton1);
			base.Controls.Add(this.LayerBox);
			base.Controls.Add(this.lable);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "SimpleQueryByAddressUI1";
			base.ShowInTaskbar = false;
			this.Text = "快速查询-按地址";
			base.Load += new EventHandler(this.SimpleQueryByAddressUI1_Load);
			base.HelpRequested += new HelpEventHandler(this.SimpleQueryByAddressUI1_HelpRequested);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public SimpleQueryByAddressUI1()
		{
			this.InitializeComponent();
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
						SimpleQueryByAddressUI1.LayerboxItem layerboxItem = new SimpleQueryByAddressUI1.LayerboxItem();
						layerboxItem.m_pPipeLayer = iFLayer;
						this.LayerBox.Items.Add(layerboxItem);
					}
				}
				else if (this.pPipeCfg.IsPipeLine(aliasName))
				{
					SimpleQueryByAddressUI1.LayerboxItem layerboxItem2 = new SimpleQueryByAddressUI1.LayerboxItem();
					layerboxItem2.m_pPipeLayer = iFLayer;
					this.LayerBox.Items.Add(layerboxItem2);
				}
			}
		}

		private void FillLayerBox()
		{
			this.LayerBox.Items.Clear();
			this.FieldValueBox.Items.Clear();
			this.ValueBox.Items.Clear();
			int layerCount = m_context.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer ipLay = m_context.FocusMap.get_Layer(i);
				this.AddLayer(ipLay);
				if (this.LayerBox.Items.Count > 0)
				{
					this.LayerBox.SelectedIndex = 0;
				}
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
					this.SelectLayer = ((SimpleQueryByAddressUI1.LayerboxItem)this.LayerBox.SelectedItem).m_pPipeLayer;
					if (this.SelectLayer == null)
					{
						result = false;
					}
					else
					{
						this.myfields = this.SelectLayer.FeatureClass.Fields;
						if (this.radioButton1.Checked)
						{
							this.FieldBox.Text = this.pPipeCfg.GetPointTableFieldName("所在道路");
							this.FindField = this.pPipeCfg.GetPointTableFieldName("点性");
						}
						else
						{
							this.FieldBox.Text = this.pPipeCfg.GetLineTableFieldName("所在道路");
							this.FindField = this.pPipeCfg.GetLineTableFieldName("管径");
							this.FindField1 = this.pPipeCfg.GetLineTableFieldName("断面尺寸");
						}
						if (this.myfields.FindField(this.FindField) < 0 && this.myfields.FindField(this.FindField1) < 0)
						{
							this.ValueBox.Enabled = false;
							this.AllBut.Enabled = false;
							this.NoneBut.Enabled = false;
							this.RevBut.Enabled = false;
						}
						else
						{
							this.ValueBox.Enabled = true;
							this.AllBut.Enabled = true;
							this.NoneBut.Enabled = true;
							this.RevBut.Enabled = true;
						}
						if (this.myfields.FindField(this.FieldBox.Text) < 0)
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
				int num = -1;
				this.bUnWipe = true;
				int num2 = this.myfields.FindField(this.FieldBox.Text);
				if (num2 >= 0)
				{
					IFeatureClass featureClass = this.SelectLayer.FeatureClass;
					IQueryFilter queryFilter = new QueryFilter();
					IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
					IFeature feature = featureCursor.NextFeature();
					this.FieldValueBox.Items.Clear();
					this.ValueBox.Items.Clear();
					int num3 = this.myfields.FindField(this.FindField);
					if (!this.radioButton1.Checked)
					{
						num = this.myfields.FindField(this.FindField1);
					}
					while (feature != null)
					{
						object obj = feature.get_Value(num2);
						if (num3 > 0)
						{
							object obj2 = feature.get_Value(num3);
							if (!(obj2 is DBNull))
							{
								string text = obj2.ToString();
								if (text.Length > 0 && !this.ValueBox.Items.Contains(text))
								{
									this.ValueBox.Items.Add(text);
								}
							}
						}
						if (num > 0)
						{
							object obj2 = feature.get_Value(num);
							if (!(obj2 is DBNull))
							{
								string text = obj2.ToString();
								if (text.Length > 0 && !this.ValueBox.Items.Contains(text))
								{
									this.ValueBox.Items.Add(text);
								}
							}
						}
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
								if (!this.FieldValueBox.Items.Contains(text))
								{
									this.FieldValueBox.Items.Add(text);
								}
								feature = featureCursor.NextFeature();
							}
						}
					}
					if (this.FieldValueBox.Items.Count > 0)
					{
						this.FieldValueBox.SelectedIndex = 0;
					}
				}
			}
		}

		private void SimpleQueryByAddressUI1_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		public void AutoFlash()
		{
			this.FillLayerBox();
			if (this.ValidateField())
			{
				this.FillValueBox();
			}
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			this.groupBox2.Text = "选择查询对象：点性";
			this.FieldValueBox.Text = "";
			this.FillLayerBox();
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			this.groupBox2.Text = "选择查询对象：管径/沟截面宽高";
			this.FieldValueBox.Text = "";
			this.FillLayerBox();
		}

		private void LayerBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ValidateField())
			{
				this.FillValueBox();
			}
		}

		private void AllBut_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				this.ValueBox.SetItemChecked(i, true);
			}
		}

		private void NoneBut_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				this.ValueBox.SetItemChecked(i, false);
			}
		}

		private void RevBut_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				if (this.ValueBox.GetItemChecked(i))
				{
					this.ValueBox.SetItemChecked(i, false);
				}
				else
				{
					this.ValueBox.SetItemChecked(i, true);
				}
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
			base.Close();
		}

		private void QueryBut_Click(object sender, EventArgs e)
		{
			IFeatureClass featureClass = this.SelectLayer.FeatureClass;
			ISpatialFilter spatialFilter = new SpatialFilter();
			IFeatureCursor pCursor = null;
			string text = "";
			if (this.FieldValueBox.Text == string.Empty)
			{
				MessageBox.Show("请确定所在位置!");
			}
			else
			{
				if (!this.BlurCheck.Checked)
				{
					text = this.FieldBox.Text;
					text += "='";
					text += this.FieldValueBox.Text;
					text += "'";
				}
				else
				{
					IDataset dataset = this.SelectLayer.FeatureClass as IDataset;
					if (dataset.Workspace.Type == (esriWorkspaceType) 2)
					{
						text = this.FieldBox.Text;
						text += " LIKE '%";
						text += this.FieldValueBox.Text;
						text += "%'";
					}
					else
					{
						text = this.FieldBox.Text;
						text += " LIKE '*";
						text += this.FieldValueBox.Text;
						text += "*'";
					}
				}
				List<string> list = new List<string>();
				list.Clear();
				for (int i = 0; i < this.ValueBox.Items.Count; i++)
				{
					if (this.ValueBox.GetItemChecked(i))
					{
						string item = this.ValueBox.Items[i].ToString();
						list.Add(item);
					}
				}
				if (this.radioButton1.Checked)
				{
					if (list.Count > 0)
					{
						text += " and ( ";
						int num = 1;
						int count = list.Count;
						foreach (string current in list)
						{
							text += this.FindField;
							text += " = '";
							text += current;
							text += "'";
							if (num < count)
							{
								text += " OR ";
							}
							num++;
						}
						text += ")";
					}
				}
				else if (list.Count > 0)
				{
					text += " and ( ";
					int num2 = 1;
					int count2 = list.Count;
					foreach (string current2 in list)
					{
						if (current2.Contains("x") || current2.Contains("X") || current2.Contains("*"))
						{
							text += this.FindField1;
							text += " = '";
							text += current2;
							text += "'";
						}
						else
						{
							text += this.FindField;
							text += " = ";
							text += current2;
						}
						if (num2 < count2)
						{
							text += " OR ";
						}
						num2++;
					}
					text += ")";
				}
				spatialFilter.WhereClause=text;
				try
				{
					pCursor = featureClass.Search(spatialFilter, false);
				}
				catch (Exception ex)
				{
					MessageBox.Show("查询条件过于复杂,请减少条件项。" + ex.Message);
				}
				this.m_iApp.SetResult(pCursor, (IFeatureSelection)this.SelectLayer);
			}
		}

		private void WipeValueBox()
		{
			this.bUnWipe = false;
			if (this.myfields != null)
			{
				int num = -1;
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
				IQueryFilter queryFilter = new QueryFilter();
				string text;
				if (!this.BlurCheck.Checked)
				{
					text = this.FieldBox.Text;
					text += "='";
					text += this.FieldValueBox.Text;
					text += "'";
				}
				else
				{
					text = this.FieldBox.Text;
					text += " LIKE '*";
					text += this.FieldValueBox.Text;
					text += "*'";
				}
				queryFilter.WhereClause=text;
				IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
				IFeature feature = featureCursor.NextFeature();
				this.ValueBox.Items.Clear();
				int num2 = this.myfields.FindField(this.FindField);
				if (!this.radioButton1.Checked)
				{
					num = this.myfields.FindField(this.FindField1);
				}
				while (feature != null)
				{
					if (num2 > 0)
					{
						object obj = feature.get_Value(num2);
						if (!(obj is DBNull))
						{
							string text2 = obj.ToString();
							if (text2.Length > 0 && !this.ValueBox.Items.Contains(text2))
							{
								this.ValueBox.Items.Add(text2);
							}
						}
					}
					if (num > 0)
					{
						object obj = feature.get_Value(num);
						if (!(obj is DBNull))
						{
							string text2 = obj.ToString();
							if (text2.Length > 0 && !this.ValueBox.Items.Contains(text2))
							{
								this.ValueBox.Items.Add(text2);
							}
						}
					}
					feature = featureCursor.NextFeature();
				}
			}
		}

		private void WipeBut_Click(object sender, EventArgs e)
		{
			this.WipeValueBox();
		}

		private void UnWipeValuebox()
		{
			if (this.myfields != null)
			{
				int num = -1;
				if (!this.bUnWipe)
				{
					this.bUnWipe = true;
					IFeatureClass featureClass = this.SelectLayer.FeatureClass;
					IQueryFilter queryFilter = new QueryFilter();
					IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
					IFeature feature = featureCursor.NextFeature();
					this.ValueBox.Items.Clear();
					int num2 = this.myfields.FindField(this.FindField);
					if (!this.radioButton1.Checked)
					{
						num = this.myfields.FindField(this.FindField1);
					}
					while (feature != null)
					{
						if (num2 > 0)
						{
							object obj = feature.get_Value(num2);
							if (!(obj is DBNull))
							{
								string text = obj.ToString();
								if (text.Length > 0 && !this.ValueBox.Items.Contains(text))
								{
									this.ValueBox.Items.Add(text);
								}
							}
						}
						if (num > 0)
						{
							object obj = feature.get_Value(num);
							if (!(obj is DBNull))
							{
								string text = obj.ToString();
								if (text.Length > 0 && !this.ValueBox.Items.Contains(text))
								{
									this.ValueBox.Items.Add(text);
								}
							}
						}
						feature = featureCursor.NextFeature();
					}
				}
			}
		}

		private void FieldValueBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.bUnWipe)
			{
				this.UnWipeValuebox();
			}
		}

		private void FieldValueBox_TextChanged(object sender, EventArgs e)
		{
		}

		private void BlurCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.bUnWipe)
			{
				this.UnWipeValuebox();
			}
		}

		private void FieldValueBox_TextUpdate(object sender, EventArgs e)
		{
			if (!this.bUnWipe)
			{
				this.UnWipeValuebox();
			}
		}

		private void FillAllBut_Click(object sender, EventArgs e)
		{
			if (!this.bUnWipe)
			{
				this.UnWipeValuebox();
			}
		}

		private void SimpleQueryByAddressUI1_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "快速查询";
			HelpNavigator command = HelpNavigator.KeywordIndex;
			Help.ShowHelp(this, url, command, parameter);
		}
	}
}
