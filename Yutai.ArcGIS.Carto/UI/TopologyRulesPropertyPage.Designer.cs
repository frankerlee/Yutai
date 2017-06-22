using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class TopologyRulesPropertyPage
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.btnDescription = new SimpleButton();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(8, 8);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(296, 208);
            this.listView1.TabIndex = 0;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "要素类";
            this.columnHeader_0.Width = 76;
            this.columnHeader_1.Text = "规则";
            this.columnHeader_1.Width = 135;
            this.columnHeader_2.Text = "要素类";
            this.columnHeader_2.Width = 75;
            this.btnDescription.Enabled = false;
            this.btnDescription.Location = new Point(312, 8);
            this.btnDescription.Name = "btnDescription";
            this.btnDescription.Size = new Size(64, 32);
            this.btnDescription.TabIndex = 1;
            this.btnDescription.Text = "描述";
            this.btnDescription.Visible = false;
            this.btnDescription.Click += new EventHandler(this.btnDescription_Click);
            base.Controls.Add(this.btnDescription);
            base.Controls.Add(this.listView1);
            base.Name = "TopologyRulesPropertyPage";
            base.Size = new Size(392, 272);
            base.Load += new EventHandler(this.TopologyRulesPropertyPage_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnDescription;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ListView listView1;
    }
}