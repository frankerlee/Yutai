using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmCheckOutManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckOutManager));
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_3 });
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(312, 273);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "名称";
            this.columnHeader_0.Width = 100;
            this.columnHeader_1.Text = "拥有者";
            this.columnHeader_1.Width = 73;
            this.columnHeader_2.Text = "版本";
            this.columnHeader_2.Width = 78;
            this.columnHeader_3.Text = "日期";
            this.columnHeader_3.Width = 79;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(312, 273);
            base.Controls.Add(this.listView1);
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCheckOutManager";
            this.Text = "检出管理器";
            base.Load += new EventHandler(this.frmCheckOutManager_Load);
            base.ResumeLayout(false);
        }

       
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private ListView listView1;
    }
}