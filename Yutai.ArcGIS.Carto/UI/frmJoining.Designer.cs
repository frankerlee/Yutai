using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;


namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmJoining
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJoining));
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.cboJoiningTableField = new ComboBoxEdit();
            this.cboJoiningTable = new ComboBoxEdit();
            this.cboJoiningField = new ComboBoxEdit();
            this.btnJoiningType = new SimpleButton();
            this.btnOpenTable = new SimpleButton();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.cboJoiningDataType = new ComboBoxEdit();
            this.groupBox1.SuspendLayout();
            this.cboJoiningTableField.Properties.BeginInit();
            this.cboJoiningTable.Properties.BeginInit();
            this.cboJoiningField.Properties.BeginInit();
            this.cboJoiningDataType.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(125, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "连接到当前图层的数据";
            this.groupBox1.Controls.Add(this.cboJoiningTableField);
            this.groupBox1.Controls.Add(this.cboJoiningTable);
            this.groupBox1.Controls.Add(this.cboJoiningField);
            this.groupBox1.Controls.Add(this.btnJoiningType);
            this.groupBox1.Controls.Add(this.btnOpenTable);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new Point(8, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(272, 248);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.cboJoiningTableField.EditValue = "";
            this.cboJoiningTableField.Location = new Point(8, 160);
            this.cboJoiningTableField.Name = "cboJoiningTableField";
            this.cboJoiningTableField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJoiningTableField.Size = new Size(240, 21);
            this.cboJoiningTableField.TabIndex = 10;
            this.cboJoiningTableField.SelectedIndexChanged += new EventHandler(this.cboJoiningTableField_SelectedIndexChanged);
            this.cboJoiningTable.EditValue = "";
            this.cboJoiningTable.Location = new Point(8, 99);
            this.cboJoiningTable.Name = "cboJoiningTable";
            this.cboJoiningTable.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJoiningTable.Size = new Size(200, 21);
            this.cboJoiningTable.TabIndex = 9;
            this.cboJoiningTable.SelectedIndexChanged += new EventHandler(this.cboJoiningTable_SelectedIndexChanged);
            this.cboJoiningField.EditValue = "";
            this.cboJoiningField.Location = new Point(8, 40);
            this.cboJoiningField.Name = "cboJoiningField";
            this.cboJoiningField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJoiningField.Size = new Size(240, 21);
            this.cboJoiningField.TabIndex = 8;
            this.cboJoiningField.SelectedIndexChanged += new EventHandler(this.cboJoiningField_SelectedIndexChanged);
            this.btnJoiningType.Location = new Point(176, 208);
            this.btnJoiningType.Name = "btnJoiningType";
            this.btnJoiningType.Size = new Size(56, 24);
            this.btnJoiningType.TabIndex = 7;
            this.btnJoiningType.Text = "高级";
            this.btnJoiningType.Click += new EventHandler(this.btnJoiningType_Click);
            this.btnOpenTable.Image = (System.Drawing.Image)resources.GetObject("btnOpenTable.Image");
            this.btnOpenTable.Location = new Point(216, 99);
            this.btnOpenTable.Name = "btnOpenTable";
            this.btnOpenTable.Size = new Size(24, 24);
            this.btnOpenTable.TabIndex = 6;
            this.btnOpenTable.Click += new EventHandler(this.btnOpenTable_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 136);
            this.label4.Name = "label4";
            this.label4.Size = new Size(155, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "3、选择表中需要连接的字段";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 74);
            this.label3.Name = "label3";
            this.label3.Size = new Size(251, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "2、要连接到当前图层中的表，或从硬盘打开表";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(179, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "1、当前图层中要进行连接的字段";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(136, 328);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(208, 328);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.cboJoiningDataType.EditValue = "从表中连接数据";
            this.cboJoiningDataType.Location = new Point(8, 32);
            this.cboJoiningDataType.Name = "cboJoiningDataType";
            this.cboJoiningDataType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJoiningDataType.Properties.Items.AddRange(new object[] { "从表中连接数据" });
            this.cboJoiningDataType.Size = new Size(224, 21);
            this.cboJoiningDataType.TabIndex = 9;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(298, 364);
            base.Controls.Add(this.cboJoiningDataType);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmJoining";
            this.Text = "连接数据";
            base.Load += new EventHandler(this.frmJoining_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cboJoiningTableField.Properties.EndInit();
            this.cboJoiningTable.Properties.EndInit();
            this.cboJoiningField.Properties.EndInit();
            this.cboJoiningDataType.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnJoiningType;
        private SimpleButton btnOK;
        private SimpleButton btnOpenTable;
        private ComboBoxEdit cboJoiningDataType;
        private ComboBoxEdit cboJoiningField;
        private ComboBoxEdit cboJoiningTable;
        private ComboBoxEdit cboJoiningTableField;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}