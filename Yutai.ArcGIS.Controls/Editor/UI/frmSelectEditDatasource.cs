using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal class frmSelectEditDatasource : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private ListBox CanEditDatasetList;
        private ColumnHeader ColumnHeader1;
        private ColumnHeader ColumnHeader2;
        private Container components = null;
        private ListView EditWorkspacelist;
        private Label label1;
        private Label label2;
        private IArray m_pEditWorkspaceInfo = null;
        private IMap m_pMap = null;

        public frmSelectEditDatasource()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.CanEditDatasetList.Items.Count == 0)
            {
                MessageBox.Show("不能编辑任何图层，请检查是否加载了要素图层，加载的要素图层是否已经进行了版本注册或是否有更新权限！", "开始编辑", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo;
                IWorkspaceEdit workspace = tag.Workspace as IWorkspaceEdit;
                if (!workspace.IsBeingEdited())
                {
                    try
                    {
                        workspace.StartEditing(true);
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace = workspace;
                        Yutai.ArcGIS.Common.Editor.Editor.EditMap = this.m_pMap;
                    }
                    catch (COMException exception)
                    {
                        if (exception.ErrorCode == -2147217069)
                        {
                            MessageBox.Show("不能编辑数据，其他应用程序正在使用该数据源!");
                        }
                        else
                        {
                           Logger.Current.Error("", exception, "");
                        }
                    }
                    catch (Exception exception2)
                    {
                       Logger.Current.Error("", exception2, "");
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EditWorkspacelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CanEditDatasetList.Items.Clear();
            this.btnOK.Enabled = false;
            if (this.EditWorkspacelist.SelectedItems.Count != 0)
            {
                this.btnOK.Enabled = true;
                try
                {
                    Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo;
                    for (int i = 0; i < tag.LayerArray.Count; i++)
                    {
                        IFeatureLayer layer = tag.LayerArray.get_Element(i) as IFeatureLayer;
                        this.CanEditDatasetList.Items.Add(layer.Name);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
                if (this.CanEditDatasetList.Items.Count == 0)
                {
                    this.btnOK.Enabled = false;
                }
                else
                {
                    this.btnOK.Enabled = true;
                }
            }
        }

        private void frmSelectEditDatasource_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void InitControl()
        {
            if (this.m_pEditWorkspaceInfo != null)
            {
                string[] items = new string[2];
                for (int i = 0; i < this.m_pEditWorkspaceInfo.Count; i++)
                {
                    Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo info = this.m_pEditWorkspaceInfo.get_Element(i) as Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo;
                    items[0] = info.Workspace.PathName;
                    items[1] = "";
                    switch (info.Workspace.Type)
                    {
                        case esriWorkspaceType.esriFileSystemWorkspace:
                            items[1] = "Shapefiles";
                            break;

                        case esriWorkspaceType.esriLocalDatabaseWorkspace:
                            items[1] = "个人空间数据库";
                            break;

                        case esriWorkspaceType.esriRemoteDatabaseWorkspace:
                            items[1] = "空间数据库连接";
                            break;
                    }
                    ListViewItem item = new ListViewItem(items) {
                        Tag = info
                    };
                    this.EditWorkspacelist.Items.Add(item);
                }
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.EditWorkspacelist = new ListView();
            this.ColumnHeader1 = new ColumnHeader();
            this.ColumnHeader2 = new ColumnHeader();
            this.label2 = new Label();
            this.CanEditDatasetList = new ListBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xca, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择要编辑的目录或数据库中的数据";
            this.EditWorkspacelist.Columns.AddRange(new ColumnHeader[] { this.ColumnHeader1, this.ColumnHeader2 });
            this.EditWorkspacelist.Location = new Point(8, 0x20);
            this.EditWorkspacelist.MultiSelect = false;
            this.EditWorkspacelist.Name = "EditWorkspacelist";
            this.EditWorkspacelist.Size = new Size(280, 0x60);
            this.EditWorkspacelist.TabIndex = 1;
            this.EditWorkspacelist.View = View.Details;
            this.EditWorkspacelist.SelectedIndexChanged += new EventHandler(this.EditWorkspacelist_SelectedIndexChanged);
            this.ColumnHeader1.Text = "源";
            this.ColumnHeader1.Width = 0x93;
            this.ColumnHeader2.Text = "类型";
            this.ColumnHeader2.Width = 0x77;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x88);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x74, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "能被编辑的图层或表";
            this.CanEditDatasetList.ItemHeight = 12;
            this.CanEditDatasetList.Location = new Point(8, 160);
            this.CanEditDatasetList.Name = "CanEditDatasetList";
            this.CanEditDatasetList.Size = new Size(0x110, 0x58);
            this.CanEditDatasetList.TabIndex = 3;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(120, 0x108);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(200, 0x108);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x124, 300);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.CanEditDatasetList);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.EditWorkspacelist);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSelectEditDatasource";
            this.Text = "开始编辑";
            base.Load += new EventHandler(this.frmSelectEditDatasource_Load);
            base.ResumeLayout(false);
        }

        public IArray EditWorkspaceInfo
        {
            set
            {
                this.m_pEditWorkspaceInfo = value;
            }
        }

        public IMap Map
        {
            set
            {
                this.m_pMap = value;
            }
        }
    }
}

