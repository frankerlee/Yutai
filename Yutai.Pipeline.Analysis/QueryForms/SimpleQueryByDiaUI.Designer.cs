
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
	    partial class SimpleQueryByDiaUI
    {
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
			this.groupBox1 = new GroupBox();
			this.radioButton2 = new RadioButton();
			this.ValueBox3 = new ComboBox();
			this.label3 = new Label();
			this.OperateBox2 = new ComboBox();
			this.radioButton1 = new RadioButton();
			this.ValueBox2 = new ComboBox();
			this.label2 = new Label();
			this.ValueBox1 = new ComboBox();
			this.label1 = new Label();
			this.OperateBox = new ComboBox();
			this.QueryBut = new Button();
			this.CloseBut = new Button();
			this.lable = new Label();
			this.LayerBox = new ComboBox();
			this.GeometrySet = new CheckBox();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.MenuItemSelectType = new ToolStripMenuItem();
			this.ByEnvelope = new ToolStripMenuItem();
			this.ByPolygon = new ToolStripMenuItem();
			this.ByCircle = new ToolStripMenuItem();
			this.MenuItemDataSelectType = new ToolStripMenuItem();
			this.CrossesSelect = new ToolStripMenuItem();
			this.WithinSelect = new ToolStripMenuItem();
			this.groupBox1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.ValueBox3);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.OperateBox2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Controls.Add(this.ValueBox2);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.ValueBox1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.OperateBox);
			this.groupBox1.Location = new System.Drawing.Point(-1, 43);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(388, 85);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "查询条件";
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(6, 53);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(14, 13);
			this.radioButton2.TabIndex = 9;
			this.radioButton2.TabStop = true;
			this.radioButton2.UseVisualStyleBackColor = true;
			this.ValueBox3.FormattingEnabled = true;
			this.ValueBox3.Location = new System.Drawing.Point(171, 51);
			this.ValueBox3.Name = "ValueBox3";
			this.ValueBox3.Size = new Size(81, 20);
			this.ValueBox3.Sorted = true;
			this.ValueBox3.TabIndex = 8;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(22, 55);
			this.label3.Name = "label3";
			this.label3.Size = new Size(65, 12);
			this.label3.TabIndex = 7;
			this.label3.Text = "沟截面宽高";
			this.OperateBox2.DropDownStyle = ComboBoxStyle.DropDownList;
			this.OperateBox2.FormattingEnabled = true;
			this.OperateBox2.Items.AddRange(new object[]
			{
				"等于",
				"不等于"
			});
			this.OperateBox2.Location = new System.Drawing.Point(89, 51);
			this.OperateBox2.Name = "OperateBox2";
			this.OperateBox2.Size = new Size(76, 20);
			this.OperateBox2.TabIndex = 6;
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(6, 24);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new Size(14, 13);
			this.radioButton1.TabIndex = 5;
			this.radioButton1.TabStop = true;
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.ValueBox2.FormattingEnabled = true;
			this.ValueBox2.Location = new System.Drawing.Point(301, 23);
			this.ValueBox2.Name = "ValueBox2";
			this.ValueBox2.Size = new Size(81, 20);
			this.ValueBox2.Sorted = true;
			this.ValueBox2.TabIndex = 4;
			this.ValueBox2.Visible = false;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(267, 26);
			this.label2.Name = "label2";
			this.label2.Size = new Size(17, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "到";
			this.label2.Visible = false;
			this.ValueBox1.FormattingEnabled = true;
			this.ValueBox1.Location = new System.Drawing.Point(171, 22);
			this.ValueBox1.Name = "ValueBox1";
			this.ValueBox1.Size = new Size(81, 20);
			this.ValueBox1.Sorted = true;
			this.ValueBox1.TabIndex = 2;
			this.ValueBox1.SelectedIndexChanged += new EventHandler(this.ValueBox1_SelectedIndexChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(31, 26);
			this.label1.Name = "label1";
			this.label1.Size = new Size(29, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "管径";
			this.OperateBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.OperateBox.FormattingEnabled = true;
			this.OperateBox.Items.AddRange(new object[]
			{
				"大于",
				"大于等于",
				"等于",
				"不等于",
				"小于",
				"小于等于",
				"介于"
			});
			this.OperateBox.Location = new System.Drawing.Point(89, 22);
			this.OperateBox.Name = "OperateBox";
			this.OperateBox.Size = new Size(76, 20);
			this.OperateBox.TabIndex = 0;
			this.OperateBox.SelectedIndexChanged += new EventHandler(this.OperateBox_SelectedIndexChanged);
			this.QueryBut.Location = new System.Drawing.Point(320, 2);
			this.QueryBut.Name = "QueryBut";
			this.QueryBut.Size = new Size(67, 23);
			this.QueryBut.TabIndex = 2;
			this.QueryBut.Text = "查询(&Q)";
			this.QueryBut.UseVisualStyleBackColor = true;
			this.QueryBut.Click += new EventHandler(this.QueryBut_Click);
			this.CloseBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseBut.Location = new System.Drawing.Point(320, 27);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(67, 20);
			this.CloseBut.TabIndex = 3;
			this.CloseBut.Text = "关闭(&C)";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(4, 17);
			this.lable.Name = "lable";
			this.lable.Size = new Size(41, 12);
			this.lable.TabIndex = 4;
			this.lable.Text = "管线层";
			this.LayerBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(53, 14);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 5;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.GeometrySet.AutoSize = true;
			this.GeometrySet.Location = new System.Drawing.Point(210, 18);
			this.GeometrySet.Name = "GeometrySet";
			this.GeometrySet.Size = new Size(72, 16);
			this.GeometrySet.TabIndex = 6;
			this.GeometrySet.Text = "空间范围";
			this.GeometrySet.UseVisualStyleBackColor = true;
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.MenuItemSelectType,
				this.MenuItemDataSelectType
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(123, 48);
			this.MenuItemSelectType.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ByEnvelope,
				this.ByPolygon,
				this.ByCircle
			});
			this.MenuItemSelectType.Name = "MenuItemSelectType";
			this.MenuItemSelectType.Size = new Size(122, 22);
			this.MenuItemSelectType.Text = "选择方式";
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
			this.MenuItemDataSelectType.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.CrossesSelect,
				this.WithinSelect
			});
			this.MenuItemDataSelectType.Name = "MenuItemDataSelectType";
			this.MenuItemDataSelectType.Size = new Size(122, 22);
			this.MenuItemDataSelectType.Text = "数据选择";
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
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(388, 130);
			base.Controls.Add(this.GeometrySet);
			base.Controls.Add(this.LayerBox);
			base.Controls.Add(this.lable);
			base.Controls.Add(this.CloseBut);
			base.Controls.Add(this.QueryBut);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "SimpleQueryByDiaUI";
			base.ShowInTaskbar = false;
			this.Text = "管径查询";
			base.Load += new EventHandler(this.SimpleQueryByDiaUI_Load);
			base.VisibleChanged += new EventHandler(this.SimpleQueryByDiaUI_VisibleChanged);
			base.FormClosed += new FormClosedEventHandler(this.SimpleQueryByDiaUI_FormClosed);
			base.HelpRequested += new HelpEventHandler(this.SimpleQueryByDiaUI_HelpRequested);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	
		private IContainer components = null;
		private IFeatureLayer SelectLayer;
		private IFields myfields;
		private string strGJ;
		private string strKG;
		private IField myfieldGJ;
		private IField myfieldKG;
		private QueryResult resultDlg;
		private ContextMenuStrip contextMenuStrip1;
		private GroupBox groupBox1;
		private Button QueryBut;
		private Button CloseBut;
		private Label lable;
		private ComboBox LayerBox;
		private Label label2;
		private ComboBox ValueBox1;
		private Label label1;
		private ComboBox OperateBox;
		private ComboBox ValueBox2;
		private RadioButton radioButton2;
		private ComboBox ValueBox3;
		private Label label3;
		private ComboBox OperateBox2;
		private RadioButton radioButton1;
		private CheckBox GeometrySet;
		private ToolStripMenuItem MenuItemSelectType;
		private ToolStripMenuItem ByEnvelope;
		private ToolStripMenuItem ByPolygon;
		private ToolStripMenuItem ByCircle;
		private ToolStripMenuItem MenuItemDataSelectType;
		private ToolStripMenuItem CrossesSelect;
		private ToolStripMenuItem WithinSelect;
    }
}