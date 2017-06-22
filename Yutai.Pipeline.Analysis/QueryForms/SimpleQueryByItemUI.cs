using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class SimpleQueryByItemUI : Form
	{
		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipeConfig pPipeCfg;

		private IFeatureLayer SelectLayer;

		private IFields myfields;

		private QueryResult resultDlg;

		private List<string> ADArray = new List<string>();

		private DataTable Sumtable = new DataTable();

		private IContainer components = null;

		private RadioButton radioButton1;

		private RadioButton radioButton2;

		private Button button1;

		private Button button2;

		private ComboBox comboBox1;

		private CheckBox checkBox1;

		public SimpleQueryByItemUI()
		{
			this.InitializeComponent();
		}

		private void SimpleQueryByItemUI_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		public void AutoFlash()
		{
			int layerCount = m_context.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer ipLay = m_context.FocusMap.get_Layer(i);
				if (this.GetLayer("项目地界", ipLay))
				{
					break;
				}
			}
			if (this.SelectLayer == null)
			{
				base.Close();
			}
			else
			{
				this.FillLayerBox();
			}
		}

		private bool GetLayer(string name, ILayer ipLay)
		{
			bool result;
			if (ipLay is IFeatureLayer)
			{
				if (ipLay.Name == name)
				{
					this.SelectLayer = (ipLay as IFeatureLayer);
					result = true;
					return result;
				}
			}
			else if (ipLay is IGroupLayer)
			{
				result = this.GetGroupLayer("项目地界", (IGroupLayer)ipLay);
				return result;
			}
			result = false;
			return result;
		}

		private bool GetGroupLayer(string name, ILayer iGLayer)
		{
			ICompositeLayer compositeLayer = (ICompositeLayer)iGLayer;
			bool result;
			if (compositeLayer == null)
			{
				result = false;
			}
			else
			{
				int count = compositeLayer.Count;
				for (int i = 0; i < count; i++)
				{
					ILayer ipLay = compositeLayer.get_Layer(i);
					if (this.GetLayer(name, ipLay))
					{
						result = true;
						return result;
					}
				}
				result = false;
			}
			return result;
		}

		private void FillLayerBox()
		{
			if (this.SelectLayer == null)
			{
				MessageBox.Show("此图层不存在");
			}
			else
			{
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
				IQueryFilter queryFilter = new QueryFilter();
				IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
				IFeature feature = featureCursor.NextFeature();
				this.comboBox1.Items.Clear();
				string text;
				if (this.radioButton1.Checked)
				{
					text = "项目名称";
				}
				else
				{
					text = "项目单位";
				}
				int num = featureClass.Fields.FindField(text);
				if (num == -1)
				{
					MessageBox.Show("没有找到字段！");
				}
				else
				{
					this.comboBox1.Items.Clear();
					while (feature != null)
					{
						object obj = feature.get_Value(num);
						string text2;
						if (obj == null || Convert.IsDBNull(obj))
						{
							text2 = "";
						}
						else
						{
							text2 = obj.ToString();
						}
						if (!this.comboBox1.Items.Contains(text2) && text2 != "")
						{
							this.comboBox1.Items.Add(text2);
						}
						feature = featureCursor.NextFeature();
					}
					if (this.comboBox1.Items.Count > 0)
					{
						this.comboBox1.SelectedIndex = 0;
					}
				}
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			this.FillLayerBox();
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.SelectLayer == null)
			{
				MessageBox.Show("项目地界层不存在!");
			}
			else
			{
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
				ISpatialFilter spatialFilter = new SpatialFilter();
				string text = this.comboBox1.Text;
				string text2;
				if (!this.checkBox1.Checked)
				{
					if (this.radioButton1.Checked)
					{
						text2 = "项目名称 = ";
					}
					else
					{
						text2 = "项目单位 = ";
					}
					text2 += " '";
					text2 += text;
					text2 += "'";
				}
				else
				{
					if (this.radioButton1.Checked)
					{
						text2 = "项目名称 LIKE ";
					}
					else
					{
						text2 = "项目单位 LIKE ";
					}
					IDataset dataset = this.SelectLayer.FeatureClass as IDataset;
					if (dataset.Workspace.Type == (esriWorkspaceType) 2)
					{
						text2 += " '%";
						text2 += text;
						text2 += "%'";
					}
					else
					{
						text2 += " '*";
						text2 += text;
						text2 += "*'";
					}
				}
				if (this.comboBox1.Text == "")
				{
					text2 = "";
				}
				spatialFilter.WhereClause=text2;
				IFeatureCursor pCursor = featureClass.Search(spatialFilter, false);
				this.m_iApp.SetResult(pCursor, (IFeatureSelection)this.SelectLayer);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
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
			this.radioButton1 = new RadioButton();
			this.radioButton2 = new RadioButton();
			this.button1 = new Button();
			this.button2 = new Button();
			this.comboBox1 = new ComboBox();
			this.checkBox1 = new CheckBox();
			base.SuspendLayout();
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(12, 12);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new Size(71, 16);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "项目名称";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(100, 12);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(71, 16);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "项目单位";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
			this.button1.Location = new System.Drawing.Point(51, 69);
			this.button1.Name = "button1";
			this.button1.Size = new Size(85, 28);
			this.button1.TabIndex = 3;
			this.button1.Text = "查询";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Location = new System.Drawing.Point(174, 69);
			this.button2.Name = "button2";
			this.button2.Size = new Size(85, 28);
			this.button2.TabIndex = 4;
			this.button2.Text = "关闭";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(12, 34);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new Size(279, 20);
			this.comboBox1.TabIndex = 6;
			this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(246, 12);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new Size(72, 16);
			this.checkBox1.TabIndex = 7;
			this.checkBox1.Text = "模糊查询";
			this.checkBox1.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(320, 111);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.radioButton2);
			base.Controls.Add(this.radioButton1);
			base.MaximizeBox = false;
			base.Name = "SimpleQueryByItemUI";
			base.ShowIcon = false;
			this.Text = "项目查询";
			base.Load += new EventHandler(this.SimpleQueryByItemUI_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
