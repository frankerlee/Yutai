using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class CustomOverlayGridPropertyPage
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.btnSelect = new SimpleButton();
            this.txtFC = new TextEdit();
            this.cboLabelField = new ComboBoxEdit();
            this.txtFC.Properties.BeginInit();
            this.cboLabelField.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(48, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据源:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 56);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "标注字段:";
            this.btnSelect.Location = new Point(216, 8);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(64, 24);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "选择";
            this.txtFC.EditValue = "";
            this.txtFC.Location = new Point(80, 8);
            this.txtFC.Name = "txtFC";
            this.txtFC.Size = new Size(112, 23);
            this.txtFC.TabIndex = 3;
            this.cboLabelField.EditValue = "";
            this.cboLabelField.Location = new Point(80, 48);
            this.cboLabelField.Name = "cboLabelField";
            this.cboLabelField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLabelField.Size = new Size(152, 23);
            this.cboLabelField.TabIndex = 4;
            base.Controls.Add(this.cboLabelField);
            base.Controls.Add(this.txtFC);
            base.Controls.Add(this.btnSelect);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "CustomOverlayGridPropertyPage";
            base.Size = new Size(344, 288);
            base.Load += new EventHandler(this.CustomOverlayGridPropertyPage_Load);
            this.txtFC.Properties.EndInit();
            this.cboLabelField.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnSelect;
        private ComboBoxEdit cboLabelField;
        private Label label1;
        private Label label2;
        private TextEdit txtFC;
    }
}