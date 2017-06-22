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
    partial class frmRelating
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelating));
            this.groupBox1 = new GroupBox();
            this.txtName = new TextEdit();
            this.label1 = new Label();
            this.btnOpenTable = new SimpleButton();
            this.cboRelatingTableField = new ComboBoxEdit();
            this.cboRelatingTable = new ComboBoxEdit();
            this.cboRelatingField = new ComboBoxEdit();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.txtName.Properties.BeginInit();
            this.cboRelatingTableField.Properties.BeginInit();
            this.cboRelatingTable.Properties.BeginInit();
            this.cboRelatingField.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnOpenTable);
            this.groupBox1.Controls.Add(this.cboRelatingTableField);
            this.groupBox1.Controls.Add(this.cboRelatingTable);
            this.groupBox1.Controls.Add(this.cboRelatingField);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(272, 312);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(8, 224);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(232, 21);
            this.txtName.TabIndex = 8;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 200);
            this.label1.Name = "label1";
            this.label1.Size = new Size(95, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "4、连接关联名字";
            this.btnOpenTable.Image = (System.Drawing.Image)resources.GetObject("btnOpenTable.Image");
            this.btnOpenTable.Location = new Point(216, 99);
            this.btnOpenTable.Name = "btnOpenTable";
            this.btnOpenTable.Size = new Size(24, 24);
            this.btnOpenTable.TabIndex = 6;
            this.btnOpenTable.Click += new EventHandler(this.btnOpenTable_Click);
            this.cboRelatingTableField.EditValue = "";
            this.cboRelatingTableField.Location = new Point(8, 165);
            this.cboRelatingTableField.Name = "cboRelatingTableField";
            this.cboRelatingTableField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRelatingTableField.Size = new Size(232, 21);
            this.cboRelatingTableField.TabIndex = 5;
            this.cboRelatingTableField.SelectedIndexChanged += new EventHandler(this.cboRelatingTableField_SelectedIndexChanged);
            this.cboRelatingTable.EditValue = "";
            this.cboRelatingTable.Location = new Point(8, 99);
            this.cboRelatingTable.Name = "cboRelatingTable";
            this.cboRelatingTable.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRelatingTable.Size = new Size(200, 21);
            this.cboRelatingTable.TabIndex = 4;
            this.cboRelatingTable.SelectedIndexChanged += new EventHandler(this.cboRelatingTable_SelectedIndexChanged);
            this.cboRelatingField.EditValue = "";
            this.cboRelatingField.Location = new Point(8, 42);
            this.cboRelatingField.Name = "cboRelatingField";
            this.cboRelatingField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRelatingField.Size = new Size(240, 21);
            this.cboRelatingField.TabIndex = 3;
            this.cboRelatingField.SelectedIndexChanged += new EventHandler(this.cboRelatingField_SelectedIndexChanged);
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
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(298, 364);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRelating";
            this.Text = "连接数据";
            base.Load += new EventHandler(this.frmRelating_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtName.Properties.EndInit();
            this.cboRelatingTableField.Properties.EndInit();
            this.cboRelatingTable.Properties.EndInit();
            this.cboRelatingField.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnOpenTable;
        private ComboBoxEdit cboRelatingField;
        private ComboBoxEdit cboRelatingTable;
        private ComboBoxEdit cboRelatingTableField;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextEdit txtName;
    }
}