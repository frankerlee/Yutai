using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesOleDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Framework;
using Yutai.ArcGIS.Framework;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;
using Cursor = System.Windows.Forms.Cursor;
using Path = System.IO.Path;
using Point = System.Drawing.Point;

namespace Yutai.Plugins.Catalog.Views
{
    public partial class CatalogView : DockPanelControlBase, IPopuMenuWrap, ICatalogView
    {
        private bool m_TreeCanDo = true;

        private bool m_CanDo = true;

        private int type = 0;

        private IAppContext _context;

        public IAppContext AppContext
        {
            get { return _context; }
            set { _context = value; }
        }


        public IGxObject CurrentSelect { get; set; }

        /// <summary>
        /// 目录列表
        /// </summary>
        public IGxCatalog GxCatalog { get; set; }


        public CatalogView(IAppContext context)
        {
            this.InitializeComponent();
            _context = context;
        }

        private void BuildContextMenu(TreeNode pNode)
        {
            if (pNode.Tag != null)
            {
                IGxObject tag = pNode.Tag as IGxObject;
                if (!(tag is IGxContextMenuWap))
                {
                    this.popupMenu1.ItemLinks.Clear();
                }
                else
                {
                    // _context.GxSelection = tag;
                    (tag as IGxContextMenuWap).Init(this);
                }
            }
        }

        private void CatalogView_Load(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                if (this.GxCatalog == null)
                {
                    this.GxCatalog = new GxCatalog();
                }
                this.kTreeView1.GxCatalog = this.GxCatalog;
                this.kTreeView1.InitTreeView();
                string name = (this.GxCatalog as IGxObject).Name;
                this.imageComboBoxEdit1.Properties.SmallImages = this.imageList1;
                this.listView1.LargeImageList = this.imageList1;
                this.listView1.SmallImageList = this.imageList1;
                ImageComboBoxItemEx imageComboBoxItemEx = new ImageComboBoxItemEx(name, this.GxCatalog,
                    this.GetImageIndex(this.GxCatalog as IGxObject), 0)
                {
                    Tag = this.GxCatalog
                };
                this.imageComboBoxEdit1.Properties.Items.Add(imageComboBoxItemEx);
                IEnumGxObject children = (this.GxCatalog as IGxObjectContainer).Children;
                children.Reset();
                for (IGxObject i = children.Next(); i != null; i = children.Next())
                {
                    imageComboBoxItemEx = new ImageComboBoxItemEx(i.Name, i.FullName, this.GetImageIndex(i), 1)
                    {
                        Tag = i
                    };
                    this.imageComboBoxEdit1.Properties.Items.Add(imageComboBoxItemEx);
                }
                this.imageComboBoxEdit1.SelectedIndex = 0;
            }
        }

        private void CatalogView_SizeChanged(object sender, EventArgs e)
        {
            if (this.panelTreeView.Visible)
            {
                if (base.Width > 600)
                {
                    this.panelContentListView.Dock = DockStyle.Right;
                    this.splitter1.Dock = DockStyle.Right;
                }
                else if (base.Height <= 500)
                {
                    this.panelContentListView.Dock = DockStyle.Right;
                    this.splitter1.Dock = DockStyle.Right;
                }
                else
                {
                    this.panelContentListView.Dock = DockStyle.Bottom;
                    this.splitter1.Dock = DockStyle.Bottom;
                }
            }
        }

        private void ClearListView()
        {
            this.listView1.SelectedItems.Clear();
            this.listView1.Items.Clear();
        }

        private void ComboBoxHandle()
        {
            ImageComboBoxItemEx item =
                this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex] as ImageComboBoxItemEx;
            IGxObject tag = item.Tag as IGxObject;
            for (int i = this.imageComboBoxEdit1.Properties.Items.Count - 1; i > 0; i--)
            {
                item = this.imageComboBoxEdit1.Properties.Items[i] as ImageComboBoxItemEx;
                IGxObject gxObject = item.Tag as IGxObject;
                if (!(gxObject is IGxCatalog))
                {
                    if (!(gxObject.Parent is IGxCatalog))
                    {
                        if (gxObject != tag)
                        {
                            if (!this.IsAncestors(gxObject, tag))
                            {
                                this.imageComboBoxEdit1.Properties.Items.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }


        private string GetFinalFileName(string filename)
        {
            string str = filename.Substring(0, filename.Length - 4);
            int num = 1;
            while (File.Exists(filename))
            {
                filename = string.Concat(str, " (", num.ToString(), ").odc");
                num++;
            }
            return filename;
        }

        private int GetImageIndex(IGxObject pGxObject)
        {
            IFeatureClass featureClass;
            int count = 0;
            string category = pGxObject.Category;
            if (pGxObject is IGxDataset)
            {
                if ((pGxObject as IGxDataset).Type == esriDatasetType.esriDTFeatureClass)
                {
                    IFeatureClassName datasetName = (pGxObject as IGxDataset).DatasetName as IFeatureClassName;
                    if (datasetName.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        category = string.Concat(category, " 注记");
                    }
                    else if (datasetName.ShapeType != esriGeometryType.esriGeometryNull)
                    {
                        category = string.Concat(category, CommonHelper.GetFeatureClassType(datasetName));
                    }
                    else
                    {
                        try
                        {
                            featureClass = (datasetName as IName).Open() as IFeatureClass;
                            category = string.Concat(category, CommonHelper.GetFeatureClassType(featureClass));
                        }
                        catch
                        {
                        }
                    }
                }
            }
            else if (pGxObject is IGxDatabase)
            {
                if ((pGxObject as IGxDatabase).IsRemoteDatabase)
                {
                    if ((pGxObject as IGxDatabase).IsConnected)
                    {
                        category = string.Concat(category, " Connect");
                    }
                }
            }
            else if (pGxObject is IGxVCTLayerObject)
            {
                category = string.Concat("VCT", (pGxObject as IGxVCTLayerObject).LayerTypeName);
            }
            else if (pGxObject is IGxLayer)
            {
                ILayer layer = (pGxObject as IGxLayer).Layer;
                if (layer == null)
                {
                    category = string.Concat(category, " Unknown");
                }
                else if (layer is IGroupLayer)
                {
                    category = string.Concat(category, " GroupLayer");
                }
                else if (layer is IRasterLayer)
                {
                    category = string.Concat(category, " RasterLayer");
                }
                else if (layer is IFeatureLayer)
                {
                    featureClass = (layer as IFeatureLayer).FeatureClass;
                    category = (featureClass != null
                        ? string.Concat(category, CommonHelper.GetFeatureClassType(featureClass))
                        : string.Concat(category, " Unknown"));
                }
            }
            else if (pGxObject is IGxDiskConnection)
            {
                if (!Directory.Exists((pGxObject as IGxFile).Path))
                {
                    category = string.Concat(category, "_Error");
                }
            }
            if (!this.imageList1.Images.ContainsKey(category))
            {
                this.imageList1.Images.Add(category, (pGxObject as IGxObjectUI).SmallImage);
                count = this.imageList1.Images.Count - 1;
            }
            else
            {
                count = this.imageList1.Images.Keys.IndexOf(category);
            }
            return count;
        }

        private IGxObject GetRoot(IGxObject pGxObjec)
        {
            IGxObject parent = pGxObjec;
            while (true)
            {
                if ((!(parent.Parent is IGxCatalog) ? false : parent.Parent != null))
                {
                    break;
                }
                parent = parent.Parent;
            }
            return parent;
        }

        private void imageComboBoxEditEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode node = null;
            ImageComboBoxItemEx item =
                this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex] as ImageComboBoxItemEx;
            IGxObject tag = item.Tag as IGxObject;
            if (this.m_CanDo)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.SetListView(tag);
                this.ComboBoxHandle();
            }
            this.m_TreeCanDo = false;
            if (!(tag is IGxCatalog))
            {
                IGxObject root = this.GetRoot(tag);
                TreeNode treeNode = null;
                foreach (TreeNode node2 in this.kTreeView1.Nodes[0].Nodes)
                {
                    if (node2.Tag == root)
                    {
                        treeNode = node2;
                        break;
                    }
                }
                if (root != tag)
                {
                    TreeNode treeNode1 = treeNode;
                    IGxObject parent = tag;
                    while (true)
                    {
                        bool flag = true;
                        parent = tag;
                        while (true)
                        {
                            flag = true;
                            if (parent.Parent == root)
                            {
                                break;
                            }
                            parent = parent.Parent;
                        }
                        root = parent;
                        foreach (TreeNode node1 in treeNode1.Nodes)
                        {
                            if (node1.Tag == parent)
                            {
                                treeNode1 = node1;
                                break;
                            }
                        }
                        if (root == tag)
                        {
                            break;
                        }
                    }
                    this.kTreeView1.SelectedNode = treeNode1;
                }
                else
                {
                    this.kTreeView1.SelectedNode = treeNode;
                }
            }
            else
            {
                this.kTreeView1.SelectedNode = this.kTreeView1.Nodes[0];
            }
            this.m_TreeCanDo = true;
            Cursor.Current = Cursors.Default;
        }


        private bool IsAncestors(IGxObject pGxObject, IGxObject pChildGxObject)
        {
            bool flag;
            IGxObject parent = pChildGxObject.Parent;
            while (true)
            {
                if (parent == null)
                {
                    flag = false;
                    break;
                }
                else if (pGxObject != parent)
                {
                    parent = parent.Parent;
                }
                else
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private void kTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ImageComboBoxItemEx item;
            int num;
            if (this.m_TreeCanDo)
            {
                if (this.kTreeView1.SelectedNode != null)
                {
                    IGxObject tag = this.kTreeView1.SelectedNode.Tag as IGxObject;
                    this.BuildContextMenu(e.Node);
                    this.SetListView(tag);
                    this.m_CanDo = false;
                    this.RemoveComboBoxHandleEx();
                    if (!(tag is IGxCatalog))
                    {
                        if (tag.Parent is IGxCatalog)
                        {
                            num = 0;
                            while (num < this.imageComboBoxEdit1.Properties.Items.Count)
                            {
                                item = this.imageComboBoxEdit1.Properties.Items[num] as ImageComboBoxItemEx;
                                if (item.Tag != tag)
                                {
                                    num++;
                                }
                                else
                                {
                                    this.imageComboBoxEdit1.SelectedIndex = num;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            IGxObject root = this.GetRoot(tag);
                            item = null;
                            int num1 = -1;
                            num = 0;
                            while (num < this.imageComboBoxEdit1.Properties.Items.Count)
                            {
                                item = this.imageComboBoxEdit1.Properties.Items[num] as ImageComboBoxItemEx;
                                if (item.Tag != root)
                                {
                                    num++;
                                }
                                else
                                {
                                    num1 = num;
                                    break;
                                }
                            }
                            if (num1 >= 0)
                            {
                                int degree = item.Degree;
                                IGxObject parent = tag;
                                while (true)
                                {
                                    bool flag = true;
                                    parent = tag;
                                    while (true)
                                    {
                                        flag = true;
                                        if (parent.Parent == root)
                                        {
                                            break;
                                        }
                                        parent = parent.Parent;
                                    }
                                    int num2 = degree + 1;
                                    degree = num2;
                                    item = new ImageComboBoxItemEx(parent.Name, parent.FullName,
                                        this.GetImageIndex(parent), num2)
                                    {
                                        Tag = tag
                                    };
                                    int num3 = num1 + 1;
                                    num1 = num3;
                                    this.imageComboBoxEdit1.Properties.Items.Insert(num3, item);
                                    root = parent;
                                    if (root == tag)
                                    {
                                        break;
                                    }
                                }
                                this.imageComboBoxEdit1.SelectedIndex = num1;
                            }
                        }
                        this.m_CanDo = true;
                    }
                    else
                    {
                        num = 0;
                        while (num < this.imageComboBoxEdit1.Properties.Items.Count)
                        {
                            item = this.imageComboBoxEdit1.Properties.Items[num] as ImageComboBoxItemEx;
                            if (item.Tag != tag)
                            {
                                num++;
                            }
                            else
                            {
                                this.imageComboBoxEdit1.SelectedIndex = num;
                                break;
                            }
                        }
                        this.m_CanDo = true;
                    }
                }
            }
        }

        private void kTreeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new Point(e.X, e.Y);
                point = this.kTreeView1.PointToScreen(point);
                popupMenu1.ShowPopup(point);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IWorkspaceName workspaceNameClass;
            IGxObject gxDatabase;
            ListViewItem listViewItem;
            string[] name;
            ImageComboBoxItemEx item;
            if (this.listView1.SelectedItems.Count != 0)
            {
                IGxObject tag = this.listView1.SelectedItems[0].Tag as IGxObject;
                if (tag is IGxObjectContainer)
                {
                    this.SetListView(tag);
                    this.m_CanDo = false;
                    if (tag.Parent is IGxCatalog)
                    {
                        int num = 0;
                        while (num < this.imageComboBoxEdit1.Properties.Items.Count)
                        {
                            item = this.imageComboBoxEdit1.Properties.Items[num] as ImageComboBoxItemEx;
                            if (item.Tag != tag)
                            {
                                num++;
                            }
                            else
                            {
                                this.imageComboBoxEdit1.SelectedIndex = num;
                                break;
                            }
                        }
                    }
                    else
                    {
                        item =
                            this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex] as
                                ImageComboBoxItemEx;
                        int degree = item.Degree;
                        item = new ImageComboBoxItemEx(tag.Name, tag.FullName, this.GetImageIndex(tag), degree + 1)
                        {
                            Tag = tag
                        };
                        int selectedIndex = this.imageComboBoxEdit1.SelectedIndex;
                        this.imageComboBoxEdit1.Properties.Items.Insert(selectedIndex + 1, item);
                        this.imageComboBoxEdit1.SelectedIndex = selectedIndex + 1;
                    }
                    this.m_CanDo = true;
                }
                else if (tag is IGxNewDatabase)
                {
                    if (tag.FullName == "添加OLE DB连接")
                    {
                        try
                        {
                            string str = string.Concat(Environment.SystemDirectory.Substring(0, 2),
                                "\\Documents and Settings\\Administrator\\Application Data\\ESRI\\ArcCatalog\\");
                            string finalFileName = string.Concat(str, "OLE DB Connection.odc");
                            if (Directory.Exists(str))
                            {
                                finalFileName = this.GetFinalFileName(finalFileName);
                                IWorkspaceFactory oLEDBWorkspaceFactoryClass = new OLEDBWorkspaceFactory();
                                workspaceNameClass = oLEDBWorkspaceFactoryClass.Create(str,
                                    Path.GetFileName(finalFileName), null, 0);
                                gxDatabase = new GxDatabase();
                                (gxDatabase as IGxDatabase).WorkspaceName = workspaceNameClass;
                                gxDatabase.Attach(tag.Parent, this.GxCatalog);
                                name = new string[] {gxDatabase.Name, gxDatabase.Category};
                                listViewItem = new ListViewItem(name, this.GetImageIndex(gxDatabase))
                                {
                                    Tag = gxDatabase
                                };
                                this.listView1.Items.Add(listViewItem);
                            }
                        }
                        catch (Exception exception)
                        {
                            exception.ToString();
                        }
                    }
                    else if (tag.FullName == "添加空间数据库连接")
                    {
                        frmCreateGDBConnection _frmCreateGDBConnection = new frmCreateGDBConnection()
                        {
                            TopMost = true
                        };
                        if (_frmCreateGDBConnection.ShowDialog() == DialogResult.OK)
                        {
                            gxDatabase = new GxDatabase();
                            workspaceNameClass = new WorkspaceName() as IWorkspaceName;
                            workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesGDB.SdeWorkspaceFactory";
                            workspaceNameClass.PathName = _frmCreateGDBConnection.ConnectionPath;
                            (gxDatabase as IGxDatabase).WorkspaceName = workspaceNameClass;
                            gxDatabase.Attach(tag.Parent, this.GxCatalog);
                            name = new string[] {gxDatabase.Name, gxDatabase.Category};
                            listViewItem = new ListViewItem(name, this.GetImageIndex(gxDatabase))
                            {
                                Tag = gxDatabase
                            };
                            this.listView1.Items.Add(listViewItem);
                        }
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IGxSelection gxSelection;
            this.Clear();
            if (this.listView1.SelectedItems.Count > 0)
            {
                if (this.GxCatalog == null)
                {
                    gxSelection = new GxSelection();
                }
                else
                {
                    gxSelection = this.GxCatalog.Selection;
                    gxSelection.Clear(null);
                }
                int count = this.listView1.SelectedItems.Count - 1;
                for (int i = 0; i < this.listView1.SelectedItems.Count; i++)
                {
                    ListViewItem item = this.listView1.SelectedItems[i];
                    if (item.Tag != null)
                    {
                        gxSelection.Select(item.Tag as IGxObject, true, (i == count ? false : true));
                    }
                }
                if (gxSelection.Count > 1)
                {
                    this.AddItem("Catalog_Copy", false);
                    this.AddItem("Catalog_Paste", false);
                    this.AddItem("Catalog_Delete", true);
                }
                else if (gxSelection.Count == 1)
                {
                    IGxObject firstObject = gxSelection.FirstObject;
                    if (firstObject is IGxContextMenuWap)
                    {
                        (firstObject as IGxContextMenuWap).Init(this);
                    }
                }
            }
        }

        private void qqqToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatalogView catalogControl = this;
            catalogControl.type = catalogControl.type + 1;
            if (this.type == 3)
            {
                this.type = 0;
            }
            if (this.type == 0)
            {
                this.panelTreeView.Visible = true;
                this.panelContentListView.Visible = false;
            }
            else if (this.type != 1)
            {
                this.panelTreeView.Visible = false;
                this.panelContentListView.Dock = DockStyle.Fill;
            }
            else
            {
                if (base.Width > 600)
                {
                    this.panelContentListView.Dock = DockStyle.Right;
                    this.splitter1.Dock = DockStyle.Right;
                }
                else if (base.Height <= 500)
                {
                    this.panelContentListView.Dock = DockStyle.Right;
                    this.splitter1.Dock = DockStyle.Right;
                }
                else
                {
                    this.panelContentListView.Dock = DockStyle.Bottom;
                    this.splitter1.Dock = DockStyle.Bottom;
                }
                this.panelContentListView.Visible = true;
            }
        }

        /// <summary>
        /// 清空组合框中非跟级节点
        /// </summary>
        private void RemoveComboBoxHandleEx()
        {
            if (this.imageComboBoxEdit1.SelectedIndex != -1)
            {
                ImageComboBoxItemEx item =
                    this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex] as
                        ImageComboBoxItemEx;
                IGxObject tag = item.Tag as IGxObject;
                for (int i = this.imageComboBoxEdit1.Properties.Items.Count - 1; i > 0; i--)
                {
                    item = this.imageComboBoxEdit1.Properties.Items[i] as ImageComboBoxItemEx;
                    IGxObject gxObject = item.Tag as IGxObject;
                    if (!(gxObject is IGxCatalog))
                    {
                        if (!(gxObject.Parent is IGxCatalog))
                        {
                            this.imageComboBoxEdit1.Properties.Items.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private void SetListView(IGxObject pGxObject)
        {
            Cursor.Current = Cursors.WaitCursor;
            string[] name = new string[2];
            this.ClearListView();
            if (pGxObject is IGxObjectContainer)
            {
                if (pGxObject is IGxDatabase)
                {
                    if (!(pGxObject as IGxDatabase).IsConnected)
                    {
                        (pGxObject as IGxDatabase).Connect();
                    }
                    if (!(pGxObject as IGxDatabase).IsConnected)
                    {
                        return;
                    }
                }
                else if (pGxObject is IGxAGSConnection)
                {
                    if (!(pGxObject as IGxAGSConnection).IsConnected)
                    {
                        (pGxObject as IGxAGSConnection).Connect();
                    }
                    if (!(pGxObject as IGxAGSConnection).IsConnected)
                    {
                        return;
                    }
                }
                IEnumGxObject children = (pGxObject as IGxObjectContainer).Children;
                children.Reset();
                IGxObject gxObject = children.Next();
                while (gxObject != null)
                {
                    name[0] = gxObject.Name;
                    name[1] = gxObject.Category;
                    ListViewItem listViewItem = new ListViewItem(name, this.GetImageIndex(gxObject))
                    {
                        Tag = gxObject
                    };
                    this.listView1.Items.Add(listViewItem);
                    gxObject = children.Next();
                }
            }
            Cursor.Current = Cursors.Default;
        }


        private OnItemClickEventHandler onItemClickEventHandler_0;

        public event OnItemClickEventHandler OnItemClickEvent
        {
            add
            {
                OnItemClickEventHandler onItemClickEventHandler;
                OnItemClickEventHandler onItemClickEventHandler0 = this.onItemClickEventHandler_0;
                do
                {
                    onItemClickEventHandler = onItemClickEventHandler0;
                    OnItemClickEventHandler onItemClickEventHandler1 =
                        (OnItemClickEventHandler) Delegate.Combine(onItemClickEventHandler, value);
                    onItemClickEventHandler0 =
                        Interlocked.CompareExchange<OnItemClickEventHandler>(ref this.onItemClickEventHandler_0,
                            onItemClickEventHandler1, onItemClickEventHandler);
                } while ((object) onItemClickEventHandler0 != (object) onItemClickEventHandler);
            }
            remove
            {
                OnItemClickEventHandler onItemClickEventHandler;
                OnItemClickEventHandler onItemClickEventHandler0 = this.onItemClickEventHandler_0;
                do
                {
                    onItemClickEventHandler = onItemClickEventHandler0;
                    OnItemClickEventHandler onItemClickEventHandler1 =
                        (OnItemClickEventHandler) Delegate.Remove(onItemClickEventHandler, value);
                    onItemClickEventHandler0 =
                        Interlocked.CompareExchange<OnItemClickEventHandler>(ref this.onItemClickEventHandler_0,
                            onItemClickEventHandler1, onItemClickEventHandler);
                } while ((object) onItemClickEventHandler0 != (object) onItemClickEventHandler);
            }
        }

        public void AddItem(string cmdName, bool isGroup)
        {
            BarItem find = _context.RibbonMenu.SubItems.FindItem(cmdName);
            if (find == null) return;
            find.Enabled = ((YutaiCommand) find.Tag).Enabled;
            BarItemLink link = popupMenu1.ItemLinks.Add(find);
            link.BeginGroup = isGroup;
        }

        public void AddItem(MenuItemDef menuItemDef)
        {
        }

        public void AddItemEx(string cmdName, string groupCmdName, bool isGroup)
        {
            BarItemLink oldLink = popupMenu1.ItemLinks.FirstOrDefault(c => c.Item.Name == groupCmdName);
            if (oldLink != null)
            {
                oldLink.BeginGroup = true;
            }
            BarItem find = _context.RibbonMenu.SubItems.FindItem(cmdName);
            if (find == null) return;
            find.Enabled = ((YutaiCommand) find.Tag).Enabled;
            BarItemLink link = popupMenu1.ItemLinks.Add(find);
            link.BeginGroup = isGroup;
        }

        public void AddItem(string cmdName, string parentName, bool isGroup)
        {
            if (!string.IsNullOrEmpty(parentName))
            {
                BarItemLink oldLink = popupMenu1.ItemLinks.FirstOrDefault(c => c.Item.Name == parentName);
                if (oldLink != null && oldLink.Item is BarSubItem)
                {
                    BarSubItem subItem = oldLink.Item as BarSubItem;
                    BarItem find = _context.RibbonMenu.SubItems.FindItem(cmdName);
                    if (find == null) return;
                    find.Enabled = ((YutaiCommand) find.Tag).Enabled;
                    BarItemLink link = subItem.ItemLinks.Add(find);
                    link.BeginGroup = isGroup;
                }
            }
            else
            {
                BarItem find = _context.RibbonMenu.SubItems.FindItem(cmdName);
                if (find == null) return;
                find.Enabled = ((YutaiCommand) find.Tag).Enabled;
                BarItemLink link = popupMenu1.ItemLinks.Add(find);
                link.BeginGroup = isGroup;
            }
        }

        public void AddSubmenuItem(string cmdName, string caption, string parentName, bool isGroup)
        {
            BarItemLink oldLink = null;
            if (!string.IsNullOrEmpty(parentName))
                oldLink = popupMenu1.ItemLinks.FirstOrDefault(c => c.Item.Name == parentName);
            BarSubItem subItem = new BarSubItem() {Name = cmdName, Caption = caption};
            if (oldLink == null)
            {
                (popupMenu1.AddItem(subItem)).BeginGroup = isGroup;
            }
            else
            {
                (((BarSubItem) (oldLink.Item)).ItemLinks.Add(subItem)).BeginGroup = isGroup;
            }
        }

        public void ClearSubItem(string cmdName)
        {
            BarItemLink oldLink = popupMenu1.ItemLinks.FirstOrDefault(c => c.Item.Name == cmdName);
            BarSubItem subItem;
            if (oldLink != null)
            {
                subItem = oldLink.Item as BarSubItem;
                subItem.ItemLinks.Clear();
            }
        }

        public void Clear()
        {
            this.popupMenu1.ItemLinks.Clear();
        }

        public void UpdateUI()
        {
            for (int i = 0; i < popupMenu1.ItemLinks.Count; i++)
            {
                BarItemLink item = popupMenu1.ItemLinks[i];
                if (item.Item is BarItem)
                {
                    if (item.Item is BarSubItem)
                    {
                        RefreshItemLinks(item.Item as BarSubItem);
                        continue;
                    }
                    if (item.Item.Tag != null)
                    {
                        item.Item.Enabled = ((YutaiCommand) item.Item.Tag).Enabled;
                    }
                }
            }
        }

        private void RefreshItemLinks(BarSubItem item)
        {
            foreach (BarItemLink link in item.ItemLinks)
            {
                if (link.Item is BarSubItem)
                {
                    RefreshItemLinks(link.Item as BarSubItem);
                    continue;
                }
                if (link.Item.Tag != null)
                {
                    link.Item.Enabled = ((YutaiCommand) link.Item.Tag).Enabled;
                }
            }
        }

        public void Show(Control control, Point point)
        {
            this.popupMenu1.ShowPopup(point);
        }

        #region Override DockPanelControlBase

        public override Bitmap Image
        {
            get { return Properties.Resources.icon_catalog_folder; }
        }

        public override string Caption
        {
            get { return "目录"; }
            set { Caption = value; }
        }

        public override DockPanelState DefaultDock
        {
            get { return DockPanelState.Right; }
        }

        public override string DockName
        {
            get { return DefaultDockName; }
        }

        public override string DefaultNestDockName
        {
            get { return ""; }
        }

        public const string DefaultDockName = "Catalog_Viewer";

        #endregion

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }

        public void Initialize(IAppContext context)
        {
            _context = context;

            popupMenu1.Ribbon = _context.MainView.RibbonManager as RibbonControl;
            GxCatalog = _context.GxCatalog as IGxCatalog;
        }
    }
}