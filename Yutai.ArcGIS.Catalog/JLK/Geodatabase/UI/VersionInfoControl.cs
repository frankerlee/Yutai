namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Bars;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class VersionInfoControl : UserControl
    {
        private BarDockControl barDockControl_0;
        private BarDockControl barDockControl_1;
        private BarDockControl barDockControl_2;
        private BarDockControl barDockControl_3;
        private BarManager barManager_0;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private ColumnHeader columnHeader_4;
        private Container container_0 = null;
        private BarButtonItem DeleteVersions;
        private IVersionedWorkspace iversionedWorkspace_0;
        private BarButtonItem NewVerion;
        private PopupMenu popupMenu1;
        private BarButtonItem Property;
        private BarButtonItem RefreshVersion;
        private BarButtonItem ReVersionName;
        private ListView VersionInfolist;

        public VersionInfoControl()
        {
            this.InitializeComponent();
        }

        private void DeleteVersions_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.VersionInfolist.SelectedItems[0].Text == "DEFAULT")
            {
                MessageBox.Show("默认版本不能删除!");
            }
            else
            {
                ListViewItem item = this.VersionInfolist.SelectedItems[0];
                string str2 = item.SubItems[1].Text + "." + item.SubItems[0].Text;
                try
                {
                    this.iversionedWorkspace_0.FindVersion(this.VersionInfolist.SelectedItems[0].Text).Delete();
                    this.VersionInfolist.Items.Remove(this.VersionInfolist.SelectedItems[0]);
                }
                catch (COMException exception)
                {
                    switch (exception.ErrorCode)
                    {
                        case -2147215997:
                            MessageBox.Show("该用户不是版本所有者，不能删除版本 [" + str2 + "]");
                            break;

                        case -2147217143:
                            MessageBox.Show("版本 [" + str2 + "] 包含有子版本，请先删除子版本后在删除该版本");
                            break;
                    }
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(exception2.Message);
                }
            }
        }

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
            this.VersionInfolist = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_4 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.barManager_0 = new BarManager();
            this.barDockControl_0 = new BarDockControl();
            this.barDockControl_1 = new BarDockControl();
            this.barDockControl_2 = new BarDockControl();
            this.barDockControl_3 = new BarDockControl();
            this.popupMenu1 = new PopupMenu();
            this.NewVerion = new BarButtonItem();
            this.ReVersionName = new BarButtonItem();
            this.DeleteVersions = new BarButtonItem();
            this.RefreshVersion = new BarButtonItem();
            this.Property = new BarButtonItem();
            this.barManager_0.BeginInit();
            this.popupMenu1.BeginInit();
            base.SuspendLayout();
            this.VersionInfolist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_4, this.columnHeader_3 });
            this.VersionInfolist.Dock = DockStyle.Fill;
            this.VersionInfolist.FullRowSelect = true;
            this.VersionInfolist.LabelEdit = true;
            this.VersionInfolist.Location = new Point(0, 0);
            this.VersionInfolist.Name = "VersionInfolist";
            this.VersionInfolist.Size = new Size(0x1a0, 200);
            this.VersionInfolist.TabIndex = 0;
            this.VersionInfolist.View = View.Details;
            this.VersionInfolist.MouseUp += new MouseEventHandler(this.VersionInfolist_MouseUp);
            this.VersionInfolist.AfterLabelEdit += new LabelEditEventHandler(this.VersionInfolist_AfterLabelEdit);
            this.VersionInfolist.SelectedIndexChanged += new EventHandler(this.VersionInfolist_SelectedIndexChanged);
            this.columnHeader_0.Text = "名称";
            this.columnHeader_1.Text = "所有者";
            this.columnHeader_1.Width = 70;
            this.columnHeader_2.Text = "访问方式";
            this.columnHeader_2.Width = 0x4c;
            this.columnHeader_4.Text = "创建日期";
            this.columnHeader_4.Width = 0x55;
            this.columnHeader_3.Text = "最后修改日期";
            this.columnHeader_3.Width = 0x76;
            this.barManager_0.DockControls.Add(this.barDockControl_0);
            this.barManager_0.DockControls.Add(this.barDockControl_1);
            this.barManager_0.DockControls.Add(this.barDockControl_2);
            this.barManager_0.DockControls.Add(this.barDockControl_3);
            this.barManager_0.Form = this;
            this.barManager_0.Items.AddRange(new BarItem[] { this.NewVerion, this.ReVersionName, this.DeleteVersions, this.RefreshVersion, this.Property });
            this.barManager_0.MaxItemId = 5;
            this.popupMenu1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.NewVerion), new LinkPersistInfo(this.ReVersionName), new LinkPersistInfo(this.DeleteVersions), new LinkPersistInfo(this.RefreshVersion, true), new LinkPersistInfo(this.Property, true) });
            this.popupMenu1.Manager = this.barManager_0;
            this.popupMenu1.Name = "popupMenu1";
            this.NewVerion.Caption = "新建";
            this.NewVerion.Id = 0;
            this.NewVerion.Name = "NewVerion";
            this.NewVerion.ItemClick += new ItemClickEventHandler(this.NewVerion_ItemClick);
            this.ReVersionName.Caption = "重命名";
            this.ReVersionName.Id = 1;
            this.ReVersionName.Name = "ReVersionName";
            this.ReVersionName.ItemClick += new ItemClickEventHandler(this.ReVersionName_ItemClick);
            this.DeleteVersions.Caption = "删除";
            this.DeleteVersions.Id = 2;
            this.DeleteVersions.Name = "DeleteVersions";
            this.DeleteVersions.ItemClick += new ItemClickEventHandler(this.DeleteVersions_ItemClick);
            this.RefreshVersion.Caption = "刷新";
            this.RefreshVersion.Id = 3;
            this.RefreshVersion.Name = "RefreshVersion";
            this.RefreshVersion.ItemClick += new ItemClickEventHandler(this.RefreshVersion_ItemClick);
            this.Property.Caption = "属性";
            this.Property.Id = 4;
            this.Property.Name = "Property";
            this.Property.ItemClick += new ItemClickEventHandler(this.Property_ItemClick);
            base.Controls.Add(this.VersionInfolist);
            base.Controls.Add(this.barDockControl_2);
            base.Controls.Add(this.barDockControl_3);
            base.Controls.Add(this.barDockControl_1);
            base.Controls.Add(this.barDockControl_0);
            base.Name = "VersionInfoControl";
            base.Size = new Size(0x1a0, 200);
            base.Load += new EventHandler(this.VersionInfoControl_Load);
            this.barManager_0.EndInit();
            this.popupMenu1.EndInit();
            base.ResumeLayout(false);
        }

        private string method_0(esriVersionAccess esriVersionAccess_0)
        {
            switch (esriVersionAccess_0)
            {
                case esriVersionAccess.esriVersionAccessPrivate:
                    return "私有";

                case esriVersionAccess.esriVersionAccessProtected:
                    return "保护";
            }
            return "公共";
        }

        private void method_1()
        {
            if (this.iversionedWorkspace_0 != null)
            {
                this.VersionInfolist.Items.Clear();
                IEnumVersionInfo versions = this.iversionedWorkspace_0.Versions;
                versions.Reset();
                IVersionInfo info2 = versions.Next();
                string[] items = new string[5];
                while (info2 != null)
                {
                    string[] strArray2 = info2.VersionName.Split(new char[] { '.' });
                    if (strArray2.Length > 1)
                    {
                        items[0] = strArray2[1];
                        items[1] = strArray2[0];
                    }
                    else
                    {
                        items[0] = strArray2[0];
                        items[1] = "";
                    }
                    items[2] = this.method_0(info2.Access);
                    items[3] = info2.Created.ToString();
                    items[4] = info2.Modified.ToString();
                    this.VersionInfolist.Items.Add(new ListViewItem(items));
                    info2 = versions.Next();
                }
            }
        }

        private void NewVerion_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListViewItem item = this.VersionInfolist.SelectedItems[0];
            string name = item.SubItems[1].Text + "." + item.SubItems[0].Text;
            try
            {
                IVersion unk = null;
                unk = this.iversionedWorkspace_0.FindVersion(name);
                IArray array = new ArrayClass();
                array.Add(unk);
                frmNewVersion version2 = new frmNewVersion {
                    ParentVersions = array
                };
                if (version2.ShowDialog() == DialogResult.OK)
                {
                    this.method_1();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Property_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListViewItem item = this.VersionInfolist.SelectedItems[0];
            string name = item.SubItems[1].Text + "." + item.SubItems[0].Text;
            try
            {
                IVersion version = this.iversionedWorkspace_0.FindVersion(name);
                frmVersionProperty property = new frmVersionProperty {
                    Version = version
                };
                if (property.ShowDialog() == DialogResult.OK)
                {
                    this.method_1();
                }
            }
            catch (COMException exception)
            {
                if (exception.ErrorCode == -2147215997)
                {
                    MessageBox.Show("该用户不是版本 [" + name + "] 的所有者，无法修改版本的信息");
                }
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message);
            }
        }

        private void RefreshVersion_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = 0; i < this.VersionInfolist.SelectedItems.Count; i++)
            {
                ListViewItem item = this.VersionInfolist.SelectedItems[i];
                string name = item.SubItems[1].Text + "." + item.SubItems[0].Text;
                try
                {
                    this.iversionedWorkspace_0.FindVersion(name).RefreshVersion();
                }
                catch (COMException exception)
                {
                    if (exception.ErrorCode == -2147215997)
                    {
                        MessageBox.Show("该用户不是版本 [" + name + "] 的所有者，无法刷新该版本");
                    }
                }
            }
        }

        private void ReVersionName_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.VersionInfolist.SelectedItems[0].Text == "DEFAULT")
            {
                MessageBox.Show("默认版本不能重命名!");
            }
            else
            {
                this.VersionInfolist.SelectedItems[0].BeginEdit();
            }
        }

        private void VersionInfoControl_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void VersionInfolist_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if ((!e.CancelEdit && (e.Label != null)) && (e.Label.Length > 0))
            {
                ListViewItem item = this.VersionInfolist.Items[e.Item];
                if (item.Text.ToUpper() == "DEFAULT")
                {
                    MessageBox.Show("DEFAULT不能重命名!");
                    e.CancelEdit = true;
                }
                else
                {
                    string name = item.SubItems[1].Text + "." + item.SubItems[0].Text;
                    try
                    {
                        this.iversionedWorkspace_0.FindVersion(name).VersionName = e.Label;
                    }
                    catch (COMException exception)
                    {
                        switch (exception.ErrorCode)
                        {
                            case -2147215997:
                                MessageBox.Show("该用户不是版本 [" + name + "] 的所有者，无法重命名该版本");
                                break;

                            case -2147215946:
                                MessageBox.Show("已存在同名版本，无法重命名版本 [" + name + "] ");
                                break;
                        }
                        e.CancelEdit = true;
                    }
                    catch (Exception exception2)
                    {
                        MessageBox.Show(exception2.Message);
                        e.CancelEdit = true;
                    }
                }
            }
        }

        private void VersionInfolist_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (this.VersionInfolist.SelectedItems.Count > 0))
            {
                Point p = base.PointToScreen(new Point(e.X, e.Y));
                this.popupMenu1.ShowPopup(p);
            }
        }

        private void VersionInfolist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.VersionInfolist.SelectedItems.Count == 1)
            {
                this.NewVerion.Enabled = true;
                this.ReVersionName.Enabled = true;
                this.Property.Enabled = true;
            }
            else
            {
                this.NewVerion.Enabled = false;
                this.ReVersionName.Enabled = false;
                this.Property.Enabled = false;
            }
        }

        public IVersionedWorkspace VersionWorkspace
        {
            set
            {
                this.iversionedWorkspace_0 = value;
            }
        }
    }
}

