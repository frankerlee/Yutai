using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class LineMaplexLabelConficCtrl
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LineMaplexLabelConficCtrl));
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
            base.SuspendLayout();
            this.imageList1.ImageSize = new Size(81, 58);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
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
            this.groupBox2.Location = new Point(16, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(288, 352);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            int[] bits = new int[4];
            this.spinEdit2.EditValue = new decimal(bits);
            this.spinEdit2.Location = new Point(176, 296);
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEdit2.Properties.UseCtrlIncrement = false;
            this.spinEdit2.Size = new Size(72, 23);
            this.spinEdit2.TabIndex = 22;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(104, 296);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "标注缓冲:";
            this.pictureEdit3.Location = new Point(8, 280);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Size = new Size(81, 58);
            this.pictureEdit3.TabIndex = 20;
            bits = new int[4];
            this.spinEdit1.EditValue = new decimal(bits);
            this.spinEdit1.Location = new Point(176, 32);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEdit1.Properties.UseCtrlIncrement = false;
            this.spinEdit1.Size = new Size(72, 23);
            this.spinEdit1.TabIndex = 19;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(104, 32);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "要素权重:";
            this.pictureEdit2.Location = new Point(8, 16);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Size = new Size(81, 58);
            this.pictureEdit2.TabIndex = 17;
            this.checkEdit5.Location = new Point(104, 232);
            this.checkEdit5.Name = "checkEdit5";
            this.checkEdit5.Properties.Caption = "从不删除标注";
            this.checkEdit5.Size = new Size(104, 19);
            this.checkEdit5.TabIndex = 15;
            this.pictureEdit5.Location = new Point(8, 216);
            this.pictureEdit5.Name = "pictureEdit5";
            this.pictureEdit5.Size = new Size(81, 58);
            this.pictureEdit5.TabIndex = 14;
            this.simpleButton3.Location = new Point(216, 168);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(56, 24);
            this.simpleButton3.TabIndex = 13;
            this.simpleButton3.Text = "限制...";
            this.checkEdit4.Location = new Point(104, 168);
            this.checkEdit4.Name = "checkEdit4";
            this.checkEdit4.Properties.Caption = "删除重复的标注";
            this.checkEdit4.Size = new Size(104, 19);
            this.checkEdit4.TabIndex = 12;
            this.pictureEdit4.Location = new Point(8, 152);
            this.pictureEdit4.Name = "pictureEdit4";
            this.pictureEdit4.Size = new Size(81, 58);
            this.pictureEdit4.TabIndex = 11;
            this.checkEdit2.Location = new Point(104, 104);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "背景标注";
            this.checkEdit2.Size = new Size(80, 19);
            this.checkEdit2.TabIndex = 1;
            this.pictureEdit1.Location = new Point(8, 88);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new Size(81, 58);
            this.pictureEdit1.TabIndex = 0;
            base.Controls.Add(this.groupBox2);
            base.Name = "LineMaplexLabelConficCtrl";
            base.Size = new Size(320, 432);
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
            base.ResumeLayout(false);
        }

       
        private IContainer components;
        private CheckEdit checkEdit2;
        private CheckEdit checkEdit4;
        private CheckEdit checkEdit5;
        private GroupBox groupBox2;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private PictureEdit pictureEdit3;
        private PictureEdit pictureEdit4;
        private PictureEdit pictureEdit5;
        private SimpleButton simpleButton3;
        private SpinEdit spinEdit1;
        private SpinEdit spinEdit2;
    }
}