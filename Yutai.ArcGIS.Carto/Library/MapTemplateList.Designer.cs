using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace Yutai.ArcGIS.Carto.Library
{
    partial class MapTemplateList
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
            this.panel1 = new Panel();
            this.label1 = new Label();
            this.listBox1 = new ListBox();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(236, 23);
            this.panel1.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择模板";
            this.listBox1.Dock = DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(0, 23);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(236, 208);
            this.listBox1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.panel1);
            base.Name = "MapTemplateList";
            base.Size = new Size(236, 235);
            base.Load += new EventHandler(this.MapTemplateList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private bool bool_1;
        private Label label1;
        private ListBox listBox1;
        private Panel panel1;
    }
}