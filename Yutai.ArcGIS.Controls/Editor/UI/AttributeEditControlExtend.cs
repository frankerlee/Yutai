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
    public partial class AttributeEditControlExtend : UserControl, IDockContent
    {
        private bool m_CanDo = false;
        private bool m_CanEdit = true;
        private IMap m_EditMap = null;
        private bool m_HasLicense = false;
        private List<object> m_list = new List<object>();
        private AnnoEditControl m_pAnnoEditControl = new AnnoEditControl();
        private AttributeListControl m_pAttributeListControl = new AttributeListControl();
        private AttributeListControl m_pAttributeListControl1 = new AttributeListControl();
        private AttributeListControl m_pAttributeListControl2 = new AttributeListControl();
        private IMap m_pMap = null;
        private MultiAttributeListControlExtend m_pMultiAttributeListControl = new MultiAttributeListControlExtend();
        private RepresentationPropertyPage m_pRepresentationPropertyPage = new RepresentationPropertyPage();
        private int m_SelectType = 0;

        public AttributeEditControlExtend()
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
                    UID uid = new UIDClass
                    {
                        Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
                    };
                    IEnumLayer layer2 = this.m_pMap.get_Layers(uid, true);
                    layer2.Reset();
                    for (ILayer layer3 = layer2.Next(); layer3 != null; layer3 = layer2.Next())
                    {
                        IFeatureLayer layer = layer3 as IFeatureLayer;
                        if (((layer != null) && Yutai.ArcGIS.Common.Editor.Editor.CheckLayerCanEdit(layer)) &&
                            ((layer as IFeatureSelection).SelectionSet.Count > 0))
                        {
                            ICursor cursor;
                            list.Add(layer);
                            (layer as IFeatureSelection).SelectionSet.Search(null, false, out cursor);
                            this.AddSelectToList(cursor, this.m_list);
                            ComReleaser.ReleaseCOMObject(cursor);
                        }
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
                                if (list.Count > 0)
                                {
                                    this.m_pAttributeListControl2.FeatureLayer = list[0] as IFeatureLayer;
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
                                if (list.Count > 0)
                                {
                                    this.m_pAttributeListControl1.FeatureLayer = list[0] as IFeatureLayer;
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
            }
        }

        private void m_pActiveViewEvents_SelectionChanged()
        {
            this.Init();
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
            get { return DockingStyle.Right; }
        }

        public IMap FocusMap
        {
            set
            {
                if (this.m_pActiveViewEvents != null)
                {
                    try
                    {
                        this.m_pActiveViewEvents.SelectionChanged -=
                        (new IActiveViewEvents_SelectionChangedEventHandler(
                            this.m_pActiveViewEvents_SelectionChanged));
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
                    this.m_pActiveViewEvents.SelectionChanged +=
                        (new IActiveViewEvents_SelectionChangedEventHandler(this.m_pActiveViewEvents_SelectionChanged));
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
            get { return base.Name; }
        }

        int IDockContent.Width
        {
            get { return base.Width; }
        }
    }
}