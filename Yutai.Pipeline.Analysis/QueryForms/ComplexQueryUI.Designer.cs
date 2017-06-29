
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
	    partial class ComplexQueryUI
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LayerBox = new System.Windows.Forms.ComboBox();
            this.FieldBox = new System.Windows.Forms.ListBox();
            this.ValueBox = new System.Windows.Forms.ListBox();
            this.SqlBox = new System.Windows.Forms.RichTextBox();
            this.AndRaio = new System.Windows.Forms.RadioButton();
            this.OrRadio = new System.Windows.Forms.RadioButton();
            this.AddItem = new System.Windows.Forms.Button();
            this.ClearAll = new System.Windows.Forms.Button();
            this.QueryButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.BigRadio = new System.Windows.Forms.RadioButton();
            this.SmallRadio = new System.Windows.Forms.RadioButton();
            this.BigeRadio = new System.Windows.Forms.RadioButton();
            this.SmalleRadio = new System.Windows.Forms.RadioButton();
            this.Noequradio = new System.Windows.Forms.RadioButton();
            this.Likeradio = new System.Windows.Forms.RadioButton();
            this.Equradio = new System.Windows.Forms.RadioButton();
            this.ValueEdit = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bGeo = new System.Windows.Forms.CheckBox();
            this.SelectText = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GlacisUpDown = new System.Windows.Forms.NumericUpDown();
            this.chkBoxTwice = new System.Windows.Forms.CheckBox();
            this.BtnGetUniqueValue = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GlacisUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LayerBox);
            this.groupBox1.Location = new System.Drawing.Point(0, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 45);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图层列表";
            // 
            // LayerBox
            // 
            this.LayerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayerBox.FormattingEnabled = true;
            this.LayerBox.Location = new System.Drawing.Point(5, 17);
            this.LayerBox.Name = "LayerBox";
            this.LayerBox.Size = new System.Drawing.Size(166, 20);
            this.LayerBox.TabIndex = 0;
            this.LayerBox.SelectedIndexChanged += new System.EventHandler(this.LayerBox_SelectedIndexChanged);
            // 
            // FieldBox
            // 
            this.FieldBox.FormattingEnabled = true;
            this.FieldBox.ItemHeight = 12;
            this.FieldBox.Location = new System.Drawing.Point(8, 15);
            this.FieldBox.Name = "FieldBox";
            this.FieldBox.Size = new System.Drawing.Size(157, 100);
            this.FieldBox.TabIndex = 2;
            this.FieldBox.SelectedIndexChanged += new System.EventHandler(this.FieldBox_SelectedIndexChanged);
            // 
            // ValueBox
            // 
            this.ValueBox.FormattingEnabled = true;
            this.ValueBox.ItemHeight = 12;
            this.ValueBox.Location = new System.Drawing.Point(340, 37);
            this.ValueBox.Name = "ValueBox";
            this.ValueBox.Size = new System.Drawing.Size(113, 100);
            this.ValueBox.Sorted = true;
            this.ValueBox.TabIndex = 3;
            this.ValueBox.SelectedIndexChanged += new System.EventHandler(this.ValueBox_SelectedIndexChanged);
            this.ValueBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ValueBox_MouseDoubleClick);
            // 
            // SqlBox
            // 
            this.SqlBox.Location = new System.Drawing.Point(3, 206);
            this.SqlBox.Name = "SqlBox";
            this.SqlBox.Size = new System.Drawing.Size(450, 21);
            this.SqlBox.TabIndex = 4;
            this.SqlBox.Text = "";
            // 
            // AndRaio
            // 
            this.AndRaio.AutoSize = true;
            this.AndRaio.Location = new System.Drawing.Point(6, 16);
            this.AndRaio.Name = "AndRaio";
            this.AndRaio.Size = new System.Drawing.Size(47, 16);
            this.AndRaio.TabIndex = 5;
            this.AndRaio.Text = "并且";
            this.AndRaio.UseVisualStyleBackColor = true;
            // 
            // OrRadio
            // 
            this.OrRadio.AutoSize = true;
            this.OrRadio.Checked = true;
            this.OrRadio.Location = new System.Drawing.Point(78, 16);
            this.OrRadio.Name = "OrRadio";
            this.OrRadio.Size = new System.Drawing.Size(47, 16);
            this.OrRadio.TabIndex = 6;
            this.OrRadio.TabStop = true;
            this.OrRadio.Text = "或者";
            this.OrRadio.UseVisualStyleBackColor = true;
            // 
            // AddItem
            // 
            this.AddItem.Location = new System.Drawing.Point(462, 6);
            this.AddItem.Name = "AddItem";
            this.AddItem.Size = new System.Drawing.Size(72, 24);
            this.AddItem.TabIndex = 7;
            this.AddItem.Text = "添加(&A)";
            this.AddItem.UseVisualStyleBackColor = true;
            this.AddItem.Click += new System.EventHandler(this.AddItem_Click);
            // 
            // ClearAll
            // 
            this.ClearAll.Location = new System.Drawing.Point(462, 37);
            this.ClearAll.Name = "ClearAll";
            this.ClearAll.Size = new System.Drawing.Size(72, 24);
            this.ClearAll.TabIndex = 8;
            this.ClearAll.Text = "清空(&C)";
            this.ClearAll.UseVisualStyleBackColor = true;
            this.ClearAll.Click += new System.EventHandler(this.ClearAll_Click);
            // 
            // QueryButton
            // 
            this.QueryButton.Location = new System.Drawing.Point(462, 178);
            this.QueryButton.Name = "QueryButton";
            this.QueryButton.Size = new System.Drawing.Size(72, 24);
            this.QueryButton.TabIndex = 9;
            this.QueryButton.Text = "查询(&Q)";
            this.QueryButton.UseVisualStyleBackColor = true;
            this.QueryButton.Click += new System.EventHandler(this.QueryButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(462, 232);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(72, 24);
            this.CloseButton.TabIndex = 10;
            this.CloseButton.Text = "关闭(&C)　";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // BigRadio
            // 
            this.BigRadio.AutoSize = true;
            this.BigRadio.Location = new System.Drawing.Point(51, 15);
            this.BigRadio.Name = "BigRadio";
            this.BigRadio.Size = new System.Drawing.Size(29, 16);
            this.BigRadio.TabIndex = 11;
            this.BigRadio.TabStop = true;
            this.BigRadio.Text = ">";
            this.BigRadio.UseVisualStyleBackColor = true;
            // 
            // SmallRadio
            // 
            this.SmallRadio.AutoSize = true;
            this.SmallRadio.Location = new System.Drawing.Point(96, 15);
            this.SmallRadio.Name = "SmallRadio";
            this.SmallRadio.Size = new System.Drawing.Size(29, 16);
            this.SmallRadio.TabIndex = 12;
            this.SmallRadio.TabStop = true;
            this.SmallRadio.Text = "<";
            this.SmallRadio.UseVisualStyleBackColor = true;
            // 
            // BigeRadio
            // 
            this.BigeRadio.AutoSize = true;
            this.BigeRadio.Location = new System.Drawing.Point(6, 49);
            this.BigeRadio.Name = "BigeRadio";
            this.BigeRadio.Size = new System.Drawing.Size(35, 16);
            this.BigeRadio.TabIndex = 13;
            this.BigeRadio.TabStop = true;
            this.BigeRadio.Text = ">=";
            this.BigeRadio.UseVisualStyleBackColor = true;
            // 
            // SmalleRadio
            // 
            this.SmalleRadio.AutoSize = true;
            this.SmalleRadio.Location = new System.Drawing.Point(51, 49);
            this.SmalleRadio.Name = "SmalleRadio";
            this.SmalleRadio.Size = new System.Drawing.Size(35, 16);
            this.SmalleRadio.TabIndex = 14;
            this.SmalleRadio.TabStop = true;
            this.SmalleRadio.Text = "<=";
            this.SmalleRadio.UseVisualStyleBackColor = true;
            // 
            // Noequradio
            // 
            this.Noequradio.AutoSize = true;
            this.Noequradio.Location = new System.Drawing.Point(96, 50);
            this.Noequradio.Name = "Noequradio";
            this.Noequradio.Size = new System.Drawing.Size(35, 16);
            this.Noequradio.TabIndex = 15;
            this.Noequradio.TabStop = true;
            this.Noequradio.Text = "!=";
            this.Noequradio.UseVisualStyleBackColor = true;
            // 
            // Likeradio
            // 
            this.Likeradio.AutoSize = true;
            this.Likeradio.Location = new System.Drawing.Point(6, 83);
            this.Likeradio.Name = "Likeradio";
            this.Likeradio.Size = new System.Drawing.Size(47, 16);
            this.Likeradio.TabIndex = 16;
            this.Likeradio.TabStop = true;
            this.Likeradio.Text = "相似";
            this.Likeradio.UseVisualStyleBackColor = true;
            // 
            // Equradio
            // 
            this.Equradio.AutoSize = true;
            this.Equradio.Checked = true;
            this.Equradio.Location = new System.Drawing.Point(6, 15);
            this.Equradio.Name = "Equradio";
            this.Equradio.Size = new System.Drawing.Size(29, 16);
            this.Equradio.TabIndex = 17;
            this.Equradio.TabStop = true;
            this.Equradio.Text = "=";
            this.Equradio.UseVisualStyleBackColor = true;
            // 
            // ValueEdit
            // 
            this.ValueEdit.Location = new System.Drawing.Point(340, 9);
            this.ValueEdit.Name = "ValueEdit";
            this.ValueEdit.Size = new System.Drawing.Size(113, 21);
            this.ValueEdit.TabIndex = 18;
            this.ValueEdit.TextChanged += new System.EventHandler(this.ValueEdit_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OrRadio);
            this.groupBox2.Controls.Add(this.AndRaio);
            this.groupBox2.Location = new System.Drawing.Point(183, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(136, 40);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "关系运算符";
            // 
            // bGeo
            // 
            this.bGeo.AutoSize = true;
            this.bGeo.Location = new System.Drawing.Point(457, 297);
            this.bGeo.Name = "bGeo";
            this.bGeo.Size = new System.Drawing.Size(72, 16);
            this.bGeo.TabIndex = 20;
            this.bGeo.Text = "空间范围";
            this.bGeo.UseVisualStyleBackColor = true;
            this.bGeo.CheckedChanged += new System.EventHandler(this.bGeo_CheckedChanged);
            // 
            // SelectText
            // 
            this.SelectText.Location = new System.Drawing.Point(3, 181);
            this.SelectText.Name = "SelectText";
            this.SelectText.ReadOnly = true;
            this.SelectText.Size = new System.Drawing.Size(450, 21);
            this.SelectText.TabIndex = 21;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.FieldBox);
            this.groupBox3.Location = new System.Drawing.Point(3, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(172, 120);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "字段列表";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Equradio);
            this.groupBox4.Controls.Add(this.BigRadio);
            this.groupBox4.Controls.Add(this.SmallRadio);
            this.groupBox4.Controls.Add(this.BigeRadio);
            this.groupBox4.Controls.Add(this.SmalleRadio);
            this.groupBox4.Controls.Add(this.Noequradio);
            this.groupBox4.Controls.Add(this.Likeradio);
            this.groupBox4.Location = new System.Drawing.Point(183, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(134, 106);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "操作符";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.GlacisUpDown);
            this.groupBox5.Location = new System.Drawing.Point(3, 280);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(450, 43);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "缓冲区半径";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "米";
            // 
            // GlacisUpDown
            // 
            this.GlacisUpDown.Location = new System.Drawing.Point(9, 17);
            this.GlacisUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.GlacisUpDown.Name = "GlacisUpDown";
            this.GlacisUpDown.Size = new System.Drawing.Size(120, 21);
            this.GlacisUpDown.TabIndex = 1;
            this.GlacisUpDown.ValueChanged += new System.EventHandler(this.GlacisUpDown_ValueChanged);
            // 
            // chkBoxTwice
            // 
            this.chkBoxTwice.AutoSize = true;
            this.chkBoxTwice.Location = new System.Drawing.Point(4, 240);
            this.chkBoxTwice.Name = "chkBoxTwice";
            this.chkBoxTwice.Size = new System.Drawing.Size(144, 16);
            this.chkBoxTwice.TabIndex = 25;
            this.chkBoxTwice.Text = "在查询结果中进行查询";
            this.chkBoxTwice.UseVisualStyleBackColor = true;
            // 
            // BtnGetUniqueValue
            // 
            this.BtnGetUniqueValue.Location = new System.Drawing.Point(340, 140);
            this.BtnGetUniqueValue.Name = "BtnGetUniqueValue";
            this.BtnGetUniqueValue.Size = new System.Drawing.Size(113, 23);
            this.BtnGetUniqueValue.TabIndex = 26;
            this.BtnGetUniqueValue.Text = "获取唯一值";
            this.BtnGetUniqueValue.UseVisualStyleBackColor = true;
            this.BtnGetUniqueValue.Click += new System.EventHandler(this.BtnGetUniqueValue_Click);
            // 
            // ComplexQueryUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 323);
            this.Controls.Add(this.BtnGetUniqueValue);
            this.Controls.Add(this.chkBoxTwice);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.SelectText);
            this.Controls.Add(this.bGeo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ValueEdit);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.QueryButton);
            this.Controls.Add(this.ClearAll);
            this.Controls.Add(this.AddItem);
            this.Controls.Add(this.SqlBox);
            this.Controls.Add(this.ValueBox);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ComplexQueryUI";
            this.ShowInTaskbar = false;
            this.Text = "复合查询";
            this.Load += new System.EventHandler(this.ComplexQueryUI_Load);
            this.VisibleChanged += new System.EventHandler(this.ComplexQueryUI_VisibleChanged);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ComplexQueryUI_HelpRequested);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GlacisUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
		private IFeatureLayer SelectLayer;
		private IFields myfields;
		private IField myfield;
		private int SelectType;
		private QueryResult resultDlg;
		private ISelectionSet m_pSelectionSetForSearch;
        private Button BtnGetUniqueValue;
    }
}