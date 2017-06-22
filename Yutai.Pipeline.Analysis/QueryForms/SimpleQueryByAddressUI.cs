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
	public class SimpleQueryByAddressUI : Form
	{
		public IGeometry m_ipGeo;

		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipeConfig pPipeCfg;

		private IFeatureLayer SelectLayer;

		private IFields myfields;

		private QueryResult resultDlg;

		private List<string> ADArray = new List<string>();

		private IContainer components = null;

		private Button CloseBut;

		private Button QueryBut;

		private RadioButton radioButton2;

		private RadioButton radioButton1;

		private ComboBox LayerBox;

		private Label lable;

		private CheckBox BlurCheck;

		private TextBox FieldBox;

		private TextBox SqlBox;

		private Button RevBut;

		private Button NoneBut;

		private Button AllBut;

		private GroupBox groupBox2;

		private CheckedListBox ValueBox;

		public SimpleQueryByAddressUI()
		{
			this.InitializeComponent();
		}

		private void FillLayerBox()
		{
			this.LayerBox.Items.Clear();
			this.ADArray.Clear();
			this.SqlBox.Text = "";
			int layerCount = m_context.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer layer = m_context.FocusMap.get_Layer(i);
				if (layer is IGroupLayer)
				{
					ICompositeLayer compositeLayer = (ICompositeLayer)layer;
					if (compositeLayer == null)
					{
						return;
					}
					int count = compositeLayer.Count;
					for (int j = 0; j < count; j++)
					{
						ILayer layer2 = compositeLayer.get_Layer(j);
						string text = layer2.Name.ToString();
						if (this.radioButton1.Checked)
						{
							if (this.pPipeCfg.IsPipePoint(text))
							{
								this.LayerBox.Items.Add(text);
							}
						}
						else if (this.pPipeCfg.IsPipeLine(text))
						{
							this.LayerBox.Items.Add(text);
						}
					}
				}
				else if (layer is IFeatureLayer)
				{
					string text = layer.Name.ToString();
					if (this.radioButton1.Checked)
					{
						if (this.pPipeCfg.PointConfigItem(text) != null)
						{
							this.LayerBox.Items.Add(text);
						}
					}
					else if (this.pPipeCfg.LineConfigItem(text) != null)
					{
						this.LayerBox.Items.Add(text);
					}
				}
			}
			if (this.LayerBox.Items.Count > 0)
			{
				this.LayerBox.SelectedIndex = 0;
				return;
			}
		}

		private bool ValidateField()
		{
			int layerCount = m_context.FocusMap.LayerCount;
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
					string b = this.LayerBox.SelectedItem.ToString();
					for (int i = 0; i < layerCount; i++)
					{
						ILayer layer = m_context.FocusMap.get_Layer(i);
						if (layer is IFeatureLayer)
						{
							string a = layer.Name.ToString();
							if (a == b)
							{
								this.SelectLayer = (IFeatureLayer)m_context.FocusMap.get_Layer(i);
								break;
							}
						}
						else if (layer is IGroupLayer)
						{
							ICompositeLayer compositeLayer = (ICompositeLayer)layer;
							if (compositeLayer != null)
							{
								int count = compositeLayer.Count;
								for (int j = 0; j < count; j++)
								{
									ILayer layer2 = compositeLayer.get_Layer(j);
									string a = layer2.Name.ToString();
									if (a == b)
									{
										this.SelectLayer = (IFeatureLayer)layer2;
										break;
									}
								}
							}
						}
					}
					if (this.SelectLayer == null)
					{
						result = false;
					}
					else
					{
						this.myfields = this.SelectLayer.FeatureClass.Fields;
						if (this.radioButton1.Checked)
						{
							this.FieldBox.Text = this.pPipeCfg.GetPointTableFieldName("所属");
						}
						else
						{
							this.FieldBox.Text = this.pPipeCfg.GetLineTableFieldName("所在道路");
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
				int num = this.myfields.FindField(this.FieldBox.Text);
				if (num >= 0)
				{
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

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			this.FillLayerBox();
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			this.FillLayerBox();
		}

		private void SimpleQueryByAddressUI_Load(object sender, EventArgs e)
		{
			this.FillLayerBox();
			if (this.ValidateField())
			{
				this.FillValueBox();
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

		private void LayerBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ValidateField())
			{
				this.FillValueBox();
			}
		}

		private void AllBut_Click(object sender, EventArgs e)
		{
			this.ADArray.Clear();
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				this.ADArray.Add(this.ValueBox.Items[i].ToString());
				this.ValueBox.SetItemChecked(i, true);
			}
			string text = "";
			foreach (string current in this.ADArray)
			{
				text += current;
				text += " ";
			}
			this.SqlBox.Text = text;
		}

		private void NoneBut_Click(object sender, EventArgs e)
		{
			this.ADArray.Clear();
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				this.ValueBox.SetItemChecked(i, false);
			}
			this.SqlBox.Text = "";
		}

		private void RevBut_Click(object sender, EventArgs e)
		{
			this.ADArray.Clear();
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				if (this.ValueBox.GetItemChecked(i))
				{
					this.ValueBox.SetItemChecked(i, false);
				}
				else
				{
					this.ADArray.Add(this.ValueBox.Items[i].ToString());
					this.ValueBox.SetItemChecked(i, true);
				}
			}
			string text = "";
			foreach (string current in this.ADArray)
			{
				text += current;
				text += " ";
			}
			this.SqlBox.Text = text;
		}

		private void ValueBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = this.ValueBox.SelectedIndex;
			string text = "";
			string item = this.ValueBox.SelectedItem.ToString();
			if (this.ValueBox.GetItemChecked(selectedIndex))
			{
				if (!this.ADArray.Contains(item))
				{
					this.ADArray.Add(item);
				}
			}
			else if (this.ADArray.Contains(item))
			{
				this.ADArray.Remove(item);
			}
			foreach (string current in this.ADArray)
			{
				text += current;
				text += " ";
			}
			this.SqlBox.Text = text;
		}

		private void BlurCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (this.BlurCheck.Checked)
			{
				this.SqlBox.ReadOnly = false;
				this.SqlBox.Text = "";
				this.ValueBox.Enabled = false;
				this.AllBut.Enabled = false;
				this.NoneBut.Enabled = false;
				this.RevBut.Enabled = false;
				this.NoneBut_Click(null, null);
			}
			else
			{
				this.SqlBox.ReadOnly = true;
				this.SqlBox.Text = "";
				this.ValueBox.Enabled = true;
				this.AllBut.Enabled = true;
				this.NoneBut.Enabled = true;
				this.RevBut.Enabled = true;
			}
		}

		private void QueryBut_Click(object sender, EventArgs e)
		{
			IFeatureClass featureClass = this.SelectLayer.FeatureClass;
			ISpatialFilter spatialFilter = new SpatialFilter();
			string text = "";
			if (!this.BlurCheck.Checked)
			{
				int count = this.ADArray.Count;
				int num = 1;
				using (List<string>.Enumerator enumerator = this.ADArray.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						text += this.FieldBox.Text;
						text += " = \"";
						text += current;
						text += "\"";
						if (num < count)
						{
							text += " OR ";
						}
						num++;
					}
					goto IL_101;
				}
			}
			text = this.FieldBox.Text;
			text += " LIKE \"*";
			text += this.SqlBox.Text;
			text += "*\"";
			IL_101:
			spatialFilter.WhereClause=text;
			IFeatureCursor pFeatureCursor = featureClass.Search(spatialFilter, false);
			if (this.resultDlg == null || this.resultDlg.IsDisposed)
			{
				this.resultDlg = new QueryResult();
				this.resultDlg.pFeatureCursor = pFeatureCursor;
				this.resultDlg.MapControl = this.MapControl;
				this.resultDlg.pFeatureSelection = (IFeatureSelection)this.SelectLayer;
				this.resultDlg.Show(this);
			}
			else
			{
				this.resultDlg.pFeatureCursor = pFeatureCursor;
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

		private void SimpleQueryByAddressUI_HelpRequested(object sender, HelpEventArgs hlpevent)
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
			this.CloseBut = new Button();
			this.QueryBut = new Button();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.LayerBox = new ComboBox();
			this.lable = new Label();
			this.BlurCheck = new CheckBox();
			this.FieldBox = new TextBox();
			this.SqlBox = new TextBox();
			this.RevBut = new Button();
			this.NoneBut = new Button();
			this.AllBut = new Button();
			this.groupBox2 = new GroupBox();
			this.ValueBox = new CheckedListBox();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.CloseBut.Location = new System.Drawing.Point(327, 41);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(75, 23);
			this.CloseBut.TabIndex = 27;
			this.CloseBut.Text = "关闭";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			this.QueryBut.Location = new System.Drawing.Point(327, 12);
			this.QueryBut.Name = "QueryBut";
			this.QueryBut.Size = new Size(75, 23);
			this.QueryBut.TabIndex = 26;
			this.QueryBut.Text = "查询";
			this.QueryBut.UseVisualStyleBackColor = true;
			this.QueryBut.Click += new EventHandler(this.QueryBut_Click);
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(142, 12);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(59, 16);
			this.radioButton2.TabIndex = 25;
			this.radioButton2.Text = "管线层";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(23, 12);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new Size(59, 16);
			this.radioButton1.TabIndex = 24;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "管点层";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(43, 35);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 23;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(12, 39);
			this.lable.Name = "lable";
			this.lable.Size = new Size(29, 12);
			this.lable.TabIndex = 22;
			this.lable.Text = "图层";
			this.BlurCheck.AutoSize = true;
			this.BlurCheck.Location = new System.Drawing.Point(332, 278);
			this.BlurCheck.Name = "BlurCheck";
			this.BlurCheck.Size = new Size(72, 16);
			this.BlurCheck.TabIndex = 28;
			this.BlurCheck.Text = "模糊查询";
			this.BlurCheck.UseVisualStyleBackColor = true;
			this.BlurCheck.CheckedChanged += new EventHandler(this.BlurCheck_CheckedChanged);
			this.FieldBox.BorderStyle = BorderStyle.None;
			this.FieldBox.Location = new System.Drawing.Point(15, 73);
			this.FieldBox.Name = "FieldBox";
			this.FieldBox.ReadOnly = true;
			this.FieldBox.Size = new Size(55, 14);
			this.FieldBox.TabIndex = 29;
			this.FieldBox.Text = "FieldBox";
			this.SqlBox.Location = new System.Drawing.Point(76, 66);
			this.SqlBox.Name = "SqlBox";
			this.SqlBox.ReadOnly = true;
			this.SqlBox.Size = new Size(222, 21);
			this.SqlBox.TabIndex = 30;
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
			this.groupBox2.Controls.Add(this.RevBut);
			this.groupBox2.Controls.Add(this.NoneBut);
			this.groupBox2.Controls.Add(this.AllBut);
			this.groupBox2.Controls.Add(this.ValueBox);
			this.groupBox2.Location = new System.Drawing.Point(14, 103);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(298, 191);
			this.groupBox2.TabIndex = 31;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "选择查询对象";
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
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(416, 306);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.SqlBox);
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
			base.Name = "SimpleQueryByAddressUI";
			this.Text = "快速查询－按地址";
			base.Load += new EventHandler(this.SimpleQueryByAddressUI_Load);
			base.HelpRequested += new HelpEventHandler(this.SimpleQueryByAddressUI_HelpRequested);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
