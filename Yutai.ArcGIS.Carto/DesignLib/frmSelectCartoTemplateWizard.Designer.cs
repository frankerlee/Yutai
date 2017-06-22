using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class frmSelectCartoTemplateWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectCartoTemplateWizard));
            this.button3 = new Button();
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new Point(385, 290);
            this.button3.Name = "button3";
            this.button3.Size = new Size(75, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            this.btnNext.Location = new Point(301, 290);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(75, 23);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = "下一步>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Location = new Point(220, 290);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(75, 23);
            this.btnLast.TabIndex = 11;
            this.btnLast.Text = "<上一步";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(467, 275);
            this.panel1.TabIndex = 12;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(467, 325);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSelectCartoTemplateWizard";
            this.Text = "选择模板";
            base.Load += new EventHandler(this.frmSelectCartoTemplateWizard_Load);
            base.ResumeLayout(false);
        }

       
        private Button btnLast;
        private Button btnNext;
        private Button button3;
        private Panel panel1;
    }
}