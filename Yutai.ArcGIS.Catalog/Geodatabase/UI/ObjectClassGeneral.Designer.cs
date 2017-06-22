using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class ObjectClassGeneral
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.cboRelateFC = new ComboBoxEdit();
            this.chkRelateFeatureClass = new CheckEdit();
            this.cboFeatureType = new ComboBoxEdit();
            this.label3 = new Label();
            this.radioGroup = new RadioGroup();
            this.txtName = new TextEdit();
            this.txtAliasName = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.cboRelateFC.Properties.BeginInit();
            this.chkRelateFeatureClass.Properties.BeginInit();
            this.cboFeatureType.Properties.BeginInit();
            this.radioGroup.Properties.BeginInit();
            this.txtName.Properties.BeginInit();
            this.txtAliasName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 47);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "别名";
            this.groupBox1.Controls.Add(this.cboRelateFC);
            this.groupBox1.Controls.Add(this.chkRelateFeatureClass);
            this.groupBox1.Controls.Add(this.cboFeatureType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.radioGroup);
            this.groupBox1.Location = new Point(24, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(304, 200);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "类型";
            this.cboRelateFC.EditValue = "";
            this.cboRelateFC.Location = new Point(40, 168);
            this.cboRelateFC.Name = "cboRelateFC";
            this.cboRelateFC.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRelateFC.Size = new Size(216, 21);
            this.cboRelateFC.TabIndex = 4;
            this.cboRelateFC.Visible = false;
            this.cboRelateFC.SelectedIndexChanged += new EventHandler(this.cboRelateFC_SelectedIndexChanged);
            this.chkRelateFeatureClass.Location = new Point(16, 144);
            this.chkRelateFeatureClass.Name = "chkRelateFeatureClass";
            this.chkRelateFeatureClass.Properties.Caption = "把注记连接到下面要素";
            this.chkRelateFeatureClass.Size = new Size(128, 19);
            this.chkRelateFeatureClass.TabIndex = 3;
            this.chkRelateFeatureClass.Visible = false;
            this.chkRelateFeatureClass.CheckedChanged += new EventHandler(this.chkRelateFeatureClass_CheckedChanged);
            this.cboFeatureType.EditValue = "ESRI注记要素";
            this.cboFeatureType.Location = new Point(32, 112);
            this.cboFeatureType.Name = "cboFeatureType";
            this.cboFeatureType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFeatureType.Properties.Items.AddRange(new object[] { "ESRI注记要素", "ESRI尺寸标注要素" });
            this.cboFeatureType.Size = new Size(216, 21);
            this.cboFeatureType.TabIndex = 2;
            this.cboFeatureType.SelectedIndexChanged += new EventHandler(this.cboFeatureType_SelectedIndexChanged);
            this.label3.Location = new Point(24, 88);
            this.label3.Name = "label3";
            this.label3.Size = new Size(248, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "选择要保存在该要素类中的定制对象类型";
            this.radioGroup.Location = new Point(8, 24);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "简单要素类型"), new RadioGroupItem(null, "该要素类保存注记要素,网络要素,维要素等对象") });
            this.radioGroup.Size = new Size(280, 64);
            this.radioGroup.TabIndex = 0;
            this.radioGroup.SelectedIndexChanged += new EventHandler(this.radioGroup_SelectedIndexChanged);
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(56, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(240, 21);
            this.txtName.TabIndex = 3;
            this.txtName.EditValueChanged += new EventHandler(this.txtName_EditValueChanged);
            this.txtAliasName.EditValue = "";
            this.txtAliasName.Location = new Point(56, 48);
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.Size = new Size(240, 21);
            this.txtAliasName.TabIndex = 4;
            this.txtAliasName.EditValueChanged += new EventHandler(this.txtAliasName_EditValueChanged);
            base.Controls.Add(this.txtAliasName);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "ObjectClassGeneral";
            base.Size = new Size(381, 313);
            base.Load += new EventHandler(this.ObjectClassGeneral_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboRelateFC.Properties.EndInit();
            this.chkRelateFeatureClass.Properties.EndInit();
            this.cboFeatureType.Properties.EndInit();
            this.radioGroup.Properties.EndInit();
            this.txtName.Properties.EndInit();
            this.txtAliasName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private ComboBoxEdit cboFeatureType;
        private ComboBoxEdit cboRelateFC;
        private CheckEdit chkRelateFeatureClass;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private RadioGroup radioGroup;
        private TextEdit txtAliasName;
        private TextEdit txtName;
    }
}