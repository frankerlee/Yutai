using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class BulidGN_Configuration
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
            this.radioGroup1 = new RadioGroup();
            this.comboBoxEdit = new ComboBoxEdit();
            this.label1 = new Label();
            this.radioGroup1.Properties.BeginInit();
            this.comboBoxEdit.Properties.BeginInit();
            base.SuspendLayout();
            this.radioGroup1.Location = new Point(16, 16);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "使用默认值"), new RadioGroupItem(null, "使用以下关键字") });
            this.radioGroup1.Size = new Size(224, 72);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.comboBoxEdit.EditValue = "";
            this.comboBoxEdit.Location = new Point(16, 120);
            this.comboBoxEdit.Name = "comboBoxEdit";
            this.comboBoxEdit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit.Size = new Size(168, 23);
            this.comboBoxEdit.TabIndex = 1;
            this.comboBoxEdit.SelectedIndexChanged += new EventHandler(this.comboBoxEdit_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 96);
            this.label1.Name = "label1";
            this.label1.Size = new Size(66, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "配置关键字";
            base.Controls.Add(this.label1);
            base.Controls.Add(this.comboBoxEdit);
            base.Controls.Add(this.radioGroup1);
            base.Name = "BulidGN_Configuration";
            base.Size = new Size(272, 216);
            base.Load += new EventHandler(this.BulidGN_Configuration_Load);
            this.radioGroup1.Properties.EndInit();
            this.comboBoxEdit.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private ComboBoxEdit comboBoxEdit;
        private Label label1;
        private RadioGroup radioGroup1;
    }
}