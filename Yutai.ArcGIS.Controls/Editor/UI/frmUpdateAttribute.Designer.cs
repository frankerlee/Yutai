using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Common.Query.UI;
using Yutai.ArcGIS.Common.Wrapper;
using Yutai.Shared;
using ProcessAssist = Yutai.Shared.ProcessAssist;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmUpdateAttribute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateAttribute));
            this.label1 = new Label();
            this.label2 = new Label();
            this.cboLayer = new ComboBoxEdit();
            this.cboFields = new ComboBoxEdit();
            this.label3 = new Label();
            this.txtNewValue = new TextEdit();
            this.chkUseSelected = new CheckEdit();
            this.label4 = new Label();
            this.btnQueryDialog = new SimpleButton();
            this.memoEdit = new MemoEdit();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.cboLayer.Properties.BeginInit();
            this.cboFields.Properties.BeginInit();
            this.txtNewValue.Properties.BeginInit();
            this.chkUseSelected.Properties.BeginInit();
            this.memoEdit.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "字段:";
            this.cboLayer.EditValue = "";
            this.cboLayer.Location = new Point(56, 8);
            this.cboLayer.Name = "cboLayer";
            this.cboLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLayer.Size = new Size(168, 21);
            this.cboLayer.TabIndex = 2;
            this.cboLayer.SelectedIndexChanged += new EventHandler(this.cboLayer_SelectedIndexChanged);
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(56, 40);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(168, 21);
            this.cboFields.TabIndex = 3;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 80);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "新值:";
            this.txtNewValue.EditValue = "";
            this.txtNewValue.Location = new Point(56, 72);
            this.txtNewValue.Name = "txtNewValue";
            this.txtNewValue.Size = new Size(168, 21);
            this.txtNewValue.TabIndex = 5;
            this.chkUseSelected.Location = new Point(8, 104);
            this.chkUseSelected.Name = "chkUseSelected";
            this.chkUseSelected.Properties.Caption = "使用选择集";
            this.chkUseSelected.Size = new Size(96, 19);
            this.chkUseSelected.TabIndex = 6;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 136);
            this.label4.Name = "label4";
            this.label4.Size = new Size(155, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "仅更新满足以下条件的要素:";
            this.btnQueryDialog.Enabled = false;
            this.btnQueryDialog.Location = new Point(16, 312);
            this.btnQueryDialog.Name = "btnQueryDialog";
            this.btnQueryDialog.Size = new Size(88, 24);
            this.btnQueryDialog.TabIndex = 9;
            this.btnQueryDialog.Text = "查询生成器";
            this.btnQueryDialog.Click += new EventHandler(this.btnQueryDialog_Click);
            this.memoEdit.EditValue = "";
            this.memoEdit.Location = new Point(8, 160);
            this.memoEdit.Name = "memoEdit";
            this.memoEdit.Size = new Size(288, 136);
            this.memoEdit.TabIndex = 8;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(168, 312);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(232, 312);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(56, 24);
            this.simpleButton2.TabIndex = 11;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(312, 349);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnQueryDialog);
            base.Controls.Add(this.memoEdit);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.chkUseSelected);
            base.Controls.Add(this.txtNewValue);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.cboLayer);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            
            base.Name = "frmUpdateAttribute";
            this.Text = "更新属性值";
            base.Load += new EventHandler(this.frmUpdateAttribute_Load);
            this.cboLayer.Properties.EndInit();
            this.cboFields.Properties.EndInit();
            this.txtNewValue.Properties.EndInit();
            this.chkUseSelected.Properties.EndInit();
            this.memoEdit.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private SimpleButton btnOK;
        private SimpleButton btnQueryDialog;
        private ComboBoxEdit cboFields;
        private ComboBoxEdit cboLayer;
        private CheckEdit chkUseSelected;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private MemoEdit memoEdit;
        private SimpleButton simpleButton2;
        private TextEdit txtNewValue;
    }
}