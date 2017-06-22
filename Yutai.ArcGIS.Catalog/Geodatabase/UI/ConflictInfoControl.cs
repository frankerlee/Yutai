using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class ConflictInfoControl : UserControl
    {
        private Container container_0 = null;
        private IArray iarray_0 = new ArrayClass();
        public IMap m_FocusMap;

        public ConflictInfoControl()
        {
            this.InitializeComponent();
        }

        private void ConflictInfoControl_Load(object sender, EventArgs e)
        {
            this.iarray_0.RemoveAll();
            this.method_0();
        }

 private void method_0()
        {
            TreeNode node = new TreeNode("冲突");
            IVersionEdit edit = this.ifeatureWorkspace_0 as IVersionEdit;
            if (edit != null)
            {
                IEnumConflictClass conflictClasses = edit.ConflictClasses;
                conflictClasses.Reset();
                for (IConflictClass class3 = conflictClasses.Next(); class3 != null; class3 = conflictClasses.Next())
                {
                    IDataset dataset = class3 as IDataset;
                    TreeNode node2 = new TreeNode(dataset.Name);
                    node.Nodes.Add(node2);
                    node2.Tag = class3;
                    this.method_1(node2, class3);
                }
                this.treeView1.Nodes.Add(node);
            }
        }

        private void method_1(TreeNode treeNode_0, IConflictClass iconflictClass_0)
        {
            this.ifeatureWorkspace_0.OpenTable((iconflictClass_0 as IDataset).Name);
            ISelectionSet deleteUpdates = iconflictClass_0.DeleteUpdates;
            this.method_3(treeNode_0, deleteUpdates, iconflictClass_0, enumConflictType.enumCTDeleteUpdates);
            deleteUpdates = iconflictClass_0.UpdateDeletes;
            this.method_3(treeNode_0, deleteUpdates, iconflictClass_0, enumConflictType.enumCTUpdateDeletes);
            deleteUpdates = iconflictClass_0.UpdateUpdates;
            this.method_3(treeNode_0, deleteUpdates, iconflictClass_0, enumConflictType.enumCTUpdateUpdates);
        }

        private void method_2(TreeNode treeNode_0, ISelectionSet iselectionSet_0, IConflictClass iconflictClass_0)
        {
            IEnumIDs iDs = iselectionSet_0.IDs;
            iDs.Reset();
            for (int i = iDs.Next(); i != -1; i = iDs.Next())
            {
                TreeNode node = new TreeNode(i.ToString());
                RowCollection unk = new RowCollection {
                    ConflictClass = iconflictClass_0
                };
                this.iarray_0.Add(unk);
                unk.OID = i;
                node.Tag = unk;
                treeNode_0.Nodes.Add(node);
            }
        }

        private void method_3(TreeNode treeNode_0, ISelectionSet iselectionSet_0, IConflictClass iconflictClass_0, enumConflictType enumConflictType_0)
        {
            IEnumIDs iDs = iselectionSet_0.IDs;
            iDs.Reset();
            for (int i = iDs.Next(); i != -1; i = iDs.Next())
            {
                TreeNode node = new TreeNode(i.ToString());
                RowCollection unk = new RowCollection {
                    ConflictClass = iconflictClass_0
                };
                this.iarray_0.Add(unk);
                unk.ConflictType = enumConflictType_0;
                unk.OID = i;
                node.Tag = unk;
                treeNode_0.Nodes.Add(node);
            }
        }

        private void RelpaceStartEditVersion_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = 0; i < this.iarray_0.Count; i++)
            {
                (this.iarray_0.get_Element(i) as RowCollection).Update(2);
            }
        }

        private void ReplaceConflictVersion_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = 0; i < this.iarray_0.Count; i++)
            {
                (this.iarray_0.get_Element(i) as RowCollection).Update(0);
            }
        }

        private void ReplaceEditVersion_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = 0; i < this.iarray_0.Count; i++)
            {
                (this.iarray_0.get_Element(i) as RowCollection).Update(1);
            }
        }

        private void ShowSetup_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.panel1.Height = e.SplitY;
            this.panel2.Height = base.Height - e.SplitY;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.listView1.Items.Clear();
            RowCollection tag = e.Node.Tag as RowCollection;
            if (tag != null)
            {
                IRow reconcileRow = tag.ReconcileRow;
                IRow preReconcileRow = tag.PreReconcileRow;
                IRow startEditingRow = tag.StartEditingRow;
                IFields fields = tag.Fields;
                if (fields != null)
                {
                    string[] items = new string[4];
                    for (int i = 0; i < fields.FieldCount; i++)
                    {
                        IField field = fields.get_Field(i);
                        items[0] = field.Name;
                        if (field.Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            items[1] = "";
                            items[2] = "";
                            items[3] = "";
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeBlob)
                        {
                            items[1] = "";
                            items[2] = "";
                            items[3] = "";
                        }
                        else
                        {
                            object obj2;
                            if (reconcileRow == null)
                            {
                                if (i == 0)
                                {
                                    items[1] = "<删除>";
                                }
                                else
                                {
                                    items[1] = "";
                                }
                            }
                            else
                            {
                                obj2 = reconcileRow.get_Value(i);
                                if (obj2 is DBNull)
                                {
                                    items[1] = "空";
                                }
                                else
                                {
                                    items[1] = obj2.ToString();
                                }
                            }
                            if (preReconcileRow == null)
                            {
                                if (i == 0)
                                {
                                    items[2] = "<删除>";
                                }
                                else
                                {
                                    items[2] = "";
                                }
                            }
                            else
                            {
                                obj2 = preReconcileRow.get_Value(i);
                                if (obj2 is DBNull)
                                {
                                    items[2] = "空";
                                }
                                else
                                {
                                    items[2] = obj2.ToString();
                                }
                            }
                            obj2 = startEditingRow.get_Value(i);
                            if (obj2 is DBNull)
                            {
                                items[3] = "空";
                            }
                            else
                            {
                                items[3] = obj2.ToString();
                            }
                        }
                        this.listView1.Items.Add(new ListViewItem(items));
                    }
                }
            }
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                p = this.treeView1.PointToScreen(p);
                this.popupMenu1.ShowPopup(p);
            }
        }

        private void ZoomToConflictVersion_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeView1.SelectedNode.Tag is RowCollection)
            {
                RowCollection tag = this.treeView1.SelectedNode.Tag as RowCollection;
                IRow preReconcileRow = tag.PreReconcileRow;
                if (preReconcileRow is IFeature)
                {
                    CommonHelper.Zoom2Feature(this.m_FocusMap as IActiveView, preReconcileRow as IFeature);
                }
            }
        }

        private void ZoomToEditVersion_ItemClick(object sender, ItemClickEventArgs e)
        {
            RowCollection tag = this.treeView1.SelectedNode.Tag as RowCollection;
            IRow reconcileRow = tag.ReconcileRow;
            if (reconcileRow is IFeature)
            {
                CommonHelper.Zoom2Feature(this.m_FocusMap as IActiveView, reconcileRow as IFeature);
            }
        }

        private void ZoomToStartEditVersion_ItemClick(object sender, ItemClickEventArgs e)
        {
            RowCollection tag = this.treeView1.SelectedNode.Tag as RowCollection;
            IRow startEditingRow = tag.StartEditingRow;
            if (startEditingRow is IFeature)
            {
                CommonHelper.Zoom2Feature(this.m_FocusMap as IActiveView, startEditingRow as IFeature);
            }
        }

        public IWorkspace EditWorkspace
        {
            set
            {
                this.ifeatureWorkspace_0 = value as IFeatureWorkspace;
            }
        }

        public IMap FocusMap
        {
            set
            {
                this.m_FocusMap = value;
            }
        }
    }
}

