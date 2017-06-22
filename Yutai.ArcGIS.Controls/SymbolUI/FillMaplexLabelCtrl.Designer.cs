using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class FillMaplexLabelCtrl
    {
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FillMaplexLabelCtrl));
            this.groupBox1 = new GroupBox();
            this.simpleButton3 = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.simpleButton1 = new SimpleButton();
            this.imageComboBoxEdit1 = new ImageComboBoxEdit();
            this.imageList1 = new ImageList(this.components);
            this.checkEdit1 = new CheckEdit();
            this.groupBox2 = new GroupBox();
            this.simpleButton5 = new SimpleButton();
            this.checkEdit3 = new CheckEdit();
            this.pictureEdit2 = new PictureEdit();
            this.checkEdit2 = new CheckEdit();
            this.pictureEdit1 = new PictureEdit();
            this.checkEdit5 = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.imageComboBoxEdit1.Properties.BeginInit();
            this.checkEdit1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.checkEdit3.Properties.BeginInit();
            this.pictureEdit2.Properties.BeginInit();
            this.checkEdit2.Properties.BeginInit();
            this.pictureEdit1.Properties.BeginInit();
            this.checkEdit5.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.checkEdit5);
            this.groupBox1.Controls.Add(this.simpleButton3);
            this.groupBox1.Controls.Add(this.simpleButton2);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Controls.Add(this.imageComboBoxEdit1);
            this.groupBox1.Controls.Add(this.checkEdit1);
            this.groupBox1.Location = new Point(16, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(288, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "常规";
            this.simpleButton3.Location = new Point(176, 152);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(72, 24);
            this.simpleButton3.TabIndex = 4;
            this.simpleButton3.Text = "方向...";
            this.simpleButton2.Location = new Point(96, 152);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(72, 24);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "标注偏移...";
            this.simpleButton1.Location = new Point(8, 152);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(72, 24);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "位置...";
            this.imageComboBoxEdit1.EditValue = 0;
            this.imageComboBoxEdit1.Location = new Point(8, 24);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.imageComboBoxEdit1.Properties.Items.AddRange(new object[] { new ImageComboBoxItem("", 0, 0) });
            this.imageComboBoxEdit1.Properties.SmallImages = this.imageList1;
            this.imageComboBoxEdit1.Size = new Size(104, 62);
            this.imageComboBoxEdit1.TabIndex = 1;
            this.imageList1.ImageSize = new Size(81, 58);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.checkEdit1.Location = new Point(16, 96);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "首先尽量水平";
            this.checkEdit1.Size = new Size(120, 19);
            this.checkEdit1.TabIndex = 0;
            this.groupBox2.Controls.Add(this.simpleButton5);
            this.groupBox2.Controls.Add(this.checkEdit3);
            this.groupBox2.Controls.Add(this.pictureEdit2);
            this.groupBox2.Controls.Add(this.checkEdit2);
            this.groupBox2.Controls.Add(this.pictureEdit1);
            this.groupBox2.Location = new Point(16, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(288, 168);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.simpleButton5.Location = new Point(176, 98);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new Size(56, 24);
            this.simpleButton5.TabIndex = 6;
            this.simpleButton5.Text = "限制...";
            this.checkEdit3.Location = new Point(104, 98);
            this.checkEdit3.Name = "checkEdit3";
            this.checkEdit3.Properties.Caption = "展开字符";
            this.checkEdit3.Size = new Size(80, 19);
            this.checkEdit3.TabIndex = 5;
            this.pictureEdit2.Location = new Point(8, 83);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Size = new Size(81, 58);
            this.pictureEdit2.TabIndex = 4;
            this.checkEdit2.Location = new Point(104, 32);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "地块放置";
            this.checkEdit2.Size = new Size(80, 19);
            this.checkEdit2.TabIndex = 1;
            this.pictureEdit1.Location = new Point(8, 17);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new Size(81, 58);
            this.pictureEdit1.TabIndex = 0;
            this.checkEdit5.Location = new Point(16, 120);
            this.checkEdit5.Name = "checkEdit5";
            this.checkEdit5.Properties.Caption = "可以在多边形边界外放置标注";
            this.checkEdit5.Size = new Size(192, 19);
            this.checkEdit5.TabIndex = 5;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "FillMaplexLabelCtrl";
            base.Size = new Size(320, 432);
            this.groupBox1.ResumeLayout(false);
            this.imageComboBoxEdit1.Properties.EndInit();
            this.checkEdit1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.checkEdit3.Properties.EndInit();
            this.pictureEdit2.Properties.EndInit();
            this.checkEdit2.Properties.EndInit();
            this.pictureEdit1.Properties.EndInit();
            this.checkEdit5.Properties.EndInit();
            base.ResumeLayout(false);
        }
    
        private IContainer components;
        private CheckEdit checkEdit1;
        private CheckEdit checkEdit2;
        private CheckEdit checkEdit3;
        private CheckEdit checkEdit5;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ImageComboBoxEdit imageComboBoxEdit1;
        private ImageList imageList1;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private SimpleButton simpleButton3;
        private SimpleButton simpleButton5;
    }
}