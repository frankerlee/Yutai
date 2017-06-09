using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class CheckOutPropertyCtrl : UserControl
    {
        private ListBox CheckOutDatasetlist;
        private Container container_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblCreateTime;
        private Label lblName;
        private Label lblOwner;
        private Label lblParentVersion;

        public CheckOutPropertyCtrl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.lblName = new Label();
            this.lblOwner = new Label();
            this.lblCreateTime = new Label();
            this.lblParentVersion = new Label();
            this.CheckOutDatasetlist = new ListBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x18, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "名字:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x18, 0x2c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "属主:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x18, 0x4a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "创建:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x18, 0x68);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x30, 0x11);
            this.label4.TabIndex = 3;
            this.label4.Text = "父版本:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x18, 0x86);
            this.label5.Name = "label5";
            this.label5.Size = new Size(60, 0x11);
            this.label5.TabIndex = 4;
            this.label5.Text = "检出数据:";
            this.lblName.Location = new Point(0x58, 14);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(160, 0x18);
            this.lblName.TabIndex = 5;
            this.lblOwner.Location = new Point(0x58, 0x2c);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new Size(160, 0x18);
            this.lblOwner.TabIndex = 6;
            this.lblCreateTime.Location = new Point(0x58, 0x4a);
            this.lblCreateTime.Name = "lblCreateTime";
            this.lblCreateTime.Size = new Size(160, 0x18);
            this.lblCreateTime.TabIndex = 7;
            this.lblParentVersion.Location = new Point(0x58, 0x68);
            this.lblParentVersion.Name = "lblParentVersion";
            this.lblParentVersion.Size = new Size(160, 0x18);
            this.lblParentVersion.TabIndex = 8;
            this.CheckOutDatasetlist.ItemHeight = 12;
            this.CheckOutDatasetlist.Location = new Point(0x18, 160);
            this.CheckOutDatasetlist.Name = "CheckOutDatasetlist";
            this.CheckOutDatasetlist.Size = new Size(0xe8, 0x7c);
            this.CheckOutDatasetlist.TabIndex = 9;
            base.Controls.Add(this.CheckOutDatasetlist);
            base.Controls.Add(this.lblParentVersion);
            base.Controls.Add(this.lblCreateTime);
            base.Controls.Add(this.lblOwner);
            base.Controls.Add(this.lblName);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "CheckOutPropertyCtrl";
            base.Size = new Size(0x110, 0x128);
            base.ResumeLayout(false);
        }
    }
}

