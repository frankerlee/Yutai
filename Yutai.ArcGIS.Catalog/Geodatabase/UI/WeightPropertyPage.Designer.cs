using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class WeightPropertyPage
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
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.Location = new Point(8, 40);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(384, 144);
            this.listView1.TabIndex = 0;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "权重名称";
            this.columnHeader_0.Width = 233;
            this.columnHeader_1.Text = "类型";
            this.columnHeader_1.Width = 132;
            base.Controls.Add(this.listView1);
            base.Name = "WeightPropertyPage";
            base.Size = new Size(416, 256);
            base.Load += new EventHandler(this.WeightPropertyPage_Load);
            base.ResumeLayout(false);
        }

       
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ListView listView1;
    }
}