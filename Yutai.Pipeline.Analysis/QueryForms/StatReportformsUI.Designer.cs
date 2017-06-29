
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    partial class StatReportformsUI
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
            this.CmbLayers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CmbStatField = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CmbCalField = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.CmbStatWay = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CmbLayers
            // 
            this.CmbLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLayers.FormattingEnabled = true;
            this.CmbLayers.Location = new System.Drawing.Point(83, 12);
            this.CmbLayers.Name = "CmbLayers";
            this.CmbLayers.Size = new System.Drawing.Size(278, 20);
            this.CmbLayers.TabIndex = 0;
            this.CmbLayers.SelectedIndexChanged += new System.EventHandler(this.CmbLayers_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "统计图层：";
            // 
            // CmbStatField
            // 
            this.CmbStatField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbStatField.FormattingEnabled = true;
            this.CmbStatField.Location = new System.Drawing.Point(83, 38);
            this.CmbStatField.Name = "CmbStatField";
            this.CmbStatField.Size = new System.Drawing.Size(156, 20);
            this.CmbStatField.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "统计字段：";
            // 
            // CmbCalField
            // 
            this.CmbCalField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCalField.FormattingEnabled = true;
            this.CmbCalField.Location = new System.Drawing.Point(83, 64);
            this.CmbCalField.Name = "CmbCalField";
            this.CmbCalField.Size = new System.Drawing.Size(156, 20);
            this.CmbCalField.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "计算字段：";
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(286, 139);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 23);
            this.BtnClose.TabIndex = 2;
            this.BtnClose.Text = "关闭";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(205, 139);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 2;
            this.BtnOK.Text = "统计";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // CmbStatWay
            // 
            this.CmbStatWay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbStatWay.FormattingEnabled = true;
            this.CmbStatWay.Items.AddRange(new object[] {
            "计数",
            "平均值"});
            this.CmbStatWay.Location = new System.Drawing.Point(83, 90);
            this.CmbStatWay.Name = "CmbStatWay";
            this.CmbStatWay.Size = new System.Drawing.Size(156, 20);
            this.CmbStatWay.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "统计方式：";
            // 
            // StatReportformsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 202);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CmbStatWay);
            this.Controls.Add(this.CmbCalField);
            this.Controls.Add(this.CmbStatField);
            this.Controls.Add(this.CmbLayers);
            this.Name = "StatReportformsUI";
            this.Text = "统计报表";
            this.Load += new System.EventHandler(this.StatReportformsUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private IContainer components = null;
        private ComboBox CmbLayers;
        private Label label1;
        private ComboBox CmbStatField;
        private Label label2;
        private ComboBox CmbCalField;
        private Label label3;
        private Button BtnClose;
        private Button BtnOK;
        private ComboBox CmbStatWay;
        private Label label4;
    }
}