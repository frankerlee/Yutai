using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    partial class ExportMap2EMFPropertyPage
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
            this.txtDescription = new TextEdit();
            this.chkPolygonizeMarkers = new CheckEdit();
            this.label2 = new Label();
            this.cboColorspace = new ComboBoxEdit();
            this.txtDescription.Properties.BeginInit();
            this.chkPolygonizeMarkers.Properties.BeginInit();
            this.cboColorspace.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "描述:";
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(48, 8);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(136, 23);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.EditValueChanged += new EventHandler(this.txtDescription_EditValueChanged);
            this.chkPolygonizeMarkers.Location = new Point(8, 48);
            this.chkPolygonizeMarkers.Name = "chkPolygonizeMarkers";
            this.chkPolygonizeMarkers.Properties.Caption = "转换标记符号为多边形";
            this.chkPolygonizeMarkers.Size = new Size(144, 19);
            this.chkPolygonizeMarkers.TabIndex = 4;
            this.chkPolygonizeMarkers.CheckedChanged += new EventHandler(this.chkPolygonizeMarkers_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 80);
            this.label2.Name = "label2";
            this.label2.Size = new Size(79, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "目标颜色空间";
            this.cboColorspace.EditValue = "RGB";
            this.cboColorspace.Location = new Point(104, 80);
            this.cboColorspace.Name = "cboColorspace";
            this.cboColorspace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboColorspace.Properties.Items.AddRange(new object[] { "RGB", "CMYK" });
            this.cboColorspace.Size = new Size(96, 23);
            this.cboColorspace.TabIndex = 6;
            this.cboColorspace.SelectedIndexChanged += new EventHandler(this.cboColorspace_SelectedIndexChanged);
            base.Controls.Add(this.cboColorspace);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.chkPolygonizeMarkers);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.label1);
            base.Name = "ExportMap2EMFPropertyPage";
            base.Size = new Size(216, 168);
            base.Load += new EventHandler(this.ExportMap2EMFPropertyPage_Load);
            this.txtDescription.Properties.EndInit();
            this.chkPolygonizeMarkers.Properties.EndInit();
            this.cboColorspace.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private ComboBoxEdit cboColorspace;
        private CheckEdit chkPolygonizeMarkers;
        private Label label1;
        private Label label2;
        private TextEdit txtDescription;
    }
}