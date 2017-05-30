using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Catalog.UI
{
    partial class frmAGSProperty
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
           
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmAGSProperty));
            this.panel1 = new Panel();
            this.xtraTabControl1 = new TabControl();

            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x11a);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(480, 0x30);
            this.panel1.TabIndex = 0;
            this.xtraTabControl1.Dock = DockStyle.Fill;
            this.xtraTabControl1.Location = new Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(480, 0x11a);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.Text = "xtraTabControl1";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(480, 330);
            base.Controls.Add(this.xtraTabControl1);
            base.Controls.Add(this.panel1);

            base.Name = "frmAGSProperty";
            this.Text = "frmAGSProperty";
            base.Load += new EventHandler(this.frmAGSProperty_Load);

            base.ResumeLayout(false);
        }

    

        #endregion

        private System.Windows.Forms.Panel panel1;
        private TabControl xtraTabControl1;
    }
}