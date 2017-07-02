using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class CatoTemplateApplySelect
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.label1 = new Label();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(10, 35);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(251, 123);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "名称";
            this.columnHeader_0.Width = 107;
            this.columnHeader_1.Text = "说明";
            this.columnHeader_1.Width = 118;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "制图模板";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listView1);
            base.Name = "CatoTemplateApplySelect";
            base.Size = new Size(273, 202);
            base.Load += new EventHandler(this.CatoTemplateApplySelect_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private Label label1;
        private ListView listView1;
        private IContainer icontainer_0 = null;
    }
}