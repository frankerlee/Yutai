using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Plugins.Events;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class EditorTemplateManageCtrl : UserControl, IDockContent
    {
        public static Dictionary<IMap, Dictionary<IFeatureLayer, List<YTEditTemplate>>> EditMap = new Dictionary<IMap, Dictionary<IFeatureLayer, List<YTEditTemplate>>>();
        private List<IFeatureLayer> FilterLayers = new List<IFeatureLayer>();
        private bool IsLoad = false;
        private FilterType m_FilterType = FilterType.NoFilter;
        private GroupType m_GroupType = GroupType.GroupByLayer;
        internal static Dictionary<IFeatureLayer, List<YTEditTemplate>> m_list = null;
        public IMap m_Map = null;

        public EditorTemplateManageCtrl()
        {
            this.InitializeComponent();
            base.VisibleChanged += new EventHandler(this.EditorTemplateManageCtrl_VisibleChanged);
            this.Text = "创建属性";
            EditTemplateManager.OnDeleteTemplate += new OnDeleteTemplateHandler(this.EditTemplateManager_OnDeleteTemplate);
            EditTemplateManager.OnAddTemplate += new OnAddTemplateHandler(this.EditTemplateManager_OnAddTemplate);
            EditTemplateManager.OnAddMoreTemplate += new OnAddMoreTemplateHandler(this.EditTemplateManager_OnAddMoreTemplate);
            if( ApplicationRef.Application != null)
            (ApplicationRef.Application as IApplicationEvents).OnMapDocumentChangedEvent += new OnMapDocumentChangedEventHandler(this.EditorTemplateManageCtrl_OnMapDocumentChangedEvent);
            EditTemplateManager.OnTemplatePropertyChange += new OnTemplatePropertyChangeHandler(this.EditTemplateManager_OnTemplatePropertyChange);
            EditTemplateManager.OnDeleteMoreTemplate += new OnDeleteMoreTemplateHandler(this.EditTemplateManager_OnDeleteMoreTemplate);
            if (ApplicationRef.Application != null)
                (ApplicationRef.Application as IApplicationEvents).OnMapCloseEvent += new OnMapCloseEventHandler(this.EditorTemplateManageCtrl_OnMapCloseEvent);
        }

        private void AddTemplate(YTEditTemplate template)
        {
            List<YTEditTemplate> list;
            ListViewGroup group;
            ListViewItem item;
            string str;
            IFeatureLayer featureLayer = template.FeatureLayer;
            if (!m_list.ContainsKey(featureLayer))
            {
                list = new List<YTEditTemplate>();
                m_list.Add(featureLayer, list);
            }
            else
            {
                list = m_list[featureLayer];
            }
            list.Add(template);
            if (this.m_GroupType != GroupType.GroupByLayer)
            {
                if (this.m_GroupType != GroupType.GroupByGeometryType)
                {
                    if (this.IsAdd(featureLayer))
                    {
                        item = new ListViewItem {
                            Text = template.Name,
                            Tag = template
                        };
                        this.listView1.AddItem(item);
                    }
                    return;
                }
                esriGeometryType shapeType = template.FeatureLayer.FeatureClass.ShapeType;
                esriFeatureType featureType = template.FeatureLayer.FeatureClass.FeatureType;
                str = "";
                if (featureType == esriFeatureType.esriFTAnnotation)
                {
                    str = "注记";
                }
                else
                {
                    switch (shapeType)
                    {
                        case esriGeometryType.esriGeometryPolyline:
                            str = "线";
                            break;

                        case esriGeometryType.esriGeometryPolygon:
                            str = "面";
                            break;

                        case esriGeometryType.esriGeometryPoint:
                        case esriGeometryType.esriGeometryMultipoint:
                            str = "点";
                            break;
                    }
                }
                group = null;
                foreach (ListViewGroup group2 in this.listView1.Groups)
                {
                    if (group2.Name == str)
                    {
                        group = group2;
                        break;
                    }
                }
            }
            else
            {
                group = null;
                foreach (ListViewGroup group2 in this.listView1.Groups)
                {
                    if (group2.Tag == featureLayer)
                    {
                        group = group2;
                        break;
                    }
                }
                if ((group == null) && this.IsAdd(featureLayer))
                {
                    group = new ListViewGroup {
                        Tag = featureLayer,
                        Name = featureLayer.Name,
                        Header = featureLayer.Name
                    };
                    this.listView1.Groups.Add(group);
                }
                if (group != null)
                {
                    item = new ListViewItem {
                        Text = template.Name,
                        Tag = template,
                        Group = group
                    };
                    this.listView1.AddItem(item);
                }
                return;
            }
            if ((group == null) && this.IsAdd(featureLayer))
            {
                group = new ListViewGroup {
                    Name = str,
                    Header = str
                };
                this.listView1.Groups.Add(group);
            }
            if (group != null)
            {
                item = new ListViewItem {
                    Text = template.Name,
                    Tag = template,
                    Group = group
                };
                this.listView1.AddItem(item);
            }
        }

        private void AnnoFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_FilterType != FilterType.Anno)
            {
                this.m_FilterType = FilterType.Anno;
                this.InitControl();
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                YTEditTemplate template = (item.Tag as YTEditTemplate).Clone();
                m_list[template.FeatureLayer].Add(template);
                ListViewItem li = new ListViewItem {
                    Text = template.Name,
                    Tag = template,
                    Group = item.Group
                };
                this.listView1.AddItem(li);
            }
        }

        private void DeleteTemplete(YTEditTemplate template)
        {
            foreach (KeyValuePair<IFeatureLayer, List<YTEditTemplate>> pair in m_list)
            {
                foreach (YTEditTemplate template2 in pair.Value)
                {
                    if (template2 == template)
                    {
                        pair.Value.Remove(template2);
                        break;
                    }
                }
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                list.Add(item);
                this.DeleteTemplete(item.Tag as YTEditTemplate);
            }
            foreach (ListViewItem item2 in list)
            {
                this.listView1.Items.Remove(item2);
            }
        }

 private void EditorEvent_OnStopEditing()
        {
            ApplicationRef.Application.HideDockWindow(this);
        }

        private void EditorTemplateManageCtrl_ItemDeleted(object Item)
        {
            if (Item is IFeatureLayer)
            {
                List<ListViewItem> list = new List<ListViewItem>();
                for (int i = 0; i < this.listView1.Groups.Count; i++)
                {
                    ListViewGroup group = this.listView1.Groups[i];
                    if (group.Tag == Item)
                    {
                        foreach (ListViewItem item in group.Items)
                        {
                            list.Add(item);
                        }
                        break;
                    }
                }
                foreach (ListViewItem item2 in list)
                {
                    this.listView1.Items.Remove(item2);
                }
                if (m_list.ContainsKey(Item as IFeatureLayer))
                {
                    m_list.Remove(Item as IFeatureLayer);
                }
            }
        }

        private void EditorTemplateManageCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
            this.InitControl();
            EditorEvent.OnStopEditing += new EditorEvent.OnStopEditingHandler(this.EditorEvent_OnStopEditing);
            this.IsLoad = true;
        }

        private void EditorTemplateManageCtrl_OnMapCloseEvent()
        {
        }

        private void EditorTemplateManageCtrl_OnMapDocumentChangedEvent()
        {
            if (ApplicationRef.Application.FocusMap == this.m_Map)
            {
                m_list.Clear();
                this.InitControl();
            }
        }

        private void EditorTemplateManageCtrl_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible && (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate == null))
            {
                this.listView1.SelectedItems.Clear();
            }
        }

        private void EditTemplateManager_OnAddMoreTemplate(List<YTEditTemplate> template)
        {
            foreach (YTEditTemplate template2 in template)
            {
                this.AddTemplate(template2);
            }
        }

        private void EditTemplateManager_OnAddTemplate(YTEditTemplate template)
        {
            this.AddTemplate(template);
        }

        private void EditTemplateManager_OnDeleteMoreTemplate(List<YTEditTemplate> template)
        {
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (YTEditTemplate template2 in template)
            {
                foreach (ListViewItem item in this.listView1.Items)
                {
                    if (item.Tag == template2)
                    {
                        this.DeleteTemplete(item.Tag as YTEditTemplate);
                        list.Add(item);
                        break;
                    }
                }
            }
            foreach (ListViewItem item2 in list)
            {
                this.listView1.Items.Remove(item2);
            }
        }

        private void EditTemplateManager_OnDeleteTemplate(YTEditTemplate template)
        {
            foreach (ListViewItem item in this.listView1.Items)
            {
                if (item.Tag == template)
                {
                    this.DeleteTemplete(item.Tag as YTEditTemplate);
                    this.listView1.Items.Remove(item);
                    break;
                }
            }
        }

        private void EditTemplateManager_OnTemplatePropertyChange(YTEditTemplate template)
        {
            foreach (ListViewItem item in this.listView1.Items)
            {
                if (item.Tag == template)
                {
                    item.Text = template.Name;
                    break;
                }
            }
        }

        private void FillFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_FilterType != FilterType.Fill)
            {
                this.m_FilterType = FilterType.Fill;
                this.InitControl();
            }
        }

        private void GeometryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_GroupType != GroupType.GroupByGeometryType)
            {
                this.m_GroupType = GroupType.GroupByGeometryType;
                this.InitControl();
            }
        }

        public void Init()
        {
            if (m_list == null)
            {
                m_list = new Dictionary<IFeatureLayer, List<YTEditTemplate>>();
                if (!EditMap.ContainsKey(this.m_Map))
                {
                    EditMap.Add(this.m_Map, m_list);
                }
            }
            if (m_list.Count == 0)
            {
                UID uid = new UIDClass {
                    Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}"
                };
                IEnumLayer layer = this.Map.get_Layers(uid, true);
                layer.Reset();
                for (ILayer layer2 = layer.Next(); layer2 != null; layer2 = layer.Next())
                {
                    if ((layer2 is IFeatureLayer) && Yutai.ArcGIS.Common.Editor.Editor.LayerCanEdit(layer2 as IFeatureLayer))
                    {
                        List<YTEditTemplate> list = YTEditTemplateFactory.Create(layer2 as IFeatureLayer);
                        m_list.Add(layer2 as IFeatureLayer, list);
                    }
                }
            }
        }

        private void InitControl()
        {
            ListViewGroup group;
            ListViewItem item;
            this.SetToolItemCheck();
            this.listView1.Groups.Clear();
            this.listView1.Items.Clear();
            if (this.m_GroupType == GroupType.GroupByLayer)
            {
                foreach (KeyValuePair<IFeatureLayer, List<YTEditTemplate>> pair in m_list)
                {
                    if (this.IsAdd(pair.Key))
                    {
                        group = new ListViewGroup {
                            Tag = pair.Key,
                            Name = pair.Key.Name,
                            Header = pair.Key.Name
                        };
                        this.listView1.Groups.Add(group);
                        if (pair.Value != null)
                        {
                            foreach (YTEditTemplate template in pair.Value)
                            {
                                item = new ListViewItem {
                                    Text = template.Name,
                                    Tag = template,
                                    Group = group
                                };
                                this.listView1.AddItem(item);
                            }
                        }
                    }
                }
            }
            else if (this.m_GroupType == GroupType.GroupByGeometryType)
            {
                ListViewGroup group2 = null;
                ListViewGroup group3 = null;
                ListViewGroup group4 = null;
                ListViewGroup group5 = null;
                foreach (KeyValuePair<IFeatureLayer, List<YTEditTemplate>> pair in m_list)
                {
                    if (!this.IsAdd(pair.Key))
                    {
                        continue;
                    }
                    esriGeometryType shapeType = pair.Key.FeatureClass.ShapeType;
                    esriFeatureType featureType = pair.Key.FeatureClass.FeatureType;
                    group = null;
                    if (featureType == esriFeatureType.esriFTAnnotation)
                    {
                        if (group5 == null)
                        {
                            group5 = new ListViewGroup {
                                Name = "注记",
                                Header = "注记"
                            };
                        }
                        group = group5;
                    }
                    else
                    {
                        switch (shapeType)
                        {
                            case esriGeometryType.esriGeometryPolyline:
                                if (group3 == null)
                                {
                                    group3 = new ListViewGroup {
                                        Name = "线",
                                        Header = "线"
                                    };
                                }
                                group = group3;
                                goto Label_0337;

                            case esriGeometryType.esriGeometryPolygon:
                                if (group2 == null)
                                {
                                    group2 = new ListViewGroup {
                                        Name = "面",
                                        Header = "面"
                                    };
                                }
                                group = group2;
                                goto Label_0337;
                        }
                        if ((shapeType != esriGeometryType.esriGeometryPoint) && (shapeType != esriGeometryType.esriGeometryMultipoint))
                        {
                            continue;
                        }
                        if (group4 == null)
                        {
                            group4 = new ListViewGroup {
                                Name = "点",
                                Header = "点"
                            };
                        }
                        group = group4;
                    }
                Label_0337:
                    this.listView1.Groups.Add(group);
                    if (pair.Value != null)
                    {
                        foreach (YTEditTemplate template in pair.Value)
                        {
                            item = new ListViewItem {
                                Text = template.Name,
                                Tag = template,
                                Group = group
                            };
                            this.listView1.AddItem(item);
                        }
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<IFeatureLayer, List<YTEditTemplate>> pair in m_list)
                {
                    if (this.IsAdd(pair.Key) && (pair.Value != null))
                    {
                        foreach (YTEditTemplate template in pair.Value)
                        {
                            item = new ListViewItem {
                                Text = template.Name,
                                Tag = template
                            };
                            this.listView1.AddItem(item);
                        }
                    }
                }
            }
        }

 private bool IsAdd(IFeatureLayer template)
        {
            if (this.m_FilterType == FilterType.NoFilter)
            {
                return true;
            }
            esriGeometryType shapeType = template.FeatureClass.ShapeType;
            esriFeatureType featureType = template.FeatureClass.FeatureType;
            if (this.m_FilterType == FilterType.Point)
            {
                if ((featureType == esriFeatureType.esriFTSimple) && ((shapeType == esriGeometryType.esriGeometryPoint) || (shapeType == esriGeometryType.esriGeometryMultipoint)))
                {
                    return true;
                }
            }
            else if (this.m_FilterType == FilterType.Line)
            {
                if ((featureType == esriFeatureType.esriFTSimple) && (shapeType == esriGeometryType.esriGeometryPolyline))
                {
                    return true;
                }
            }
            else if (this.m_FilterType == FilterType.Fill)
            {
                if ((featureType == esriFeatureType.esriFTSimple) && (shapeType == esriGeometryType.esriGeometryPolygon))
                {
                    return true;
                }
            }
            else if (this.m_FilterType == FilterType.Anno)
            {
                if (featureType == esriFeatureType.esriFTAnnotation)
                {
                    return true;
                }
            }
            else if ((this.m_FilterType == FilterType.Layer) && (this.FilterLayers.IndexOf(template) == -1))
            {
                return true;
            }
            return false;
        }

        private bool IsAdd(YTEditTemplate template)
        {
            if (this.m_FilterType == FilterType.NoFilter)
            {
                return true;
            }
            esriGeometryType shapeType = template.FeatureLayer.FeatureClass.ShapeType;
            esriFeatureType featureType = template.FeatureLayer.FeatureClass.FeatureType;
            if (this.m_FilterType == FilterType.Point)
            {
                if ((featureType == esriFeatureType.esriFTSimple) && ((shapeType == esriGeometryType.esriGeometryPoint) || (shapeType == esriGeometryType.esriGeometryMultipoint)))
                {
                    return true;
                }
            }
            else if (this.m_FilterType == FilterType.Line)
            {
                if ((featureType == esriFeatureType.esriFTSimple) && (shapeType == esriGeometryType.esriGeometryPolyline))
                {
                    return true;
                }
            }
            else if (this.m_FilterType == FilterType.Fill)
            {
                if ((featureType == esriFeatureType.esriFTSimple) && (shapeType == esriGeometryType.esriGeometryPolygon))
                {
                    return true;
                }
            }
            else if (this.m_FilterType == FilterType.Anno)
            {
                if (featureType == esriFeatureType.esriFTAnnotation)
                {
                    return true;
                }
            }
            else if ((this.m_FilterType == FilterType.Layer) && (this.FilterLayers.IndexOf(template.FeatureLayer) != -1))
            {
                return true;
            }
            return false;
        }

        private void LayerFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IFeatureLayer> list = new List<IFeatureLayer>();
            foreach (KeyValuePair<IFeatureLayer, List<YTEditTemplate>> pair in m_list)
            {
                list.Add(pair.Key);
            }
            if (list.Count > 0)
            {
                frmFilterLayerSelect select = new frmFilterLayerSelect {
                    FilterLayers = this.FilterLayers,
                    Layers = list
                };
                if (select.ShowDialog() == DialogResult.OK)
                {
                    this.m_FilterType = FilterType.Layer;
                    this.InitControl();
                }
            }
        }

        private void LayerGroupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.m_GroupType != GroupType.GroupByLayer)
            {
                this.m_GroupType = GroupType.GroupByLayer;
                this.InitControl();
            }
        }

        private void LineFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_FilterType != FilterType.Line)
            {
                this.m_FilterType = FilterType.Line;
                this.InitControl();
            }
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate = this.listView1.SelectedItems[0].Tag as YTEditTemplate;
                ApplicationRef.Application.UpdateUI();
            }
            else
            {
                Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate = null;
                ApplicationRef.Application.UpdateUI();
            }
            if ((e.Button == MouseButtons.Right) && (this.listView1.SelectedItems.Count > 0))
            {
                this.contextMenuStrip1.Show(this.listView1, e.Location);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void PointFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_FilterType != FilterType.Point)
            {
                this.m_FilterType = FilterType.Point;
                this.InitControl();
            }
        }

        private void PropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTemplateProperty property = new frmEditTemplateProperty();
            YTEditTemplate tag = this.listView1.SelectedItems[0].Tag as YTEditTemplate;
            property.EditTemplate = tag;
            if ((property.ShowDialog() == DialogResult.OK) && (this.listView1.SelectedItems[0].Text != tag.Name))
            {
                this.listView1.SelectedItems[0].Text = tag.Name;
            }
        }

        private void SetToolItemCheck()
        {
            this.清除分组ToolStripMenuItem.Enabled = this.m_GroupType != GroupType.NoGroup;
            this.LayerGroupToolStripMenuItem1.Checked = this.m_GroupType == GroupType.GroupByLayer;
            this.GeometryToolStripMenuItem.Checked = this.m_GroupType == GroupType.GroupByGeometryType;
            this.ShowAllToolStripMenuItem.Checked = this.m_FilterType == FilterType.NoFilter;
            this.PointFilterToolStripMenuItem.Checked = this.m_FilterType == FilterType.Point;
            this.LineFilterToolStripMenuItem.Checked = this.m_FilterType == FilterType.Line;
            this.FillFilterToolStripMenuItem.Checked = this.m_FilterType == FilterType.Fill;
            this.AnnoFilterToolStripMenuItem.Checked = this.m_FilterType == FilterType.Anno;
            this.LayerFilterToolStripMenuItem.Checked = this.m_FilterType == FilterType.Layer;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new frmTemplatesGroup { Map = this.Map, Templates = m_list }.ShowDialog();
        }

        private void 清除分组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_GroupType != GroupType.NoGroup)
            {
                this.m_GroupType = GroupType.NoGroup;
                this.InitControl();
            }
        }

        private void 显示所有模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_FilterType != FilterType.NoFilter)
            {
                this.m_FilterType = FilterType.NoFilter;
                this.InitControl();
            }
        }

        public DockingStyle DefaultDockingStyle
        {
            get
            {
                return DockingStyle.Right;
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

        public IMap Map
        {
            get
            {
                return this.m_Map;
            }
            set
            {
                if (this.m_Map != null)
                {
                    (this.m_Map as IActiveViewEvents_Event).ItemDeleted-=(new IActiveViewEvents_ItemDeletedEventHandler(this.EditorTemplateManageCtrl_ItemDeleted));
                }
                this.m_Map = value;
                (this.m_Map as IActiveViewEvents_Event).ItemDeleted+=(new IActiveViewEvents_ItemDeletedEventHandler(this.EditorTemplateManageCtrl_ItemDeleted));
                if (EditMap.ContainsKey(this.m_Map))
                {
                    m_list = EditMap[this.Map];
                    m_list.Clear();
                }
                else
                {
                    m_list = null;
                }
                this.Init();
                this.InitControl();
            }
        }

        private enum FilterType
        {
            NoFilter,
            Point,
            Line,
            Fill,
            Anno,
            Layer
        }

        private enum GroupType
        {
            NoGroup,
            GroupByGeometryType,
            GroupByLayer
        }
    }
}

