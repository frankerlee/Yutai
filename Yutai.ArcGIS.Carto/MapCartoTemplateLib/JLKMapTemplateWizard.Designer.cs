using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class JLKMapTemplateWizard
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
            this.panel2 = new Panel();
            this.btnLast = new Button();
            this.button3 = new Button();
            this.btnNext = new Button();
            this.panel1 = new Panel();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 348);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(468, 27);
            this.panel2.TabIndex = 3;
            this.btnLast.Location = new Point(195, 3);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(75, 23);
            this.btnLast.TabIndex = 11;
            this.btnLast.Text = "<上一步";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new Point(360, 3);
            this.button3.Name = "button3";
            this.button3.Size = new Size(75, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            this.btnNext.Location = new Point(276, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(75, 23);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = "下一步>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(468, 348);
            this.panel1.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(468, 375);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            base.Name = "JLKMapTemplateWizard";
            this.Text = "制图模板向导";
            base.Load += new EventHandler(this.JLKMapTemplateWizard_Load);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private Button btnLast;
        private Button btnNext;
        private Button button3;
        private MapCartoTemplateLib.MapTemplate mapTemplate_0;
        private Panel panel1;
        private Panel panel2;
    }
}