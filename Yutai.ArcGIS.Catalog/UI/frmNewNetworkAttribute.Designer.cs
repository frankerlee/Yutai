using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class frmNewNetworkAttribute
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
            this.labelControl1 = new LabelControl();
            this.txtName = new TextEdit();
            this.labelControl2 = new LabelControl();
            this.labelControl3 = new LabelControl();
            this.labelControl4 = new LabelControl();
            this.cboUsageType = new ComboBoxEdit();
            this.cboUnit = new ComboBoxEdit();
            this.cboDataType = new ComboBoxEdit();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.txtName.Properties.BeginInit();
            this.cboUsageType.Properties.BeginInit();
            this.cboUnit.Properties.BeginInit();
            this.cboDataType.Properties.BeginInit();
            base.SuspendLayout();
            this.labelControl1.Location = new Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new Size(28, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "名字:";
            this.txtName.Location = new Point(72, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(141, 21);
            this.txtName.TabIndex = 1;
            this.txtName.EditValueChanged += new EventHandler(this.txtName_EditValueChanged);
            this.labelControl2.Location = new Point(12, 48);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new Size(48, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "使用方式";
            this.labelControl3.Location = new Point(12, 82);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new Size(24, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "单位";
            this.labelControl4.Location = new Point(12, 122);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new Size(48, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "数据类型";
            this.cboUsageType.EditValue = "Cost";
            this.cboUsageType.Location = new Point(72, 48);
            this.cboUsageType.Name = "cboUsageType";
            this.cboUsageType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUsageType.Properties.Items.AddRange(new object[] { "Cost", "Descriptor", "Restriction", "Hierarchy" });
            this.cboUsageType.Size = new Size(141, 21);
            this.cboUsageType.TabIndex = 5;
            this.cboUsageType.SelectedIndexChanged += new EventHandler(this.cboUsageType_SelectedIndexChanged);
            this.cboUnit.EditValue = "未知单位";
            this.cboUnit.Location = new Point(72, 82);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUnit.Properties.Items.AddRange(new object[] { "未知单位", "英寸", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "十进制度", "分米", "秒", "分钟", "小时", "天" });
            this.cboUnit.Size = new Size(141, 21);
            this.cboUnit.TabIndex = 6;
            this.cboDataType.Location = new Point(72, 115);
            this.cboDataType.Name = "cboDataType";
            this.cboDataType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDataType.Size = new Size(141, 21);
            this.cboDataType.TabIndex = 7;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(59, 153);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(163, 152);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(75, 23);
            this.simpleButton2.TabIndex = 9;
            this.simpleButton2.Text = "取消";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(271, 185);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.cboDataType);
            base.Controls.Add(this.cboUnit);
            base.Controls.Add(this.cboUsageType);
            base.Controls.Add(this.labelControl4);
            base.Controls.Add(this.labelControl3);
            base.Controls.Add(this.labelControl2);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.labelControl1);
            base.Name = "frmNewNetworkAttribute";
            this.Text = "新建网络属性";
            base.Load += new EventHandler(this.frmNewNetworkAttribute_Load);
            this.txtName.Properties.EndInit();
            this.cboUsageType.Properties.EndInit();
            this.cboUnit.Properties.EndInit();
            this.cboDataType.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnOK;
        private ComboBoxEdit cboDataType;
        private ComboBoxEdit cboUnit;
        private ComboBoxEdit cboUsageType;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LabelControl labelControl3;
        private LabelControl labelControl4;
        private SimpleButton simpleButton2;
        private TextEdit txtName;
    }
}