using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class AttributeEditControlExtendEx : UserControl, IDockContent
    {
        private Bar bar2;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private IContainer components = null;
        private BarButtonItem FlashObject;
        private bool m_CanDo = false;
        private bool m_CanEdit = true;
        private IMap m_EditMap = null;
        private bool m_HasLicense = false;
        private List<object> m_list = new List<object>();
        private IActiveViewEvents_Event m_pActiveViewEvents;
        private AnnoEditControl m_pAnnoEditControl = new AnnoEditControl();
        private AttributeListControl m_pAttributeListControl = new AttributeListControl();
        private AttributeListControl m_pAttributeListControl1 = new AttributeListControl();
        private AttributeListControl m_pAttributeListControl2 = new AttributeListControl();
        private IMap m_pMap = null;
        private MultiAttributeListControlExtend m_pMultiAttributeListControl = new MultiAttributeListControlExtend();
        private RepresentationPropertyPage m_pRepresentationPropertyPage = new RepresentationPropertyPage();
        private int m_SelectType = 0;
        private Panel panel1;
        private Panel panel2;
        private Splitter splitter1;
        private TabControl tabControl1;
        private TabControl tabControl2;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TreeView treeView1;
        private BarDockControl barDockControl5;
        private BarDockControl barDockControl6;
        private BarDockControl barDockControl7;
        private BarDockControl barDockControl8;
        private BarDockControl barDockControl9;
        private BarDockControl barDockControl10;
        private BarDockControl barDockControl11;
        private BarDockControl barDockControl12;
        private BarDockControl barDockControl13;
        private BarDockControl barDockControl14;
        private BarButtonItem ZoomTo;

        public AttributeEditControlExtendEx()
        {
            this.InitializeComponent();
            this.Text = "属性编辑";
            this.tabControl1.Visible = false;
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
                this.m_pAnnoEditControl.Dock = DockStyle.Fill;
                this.m_pAttributeListControl.Dock = DockStyle.Fill;
                this.tabPage1.Controls.Add(this.m_pAnnoEditControl);
                this.tabPage2.Controls.Add(this.m_pAttributeListControl);
                this.m_pAttributeListControl1.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(this.m_pAttributeListControl1);
                this.m_pAttributeListControl2.Dock = DockStyle.Fill;
                this.tabPage3.Controls.Add(this.m_pAttributeListControl2);
                this.m_pRepresentationPropertyPage.Dock = DockStyle.Fill;
                this.tabPage4.Controls.Add(this.m_pRepresentationPropertyPage);
                this.m_pMultiAttributeListControl.Dock = DockStyle.Fill;
                this.panel2.Controls.Add(this.m_pMultiAttributeListControl);
                this.Text = "属性编辑";
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
            else if (this.m_SelectType == 2)
            {
                this.m_pAttributeListControl.SelectObject = null;
                this.m_pAnnoEditControl.AnnotationFeature = null;
                this.tabControl1.Visible = false;
            }
            else if (this.m_SelectType == 3)
            {
                this.m_pMultiAttributeListControl.LayerList = null;
                this.panel2.Visible = false;
            }
            this.tabControl2.Visible = false;
            this.ZoomTo.Enabled = false;
            this.FlashObject.Enabled = false;
            this.m_SelectType = 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
            ApplicationRef.Application.HideDockWindow(this);
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
                else
                {
                    List<object> list = new List<object>();
                    UID uid = new UIDClass {
                        Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
                    };
                    IEnumLayer layer2 = this.m_pMap.get_Layers(uid, true);
                    layer2.Reset();
                    for (ILayer layer3 = layer2.Next(); layer3 != null; layer3 = layer2.Next())
                    {
                        IFeatureLayer layer = layer3 as IFeatureLayer;
                        if (((layer != null) && Yutai.ArcGIS.Common.Editor.Editor.CheckLayerCanEdit(layer)) && ((layer as IFeatureSelection).SelectionSet.Count > 0))
                        {
                            list.Add(layer);
                            TreeNode node = new TreeNode(layer.Name) {
                                Tag = layer
                            };
                            this.treeView1.Nodes.Add(node);
                            this.AddSelectToTree(node, layer);
                        }
                    }
                    if (this.treeView1.Nodes.Count > 0)
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[0].Nodes[0];
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.FlashObject = new DevExpress.XtraBars.BarButtonItem();
            this.ZoomTo = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.barDockControl5 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl6 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl7 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl8 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl9 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl10 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl11 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl12 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl13 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl14 = new DevExpress.XtraBars.BarDockControl();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 123);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(330, 109);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(322, 83);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "注记";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(322, 83);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "属性";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 123);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 109);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 123);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 109);
            this.panel2.TabIndex = 2;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.FlashObject,
            this.ZoomTo});
            this.barManager1.MaxItemId = 2;
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 3";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.FlashObject),
            new DevExpress.XtraBars.LinkPersistInfo(this.ZoomTo)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Custom 3";
            // 
            // FlashObject
            // 
            this.FlashObject.Caption = "闪烁";
            this.FlashObject.Id = 0;
            this.FlashObject.Name = "FlashObject";
            this.FlashObject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FlashObject_ItemClick);
            // 
            // ZoomTo
            // 
            this.ZoomTo.Caption = "缩放到";
            this.ZoomTo.Id = 1;
            this.ZoomTo.Name = "ZoomTo";
            this.ZoomTo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ZoomTo_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(330, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 232);
            this.barDockControlBottom.Size = new System.Drawing.Size(330, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 201);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(330, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 201);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(322, 183);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "注记";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(322, 183);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "属性";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 123);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(330, 109);
            this.tabControl2.TabIndex = 4;
            this.tabControl2.Visible = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(322, 83);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "属性";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(322, 83);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "规则";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 31);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(330, 92);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 123);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(330, 3);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // barDockControl5
            // 
            this.barDockControl5.CausesValidation = false;
            this.barDockControl5.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl5.Location = new System.Drawing.Point(0, 0);
            this.barDockControl5.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControl6
            // 
            this.barDockControl6.CausesValidation = false;
            this.barDockControl6.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl6.Location = new System.Drawing.Point(0, 0);
            this.barDockControl6.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControl7
            // 
            this.barDockControl7.CausesValidation = false;
            this.barDockControl7.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl7.Location = new System.Drawing.Point(0, 0);
            this.barDockControl7.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControl8
            // 
            this.barDockControl8.CausesValidation = false;
            this.barDockControl8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl8.Location = new System.Drawing.Point(0, 0);
            this.barDockControl8.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControl9
            // 
            this.barDockControl9.CausesValidation = false;
            this.barDockControl9.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl9.Location = new System.Drawing.Point(0, 0);
            this.barDockControl9.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControl10
            // 
            this.barDockControl10.CausesValidation = false;
            this.barDockControl10.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl10.Location = new System.Drawing.Point(0, 0);
            this.barDockControl10.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControl11
            // 
            this.barDockControl11.CausesValidation = false;
            this.barDockControl11.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl11.Location = new System.Drawing.Point(0, 0);
            this.barDockControl11.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControl12
            // 
            this.barDockControl12.CausesValidation = false;
            this.barDockControl12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl12.Location = new System.Drawing.Point(0, 0);
            this.barDockControl12.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControl13
            // 
            this.barDockControl13.CausesValidation = false;
            this.barDockControl13.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl13.Location = new System.Drawing.Point(0, 0);
            this.barDockControl13.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControl14
            // 
            this.barDockControl14.CausesValidation = false;
            this.barDockControl14.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl14.Location = new System.Drawing.Point(0, 0);
            this.barDockControl14.Size = new System.Drawing.Size(0, 0);
            // 
            // AttributeEditControlExtendEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "AttributeEditControlExtendEx";
            this.Size = new System.Drawing.Size(330, 232);
            this.Load += new System.EventHandler(this.AttributeEditControlExtend_Load);
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
                if (this.m_list.Count == 1)
                {
                    object obj2 = this.m_list[0];
                    if (obj2 is IAnnotationFeature)
                    {
                        this.m_pAttributeListControl.SelectObject = obj2 as IObject;
                        this.m_pAnnoEditControl.AnnotationFeature = obj2 as IAnnotationFeature;
                        if (this.m_SelectType != 2)
                        {
                            this.tabControl1.Visible = true;
                            this.panel1.Visible = false;
                            this.panel2.Visible = false;
                            this.tabControl2.Visible = true;
                            this.m_SelectType = 2;
                        }
                    }
                    else if (RepresentationAssist.HasRepresentation(obj2 as IFeature))
                    {
                        if (tag != null)
                        {
                            this.m_pAttributeListControl2.FeatureLayer = tag;
                        }
                        this.m_pAttributeListControl2.SelectObject = obj2 as IObject;
                        if (this.m_SelectType != 4)
                        {
                            this.tabControl2.Visible = false;
                            this.panel1.Visible = true;
                            this.panel2.Visible = false;
                            this.tabControl1.Visible = true;
                            this.m_SelectType = 4;
                        }
                    }
                    else
                    {
                        if (tag != null)
                        {
                            this.m_pAttributeListControl1.FeatureLayer = tag;
                        }
                        this.m_pAttributeListControl1.SelectObject = obj2 as IObject;
                        if (this.m_SelectType != 1)
                        {
                            this.tabControl1.Visible = false;
                            this.panel1.Visible = true;
                            this.panel2.Visible = false;
                            this.tabControl2.Visible = true;
                            this.m_SelectType = 1;
                        }
                        this.m_pAttributeListControl1.Visible = true;
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
                        this.tabControl1.Visible = false;
                        this.panel2.Visible = true;
                        this.panel1.Visible = false;
                        this.tabControl2.Visible = false;
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
                    this.m_pAnnoEditControl.ActiveView = this.m_pMap as IActiveView;
                    this.m_pAttributeListControl.ActiveView = this.m_pMap as IActiveView;
                    this.m_pAttributeListControl1.ActiveView = this.m_pMap as IActiveView;
                    this.m_pAttributeListControl2.ActiveView = this.m_pMap as IActiveView;
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

