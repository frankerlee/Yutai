using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmAddValuesEx
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.btnGetAllValues = new SimpleButton();
            this.groupBox1 = new GroupBox();
            this.btnAddNewValue = new SimpleButton();
            this.txtNewValue = new TextEdit();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.ValuelistBoxControl = new ListBoxControl();
            this.groupBox1.SuspendLayout();
            this.txtNewValue.Properties.BeginInit();
            ((ISupportInitialize) this.ValuelistBoxControl).BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(79, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择增加的值";
            this.btnGetAllValues.Location = new Point(24, 176);
            this.btnGetAllValues.Name = "btnGetAllValues";
            this.btnGetAllValues.Size = new Size(112, 24);
            this.btnGetAllValues.TabIndex = 2;
            this.btnGetAllValues.Text = "完成列表";
            this.btnGetAllValues.Click += new EventHandler(this.btnGetAllValues_Click);
            this.groupBox1.Controls.Add(this.btnAddNewValue);
            this.groupBox1.Controls.Add(this.txtNewValue);
            this.groupBox1.Location = new Point(8, 216);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(232, 64);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新值";
            this.btnAddNewValue.Location = new Point(152, 24);
            this.btnAddNewValue.Name = "btnAddNewValue";
            this.btnAddNewValue.Size = new Size(72, 24);
            this.btnAddNewValue.TabIndex = 1;
            this.btnAddNewValue.Text = "添加到列表";
            this.btnAddNewValue.Click += new EventHandler(this.btnAddNewValue_Click);
            this.txtNewValue.EditValue = "";
            this.txtNewValue.Location = new Point(16, 24);
            this.txtNewValue.Name = "txtNewValue";
            this.txtNewValue.Size = new Size(120, 23);
            this.txtNewValue.TabIndex = 0;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(168, 40);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(168, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.ValuelistBoxControl.ItemHeight = 15;
            this.ValuelistBoxControl.Location = new Point(16, 40);
            this.ValuelistBoxControl.Name = "ValuelistBoxControl";
            this.ValuelistBoxControl.SelectionMode = SelectionMode.MultiExtended;
            this.ValuelistBoxControl.Size = new Size(136, 120);
            this.ValuelistBoxControl.SortOrder = SortOrder.Ascending;
            this.ValuelistBoxControl.TabIndex = 6;
            this.ValuelistBoxControl.SelectedIndexChanged += new EventHandler(this.ValuelistBoxControl_SelectedIndexChanged);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(248, 293);
            base.Controls.Add(this.ValuelistBoxControl);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnGetAllValues);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAddValuesEx";
            this.Text = "新值";
            base.Load += new EventHandler(this.frmAddValuesEx_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtNewValue.Properties.EndInit();
            ((ISupportInitialize) this.ValuelistBoxControl).EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAddNewValue;
        private SimpleButton btnCancel;
        private SimpleButton btnGetAllValues;
        private SimpleButton btnOK;
        private GroupBox groupBox1;
        private Label label1;
        private TextEdit txtNewValue;
        private ListBoxControl ValuelistBoxControl;
    }
}