using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Controls.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class frmRepresationRule
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRepresationRule));
            this.btnAdd = new Button();
            this.btnCnacel = new Button();
            base.SuspendLayout();
            this.btnAdd.Location = new Point(169, 371);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(39, 25);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "确定";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnCnacel.Location = new Point(249, 371);
            this.btnCnacel.Name = "btnCnacel";
            this.btnCnacel.Size = new Size(39, 25);
            this.btnCnacel.TabIndex = 3;
            this.btnCnacel.Text = "取消";
            this.btnCnacel.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(292, 408);
            base.Controls.Add(this.btnCnacel);
            base.Controls.Add(this.btnAdd);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRepresationRule";
            this.Text = "Representation Rule";
            base.Load += new EventHandler(this.frmRepresationRule_Load);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnAdd;
        private Button btnCnacel;
    }
}