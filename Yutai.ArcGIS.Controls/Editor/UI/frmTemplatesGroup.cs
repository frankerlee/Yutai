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
    public class frmTemplatesGroup : Form
    {
        private Button button1;
        private ColumnHeader columnHeader1;
        private IContainer components = null;
        private ToolStripSplitButton DeletetoolStripButton;
        private ImageList imageList1;
        private ImageList imageList2;
        private ListView listView1;
        private ListView listView2;
        private bool OnlyShowHasTemplate = false;
        private bool OnlyShowVisibleLayer = false;
        private ToolStripButton propertytoolStripButton;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripMenuItem 删除ToolStripMenuItem;
        private ToolStripMenuItem 删除地图中全部内容ToolStripMenuItem;
        private ToolStripMenuItem 删除图层中全部内容ToolStripMenuItem;
        private ToolStripMenuItem 显示可见图层ToolStripMenuItem;
        private ToolStripMenuItem 显示有模板的图层ToolStripMenuItem;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplatesGroup));
            this.toolStrip1 = new ToolStrip();
            this.toolStripButton1 = new ToolStripDropDownButton();
            this.显示可见图层ToolStripMenuItem = new ToolStripMenuItem();
            this.显示有模板的图层ToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripButton2 = new ToolStripButton();
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.imageList1 = new ImageList(this.components);
            this.listView2 = new ListView();
            this.imageList2 = new ImageList(this.components);
            this.button1 = new Button();
            this.DeletetoolStripButton = new ToolStripSplitButton();
            this.删除ToolStripMenuItem = new ToolStripMenuItem();
            this.删除图层中全部内容ToolStripMenuItem = new ToolStripMenuItem();
            this.删除地图中全部内容ToolStripMenuItem = new ToolStripMenuItem();
            this.propertytoolStripButton = new ToolStripButton();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripButton1, this.toolStripButton2, this.DeletetoolStripButton, this.propertytoolStripButton });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x196, 0x19);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.DropDownItems.AddRange(new ToolStripItem[] { this.显示可见图层ToolStripMenuItem, this.显示有模板的图层ToolStripMenuItem });
            this.toolStripButton1.Image = (Image) resources.GetObject("toolStripButton1.Image");
            this.toolStripButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new Size(0x1d, 0x16);
            this.toolStripButton1.Text = "toolStripButton1";
            this.显示可见图层ToolStripMenuItem.Name = "显示可见图层ToolStripMenuItem";
            this.显示可见图层ToolStripMenuItem.Size = new Size(0xac, 0x16);
            this.显示可见图层ToolStripMenuItem.Text = "显示可见图层";
            this.显示可见图层ToolStripMenuItem.Click += new EventHandler(this.显示可见图层ToolStripMenuItem_Click);
            this.显示有模板的图层ToolStripMenuItem.Name = "显示有模板的图层ToolStripMenuItem";
            this.显示有模板的图层ToolStripMenuItem.Size = new Size(0xac, 0x16);
            this.显示有模板的图层ToolStripMenuItem.Text = "显示有模板的图层";
            this.显示有模板的图层ToolStripMenuItem.Click += new EventHandler(this.显示有模板的图层ToolStripMenuItem_Click);
            this.toolStripButton2.Image = (Image) resources.GetObject("toolStripButton2.Image");
            this.toolStripButton2.ImageTransparentColor = Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new Size(0x4c, 0x16);
            this.toolStripButton2.Text = "新建模板";
            this.toolStripButton2.Click += new EventHandler(this.toolStripButton2_Click);
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1 });
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 0x1c);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x8b, 0xeb);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader1.Text = "图层列表";
            this.columnHeader1.Width = 0x80;
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "AnnoLayer");
            this.imageList1.Images.SetKeyName(1, "LineLayer");
            this.imageList1.Images.SetKeyName(2, "PointLayer");
            this.imageList1.Images.SetKeyName(3, "FillLayer");
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(0x9d, 0x1c);
            this.listView2.Name = "listView2";
            this.listView2.Size = new Size(0xed, 0xeb);
            this.listView2.SmallImageList = this.imageList2;
            this.listView2.TabIndex = 2;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = View.SmallIcon;
            this.listView2.SelectedIndexChanged += new EventHandler(this.listView2_SelectedIndexChanged);
            this.imageList2.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new Size(0x10, 0x10);
            this.imageList2.TransparentColor = Color.Transparent;
            this.button1.DialogResult = DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(0x13f, 0x10d);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 3;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.DeletetoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.DeletetoolStripButton.DropDownItems.AddRange(new ToolStripItem[] { this.删除ToolStripMenuItem, this.删除图层中全部内容ToolStripMenuItem, this.删除地图中全部内容ToolStripMenuItem });
            this.DeletetoolStripButton.Image = (Image) resources.GetObject("DeletetoolStripButton.Image");
            this.DeletetoolStripButton.ImageTransparentColor = Color.Magenta;
            this.DeletetoolStripButton.Name = "DeletetoolStripButton";
            this.DeletetoolStripButton.Size = new Size(0x30, 0x16);
            this.DeletetoolStripButton.Text = "删除";
            this.DeletetoolStripButton.ButtonClick += new EventHandler(this.DeletetoolStripButton_Click);
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new Size(0xb8, 0x16);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new EventHandler(this.DeletetoolStripButton_Click);
            this.删除图层中全部内容ToolStripMenuItem.Name = "删除图层中全部内容ToolStripMenuItem";
            this.删除图层中全部内容ToolStripMenuItem.Size = new Size(0xb8, 0x16);
            this.删除图层中全部内容ToolStripMenuItem.Text = "删除图层中全部内容";
            this.删除图层中全部内容ToolStripMenuItem.Click += new EventHandler(this.删除图层中全部内容ToolStripMenuItem_Click);
            this.删除地图中全部内容ToolStripMenuItem.Name = "删除地图中全部内容ToolStripMenuItem";
            this.删除地图中全部内容ToolStripMenuItem.Size = new Size(0xb8, 0x16);
            this.删除地图中全部内容ToolStripMenuItem.Text = "删除地图中全部内容";
            this.删除地图中全部内容ToolStripMenuItem.Click += new EventHandler(this.删除地图中全部内容ToolStripMenuItem_Click);
            this.propertytoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.propertytoolStripButton.Image = (Image) resources.GetObject("propertytoolStripButton.Image");
            this.propertytoolStripButton.ImageTransparentColor = Color.Magenta;
            this.propertytoolStripButton.Name = "propertytoolStripButton";
            this.propertytoolStripButton.Size = new Size(0x24, 0x16);
            this.propertytoolStripButton.Text = "属性";
            this.propertytoolStripButton.Click += new EventHandler(this.propertytoolStripButton_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x196, 0x12a);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.listView2);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.toolStrip1);
            base.Name = "frmTemplatesGroup";
            this.Text = "要素模板";
            base.Load += new EventHandler(this.frmTemplatesGroup_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
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

