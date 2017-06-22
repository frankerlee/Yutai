using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class ZDAttributeEditControl : UserControl, IDockContent
    {
        private bool m_CanDo = false;
        private bool m_CanEdit = true;
        private IMap m_EditMap = null;
        private bool m_HasLicense = false;
        private List<object> m_list = new List<object>();
        private ZDAttributeListControl m_pAttributeListControl1 = new ZDAttributeListControl();
        private IMap m_pMap = null;
        private MultiAttributeListControlExtend m_pMultiAttributeListControl = new MultiAttributeListControlExtend();
        private int m_SelectType = 0;

        public ZDAttributeEditControl()
        {
            this.InitializeComponent();
            this.Text = "宗地属性编辑";
            this.panel1.Visible = false;
            this.panel2.Visible = false;
        }

        private void AddSelectToList(ICursor pCursor, List<object> pList)
        {
            for (IRow row = pCursor.NextRow(); row != null; row = pCursor.NextRow())
            {
                pList.Add(row);
            }
        }

        private void AddSelectToTree(TreeNode pParentNode, IFeatureLayer pFL)
        {
            ICursor cursor;
            (pFL as IFeatureSelection).SelectionSet.Search(null, false, out cursor);
            for (IRow row = cursor.NextRow(); row != null; row = cursor.NextRow())
            {
                TreeNode node = new TreeNode(row.OID.ToString()) {
                    Tag = row
                };
                pParentNode.Nodes.Add(node);
            }
            ComReleaser.ReleaseCOMObject(cursor);
        }

        private void AttributeEditControlExtend_Load(object sender, EventArgs e)
        {
            if (EditorLicenseProviderCheck.Check())
            {
                this.m_HasLicense = true;
                EditorEvent.OnStartEditing += new EditorEvent.OnStartEditingHandler(this.EditorEvent_OnStartEditing);
                EditorEvent.OnStopEditing += new EditorEvent.OnStopEditingHandler(this.EditorEvent_OnStopEditing);
                this.m_CanDo = true;
                this.m_pAttributeListControl1.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(this.m_pAttributeListControl1);
                this.m_pMultiAttributeListControl.Dock = DockStyle.Fill;
                this.panel2.Controls.Add(this.m_pMultiAttributeListControl);
                this.Text = "宗地属性编辑";
                this.Init();
            }
        }

        private void DisEnable()
        {
            if (this.m_SelectType == 1)
            {
                this.m_pAttributeListControl1.SelectObject = null;
                this.panel1.Visible = false;
            }
            else if (this.m_SelectType == 3)
            {
                this.m_pMultiAttributeListControl.LayerList = null;
                this.panel2.Visible = false;
            }
            this.ZoomTo.Enabled = false;
            this.FlashObject.Enabled = false;
            this.m_SelectType = 0;
        }

 private void EditorEvent_OnStartEditing()
        {
            this.m_EditMap = this.m_pMap;
            this.m_CanEdit = true;
            this.Init();
        }

        private void EditorEvent_OnStopEditing()
        {
            this.m_EditMap = null;
            this.m_CanEdit = false;
            this.Init();
          //  ApplicationRef.Application.HideDockWindow(this);
        }

        private void FlashObject_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.m_list.Count == 1)
            {
                IActiveView pMap = this.m_pMap as IActiveView;
                IFeature feature = this.m_list[0] as IFeature;
                Flash.FlashFeature(pMap.ScreenDisplay, feature);
            }
            else if (this.m_list.Count > 1)
            {
                this.m_pMultiAttributeListControl.FlashObject();
            }
        }

        public void Init()
        {
            if (base.Visible)
            {
                this.treeView1.Nodes.Clear();
                if (this.m_HasLicense)
                {
                    this.m_list.Clear();
                    if (!(this.m_CanEdit && (this.m_pMap != null)))
                    {
                        this.DisEnable();
                    }
                    else if (this.m_pMap.SelectionCount == 0)
                    {
                        this.DisEnable();
                    }
                    else if (ZDEditTools.ZDFeatureLayer != null)
                    {
                        IFeatureLayer zDFeatureLayer = ZDEditTools.ZDFeatureLayer;
                        if ((zDFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
                        {
                            TreeNode node = new TreeNode(zDFeatureLayer.Name) {
                                Tag = zDFeatureLayer
                            };
                            this.treeView1.Nodes.Add(node);
                            this.AddSelectToTree(node, zDFeatureLayer);
                            this.treeView1.ExpandAll();
                            this.treeView1.SelectedNode = this.treeView1.Nodes[0].Nodes[0];
                        }
                    }
                }
            }
        }

 private void m_pActiveViewEvents_SelectionChanged()
        {
            this.Init();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.m_list.Clear();
            IFeatureLayer tag = null;
            if (e.Node.Tag is IFeatureLayer)
            {
                tag = e.Node.Tag as IFeatureLayer;
                for (int i = 0; i < e.Node.Nodes.Count; i++)
                {
                    this.m_list.Add(e.Node.Nodes[i].Tag);
                }
            }
            else if (e.Node.Tag is IRow)
            {
                tag = e.Node.Parent.Tag as IFeatureLayer;
                this.m_list.Add(e.Node.Tag);
            }
            if (this.m_list.Count == 0)
            {
                this.DisEnable();
            }
            else
            {
                this.ZoomTo.Enabled = true;
                this.FlashObject.Enabled = true;
                if (this.m_list.Count >= 1)
                {
                    object obj2 = this.m_list[0];
                    if (tag != null)
                    {
                        this.m_pAttributeListControl1.FeatureLayer = tag;
                    }
                    this.m_pAttributeListControl1.SelectObject = obj2 as IObject;
                    this.m_pAttributeListControl1.Visible = true;
                    if (this.m_SelectType != 1)
                    {
                        this.panel1.Visible = true;
                        this.panel2.Visible = false;
                        this.m_SelectType = 1;
                    }
                }
                else
                {
                    List<object> list = new List<object> {
                        tag
                    };
                    this.m_pMultiAttributeListControl.LayerList = list;
                    if (this.m_SelectType != 3)
                    {
                        this.panel2.Visible = true;
                        this.panel1.Visible = false;
                        this.m_SelectType = 3;
                    }
                }
            }
        }

        private void ZoomTo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.m_list.Count == 1)
            {
                IActiveView pMap = this.m_pMap as IActiveView;
                IFeature feature = this.m_list[0] as IFeature;
                CommonHelper.Zoom2Feature(pMap, feature);
            }
            else if (this.m_list.Count > 1)
            {
                this.m_pMultiAttributeListControl.ZoomToSelectObject();
            }
        }

        public DockingStyle DefaultDockingStyle
        {
            get
            {
                return DockingStyle.Right;
            }
        }

        public IMap FocusMap
        {
            set
            {
                if (this.m_pActiveViewEvents != null)
                {
                    try
                    {
                        this.m_pActiveViewEvents.SelectionChanged-=(new IActiveViewEvents_SelectionChangedEventHandler(this.m_pActiveViewEvents_SelectionChanged));
                    }
                    catch
                    {
                    }
                }
                this.m_pMap = value;
                if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null)
                {
                    if (this.m_pMap == Yutai.ArcGIS.Common.Editor.Editor.EditMap)
                    {
                        this.m_CanEdit = true;
                    }
                    else
                    {
                        this.m_CanEdit = false;
                    }
                }
                if (this.m_pMap != null)
                {
                    this.m_pActiveViewEvents = this.m_pMap as IActiveViewEvents_Event;
                    this.m_pActiveViewEvents.SelectionChanged+=(new IActiveViewEvents_SelectionChangedEventHandler(this.m_pActiveViewEvents_SelectionChanged));
                    this.m_pAttributeListControl1.ActiveView = this.m_pMap as IActiveView;
                    this.m_pMultiAttributeListControl.ActiveView = this.m_pMap as IActiveView;
                    if (this.m_CanDo)
                    {
                        this.Init();
                    }
                }
            }
        }

        string IDockContent.Name
        {
            get
            {
                return base.Name;
            }
        }

        int IDockContent.Width
        {
            get
            {
                return base.Width;
            }
        }
    }
}

