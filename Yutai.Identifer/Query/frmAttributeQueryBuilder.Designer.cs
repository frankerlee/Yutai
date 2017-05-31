using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Plugins.Identifer.Query
{
    partial class frmAttributeQueryBuilder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbMap = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSelectType = new System.Windows.Forms.ComboBox();
            this.chkShowSelectbaleLayer = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxLayer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkZoomToSelect = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(232, 502);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(56, 24);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(296, 502);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 24);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbMap);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboSelectType);
            this.panel1.Controls.Add(this.chkShowSelectbaleLayer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxLayer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 112);
            this.panel1.TabIndex = 7;
            // 
            // cmbMap
            // 
            this.cmbMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMap.Items.AddRange(new object[] {
            "二维地图",
            "三维地图"});
            this.cmbMap.Location = new System.Drawing.Point(56, 6);
            this.cmbMap.Name = "cmbMap";
            this.cmbMap.Size = new System.Drawing.Size(318, 20);
            this.cmbMap.TabIndex = 53;
            this.cmbMap.SelectedIndexChanged += new System.EventHandler(this.cmbMap_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 52;
            this.label2.Text = "地图:";
            // 
            // cboSelectType
            // 
            this.cboSelectType.Items.AddRange(new object[] {
            "创建一个新的选择集",
            "添加到现有选择集",
            "从现有选择集中删除",
            "从现有选择集中选择"});
            this.cboSelectType.Location = new System.Drawing.Point(56, 82);
            this.cboSelectType.Name = "cboSelectType";
            this.cboSelectType.Size = new System.Drawing.Size(318, 20);
            this.cboSelectType.TabIndex = 51;
            this.cboSelectType.Text = "创建一个新的选择集";
            this.cboSelectType.SelectedIndexChanged += new System.EventHandler(this.cboSelectType_SelectedIndexChanged);
            // 
            // chkShowSelectbaleLayer
            // 
            this.chkShowSelectbaleLayer.Location = new System.Drawing.Point(56, 58);
            this.chkShowSelectbaleLayer.Name = "chkShowSelectbaleLayer";
            this.chkShowSelectbaleLayer.Size = new System.Drawing.Size(168, 19);
            this.chkShowSelectbaleLayer.TabIndex = 50;
            this.chkShowSelectbaleLayer.Text = "只显示可选择图层";
            this.chkShowSelectbaleLayer.Click += new System.EventHandler(this.chkShowSelectbaleLayer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 49;
            this.label3.Text = "方法:";
            // 
            // comboBoxLayer
            // 
            this.comboBoxLayer.Location = new System.Drawing.Point(56, 33);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Size = new System.Drawing.Size(318, 20);
            this.comboBoxLayer.TabIndex = 48;
            this.comboBoxLayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 47;
            this.label1.Text = "图层:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(8, 502);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 24);
            this.btnClear.TabIndex = 51;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(386, 360);
            this.panel2.TabIndex = 52;
            // 
            // chkZoomToSelect
            // 
            this.chkZoomToSelect.AutoSize = true;
            this.chkZoomToSelect.Location = new System.Drawing.Point(12, 480);
            this.chkZoomToSelect.Name = "chkZoomToSelect";
            this.chkZoomToSelect.Size = new System.Drawing.Size(108, 16);
            this.chkZoomToSelect.TabIndex = 53;
            this.chkZoomToSelect.Text = "缩放到选中要素";
            this.chkZoomToSelect.UseVisualStyleBackColor = true;
            // 
            // frmAttributeQueryBuilder
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(386, 536);
            this.Controls.Add(this.chkZoomToSelect);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAttributeQueryBuilder";
            this.ShowInTaskbar = false;
            this.Text = "属性查询";
            this.Load += new System.EventHandler(this.frmAttributeQueryBuilder_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        /// 
        /// 
        private Button btnApply;

        private Button btnClose;

        private Panel panel1;

        private Button btnClear;

        private ComboBox cboSelectType;

        private CheckBox chkShowSelectbaleLayer;

        private Label label3;

        private ComboBox comboBoxLayer;

        private Label label1;

        private Panel panel2;

        private CheckBox chkZoomToSelect;
        private ComboBox cmbMap;
        private Label label2;
    }
}