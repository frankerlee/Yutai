using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.PipeConfig;
using Yutai.Plugins.Interfaces;
using QueryResult = Yutai.Pipeline.Analysis.QueryForms.QueryResult;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class BaseQueryUI
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
            this.components = new System.ComponentModel.Container();
            this.Layerbox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.QueryButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.FieldsBox = new System.Windows.Forms.ComboBox();
            this.ValueBox = new System.Windows.Forms.ListBox();
            this.Equalradio = new System.Windows.Forms.RadioButton();
            this.Bigradio = new System.Windows.Forms.RadioButton();
            this.BigeRaido = new System.Windows.Forms.RadioButton();
            this.SmallRadio = new System.Windows.Forms.RadioButton();
            this.SmalelRadio = new System.Windows.Forms.RadioButton();
            this.LikeRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NoEqualRadio = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ValueEdit = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.GeometrySet = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.选择方式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ByEnvelope = new System.Windows.Forms.ToolStripMenuItem();
            this.ByPolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.ByCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.数据选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CrossesSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.WithinSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.Clearbut = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Layerbox
            // 
            this.Layerbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Layerbox.FormattingEnabled = true;
            this.Layerbox.Items.AddRange(new object[] {
            "油田电力点",
            "中压天然气点1"});
            this.Layerbox.Location = new System.Drawing.Point(58, 11);
            this.Layerbox.Name = "Layerbox";
            this.Layerbox.Size = new System.Drawing.Size(162, 20);
            this.Layerbox.TabIndex = 0;
            this.Layerbox.SelectedIndexChanged += new System.EventHandler(this.Layerbox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "查询层";
            // 
            // QueryButton
            // 
            this.QueryButton.Location = new System.Drawing.Point(12, 263);
            this.QueryButton.Name = "QueryButton";
            this.QueryButton.Size = new System.Drawing.Size(75, 23);
            this.QueryButton.TabIndex = 2;
            this.QueryButton.Text = "查询(&Q)";
            this.QueryButton.UseVisualStyleBackColor = true;
            this.QueryButton.Click += new System.EventHandler(this.QueryButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "查询字段";
            // 
            // FieldsBox
            // 
            this.FieldsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FieldsBox.FormattingEnabled = true;
            this.FieldsBox.Items.AddRange(new object[] {
            "查询字段"});
            this.FieldsBox.Location = new System.Drawing.Point(58, 38);
            this.FieldsBox.Name = "FieldsBox";
            this.FieldsBox.Size = new System.Drawing.Size(162, 20);
            this.FieldsBox.TabIndex = 4;
            this.FieldsBox.SelectedIndexChanged += new System.EventHandler(this.FieldsBox_SelectedIndexChanged);
            // 
            // ValueBox
            // 
            this.ValueBox.FormattingEnabled = true;
            this.ValueBox.ItemHeight = 12;
            this.ValueBox.Location = new System.Drawing.Point(7, 37);
            this.ValueBox.Name = "ValueBox";
            this.ValueBox.Size = new System.Drawing.Size(126, 112);
            this.ValueBox.Sorted = true;
            this.ValueBox.TabIndex = 5;
            this.ValueBox.SelectedIndexChanged += new System.EventHandler(this.ValueBox_SelectedIndexChanged);
            // 
            // Equalradio
            // 
            this.Equalradio.AutoSize = true;
            this.Equalradio.Checked = true;
            this.Equalradio.Location = new System.Drawing.Point(5, 19);
            this.Equalradio.Name = "Equalradio";
            this.Equalradio.Size = new System.Drawing.Size(29, 16);
            this.Equalradio.TabIndex = 7;
            this.Equalradio.TabStop = true;
            this.Equalradio.Text = "=";
            this.Equalradio.UseVisualStyleBackColor = true;
            // 
            // Bigradio
            // 
            this.Bigradio.AutoSize = true;
            this.Bigradio.Location = new System.Drawing.Point(5, 55);
            this.Bigradio.Name = "Bigradio";
            this.Bigradio.Size = new System.Drawing.Size(29, 16);
            this.Bigradio.TabIndex = 8;
            this.Bigradio.TabStop = true;
            this.Bigradio.Text = ">";
            this.Bigradio.UseVisualStyleBackColor = true;
            // 
            // BigeRaido
            // 
            this.BigeRaido.AutoSize = true;
            this.BigeRaido.Location = new System.Drawing.Point(5, 73);
            this.BigeRaido.Name = "BigeRaido";
            this.BigeRaido.Size = new System.Drawing.Size(35, 16);
            this.BigeRaido.TabIndex = 9;
            this.BigeRaido.TabStop = true;
            this.BigeRaido.Text = ">=";
            this.BigeRaido.UseVisualStyleBackColor = true;
            // 
            // SmallRadio
            // 
            this.SmallRadio.AutoSize = true;
            this.SmallRadio.Location = new System.Drawing.Point(5, 91);
            this.SmallRadio.Name = "SmallRadio";
            this.SmallRadio.Size = new System.Drawing.Size(29, 16);
            this.SmallRadio.TabIndex = 10;
            this.SmallRadio.TabStop = true;
            this.SmallRadio.Text = "<";
            this.SmallRadio.UseVisualStyleBackColor = true;
            // 
            // SmalelRadio
            // 
            this.SmalelRadio.AutoSize = true;
            this.SmalelRadio.Location = new System.Drawing.Point(5, 109);
            this.SmalelRadio.Name = "SmalelRadio";
            this.SmalelRadio.Size = new System.Drawing.Size(35, 16);
            this.SmalelRadio.TabIndex = 11;
            this.SmalelRadio.TabStop = true;
            this.SmalelRadio.Text = ">=";
            this.SmalelRadio.UseVisualStyleBackColor = true;
            // 
            // LikeRadio
            // 
            this.LikeRadio.AutoSize = true;
            this.LikeRadio.Location = new System.Drawing.Point(5, 127);
            this.LikeRadio.Name = "LikeRadio";
            this.LikeRadio.Size = new System.Drawing.Size(47, 16);
            this.LikeRadio.TabIndex = 12;
            this.LikeRadio.TabStop = true;
            this.LikeRadio.Text = "相似";
            this.LikeRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NoEqualRadio);
            this.groupBox1.Controls.Add(this.LikeRadio);
            this.groupBox1.Controls.Add(this.SmalelRadio);
            this.groupBox1.Controls.Add(this.SmallRadio);
            this.groupBox1.Controls.Add(this.BigeRaido);
            this.groupBox1.Controls.Add(this.Bigradio);
            this.groupBox1.Controls.Add(this.Equalradio);
            this.groupBox1.Location = new System.Drawing.Point(1, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(74, 154);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "关系符";
            // 
            // NoEqualRadio
            // 
            this.NoEqualRadio.AutoSize = true;
            this.NoEqualRadio.Location = new System.Drawing.Point(5, 37);
            this.NoEqualRadio.Name = "NoEqualRadio";
            this.NoEqualRadio.Size = new System.Drawing.Size(35, 16);
            this.NoEqualRadio.TabIndex = 13;
            this.NoEqualRadio.TabStop = true;
            this.NoEqualRadio.Text = "!=";
            this.NoEqualRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ValueEdit);
            this.groupBox2.Controls.Add(this.ValueBox);
            this.groupBox2.Location = new System.Drawing.Point(81, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(139, 154);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "字段值";
            // 
            // ValueEdit
            // 
            this.ValueEdit.Location = new System.Drawing.Point(6, 13);
            this.ValueEdit.Name = "ValueEdit";
            this.ValueEdit.Size = new System.Drawing.Size(127, 21);
            this.ValueEdit.TabIndex = 6;
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(128, 263);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 15;
            this.CloseButton.Text = "关闭(&C)";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // GeometrySet
            // 
            this.GeometrySet.AutoSize = true;
            this.GeometrySet.ContextMenuStrip = this.contextMenuStrip1;
            this.GeometrySet.Location = new System.Drawing.Point(5, 236);
            this.GeometrySet.Name = "GeometrySet";
            this.GeometrySet.Size = new System.Drawing.Size(72, 16);
            this.GeometrySet.TabIndex = 16;
            this.GeometrySet.Text = "空间范围";
            this.GeometrySet.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择方式ToolStripMenuItem,
            this.数据选择ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // 选择方式ToolStripMenuItem
            // 
            this.选择方式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ByEnvelope,
            this.ByPolygon,
            this.ByCircle});
            this.选择方式ToolStripMenuItem.Name = "选择方式ToolStripMenuItem";
            this.选择方式ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.选择方式ToolStripMenuItem.Text = "选择方式";
            // 
            // ByEnvelope
            // 
            this.ByEnvelope.Checked = true;
            this.ByEnvelope.CheckOnClick = true;
            this.ByEnvelope.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ByEnvelope.Name = "ByEnvelope";
            this.ByEnvelope.Size = new System.Drawing.Size(136, 22);
            this.ByEnvelope.Text = "矩形选择";
            this.ByEnvelope.Click += new System.EventHandler(this.ByEnvelope_Click);
            // 
            // ByPolygon
            // 
            this.ByPolygon.CheckOnClick = true;
            this.ByPolygon.Name = "ByPolygon";
            this.ByPolygon.Size = new System.Drawing.Size(136, 22);
            this.ByPolygon.Text = "多边形选择";
            this.ByPolygon.Click += new System.EventHandler(this.ByPolygon_Click);
            // 
            // ByCircle
            // 
            this.ByCircle.CheckOnClick = true;
            this.ByCircle.Name = "ByCircle";
            this.ByCircle.Size = new System.Drawing.Size(136, 22);
            this.ByCircle.Text = "圆形选择";
            this.ByCircle.Click += new System.EventHandler(this.ByCircle_Click);
            // 
            // 数据选择ToolStripMenuItem
            // 
            this.数据选择ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CrossesSelect,
            this.WithinSelect});
            this.数据选择ToolStripMenuItem.Name = "数据选择ToolStripMenuItem";
            this.数据选择ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.数据选择ToolStripMenuItem.Text = "数据选择";
            // 
            // CrossesSelect
            // 
            this.CrossesSelect.Checked = true;
            this.CrossesSelect.CheckOnClick = true;
            this.CrossesSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CrossesSelect.Name = "CrossesSelect";
            this.CrossesSelect.Size = new System.Drawing.Size(100, 22);
            this.CrossesSelect.Text = "相交";
            this.CrossesSelect.Click += new System.EventHandler(this.CrossesSelect_Click);
            // 
            // WithinSelect
            // 
            this.WithinSelect.Name = "WithinSelect";
            this.WithinSelect.Size = new System.Drawing.Size(100, 22);
            this.WithinSelect.Text = "内部";
            this.WithinSelect.Click += new System.EventHandler(this.WithinSelect_Click);
            // 
            // Clearbut
            // 
            this.Clearbut.Location = new System.Drawing.Point(83, 230);
            this.Clearbut.Name = "Clearbut";
            this.Clearbut.Size = new System.Drawing.Size(69, 24);
            this.Clearbut.TabIndex = 17;
            this.Clearbut.Text = "清理屏幕";
            this.Clearbut.UseVisualStyleBackColor = true;
            this.Clearbut.Visible = false;
            this.Clearbut.Click += new System.EventHandler(this.Clearbut_Click);
            // 
            // BaseQueryUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 290);
            this.Controls.Add(this.Clearbut);
            this.Controls.Add(this.GeometrySet);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FieldsBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.QueryButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Layerbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "BaseQueryUI";
            this.ShowInTaskbar = false;
            this.Text = "基本查询";
            this.Load += new System.EventHandler(this.BaseQueryUI_Load);
            this.VisibleChanged += new System.EventHandler(this.BaseQueryUI_VisibleChanged);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.BaseQueryUI_HelpRequested);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	
		private IContainer components = null;
		private IFields myfields;
		private int LayerType;
		private IFeatureLayer SelectLayer;
		private QueryResult resultDlg;
		private string strWinText;
		private ContextMenuStrip contextMenuStrip1;
		private ComboBox Layerbox;
		private Label label1;
		private Button QueryButton;
		private Label label2;
		private ComboBox FieldsBox;
		private ListBox ValueBox;
		private RadioButton Equalradio;
		private RadioButton Bigradio;
		private RadioButton BigeRaido;
		private RadioButton SmallRadio;
		private RadioButton SmalelRadio;
		private RadioButton LikeRadio;
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private Button CloseButton;
		private CheckBox GeometrySet;
		private RadioButton NoEqualRadio;
		private ToolStripMenuItem 选择方式ToolStripMenuItem;
		private ToolStripMenuItem 数据选择ToolStripMenuItem;
		private ToolStripMenuItem ByEnvelope;
		private ToolStripMenuItem ByPolygon;
		private ToolStripMenuItem ByCircle;
		private ToolStripMenuItem CrossesSelect;
		private ToolStripMenuItem WithinSelect;
		private Button Clearbut;
		private TextBox ValueEdit;
    }
}