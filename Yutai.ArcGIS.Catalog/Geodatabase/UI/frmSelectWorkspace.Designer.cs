using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmSelectWorkspace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectWorkspace));
            this.label1 = new Label();
            this.EditWorkspacelist = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.label2 = new Label();
            this.CanEditDatasetList = new ListBox();
            this.btnOK = new SimpleButton();
            this.simpleButton1 = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "空间数据库";
            this.EditWorkspacelist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.EditWorkspacelist.Location = new Point(8, 32);
            this.EditWorkspacelist.Name = "EditWorkspacelist";
            this.EditWorkspacelist.Size = new Size(264, 112);
            this.EditWorkspacelist.TabIndex = 1;
            this.EditWorkspacelist.UseCompatibleStateImageBehavior = false;
            this.EditWorkspacelist.View = View.Details;
            this.EditWorkspacelist.SelectedIndexChanged += new EventHandler(this.EditWorkspacelist_SelectedIndexChanged);
            this.columnHeader_0.Text = "";
            this.columnHeader_0.Width = 98;
            this.columnHeader_1.Text = "";
            this.columnHeader_1.Width = 118;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 152);
            this.label2.Name = "label2";
            this.label2.Size = new Size(101, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "空间数据库层和表";
            this.CanEditDatasetList.ItemHeight = 12;
            this.CanEditDatasetList.Location = new Point(16, 176);
            this.CanEditDatasetList.Name = "CanEditDatasetList";
            this.CanEditDatasetList.Size = new Size(256, 88);
            this.CanEditDatasetList.TabIndex = 3;
            this.btnOK.Location = new Point(144, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new Point(216, 280);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(56, 24);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(292, 317);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.CanEditDatasetList);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.EditWorkspacelist);
            base.Controls.Add(this.label1);
            
            base.Name = "frmSelectWorkspace";
            this.Text = "frmSelectWorkspace";
            base.Load += new EventHandler(this.frmSelectWorkspace_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnOK;
        private ListBox CanEditDatasetList;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ListView EditWorkspacelist;
        private Label label1;
        private Label label2;
        private SimpleButton simpleButton1;
    }
}