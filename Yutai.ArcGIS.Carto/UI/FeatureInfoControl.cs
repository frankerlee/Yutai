using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.CodeDomainEx;
using Yutai.ArcGIS.Common.Display;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    public class FeatureInfoControl : UserControl, IDockContent
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private Button button1;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ComboBoxEdit combLayer;
        private ContextMenuStrip contextMenuStrip1;
        private ESRI.ArcGIS.Carto.IActiveViewEvents_Event iactiveViewEvents_Event_0;
        private IArray iarray_0;
        private IBasicMap ibasicMap_0;
        private IContainer icontainer_0;
        private IdentifyTypeEnum identifyTypeEnum_0 = IdentifyTypeEnum.enumITAllLayer;
        private IFeature ifeature_0 = null;
        private IFeature ifeature_1 = null;
        private ILayer ilayer_0 = null;
        private IList<string> ilist_0 = null;
        private ListView Infolist;
        private IPoint ipoint_0;
        private Label label1;
        private TreeView objTree;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panelAttach;
        private ToolStripMenuItem PanToFeature;
        private TextBox textBox1;
        private TextEdit txtPos;
        private ToolStripMenuItem ZoomToFeature;

        public event IdentifyLayerChangedHandler IdentifyLayerChanged;

        public event IdentifyTypeChangedHandler IdentifyTypeChanged;

        public FeatureInfoControl()
        {
            this.InitializeComponent();
            this.Text = "查看属性";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new frmAttachment { Attachments = this.button1.Tag as List<IAttachment> }.ShowDialog();
        }

        private void combLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_2)
            {
                if (this.combLayer.SelectedIndex > 3)
                {
                    this.identifyTypeEnum_0 = IdentifyTypeEnum.enumITCurrentLayer;
                    this.ilayer_0 = (this.combLayer.SelectedItem as LayerObject).Layer;
                    if (this.IdentifyLayerChanged != null)
                    {
                        this.IdentifyLayerChanged(this, this.ilayer_0);
                    }
                }
                else
                {
                    this.ilayer_0 = null;
                    this.identifyTypeEnum_0 = (IdentifyTypeEnum) this.combLayer.SelectedIndex;
                    if (this.IdentifyTypeChanged != null)
                    {
                        this.IdentifyTypeChanged(this, this.identifyTypeEnum_0);
                    }
                }
                this.method_0();
            }
        }

        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_3);
        }

        private void FeatureInfoControl_Load(object sender, EventArgs e)
        {
            this.bool_0 = true;
            this.method_11();
            this.SetControl();
            this.bool_2 = true;
        }

        private void FeatureInfoControl_SizeChanged(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            this.panel1 = new Panel();
            this.combLayer = new ComboBoxEdit();
            this.panel4 = new Panel();
            this.label1 = new Label();
            this.txtPos = new TextEdit();
            this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
            this.ZoomToFeature = new ToolStripMenuItem();
            this.PanToFeature = new ToolStripMenuItem();
            this.panel3 = new Panel();
            this.objTree = new TreeView();
            this.panel5 = new Panel();
            this.panelAttach = new Panel();
            this.textBox1 = new TextBox();
            this.button1 = new Button();
            this.panel2 = new Panel();
            this.Infolist = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.panel1.SuspendLayout();
            this.combLayer.Properties.BeginInit();
            this.panel4.SuspendLayout();
            this.txtPos.Properties.BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelAttach.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.combLayer);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(570, 0x1a);
            this.panel1.TabIndex = 0;
            this.panel1.SizeChanged += new EventHandler(this.panel1_SizeChanged);
            this.combLayer.Dock = DockStyle.Fill;
            this.combLayer.EditValue = "";
            this.combLayer.Location = new System.Drawing.Point(40, 0);
            this.combLayer.Name = "combLayer";
            this.combLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.combLayer.Size = new Size(530, 0x15);
            this.combLayer.TabIndex = 3;
            this.combLayer.SelectedIndexChanged += new EventHandler(this.combLayer_SelectedIndexChanged);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(40, 0x1a);
            this.panel4.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层";
            this.txtPos.Dock = DockStyle.Top;
            this.txtPos.EditValue = "";
            this.txtPos.Location = new System.Drawing.Point(0, 0);
            this.txtPos.Name = "txtPos";
            this.txtPos.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.txtPos.Properties.Appearance.Options.UseBackColor = true;
            this.txtPos.Properties.ReadOnly = true;
            this.txtPos.Size = new Size(570, 0x15);
            this.txtPos.TabIndex = 1;
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.ZoomToFeature, this.PanToFeature });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x89, 0x30);
            this.ZoomToFeature.Name = "ZoomToFeature";
            this.ZoomToFeature.Size = new Size(0x88, 0x16);
            this.ZoomToFeature.Text = "缩放到要素";
            this.ZoomToFeature.Click += new EventHandler(this.ZoomToFeature_Click);
            this.PanToFeature.Name = "PanToFeature";
            this.PanToFeature.Size = new Size(0x88, 0x16);
            this.PanToFeature.Text = "平移到要素";
            this.PanToFeature.Click += new EventHandler(this.PanToFeature_Click);
            this.panel3.Controls.Add(this.objTree);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(570, 0x5f);
            this.panel3.TabIndex = 2;
            this.objTree.Dock = DockStyle.Fill;
            this.objTree.HideSelection = false;
            this.objTree.Location = new System.Drawing.Point(0, 0x1a);
            this.objTree.Name = "objTree";
            this.objTree.Size = new Size(570, 0x45);
            this.objTree.TabIndex = 2;
            this.objTree.AfterSelect += new TreeViewEventHandler(this.objTree_AfterSelect);
            this.panel5.Controls.Add(this.panelAttach);
            this.panel5.Controls.Add(this.txtPos);
            this.panel5.Dock = DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new Size(570, 0x2a);
            this.panel5.TabIndex = 1;
            this.panelAttach.AutoSize = true;
            this.panelAttach.Controls.Add(this.textBox1);
            this.panelAttach.Controls.Add(this.button1);
            this.panelAttach.Dock = DockStyle.Fill;
            this.panelAttach.Location = new System.Drawing.Point(0, 0x15);
            this.panelAttach.Name = "panelAttach";
            this.panelAttach.Size = new Size(570, 0x15);
            this.panelAttach.TabIndex = 2;
            this.textBox1.Dock = DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0x40, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x1fa, 0x15);
            this.textBox1.TabIndex = 1;
            this.button1.Dock = DockStyle.Left;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x40, 0x15);
            this.button1.TabIndex = 0;
            this.button1.Text = "附件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.panel2.Controls.Add(this.Infolist);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0x5f);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(570, 0xf4);
            this.panel2.TabIndex = 5;
            this.Infolist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.Infolist.Dock = DockStyle.Fill;
            this.Infolist.Location = new System.Drawing.Point(0, 0x2a);
            this.Infolist.Name = "Infolist";
            this.Infolist.Size = new Size(570, 0xca);
            this.Infolist.TabIndex = 5;
            this.Infolist.UseCompatibleStateImageBehavior = false;
            this.Infolist.View = View.Details;
            this.columnHeader_0.Text = "字段";
            this.columnHeader_0.Width = 0x57;
            this.columnHeader_1.Text = "字段值";
            this.columnHeader_1.Width = 0xa4;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel3);
            base.Name = "FeatureInfoControl";
            base.Size = new Size(570, 0x153);
            base.Load += new EventHandler(this.FeatureInfoControl_Load);
            base.SizeChanged += new EventHandler(this.FeatureInfoControl_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.combLayer.Properties.EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.txtPos.Properties.EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panelAttach.ResumeLayout(false);
            this.panelAttach.PerformLayout();
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.objTree.Nodes.Clear();
            this.Infolist.Items.Clear();
            object layer = null;
            TreeNode node = null;
            TreeNode node2 = null;
            IIdentifyObj obj3 = null;
            object obj4 = null;
            if (this.iarray_0 != null)
            {
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    TreeNode node3;
                    obj4 = this.iarray_0.get_Element(i);
                    if (obj4 is IIdentifyObj)
                    {
                        obj3 = obj4 as IIdentifyObj;
                        if ((this.identifyTypeEnum_0 != IdentifyTypeEnum.enumITCurrentLayer) || (obj3.Layer == this.ilayer_0))
                        {
                            if (obj3 is IFeatureIdentifyObj)
                            {
                                IRow row = (obj3 as IRowIdentifyObject).Row;
                                if (layer != obj3.Layer)
                                {
                                    layer = obj3.Layer;
                                    node = new TreeNode(obj3.Layer.Name);
                                    this.objTree.Nodes.Add(node);
                                    node.Tag = obj3.Layer;
                                    node.ExpandAll();
                                }
                                if (row.HasOID)
                                {
                                    node3 = new TreeNode(row.OID.ToString());
                                }
                                else
                                {
                                    node3 = new TreeNode(row.get_Value(0).ToString());
                                }
                                node3.Tag = obj3;
                                if (node2 == null)
                                {
                                    node2 = node3;
                                }
                                this.method_3(row as IFeature, node3);
                                if (obj3.Layer is IRelationshipClassCollection)
                                {
                                    this.method_1((obj3.Layer as IRelationshipClassCollection).RelationshipClasses, row as IObject, node3, false);
                                }
                                node.Nodes.Add(node3);
                            }
                            else if ((obj3 is IRasterIdentifyObj) || (obj3 is ITinIdentifyObj))
                            {
                                layer = null;
                                node = new TreeNode(obj3.Layer.Name);
                                this.objTree.Nodes.Add(node);
                                node.Tag = obj3.Layer;
                                node.ExpandAll();
                                node3 = new TreeNode(obj3.Name) {
                                    Tag = obj3
                                };
                                if (node2 == null)
                                {
                                    node2 = node3;
                                }
                                node.Nodes.Add(node3);
                            }
                            else
                            {
                                layer = null;
                                node = new TreeNode(obj3.Layer.Name);
                                this.objTree.Nodes.Add(node);
                                node.Tag = obj3.Layer;
                                node.ExpandAll();
                                node3 = new TreeNode(obj3.Name) {
                                    Tag = obj3
                                };
                                if (node2 == null)
                                {
                                    node2 = node3;
                                }
                                node.Nodes.Add(node3);
                            }
                        }
                    }
                    else
                    {
                        IFeature feature;
                        if (obj4 is IFeatureFindData2)
                        {
                            feature = (obj4 as IFeatureFindData2).Feature;
                            node = new TreeNode(feature.Class.AliasName);
                            this.objTree.Nodes.Add(node);
                            node.Tag = feature.Class;
                            node.ExpandAll();
                            node3 = new TreeNode(feature.OID.ToString()) {
                                Tag = feature
                            };
                            if (node2 == null)
                            {
                                node2 = node3;
                            }
                            node.Nodes.Add(node3);
                            this.method_6((obj4 as IFeatureFindData2).Layer as IFeatureLayer, feature);
                        }
                        else if (obj4 is IHit3D)
                        {
                            IHit3D hitd = obj4 as IHit3D;
                            if (hitd.Object is IFeature)
                            {
                                feature = hitd.Object as IFeature;
                                node = new TreeNode(feature.Class.AliasName);
                                this.objTree.Nodes.Add(node);
                                node.Tag = feature.Class;
                                node.ExpandAll();
                                node3 = new TreeNode(feature.OID.ToString()) {
                                    Tag = feature
                                };
                                if (node2 == null)
                                {
                                    node2 = node3;
                                }
                                node.Nodes.Add(node3);
                                this.method_5(feature);
                            }
                            else if (hitd.Owner is ITinIdentifyObj)
                            {
                                layer = null;
                            }
                        }
                    }
                }
                this.objTree.SelectedNode = node2;
            }
        }

        private void method_1(IEnumRelationshipClass ienumRelationshipClass_0, IObject iobject_0, TreeNode treeNode_0, bool bool_3)
        {
            ienumRelationshipClass_0.Reset();
            for (IRelationshipClass class2 = ienumRelationshipClass_0.Next(); class2 != null; class2 = ienumRelationshipClass_0.Next())
            {
                try
                {
                    IObjectClass destinationClass;
                    if (bool_3)
                    {
                        destinationClass = class2.DestinationClass;
                    }
                    else
                    {
                        destinationClass = class2.OriginClass;
                    }
                    TreeNode node = new TreeNode(destinationClass.AliasName) {
                        Tag = destinationClass
                    };
                    ISet objectsRelatedToObject = class2.GetObjectsRelatedToObject(iobject_0);
                    objectsRelatedToObject.Reset();
                    for (IRowBuffer buffer = objectsRelatedToObject.Next() as IRowBuffer; buffer != null; buffer = objectsRelatedToObject.Next() as IRowBuffer)
                    {
                        TreeNode node2 = new TreeNode(buffer.get_Value(0).ToString()) {
                            Tag = buffer
                        };
                        node.Nodes.Add(node2);
                    }
                    if (node.Nodes.Count > 0)
                    {
                        treeNode_0.Nodes.Add(node);
                    }
                }
                catch (Exception exception)
                {
                    exception.ToString();
                }
            }
        }

        private bool method_10(ILayer ilayer_1)
        {
            return (IdentifyHelper.IdentifyLayers.IndexOf(ilayer_1.Name) == -1);
        }

        private void method_11()
        {
            if (this.bool_1)
            {
                this.combLayer.Properties.Items.Clear();
                int num = -1;
                this.combLayer.Properties.Items.Add("最上层");
                this.combLayer.Properties.Items.Add("可见图层");
                this.combLayer.Properties.Items.Add("可选择图层");
                this.combLayer.Properties.Items.Add("所有图层");
                this.combLayer.Enabled = true;
                int num2 = 0;
                for (int i = 0; i < this.ibasicMap_0.LayerCount; i++)
                {
                    ILayer layer = this.ibasicMap_0.get_Layer(i);
                    if (layer is IGroupLayer)
                    {
                        this.method_9(layer as ICompositeLayer, ref num2, ref num);
                    }
                    else if (layer is IFeatureLayer)
                    {
                        if (this.method_10(layer))
                        {
                            this.combLayer.Properties.Items.Add(new LayerObject(layer));
                            num2++;
                            if (layer == this.ilayer_0)
                            {
                                num = this.combLayer.Properties.Items.Count - 1;
                            }
                        }
                    }
                    else if (layer is IRasterLayer)
                    {
                        if (this.method_10(layer))
                        {
                            this.combLayer.Properties.Items.Add(new LayerObject(layer));
                            num2++;
                            if (layer == this.ilayer_0)
                            {
                                num = this.combLayer.Properties.Items.Count - 1;
                            }
                        }
                    }
                    else if (layer is ITinLayer)
                    {
                        if (this.method_10(layer))
                        {
                            this.combLayer.Properties.Items.Add(new LayerObject(layer));
                            num2++;
                            if (layer == this.ilayer_0)
                            {
                                num = this.combLayer.Properties.Items.Count - 1;
                            }
                        }
                    }
                    else if ((layer is IMapServerLayer) && this.method_10(layer))
                    {
                        this.combLayer.Properties.Items.Add(new LayerObject(layer));
                        num2++;
                        if (layer == this.ilayer_0)
                        {
                            num = this.combLayer.Properties.Items.Count - 1;
                        }
                    }
                }
                if (num == -1)
                {
                    this.combLayer.SelectedIndex = (int) this.identifyTypeEnum_0;
                }
            }
            else
            {
                this.combLayer.Enabled = false;
            }
        }

        private void method_12(object sender, MouseEventArgs e)
        {
            this.method_8(e.X, e.Y);
        }

        private void method_13()
        {
            if (this.ilayer_0 != null)
            {
                foreach (TreeNode node in this.objTree.Nodes)
                {
                    if ((node.Tag is ILayer) && (this.ilayer_0 == node.Tag))
                    {
                    }
                }
            }
        }

        private void method_14(object object_0)
        {
            if (base.Visible && this.bool_1)
            {
                this.bool_2 = false;
                this.combLayer.Properties.Items.Clear();
                this.combLayer.Properties.Items.Add("最上层");
                this.combLayer.Properties.Items.Add("可见图层");
                this.combLayer.Properties.Items.Add("可选择图层");
                this.combLayer.Properties.Items.Add("所有图层");
                int num = -1;
                for (int i = 0; i < this.ibasicMap_0.LayerCount; i++)
                {
                    ILayer layer = this.ibasicMap_0.get_Layer(i);
                    if (layer is IFeatureLayer)
                    {
                        if (layer == this.ilayer_0)
                        {
                            num = i;
                        }
                        this.combLayer.Properties.Items.Add(new LayerObject(layer));
                    }
                }
                if (num != -1)
                {
                    this.combLayer.SelectedIndex = num + 4;
                }
                else
                {
                    this.combLayer.SelectedIndex = (int) this.identifyTypeEnum_0;
                }
                this.bool_2 = true;
            }
        }

        private void method_15(object object_0)
        {
            if (base.Visible)
            {
                this.bool_2 = false;
                this.combLayer.Properties.Items.Clear();
                this.combLayer.Properties.Items.Add("最上层");
                this.combLayer.Properties.Items.Add("可见图层");
                this.combLayer.Properties.Items.Add("可选择图层");
                this.combLayer.Properties.Items.Add("所有图层");
                int num = -1;
                for (int i = 0; i < this.ibasicMap_0.LayerCount; i++)
                {
                    ILayer layer = this.ibasicMap_0.get_Layer(i);
                    if (layer is IFeatureLayer)
                    {
                        if (layer == this.ilayer_0)
                        {
                            num = i;
                        }
                        this.combLayer.Properties.Items.Add(new LayerObject(layer));
                    }
                }
                if (num != -1)
                {
                    this.combLayer.SelectedIndex = num + 4;
                }
                else
                {
                    if (this.ilayer_0 != null)
                    {
                        this.ilayer_0 = null;
                        this.identifyTypeEnum_0 = IdentifyTypeEnum.enumITAllLayer;
                        if (this.IdentifyTypeChanged != null)
                        {
                            this.IdentifyTypeChanged(this, this.identifyTypeEnum_0);
                        }
                    }
                    this.combLayer.SelectedIndex = (int) this.identifyTypeEnum_0;
                }
                this.bool_2 = true;
            }
        }

        private void method_16(object object_0, int int_0)
        {
        }

        private void method_17()
        {
            this.bool_2 = false;
            this.combLayer.Properties.Items.Clear();
            this.combLayer.Properties.Items.Add("最上层");
            this.combLayer.Properties.Items.Add("可见图层");
            this.combLayer.Properties.Items.Add("可选择图层");
            this.combLayer.Properties.Items.Add("所有图层");
            if (this.ilayer_0 != null)
            {
                this.ilayer_0 = null;
                this.identifyTypeEnum_0 = IdentifyTypeEnum.enumITAllLayer;
            }
            this.combLayer.SelectedIndex = (int) this.identifyTypeEnum_0;
            this.bool_2 = true;
        }

        private void method_18(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.ifeature_1 = null;
                TreeNode nodeAt = this.objTree.GetNodeAt(e.X, e.Y);
                if ((nodeAt != null) && (nodeAt.Tag != null))
                {
                    object tag = nodeAt.Tag;
                    if (tag is IIdentifyObj)
                    {
                        if (tag is IFeatureIdentifyObj)
                        {
                            this.ifeature_1 = (tag as IRowIdentifyObject).Row as IFeature;
                        }
                    }
                    else
                    {
                        IFeature feature = nodeAt.Tag as IFeature;
                        if (feature != null)
                        {
                            this.ifeature_1 = feature;
                        }
                    }
                    if (this.ifeature_1 != null)
                    {
                        this.contextMenuStrip1.Show(this.objTree, e.Location);
                    }
                }
            }
        }

        private void method_2(IRelationshipClass irelationshipClass_0, IObject iobject_0, TreeNode treeNode_0, bool bool_3)
        {
            try
            {
                IObjectClass destinationClass;
                if (bool_3)
                {
                    destinationClass = irelationshipClass_0.DestinationClass;
                }
                else
                {
                    destinationClass = irelationshipClass_0.OriginClass;
                }
                TreeNode node = new TreeNode(destinationClass.AliasName) {
                    Tag = destinationClass
                };
                IEnumRelationship relationshipsForObject = irelationshipClass_0.GetRelationshipsForObject(iobject_0);
                relationshipsForObject.Reset();
                for (IRelationship relationship2 = relationshipsForObject.Next(); relationship2 != null; relationship2 = relationshipsForObject.Next())
                {
                    if (relationship2.DestinationObject != null)
                    {
                        TreeNode node2 = new TreeNode(relationship2.DestinationObject.OID.ToString()) {
                            Tag = relationship2.DestinationObject
                        };
                        node.Nodes.Add(node2);
                    }
                }
                if (node.Nodes.Count > 0)
                {
                    treeNode_0.Nodes.Add(node);
                }
            }
            catch
            {
            }
        }

        private void method_3(IFeature ifeature_2, TreeNode treeNode_0)
        {
            IObjectClass destinationClass;
            TreeNode node;
            IEnumRelationship relationshipsForObject;
            IRelationship relationship2;
            TreeNode node2;
            IEnumRelationshipClass class2 = ifeature_2.Class.get_RelationshipClasses(esriRelRole.esriRelRoleOrigin);
            class2.Reset();
            IRelationshipClass class3 = class2.Next();
            while (true)
            {
                if (class3 == null)
                {
                    break;
                }
                try
                {
                    destinationClass = class3.DestinationClass;
                    node = new TreeNode(destinationClass.AliasName) {
                        Tag = destinationClass
                    };
                    relationshipsForObject = class3.GetRelationshipsForObject(ifeature_2);
                    relationshipsForObject.Reset();
                    relationship2 = relationshipsForObject.Next();
                    while (relationship2 != null)
                    {
                        if (relationship2.DestinationObject != null)
                        {
                            node2 = new TreeNode(relationship2.DestinationObject.OID.ToString()) {
                                Tag = relationship2.DestinationObject
                            };
                            node.Nodes.Add(node2);
                        }
                        relationship2 = relationshipsForObject.Next();
                    }
                    if (node.Nodes.Count > 0)
                    {
                        treeNode_0.Nodes.Add(node);
                    }
                }
                catch
                {
                }
                class3 = class2.Next();
            }
            class2 = ifeature_2.Class.get_RelationshipClasses(esriRelRole.esriRelRoleDestination);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                destinationClass = class3.OriginClass;
                node = new TreeNode(destinationClass.AliasName) {
                    Tag = destinationClass
                };
                relationshipsForObject = class3.GetRelationshipsForObject(ifeature_2);
                relationshipsForObject.Reset();
                for (relationship2 = relationshipsForObject.Next(); relationship2 != null; relationship2 = relationshipsForObject.Next())
                {
                    if (relationship2.OriginObject != null)
                    {
                        node2 = new TreeNode(relationship2.OriginObject.OID.ToString()) {
                            Tag = relationship2.OriginObject
                        };
                        node.Nodes.Add(node2);
                    }
                }
                if (node.Nodes.Count > 0)
                {
                    treeNode_0.Nodes.Add(node);
                }
            }
        }

        private List<IAttachment> method_4(IObject iobject_0)
        {
            List<IAttachment> list = new List<IAttachment>();
            IClass class2 = iobject_0.Class;
            if (class2 is ITableAttachments)
            {
                ITableAttachments attachments = (ITableAttachments) class2;
                if (!attachments.HasAttachments)
                {
                    return list;
                }
                IAttachmentManager attachmentManager = attachments.AttachmentManager;
                ILongArray oids = new LongArrayClass();
                oids.Add(iobject_0.OID);
                IEnumAttachment attachmentsByParentIDs = attachmentManager.GetAttachmentsByParentIDs(oids, false);
                attachmentsByParentIDs.Reset();
                IAttachment item = null;
                while ((item = attachmentsByParentIDs.Next()) != null)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        private void method_5(IObject iobject_0)
        {
            IObjectClass class1 = iobject_0.Class;
            List<IAttachment> list = this.method_4(iobject_0);
            if (list.Count > 0)
            {
                this.textBox1.Text = "共有附件" + list.Count;
                this.button1.Tag = list;
                this.panel5.AutoSize = false;
            }
            else
            {
                this.panel5.AutoSize = true;
            }
            this.Infolist.Items.Clear();
            string text = "几何数据";
            for (int i = 0; i < iobject_0.Fields.FieldCount; i++)
            {
                IField field = iobject_0.Fields.get_Field(i);
                this.Infolist.Items.Add(field.AliasName);
                if (field.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add(text);
                }
                else if (field.Type == esriFieldType.esriFieldTypeBlob)
                {
                    this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add("二进制数据");
                }
                else if (field.Type == esriFieldType.esriFieldTypeBlob)
                {
                    this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add("二进制数据");
                }
                else if (field.Type == esriFieldType.esriFieldTypeOID)
                {
                    this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add(iobject_0.get_Value(i).ToString());
                }
                else
                {
                    string str2 = this.method_7(iobject_0, field, i);
                    this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add(str2);
                }
            }
        }

        private void method_6(IFeatureLayer ifeatureLayer_0, IObject iobject_0)
        {
            string str;
            int num;
            IField field;
            IFieldInfo info;
            string str2;
            List<IAttachment> list = this.method_4(iobject_0);
            if (list.Count > 0)
            {
                this.textBox1.Text = "共有附件" + list.Count;
                this.button1.Tag = list;
                this.panel5.AutoSize = false;
            }
            else
            {
                this.panel5.AutoSize = true;
            }
            this.Infolist.Items.Clear();
            IRelationshipClassCollection classs = ifeatureLayer_0 as IRelationshipClassCollection;
            IEnumRelationshipClass relationshipClasses = classs.RelationshipClasses;
            relationshipClasses.Reset();
            relationshipClasses.Next();
            ITable displayTable = (ifeatureLayer_0 as IDisplayTable).DisplayTable;
            if (displayTable == null)
            {
                str = "几何数据";
                for (num = 0; num < (ifeatureLayer_0 as ILayerFields).FieldCount; num++)
                {
                    field = (ifeatureLayer_0 as ILayerFields).get_Field(num);
                    info = (ifeatureLayer_0 as ILayerFields).get_FieldInfo(num);
                    if (info.Visible)
                    {
                        this.Infolist.Items.Add(info.Alias);
                        if (field.Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add(str);
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeBlob)
                        {
                            this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add("二进制数据");
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeOID)
                        {
                            this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add(iobject_0.get_Value(num).ToString());
                        }
                        else
                        {
                            str2 = this.method_7(iobject_0, field, num);
                            this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add(str2);
                        }
                    }
                }
            }
            else
            {
                IRow row = displayTable.GetRow(iobject_0.OID);
                str = "几何数据";
                for (num = 0; num < (ifeatureLayer_0 as ILayerFields).FieldCount; num++)
                {
                    field = (ifeatureLayer_0 as ILayerFields).get_Field(num);
                    info = (ifeatureLayer_0 as ILayerFields).get_FieldInfo(num);
                    if (info.Visible)
                    {
                        this.Infolist.Items.Add(info.Alias);
                        if (field.Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add(str);
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeBlob)
                        {
                            this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add("二进制数据");
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeOID)
                        {
                            this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add(row.get_Value(num).ToString());
                        }
                        else
                        {
                            str2 = this.method_7(row, field, num);
                            this.Infolist.Items[this.Infolist.Items.Count - 1].SubItems.Add(str2);
                        }
                    }
                }
            }
        }

        private string method_7(IRow irow_0, IField ifield_0, int int_0)
        {
            int num;
            string str = irow_0.get_Value(int_0).ToString();
            ISubtypes table = irow_0.Table as ISubtypes;
            ICodedValueDomain domain = null;
            IList list = new ArrayList();
            IDomain domain2 = null;
            if ((table != null) && table.HasSubtype)
            {
                if (table.SubtypeFieldName == ifield_0.Name)
                {
                    try
                    {
                        str = table.get_SubtypeName((irow_0 as IRowSubtypes).SubtypeCode);
                    }
                    catch
                    {
                    }
                }
                else
                {
                    domain2 = table.get_Domain((irow_0 as IRowSubtypes).SubtypeCode, ifield_0.Name);
                    if (domain2 is ICodedValueDomain)
                    {
                        domain = domain2 as ICodedValueDomain;
                        if (ifield_0.IsNullable)
                        {
                            list.Add("<空>");
                        }
                        num = 0;
                        while (num < domain.CodeCount)
                        {
                            if (str == domain.get_Value(num).ToString())
                            {
                                str = domain.get_Name(num);
                                break;
                            }
                            num++;
                        }
                    }
                }
            }
            domain2 = ifield_0.Domain;
            if (domain2 != null)
            {
                if (domain2 is ICodedValueDomain)
                {
                    domain = domain2 as ICodedValueDomain;
                    num = 0;
                    while (num < domain.CodeCount)
                    {
                        if (str == domain.get_Value(num).ToString())
                        {
                            return domain.get_Name(num);
                        }
                        num++;
                    }
                }
                return str;
            }
            string name = (irow_0.Table as IDataset).Name;
            CodeDomainEx codeDomainEx = CodeDomainManage.GetCodeDomainEx(ifield_0.Name, name);
            if (codeDomainEx == null)
            {
                return str;
            }
            if ((codeDomainEx.ParentIDFieldName == null) || (codeDomainEx.ParentIDFieldName.Length == 0))
            {
                NameValueCollection codeDomain = codeDomainEx.GetCodeDomain();
                for (num = 0; num < codeDomain.Count; num++)
                {
                    string str3 = codeDomain.Keys[num];
                    if (str == codeDomain[str3])
                    {
                        str = str3;
                    }
                }
                return str;
            }
            return codeDomainEx.FindName(str);
        }

        private void method_8(int int_0, int int_1)
        {
            TreeNode nodeAt = this.objTree.GetNodeAt(int_0, int_1);
            if ((nodeAt != null) && (nodeAt.Tag != null))
            {
                object tag = nodeAt.Tag;
                IActiveView view = (IActiveView) this.ibasicMap_0;
                if (tag is IIdentifyObj)
                {
                    (tag as IIdentifyObj).Flash(view.ScreenDisplay);
                }
                else
                {
                    IFeature oOlOOlOO = nodeAt.Tag as IFeature;
                    if (oOlOOlOO != null)
                    {
                        Flash.FlashFeature(view.ScreenDisplay, oOlOOlOO);
                    }
                }
            }
        }

        private void method_9(ICompositeLayer icompositeLayer_0, ref int int_0, ref int int_1)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_9(layer as ICompositeLayer, ref int_0, ref int_1);
                }
                else if ((((layer is IFeatureLayer) || (layer is IRasterLayer)) || (layer is IMapServerLayer)) && this.method_10(layer))
                {
                    this.combLayer.Properties.Items.Add(new LayerObject(layer));
                    int_0++;
                    if (layer == this.ilayer_0)
                    {
                        int_1 = this.combLayer.Properties.Items.Count - 1;
                    }
                }
            }
        }

        private void objTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if ((this.objTree.SelectedNode != null) && (this.objTree.SelectedNode.Tag != null))
                {
                    IActiveView view;
                    object tag = null;
                    tag = this.objTree.SelectedNode.Tag;
                    if (tag is IIdentifyObj)
                    {
                        IIdentifyObj obj3 = tag as IIdentifyObj;
                        this.Infolist.Visible = true;
                        if (obj3 is IFeatureIdentifyObj)
                        {
                            this.ifeature_0 = (obj3 as IRowIdentifyObject).Row as IFeature;
                            view = (IActiveView) this.ibasicMap_0;
                            this.method_6(obj3.Layer as IFeatureLayer, this.ifeature_0);
                            obj3.Flash(view.ScreenDisplay);
                        }
                        else
                        {
                            int num;
                            string[] strArray;
                            string str;
                            string str2;
                            ListViewItem item;
                            if (obj3 is IRasterIdentifyObj2)
                            {
                                this.panel5.AutoSize = true;
                                this.Infolist.Items.Clear();
                                num = 0;
                                strArray = new string[2];
                                while (true)
                                {
                                    try
                                    {
                                        (obj3 as IRasterIdentifyObj2).GetPropAndValues(num, out str, out str2);
                                        if (str == null)
                                        {
                                            break;
                                        }
                                        strArray[0] = str;
                                        strArray[1] = str2;
                                        item = new ListViewItem(strArray);
                                        this.Infolist.Items.Add(item);
                                        num++;
                                    }
                                    catch
                                    {
                                        break;
                                    }
                                }
                                view = (IActiveView) this.ibasicMap_0;
                                obj3.Flash(view.ScreenDisplay);
                            }
                            else
                            {
                                Exception exception;
                                if (obj3 is ITinIdentifyObj2)
                                {
                                    this.panel5.AutoSize = true;
                                    this.Infolist.Items.Clear();
                                    num = 0;
                                    strArray = new string[2];
                                    while (true)
                                    {
                                        try
                                        {
                                            (obj3 as ITinIdentifyObj2).SetupEntity();
                                            (obj3 as ITinIdentifyObj2).GetPropAndValues(num, out str, out str2);
                                            if (str == null)
                                            {
                                                break;
                                            }
                                            strArray[0] = str;
                                            strArray[1] = str2;
                                            item = new ListViewItem(strArray);
                                            this.Infolist.Items.Add(item);
                                            num++;
                                        }
                                        catch (Exception exception1)
                                        {
                                            exception = exception1;
                                            string message = exception.Message;
                                            break;
                                        }
                                    }
                                    view = (IActiveView) this.ibasicMap_0;
                                    obj3.Flash(view.ScreenDisplay);
                                }
                                else if (obj3 is ICadIdentifyObj2)
                                {
                                    this.panel5.AutoSize = true;
                                    this.Infolist.Items.Clear();
                                    num = 0;
                                    strArray = new string[2];
                                    while (true)
                                    {
                                        try
                                        {
                                            (obj3 as ICadIdentifyObj2).SetupEntity();
                                            (obj3 as ICadIdentifyObj2).GetPropAndValues(num, out str, out str2);
                                            if (str == null)
                                            {
                                                break;
                                            }
                                            strArray[0] = str;
                                            strArray[1] = str2;
                                            item = new ListViewItem(strArray);
                                            this.Infolist.Items.Add(item);
                                            num++;
                                        }
                                        catch (Exception exception2)
                                        {
                                            exception = exception2;
                                            string text2 = exception.Message;
                                            break;
                                        }
                                    }
                                    view = (IActiveView) this.ibasicMap_0;
                                    obj3.Flash(view.ScreenDisplay);
                                }
                                else if (obj3 is IIdentifyObject)
                                {
                                    object obj4;
                                    object obj5;
                                    this.panel5.AutoSize = true;
                                    (obj3 as IIdentifyObject).PropertySet.GetAllProperties(out obj4, out obj5);
                                    this.Infolist.Items.Clear();
                                    strArray = new string[2];
                                    System.Array array = obj4;
                                    System.Array array2 = obj5;
                                    for (int i = 0; i < array.Length; i++)
                                    {
                                        strArray[0] = array.GetValue(i).ToString();
                                        strArray[1] = array2.GetValue(i).ToString();
                                        item = new ListViewItem(strArray);
                                        this.Infolist.Items.Add(item);
                                    }
                                    view = (IActiveView) this.ibasicMap_0;
                                    obj3.Flash(view.ScreenDisplay);
                                }
                            }
                        }
                    }
                    else if (tag is IFeature)
                    {
                        this.Infolist.Visible = true;
                        this.ifeature_0 = tag as IFeature;
                        view = (IActiveView) this.ibasicMap_0;
                        this.method_5(this.ifeature_0);
                        Flash.FlashFeature(view.ScreenDisplay, this.ifeature_0);
                    }
                    else
                    {
                        this.Infolist.Visible = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            this.combLayer.Width = (this.panel1.Width - this.combLayer.Left) - 10;
        }

        private void PanToFeature_Click(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                (sender as Control).Hide();
            }
            if (this.ifeature_1 != null)
            {
                Common.Pan2Feature(this.ibasicMap_0 as IActiveView, this.ifeature_1);
                (this.ibasicMap_0 as IActiveView).ScreenDisplay.UpdateWindow();
                Flash.FlashFeature((this.ibasicMap_0 as IActiveView).ScreenDisplay, this.ifeature_1);
            }
        }

        public void SetControl()
        {
            if (this.bool_0)
            {
                try
                {
                    if (this.ipoint_0 != null)
                    {
                        string str = "位置：(" + this.ipoint_0.X.ToString("#.#####") + ", " + this.ipoint_0.Y.ToString("#.#####") + ")";
                        this.txtPos.Text = str;
                    }
                    else
                    {
                        this.txtPos.Text = "";
                    }
                    this.method_0();
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
            }
        }

        public void SetInfo(IPoint ipoint_1, IArray iarray_1)
        {
            this.ipoint_0 = ipoint_1;
            this.iarray_0 = iarray_1;
        }

        public void SetInfo(IPoint ipoint_1, IArray iarray_1, IFeature ifeature_2)
        {
            this.ipoint_0 = ipoint_1;
            this.iarray_0 = iarray_1;
            this.ifeature_0 = ifeature_2;
        }

        private void ZoomToFeature_Click(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                (sender as Control).Hide();
            }
            if (this.ifeature_1 != null)
            {
                Common.Zoom2Feature(this.ibasicMap_0 as IActiveView, this.ifeature_1);
                (this.ibasicMap_0 as IActiveView).ScreenDisplay.UpdateWindow();
                Flash.FlashFeature((this.ibasicMap_0 as IActiveView).ScreenDisplay, this.ifeature_1);
            }
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.ilayer_0 = value;
            }
        }

        public DockingStyle DefaultDockingStyle
        {
            get
            {
                return DockingStyle.Right;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.ibasicMap_0 = value;
                if (this.iactiveViewEvents_Event_0 != null)
                {
                    this.iactiveViewEvents_Event_0 = null;
                }
                try
                {
                    this.iactiveViewEvents_Event_0 = value as ESRI.ArcGIS.Carto.IActiveViewEvents_Event;
                    this.iactiveViewEvents_Event_0.ItemAdded+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.method_14));
                    this.iactiveViewEvents_Event_0.ItemDeleted+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.method_15));
                    this.iactiveViewEvents_Event_0.ItemReordered+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemReorderedEventHandler(this.method_16));
                    this.iactiveViewEvents_Event_0.ContentsCleared+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ContentsClearedEventHandler(this.method_17));
                    if (this.bool_2)
                    {
                        this.bool_2 = false;
                        this.method_11();
                        this.bool_2 = true;
                    }
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
            }
        }

        public IList<string> IdentifyLayers
        {
            set
            {
                this.ilist_0 = value;
            }
        }

        public IdentifyTypeEnum IdentifyType
        {
            set
            {
                this.identifyTypeEnum_0 = value;
            }
        }

        public bool IsIdentify
        {
            set
            {
                this.bool_1 = value;
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

        public delegate void IdentifyLayerChangedHandler(object object_0, object object_1);

        public delegate void IdentifyTypeChangedHandler(object object_0, object object_1);
    }
}

