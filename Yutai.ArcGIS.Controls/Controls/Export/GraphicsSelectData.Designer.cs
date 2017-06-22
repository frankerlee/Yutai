using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    partial class GraphicsSelectData
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
            this.radioGroupExportType = new RadioGroup();
            this.cboLayers = new ComboBoxEdit();
            this.radioGroupExportType.Properties.BeginInit();
            this.cboLayers.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择图层:";
            this.radioGroupExportType.Location = new Point(24, 56);
            this.radioGroupExportType.Name = "radioGroupExportType";
            this.radioGroupExportType.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroupExportType.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupExportType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroupExportType.Properties.Columns = 2;
            this.radioGroupExportType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "导出全部要素"), new RadioGroupItem(null, "只导出选择要素") });
            this.radioGroupExportType.Size = new Size(232, 24);
            this.radioGroupExportType.TabIndex = 3;
            this.cboLayers.EditValue = "";
            this.cboLayers.Location = new Point(72, 8);
            this.cboLayers.Name = "cboLayers";
            this.cboLayers.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLayers.Size = new Size(176, 23);
            this.cboLayers.TabIndex = 4;
            this.cboLayers.SelectedIndexChanged += new EventHandler(this.cboLayers_SelectedIndexChanged);
            this.BackColor = SystemColors.ControlLight;
            base.Controls.Add(this.cboLayers);
            base.Controls.Add(this.radioGroupExportType);
            base.Controls.Add(this.label1);
            base.Name = "GraphicsSelectData";
            base.Size = new Size(288, 152);
            base.Load += new EventHandler(this.ExportToExcelSelectData_Load);
            this.radioGroupExportType.Properties.EndInit();
            this.cboLayers.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private ComboBoxEdit cboLayers;
        private Label label1;
        private RadioGroup radioGroupExportType;
    }
}