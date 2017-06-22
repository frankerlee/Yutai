using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class frmTemplatesGroup : Form
    {
        private bool OnlyShowHasTemplate = false;
        private bool OnlyShowVisibleLayer = false;

        public frmTemplatesGroup()
        {
            this.InitializeComponent();
        }

        private void DeletetoolStripButton_Click(object sender, EventArgs e)
        {
            List<ListViewItem> list = new List<ListViewItem>();
            List<YTEditTemplate> list2 = new List<YTEditTemplate>();
            foreach (ListViewItem item in this.listView2.SelectedItems)
            {
                list.Add(item);
                list2.Add(item.Tag as YTEditTemplate);
            }
            foreach (ListViewItem item2 in list)
            {
                this.listView2.Items.Remove(item2);
            }
        }

 private void frmTemplatesGroup_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void InitControl()
        {
            this.listView1.Items.Clear();
            this.listView2.Items.Clear();
            UID uid = new UIDClass {
                Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}"
            };
            IEnumLayer layer = this.Map.get_Layers(uid, true);
            layer.Reset();
            for (ILayer layer2 = layer.Next(); layer2 != null; layer2 = layer.Next())
            {
                if (!(layer2 is IFeatureLayer) || !Yutai.ArcGIS.Common.Editor.Editor.LayerCanEdit(layer2 as IFeatureLayer))
                {
                    continue;
                }
                bool visible = true;
                if (this.OnlyShowVisibleLayer)
                {
                    visible = layer2.Visible;
                }
                if (visible)
                {
                    if (this.OnlyShowHasTemplate)
                    {
                        if (this.Templates.ContainsKey(layer2 as IFeatureLayer))
                        {
                            visible = true;
                        }
                        else
                        {
                            visible = false;
                        }
                    }
                    if (visible)
                    {
                        ListViewItem item = new ListViewItem(layer2.Name);
                        switch ((layer2 as IFeatureLayer2).FeatureClass.FeatureType)
                        {
                            case esriFeatureType.esriFTAnnotation:
                                item.ImageKey = "AnnoLayer";
                                break;

                            case esriFeatureType.esriFTSimple:
                            {
                                esriGeometryType shapeType = (layer2 as IFeatureLayer2).ShapeType;
                                if ((shapeType == esriGeometryType.esriGeometryMultipoint) || (shapeType == esriGeometryType.esriGeometryPoint))
                                {
                                    item.ImageKey = "PointLayer";
                                    break;
                                }
                                if (shapeType == esriGeometryType.esriGeometryPolyline)
                                {
                                    item.ImageKey = "LineLayer";
                                }
                                else if (shapeType == esriGeometryType.esriGeometryPolygon)
                                {
                                    item.ImageKey = "FillLayer";
                                }
                                break;
                            }
                        }
                        item.Tag = layer2;
                        this.listView1.Items.Add(item);
                    }
                }
            }
            if (this.listView1.Items.Count > 0)
            {
                this.listView1.Items[0].Selected = true;
            }
        }

 private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listView2.Items.Clear();
            if (this.listView1.SelectedItems.Count != 0)
            {
                IFeatureLayer tag = this.listView1.SelectedItems[0].Tag as IFeatureLayer;
                List<YTEditTemplate> list = this.Templates[tag];
                this.删除图层中全部内容ToolStripMenuItem.Enabled = list.Count > 0;
                this.删除ToolStripMenuItem.Enabled = list.Count > 0;
                foreach (YTEditTemplate template in list)
                {
                    ListViewItem item = new ListViewItem {
                        Text = template.Name,
                        Tag = template
                    };
                    if (!this.imageList2.Images.ContainsKey(template.ImageKey) && (template.Bitmap != null))
                    {
                        this.imageList2.Images.Add(template.ImageKey, template.Bitmap);
                    }
                    item.ImageKey = template.ImageKey;
                    this.listView2.Items.Add(item);
                }
                if (this.listView2.Items.Count > 0)
                {
                    this.listView2.Items[0].Selected = true;
                }
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.删除ToolStripMenuItem.Enabled = this.listView2.SelectedIndices.Count > 0;
            this.propertytoolStripButton.Enabled = this.listView2.SelectedIndices.Count > 0;
        }

        private void propertytoolStripButton_Click(object sender, EventArgs e)
        {
            frmEditTemplateProperty property = new frmEditTemplateProperty();
            YTEditTemplate tag = this.listView2.SelectedItems[0].Tag as YTEditTemplate;
            property.EditTemplate = tag;
            if ((property.ShowDialog() == DialogResult.OK) && (this.listView2.SelectedItems[0].Text != tag.Name))
            {
                this.listView2.SelectedItems[0].Text = tag.Name;
                EditTemplateManager.TemplatePropertyChange(tag);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmNewTemplate template = new frmNewTemplate {
                Map = this.Map
            };
            if (template.ShowDialog() == DialogResult.OK)
            {
                foreach (KeyValuePair<IFeatureLayer, List<Yutai.ArcGIS.Common.Editor.YTEditTemplate>> pair in template.TemplateList)
                {
                    List<YTEditTemplate> list = null;
                    if (this.Templates.ContainsKey(pair.Key))
                    {
                        list = this.Templates[pair.Key];
                    }
                    else
                    {
                        list = new List<YTEditTemplate>();
                        this.Templates.Add(pair.Key, list);
                    }
                    list.AddRange(pair.Value);
                }
                this.listView1_SelectedIndexChanged(this, new EventArgs());
            }
        }

        private void 删除地图中全部内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除地图中所有模板?", "组织模板", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<YTEditTemplate> template = new List<YTEditTemplate>();
                foreach (KeyValuePair<IFeatureLayer, List<YTEditTemplate>> pair in this.Templates)
                {
                    template.AddRange(pair.Value);
                }
                if (template.Count > 0)
                {
                    EditTemplateManager.DeleteMoreEditTemplate(template);
                    this.listView2.Items.Clear();
                }
            }
        }

        private void 删除图层中全部内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<YTEditTemplate> template = new List<YTEditTemplate>();
            foreach (ListViewItem item in this.listView2.Items)
            {
                template.Add(item.Tag as YTEditTemplate);
            }
            if (template.Count > 0)
            {
                EditTemplateManager.DeleteMoreEditTemplate(template);
                this.listView2.Items.Clear();
            }
            this.删除图层中全部内容ToolStripMenuItem.Enabled = false;
            this.删除ToolStripMenuItem.Enabled = false;
        }

        private void 显示可见图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnlyShowVisibleLayer = !this.OnlyShowVisibleLayer;
            this.显示可见图层ToolStripMenuItem.Checked = this.OnlyShowVisibleLayer;
            this.InitControl();
        }

        private void 显示有模板的图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnlyShowHasTemplate = !this.OnlyShowHasTemplate;
            this.显示有模板的图层ToolStripMenuItem.Checked = this.OnlyShowHasTemplate;
            this.InitControl();
        }

        public IMap Map { get; set; }

        public Dictionary<IFeatureLayer, List<YTEditTemplate>> Templates { get; set; }
    }
}

