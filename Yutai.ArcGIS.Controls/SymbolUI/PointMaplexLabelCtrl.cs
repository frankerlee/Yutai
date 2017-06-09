﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class PointMaplexLabelCtrl : UserControl
    {
        private SimpleButton btnPointPP;
        private ImageComboBoxEdit cboPointPlacementMethod;
        private CheckEdit chkCanShiftPointLabel;
        private CheckEdit chkEnablePointPP;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ImageList imageList1;
        private bool m_CanDo = false;
        private IMaplexOverposterLayerProperties m_pMaplexOLP = null;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private SimpleButton simpleButton3;

        public PointMaplexLabelCtrl()
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PointMaplexLabelCtrl));
            this.groupBox1 = new GroupBox();
            this.simpleButton3 = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.simpleButton1 = new SimpleButton();
            this.cboPointPlacementMethod = new ImageComboBoxEdit();
            this.imageList1 = new ImageList(this.components);
            this.groupBox2 = new GroupBox();
            this.chkCanShiftPointLabel = new CheckEdit();
            this.pictureEdit2 = new PictureEdit();
            this.btnPointPP = new SimpleButton();
            this.chkEnablePointPP = new CheckEdit();
            this.pictureEdit1 = new PictureEdit();
            this.groupBox1.SuspendLayout();
            this.cboPointPlacementMethod.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.chkCanShiftPointLabel.Properties.BeginInit();
            this.pictureEdit2.Properties.BeginInit();
            this.chkEnablePointPP.Properties.BeginInit();
            this.pictureEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.simpleButton3);
            this.groupBox1.Controls.Add(this.simpleButton2);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Controls.Add(this.cboPointPlacementMethod);
            this.groupBox1.Location = new Point(0x10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x120, 0xa8);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "常规";
            this.simpleButton3.Location = new Point(0xb0, 0x80);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(0x48, 0x18);
            this.simpleButton3.TabIndex = 4;
            this.simpleButton3.Text = "方向...";
            this.simpleButton2.Location = new Point(0x60, 0x80);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x48, 0x18);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "标注偏移...";
            this.simpleButton1.Location = new Point(8, 0x80);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x48, 0x18);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "位置...";
            this.cboPointPlacementMethod.EditValue = 0;
            this.cboPointPlacementMethod.Location = new Point(8, 0x30);
            this.cboPointPlacementMethod.Name = "cboPointPlacementMethod";
            this.cboPointPlacementMethod.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboPointPlacementMethod.Properties.Items.AddRange(new object[] { new ImageComboBoxItem("", 0, 0) });
            this.cboPointPlacementMethod.Properties.SmallImages = this.imageList1;
            this.cboPointPlacementMethod.Size = new Size(0x68, 0x3e);
            this.cboPointPlacementMethod.TabIndex = 1;
            this.imageList1.ImageSize = new Size(0x51, 0x3a);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.groupBox2.Controls.Add(this.chkCanShiftPointLabel);
            this.groupBox2.Controls.Add(this.pictureEdit2);
            this.groupBox2.Controls.Add(this.btnPointPP);
            this.groupBox2.Controls.Add(this.chkEnablePointPP);
            this.groupBox2.Controls.Add(this.pictureEdit1);
            this.groupBox2.Location = new Point(0x10, 0xb8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x120, 0x98);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.chkCanShiftPointLabel.Location = new Point(0x60, 0x62);
            this.chkCanShiftPointLabel.Name = "chkCanShiftPointLabel";
            this.chkCanShiftPointLabel.Properties.Caption = "跟据固定的位置移动标注";
            this.chkCanShiftPointLabel.Size = new Size(160, 0x13);
            this.chkCanShiftPointLabel.TabIndex = 5;
            this.pictureEdit2.Location = new Point(8, 0x53);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Size = new Size(0x51, 0x3a);
            this.pictureEdit2.TabIndex = 4;
            this.btnPointPP.Location = new Point(0xe0, 0x20);
            this.btnPointPP.Name = "btnPointPP";
            this.btnPointPP.Size = new Size(0x38, 0x18);
            this.btnPointPP.TabIndex = 3;
            this.btnPointPP.Text = "区域...";
            this.chkEnablePointPP.Location = new Point(0x60, 0x20);
            this.chkEnablePointPP.Name = "chkEnablePointPP";
            this.chkEnablePointPP.Properties.Caption = "用户定义的区域";
            this.chkEnablePointPP.Size = new Size(0x70, 0x13);
            this.chkEnablePointPP.TabIndex = 1;
            this.pictureEdit1.Location = new Point(8, 0x11);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new Size(0x51, 0x3a);
            this.pictureEdit1.TabIndex = 0;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "PointMaplexLabelCtrl";
            base.Size = new Size(0x148, 0x1b0);
            base.Load += new EventHandler(this.PointMaplexLabelCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboPointPlacementMethod.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.chkCanShiftPointLabel.Properties.EndInit();
            this.pictureEdit2.Properties.EndInit();
            this.chkEnablePointPP.Properties.EndInit();
            this.pictureEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void PointMaplexLabelCtrl_Load(object sender, EventArgs e)
        {
            if (this.m_pMaplexOLP != null)
            {
            }
        }
    }
}
