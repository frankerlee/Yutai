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
            this.panelParam = new Panel();
            this.btnModify = new Button();
            this.listView2 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.panelJTB = new Panel();
            this.groupBox1 = new GroupBox();
            this.txtJTB8 = new TextBox();
            this.txtJTB7 = new TextBox();
            this.txtJTB6 = new TextBox();
            this.txtJTB9 = new TextBox();
            this.txtJTB4 = new TextBox();
            this.txtJTB3 = new TextBox();
            this.txtJTB2 = new TextBox();
            this.txtJTB1 = new TextBox();
            this.panelParam.SuspendLayout();
            this.panelJTB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.panelParam.Controls.Add(this.btnModify);
            this.panelParam.Controls.Add(this.listView2);
            this.panelParam.Location = new Point(17, 105);
            this.panelParam.Name = "panelParam";
            this.panelParam.Size = new Size(442, 168);
            this.panelParam.TabIndex = 14;
            this.btnModify.Enabled = false;
            this.btnModify.Location = new Point(361, 14);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(60, 23);
            this.btnModify.TabIndex = 2;
            this.btnModify.Text = "修改值";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.listView2.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView2.FullRowSelect = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new Point(12, 14);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new Size(343, 137);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = View.Details;
            this.listView2.SelectedIndexChanged += new EventHandler(this.listView2_SelectedIndexChanged);
            this.columnHeader_0.Text = "参数名";
            this.columnHeader_0.Width = 110;
            this.columnHeader_1.Text = "值";
            this.columnHeader_1.Width = 96;
            this.panelJTB.Controls.Add(this.groupBox1);
            this.panelJTB.Location = new Point(17, 6);
            this.panelJTB.Name = "panelJTB";
            this.panelJTB.Size = new Size(355, 95);
            this.panelJTB.TabIndex = 13;
            this.groupBox1.Controls.Add(this.txtJTB8);
            this.groupBox1.Controls.Add(this.txtJTB7);
            this.groupBox1.Controls.Add(this.txtJTB6);
            this.groupBox1.Controls.Add(this.txtJTB9);
            this.groupBox1.Controls.Add(this.txtJTB4);
            this.groupBox1.Controls.Add(this.txtJTB3);
            this.groupBox1.Controls.Add(this.txtJTB2);
            this.groupBox1.Controls.Add(this.txtJTB1);
            this.groupBox1.Location = new Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(340, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "接图表";
            this.txtJTB8.Location = new Point(113, 60);
            this.txtJTB8.Name = "txtJTB8";
            this.txtJTB8.Size = new Size(101, 21);
            this.txtJTB8.TabIndex = 8;
            this.txtJTB7.Location = new Point(7, 60);
            this.txtJTB7.Name = "txtJTB7";
            this.txtJTB7.Size = new Size(101, 21);
            this.txtJTB7.TabIndex = 7;
            this.txtJTB6.Location = new Point(221, 37);
            this.txtJTB6.Name = "txtJTB6";
            this.txtJTB6.Size = new Size(101, 21);
            this.txtJTB6.TabIndex = 6;
            this.txtJTB9.Location = new Point(221, 60);
            this.txtJTB9.Name = "txtJTB9";
            this.txtJTB9.Size = new Size(101, 21);
            this.txtJTB9.TabIndex = 9;
            this.txtJTB4.Location = new Point(7, 37);
            this.txtJTB4.Name = "txtJTB4";
            this.txtJTB4.Size = new Size(101, 21);
            this.txtJTB4.TabIndex = 3;
            this.txtJTB3.Location = new Point(221, 15);
            this.txtJTB3.Name = "txtJTB3";
            this.txtJTB3.Size = new Size(101, 21);
            this.txtJTB3.TabIndex = 2;
            this.txtJTB2.Location = new Point(114, 15);
            this.txtJTB2.Name = "txtJTB2";
            this.txtJTB2.Size = new Size(101, 21);
            this.txtJTB2.TabIndex = 1;
            this.txtJTB1.Location = new Point(7, 15);
            this.txtJTB1.Name = "txtJTB1";
            this.txtJTB1.Size = new Size(101, 21);
            this.txtJTB1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panelParam);
            base.Controls.Add(this.panelJTB);
            base.Name = "MapTemplateParamPage";
            base.Size = new Size(532, 287);
            base.Load += new EventHandler(this.MapTemplateParamPage_Load);
            this.panelParam.ResumeLayout(false);
            this.panelJTB.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
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