using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace Yutai.ArcGIS.Carto.Library
{
    partial class frmMapTemplateApplyWizard
    {
        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_3);
        }

       
        private void InitializeComponent()
        {
            this.panel2 = new Panel();
            this.panel1 = new Panel();
            this.button3 = new Button();
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 301);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(546, 43);
            this.panel2.TabIndex = 1;
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(546, 301);
            this.panel1.TabIndex = 2;
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(453, 8);
            this.button3.Name = "button3";
            this.button3.Size = new Size(75, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            this.btnNext.Location = new System.Drawing.Point(369, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(75, 23);
            this.btnNext.TabIndex = 12;
            this.btnNext.Text = "下一步>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Location = new System.Drawing.Point(288, 8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(75, 23);
            this.btnLast.TabIndex = 11;
            this.btnLast.Text = "<上一步";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(546, 344);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            base.Name = "frmMapTemplateApplyWizard";
            this.Text = "地图模板向导";
            base.Load += new EventHandler(this.frmMapTemplateApplyWizard_Load);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private bool bool_1;
        private bool bool_2;
        private Button btnLast;
        private Button btnNext;
        private Button button3;
        private Panel panel1;
        private Panel panel2;
    }
}