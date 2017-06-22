using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class frmSelectParam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectParam));
            this.button1 = new Button();
            this.button2 = new Button();
            this.listBox1 = new ListBox();
            base.SuspendLayout();
            this.button1.Location = new Point(98, 178);
            this.button1.Name = "button1";
            this.button1.Size = new Size(66, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button2.Location = new Point(170, 178);
            this.button2.Name = "button2";
            this.button2.Size = new Size(66, 27);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(9, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(227, 160);
            this.listBox1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(255, 213);
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            
            base.Name = "frmSelectParam";
            this.Text = "选择参数";
            base.Load += new EventHandler(this.frmSelectParam_Load);
            base.ResumeLayout(false);
        }
    
        private Button button1;
        private Button button2;
        private ListBox listBox1;
    }
}