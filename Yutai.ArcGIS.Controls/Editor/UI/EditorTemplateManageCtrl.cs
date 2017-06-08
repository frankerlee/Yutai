using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class EditorTemplateManageCtrl : UserControl, IDockContent
    {
        private ToolStripMenuItem AnnoFilterToolStripMenuItem;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem CopyToolStripMenuItem;
        private ToolStripMenuItem DeleteToolStripMenuItem;
        public static Dictionary<IMap, Dictionary<IFeatureLayer, List<JLKEditTemplate>>> EditMap = new Dictionary<IMap, Dictionary<IFeatureLayer, List<JLKEditTemplate>>>();
        private ToolStripMenuItem FillFilterToolStripMenuItem;
        private List<IFeatureLayer> FilterLayers = new List<IFeatureLayer>();
        private ToolStripMenuItem GeometryToolStripMenuItem;
        private bool IsLoad = false;
        private ToolStripMenuItem LayerFilterToolStripMenuItem;
        private ToolStripMenuItem LayerGroupToolStripMenuItem1;
        private ToolStripMenuItem LineFilterToolStripMenuItem;
        private EditTemplateListView listView1;
        private FilterType m_FilterType = FilterType.NoFilter;
        private GroupType m_GroupType = GroupType.GroupByLayer;
        internal static Dictionary<IFeatureLayer, List<JLKEditTemplate>> m_list = null;
        public IMap m_Map = null;
        private ToolStripMenuItem PointFilterToolStripMenuItem;
        private ToolStripMenuItem PropertyToolStripMenuItem;
        private ToolStripMenuItem ShowAllToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem 分组依据ToolStripMenuItem;
        private ToolStripMenuItem 过滤依据ToolStripMenuItem;
        private ToolStripMenuItem 清除分组ToolStripMenuItem;

        public EditorTemplateManageCtrl()
        {
            this.InitializeComponent();
            base.VisibleChanged += new EventHandler(this.EditorTemplateManageCtrl_VisibleChanged);
            this.Text = "创建属性";
            EditTemplateManager.OnDeleteTemplate += new OnDeleteTemplateHandler(this.EditTemplateManager_OnDeleteTemplate);
            EditTemplateManager.OnAddTemplate += new OnAddTemplateHandler(this.EditTemplateManager_OnAddTemplate);
            EditTemplateManager.OnAddMoreTemplate += new OnAddMoreTemplateHandler(this.EditTemplateManager_OnAddMoreTemplate);
            (ApplicationRef.Application as IApplicationEvents).OnMapDocumentChangedEvent += new OnMapDocumentChangedEventHandler(this.EditorTemplateManageCtrl_OnMapDocumentChangedEvent);
            EditTemplateManager.OnTemplatePropertyChange += new OnTemplatePropertyChangeHandler(this.EditTemplateManager_OnTemplatePropertyChange);
            EditTemplateManager.OnDeleteMoreTemplate += new OnDeleteMoreTemplateHandler(this.EditTemplateManager_OnDeleteMoreTemplate);
            (ApplicationRef.Application as IApplicationEvents).OnMapCloseEvent += new OnMapCloseEventHandler(this.EditorTemplateManageCtrl_OnMapCloseEvent);
        }

        private void AddTemplate(JLKEditTemplate template)
        {
            List<JLKEditTemplate> list;
            ListViewGroup group;
            ListViewItem item;
            string str;
            IFeatureLayer featureLayer = template.FeatureLayer;
            if (!m_list.ContainsKey(featureLayer))
            {
                list = new List<JLKEditTemplate>();
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
                JLKEditTemplate template = (item.Tag as JLKEditTemplate).Clone();
                m_list[template.FeatureLayer].Add(template);
                ListViewItem li = new ListViewItem {
                    Text = template.Name,
                    Tag = template,
                    Group = item.Group
                };
                this.listView1.AddItem(li);
            }
        }

        private void DeleteTemplete(JLKEditTemplate template)
        {
            foreach (KeyValuePair<IFeatureLayer, List<JLKEditTemplate>> pair in m_list)
            {
                foreach (JLKEditTemplate template2 in pair.Value)
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
                this.DeleteTemplete(item.Tag as JLKEditTemplate);
            }
            foreach (ListViewItem item2 in list)
            {
                this.listView1.Items.Remove(item2);
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
            if (base.Visible && (Editor.Editor.CurrentEditTemplate == null))
            {
                this.listView1.SelectedItems.Clear();
            }
        }

        private void EditTemplateManager_OnAddMoreTemplate(List<JLKEditTemplate> template)
        {
            foreach (JLKEditTemplate template2 in template)
            {
                this.AddTemplate(template2);
            }
        }

        private void EditTemplateManager_OnAddTemplate(JLKEditTemplate template)
        {
            this.AddTemplate(template);
        }

        private void EditTemplateManager_OnDeleteMoreTemplate(List<JLKEditTemplate> template)
        {
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (JLKEditTemplate template2 in template)
            {
                foreach (ListViewItem item in this.listView1.Items)
                {
                    if (item.Tag == template2)
                    {
                        this.DeleteTemplete(item.Tag as JLKEditTemplate);
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

        private void EditTemplateManager_OnDeleteTemplate(JLKEditTemplate template)
        {
            foreach (ListViewItem item in this.listView1.Items)
            {
                if (item.Tag == template)
                {
                    this.DeleteTemplete(item.Tag as JLKEditTemplate);
                    this.listView1.Items.Remove(item);
                    break;
                }
            }
        }

        private void EditTemplateManager_OnTemplatePropertyChange(JLKEditTemplate template)
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
                m_list = new Dictionary<IFeatureLayer, List<JLKEditTemplate>>();
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
                    if ((layer2 is IFeatureLayer) && Editor.Editor.LayerCanEdit(layer2 as IFeatureLayer))
                    {
                        List<JLKEditTemplate> list = JLKEditTemplateFactory.Create(layer2 as IFeatureLayer);
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
                foreach (KeyValuePair<IFeatureLayer, List<JLKEditTemplate>> pair in m_list)
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
                            foreach (JLKEditTemplate template in pair.Value)
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
                foreach (KeyValuePair<IFeatureLayer, List<JLKEditTemplate>> pair in m_list)
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
                        foreach (JLKEditTemplate template in pair.Value)
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
                foreach (KeyValuePair<IFeatureLayer, List<JLKEditTemplate>> pair in m_list)
                {
                    if (this.IsAdd(pair.Key) && (pair.Value != null))
                    {
                        foreach (JLKEditTemplate template in pair.Value)
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorTemplateManageCtrl));
            this.toolStrip1 = new ToolStrip();
            this.toolStripDropDownButton1 = new ToolStripDropDownButton();
            this.ShowAllToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.过滤依据ToolStripMenuItem = new ToolStripMenuItem();
            this.PointFilterToolStripMenuItem = new ToolStripMenuItem();
            this.LineFilterToolStripMenuItem = new ToolStripMenuItem();
            this.FillFilterToolStripMenuItem = new ToolStripMenuItem();
            this.AnnoFilterToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.LayerFilterToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator4 = new ToolStripSeparator();
            this.分组依据ToolStripMenuItem = new ToolStripMenuItem();
            this.GeometryToolStripMenuItem = new ToolStripMenuItem();
            this.LayerGroupToolStripMenuItem1 = new ToolStripMenuItem();
            this.toolStripSeparator5 = new ToolStripSeparator();
            this.清除分组ToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripButton1 = new ToolStripButton();
            this.listView1 = new EditTemplateListView();
            this.contextMenuStrip1 = new ContextMenuStrip();
            this.DeleteToolStripMenuItem = new ToolStripMenuItem();
            this.CopyToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.PropertyToolStripMenuItem = new ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripDropDownButton1, this.toolStripButton1 });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x159, 0x19);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            this.toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { this.ShowAllToolStripMenuItem, this.toolStripSeparator3, this.过滤依据ToolStripMenuItem, this.toolStripSeparator4, this.分组依据ToolStripMenuItem });
            this.toolStripDropDownButton1.Image = (Image) resources.GetObject("toolStripDropDownButton1.Image");
            this.toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new Size(0x1d, 0x16);
            this.toolStripDropDownButton1.Text = "通过分组和过滤排列模板";
            this.ShowAllToolStripMenuItem.Name = "ShowAllToolStripMenuItem";
            this.ShowAllToolStripMenuItem.Size = new Size(0x94, 0x16);
            this.ShowAllToolStripMenuItem.Text = "显示所有模板";
            this.ShowAllToolStripMenuItem.Click += new EventHandler(this.显示所有模板ToolStripMenuItem_Click);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(0x91, 6);
            this.过滤依据ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.PointFilterToolStripMenuItem, this.LineFilterToolStripMenuItem, this.FillFilterToolStripMenuItem, this.AnnoFilterToolStripMenuItem, this.toolStripSeparator2, this.LayerFilterToolStripMenuItem });
            this.过滤依据ToolStripMenuItem.Name = "过滤依据ToolStripMenuItem";
            this.过滤依据ToolStripMenuItem.Size = new Size(0x94, 0x16);
            this.过滤依据ToolStripMenuItem.Text = "过滤依据";
            this.PointFilterToolStripMenuItem.Name = "PointFilterToolStripMenuItem";
            this.PointFilterToolStripMenuItem.Size = new Size(100, 0x16);
            this.PointFilterToolStripMenuItem.Text = "点";
            this.PointFilterToolStripMenuItem.Click += new EventHandler(this.PointFilterToolStripMenuItem_Click);
            this.LineFilterToolStripMenuItem.Name = "LineFilterToolStripMenuItem";
            this.LineFilterToolStripMenuItem.Size = new Size(100, 0x16);
            this.LineFilterToolStripMenuItem.Text = "线";
            this.LineFilterToolStripMenuItem.Click += new EventHandler(this.LineFilterToolStripMenuItem_Click);
            this.FillFilterToolStripMenuItem.Name = "FillFilterToolStripMenuItem";
            this.FillFilterToolStripMenuItem.Size = new Size(100, 0x16);
            this.FillFilterToolStripMenuItem.Text = "面";
            this.FillFilterToolStripMenuItem.Click += new EventHandler(this.FillFilterToolStripMenuItem_Click);
            this.AnnoFilterToolStripMenuItem.Name = "AnnoFilterToolStripMenuItem";
            this.AnnoFilterToolStripMenuItem.Size = new Size(100, 0x16);
            this.AnnoFilterToolStripMenuItem.Text = "注记";
            this.AnnoFilterToolStripMenuItem.Click += new EventHandler(this.AnnoFilterToolStripMenuItem_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(0x61, 6);
            this.LayerFilterToolStripMenuItem.Name = "LayerFilterToolStripMenuItem";
            this.LayerFilterToolStripMenuItem.Size = new Size(100, 0x16);
            this.LayerFilterToolStripMenuItem.Text = "图层";
            this.LayerFilterToolStripMenuItem.Click += new EventHandler(this.LayerFilterToolStripMenuItem_Click);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new Size(0x91, 6);
            this.分组依据ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.GeometryToolStripMenuItem, this.LayerGroupToolStripMenuItem1, this.toolStripSeparator5, this.清除分组ToolStripMenuItem });
            this.分组依据ToolStripMenuItem.Name = "分组依据ToolStripMenuItem";
            this.分组依据ToolStripMenuItem.Size = new Size(0x94, 0x16);
            this.分组依据ToolStripMenuItem.Text = "分组依据";
            this.GeometryToolStripMenuItem.Name = "GeometryToolStripMenuItem";
            this.GeometryToolStripMenuItem.Size = new Size(0x7c, 0x16);
            this.GeometryToolStripMenuItem.Text = "类型";
            this.GeometryToolStripMenuItem.Click += new EventHandler(this.GeometryToolStripMenuItem_Click);
            this.LayerGroupToolStripMenuItem1.Name = "LayerGroupToolStripMenuItem1";
            this.LayerGroupToolStripMenuItem1.Size = new Size(0x7c, 0x16);
            this.LayerGroupToolStripMenuItem1.Text = "图层";
            this.LayerGroupToolStripMenuItem1.Click += new EventHandler(this.LayerGroupToolStripMenuItem1_Click);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new Size(0x79, 6);
            this.清除分组ToolStripMenuItem.Name = "清除分组ToolStripMenuItem";
            this.清除分组ToolStripMenuItem.Size = new Size(0x7c, 0x16);
            this.清除分组ToolStripMenuItem.Text = "清除分组";
            this.清除分组ToolStripMenuItem.Click += new EventHandler(this.清除分组ToolStripMenuItem_Click);
            this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = (Image) resources.GetObject("toolStripButton1.Image");
            this.toolStripButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new Size(0x17, 0x16);
            this.toolStripButton1.Text = "组织模板";
            this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0x19);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x159, 0x138);
            this.listView1.TabIndex = 1;
            this.listView1.View = View.SmallIcon;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseUp += new MouseEventHandler(this.listView1_MouseUp);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.DeleteToolStripMenuItem, this.CopyToolStripMenuItem, this.toolStripSeparator1, this.PropertyToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x65, 0x4c);
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new Size(100, 0x16);
            this.DeleteToolStripMenuItem.Text = "删除";
            this.DeleteToolStripMenuItem.Click += new EventHandler(this.DeleteToolStripMenuItem_Click);
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new Size(100, 0x16);
            this.CopyToolStripMenuItem.Text = "复制";
            this.CopyToolStripMenuItem.Click += new EventHandler(this.CopyToolStripMenuItem_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(0x61, 6);
            this.PropertyToolStripMenuItem.Name = "PropertyToolStripMenuItem";
            this.PropertyToolStripMenuItem.Size = new Size(100, 0x16);
            this.PropertyToolStripMenuItem.Text = "属性";
            this.PropertyToolStripMenuItem.Click += new EventHandler(this.PropertyToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.toolStrip1);
            base.Name = "EditorTemplateManageCtrl";
            base.Size = new Size(0x159, 0x151);
            base.Load += new EventHandler(this.EditorTemplateManageCtrl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
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

        private bool IsAdd(JLKEditTemplate template)
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
            foreach (KeyValuePair<IFeatureLayer, List<JLKEditTemplate>> pair in m_list)
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
                Editor.Editor.CurrentEditTemplate = this.listView1.SelectedItems[0].Tag as JLKEditTemplate;
                ApplicationRef.Application.UpdateUI();
            }
            else
            {
                Editor.Editor.CurrentEditTemplate = null;
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
            JLKEditTemplate tag = this.listView1.SelectedItems[0].Tag as JLKEditTemplate;
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
                    (this.m_Map as IActiveViewEvents_Event).remove_ItemDeleted(new IActiveViewEvents_ItemDeletedEventHandler(this.EditorTemplateManageCtrl_ItemDeleted));
                }
                this.m_Map = value;
                (this.m_Map as IActiveViewEvents_Event).add_ItemDeleted(new IActiveViewEvents_ItemDeletedEventHandler(this.EditorTemplateManageCtrl_ItemDeleted));
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

