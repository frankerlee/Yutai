using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmRuleInfo
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
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRuleInfo));
            this.label2 = new Label();
            this.btnOK = new Button();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.groupBox1 = new GroupBox();
            this.lblTopoInfo2 = new Label();
            this.lblTopoInfo1 = new Label();
            this.chkShowError = new CheckEdit();
            this.imageComboBoxEdit1 = new ImageComboBoxEdit();
            this.txtRuleName = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.chkShowError.Properties.BeginInit();
            this.imageComboBoxEdit1.Properties.BeginInit();
            this.txtRuleName.Properties.BeginInit();
            base.SuspendLayout();
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "规则";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(216, 216);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "关闭";
            this.imageList_0.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "");
            this.imageList_0.Images.SetKeyName(1, "");
            this.imageList_0.Images.SetKeyName(2, "");
            this.imageList_0.Images.SetKeyName(3, "");
            this.imageList_0.Images.SetKeyName(4, "");
            this.imageList_0.Images.SetKeyName(5, "");
            this.imageList_0.Images.SetKeyName(6, "");
            this.imageList_0.Images.SetKeyName(7, "");
            this.imageList_0.Images.SetKeyName(8, "");
            this.imageList_0.Images.SetKeyName(9, "");
            this.imageList_0.Images.SetKeyName(10, "");
            this.imageList_0.Images.SetKeyName(11, "");
            this.imageList_0.Images.SetKeyName(12, "");
            this.imageList_0.Images.SetKeyName(13, "");
            this.imageList_0.Images.SetKeyName(14, "");
            this.imageList_0.Images.SetKeyName(15, "");
            this.imageList_0.Images.SetKeyName(16, "");
            this.imageList_0.Images.SetKeyName(17, "");
            this.imageList_0.Images.SetKeyName(18, "");
            this.imageList_0.Images.SetKeyName(19, "");
            this.imageList_0.Images.SetKeyName(20, "");
            this.imageList_0.Images.SetKeyName(21, "");
            this.imageList_0.Images.SetKeyName(22, "");
            this.imageList_0.Images.SetKeyName(23, "");
            this.imageList_0.Images.SetKeyName(24, "");
            this.imageList_0.Images.SetKeyName(25, "");
            this.imageList_0.Images.SetKeyName(26, "");
            this.imageList_0.Images.SetKeyName(27, "");
            this.imageList_0.Images.SetKeyName(28, "");
            this.imageList_0.Images.SetKeyName(29, "");
            this.imageList_0.Images.SetKeyName(30, "");
            this.imageList_0.Images.SetKeyName(31, "");
            this.imageList_0.Images.SetKeyName(32, "");
            this.imageList_0.Images.SetKeyName(33, "");
            this.imageList_0.Images.SetKeyName(34, "");
            this.imageList_0.Images.SetKeyName(35, "");
            this.imageList_0.Images.SetKeyName(36, "");
            this.imageList_0.Images.SetKeyName(37, "");
            this.imageList_0.Images.SetKeyName(38, "");
            this.imageList_0.Images.SetKeyName(39, "");
            this.imageList_0.Images.SetKeyName(40, "");
            this.imageList_0.Images.SetKeyName(41, "");
            this.imageList_0.Images.SetKeyName(42, "");
            this.imageList_0.Images.SetKeyName(43, "");
            this.imageList_0.Images.SetKeyName(44, "");
            this.imageList_0.Images.SetKeyName(45, "");
            this.imageList_0.Images.SetKeyName(46, "");
            this.imageList_0.Images.SetKeyName(47, "");
            this.imageList_0.Images.SetKeyName(48, "");
            this.imageList_0.Images.SetKeyName(49, "");
            this.groupBox1.Controls.Add(this.lblTopoInfo2);
            this.groupBox1.Controls.Add(this.lblTopoInfo1);
            this.groupBox1.Controls.Add(this.chkShowError);
            this.groupBox1.Controls.Add(this.imageComboBoxEdit1);
            this.groupBox1.Location = new Point(16, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(272, 176);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "规则描述";
            this.lblTopoInfo2.Location = new Point(104, 104);
            this.lblTopoInfo2.Name = "lblTopoInfo2";
            this.lblTopoInfo2.Size = new Size(160, 48);
            this.lblTopoInfo2.TabIndex = 13;
            this.lblTopoInfo1.Location = new Point(104, 32);
            this.lblTopoInfo1.Name = "lblTopoInfo1";
            this.lblTopoInfo1.Size = new Size(160, 48);
            this.lblTopoInfo1.TabIndex = 12;
            this.chkShowError.EditValue = true;
            this.chkShowError.Location = new Point(8, 152);
            this.chkShowError.Name = "chkShowError";
            this.chkShowError.Properties.Caption = "显示错误";
            this.chkShowError.Size = new Size(75, 19);
            this.chkShowError.TabIndex = 11;
            this.chkShowError.CheckedChanged += new EventHandler(this.chkShowError_CheckedChanged);
            this.imageComboBoxEdit1.EditValue = 0;
            this.imageComboBoxEdit1.Location = new Point(9, 24);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.imageComboBoxEdit1.Properties.Items.AddRange(new ImageComboBoxItem[] { 
                new ImageComboBoxItem("", 0, 0), new ImageComboBoxItem("", 1, 1), new ImageComboBoxItem("", 2, 2), new ImageComboBoxItem("", 3, 3), new ImageComboBoxItem("", 4, 4), new ImageComboBoxItem("", 5, 5), new ImageComboBoxItem("", 6, 6), new ImageComboBoxItem("", 7, 7), new ImageComboBoxItem("", 8, 8), new ImageComboBoxItem("", 9, 9), new ImageComboBoxItem("", 10, 10), new ImageComboBoxItem("", 11, 11), new ImageComboBoxItem("", 12, 12), new ImageComboBoxItem("", 13, 13), new ImageComboBoxItem("", 14, 14), new ImageComboBoxItem("", 15, 15), 
                new ImageComboBoxItem("", 16, 16), new ImageComboBoxItem("", 17, 17), new ImageComboBoxItem("", 18, 18), new ImageComboBoxItem("", 19, 19), new ImageComboBoxItem("", 20, 20), new ImageComboBoxItem("", 21, 21), new ImageComboBoxItem("", 22, 22), new ImageComboBoxItem("", 23, 23), new ImageComboBoxItem("", 24, 24), new ImageComboBoxItem("", 25, 25), new ImageComboBoxItem("", 26, 26), new ImageComboBoxItem("", 27, 27), new ImageComboBoxItem("", 28, 28), new ImageComboBoxItem("", 29, 29), new ImageComboBoxItem("", 30, 30), new ImageComboBoxItem("", 31, 31), 
                new ImageComboBoxItem("", 32, 32), new ImageComboBoxItem("", 33, 33), new ImageComboBoxItem("", 34, 34), new ImageComboBoxItem("", 35, 35), new ImageComboBoxItem("", 36, 36), new ImageComboBoxItem("", 37, 37), new ImageComboBoxItem("", 38, 38), new ImageComboBoxItem("", 39, 39), new ImageComboBoxItem("", 40, 40), new ImageComboBoxItem("", 41, 41), new ImageComboBoxItem("", 42, 42), new ImageComboBoxItem("", 43, 43), new ImageComboBoxItem("", 44, 44), new ImageComboBoxItem("", 45, 45), new ImageComboBoxItem("", 46, 46), new ImageComboBoxItem("", 47, 47), 
                new ImageComboBoxItem("", 48, 48), new ImageComboBoxItem("", 49, 49)
             });
            this.imageComboBoxEdit1.Properties.LargeImages = this.imageList_0;
            this.imageComboBoxEdit1.Properties.ReadOnly = true;
            this.imageComboBoxEdit1.Properties.ShowDropDown = ShowDropDown.Never;
            this.imageComboBoxEdit1.Properties.SmallImages = this.imageList_0;
            this.imageComboBoxEdit1.Size = new Size(88, 122);
            this.imageComboBoxEdit1.TabIndex = 9;
            this.txtRuleName.EditValue = "";
            this.txtRuleName.Location = new Point(56, 5);
            this.txtRuleName.Name = "txtRuleName";
            this.txtRuleName.Properties.ReadOnly = true;
            this.txtRuleName.Size = new Size(224, 21);
            this.txtRuleName.TabIndex = 10;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(296, 245);
            base.Controls.Add(this.txtRuleName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label2);
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRuleInfo";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "规则属性";
            base.Load += new EventHandler(this.frmRuleInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.chkShowError.Properties.EndInit();
            this.imageComboBoxEdit1.Properties.EndInit();
            this.txtRuleName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnOK;
        private CheckEdit chkShowError;
        private esriTopologyRuleType esriTopologyRuleType_3;
        private GroupBox groupBox1;
        private IContainer icontainer_0;
        private ImageComboBoxEdit imageComboBoxEdit1;
        private ImageList imageList_0;
        private int int_0;
        private Label label2;
        private Label lblTopoInfo1;
        private Label lblTopoInfo2;
        private TextEdit txtRuleName;
    }
}