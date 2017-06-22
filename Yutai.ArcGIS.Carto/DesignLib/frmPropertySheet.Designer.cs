using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class frmPropertySheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPropertySheet));
            this.panel1 = new Panel();
            this.btnOK = new Button();
            this.button2 = new Button();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(344, 253);
            this.panel1.TabIndex = 0;
            this.btnOK.Location = new Point(168, 259);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(73, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.Location = new Point(260, 259);
            this.button2.Name = "button2";
            this.button2.Size = new Size(73, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(344, 291);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmPropertySheet";
            this.Text = "位置属性";
            base.ResumeLayout(false);
        }
    
        private Button btnOK;
        private Button button2;
        private Panel panel1;
    }
}