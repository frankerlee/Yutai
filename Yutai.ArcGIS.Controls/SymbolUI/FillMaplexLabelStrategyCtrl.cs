using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class FillMaplexLabelStrategyCtrl : UserControl
    {
        private CheckEdit checkEdit2;
        private CheckEdit checkEdit3;
        private CheckEdit checkEdit4;
        private CheckEdit checkEdit5;
        private IContainer components;
        private GroupBox groupBox2;
        private ImageList imageList1;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private PictureEdit pictureEdit4;
        private PictureEdit pictureEdit5;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton3;
        private SimpleButton simpleButton4;
        private SimpleButton simpleButton5;

        public FillMaplexLabelStrategyCtrl()
        {
            this.InitializeComponent();
        }

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
            this.components = new Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FillMaplexLabelStrategyCtrl));
            this.imageList1 = new ImageList(this.components);
            this.groupBox2 = new GroupBox();
            this.simpleButton4 = new SimpleButton();
            this.checkEdit5 = new CheckEdit();
            this.pictureEdit5 = new PictureEdit();
            this.simpleButton3 = new SimpleButton();
            this.checkEdit4 = new CheckEdit();
            this.pictureEdit4 = new PictureEdit();
            this.simpleButton1 = new SimpleButton();
            this.simpleButton5 = new SimpleButton();
            this.checkEdit3 = new CheckEdit();
            this.pictureEdit2 = new PictureEdit();
            this.checkEdit2 = new CheckEdit();
            this.pictureEdit1 = new PictureEdit();
            this.groupBox2.SuspendLayout();
            this.checkEdit5.Properties.BeginInit();
            this.pictureEdit5.Properties.BeginInit();
            this.checkEdit4.Properties.BeginInit();
            this.pictureEdit4.Properties.BeginInit();
            this.checkEdit3.Properties.BeginInit();
            this.pictureEdit2.Properties.BeginInit();
            this.checkEdit2.Properties.BeginInit();
            this.pictureEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.imageList1.ImageSize = new Size(0x51, 0x3a);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.groupBox2.Controls.Add(this.simpleButton4);
            this.groupBox2.Controls.Add(this.checkEdit5);
            this.groupBox2.Controls.Add(this.pictureEdit5);
            this.groupBox2.Controls.Add(this.simpleButton3);
            this.groupBox2.Controls.Add(this.checkEdit4);
            this.groupBox2.Controls.Add(this.pictureEdit4);
            this.groupBox2.Controls.Add(this.simpleButton1);
            this.groupBox2.Controls.Add(this.simpleButton5);
            this.groupBox2.Controls.Add(this.checkEdit3);
            this.groupBox2.Controls.Add(this.pictureEdit2);
            this.groupBox2.Controls.Add(this.checkEdit2);
            this.groupBox2.Controls.Add(this.pictureEdit1);
            this.groupBox2.Location = new Point(0x10, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x120, 0x178);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.simpleButton4.Location = new Point(200, 0xe8);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new Size(0x38, 0x18);
            this.simpleButton4.TabIndex = 0x10;
            this.simpleButton4.Text = "选项...";
            this.checkEdit5.Location = new Point(0x68, 0xe8);
            this.checkEdit5.Name = "checkEdit5";
            this.checkEdit5.Properties.Caption = "缩写标注";
            this.checkEdit5.Size = new Size(80, 0x13);
            this.checkEdit5.TabIndex = 15;
            this.pictureEdit5.Location = new Point(8, 0xd8);
            this.pictureEdit5.Name = "pictureEdit5";
            this.pictureEdit5.Size = new Size(0x51, 0x3a);
            this.pictureEdit5.TabIndex = 14;
            this.simpleButton3.Location = new Point(200, 0xa8);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(0x38, 0x18);
            this.simpleButton3.TabIndex = 13;
            this.simpleButton3.Text = "限制...";
            this.checkEdit4.Location = new Point(0x68, 0xa8);
            this.checkEdit4.Name = "checkEdit4";
            this.checkEdit4.Properties.Caption = "减小字体大小";
            this.checkEdit4.Size = new Size(80, 0x13);
            this.checkEdit4.TabIndex = 12;
            this.pictureEdit4.Location = new Point(8, 0x98);
            this.pictureEdit4.Name = "pictureEdit4";
            this.pictureEdit4.Size = new Size(0x51, 0x3a);
            this.pictureEdit4.TabIndex = 11;
            this.simpleButton1.Location = new Point(200, 0x20);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x38, 0x18);
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "选项...";
            this.simpleButton5.Location = new Point(200, 0x62);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new Size(0x38, 0x18);
            this.simpleButton5.TabIndex = 6;
            this.simpleButton5.Text = "选项...";
            this.checkEdit3.Location = new Point(0x68, 0x62);
            this.checkEdit3.Name = "checkEdit3";
            this.checkEdit3.Properties.Caption = "超出的要素";
            this.checkEdit3.Size = new Size(80, 0x13);
            this.checkEdit3.TabIndex = 5;
            this.pictureEdit2.Location = new Point(8, 0x53);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Size = new Size(0x51, 0x3a);
            this.pictureEdit2.TabIndex = 4;
            this.checkEdit2.Location = new Point(0x68, 0x20);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "堆叠标注";
            this.checkEdit2.Size = new Size(80, 0x13);
            this.checkEdit2.TabIndex = 1;
            this.pictureEdit1.Location = new Point(8, 0x11);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new Size(0x51, 0x3a);
            this.pictureEdit1.TabIndex = 0;
            base.Controls.Add(this.groupBox2);
            base.Name = "FillMaplexLabelStrategyCtrl";
            base.Size = new Size(320, 0x1b0);
            this.groupBox2.ResumeLayout(false);
            this.checkEdit5.Properties.EndInit();
            this.pictureEdit5.Properties.EndInit();
            this.checkEdit4.Properties.EndInit();
            this.pictureEdit4.Properties.EndInit();
            this.checkEdit3.Properties.EndInit();
            this.pictureEdit2.Properties.EndInit();
            this.checkEdit2.Properties.EndInit();
            this.pictureEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }
    }
}

