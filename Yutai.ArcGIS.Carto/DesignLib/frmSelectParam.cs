using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class frmSelectParam : Form
    {
        private Button button1;
        private Button button2;
        private IContainer icontainer_0 = null;
        private ListBox listBox1;

        public frmSelectParam()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmSelectParam_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectParam));
            this.button1 = new Button();
            this.button2 = new Button();
            this.listBox1 = new ListBox();
            base.SuspendLayout();
            this.button1.Location = new Point(0x62, 0xb2);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x42, 0x1b);
            this.button1.TabIndex = 0;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button2.Location = new Point(170, 0xb2);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x42, 0x1b);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(9, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0xe3, 160);
            this.listBox1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xff, 0xd5);
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            
            base.Name = "frmSelectParam";
            this.Text = "选择参数";
            base.Load += new EventHandler(this.frmSelectParam_Load);
            base.ResumeLayout(false);
        }
    }
}

