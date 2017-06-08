using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class FillMaplexLabelConficCtrl : UserControl
    {
        private CheckEdit checkEdit2;
        private CheckEdit checkEdit4;
        private CheckEdit checkEdit5;
        private IContainer components;
        private GroupBox groupBox2;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private PictureEdit pictureEdit3;
        private PictureEdit pictureEdit4;
        private PictureEdit pictureEdit5;
        private SimpleButton simpleButton3;
        private SpinEdit spinEdit1;
        private SpinEdit spinEdit2;
        private SpinEdit spinEdit3;

        public FillMaplexLabelConficCtrl()
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FillMaplexLabelConficCtrl));
            this.imageList1 = new ImageList(this.components);
            this.groupBox2 = new GroupBox();
            this.spinEdit2 = new SpinEdit();
            this.label2 = new Label();
            this.pictureEdit3 = new PictureEdit();
            this.spinEdit1 = new SpinEdit();
            this.label1 = new Label();
            this.pictureEdit2 = new PictureEdit();
            this.checkEdit5 = new CheckEdit();
            this.pictureEdit5 = new PictureEdit();
            this.simpleButton3 = new SimpleButton();
            this.checkEdit4 = new CheckEdit();
            this.pictureEdit4 = new PictureEdit();
            this.checkEdit2 = new CheckEdit();
            this.pictureEdit1 = new PictureEdit();
            this.spinEdit3 = new SpinEdit();
            this.label3 = new Label();
            this.groupBox2.SuspendLayout();
            this.spinEdit2.Properties.BeginInit();
            this.pictureEdit3.Properties.BeginInit();
            this.spinEdit1.Properties.BeginInit();
            this.pictureEdit2.Properties.BeginInit();
            this.checkEdit5.Properties.BeginInit();
            this.pictureEdit5.Properties.BeginInit();
            this.checkEdit4.Properties.BeginInit();
            this.pictureEdit4.Properties.BeginInit();
            this.checkEdit2.Properties.BeginInit();
            this.pictureEdit1.Properties.BeginInit();
            this.spinEdit3.Properties.BeginInit();
            base.SuspendLayout();
            this.imageList1.ImageSize = new Size(0x51, 0x3a);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.groupBox2.Controls.Add(this.spinEdit3);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.spinEdit2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.pictureEdit3);
            this.groupBox2.Controls.Add(this.spinEdit1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.pictureEdit2);
            this.groupBox2.Controls.Add(this.checkEdit5);
            this.groupBox2.Controls.Add(this.pictureEdit5);
            this.groupBox2.Controls.Add(this.simpleButton3);
            this.groupBox2.Controls.Add(this.checkEdit4);
            this.groupBox2.Controls.Add(this.pictureEdit4);
            this.groupBox2.Controls.Add(this.checkEdit2);
            this.groupBox2.Controls.Add(this.pictureEdit1);
            this.groupBox2.Location = new Point(0x10, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x120, 0x160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            int[] bits = new int[4];
            this.spinEdit2.EditValue = new decimal(bits);
            this.spinEdit2.Location = new Point(0xb0, 0x128);
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEdit2.Properties.UseCtrlIncrement = false;
            this.spinEdit2.Size = new Size(0x48, 0x17);
            this.spinEdit2.TabIndex = 0x16;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x68, 0x128);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 0x11);
            this.label2.TabIndex = 0x15;
            this.label2.Text = "标注缓冲:";
            this.pictureEdit3.Location = new Point(8, 280);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Size = new Size(0x51, 0x3a);
            this.pictureEdit3.TabIndex = 20;
            bits = new int[4];
            this.spinEdit1.EditValue = new decimal(bits);
            this.spinEdit1.Location = new Point(0xb0, 0x20);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEdit1.Properties.UseCtrlIncrement = false;
            this.spinEdit1.Size = new Size(0x48, 0x17);
            this.spinEdit1.TabIndex = 0x13;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x68, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x55, 0x11);
            this.label1.TabIndex = 0x12;
            this.label1.Text = "内部要素权重:";
            this.pictureEdit2.Location = new Point(8, 0x10);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Size = new Size(0x51, 0x3a);
            this.pictureEdit2.TabIndex = 0x11;
            this.checkEdit5.Location = new Point(0x68, 0xe8);
            this.checkEdit5.Name = "checkEdit5";
            this.checkEdit5.Properties.Caption = "从不删除标注";
            this.checkEdit5.Size = new Size(0x68, 0x13);
            this.checkEdit5.TabIndex = 15;
            this.pictureEdit5.Location = new Point(8, 0xd8);
            this.pictureEdit5.Name = "pictureEdit5";
            this.pictureEdit5.Size = new Size(0x51, 0x3a);
            this.pictureEdit5.TabIndex = 14;
            this.simpleButton3.Location = new Point(0xd8, 0xa8);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(0x38, 0x18);
            this.simpleButton3.TabIndex = 13;
            this.simpleButton3.Text = "限制...";
            this.checkEdit4.Location = new Point(0x68, 0xa8);
            this.checkEdit4.Name = "checkEdit4";
            this.checkEdit4.Properties.Caption = "删除重复的标注";
            this.checkEdit4.Size = new Size(0x68, 0x13);
            this.checkEdit4.TabIndex = 12;
            this.pictureEdit4.Location = new Point(8, 0x98);
            this.pictureEdit4.Name = "pictureEdit4";
            this.pictureEdit4.Size = new Size(0x51, 0x3a);
            this.pictureEdit4.TabIndex = 11;
            this.checkEdit2.Location = new Point(0x68, 0x68);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "背景标注";
            this.checkEdit2.Size = new Size(80, 0x13);
            this.checkEdit2.TabIndex = 1;
            this.pictureEdit1.Location = new Point(8, 0x58);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new Size(0x51, 0x3a);
            this.pictureEdit1.TabIndex = 0;
            bits = new int[4];
            this.spinEdit3.EditValue = new decimal(bits);
            this.spinEdit3.Location = new Point(0xb0, 0x40);
            this.spinEdit3.Name = "spinEdit3";
            this.spinEdit3.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEdit3.Properties.UseCtrlIncrement = false;
            this.spinEdit3.Size = new Size(0x48, 0x17);
            this.spinEdit3.TabIndex = 0x18;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x68, 0x40);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x55, 0x11);
            this.label3.TabIndex = 0x17;
            this.label3.Text = "边界要素权重:";
            base.Controls.Add(this.groupBox2);
            base.Name = "FillMaplexLabelConficCtrl";
            base.Size = new Size(320, 0x1b0);
            base.Load += new EventHandler(this.PointMaplexLabelConficCtrl_Load);
            this.groupBox2.ResumeLayout(false);
            this.spinEdit2.Properties.EndInit();
            this.pictureEdit3.Properties.EndInit();
            this.spinEdit1.Properties.EndInit();
            this.pictureEdit2.Properties.EndInit();
            this.checkEdit5.Properties.EndInit();
            this.pictureEdit5.Properties.EndInit();
            this.checkEdit4.Properties.EndInit();
            this.pictureEdit4.Properties.EndInit();
            this.checkEdit2.Properties.EndInit();
            this.pictureEdit1.Properties.EndInit();
            this.spinEdit3.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void PointMaplexLabelConficCtrl_Load(object sender, EventArgs e)
        {
        }
    }
}

