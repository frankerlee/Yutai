using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmTable2Domain
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTable2Domain));
            this.label1 = new Label();
            this.txtDomainName = new TextEdit();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.cboNameField = new ComboBoxEdit();
            this.cdoCodeField = new ComboBoxEdit();
            this.cboMergePolicy = new ComboBoxEdit();
            this.cboSplitPolicy = new ComboBoxEdit();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.label6 = new Label();
            this.txtTable = new TextEdit();
            this.btnSelectInputFeatures = new SimpleButton();
            this.label7 = new Label();
            this.txtDes = new MemoEdit();
            this.txtDomainName.Properties.BeginInit();
            this.cboNameField.Properties.BeginInit();
            this.cdoCodeField.Properties.BeginInit();
            this.cboMergePolicy.Properties.BeginInit();
            this.cboSplitPolicy.Properties.BeginInit();
            this.txtTable.Properties.BeginInit();
            this.txtDes.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 42);
            this.label1.Name = "label1";
            this.label1.Size = new Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "域值名称:";
            this.txtDomainName.EditValue = "";
            this.txtDomainName.Location = new Point(72, 40);
            this.txtDomainName.Name = "txtDomainName";
            this.txtDomainName.Size = new Size(272, 21);
            this.txtDomainName.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 160);
            this.label2.Name = "label2";
            this.label2.Size = new Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "名称字段:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(184, 158);
            this.label3.Name = "label3";
            this.label3.Size = new Size(59, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "代码字段:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 192);
            this.label4.Name = "label4";
            this.label4.Size = new Size(59, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "合并策略:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(184, 185);
            this.label5.Name = "label5";
            this.label5.Size = new Size(59, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "拆分策略:";
            this.cboNameField.EditValue = "";
            this.cboNameField.Location = new Point(72, 152);
            this.cboNameField.Name = "cboNameField";
            this.cboNameField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboNameField.Size = new Size(104, 21);
            this.cboNameField.TabIndex = 6;
            this.cdoCodeField.EditValue = "";
            this.cdoCodeField.Location = new Point(248, 152);
            this.cdoCodeField.Name = "cdoCodeField";
            this.cdoCodeField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cdoCodeField.Size = new Size(104, 21);
            this.cdoCodeField.TabIndex = 7;
            this.cboMergePolicy.EditValue = "默认值";
            this.cboMergePolicy.Location = new Point(72, 184);
            this.cboMergePolicy.Name = "cboMergePolicy";
            this.cboMergePolicy.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboMergePolicy.Properties.Items.AddRange(new object[] { "默认值", "总和值", "加权平均" });
            this.cboMergePolicy.Size = new Size(104, 21);
            this.cboMergePolicy.TabIndex = 8;
            this.cboSplitPolicy.EditValue = "默认值";
            this.cboSplitPolicy.Location = new Point(248, 184);
            this.cboSplitPolicy.Name = "cboSplitPolicy";
            this.cboSplitPolicy.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSplitPolicy.Properties.Items.AddRange(new object[] { "默认值", "复制", "几何比例" });
            this.cboSplitPolicy.Size = new Size(104, 21);
            this.cboSplitPolicy.TabIndex = 9;
            this.btnOK.Location = new Point(208, 224);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(288, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(8, 10);
            this.label6.Name = "label6";
            this.label6.Size = new Size(47, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "选择表:";
            this.txtTable.EditValue = "";
            this.txtTable.Location = new Point(72, 8);
            this.txtTable.Name = "txtTable";
            this.txtTable.Properties.ReadOnly = true;
            this.txtTable.Size = new Size(272, 21);
            this.txtTable.TabIndex = 13;
            this.btnSelectInputFeatures.Image = (Image) resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(352, 8);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(24, 24);
            this.btnSelectInputFeatures.TabIndex = 14;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(8, 72);
            this.label7.Name = "label7";
            this.label7.Size = new Size(59, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "域值说明:";
            this.txtDes.EditValue = "";
            this.txtDes.Location = new Point(72, 72);
            this.txtDes.Name = "txtDes";
            this.txtDes.Size = new Size(272, 72);
            this.txtDes.TabIndex = 16;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(386, 271);
            base.Controls.Add(this.txtDes);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.txtTable);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.cboSplitPolicy);
            base.Controls.Add(this.cboMergePolicy);
            base.Controls.Add(this.cdoCodeField);
            base.Controls.Add(this.cboNameField);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtDomainName);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmTable2Domain";
            this.Text = "创建代码用域值";
            base.Load += new EventHandler(this.frmTable2Domain_Load);
            this.txtDomainName.Properties.EndInit();
            this.cboNameField.Properties.EndInit();
            this.cdoCodeField.Properties.EndInit();
            this.cboMergePolicy.Properties.EndInit();
            this.cboSplitPolicy.Properties.EndInit();
            this.txtTable.Properties.EndInit();
            this.txtDes.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnSelectInputFeatures;
        private ComboBoxEdit cboMergePolicy;
        private ComboBoxEdit cboNameField;
        private ComboBoxEdit cboSplitPolicy;
        private ComboBoxEdit cdoCodeField;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private MemoEdit txtDes;
        private TextEdit txtDomainName;
        private TextEdit txtTable;
    }
}