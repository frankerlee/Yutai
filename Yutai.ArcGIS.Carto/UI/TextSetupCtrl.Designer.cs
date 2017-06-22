using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class TextSetupCtrl
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextSetupCtrl));
            this.label1 = new Label();
            this.txtString = new MemoEdit();
            this.rdoTHAFul = new RadioButton();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.rdoTHALeft = new RadioButton();
            this.rdoTHACenter = new RadioButton();
            this.rdoTHARight = new RadioButton();
            this.label2 = new Label();
            this.txtFontInfo = new TextEdit();
            this.label3 = new Label();
            this.txtAngle = new SpinEdit();
            this.txtCharacterSpace = new SpinEdit();
            this.label4 = new Label();
            this.txtLeading = new SpinEdit();
            this.label5 = new Label();
            this.btnSymbolSelector = new SimpleButton();
            this.txtString.Properties.BeginInit();
            this.txtFontInfo.Properties.BeginInit();
            this.txtAngle.Properties.BeginInit();
            this.txtCharacterSpace.Properties.BeginInit();
            this.txtLeading.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "文本:";
            this.txtString.EditValue = "";
            this.txtString.Location = new Point(16, 32);
            this.txtString.Name = "txtString";
            this.txtString.Size = new Size(304, 104);
            this.txtString.TabIndex = 1;
            this.txtString.EditValueChanged += new EventHandler(this.txtString_EditValueChanged);
            this.rdoTHAFul.Appearance = Appearance.Button;
            this.rdoTHAFul.ImageIndex = 6;
            this.rdoTHAFul.ImageList = this.imageList_0;
            this.rdoTHAFul.Location = new Point(296, 144);
            this.rdoTHAFul.Name = "rdoTHAFul";
            this.rdoTHAFul.Size = new Size(28, 24);
            this.rdoTHAFul.TabIndex = 15;
            this.rdoTHAFul.Click += new EventHandler(this.rdoTHAFul_Click);
            this.imageList_0.ImageSize = new Size(16, 16);
            this.imageList_0.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Magenta;
            this.rdoTHALeft.Appearance = Appearance.Button;
            this.rdoTHALeft.ImageIndex = 3;
            this.rdoTHALeft.ImageList = this.imageList_0;
            this.rdoTHALeft.Location = new Point(200, 144);
            this.rdoTHALeft.Name = "rdoTHALeft";
            this.rdoTHALeft.Size = new Size(28, 24);
            this.rdoTHALeft.TabIndex = 14;
            this.rdoTHALeft.Click += new EventHandler(this.rdoTHALeft_Click);
            this.rdoTHACenter.Appearance = Appearance.Button;
            this.rdoTHACenter.ImageIndex = 4;
            this.rdoTHACenter.ImageList = this.imageList_0;
            this.rdoTHACenter.Location = new Point(232, 144);
            this.rdoTHACenter.Name = "rdoTHACenter";
            this.rdoTHACenter.Size = new Size(28, 24);
            this.rdoTHACenter.TabIndex = 13;
            this.rdoTHACenter.Click += new EventHandler(this.rdoTHACenter_Click);
            this.rdoTHARight.Appearance = Appearance.Button;
            this.rdoTHARight.ImageIndex = 5;
            this.rdoTHARight.ImageList = this.imageList_0;
            this.rdoTHARight.Location = new Point(264, 144);
            this.rdoTHARight.Name = "rdoTHARight";
            this.rdoTHARight.Size = new Size(28, 24);
            this.rdoTHARight.TabIndex = 12;
            this.rdoTHARight.TabStop = true;
            this.rdoTHARight.Click += new EventHandler(this.rdoTHARight_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 144);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "字体:";
            this.txtFontInfo.EditValue = "";
            this.txtFontInfo.Location = new Point(48, 144);
            this.txtFontInfo.Name = "txtFontInfo";
            this.txtFontInfo.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.txtFontInfo.Properties.Appearance.Options.UseBackColor = true;
            this.txtFontInfo.Properties.ReadOnly = true;
            this.txtFontInfo.Size = new Size(128, 23);
            this.txtFontInfo.TabIndex = 17;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 184);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "角度:";
            int[] bits = new int[4];
            this.txtAngle.EditValue = new decimal(bits);
            this.txtAngle.Location = new Point(48, 176);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            int[] bits2 = new int[4];
            bits2[0] = 360;
            this.txtAngle.Properties.MaxValue = new decimal(bits2);
            int[] bits3 = new int[4];
            bits3[0] = 360;
            bits3[3] = -2147483648;
            this.txtAngle.Properties.MinValue = new decimal(bits3);
            this.txtAngle.Properties.UseCtrlIncrement = false;
            this.txtAngle.Size = new Size(72, 23);
            this.txtAngle.TabIndex = 19;
            this.txtAngle.EditValueChanged += new EventHandler(this.txtAngle_EditValueChanged);
            int[] bits4 = new int[4];
            this.txtCharacterSpace.EditValue = new decimal(bits4);
            this.txtCharacterSpace.Location = new Point(248, 176);
            this.txtCharacterSpace.Name = "txtCharacterSpace";
            this.txtCharacterSpace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            int[] bits5 = new int[4];
            bits5[0] = 360;
            this.txtCharacterSpace.Properties.MaxValue = new decimal(bits5);
            int[] bits6 = new int[4];
            bits6[0] = 360;
            bits6[3] = -2147483648;
            this.txtCharacterSpace.Properties.MinValue = new decimal(bits6);
            this.txtCharacterSpace.Properties.UseCtrlIncrement = false;
            this.txtCharacterSpace.Size = new Size(72, 23);
            this.txtCharacterSpace.TabIndex = 21;
            this.txtCharacterSpace.EditValueChanged += new EventHandler(this.txtCharacterSpace_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(184, 176);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "字符间距:";
            int[] bits7 = new int[4];
            this.txtLeading.EditValue = new decimal(bits7);
            this.txtLeading.Location = new Point(248, 208);
            this.txtLeading.Name = "txtLeading";
            this.txtLeading.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            int[] bits8 = new int[4];
            bits8[0] = 360;
            this.txtLeading.Properties.MaxValue = new decimal(bits8);
            int[] bits9 = new int[4];
            bits9[0] = 360;
            bits9[3] = -2147483648;
            this.txtLeading.Properties.MinValue = new decimal(bits9);
            this.txtLeading.Properties.UseCtrlIncrement = false;
            this.txtLeading.Size = new Size(72, 23);
            this.txtLeading.TabIndex = 23;
            this.txtLeading.EditValueChanged += new EventHandler(this.txtLeading_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(192, 208);
            this.label5.Name = "label5";
            this.label5.Size = new Size(48, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "行间距:";
            this.btnSymbolSelector.Location = new Point(200, 240);
            this.btnSymbolSelector.Name = "btnSymbolSelector";
            this.btnSymbolSelector.Size = new Size(72, 24);
            this.btnSymbolSelector.TabIndex = 24;
            this.btnSymbolSelector.Text = "更改符号";
            this.btnSymbolSelector.Click += new EventHandler(this.btnSymbolSelector_Click);
            base.Controls.Add(this.btnSymbolSelector);
            base.Controls.Add(this.txtLeading);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtCharacterSpace);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtAngle);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtFontInfo);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.rdoTHAFul);
            base.Controls.Add(this.rdoTHALeft);
            base.Controls.Add(this.rdoTHACenter);
            base.Controls.Add(this.rdoTHARight);
            base.Controls.Add(this.txtString);
            base.Controls.Add(this.label1);
            base.Name = "TextSetupCtrl";
            base.Size = new Size(344, 280);
            base.Load += new EventHandler(this.TextSetupCtrl_Load);
            this.txtString.Properties.EndInit();
            this.txtFontInfo.Properties.EndInit();
            this.txtAngle.Properties.EndInit();
            this.txtCharacterSpace.Properties.EndInit();
            this.txtLeading.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private SimpleButton btnSymbolSelector;
        private esriTextHorizontalAlignment esriTextHorizontalAlignment_0;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private RadioButton rdoTHACenter;
        private RadioButton rdoTHAFul;
        private RadioButton rdoTHALeft;
        private RadioButton rdoTHARight;
        private SpinEdit txtAngle;
        private SpinEdit txtCharacterSpace;
        private TextEdit txtFontInfo;
        private SpinEdit txtLeading;
        private MemoEdit txtString;
    }
}