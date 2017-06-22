
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class ComplexQueryUI : Form
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

		private GroupBox groupBox1;

		private ListBox FieldBox;

		private ListBox ValueBox;

		private RichTextBox SqlBox;

		private RadioButton AndRaio;

		private RadioButton OrRadio;

		private Button AddItem;

		private Button ClearAll;

		private Button QueryButton;

		private Button CloseButton;

		private RadioButton BigRadio;

		private RadioButton SmallRadio;

		private RadioButton BigeRadio;

		private RadioButton SmalleRadio;

		private RadioButton Noequradio;

		private RadioButton Likeradio;

		private RadioButton Equradio;

		private TextBox ValueEdit;

		private GroupBox groupBox2;

		private CheckBox bGeo;

		private TextBox SelectText;

		private ComboBox LayerBox;

		private GroupBox groupBox3;

		private GroupBox groupBox4;

		private GroupBox groupBox5;

		private NumericUpDown GlacisUpDown;

		private Label label1;

		private CheckBox chkBoxTwice;

		public IMapControl3 MapControl;

		public IPipeConfig pPipeCfg;

		public IGeometry newGeometry;

		private IFeatureLayer SelectLayer;

		private IFields myfields;

		private IField myfield;

		private bool bFirst = true;

		public int DrawType = 1;

		private int SelectType;

		public IGeometry m_ipGeo;

		public IAppContext m_context;

		private QueryResult resultDlg;

		public object mainform;

		public IGeometry m_OriginGeo;

		private ISelectionSet m_pSelectionSetForSearch;

		public bool SelectGeometry
		{
			get
			{
				return this.bGeo.Checked;
			}
		}

		public double GlacisNum
		{
			get
			{
				return (double)this.GlacisUpDown.Value;
			}
			set
			{
				this.GlacisUpDown.Value = (decimal)value;
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
			this.groupBox1 = new GroupBox();
			this.LayerBox = new ComboBox();
			this.FieldBox = new ListBox();
			this.ValueBox = new ListBox();
			this.SqlBox = new RichTextBox();
			this.AndRaio = new RadioButton();
			this.OrRadio = new RadioButton();
			this.AddItem = new Button();
			this.ClearAll = new Button();
			this.QueryButton = new Button();
			this.CloseButton = new Button();
			this.BigRadio = new RadioButton();
			this.SmallRadio = new RadioButton();
			this.BigeRadio = new RadioButton();
			this.SmalleRadio = new RadioButton();
			this.Noequradio = new RadioButton();
			this.Likeradio = new RadioButton();
			this.Equradio = new RadioButton();
			this.ValueEdit = new TextBox();
			this.groupBox2 = new GroupBox();
			this.bGeo = new CheckBox();
			this.SelectText = new TextBox();
			this.groupBox3 = new GroupBox();
			this.groupBox4 = new GroupBox();
			this.groupBox5 = new GroupBox();
			this.label1 = new Label();
			this.GlacisUpDown = new NumericUpDown();
			this.chkBoxTwice = new CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((ISupportInitialize)this.GlacisUpDown).BeginInit();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.LayerBox);
			this.groupBox1.Location = new System.Drawing.Point(0, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(175, 45);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "图层列表";
			this.LayerBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(5, 17);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(166, 20);
			this.LayerBox.TabIndex = 0;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.FieldBox.FormattingEnabled = true;
			this.FieldBox.ItemHeight = 12;
			this.FieldBox.Location = new System.Drawing.Point(8, 15);
			this.FieldBox.Name = "FieldBox";
			this.FieldBox.Size = new Size(157, 100);
			this.FieldBox.TabIndex = 2;
			this.FieldBox.SelectedIndexChanged += new EventHandler(this.FieldBox_SelectedIndexChanged);
			this.ValueBox.FormattingEnabled = true;
			this.ValueBox.ItemHeight = 12;
			this.ValueBox.Location = new System.Drawing.Point(340, 37);
			this.ValueBox.Name = "ValueBox";
			this.ValueBox.Size = new Size(113, 124);
			this.ValueBox.Sorted = true;
			this.ValueBox.TabIndex = 3;
			this.ValueBox.MouseDoubleClick += new MouseEventHandler(this.ValueBox_MouseDoubleClick);
			this.ValueBox.SelectedIndexChanged += new EventHandler(this.ValueBox_SelectedIndexChanged);
			this.SqlBox.Location = new System.Drawing.Point(3, 206);
			this.SqlBox.Name = "SqlBox";
			this.SqlBox.Size = new Size(450, 21);
			this.SqlBox.TabIndex = 4;
			this.SqlBox.Text = "";
			this.AndRaio.AutoSize = true;
			this.AndRaio.Location = new System.Drawing.Point(6, 16);
			this.AndRaio.Name = "AndRaio";
			this.AndRaio.Size = new Size(47, 16);
			this.AndRaio.TabIndex = 5;
			this.AndRaio.Text = "并且";
			this.AndRaio.UseVisualStyleBackColor = true;
			this.OrRadio.AutoSize = true;
			this.OrRadio.Checked = true;
			this.OrRadio.Location = new System.Drawing.Point(78, 16);
			this.OrRadio.Name = "OrRadio";
			this.OrRadio.Size = new Size(47, 16);
			this.OrRadio.TabIndex = 6;
			this.OrRadio.TabStop = true;
			this.OrRadio.Text = "或者";
			this.OrRadio.UseVisualStyleBackColor = true;
			this.AddItem.Location = new System.Drawing.Point(462, 6);
			this.AddItem.Name = "AddItem";
			this.AddItem.Size = new Size(72, 24);
			this.AddItem.TabIndex = 7;
			this.AddItem.Text = "添加(&A)";
			this.AddItem.UseVisualStyleBackColor = true;
			this.AddItem.Click += new EventHandler(this.AddItem_Click);
			this.ClearAll.Location = new System.Drawing.Point(462, 37);
			this.ClearAll.Name = "ClearAll";
			this.ClearAll.Size = new Size(72, 24);
			this.ClearAll.TabIndex = 8;
			this.ClearAll.Text = "清空(&C)";
			this.ClearAll.UseVisualStyleBackColor = true;
			this.ClearAll.Click += new EventHandler(this.ClearAll_Click);
			this.QueryButton.Location = new System.Drawing.Point(462, 178);
			this.QueryButton.Name = "QueryButton";
			this.QueryButton.Size = new Size(72, 24);
			this.QueryButton.TabIndex = 9;
			this.QueryButton.Text = "查询(&Q)";
			this.QueryButton.UseVisualStyleBackColor = true;
			this.QueryButton.Click += new EventHandler(this.QueryButton_Click);
			this.CloseButton.DialogResult = DialogResult.Cancel;
			this.CloseButton.Location = new System.Drawing.Point(462, 232);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new Size(72, 24);
			this.CloseButton.TabIndex = 10;
			this.CloseButton.Text = "关闭(&C)\u3000";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new EventHandler(this.CloseButton_Click);
			this.BigRadio.AutoSize = true;
			this.BigRadio.Location = new System.Drawing.Point(51, 15);
			this.BigRadio.Name = "BigRadio";
			this.BigRadio.Size = new Size(29, 16);
			this.BigRadio.TabIndex = 11;
			this.BigRadio.TabStop = true;
			this.BigRadio.Text = ">";
			this.BigRadio.UseVisualStyleBackColor = true;
			this.SmallRadio.AutoSize = true;
			this.SmallRadio.Location = new System.Drawing.Point(96, 15);
			this.SmallRadio.Name = "SmallRadio";
			this.SmallRadio.Size = new Size(29, 16);
			this.SmallRadio.TabIndex = 12;
			this.SmallRadio.TabStop = true;
			this.SmallRadio.Text = "<";
			this.SmallRadio.UseVisualStyleBackColor = true;
			this.BigeRadio.AutoSize = true;
			this.BigeRadio.Location = new System.Drawing.Point(6, 49);
			this.BigeRadio.Name = "BigeRadio";
			this.BigeRadio.Size = new Size(35, 16);
			this.BigeRadio.TabIndex = 13;
			this.BigeRadio.TabStop = true;
			this.BigeRadio.Text = ">=";
			this.BigeRadio.UseVisualStyleBackColor = true;
			this.SmalleRadio.AutoSize = true;
			this.SmalleRadio.Location = new System.Drawing.Point(51, 49);
			this.SmalleRadio.Name = "SmalleRadio";
			this.SmalleRadio.Size = new Size(35, 16);
			this.SmalleRadio.TabIndex = 14;
			this.SmalleRadio.TabStop = true;
			this.SmalleRadio.Text = "<=";
			this.SmalleRadio.UseVisualStyleBackColor = true;
			this.Noequradio.AutoSize = true;
			this.Noequradio.Location = new System.Drawing.Point(96, 50);
			this.Noequradio.Name = "Noequradio";
			this.Noequradio.Size = new Size(35, 16);
			this.Noequradio.TabIndex = 15;
			this.Noequradio.TabStop = true;
			this.Noequradio.Text = "!=";
			this.Noequradio.UseVisualStyleBackColor = true;
			this.Likeradio.AutoSize = true;
			this.Likeradio.Location = new System.Drawing.Point(6, 83);
			this.Likeradio.Name = "Likeradio";
			this.Likeradio.Size = new Size(47, 16);
			this.Likeradio.TabIndex = 16;
			this.Likeradio.TabStop = true;
			this.Likeradio.Text = "相似";
			this.Likeradio.UseVisualStyleBackColor = true;
			this.Equradio.AutoSize = true;
			this.Equradio.Checked = true;
			this.Equradio.Location = new System.Drawing.Point(6, 15);
			this.Equradio.Name = "Equradio";
			this.Equradio.Size = new Size(29, 16);
			this.Equradio.TabIndex = 17;
			this.Equradio.TabStop = true;
			this.Equradio.Text = "=";
			this.Equradio.UseVisualStyleBackColor = true;
			this.ValueEdit.Location = new System.Drawing.Point(340, 9);
			this.ValueEdit.Name = "ValueEdit";
			this.ValueEdit.Size = new Size(113, 21);
			this.ValueEdit.TabIndex = 18;
			this.ValueEdit.TextChanged += new EventHandler(this.ValueEdit_TextChanged);
			this.groupBox2.Controls.Add(this.OrRadio);
			this.groupBox2.Controls.Add(this.AndRaio);
			this.groupBox2.Location = new System.Drawing.Point(183, 124);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(136, 40);
			this.groupBox2.TabIndex = 19;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "关系运算符";
			this.bGeo.AutoSize = true;
			this.bGeo.Location = new System.Drawing.Point(457, 297);
			this.bGeo.Name = "bGeo";
			this.bGeo.Size = new Size(72, 16);
			this.bGeo.TabIndex = 20;
			this.bGeo.Text = "空间范围";
			this.bGeo.UseVisualStyleBackColor = true;
			this.bGeo.CheckedChanged += new EventHandler(this.bGeo_CheckedChanged);
			this.SelectText.Location = new System.Drawing.Point(3, 181);
			this.SelectText.Name = "SelectText";
			this.SelectText.ReadOnly = true;
			this.SelectText.Size = new Size(450, 21);
			this.SelectText.TabIndex = 21;
			this.groupBox3.Controls.Add(this.FieldBox);
			this.groupBox3.Location = new System.Drawing.Point(3, 53);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(172, 120);
			this.groupBox3.TabIndex = 22;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "字段列表";
			this.groupBox4.Controls.Add(this.Equradio);
			this.groupBox4.Controls.Add(this.BigRadio);
			this.groupBox4.Controls.Add(this.SmallRadio);
			this.groupBox4.Controls.Add(this.BigeRadio);
			this.groupBox4.Controls.Add(this.SmalleRadio);
			this.groupBox4.Controls.Add(this.Noequradio);
			this.groupBox4.Controls.Add(this.Likeradio);
			this.groupBox4.Location = new System.Drawing.Point(183, 2);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new Size(134, 106);
			this.groupBox4.TabIndex = 23;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "操作符";
			this.groupBox5.Controls.Add(this.label1);
			this.groupBox5.Controls.Add(this.GlacisUpDown);
			this.groupBox5.Location = new System.Drawing.Point(3, 280);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new Size(450, 43);
			this.groupBox5.TabIndex = 24;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "缓冲区半径";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(131, 22);
			this.label1.Name = "label1";
			this.label1.Size = new Size(17, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "米";
			this.GlacisUpDown.Location = new System.Drawing.Point(9, 17);
			int[] array = new int[4];
			array[0] = 1000;
			this.GlacisUpDown.Maximum = new decimal(array);
			this.GlacisUpDown.Name = "GlacisUpDown";
			this.GlacisUpDown.Size = new Size(120, 21);
			this.GlacisUpDown.TabIndex = 1;
			this.GlacisUpDown.ValueChanged += new EventHandler(this.GlacisUpDown_ValueChanged);
			this.chkBoxTwice.AutoSize = true;
			this.chkBoxTwice.Location = new System.Drawing.Point(4, 240);
			this.chkBoxTwice.Name = "chkBoxTwice";
			this.chkBoxTwice.Size = new Size(144, 16);
			this.chkBoxTwice.TabIndex = 25;
			this.chkBoxTwice.Text = "在查询结果中进行查询";
			this.chkBoxTwice.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(539, 323);
			base.Controls.Add(this.chkBoxTwice);
			base.Controls.Add(this.groupBox5);
			base.Controls.Add(this.groupBox4);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.SelectText);
			base.Controls.Add(this.bGeo);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.ValueEdit);
			base.Controls.Add(this.CloseButton);
			base.Controls.Add(this.QueryButton);
			base.Controls.Add(this.ClearAll);
			base.Controls.Add(this.AddItem);
			base.Controls.Add(this.SqlBox);
			base.Controls.Add(this.ValueBox);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "ComplexQueryUI";
			base.ShowInTaskbar = false;
			this.Text = "复合查询";
			base.Load += new EventHandler(this.ComplexQueryUI_Load);
			base.VisibleChanged += new EventHandler(this.ComplexQueryUI_VisibleChanged);
			base.HelpRequested += new HelpEventHandler(this.ComplexQueryUI_HelpRequested);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			((ISupportInitialize)this.GlacisUpDown).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void MakeSelectionSetForSearch(IFeatureLayer pFeaLay)
		{
			if (this.chkBoxTwice.Checked && this.m_pSelectionSetForSearch != null)
			{
				IFeatureSelection featureSelection = (IFeatureSelection)pFeaLay;
				this.m_pSelectionSetForSearch = featureSelection.SelectionSet;
			}
			else if (pFeaLay == null)
			{
				MessageBox.Show("FeatureClass为空");
			}
			else
			{
				IFeatureClass featureClass = pFeaLay.FeatureClass;
				IQueryFilter queryFilter = new QueryFilter();
				ISelectionSet pSelectionSetForSearch = featureClass.Select(queryFilter, (esriSelectionType) 1, (esriSelectionOption) 1, null);
				this.m_pSelectionSetForSearch = pSelectionSetForSearch;
			}
		}

		public ComplexQueryUI()
		{
			this.InitializeComponent();
		}

		~ComplexQueryUI()
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
				iFLayer.Name.ToString();
				ComplexQueryUI.LayerboxItem layerboxItem = new ComplexQueryUI.LayerboxItem();
				layerboxItem.m_pPipeLayer = iFLayer;
				this.LayerBox.Items.Add(layerboxItem);
			}
		}

		private void Fill()
		{
			this.LayerBox.Items.Clear();
			int layerCount = this.MapControl.LayerCount;
			if (layerCount >= 1)
			{
				for (int i = 0; i < layerCount; i++)
				{
					ILayer ipLay = m_context.FocusMap.get_Layer(i);
					this.AddLayer(ipLay);
				}
				this.LayerBox.SelectedIndex = 0;
			}
		}

		private void FillField()
		{
			this.LayerBox.SelectedItem.ToString();
			if (this.MapControl != null)
			{
				this.SelectLayer = ((ComplexQueryUI.LayerboxItem)this.LayerBox.SelectedItem).m_pPipeLayer;
				if (this.SelectLayer != null)
				{
					this.myfields = this.SelectLayer.FeatureClass.Fields;
					this.FieldBox.Items.Clear();
					for (int i = 0; i < this.myfields.FieldCount; i++)
					{
						IField field = this.myfields.get_Field(i);
						string name = field.Name;
						if (field.Type != (esriFieldType) 6 && field.Type != (esriFieldType) 7 && !(name.ToUpper() == "ENABLED") && !(name.ToUpper() == "SHAPE.LEN") && !(name.ToUpper() == "SHAPE.AREA"))
						{
							this.FieldBox.Items.Add(name);
						}
					}
					if (this.FieldBox.Items.Count > 0)
					{
						this.FieldBox.SelectedIndex = 0;
					}
					else
					{
						this.ValueEdit.Text = "";
						this.ValueBox.Items.Clear();
					}
				}
			}
		}

		private void FillValue()
		{
			if (this.myfields != null)
			{
				if (this.FieldBox.Items.Count >= 1)
				{
					int num = this.myfields.FindField(this.FieldBox.SelectedItem.ToString());
					this.myfield = this.myfields.get_Field(num);
					if (this.myfield.Type == (esriFieldType) 4)
					{
						this.BigeRadio.Enabled = false;
						this.BigRadio.Enabled = false;
						this.SmallRadio.Enabled = false;
						this.SmalleRadio.Enabled = false;
						this.Likeradio.Enabled = true;
					}
					else
					{
						this.BigeRadio.Enabled = true;
						this.BigRadio.Enabled = true;
						this.SmalleRadio.Enabled = true;
						this.SmallRadio.Enabled = true;
						this.Likeradio.Enabled = false;
					}
					this.Equradio.Checked = true;
					IFeatureClass featureClass = this.SelectLayer.FeatureClass;
					IQueryFilter queryFilter = new QueryFilter();
					IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
					IFeature feature = featureCursor.NextFeature();
					this.ValueBox.Items.Clear();
					while (feature != null)
					{
						object obj = feature.get_Value(num);
						string text;
						if (obj is DBNull)
						{
							text = "NULL";
						}
						else if (this.myfield.Type == (esriFieldType) 5)
						{
							text = Convert.ToDateTime(obj).ToShortDateString();
						}
						else
						{
							text = obj.ToString();
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
		}

		private void ComplexQueryUI_Load(object sender, EventArgs e)
		{
			this.Fill();
			this.FillField();
			this.FillValue();
		}

		public void AutoFlash()
		{
			this.Fill();
			this.FillField();
			this.FillValue();
		}

		private void LayerBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.FillField();
			this.SelectText.Text = "";
			this.SqlBox.Text = "";
			this.bFirst = true;
		}

		private void FieldBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.FillValue();
			this.ValueEdit.Text = "";
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			if (this.SelectLayer != null)
			{
				IFeatureSelection featureSelection = (IFeatureSelection)this.SelectLayer;
				featureSelection.Clear();
				featureSelection.SelectionSet.Refresh();
				IActiveView activeView = m_context.ActiveView;
				this.SqlBox.Text = "";
				this.SelectText.Text = "";
				this.bFirst = true;
				this.m_ipGeo = null;
				this.m_OriginGeo = null;
				this.GlacisUpDown.Value = 0m;
				activeView.Refresh();
			}
			this.bGeo.Checked = false;
			base.Close();
		}

		private void ValueBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValueEdit.Text = this.ValueBox.SelectedItem.ToString();
		}

		private void AddItem_Click(object sender, EventArgs e)
		{
			string text = "*";
			IDataset dataset = this.SelectLayer.FeatureClass as IDataset;
			if (dataset.Workspace.Type == (esriWorkspaceType) 2)
			{
				text = "%";
			}
			string text2 = this.SqlBox.Text;
			if (this.bFirst)
			{
				this.bFirst = false;
				this.SelectText.Text = "select * from ";
				TextBox selectText = this.SelectText;
				TextBox expr_73 = selectText;
				expr_73.Text += this.LayerBox.SelectedItem.ToString();
				TextBox selectText2 = this.SelectText;
				TextBox expr_9E = selectText2;
				expr_9E.Text += " where ";
			}
			else if (this.AndRaio.Checked)
			{
				text2 += " and ";
			}
			else
			{
				text2 += " or ";
			}
			string text3 = this.FieldBox.SelectedItem.ToString();
			string text4 = this.ValueEdit.Text;
			if (text4 == "NULL")
			{
				if (this.Equradio.Checked)
				{
					text2 += text3;
					text2 += " IS NULL";
				}
				if (this.Noequradio.Checked)
				{
					text2 = text2 + "NOT(" + text3 + " IS NULL)";
				}
				if (this.Likeradio.Checked)
				{
					text2 += text3;
					text2 += " LIKE NULL";
				}
			}
			else
			{
				text2 += text3;
				if (this.Equradio.Checked)
				{
					text2 += "=";
				}
				if (this.Noequradio.Checked)
				{
					text2 += "<>";
				}
				if (this.SmallRadio.Checked)
				{
					text2 += "<";
				}
				if (this.SmalleRadio.Checked)
				{
					text2 += "<=";
				}
				if (this.BigRadio.Checked)
				{
					text2 += ">";
				}
				if (this.BigeRadio.Checked)
				{
					text2 += ">=";
				}
				if (this.Likeradio.Checked)
				{
					text2 += " like ";
				}
				if (text4 == "空字段值")
				{
					text4 = "";
				}
				if (this.myfield.Type == (esriFieldType) 4)
				{
					if (this.Likeradio.Checked)
					{
						text2 = text2 + "'" + text;
						text2 += text4;
						text2 = text2 + text + "'";
					}
					else
					{
						text2 += "'";
						text2 += text4;
						text2 += "'";
					}
				}
				else if (this.myfield.Type == (esriFieldType) 5)
				{
					if (this.SelectLayer.DataSourceType == "SDE Feature Class")
					{
						text2 += "TO_DATE('";
						text2 += text4;
						text2 += "','YYYY-MM-DD')";
					}
					if (this.SelectLayer.DataSourceType == "Personal Geodatabase Feature Class")
					{
						text2 += "#";
						text2 += text4;
						text2 += "#";
					}
				}
				else
				{
					text2 += text4;
				}
			}
			this.SqlBox.Text = text2;
		}

		private void ClearAll_Click(object sender, EventArgs e)
		{
			this.SqlBox.Clear();
			this.SelectText.Clear();
			this.bFirst = true;
			this.bGeo.Checked = false;
			this.m_ipGeo = null;
		}

		private void ComplexQueryUI_VisibleChanged(object sender, EventArgs e)
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

		private void QueryButton_Click(object sender, EventArgs e)
		{
			ISpatialFilter spatialFilter = new SpatialFilter();
			IFeatureCursor featureCursor = null;
			if (!(this.SqlBox.Text == "") || MessageBox.Show("末指定属性条件,是否查询?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				spatialFilter.WhereClause=this.SqlBox.Text;
				if (this.bGeo.Checked && this.m_ipGeo != null)
				{
					if (this.GlacisUpDown.Value > 0m)
					{
						ITopologicalOperator topologicalOperator = (ITopologicalOperator)this.m_OriginGeo;
						IGeometry ipGeo = topologicalOperator.Buffer((double)this.GlacisUpDown.Value);
						this.m_ipGeo = ipGeo;
						spatialFilter.Geometry=(this.m_ipGeo);
					}
					else
					{
						spatialFilter.Geometry=(this.m_OriginGeo);
					}
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
					this.MakeSelectionSetForSearch(this.SelectLayer);
					ICursor cursor = featureCursor as ICursor;
					this.m_pSelectionSetForSearch.Search(spatialFilter, false, out cursor);
					if (cursor is IFeatureCursor)
					{
						featureCursor = (cursor as IFeatureCursor);
					}
				}
				catch (Exception)
				{
					MessageBox.Show("查询值有误,请检查!");
					return;
				}
				this.m_iApp.SetResult(featureCursor, (IFeatureSelection)this.SelectLayer);
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			try
			{
                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
                IFeatureSelection featureSelection = (IFeatureSelection)this.SelectLayer;
				featureSelection.Clear();
				featureSelection.SelectionSet.Refresh();
				IActiveView activeView = m_context.ActiveView;
				activeView.Refresh();
				this.SqlBox.Text = "";
				this.SelectText.Text = "";
				this.bGeo.Checked = false;
				this.bFirst = true;
				this.m_ipGeo = null;
				base.OnClosed(e);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void ValueBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.AddItem_Click(null, null);
		}

		private void GlacisUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (this.bGeo.Checked && this.m_OriginGeo != null)
			{
				if (this.GlacisUpDown.Value > 0m)
				{
					try
					{
						ITopologicalOperator topologicalOperator = (ITopologicalOperator)this.m_OriginGeo;
						IGeometry geometry = topologicalOperator.Buffer((double)this.GlacisUpDown.Value);
						if (geometry != null)
						{
							this.m_ipGeo = geometry;
						}
						else
						{
							MessageBox.Show("复杂多边形,创建缓冲区失败!");
							this.m_ipGeo = this.m_OriginGeo;
						}
						goto IL_CF;
					}
					catch (Exception)
					{
						MessageBox.Show("复杂多边形,创建缓冲区失败!");
						this.GlacisUpDown.Value = 0m;
						this.m_ipGeo = this.m_OriginGeo;
						goto IL_CF;
					}
				}
				this.m_ipGeo = this.m_OriginGeo;
				IL_CF:
				IActiveView activeView = m_context.ActiveView;
				activeView.Refresh();
			}
		}

		private void bGeo_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.bGeo.Checked)
			{
				this.m_ipGeo = null;
				this.m_OriginGeo = null;
				this.MapControl.Refresh((esriViewDrawPhase) 32, null, null);
			}
		}

		private void ValueEdit_TextChanged(object sender, EventArgs e)
		{
		}

		private void ComplexQueryUI_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "复合查询";
			HelpNavigator command = HelpNavigator.KeywordIndex;
			Help.ShowHelp(this, url, command, parameter);
		}
	}
}
