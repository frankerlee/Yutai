using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class GeometryNetGeneralPropertyPage
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
            this.label1 = new Label();
            this.textEdit1 = new TextEdit();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(56, 8);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(184, 23);
            this.textEdit1.TabIndex = 1;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.Location = new Point(16, 48);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(280, 176);
            this.listView1.TabIndex = 2;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "要素类名称";
            this.columnHeader_0.Width = 127;
            this.columnHeader_1.Text = "规则";
            this.columnHeader_1.Width = 121;
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.textEdit1);
            base.Controls.Add(this.label1);
            base.Name = "GeometryNetGeneralPropertyPage";
            base.Size = new Size(320, 256);
            base.Load += new EventHandler(this.GeometryNetGeneralPropertyPage_Load);
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private Label label1;
        private ListView listView1;
        private TextEdit textEdit1;
    }
}