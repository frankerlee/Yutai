using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class SelectExportChangeWKCtrl : UserControl
    {
        private ListBox CanEditDatasetList;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private Container container_0 = null;
        private ListView EditWorkspacelist;
        private IArray iarray_0 = null;
        private Label label1;
        private Label label2;

        public SelectExportChangeWKCtrl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        public bool Do()
        {
            if (this.EditWorkspacelist.SelectedItems.Count == 0)
            {
                return false;
            }
            CheckInOutWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as CheckInOutWorkspaceInfo;
            ExportChangesHelper.m_pHelper.CheckoutWorkspaceName = (tag.Workspace as IDataset).FullName as IWorkspaceName;
            return true;
        }

        private void EditWorkspacelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CanEditDatasetList.Items.Clear();
            if (this.EditWorkspacelist.SelectedItems.Count != 0)
            {
                try
                {
                    CheckInOutWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as CheckInOutWorkspaceInfo;
                    IEnumName enumName = tag.EnumName;
                    enumName.Reset();
                    for (IName name2 = enumName.Next(); name2 != null; name2 = enumName.Next())
                    {
                        this.CanEditDatasetList.Items.Add((name2 as IDatasetName).Name);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void InitializeComponent()
        {
            this.CanEditDatasetList = new ListBox();
            this.label2 = new Label();
            this.label1 = new Label();
            this.EditWorkspacelist = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            base.SuspendLayout();
            this.CanEditDatasetList.ItemHeight = 12;
            this.CanEditDatasetList.Location = new Point(0x10, 160);
            this.CanEditDatasetList.Name = "CanEditDatasetList";
            this.CanEditDatasetList.Size = new Size(280, 0x58);
            this.CanEditDatasetList.TabIndex = 7;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x88);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x80, 0x11);
            this.label2.TabIndex = 6;
            this.label2.Text = "导出以下层或表的变化";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xa5, 0x11);
            this.label1.TabIndex = 4;
            this.label1.Text = "选择要导出变化的检出数据库";
            this.EditWorkspacelist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.EditWorkspacelist.Location = new Point(0x10, 0x20);
            this.EditWorkspacelist.MultiSelect = false;
            this.EditWorkspacelist.Name = "EditWorkspacelist";
            this.EditWorkspacelist.Size = new Size(280, 0x60);
            this.EditWorkspacelist.TabIndex = 5;
            this.EditWorkspacelist.View = View.Details;
            this.EditWorkspacelist.SelectedIndexChanged += new EventHandler(this.EditWorkspacelist_SelectedIndexChanged);
            this.columnHeader_0.Text = "源";
            this.columnHeader_0.Width = 0x93;
            this.columnHeader_1.Text = "类型";
            this.columnHeader_1.Width = 0x77;
            base.Controls.Add(this.CanEditDatasetList);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.EditWorkspacelist);
            base.Name = "SelectExportChangeWKCtrl";
            base.Size = new Size(0x158, 0x128);
            base.Load += new EventHandler(this.SelectExportChangeWKCtrl_Load);
            base.ResumeLayout(false);
        }

        private void SelectExportChangeWKCtrl_Load(object sender, EventArgs e)
        {
            if (this.iarray_0 != null)
            {
                string[] items = new string[2];
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    CheckInOutWorkspaceInfo info = this.iarray_0.get_Element(i) as CheckInOutWorkspaceInfo;
                    items[0] = info.Workspace.PathName;
                    items[1] = "空间数据库连接";
                    ListViewItem item = new ListViewItem(items) {
                        Tag = info
                    };
                    this.EditWorkspacelist.Items.Add(item);
                }
            }
        }

        public IArray WorkspaceArray
        {
            set
            {
                this.iarray_0 = value;
            }
        }
    }
}

