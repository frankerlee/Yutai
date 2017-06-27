
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class SimpleStat
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
            this.CalButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RevBut = new System.Windows.Forms.Button();
            this.NoneBut = new System.Windows.Forms.Button();
            this.AllBut = new System.Windows.Forms.Button();
            this._layersCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this._gjListBox = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.InsertBut = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GeometrySet = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // CalButton
            // 
            this.CalButton.Location = new System.Drawing.Point(107, 306);
            this.CalButton.Name = "CalButton";
            this.CalButton.Size = new System.Drawing.Size(71, 23);
            this.CalButton.TabIndex = 3;
            this.CalButton.Text = "确定(&Q)";
            this.CalButton.UseVisualStyleBackColor = true;
            this.CalButton.Click += new System.EventHandler(this.CalButton_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RevBut);
            this.groupBox1.Controls.Add(this.NoneBut);
            this.groupBox1.Controls.Add(this.AllBut);
            this.groupBox1.Controls.Add(this._layersCheckedListBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 108);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "管线数据层列表";
            // 
            // RevBut
            // 
            this.RevBut.Location = new System.Drawing.Point(229, 70);
            this.RevBut.Name = "RevBut";
            this.RevBut.Size = new System.Drawing.Size(76, 23);
            this.RevBut.TabIndex = 6;
            this.RevBut.Text = "反选(&I)";
            this.RevBut.UseVisualStyleBackColor = true;
            this.RevBut.Click += new System.EventHandler(this.RevBut_Click);
            // 
            // NoneBut
            // 
            this.NoneBut.Location = new System.Drawing.Point(230, 41);
            this.NoneBut.Name = "NoneBut";
            this.NoneBut.Size = new System.Drawing.Size(76, 23);
            this.NoneBut.TabIndex = 5;
            this.NoneBut.Text = "全不选(&N)";
            this.NoneBut.UseVisualStyleBackColor = true;
            this.NoneBut.Click += new System.EventHandler(this.NoneBut_Click);
            // 
            // AllBut
            // 
            this.AllBut.Location = new System.Drawing.Point(230, 12);
            this.AllBut.Name = "AllBut";
            this.AllBut.Size = new System.Drawing.Size(76, 23);
            this.AllBut.TabIndex = 4;
            this.AllBut.Text = "全选(&A)";
            this.AllBut.UseVisualStyleBackColor = true;
            this.AllBut.Click += new System.EventHandler(this.AllBut_Click);
            // 
            // _layersCheckedListBox
            // 
            this._layersCheckedListBox.CheckOnClick = true;
            this._layersCheckedListBox.FormattingEnabled = true;
            this._layersCheckedListBox.Location = new System.Drawing.Point(6, 16);
            this._layersCheckedListBox.Name = "_layersCheckedListBox";
            this._layersCheckedListBox.Size = new System.Drawing.Size(217, 84);
            this._layersCheckedListBox.Sorted = true;
            this._layersCheckedListBox.TabIndex = 2;
            this._layersCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(187, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "关闭(&G)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _gjListBox
            // 
            this._gjListBox.FormattingEnabled = true;
            this._gjListBox.ItemHeight = 12;
            this._gjListBox.Location = new System.Drawing.Point(6, 14);
            this._gjListBox.Name = "_gjListBox";
            this._gjListBox.Size = new System.Drawing.Size(73, 148);
            this._gjListBox.Sorted = true;
            this._gjListBox.TabIndex = 17;
            this._gjListBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._gjListBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(85, 169);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "管径范围";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.InsertBut);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Location = new System.Drawing.Point(103, 126);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(78, 169);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作";
            // 
            // InsertBut
            // 
            this.InsertBut.Location = new System.Drawing.Point(6, 140);
            this.InsertBut.Name = "InsertBut";
            this.InsertBut.Size = new System.Drawing.Size(69, 23);
            this.InsertBut.TabIndex = 28;
            this.InsertBut.Text = "插入行";
            this.InsertBut.UseVisualStyleBackColor = true;
            this.InsertBut.Click += new System.EventHandler(this.InsertBut_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 80);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(69, 23);
            this.button5.TabIndex = 27;
            this.button5.Text = "添加行";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 110);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(69, 23);
            this.button4.TabIndex = 26;
            this.button4.Text = "删除行";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 50);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 23);
            this.button3.TabIndex = 25;
            this.button3.Text = "添加上限";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "添加下限";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Location = new System.Drawing.Point(187, 126);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(137, 169);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "统计范围设置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "下限=<统计值<上限";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4});
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridView1.Location = new System.Drawing.Point(6, 14);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(126, 127);
            this.dataGridView1.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "下限";
            this.Column1.MaxInputLength = 6;
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "上限";
            this.Column4.MaxInputLength = 6;
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 60;
            // 
            // GeometrySet
            // 
            this.GeometrySet.AutoSize = true;
            this.GeometrySet.Location = new System.Drawing.Point(18, 310);
            this.GeometrySet.Name = "GeometrySet";
            this.GeometrySet.Size = new System.Drawing.Size(72, 16);
            this.GeometrySet.TabIndex = 22;
            this.GeometrySet.Text = "空间范围";
            this.GeometrySet.UseVisualStyleBackColor = true;
            this.GeometrySet.Click += new System.EventHandler(this.GeometrySet_Click);
            // 
            // SimpleStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 335);
            this.Controls.Add(this.GeometrySet);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CalButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SimpleStat";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "管径分段统计";
            this.Load += new System.EventHandler(this.SimpleStat_Load);
            this.VisibleChanged += new System.EventHandler(this.SimpleStat_VisibleChanged);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.SimpleStat_HelpRequested);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	
		private IContainer components = null;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private string strGJ;
		private IField myfield;
		private IFeatureLayer SelectLayer;
		private IFields myfields;
		private IField myfieldGJ;
		private QueryResult resultDlg;
		private Button CalButton;
		private GroupBox groupBox1;
		private Button RevBut;
		private Button NoneBut;
		private Button AllBut;
		private CheckedListBox _layersCheckedListBox;
		private Button button1;
		private ListBox _gjListBox;
		private GroupBox groupBox2;
		private GroupBox groupBox3;
		private Button button3;
		private Button button2;
		private GroupBox groupBox4;
		private DataGridView dataGridView1;
		private Button button4;
		private Button button5;
		private DataGridViewTextBoxColumn Column1;
		private DataGridViewTextBoxColumn Column4;
		private Label label1;
		private Button InsertBut;
		private CheckBox GeometrySet;
    }
}