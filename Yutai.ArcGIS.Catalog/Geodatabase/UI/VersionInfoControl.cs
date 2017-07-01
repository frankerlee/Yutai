using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class VersionInfoControl : UserControl
    {
        private Container container_0 = null;

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
                    string[] strArray2 = info2.VersionName.Split(new char[] {'.'});
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
                frmNewVersion version2 = new frmNewVersion
                {
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
                frmVersionProperty property = new frmVersionProperty
                {
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
            set { this.iversionedWorkspace_0 = value; }
        }
    }
}