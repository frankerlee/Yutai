using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace Yutai.ArcGIS.Carto.Library
{
    partial class MapTemplateParamPage
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.panelParam = new System.Windows.Forms.Panel();
            this.btnModify = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader_0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelJTB = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtJTB8 = new System.Windows.Forms.TextBox();
            this.txtJTB7 = new System.Windows.Forms.TextBox();
            this.txtJTB6 = new System.Windows.Forms.TextBox();
            this.txtJTB9 = new System.Windows.Forms.TextBox();
            this.txtJTB4 = new System.Windows.Forms.TextBox();
            this.txtJTB3 = new System.Windows.Forms.TextBox();
            this.txtJTB2 = new System.Windows.Forms.TextBox();
            this.txtJTB1 = new System.Windows.Forms.TextBox();
            this.panelParam.SuspendLayout();
            this.panelJTB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelParam
            // 
            this.panelParam.Controls.Add(this.btnModify);
            this.panelParam.Controls.Add(this.listView2);
            this.panelParam.Location = new System.Drawing.Point(17, 105);
            this.panelParam.Name = "panelParam";
            this.panelParam.Size = new System.Drawing.Size(442, 168);
            this.panelParam.TabIndex = 14;
            // 
            // btnModify
            // 
            this.btnModify.Enabled = false;
            this.btnModify.Location = new System.Drawing.Point(361, 14);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(60, 23);
            this.btnModify.TabIndex = 2;
            this.btnModify.Text = "修改值";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_0,
            this.columnHeader_1});
            this.listView2.FullRowSelect = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(12, 14);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(343, 137);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            // 
            // columnHeader_0
            // 
            this.columnHeader_0.Text = "参数名";
            this.columnHeader_0.Width = 110;
            // 
            // columnHeader_1
            // 
            this.columnHeader_1.Text = "值";
            this.columnHeader_1.Width = 96;
            // 
            // panelJTB
            // 
            this.panelJTB.Controls.Add(this.groupBox1);
            this.panelJTB.Location = new System.Drawing.Point(17, 6);
            this.panelJTB.Name = "panelJTB";
            this.panelJTB.Size = new System.Drawing.Size(355, 95);
            this.panelJTB.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtJTB8);
            this.groupBox1.Controls.Add(this.txtJTB7);
            this.groupBox1.Controls.Add(this.txtJTB6);
            this.groupBox1.Controls.Add(this.txtJTB9);
            this.groupBox1.Controls.Add(this.txtJTB4);
            this.groupBox1.Controls.Add(this.txtJTB3);
            this.groupBox1.Controls.Add(this.txtJTB2);
            this.groupBox1.Controls.Add(this.txtJTB1);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "接图表";
            // 
            // txtJTB8
            // 
            this.txtJTB8.Location = new System.Drawing.Point(113, 60);
            this.txtJTB8.Name = "txtJTB8";
            this.txtJTB8.Size = new System.Drawing.Size(101, 21);
            this.txtJTB8.TabIndex = 8;
            // 
            // txtJTB7
            // 
            this.txtJTB7.Location = new System.Drawing.Point(7, 60);
            this.txtJTB7.Name = "txtJTB7";
            this.txtJTB7.Size = new System.Drawing.Size(101, 21);
            this.txtJTB7.TabIndex = 7;
            // 
            // txtJTB6
            // 
            this.txtJTB6.Location = new System.Drawing.Point(221, 37);
            this.txtJTB6.Name = "txtJTB6";
            this.txtJTB6.Size = new System.Drawing.Size(101, 21);
            this.txtJTB6.TabIndex = 6;
            // 
            // txtJTB9
            // 
            this.txtJTB9.Location = new System.Drawing.Point(221, 60);
            this.txtJTB9.Name = "txtJTB9";
            this.txtJTB9.Size = new System.Drawing.Size(101, 21);
            this.txtJTB9.TabIndex = 9;
            // 
            // txtJTB4
            // 
            this.txtJTB4.Location = new System.Drawing.Point(7, 37);
            this.txtJTB4.Name = "txtJTB4";
            this.txtJTB4.Size = new System.Drawing.Size(101, 21);
            this.txtJTB4.TabIndex = 3;
            // 
            // txtJTB3
            // 
            this.txtJTB3.Location = new System.Drawing.Point(221, 15);
            this.txtJTB3.Name = "txtJTB3";
            this.txtJTB3.Size = new System.Drawing.Size(101, 21);
            this.txtJTB3.TabIndex = 2;
            // 
            // txtJTB2
            // 
            this.txtJTB2.Location = new System.Drawing.Point(114, 15);
            this.txtJTB2.Name = "txtJTB2";
            this.txtJTB2.Size = new System.Drawing.Size(101, 21);
            this.txtJTB2.TabIndex = 1;
            // 
            // txtJTB1
            // 
            this.txtJTB1.Location = new System.Drawing.Point(7, 15);
            this.txtJTB1.Name = "txtJTB1";
            this.txtJTB1.Size = new System.Drawing.Size(101, 21);
            this.txtJTB1.TabIndex = 0;
            // 
            // MapTemplateParamPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelParam);
            this.Controls.Add(this.panelJTB);
            this.Name = "MapTemplateParamPage";
            this.Size = new System.Drawing.Size(476, 287);
            this.Load += new System.EventHandler(this.MapTemplateParamPage_Load);
            this.panelParam.ResumeLayout(false);
            this.panelJTB.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

       
        private Button btnModify;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private GroupBox groupBox1;
        private ListView listView2;
        private Panel panelJTB;
        private Panel panelParam;
        private TextBox textBox_0;
        private TextBox txtJTB1;
        private TextBox txtJTB2;
        private TextBox txtJTB3;
        private TextBox txtJTB4;
        private TextBox txtJTB6;
        private TextBox txtJTB7;
        private TextBox txtJTB8;
        private TextBox txtJTB9;
    }
}