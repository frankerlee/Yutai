using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class RasterRenderPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.listBox1 = new ListBox();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(6, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(120, 256);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.panel1.Location = new Point(132, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(397, 269);
            this.panel1.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.listBox1);
            base.Name = "RasterRenderPropertyPage";
            base.Size = new Size(532, 276);
            base.Load += new EventHandler(this.RasterRenderPropertyPage_Load);
            base.ResumeLayout(false);
        }

       
        private ListBox listBox1;
        private Panel panel1;
    }
}