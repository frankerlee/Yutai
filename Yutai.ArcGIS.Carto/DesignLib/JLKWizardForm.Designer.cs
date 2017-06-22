using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class JLKWizardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JLKWizardForm));
            this.panel1 = new Panel();
            this.btnLast = new Button();
            this.btnNext = new Button();
            this.button3 = new Button();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(377, 234);
            this.panel1.TabIndex = 8;
            this.btnLast.Location = new Point(138, 249);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(75, 23);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "<上一步";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnNext.Location = new Point(219, 249);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(75, 23);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "下一步>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new Point(303, 249);
            this.button3.Name = "button3";
            this.button3.Size = new Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(377, 275);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "JLKWizardForm";
            this.Text = "模板向导";
            base.Load += new EventHandler(this.JLKWizardForm_Load);
            base.ResumeLayout(false);
        }

       
        private Button btnLast;
        private Button btnNext;
        private Button button3;
        private Panel panel1;
    }
}