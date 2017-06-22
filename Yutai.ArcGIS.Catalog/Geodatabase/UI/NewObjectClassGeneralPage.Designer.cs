using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class NewObjectClassGeneralPage
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.txtAliasName = new TextEdit();
            this.txtName = new TextEdit();
            this.groupBox1 = new GroupBox();
            this.cboRelateFC = new ComboBoxEdit();
            this.cboFeatureType = new ComboBoxEdit();
            this.chkRelateFeatureClass = new CheckEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.chkHasZ = new CheckEdit();
            this.chkHasM = new CheckEdit();
            this.txtAliasName.Properties.BeginInit();
            this.txtName.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboRelateFC.Properties.BeginInit();
            this.cboFeatureType.Properties.BeginInit();
            this.chkRelateFeatureClass.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.chkHasZ.Properties.BeginInit();
            this.chkHasM.Properties.BeginInit();
            base.SuspendLayout();
            this.txtAliasName.EditValue = "";
            this.txtAliasName.Location = new System.Drawing.Point(48, 40);
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.Size = new Size(240, 21);
            this.txtAliasName.TabIndex = 9;
            this.txtAliasName.EditValueChanged += new EventHandler(this.txtAliasName_EditValueChanged);
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(48, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(240, 21);
            this.txtName.TabIndex = 8;
            this.groupBox1.Controls.Add(this.cboRelateFC);
            this.groupBox1.Controls.Add(this.cboFeatureType);
            this.groupBox1.Controls.Add(this.chkRelateFeatureClass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(304, 158);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "类型";
            this.cboRelateFC.Location = new System.Drawing.Point(16, 94);
            this.cboRelateFC.Name = "cboRelateFC";
            this.cboRelateFC.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRelateFC.Size = new Size(203, 21);
            this.cboRelateFC.TabIndex = 6;
            this.cboRelateFC.Visible = false;
            this.cboFeatureType.EditValue = "点要素";
            this.cboFeatureType.Location = new System.Drawing.Point(16, 41);
            this.cboFeatureType.Name = "cboFeatureType";
            this.cboFeatureType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFeatureType.Properties.Items.AddRange(new object[] { "点要素", "多点要素", "线要素", "多边形要素", "面片要素" });
            this.cboFeatureType.Size = new Size(203, 21);
            this.cboFeatureType.TabIndex = 5;
            this.cboFeatureType.SelectedIndexChanged += new EventHandler(this.cboFeatureType_SelectedIndexChanged);
            this.chkRelateFeatureClass.Location = new System.Drawing.Point(16, 68);
            this.chkRelateFeatureClass.Name = "chkRelateFeatureClass";
            this.chkRelateFeatureClass.Properties.Caption = "把注记连接到下面要素";
            this.chkRelateFeatureClass.Size = new Size(190, 19);
            this.chkRelateFeatureClass.TabIndex = 3;
            this.chkRelateFeatureClass.Visible = false;
            this.chkRelateFeatureClass.CheckedChanged += new EventHandler(this.chkRelateFeatureClass_CheckedChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 26);
            this.label3.Name = "label3";
            this.label3.Size = new Size(221, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "选择要保存在该要素类中的定制对象类型";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 39);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "别名";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "名称";
            this.groupBox2.Controls.Add(this.chkHasZ);
            this.groupBox2.Controls.Add(this.chkHasM);
            this.groupBox2.Location = new System.Drawing.Point(8, 239);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(304, 88);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "几何属性";
            this.chkHasZ.Location = new System.Drawing.Point(6, 45);
            this.chkHasZ.Name = "chkHasZ";
            this.chkHasZ.Properties.Caption = "包含Z值。用来存放三维值";
            this.chkHasZ.Size = new Size(190, 19);
            this.chkHasZ.TabIndex = 5;
            this.chkHasM.Location = new System.Drawing.Point(6, 20);
            this.chkHasM.Name = "chkHasM";
            this.chkHasM.Properties.Caption = "包含M值。用来存放路径值";
            this.chkHasM.Size = new Size(190, 19);
            this.chkHasM.TabIndex = 4;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.txtAliasName);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "NewObjectClassGeneralPage";
            base.Size = new Size(344, 371);
            base.Load += new EventHandler(this.NewObjectClassGeneralPage_Load);
            this.txtAliasName.Properties.EndInit();
            this.txtName.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cboRelateFC.Properties.EndInit();
            this.cboFeatureType.Properties.EndInit();
            this.chkRelateFeatureClass.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.chkHasZ.Properties.EndInit();
            this.chkHasM.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private ComboBoxEdit cboFeatureType;
        private ComboBoxEdit cboRelateFC;
        private CheckEdit chkHasM;
        private CheckEdit chkHasZ;
        private CheckEdit chkRelateFeatureClass;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextEdit txtAliasName;
        private TextEdit txtName;
    }
}