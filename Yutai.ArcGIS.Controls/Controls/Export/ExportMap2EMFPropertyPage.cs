using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal class ExportMap2EMFPropertyPage : UserControl
    {
        private ComboBoxEdit cboColorspace;
        private CheckEdit chkPolygonizeMarkers;
        private Container components = null;
        private Label label1;
        private Label label2;
        private bool m_CanDo = false;
        private IExport m_pExport = null;
        private TextEdit txtDescription;

        public ExportMap2EMFPropertyPage()
        {
            this.InitializeComponent();
        }

        private void cboColorspace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (this.cboColorspace.SelectedIndex != -1))
            {
                (this.m_pExport as IExportColorspaceSettings).Colorspace = (esriExportColorspace) this.cboColorspace.SelectedIndex;
            }
        }

        private void chkPolygonizeMarkers_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pExport as IExportVectorOptions).PolygonizeMarkers = this.chkPolygonizeMarkers.Checked;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ExportMap2EMFPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.m_pExport != null)
            {
                this.txtDescription.Text = (this.m_pExport as IExportEMF).Description;
                this.chkPolygonizeMarkers.Checked = (this.m_pExport as IExportVectorOptions).PolygonizeMarkers;
                this.cboColorspace.SelectedIndex = (int) (this.m_pExport as IExportColorspaceSettings).Colorspace;
                this.m_CanDo = true;
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.txtDescription = new TextEdit();
            this.chkPolygonizeMarkers = new CheckEdit();
            this.label2 = new Label();
            this.cboColorspace = new ComboBoxEdit();
            this.txtDescription.Properties.BeginInit();
            this.chkPolygonizeMarkers.Properties.BeginInit();
            this.cboColorspace.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "描述:";
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(0x30, 8);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0x88, 0x17);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.EditValueChanged += new EventHandler(this.txtDescription_EditValueChanged);
            this.chkPolygonizeMarkers.Location = new Point(8, 0x30);
            this.chkPolygonizeMarkers.Name = "chkPolygonizeMarkers";
            this.chkPolygonizeMarkers.Properties.Caption = "转换标记符号为多边形";
            this.chkPolygonizeMarkers.Size = new Size(0x90, 0x13);
            this.chkPolygonizeMarkers.TabIndex = 4;
            this.chkPolygonizeMarkers.CheckedChanged += new EventHandler(this.chkPolygonizeMarkers_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 80);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4f, 0x11);
            this.label2.TabIndex = 5;
            this.label2.Text = "目标颜色空间";
            this.cboColorspace.EditValue = "RGB";
            this.cboColorspace.Location = new Point(0x68, 80);
            this.cboColorspace.Name = "cboColorspace";
            this.cboColorspace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboColorspace.Properties.Items.AddRange(new object[] { "RGB", "CMYK" });
            this.cboColorspace.Size = new Size(0x60, 0x17);
            this.cboColorspace.TabIndex = 6;
            this.cboColorspace.SelectedIndexChanged += new EventHandler(this.cboColorspace_SelectedIndexChanged);
            base.Controls.Add(this.cboColorspace);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.chkPolygonizeMarkers);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.label1);
            base.Name = "ExportMap2EMFPropertyPage";
            base.Size = new Size(0xd8, 0xa8);
            base.Load += new EventHandler(this.ExportMap2EMFPropertyPage_Load);
            this.txtDescription.Properties.EndInit();
            this.chkPolygonizeMarkers.Properties.EndInit();
            this.cboColorspace.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void txtDescription_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pExport as IExportEMF).Description = this.txtDescription.Text;
            }
        }

        public IExport Export
        {
            set
            {
                this.m_pExport = value;
            }
        }
    }
}

