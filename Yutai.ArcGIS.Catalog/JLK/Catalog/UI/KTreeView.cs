namespace JLK.Catalog.UI
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.DataSourcesFile;
    using ESRI.ArcGIS.DataSourcesOleDB;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Catalog;
    using JLK.Utility;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class KTreeView : TreeView
    {
        private ArrayList arrayList_0 = new ArrayList();
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private ImageList imageList_0 = null;
        private IPropertySet ipropertySet_0 = new PropertySetClass();
        private System.Drawing.Point point_0;
        private SortedList<string, int> sortedList_0 = new SortedList<string, int>();
        private TreeNode treeNode_0 = null;
        private TreeNode treeNode_1 = null;

        public event SelectNodeChangedHandler SelectNodeChanged;

        public KTreeView()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                this.method_0();
                this.imageList_0 = base.ImageList;
                if (this.imageList_0 == null)
                {
                    this.imageList_0 = new ImageList();
                    base.ImageList = this.imageList_0;
                }
                base.MouseDown += new MouseEventHandler(this.KTreeView_MouseDown);
                base.DoubleClick += new EventHandler(this.KTreeView_DoubleClick);
                base.DragDrop += new DragEventHandler(this.KTreeView_DragDrop);
                base.AfterLabelEdit += new NodeLabelEditEventHandler(this.KTreeView_AfterLabelEdit);
                base.BeforeLabelEdit += new NodeLabelEditEventHandler(this.KTreeView_BeforeLabelEdit);
                base.DragOver += new DragEventHandler(this.KTreeView_DragOver);
                base.MouseMove += new MouseEventHandler(this.KTreeView_MouseMove);
                base.MouseUp += new MouseEventHandler(this.KTreeView_MouseUp);
                base.QueryContinueDrag += new QueryContinueDragEventHandler(this.KTreeView_QueryContinueDrag);
                base.AfterSelect += new TreeViewEventHandler(this.KTreeView_AfterSelect);
                base.BeforeExpand += new TreeViewCancelEventHandler(this.KTreeView_BeforeExpand);
                base.AllowDrop = true;
                base.HideSelection = false;
                base.LabelEdit = true;
            }
        }

        public void AddChildNode(IGxObject igxObject_1, TreeNode treeNode_2)
        {
            IGxObject tag = treeNode_2.Tag as IGxObject;
            igxObject_1.Attach(tag, this.igxCatalog_0);
            TreeNode node = this.method_6(igxObject_1);
            treeNode_2.Nodes.Add(node);
        }

        public void AddChildNodes(IGxObject igxObject_1, TreeNode treeNode_2)
        {
            if (igxObject_1 is IGxObjectContainer)
            {
                if ((igxObject_1 as IGxObjectContainer).AreChildrenViewable)
                {
                    IEnumGxObject children = (igxObject_1 as IGxObjectContainer).Children;
                    children.Reset();
                    for (IGxObject obj3 = children.Next(); obj3 != null; obj3 = children.Next())
                    {
                        TreeNode node = this.method_6(obj3);
                        this.AddChildNodes(obj3, node);
                        treeNode_2.Nodes.Add(node);
                    }
                }
                else if ((igxObject_1 as IGxObjectContainer).HasChildren)
                {
                    if (igxObject_1 is IGxDatabase)
                    {
                        if (!(igxObject_1 as IGxDatabase).IsRemoteDatabase)
                        {
                            this.method_5(treeNode_2);
                        }
                    }
                    else
                    {
                        this.method_5(treeNode_2);
                    }
                }
            }
        }

        public void ConnectArcGISServer()
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            IGxObject tag = base.SelectedNode.Tag as IGxObject;
            (tag as IGxAGSConnection).Connect();
            this.method_7(base.SelectedNode);
            base.SelectedNode.ImageIndex = this.method_3(tag);
            base.SelectedNode.SelectedImageIndex = this.method_4(tag);
            GxCatalogCommon.GetCatalog(tag).ObjectRefreshed(tag);
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        public void ConnectDatabase()
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            IGxObject tag = base.SelectedNode.Tag as IGxObject;
            (tag as IGxDatabase).Connect();
            if ((tag as IGxDatabase).IsConnected)
            {
                this.method_7(base.SelectedNode);
                base.SelectedNode.ImageIndex = this.method_3(tag);
                base.SelectedNode.SelectedImageIndex = this.method_4(tag);
                GxCatalogCommon.GetCatalog(tag).ObjectRefreshed(tag);
            }
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr intptr_0);
        public void DisConnectArcGISServer()
        {
            TreeNode selectedNode = base.SelectedNode;
            IGxAGSConnection tag = selectedNode.Tag as IGxAGSConnection;
            if ((tag != null) && tag.IsConnected)
            {
                tag.Disconnect();
                selectedNode.Nodes.Clear();
                base.SelectedNode.ImageIndex = this.method_3(tag as IGxObject);
                base.SelectedNode.SelectedImageIndex = this.method_4(tag as IGxObject);
                base.SelectedNode = base.SelectedNode.Parent;
            }
        }

        public void DisConnectDatabase()
        {
            TreeNode selectedNode = base.SelectedNode;
            IGxDatabase tag = selectedNode.Tag as IGxDatabase;
            if ((tag != null) && tag.IsConnected)
            {
                tag.Disconnect();
                selectedNode.Nodes.Clear();
                base.SelectedNode.ImageIndex = this.method_3(tag as IGxObject);
                base.SelectedNode.SelectedImageIndex = this.method_4(tag as IGxObject);
                base.SelectedNode = base.SelectedNode.Parent;
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        public void Expand(TreeNode treeNode_2)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            try
            {
                IGxObject tag = treeNode_2.Tag as IGxObject;
                if ((tag is IGxDatabase) && (tag as IGxDatabase).IsRemoteDatabase)
                {
                    System.Windows.Forms.Cursor.Current = Cursors.Default;
                    return;
                }
                this.method_7(treeNode_2);
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
            }
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        public void InitTreeView()
        {
            if (this.igxCatalog_0 == null)
            {
                this.GxCatalog = new JLK.Catalog.GxCatalog();
            }
            base.Nodes.Clear();
            TreeNode node = new TreeNode((this.igxCatalog_0 as IGxObject).Name, this.method_3(this.igxCatalog_0 as IGxObject), this.method_4(this.igxCatalog_0 as IGxObject));
            base.Nodes.Add(node);
            node.Tag = this.igxCatalog_0;
            this.method_7(node);
        }

        private void KTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            IGxObjectEdit tag = e.Node.Tag as IGxObjectEdit;
            if (tag == null)
            {
                e.CancelEdit = true;
            }
            else if (!tag.CanRename())
            {
                e.CancelEdit = true;
            }
            else
            {
                tag.Rename(e.Label);
                if (e.Label == (tag as IGxObject).Name)
                {
                    e.CancelEdit = false;
                }
                else
                {
                    e.CancelEdit = true;
                }
            }
        }

        private void KTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.igxCatalog_0.Selection.Select(e.Node.Tag as IGxObject, false, null);
            this.Expand(e.Node);
        }

        private void KTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            this.Expand(e.Node);
        }

        private void KTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            IGxObjectEdit tag = e.Node.Tag as IGxObjectEdit;
            if (tag == null)
            {
                e.CancelEdit = true;
            }
            else if (!tag.CanRename())
            {
                e.CancelEdit = true;
            }
        }

        private void KTreeView_DoubleClick(object sender, EventArgs e)
        {
            if (base.SelectedNode != null)
            {
                IGxObject tag = base.SelectedNode.Tag as IGxObject;
                if (tag != null)
                {
                    IGxObject newObject;
                    TreeNode node;
                    Exception exception;
                    if (tag is IGxNewDatabase)
                    {
                        IWorkspaceName name;
                        if (tag.FullName == "添加OLE DB连接")
                        {
                            try
                            {
                                string path = Environment.SystemDirectory.Substring(0, 2) + @"\Documents and Settings\Administrator\Application Data\ESRI\ArcCatalog\";
                                string str2 = path + "OLE DB Connection.odc";
                                if (Directory.Exists(path))
                                {
                                    str2 = this.method_17(str2);
                                    IWorkspaceFactory factory = new OLEDBWorkspaceFactoryClass();
                                    name = factory.Create(path, System.IO.Path.GetFileName(str2), null, 0);
                                    newObject = new GxDatabase();
                                    (newObject as IGxDatabase).WorkspaceName = name;
                                    newObject.Attach(tag.Parent, this.igxCatalog_0);
                                    node = new TreeNode(newObject.Name, this.method_3(newObject), this.method_4(newObject)) {
                                        Tag = newObject
                                    };
                                    base.SelectedNode.Parent.Nodes.Add(node);
                                }
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                                exception.ToString();
                            }
                        }
                        else if (tag.FullName == "添加空间数据库连接")
                        {
                            frmCreateGDBConnection connection = new frmCreateGDBConnection();
                            if (connection.ShowDialog() == DialogResult.OK)
                            {
                                newObject = new GxDatabase();
                                name = new WorkspaceNameClass {
                                    WorkspaceFactoryProgID = "esriDataSourcesGDB.SdeWorkspaceFactory",
                                    PathName = connection.ConnectionPath
                                };
                                (newObject as IGxDatabase).WorkspaceName = name;
                                newObject.Attach(tag.Parent, this.igxCatalog_0);
                                node = new TreeNode(newObject.Name, this.method_3(newObject), this.method_4(newObject)) {
                                    Tag = newObject
                                };
                                base.SelectedNode.Parent.Nodes.Add(node);
                            }
                        }
                    }
                    else if (tag.FullName == "添加Database Server")
                    {
                        frmAddDatabaseServer server = new frmAddDatabaseServer();
                        if (server.ShowDialog() != DialogResult.OK)
                        {
                        }
                    }
                    else if (tag is IGxDatabase)
                    {
                        if ((tag as IGxDatabase).IsRemoteDatabase && !(tag as IGxDatabase).IsConnected)
                        {
                            this.ConnectDatabase();
                        }
                    }
                    else if (tag is IGxAGSConnection)
                    {
                        if (!(tag as IGxAGSConnection).IsConnected)
                        {
                            this.ConnectArcGISServer();
                        }
                    }
                    else if (tag is IGxGDSConnection)
                    {
                        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                        IGxObject obj4 = base.SelectedNode.Tag as IGxObject;
                        (obj4 as IGxGDSConnection).Connect();
                        if ((obj4 as IGxGDSConnection).IsConnected)
                        {
                            this.method_7(base.SelectedNode);
                            base.SelectedNode.ImageIndex = this.method_3(obj4);
                            base.SelectedNode.SelectedImageIndex = this.method_4(obj4);
                            GxCatalogCommon.GetCatalog(tag).ObjectRefreshed(obj4);
                        }
                        System.Windows.Forms.Cursor.Current = Cursors.Default;
                    }
                    else if (tag.Name == "添加ArcGIS Server")
                    {
                        frmNewArcGISServer server2 = new frmNewArcGISServer();
                        if (server2.ShowDialog() == DialogResult.OK)
                        {
                            newObject = server2.NewObject;
                            if (newObject != null)
                            {
                                newObject.Attach(tag.Parent, this.igxCatalog_0);
                                node = new TreeNode(newObject.Name, this.method_3(newObject), this.method_4(newObject)) {
                                    Tag = newObject
                                };
                                base.SelectedNode.Parent.Nodes.Add(node);
                            }
                        }
                    }
                    else if (tag.Name == "添加Server Object")
                    {
                        try
                        {
                            frmNewServerObject obj5 = new frmNewServerObject {
                                AGSServerConnectionName = (tag.Parent as IGxAGSConnection).AGSServerConnectionName
                            };
                            if (obj5.ShowDialog() == DialogResult.OK)
                            {
                                tag.Parent.Refresh();
                            }
                        }
                        catch (Exception exception2)
                        {
                            exception = exception2;
                        }
                    }
                }
            }
        }

        private void KTreeView_DragDrop(object sender, DragEventArgs e)
        {
            if (this.bool_0)
            {
                IGxObject tag = this.treeNode_1.Tag as IGxObject;
                if (tag is IGxObjectContainer)
                {
                    this.Expand(this.treeNode_1);
                    IEnumNameEdit edit = new NamesEnumeratorClass();
                    IEnumerator enumerator = (e.Data.GetData("System.Collections.ArrayList") as ArrayList).GetEnumerator();
                    enumerator.Reset();
                    IGxObject current = null;
                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current as IGxObject;
                        if (current != null)
                        {
                            edit.Add(current.InternalObjectName);
                        }
                    }
                    if ((tag as IGxPasteTarget).Paste(edit as IEnumName, ref this.bool_1) && this.bool_1)
                    {
                        enumerator.Reset();
                        while (enumerator.MoveNext())
                        {
                            current = enumerator.Current as IGxObject;
                            if (current is IGxDataset)
                            {
                                if ((current as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureDataset)
                                {
                                    IEnumGxObject children = (current as IGxObjectContainer).Children;
                                    children.Reset();
                                    for (IGxObject obj5 = children.Next(); obj5 != null; obj5 = children.Next())
                                    {
                                        obj5.Detach();
                                    }
                                }
                                else
                                {
                                    current.Detach();
                                }
                            }
                            else if (current != null)
                            {
                                current.Detach();
                            }
                        }
                    }
                    if (this.selectNodeChangedHandler_0 != null)
                    {
                        this.selectNodeChangedHandler_0(this);
                    }
                }
            }
        }

        private void KTreeView_DragOver(object sender, DragEventArgs e)
        {
            System.Drawing.Point p = new System.Drawing.Point(e.X, e.Y);
            p = base.PointToClient(p);
            this.treeNode_1 = base.GetNodeAt(p);
            this.bool_0 = false;
            if ((this.treeNode_1 != null) && (this.treeNode_1.Tag is IGxPasteTarget))
            {
                IEnumerator enumerator = (e.Data.GetData("System.Collections.ArrayList") as ArrayList).GetEnumerator();
                enumerator.Reset();
                IEnumNameEdit edit = new NamesEnumeratorClass();
                IGxObject current = null;
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current as IGxObject;
                    if (current != null)
                    {
                        edit.Add(current.InternalObjectName);
                    }
                }
                this.bool_0 = (this.treeNode_1.Tag as IGxPasteTarget).CanPaste(edit as IEnumName, ref this.bool_1);
            }
            if (this.bool_0)
            {
                if (this.bool_1)
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void KTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.treeNode_0 = base.GetNodeAt(e.X, e.Y);
                    if (this.treeNode_0 != null)
                    {
                        this.igxObject_0 = this.treeNode_0.Tag as IGxObject;
                        this.arrayList_0.Clear();
                        if (!(this.igxObject_0 is IGxDataset))
                        {
                            if (this.igxObject_0 is IGxObjectEdit)
                            {
                                if (!(this.igxObject_0 as IGxObjectEdit).CanCopy())
                                {
                                    this.igxObject_0 = null;
                                }
                            }
                            else
                            {
                                this.igxObject_0 = null;
                            }
                        }
                        if (this.igxObject_0 != null)
                        {
                            this.arrayList_0.Add(this.igxObject_0.InternalObjectName);
                            this.arrayList_0.Add(this.igxObject_0);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
            }
        }

        private void KTreeView_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.igxObject_0 != null) && ((e.Button & MouseButtons.Left) == MouseButtons.Left))
            {
                this.point_0 = SystemInformation.WorkingArea.Location;
                System.Windows.Forms.DataObject data = new System.Windows.Forms.DataObject();
                data.SetData(this.arrayList_0);
                base.DoDragDrop(data, DragDropEffects.Link | DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll);
            }
        }

        private void KTreeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                base.SelectedNode = base.GetNodeAt(e.X, e.Y);
            }
            else
            {
                this.igxObject_0 = null;
            }
        }

        private void KTreeView_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (e.EscapePressed)
            {
                e.Action = DragAction.Cancel;
            }
        }

        private void method_0()
        {
            this.container_0 = new Container();
        }

        private string method_1(IFeatureClass ifeatureClass_0)
        {
            if (ifeatureClass_0.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                return " Annontion";
            }
            switch (ifeatureClass_0.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    return " POINT";

                case esriGeometryType.esriGeometryMultipoint:
                    return " POINT";

                case esriGeometryType.esriGeometryPolyline:
                    return " LINE";

                case esriGeometryType.esriGeometryPolygon:
                    return " FILL";
            }
            return "";
        }

        private TreeNode method_10(TreeNodeCollection treeNodeCollection_0, IGxObject igxObject_1)
        {
            for (int i = 0; i < treeNodeCollection_0.Count; i++)
            {
                TreeNode node = treeNodeCollection_0[i];
                if (node.Tag == igxObject_1)
                {
                    return node;
                }
                node = this.method_10(node.Nodes, igxObject_1);
                if (node != null)
                {
                    return node;
                }
            }
            return null;
        }

        private TreeNode method_11(TreeNodeCollection treeNodeCollection_0, IGxObject igxObject_1, bool bool_2)
        {
            for (int i = 0; i < treeNodeCollection_0.Count; i++)
            {
                TreeNode node = treeNodeCollection_0[i];
                if (node.Tag == igxObject_1)
                {
                    return node;
                }
                if (bool_2)
                {
                    node = this.method_10(node.Nodes, igxObject_1);
                    if (node != null)
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        private void method_12(IGxObject igxObject_1)
        {
            IGxObject parent = igxObject_1.Parent;
            TreeNode node = this.method_10(base.Nodes, parent);
            if (node != null)
            {
                IEnumGxObject children = (parent as IGxObjectContainer).Children;
                children.Reset();
                int index = 0;
                for (IGxObject obj4 = children.Next(); obj4 != null; obj4 = children.Next())
                {
                    if (obj4 == igxObject_1)
                    {
                        break;
                    }
                    index++;
                }
                TreeNode node2 = this.method_6(igxObject_1);
                node.Nodes.Insert(index, node2);
                if ((igxObject_1 is IGxDatabase) && !(igxObject_1 as IGxDatabase).IsRemoteDatabase)
                {
                    this.method_5(node2);
                }
            }
        }

        private void method_13(IGxObject igxObject_1)
        {
            TreeNode node = this.method_10(base.Nodes, igxObject_1);
            if (node != null)
            {
                node.Text = igxObject_1.Name;
                node.Tag = igxObject_1;
                node.ImageIndex = this.method_3(igxObject_1);
                node.SelectedImageIndex = this.method_4(igxObject_1);
            }
        }

        private void method_14(IGxObject igxObject_1)
        {
            TreeNode node = this.method_10(base.Nodes, igxObject_1);
            if (node != null)
            {
                node.Nodes.Clear();
                node.ImageIndex = this.method_3(igxObject_1);
                node.SelectedImageIndex = this.method_4(igxObject_1);
                this.method_7(node);
            }
        }

        private void method_15(IGxObject igxObject_1)
        {
            IGxObject parent = igxObject_1.Parent;
            TreeNode node = this.method_10(base.Nodes, parent);
            if (node != null)
            {
                TreeNode node2 = this.method_10(base.Nodes, igxObject_1);
                if (node2 != null)
                {
                    node.Nodes.Remove(node2);
                }
            }
        }

        private void method_16()
        {
        }

        private string method_17(string string_0)
        {
            string str = string_0.Substring(0, string_0.Length - 4);
            for (int i = 1; File.Exists(string_0); i++)
            {
                string_0 = str + " (" + i.ToString() + ").odc";
            }
            return string_0;
        }

        private string method_2(IFeatureClassName ifeatureClassName_0)
        {
            if (ifeatureClassName_0.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                return " Annontion";
            }
            switch (ifeatureClassName_0.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    return " POINT";

                case esriGeometryType.esriGeometryMultipoint:
                    return " POINT";

                case esriGeometryType.esriGeometryPolyline:
                    return " LINE";

                case esriGeometryType.esriGeometryPolygon:
                    return " FILL";
            }
            return "";
        }

        private int method_3(IGxObject igxObject_1)
        {
            IFeatureClass featureClass;
            int num = 0;
            string category = igxObject_1.Category;
            if (igxObject_1 is IGxDataset)
            {
                if ((igxObject_1 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureClass)
                {
                    IFeatureClassName datasetName = (igxObject_1 as IGxDataset).DatasetName as IFeatureClassName;
                    if (datasetName.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        category = category + " Annontion";
                    }
                    else if (datasetName.ShapeType == esriGeometryType.esriGeometryNull)
                    {
                        try
                        {
                            featureClass = (datasetName as IName).Open() as IFeatureClass;
                            category = category + this.method_1(featureClass);
                        }
                        catch (Exception exception)
                        {
                            CErrorLog.writeErrorLog(this, exception, "");
                        }
                    }
                    else
                    {
                        category = category + this.method_2(datasetName);
                    }
                }
            }
            else if (igxObject_1 is IGxDatabase)
            {
                if ((igxObject_1 as IGxDatabase).IsRemoteDatabase && (igxObject_1 as IGxDatabase).IsConnected)
                {
                    category = category + " Connect";
                }
            }
            else if (igxObject_1 is IGxAGSConnection)
            {
                if ((igxObject_1 as IGxAGSConnection).IsConnected)
                {
                    category = category + " Connect";
                }
            }
            else if (igxObject_1 is IGxLayer)
            {
                ILayer layer = (igxObject_1 as IGxLayer).Layer;
                if (layer == null)
                {
                    category = category + " Unknown";
                }
                else if (layer is IGroupLayer)
                {
                    category = category + " GroupLayer";
                }
                else if (layer is IRasterLayer)
                {
                    category = category + " RasterLayer";
                }
                else if (layer is IFeatureLayer)
                {
                    featureClass = (layer as IFeatureLayer).FeatureClass;
                    if (featureClass == null)
                    {
                        category = category + " Unknown";
                    }
                    else
                    {
                        category = category + this.method_1(featureClass);
                    }
                }
            }
            else if (igxObject_1 is IGxAGSObject)
            {
                category = category + (igxObject_1 as IGxAGSObject).Status;
            }
            else if (igxObject_1 is IGxDiskConnection)
            {
                if (!Directory.Exists((igxObject_1 as IGxFile).Path))
                {
                    category = category + "_Error";
                }
            }
            else if (igxObject_1 is IGxGDSConnection)
            {
                category = category + (igxObject_1 as IGxGDSConnection).IsConnected.ToString();
            }
            if (category == "")
            {
                category = igxObject_1.Name;
            }
            if (this.sortedList_0.ContainsKey(category))
            {
                return this.sortedList_0[category];
            }
            this.imageList_0.Images.Add((igxObject_1 as IGxObjectUI).SmallImage);
            num = this.imageList_0.Images.Count - 1;
            this.sortedList_0.Add(category, num);
            return num;
        }

        private int method_4(IGxObject igxObject_1)
        {
            object property = null;
            IFeatureClass featureClass;
            string category = igxObject_1.Category;
            if (igxObject_1 is IGxDataset)
            {
                if ((igxObject_1 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureClass)
                {
                    IFeatureClassName datasetName = (igxObject_1 as IGxDataset).DatasetName as IFeatureClassName;
                    if (datasetName.ShapeType == esriGeometryType.esriGeometryNull)
                    {
                        try
                        {
                            featureClass = (datasetName as IName).Open() as IFeatureClass;
                            category = category + this.method_1(featureClass);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        category = category + this.method_2(datasetName);
                    }
                }
            }
            else if (igxObject_1 is IGxDatabase)
            {
                if ((igxObject_1 as IGxDatabase).IsRemoteDatabase && (igxObject_1 as IGxDatabase).IsConnected)
                {
                    category = category + " Connect";
                }
            }
            else if (igxObject_1 is IGxAGSConnection)
            {
                if ((igxObject_1 as IGxAGSConnection).IsConnected)
                {
                    category = category + " Connect";
                }
            }
            else if (igxObject_1 is IGxLayer)
            {
                ILayer layer = (igxObject_1 as IGxLayer).Layer;
                if (layer == null)
                {
                    category = category + " Unknown";
                }
                else if (layer is IGroupLayer)
                {
                    category = category + " GroupLayer";
                }
                else if (layer is IRasterLayer)
                {
                    category = category + " RasterLayer";
                }
                else if (layer is IFeatureLayer)
                {
                    featureClass = (layer as IFeatureLayer).FeatureClass;
                    if (featureClass == null)
                    {
                        category = category + " Unknown";
                    }
                    else
                    {
                        category = category + this.method_1(featureClass);
                    }
                }
            }
            else if (igxObject_1 is IGxAGSObject)
            {
                category = category + (igxObject_1 as IGxAGSObject).Status;
            }
            else if (igxObject_1 is IGxDiskConnection)
            {
                if (!Directory.Exists((igxObject_1 as IGxFile).Path))
                {
                    category = category + "_Error";
                }
            }
            else if (igxObject_1 is IGxGDSConnection)
            {
                category = category + (igxObject_1 as IGxGDSConnection).IsConnected.ToString();
            }
            if (category == "")
            {
                category = igxObject_1.Name;
            }
            try
            {
                property = this.ipropertySet_0.GetProperty(category);
            }
            catch
            {
            }
            try
            {
                if (property == null)
                {
                    this.imageList_0.Images.Add((igxObject_1 as IGxObjectUI).SmallSelectedImage);
                    this.ipropertySet_0.SetProperty(category, this.imageList_0.Images.Count - 1);
                    return (this.imageList_0.Images.Count - 1);
                }
                return (int) property;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return 0;
        }

        private void method_5(TreeNode treeNode_2)
        {
            TreeNode node = new TreeNode("Temp");
            treeNode_2.Nodes.Add(node);
        }

        private TreeNode method_6(IGxObject igxObject_1)
        {
            return new TreeNode(igxObject_1.Name, this.method_3(igxObject_1), this.method_4(igxObject_1)) { Tag = igxObject_1 };
        }

        private void method_7(TreeNode treeNode_2)
        {
            if ((treeNode_2.Nodes.Count <= 1) && ((treeNode_2.Nodes.Count != 1) || (treeNode_2.Nodes[0].Tag == null)))
            {
                IGxObject tag = treeNode_2.Tag as IGxObject;
                if (tag is IGxObjectContainer)
                {
                    IEnumGxObject children = (tag as IGxObjectContainer).Children;
                    treeNode_2.Nodes.Clear();
                    children.Reset();
                    for (tag = children.Next(); tag != null; tag = children.Next())
                    {
                        TreeNode node = this.method_6(tag);
                        if ((tag is IGxObjectContainer) && (tag as IGxObjectContainer).HasChildren)
                        {
                            if (tag is IGxDatabase)
                            {
                                if (!(tag as IGxDatabase).IsRemoteDatabase)
                                {
                                    this.method_5(node);
                                }
                            }
                            else
                            {
                                this.method_5(node);
                            }
                        }
                        treeNode_2.Nodes.Add(node);
                    }
                }
                else
                {
                    treeNode_2.Nodes.Clear();
                }
            }
        }

        private IFeatureClass method_8(string string_0)
        {
            string directoryName = System.IO.Path.GetDirectoryName(string_0);
            IPropertySet connectionProperties = new PropertySetClass();
            connectionProperties.SetProperty("DATABASE", directoryName);
            IWorkspaceFactory factory = new ShapefileWorkspaceFactoryClass();
            IWorkspace workspace = factory.Open(connectionProperties, 0);
            string fileName = System.IO.Path.GetFileName(string_0);
            return ((IFeatureWorkspace) workspace).OpenFeatureClass(fileName);
        }

        private IFeatureClass method_9(string string_0)
        {
            string directoryName = System.IO.Path.GetDirectoryName(string_0);
            IPropertySet connectionProperties = new PropertySetClass();
            connectionProperties.SetProperty("DATABASE", directoryName);
            IWorkspaceFactory factory = null;
            IWorkspace workspace = factory.Open(connectionProperties, 0);
            string fileName = System.IO.Path.GetFileName(string_0);
            return ((IFeatureWorkspace) workspace).OpenFeatureClass(fileName);
        }

        public void RefreshNode()
        {
            TreeNode selectedNode = base.SelectedNode;
            selectedNode.Nodes.Clear();
            IGxObject tag = selectedNode.Tag as IGxObject;
            this.AddChildNodes(tag, selectedNode);
        }

        public IGxCatalog GxCatalog
        {
            get
            {
                return this.igxCatalog_0;
            }
            set
            {
                if (value != null)
                {
                    this.igxCatalog_0 = value;
                    (this.igxCatalog_0 as IGxCatalogEvents).OnObjectAdded += new OnObjectAddedEventHandler(this.method_12);
                    (this.igxCatalog_0 as IGxCatalogEvents).OnObjectChanged += new OnObjectChangedEventHandler(this.method_13);
                    (this.igxCatalog_0 as IGxCatalogEvents).OnObjectRefreshed += new OnObjectRefreshedEventHandler(this.method_14);
                    (this.igxCatalog_0 as IGxCatalogEvents).OnObjectDeleted += new OnObjectDeletedEventHandler(this.method_15);
                    (this.igxCatalog_0 as IGxCatalogEvents).OnRefreshAll += new OnRefreshAllEventHandler(this.method_16);
                }
            }
        }
    }
}

