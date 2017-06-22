using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Controls.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class RepresentationRendererPage
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.representationruleListBox1 = new RepresentationruleListBox();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.representationruleListBox1.DrawMode = DrawMode.OwnerDrawVariable;
            this.representationruleListBox1.FormattingEnabled = true;
            this.representationruleListBox1.Location = new Point(14, 12);
            this.representationruleListBox1.Name = "representationruleListBox1";
            this.representationruleListBox1.Size = new Size(140, 265);
            this.representationruleListBox1.TabIndex = 0;
            this.representationruleListBox1.SelectedIndexChanged += new EventHandler(this.representationruleListBox1_SelectedIndexChanged);
            this.panel1.Location = new Point(162, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(287, 265);
            this.panel1.TabIndex = 1;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.representationruleListBox1);
            base.Name = "RepresentationRendererPage";
            base.Size = new Size(462, 296);
            base.Load += new EventHandler(this.RepresentationRendererPage_Load);
            base.ResumeLayout(false);
        }

       
        private Panel panel1;
        private RepresentationruleListBox representationruleListBox1;
    }
}