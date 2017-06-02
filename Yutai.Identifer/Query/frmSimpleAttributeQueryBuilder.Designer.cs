using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Plugins.Identifer.Query
{
    partial class frmSimpleAttributeQueryBuilder
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "frmSimpleAttributeQueryBuilder";
          
            this.btnApply = new Button();
            this.btnClose = new Button();
            this.panel1 = new Panel();
            this.btnClear = new Button();
            base.SuspendLayout();
            this.btnApply.Location = new Point(272, 384);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(56, 24);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "确定";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnClose.Location = new Point(344, 384);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(56, 24);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(408, 376);
            this.panel1.TabIndex = 7;
            this.btnClear.Location = new Point(16, 384);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(56, 24);
            this.btnClear.TabIndex = 51;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(408, 415);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnApply);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
           
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAttributeQueryBuilder";
            this.Text = "查询生成器";
            base.Load += new EventHandler(this.frmAttributeQueryBuilder_Load);
            base.ResumeLayout(false);
        }

        #endregion
    }
}