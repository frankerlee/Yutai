using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class frmSetTabelCell
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
            this.label1 = new Label();
            this.cboRow = new ComboBox();
            this.cboCol = new ComboBox();
            this.label2 = new Label();
            this.btnExpress = new Button();
            this.label3 = new Label();
            this.txtExpress = new TextBox();
            this.btnOK = new Button();
            this.button1 = new Button();
            this.button2 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "行";
            this.cboRow.FormattingEnabled = true;
            this.cboRow.Location = new Point(75, 23);
            this.cboRow.Name = "cboRow";
            this.cboRow.Size = new Size(121, 20);
            this.cboRow.TabIndex = 1;
            this.cboRow.SelectedIndexChanged += new EventHandler(this.cboCol_SelectedIndexChanged);
            this.cboCol.FormattingEnabled = true;
            this.cboCol.Location = new Point(75, 50);
            this.cboCol.Name = "cboCol";
            this.cboCol.Size = new Size(121, 20);
            this.cboCol.TabIndex = 3;
            this.cboCol.SelectedIndexChanged += new EventHandler(this.cboCol_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "列";
            this.btnExpress.Location = new Point(202, 81);
            this.btnExpress.Name = "btnExpress";
            this.btnExpress.Size = new Size(73, 23);
            this.btnExpress.TabIndex = 4;
            this.btnExpress.Text = "设置表达式";
            this.btnExpress.UseVisualStyleBackColor = true;
            this.btnExpress.Click += new EventHandler(this.btnExpress_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "表达式";
            this.txtExpress.Location = new Point(75, 81);
            this.txtExpress.Multiline = true;
            this.txtExpress.Name = "txtExpress";
            this.txtExpress.Size = new Size(121, 93);
            this.txtExpress.TabIndex = 6;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(75, 191);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button1.Location = new Point(200, 151);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "应用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new Point(171, 191);
            this.button2.Name = "button2";
            this.button2.Size = new Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(292, 226);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtExpress);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnExpress);
            base.Controls.Add(this.cboCol);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.cboRow);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSetTabelCell";
            this.Text = "设置单元格文本";
            base.Load += new EventHandler(this.frmSetTabelCell_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnExpress;
        private Button btnOK;
        private Button button1;
        private Button button2;
        private ComboBox cboCol;
        private ComboBox cboRow;
        private Label label1;
        private Label label2;
        private Label label3;
        private MapCartoTemplateLib.MapTemplateTableElement mapTemplateTableElement_0;
        private TextBox txtExpress;
    }
}