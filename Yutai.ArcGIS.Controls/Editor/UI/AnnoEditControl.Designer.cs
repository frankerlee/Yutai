using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class AnnoEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnnoEditControl));
            this.memoEdit1 = new MemoEdit();
            this.groupBox1 = new GroupBox();
            this.cboFontSize = new ComboBoxEdit();
            this.colorEdit1 = new ColorEdit();
            this.rdoTHAFul = new RadioButton();
            this.imageList1 = new ImageList(this.components);
            this.rdoTHALeft = new RadioButton();
            this.rdoTHACenter = new RadioButton();
            this.rdoTHARight = new RadioButton();
            this.chkUnderline = new CheckBox();
            this.chkItalic = new CheckBox();
            this.chkBold = new CheckBox();
            this.cboFontName = new System.Windows.Forms.ComboBox();
            this.btnApply = new SimpleButton();
            this.btnReset = new SimpleButton();
            this.memoEdit1.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboFontSize.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.memoEdit1.EditValue = "";
            this.memoEdit1.Location = new Point(8, 8);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new Size(232, 88);
            this.memoEdit1.TabIndex = 0;
            this.memoEdit1.EditValueChanged += new EventHandler(this.memoEdit1_EditValueChanged);
            this.groupBox1.Controls.Add(this.cboFontSize);
            this.groupBox1.Controls.Add(this.colorEdit1);
            this.groupBox1.Controls.Add(this.rdoTHAFul);
            this.groupBox1.Controls.Add(this.rdoTHALeft);
            this.groupBox1.Controls.Add(this.rdoTHACenter);
            this.groupBox1.Controls.Add(this.rdoTHARight);
            this.groupBox1.Controls.Add(this.chkUnderline);
            this.groupBox1.Controls.Add(this.chkItalic);
            this.groupBox1.Controls.Add(this.chkBold);
            this.groupBox1.Controls.Add(this.cboFontName);
            this.groupBox1.Location = new Point(8, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(280, 88);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.cboFontSize.EditValue = "5";
            this.cboFontSize.Location = new Point(200, 17);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontSize.Properties.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Size = new Size(64, 21);
            this.cboFontSize.TabIndex = 14;
            this.cboFontSize.EditValueChanged += new EventHandler(this.cboFontSize_EditValueChanged);
            this.cboFontSize.SelectedIndexChanged += new EventHandler(this.cboFontSize_SelectedIndexChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(8, 48);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 21);
            this.colorEdit1.TabIndex = 13;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.rdoTHAFul.Appearance = Appearance.Button;
            this.rdoTHAFul.ImageIndex = 6;
            this.rdoTHAFul.ImageList = this.imageList1;
            this.rdoTHAFul.Location = new Point(244, 45);
            this.rdoTHAFul.Name = "rdoTHAFul";
            this.rdoTHAFul.Size = new Size(28, 24);
            this.rdoTHAFul.TabIndex = 11;
            this.rdoTHAFul.Click += new EventHandler(this.rdoTHAFul_Click);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.rdoTHALeft.Appearance = Appearance.Button;
            this.rdoTHALeft.ImageIndex = 3;
            this.rdoTHALeft.ImageList = this.imageList1;
            this.rdoTHALeft.Location = new Point(160, 45);
            this.rdoTHALeft.Name = "rdoTHALeft";
            this.rdoTHALeft.Size = new Size(28, 24);
            this.rdoTHALeft.TabIndex = 10;
            this.rdoTHALeft.Click += new EventHandler(this.rdoTHALeft_Click);
            this.rdoTHACenter.Appearance = Appearance.Button;
            this.rdoTHACenter.ImageIndex = 4;
            this.rdoTHACenter.ImageList = this.imageList1;
            this.rdoTHACenter.Location = new Point(188, 45);
            this.rdoTHACenter.Name = "rdoTHACenter";
            this.rdoTHACenter.Size = new Size(28, 24);
            this.rdoTHACenter.TabIndex = 9;
            this.rdoTHACenter.Click += new EventHandler(this.rdoTHACenter_Click);
            this.rdoTHARight.Appearance = Appearance.Button;
            this.rdoTHARight.ImageIndex = 5;
            this.rdoTHARight.ImageList = this.imageList1;
            this.rdoTHARight.Location = new Point(216, 45);
            this.rdoTHARight.Name = "rdoTHARight";
            this.rdoTHARight.Size = new Size(28, 24);
            this.rdoTHARight.TabIndex = 8;
            this.rdoTHARight.TabStop = true;
            this.rdoTHARight.Click += new EventHandler(this.rdoTHARight_Click);
            this.chkUnderline.Appearance = Appearance.Button;
            this.chkUnderline.ImageIndex = 2;
            this.chkUnderline.ImageList = this.imageList1;
            this.chkUnderline.Location = new Point(120, 45);
            this.chkUnderline.Name = "chkUnderline";
            this.chkUnderline.Size = new Size(28, 24);
            this.chkUnderline.TabIndex = 7;
            this.chkUnderline.Click += new EventHandler(this.chkUnderline_Click);
            this.chkItalic.Appearance = Appearance.Button;
            this.chkItalic.ImageIndex = 1;
            this.chkItalic.ImageList = this.imageList1;
            this.chkItalic.Location = new Point(92, 45);
            this.chkItalic.Name = "chkItalic";
            this.chkItalic.Size = new Size(28, 24);
            this.chkItalic.TabIndex = 6;
            this.chkItalic.Click += new EventHandler(this.chkItalic_Click);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList1;
            this.chkBold.Location = new Point(64, 45);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(28, 24);
            this.chkBold.TabIndex = 5;
            this.chkBold.Click += new EventHandler(this.chkBold_Click);
            this.cboFontName.Location = new Point(8, 16);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(184, 20);
            this.cboFontName.TabIndex = 2;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.btnApply.Location = new Point(256, 16);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(48, 24);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnReset.Location = new Point(256, 48);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new Size(48, 24);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "重置";
            this.btnReset.Click += new EventHandler(this.btnReset_Click);
            base.Controls.Add(this.btnReset);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.memoEdit1);
            base.Name = "AnnoEditControl";
            base.Size = new Size(312, 208);
            base.Load += new EventHandler(this.AnnoEditControl_Load);
            this.memoEdit1.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.cboFontSize.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private IContainer components;
        private SimpleButton btnApply;
        private SimpleButton btnReset;
        private System.Windows.Forms.ComboBox cboFontName;
        private ComboBoxEdit cboFontSize;
        private CheckBox chkBold;
        private CheckBox chkItalic;
        private CheckBox chkUnderline;
        private ColorEdit colorEdit1;
        private GroupBox groupBox1;
        private ImageList imageList1;
        private MemoEdit memoEdit1;
        private RadioButton rdoTHACenter;
        private RadioButton rdoTHAFul;
        private RadioButton rdoTHALeft;
        private RadioButton rdoTHARight;
    }
}