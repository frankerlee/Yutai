using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class FindControl
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.chkContains = new CheckEdit();
            this.label3 = new Label();
            this.radioGroup = new RadioGroup();
            this.cboSearchString = new ComboBoxEdit();
            this.cboLayers = new ComboBoxEdit();
            this.cboFields = new ComboBoxEdit();
            this.chkContains.Properties.BeginInit();
            this.radioGroup.Properties.BeginInit();
            this.cboSearchString.Properties.BeginInit();
            this.cboLayers.Properties.BeginInit();
            this.cboFields.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "查找:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 44);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "图层:";
            this.chkContains.Location = new Point(8, 72);
            this.chkContains.Name = "chkContains";
            this.chkContains.Properties.Caption = "模糊查找";
            this.chkContains.Size = new Size(160, 19);
            this.chkContains.TabIndex = 2;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(200, 11);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "搜索:";
            this.radioGroup.Location = new Point(240, 8);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup.Size = new Size(168, 72);
            this.radioGroup.TabIndex = 6;
            this.radioGroup.SelectedIndexChanged += new EventHandler(this.radioGroup_SelectedIndexChanged);
            this.cboSearchString.EditValue = "";
            this.cboSearchString.Location = new Point(56, 8);
            this.cboSearchString.Name = "cboSearchString";
            this.cboSearchString.Size = new Size(128, 23);
            this.cboSearchString.TabIndex = 12;
            this.cboSearchString.TextChanged += new EventHandler(this.cboSearchString_TextChanged);
            this.cboLayers.EditValue = "";
            this.cboLayers.Location = new Point(56, 40);
            this.cboLayers.Name = "cboLayers";
            this.cboLayers.Size = new Size(128, 23);
            this.cboLayers.TabIndex = 13;
            this.cboLayers.SelectedIndexChanged += new EventHandler(this.cboLayers_SelectedIndexChanged);
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(304, 32);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(128, 23);
            this.cboFields.TabIndex = 14;
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.cboLayers);
            base.Controls.Add(this.cboSearchString);
            base.Controls.Add(this.radioGroup);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.chkContains);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "FindControl";
            base.Size = new Size(496, 104);
            base.Load += new EventHandler(this.FindControl_Load);
            this.chkContains.Properties.EndInit();
            this.radioGroup.Properties.EndInit();
            this.cboSearchString.Properties.EndInit();
            this.cboLayers.Properties.EndInit();
            this.cboFields.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private ComboBoxEdit cboFields;
        private ComboBoxEdit cboLayers;
        private ComboBoxEdit cboSearchString;
        private CheckEdit chkContains;
        private IActiveViewEvents_Event iactiveViewEvents_Event_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private RadioGroup radioGroup;
    }
}