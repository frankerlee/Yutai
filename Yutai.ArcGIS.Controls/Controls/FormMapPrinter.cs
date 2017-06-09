using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using ESRI.ArcGIS.ADF.COMSupport;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Controls.ApplicationStyle;
using Yutai.ArcGIS.Controls.Historical;
using Yutai.ArcGIS.Controls.SymbolUI;
using Yutai.ArcGIS.Framework;
using Yutai.Shared;
using LoadComponent = Yutai.ArcGIS.Common.LoadComponent;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class FormMapPrinter : Form
    {
        private BarButtonItem AddData;
        public AxPageLayoutControl axPageLayoutControl1;
        private Bar bar1;
        private Bar bar2;
        private Bar bar3;
        private Bar bar4;
        private Bar bar6;
        private BarAndDockingController barAndDockingController1;
        private BarDockControl barDockControl1;
        private BarDockControl barDockControl2;
        private BarDockControl barDockControl3;
        private BarDockControl barDockControl4;
        private BarEditItem barEditItem1;
        private BarManager barManager1;
        private BarStaticItem BarMessageItem;
        private BarStaticItem BarMousePositionItem;
        private BarStaticItem BarPagePositionItem;
        private BarStaticItem BarStaticItem1;
        private BarButtonItem ChangeLayout;
        private BarButtonItem ClearBackground;
        private BarButtonItem ClearBorder;
        private BarButtonItem ClearShadow;
        private IContainer components;
        private BarButtonItem ControlToolsGraphicElement_Group;
        private BarButtonItem ControlToolsGraphicElement_NewCircle;
        private BarButtonItem ControlToolsGraphicElement_NewCurve;
        private BarButtonItem ControlToolsGraphicElement_NewEllipse;
        private BarButtonItem ControlToolsGraphicElement_NewFrame;
        private BarButtonItem ControlToolsGraphicElement_NewFreeHand;
        private BarButtonItem ControlToolsGraphicElement_NewLine;
        private BarButtonItem ControlToolsGraphicElement_NewPolygon;
        private BarButtonItem ControlToolsGraphicElement_NewRectangle;
        private BarButtonItem ControlToolsGraphicElement_RotateElement;
        private BarButtonItem ControlToolsGraphicElement_RotateLeft;
        private BarButtonItem ControlToolsGraphicElement_RotateRight;
        private BarButtonItem ControlToolsGraphicElement_SelectTool;
        private BarButtonItem ControlToolsGraphicElement_Ungroup;
        private BarButtonItem ControlToolsMapNavigation_ClearMapRotation;
        private BarButtonItem ControlToolsMapNavigation_RefreshView;
        private BarButtonItem ControlToolsMapNavigation_Rotate;
        private BarButtonItem ControlToolsPageLayout_NewMap;
        private BarButtonItem ControlToolsPageLayout_PagePan;
        private BarButtonItem ControlToolsPageLayout_PageZoomIn;
        private BarButtonItem ControlToolsPageLayout_PageZoomInFixed;
        private BarButtonItem ControlToolsPageLayout_PageZoomOut;
        private BarButtonItem ControlToolsPageLayout_PageZoomOutFixed;
        private BarButtonItem ControlToolsPageLayout_Zoom100Percent;
        private BarButtonItem ControlToolsPageLayout_ZoomPageToLastExtentBack;
        private BarButtonItem ControlToolsPageLayout_ZoomPageToLastExtentForward;
        private BarButtonItem ControlToolsPageLayout_ZoomPageWidth;
        private BarButtonItem ControlToolsPageLayout_ZoomWholePage;
        private BarButtonItem CPrintSetupCommand;
        private BarButtonItem ElementSelectTool;
        private BarButtonItem FixedZoomIn;
        private BarButtonItem FixedZoomOut;
        private BarButtonItem FullExtent;
        private ImageList imageList1;
        private ImageList imageList2;
        private BarButtonItem InsertCircleElement;
        private BarButtonItem InsertEllipseElement;
        private BarSubItem InsertItem;
        private BarButtonItem InsertJTBElement;
        private BarButtonItem InsertLegend;
        private BarButtonItem InsertLineElement;
        private BarButtonItem InsertMapFrame;
        private BarButtonItem InsertNeatline;
        private BarButtonItem InsertNewCurveElement;
        private BarButtonItem InsertNorthArrow;
        private BarButtonItem InsertObject;
        private BarButtonItem InsertOutLine;
        private BarButtonItem InsertPicture;
        private BarButtonItem InsertPolygonElement;
        private BarButtonItem InsertRectangleElement;
        private BarButtonItem InsertScaleBar;
        private BarButtonItem InsertScaleText;
        private BarButtonItem InsertText;
        private BarButtonItem InsertTitle;
        private BarButtonItem LabelManager;
        private IList m_CommandList = new ArrayList();
        private IList m_esriCommandList = new ArrayList();
        private IApplication m_pApp = new ApplicationBase();
        private PaintStyleMenuItem m_pApplicationStyle = new PaintStyleMenuItem();
        private IGeometry m_pClipGeometry = null;
        private IMap m_pSourcesMap = null;
        private IPageLayout m_pSourcesPageLayout = null;
        private Bar MainMenubar;
        private BarButtonItem MapGridProperty;
        private BarEditItem MapScaleSetCommand;
        private BarButtonItem NewMapGridCommand;
        private BarButtonItem NewMapGridWizardCommand;
        private BarButtonItem NextExtent;
        private BarButtonItem OpenDocument;
        private BarButtonItem PageSetupCommand;
        private BarButtonItem Pan;
        private BarButtonItem PreviousExtent;
        private RepositoryItemComboBox repositoryItemComboBox1;
        private RepositoryItemComboBox repositoryItemComboBox2;
        private BarButtonItem SaveDocument;
        private BarSubItem Set;
        private BarButtonItem SetMapScale;
        private BarButtonItem SetupBackground;
        private BarButtonItem SetupBorder;
        private BarButtonItem SetupShadow;
        private BarButtonItem StyleManager;
        private BarButtonItem UnLockLabels;
        private BarButtonItem UnPlacedLabels;
        private const int WM_ENTERSIZEMOVE = 0x231;
        private const int WM_EXITSIZEMOVE = 0x232;
        private BarButtonItem ZoomIn;
        private BarButtonItem ZoomOut;

        public FormMapPrinter()
        {
            this.InitializeComponent();
        }

        private void barManager1_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand tag = null;
            if (e.Item.Tag is ICommand)
            {
                tag = e.Item.Tag as ICommand;
            }
            else
            {
                tag = this.FindCommand(e.Item.Name);
            }
            if (tag != null)
            {
                if (tag is ITool)
                {
                    this.SetCurrentTool(tag as ITool);
                }
                else
                {
                    tag.OnClick();
                }
                this.UpdataUI();
            }
        }

        private void CopyMap(IMap fromCopyMap, IMap toCopyMap)
        {
            int num;
            toCopyMap.ClearLayers();
            toCopyMap.DistanceUnits = fromCopyMap.DistanceUnits;
            toCopyMap.MapUnits = fromCopyMap.MapUnits;
            toCopyMap.SpatialReferenceLocked = false;
            toCopyMap.SpatialReference = fromCopyMap.SpatialReference;
            toCopyMap.Name = fromCopyMap.Name;
            for (num = fromCopyMap.LayerCount - 1; num >= 0; num--)
            {
                ILayer layer = fromCopyMap.get_Layer(num);
                toCopyMap.AddLayer(layer);
            }
            IGraphicsContainer container = fromCopyMap as IGraphicsContainer;
            container.Reset();
            IElement element = container.Next();
            int zorder = 0;
            while (element != null)
            {
                (toCopyMap as IGraphicsContainer).AddElement(element, zorder);
                zorder++;
                element = container.Next();
            }
            ITableCollection tables = fromCopyMap as ITableCollection;
            for (num = 0; num < tables.TableCount; num++)
            {
                (toCopyMap as ITableCollection).AddTable(tables.get_Table(num));
            }
            (toCopyMap as IActiveView).Extent = (fromCopyMap as IActiveView).Extent;
        }

        private BarItem CreateBarItem(ICommand pCommand)
        {
            BarItem item;
            BarManagerCategory category;
            if (pCommand is IBarEditItem)
            {
                item = new BarEditItem();
                RepositoryItemComboBox box = new RepositoryItemComboBox();
                item.Width = (pCommand as IBarEditItem).Width;
                (item as BarEditItem).Edit = box;
                box.AutoHeight = false;
                box.Name = pCommand.Name + "_ItemComboBox";
                (pCommand as IBarEditItem).BarEditItem = item;
            }
            else
            {
                item = new BarButtonItem();
            }
            this.barManager1.Items.Add(item);
            item.Id = this.barManager1.GetNewItemId();
            item.Name = pCommand.Name;
            item.Tag = pCommand;
            item.Caption = pCommand.Caption;
            if (pCommand.Tooltip != null)
            {
                item.Hint = pCommand.Tooltip;
            }
            else if (pCommand.Caption != null)
            {
                item.Hint = pCommand.Caption;
            }
            if (pCommand.Bitmap != 0)
            {
                try
                {
                    IntPtr hbitmap = new IntPtr(pCommand.Bitmap);
                    item.Glyph = Image.FromHbitmap(hbitmap);
                }
                catch
                {
                }
            }
            if (pCommand.Category != null)
            {
                if (pCommand.Category.Length > 0)
                {
                    category = this.barManager1.Categories[pCommand.Category];
                    if (category == null)
                    {
                        category = new BarManagerCategory(pCommand.Category, Guid.NewGuid());
                        this.barManager1.Categories.Add(category);
                    }
                    item.Category = category;
                    return item;
                }
                category = this.barManager1.Categories["其他"];
                if (category == null)
                {
                    category = new BarManagerCategory("其他", Guid.NewGuid());
                    this.barManager1.Categories.Add(category);
                }
                item.Category = category;
                return item;
            }
            category = this.barManager1.Categories["其他"];
            if (category == null)
            {
                category = new BarManagerCategory("其他", Guid.NewGuid());
                this.barManager1.Categories.Add(category);
            }
            item.Category = category;
            return item;
        }

        private void CreateESRICommand()
        {
            ICommand command = new ControlsMapDownCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsMapRefreshViewCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsMapLeftCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsMapRightCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsMapUpCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsMapRotateToolClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsPagePanToolClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsPageZoom100PercentCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsPageZoomInFixedCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsPageZoomInToolClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsPageZoomOutFixedCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsPageZoomOutToolClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsPageZoomPageToLastExtentBackCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsPageZoomPageToLastExtentForwardCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsPageZoomPageWidthCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsPageZoomWholePageCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsRotateElementToolClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsRotateLeftCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsRotateRightCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsSelectToolClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsSendBackwardCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsSendToBackCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsGroupCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsUngroupCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsMapClearMapRotationCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsBringForwardCommandClass();
            this.m_esriCommandList.Add(command);
            command = new ControlsBringToFrontCommandClass();
            this.m_esriCommandList.Add(command);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private ICommand FindCommand(string Name)
        {
            ICommand command = null;
            int num;
            for (num = 0; num < this.m_CommandList.Count; num++)
            {
                command = this.m_CommandList[num] as ICommand;
                if (Name == command.Name)
                {
                    return command;
                }
            }
            for (num = 0; num < this.m_esriCommandList.Count; num++)
            {
                command = this.m_esriCommandList[num] as ICommand;
                if (Name == command.Name)
                {
                    return command;
                }
            }
            return null;
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            ApplicationBase.IsPrintForm = false;
            AOUninitialize.Shutdown();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplicationBase.IsPrintForm = true;
            this.m_pApp.Hook = this.axPageLayoutControl1.Object;
            this.fullPageLayerOut(this.axPageLayoutControl1.ActiveView.FocusMap, (this.m_pSourcesMap as IActiveView).Extent);
            this.CopyMap(this.m_pSourcesMap, this.axPageLayoutControl1.ActiveView.FocusMap);
            if (this.m_pClipGeometry != null)
            {
                (this.axPageLayoutControl1.ActiveView.FocusMap as IMapAdmin2).ClipBounds = this.m_pClipGeometry;
            }
            if (this.m_pSourcesPageLayout != null)
            {
            }
            string fileName = System.IO.Path.Combine(Application.StartupPath, "ForMapPtinterMenu.xml");
            this.LoadTools(this.m_pApp, fileName);
            this.CreateESRICommand();
            this.SetHook(this.axPageLayoutControl1.Object);
            DocumentManager.Register(this.axPageLayoutControl1.Object);
            this.UpdataUI();
        }

        private void fullPageLayerOut(IMap PageLayOutOfMap, IEnvelope MapControlMapOfExtend)
        {
            try
            {
                IActiveView view = (IActiveView) PageLayOutOfMap;
                view.ScreenDisplay.DisplayTransformation.VisibleBounds = MapControlMapOfExtend;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        private ITool GetCurrentTool()
        {
            return ((IPageLayoutControl2) this.axPageLayoutControl1.Object).CurrentTool;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMapPrinter));
            this.imageList2 = new ImageList(this.components);
            this.barManager1 = new BarManager(this.components);
            this.MainMenubar = new Bar();
            this.InsertItem = new BarSubItem();
            this.AddData = new BarButtonItem();
            this.OpenDocument = new BarButtonItem();
            this.SaveDocument = new BarButtonItem();
            this.InsertTitle = new BarButtonItem();
            this.InsertText = new BarButtonItem();
            this.InsertPicture = new BarButtonItem();
            this.InsertLineElement = new BarButtonItem();
            this.InsertCircleElement = new BarButtonItem();
            this.InsertEllipseElement = new BarButtonItem();
            this.InsertPolygonElement = new BarButtonItem();
            this.InsertRectangleElement = new BarButtonItem();
            this.InsertNewCurveElement = new BarButtonItem();
            this.InsertNeatline = new BarButtonItem();
            this.InsertLegend = new BarButtonItem();
            this.InsertNorthArrow = new BarButtonItem();
            this.InsertScaleBar = new BarButtonItem();
            this.InsertScaleText = new BarButtonItem();
            this.NewMapGridWizardCommand = new BarButtonItem();
            this.InsertObject = new BarButtonItem();
            this.Set = new BarSubItem();
            this.PageSetupCommand = new BarButtonItem();
            this.SetMapScale = new BarButtonItem();
            this.SetupBorder = new BarButtonItem();
            this.ClearBorder = new BarButtonItem();
            this.SetupShadow = new BarButtonItem();
            this.ClearShadow = new BarButtonItem();
            this.SetupBackground = new BarButtonItem();
            this.ClearBackground = new BarButtonItem();
            this.StyleManager = new BarButtonItem();
            this.bar2 = new Bar();
            this.ZoomIn = new BarButtonItem();
            this.ZoomOut = new BarButtonItem();
            this.FixedZoomIn = new BarButtonItem();
            this.FixedZoomOut = new BarButtonItem();
            this.MapScaleSetCommand = new BarEditItem();
            this.repositoryItemComboBox1 = new RepositoryItemComboBox();
            this.Pan = new BarButtonItem();
            this.PreviousExtent = new BarButtonItem();
            this.NextExtent = new BarButtonItem();
            this.FullExtent = new BarButtonItem();
            this.ControlToolsMapNavigation_RefreshView = new BarButtonItem();
            this.ElementSelectTool = new BarButtonItem();
            this.CPrintSetupCommand = new BarButtonItem();
            this.bar4 = new Bar();
            this.BarMessageItem = new BarStaticItem();
            this.BarStaticItem1 = new BarStaticItem();
            this.BarMousePositionItem = new BarStaticItem();
            this.BarPagePositionItem = new BarStaticItem();
            this.bar6 = new Bar();
            this.ControlToolsPageLayout_PageZoomIn = new BarButtonItem();
            this.ControlToolsPageLayout_PageZoomOut = new BarButtonItem();
            this.ControlToolsPageLayout_PageZoomInFixed = new BarButtonItem();
            this.ControlToolsPageLayout_PageZoomOutFixed = new BarButtonItem();
            this.ControlToolsPageLayout_Zoom100Percent = new BarButtonItem();
            this.ControlToolsPageLayout_ZoomWholePage = new BarButtonItem();
            this.ControlToolsPageLayout_ZoomPageWidth = new BarButtonItem();
            this.ControlToolsPageLayout_PagePan = new BarButtonItem();
            this.ControlToolsPageLayout_ZoomPageToLastExtentBack = new BarButtonItem();
            this.ControlToolsPageLayout_ZoomPageToLastExtentForward = new BarButtonItem();
            this.bar1 = new Bar();
            this.ChangeLayout = new BarButtonItem();
            this.MapGridProperty = new BarButtonItem();
            this.InsertJTBElement = new BarButtonItem();
            this.bar3 = new Bar();
            this.LabelManager = new BarButtonItem();
            this.UnPlacedLabels = new BarButtonItem();
            this.UnLockLabels = new BarButtonItem();
            this.barAndDockingController1 = new BarAndDockingController(this.components);
            this.barDockControl1 = new BarDockControl();
            this.barDockControl2 = new BarDockControl();
            this.barDockControl3 = new BarDockControl();
            this.barDockControl4 = new BarDockControl();
            this.imageList1 = new ImageList(this.components);
            this.ControlToolsGraphicElement_NewCircle = new BarButtonItem();
            this.ControlToolsGraphicElement_NewCurve = new BarButtonItem();
            this.ControlToolsGraphicElement_NewEllipse = new BarButtonItem();
            this.ControlToolsGraphicElement_NewFrame = new BarButtonItem();
            this.ControlToolsGraphicElement_NewFreeHand = new BarButtonItem();
            this.ControlToolsGraphicElement_NewLine = new BarButtonItem();
            this.ControlToolsGraphicElement_NewPolygon = new BarButtonItem();
            this.ControlToolsGraphicElement_NewRectangle = new BarButtonItem();
            this.ControlToolsPageLayout_NewMap = new BarButtonItem();
            this.ControlToolsMapNavigation_Rotate = new BarButtonItem();
            this.ControlToolsGraphicElement_RotateElement = new BarButtonItem();
            this.ControlToolsGraphicElement_RotateLeft = new BarButtonItem();
            this.ControlToolsGraphicElement_RotateRight = new BarButtonItem();
            this.ControlToolsGraphicElement_Group = new BarButtonItem();
            this.ControlToolsGraphicElement_Ungroup = new BarButtonItem();
            this.ControlToolsMapNavigation_ClearMapRotation = new BarButtonItem();
            this.InsertMapFrame = new BarButtonItem();
            this.ControlToolsGraphicElement_SelectTool = new BarButtonItem();
            this.axPageLayoutControl1 = new AxPageLayoutControl();
            this.barEditItem1 = new BarEditItem();
            this.repositoryItemComboBox2 = new RepositoryItemComboBox();
            this.barManager1.BeginInit();
            this.repositoryItemComboBox1.BeginInit();
            this.barAndDockingController1.BeginInit();
            this.axPageLayoutControl1.BeginInit();
            this.repositoryItemComboBox2.BeginInit();
            base.SuspendLayout();
            this.imageList2.ImageStream = (ImageListStreamer) resources.GetObject("imageList2.ImageStream");
            this.imageList2.TransparentColor = Color.Magenta;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            this.imageList2.Images.SetKeyName(3, "");
            this.imageList2.Images.SetKeyName(4, "");
            this.imageList2.Images.SetKeyName(5, "");
            this.imageList2.Images.SetKeyName(6, "");
            this.imageList2.Images.SetKeyName(7, "");
            this.imageList2.Images.SetKeyName(8, "");
            this.imageList2.Images.SetKeyName(9, "");
            this.imageList2.Images.SetKeyName(10, "");
            this.imageList2.Images.SetKeyName(11, "");
            this.imageList2.Images.SetKeyName(12, "");
            this.imageList2.Images.SetKeyName(13, "");
            this.imageList2.Images.SetKeyName(14, "");
            this.imageList2.Images.SetKeyName(15, "");
            this.imageList2.Images.SetKeyName(0x10, "");
            this.imageList2.Images.SetKeyName(0x11, "");
            this.imageList2.Images.SetKeyName(0x12, "");
            this.imageList2.Images.SetKeyName(0x13, "");
            this.imageList2.Images.SetKeyName(20, "");
            this.imageList2.Images.SetKeyName(0x15, "");
            this.imageList2.Images.SetKeyName(0x16, "");
            this.imageList2.Images.SetKeyName(0x17, "");
            this.imageList2.Images.SetKeyName(0x18, "");
            this.imageList2.Images.SetKeyName(0x19, "");
            this.imageList2.Images.SetKeyName(0x1a, "");
            this.imageList2.Images.SetKeyName(0x1b, "");
            this.imageList2.Images.SetKeyName(0x1c, "");
            this.imageList2.Images.SetKeyName(0x1d, "");
            this.imageList2.Images.SetKeyName(30, "");
            this.barManager1.Bars.AddRange(new Bar[] { this.MainMenubar, this.bar2, this.bar4, this.bar6, this.bar1, this.bar3 });
            this.barManager1.Categories.AddRange(new BarManagerCategory[] { new BarManagerCategory("弹出菜单", new Guid("1d5a55eb-4a56-4fd3-9818-45cc5ef97b55")), new BarManagerCategory("平移/缩放", new Guid("de6da363-5d66-4195-af3a-49f2e42d8cc0")), new BarManagerCategory("其他", new Guid("b59fd33d-a36e-479a-a09e-8bc218840424")), new BarManagerCategory("图形元素", new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a")), new BarManagerCategory("页面布局", new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6")), new BarManagerCategory("地图浏览", new Guid("e5f39d20-2be7-4874-86cf-bc2c58756ba9")), new BarManagerCategory("制图", new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088")) });
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl3);
            this.barManager1.DockControls.Add(this.barDockControl4);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new BarItem[] { 
                this.ZoomIn, this.ZoomOut, this.PreviousExtent, this.Pan, this.FixedZoomIn, this.FixedZoomOut, this.NextExtent, this.FullExtent, this.BarMessageItem, this.BarStaticItem1, this.BarMousePositionItem, this.BarPagePositionItem, this.ControlToolsGraphicElement_NewCircle, this.ControlToolsGraphicElement_NewCurve, this.ControlToolsGraphicElement_NewEllipse, this.ControlToolsGraphicElement_NewFrame, 
                this.ControlToolsGraphicElement_NewFreeHand, this.ControlToolsGraphicElement_NewLine, this.ControlToolsGraphicElement_NewPolygon, this.ControlToolsGraphicElement_NewRectangle, this.ControlToolsPageLayout_NewMap, this.ControlToolsMapNavigation_Rotate, this.ControlToolsPageLayout_PagePan, this.ControlToolsPageLayout_Zoom100Percent, this.ControlToolsPageLayout_PageZoomInFixed, this.ControlToolsPageLayout_PageZoomIn, this.ControlToolsPageLayout_PageZoomOutFixed, this.ControlToolsPageLayout_PageZoomOut, this.ControlToolsPageLayout_ZoomPageToLastExtentBack, this.ControlToolsPageLayout_ZoomPageToLastExtentForward, this.ControlToolsPageLayout_ZoomPageWidth, this.ControlToolsPageLayout_ZoomWholePage, 
                this.ControlToolsGraphicElement_RotateElement, this.ControlToolsGraphicElement_RotateLeft, this.ControlToolsGraphicElement_RotateRight, this.ElementSelectTool, this.ControlToolsGraphicElement_Group, this.ControlToolsGraphicElement_Ungroup, this.ControlToolsMapNavigation_ClearMapRotation, this.ControlToolsMapNavigation_RefreshView, this.StyleManager, this.InsertMapFrame, this.InsertNorthArrow, this.InsertItem, this.InsertTitle, this.InsertText, this.InsertNeatline, this.InsertLegend, 
                this.InsertScaleBar, this.InsertScaleText, this.SetupBorder, this.ClearBorder, this.SetupShadow, this.ClearShadow, this.SetupBackground, this.ClearBackground, this.Set, this.CPrintSetupCommand, this.PageSetupCommand, this.SetMapScale, this.NewMapGridWizardCommand, this.OpenDocument, this.SaveDocument, this.AddData, 
                this.ControlToolsGraphicElement_SelectTool, this.InsertPicture, this.InsertLineElement, this.InsertCircleElement, this.InsertEllipseElement, this.InsertPolygonElement, this.InsertRectangleElement, this.InsertNewCurveElement, this.InsertObject, this.LabelManager, this.UnPlacedLabels, this.UnLockLabels, this.ChangeLayout, this.MapGridProperty, this.InsertJTBElement, this.MapScaleSetCommand, 
                this.barEditItem1
             });
            this.barManager1.MainMenu = this.MainMenubar;
            this.barManager1.MaxItemId = 0xbf;
            this.barManager1.RepositoryItems.AddRange(new RepositoryItem[] { this.repositoryItemComboBox1, this.repositoryItemComboBox2 });
            this.barManager1.ShowFullMenus = true;
            this.barManager1.StatusBar = this.bar4;
            this.barManager1.ItemClick += new ItemClickEventHandler(this.barManager1_ItemClick);
            this.MainMenubar.BarName = "主菜单";
            this.MainMenubar.DockCol = 0;
            this.MainMenubar.DockRow = 0;
            this.MainMenubar.DockStyle = BarDockStyle.Top;
            this.MainMenubar.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.InsertItem, true), new LinkPersistInfo(this.Set) });
            this.MainMenubar.OptionsBar.AllowQuickCustomization = false;
            this.MainMenubar.OptionsBar.MultiLine = true;
            this.MainMenubar.OptionsBar.UseWholeRow = true;
            this.MainMenubar.Text = "主菜单";
            this.InsertItem.Caption = "插入(&I)";
            this.InsertItem.CategoryGuid = new Guid("1d5a55eb-4a56-4fd3-9818-45cc5ef97b55");
            this.InsertItem.Id = 120;
            this.InsertItem.ItemShortcut = new BarShortcut(Keys.Alt | Keys.I);
            this.InsertItem.LinksPersistInfo.AddRange(new LinkPersistInfo[] { 
                new LinkPersistInfo(this.AddData, true), new LinkPersistInfo(this.OpenDocument, true), new LinkPersistInfo(this.SaveDocument), new LinkPersistInfo(this.InsertTitle, true), new LinkPersistInfo(this.InsertText), new LinkPersistInfo(this.InsertPicture), new LinkPersistInfo(this.InsertLineElement, true), new LinkPersistInfo(this.InsertCircleElement), new LinkPersistInfo(this.InsertEllipseElement), new LinkPersistInfo(this.InsertPolygonElement), new LinkPersistInfo(this.InsertRectangleElement), new LinkPersistInfo(this.InsertNewCurveElement), new LinkPersistInfo(this.InsertNeatline, true), new LinkPersistInfo(this.InsertLegend, true), new LinkPersistInfo(this.InsertNorthArrow), new LinkPersistInfo(this.InsertScaleBar), 
                new LinkPersistInfo(this.InsertScaleText), new LinkPersistInfo(this.NewMapGridWizardCommand, true), new LinkPersistInfo(this.InsertObject)
             });
            this.InsertItem.Name = "InsertItem";
            this.AddData.Caption = "添加数据";
            this.AddData.CategoryGuid = new Guid("b59fd33d-a36e-479a-a09e-8bc218840424");
            this.AddData.Id = 0xad;
            this.AddData.Name = "AddData";
            this.OpenDocument.Caption = "打开文档";
            this.OpenDocument.CategoryGuid = new Guid("1d5a55eb-4a56-4fd3-9818-45cc5ef97b55");
            this.OpenDocument.Id = 0xab;
            this.OpenDocument.Name = "OpenDocument";
            this.SaveDocument.Caption = "保存文档";
            this.SaveDocument.CategoryGuid = new Guid("1d5a55eb-4a56-4fd3-9818-45cc5ef97b55");
            this.SaveDocument.Id = 0xac;
            this.SaveDocument.Name = "SaveDocument";
            this.InsertTitle.Caption = "标题";
            this.InsertTitle.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertTitle.Glyph = (Image) resources.GetObject("InsertTitle.Glyph");
            this.InsertTitle.Hint = "插入标题";
            this.InsertTitle.Id = 0x7a;
            this.InsertTitle.Name = "InsertTitle";
            this.InsertText.Caption = "文字";
            this.InsertText.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertText.Glyph = (Image) resources.GetObject("InsertText.Glyph");
            this.InsertText.Hint = "插入文字";
            this.InsertText.Id = 0x7b;
            this.InsertText.Name = "InsertText";
            this.InsertPicture.Caption = "图片";
            this.InsertPicture.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertPicture.Id = 0xaf;
            this.InsertPicture.Name = "InsertPicture";
            this.InsertLineElement.Caption = "新建线";
            this.InsertLineElement.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertLineElement.Id = 0xb0;
            this.InsertLineElement.Name = "InsertLineElement";
            this.InsertCircleElement.Caption = "新建圆";
            this.InsertCircleElement.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertCircleElement.Id = 0xb1;
            this.InsertCircleElement.Name = "InsertCircleElement";
            this.InsertEllipseElement.Caption = "新建椭圆";
            this.InsertEllipseElement.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertEllipseElement.Id = 0xb2;
            this.InsertEllipseElement.Name = "InsertEllipseElement";
            this.InsertPolygonElement.Caption = "新建多边形";
            this.InsertPolygonElement.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertPolygonElement.Id = 0xb3;
            this.InsertPolygonElement.Name = "InsertPolygonElement";
            this.InsertRectangleElement.Caption = "新建矩形";
            this.InsertRectangleElement.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertRectangleElement.Id = 180;
            this.InsertRectangleElement.Name = "InsertRectangleElement";
            this.InsertNewCurveElement.Caption = "新建曲线";
            this.InsertNewCurveElement.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertNewCurveElement.Id = 0xb5;
            this.InsertNewCurveElement.Name = "InsertNewCurveElement";
            this.InsertNeatline.Caption = "轮廓线";
            this.InsertNeatline.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertNeatline.Glyph = (Image) resources.GetObject("InsertNeatline.Glyph");
            this.InsertNeatline.Hint = "插入轮廓线";
            this.InsertNeatline.Id = 0x7c;
            this.InsertNeatline.Name = "InsertNeatline";
            this.InsertLegend.Caption = "图例";
            this.InsertLegend.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertLegend.Glyph = (Image) resources.GetObject("InsertLegend.Glyph");
            this.InsertLegend.Hint = "插入图例";
            this.InsertLegend.Id = 0x7d;
            this.InsertLegend.Name = "InsertLegend";
            this.InsertNorthArrow.Caption = "指北针";
            this.InsertNorthArrow.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertNorthArrow.Glyph = (Image) resources.GetObject("InsertNorthArrow.Glyph");
            this.InsertNorthArrow.Hint = "插入指北针";
            this.InsertNorthArrow.Id = 0x77;
            this.InsertNorthArrow.Name = "InsertNorthArrow";
            this.InsertScaleBar.Caption = "比例尺";
            this.InsertScaleBar.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertScaleBar.Glyph = (Image) resources.GetObject("InsertScaleBar.Glyph");
            this.InsertScaleBar.Hint = "插入比例尺";
            this.InsertScaleBar.Id = 0x7e;
            this.InsertScaleBar.Name = "InsertScaleBar";
            this.InsertScaleText.Caption = "比例尺文本";
            this.InsertScaleText.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertScaleText.Glyph = (Image) resources.GetObject("InsertScaleText.Glyph");
            this.InsertScaleText.Hint = "插入比例尺文本";
            this.InsertScaleText.Id = 0x7f;
            this.InsertScaleText.Name = "InsertScaleText";
            this.NewMapGridWizardCommand.Caption = "创建格网";
            this.NewMapGridWizardCommand.CategoryGuid = new Guid("b59fd33d-a36e-479a-a09e-8bc218840424");
            this.NewMapGridWizardCommand.Id = 170;
            this.NewMapGridWizardCommand.Name = "NewMapGridWizardCommand";
            this.InsertObject.Caption = "插入对象";
            this.InsertObject.Id = 0xb6;
            this.InsertObject.Name = "InsertObject";
            this.Set.Caption = "设置(&S)";
            this.Set.CategoryGuid = new Guid("1d5a55eb-4a56-4fd3-9818-45cc5ef97b55");
            this.Set.Id = 0xa6;
            this.Set.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.PageSetupCommand), new LinkPersistInfo(this.SetMapScale), new LinkPersistInfo(this.SetupBorder, true), new LinkPersistInfo(this.ClearBorder), new LinkPersistInfo(this.SetupShadow, true), new LinkPersistInfo(this.ClearShadow), new LinkPersistInfo(this.SetupBackground, true), new LinkPersistInfo(this.ClearBackground), new LinkPersistInfo(this.StyleManager, true) });
            this.Set.Name = "Set";
            this.PageSetupCommand.Caption = "页面设置";
            this.PageSetupCommand.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.PageSetupCommand.Id = 0xa8;
            this.PageSetupCommand.Name = "PageSetupCommand";
            this.SetMapScale.Caption = "设置输出比例尺";
            this.SetMapScale.CategoryGuid = new Guid("b59fd33d-a36e-479a-a09e-8bc218840424");
            this.SetMapScale.Id = 0xa9;
            this.SetMapScale.Name = "SetMapScale";
            this.SetupBorder.Caption = "设置边界";
            this.SetupBorder.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.SetupBorder.Id = 0x9e;
            this.SetupBorder.Name = "SetupBorder";
            this.ClearBorder.Caption = "清除边界";
            this.ClearBorder.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.ClearBorder.Id = 0x9f;
            this.ClearBorder.Name = "ClearBorder";
            this.SetupShadow.Caption = "设置阴影";
            this.SetupShadow.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.SetupShadow.Id = 160;
            this.SetupShadow.Name = "SetupShadow";
            this.ClearShadow.Caption = "清除阴影";
            this.ClearShadow.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.ClearShadow.Id = 0xa1;
            this.ClearShadow.Name = "ClearShadow";
            this.SetupBackground.Caption = "设置背景";
            this.SetupBackground.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.SetupBackground.Id = 0xa2;
            this.SetupBackground.Name = "SetupBackground";
            this.ClearBackground.Caption = "清除背景";
            this.ClearBackground.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.ClearBackground.Id = 0xa3;
            this.ClearBackground.Name = "ClearBackground";
            this.StyleManager.Caption = "符号管理器";
            this.StyleManager.CategoryGuid = new Guid("b59fd33d-a36e-479a-a09e-8bc218840424");
            this.StyleManager.Id = 0x67;
            this.StyleManager.Name = "StyleManager";
            this.StyleManager.ItemClick += new ItemClickEventHandler(this.StyleManager_ItemClick);
            this.bar2.BarName = "工具";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 1;
            this.bar2.DockStyle = BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(0x176, 0xc6);
            this.bar2.FloatSize = new Size(0xc0, 0x18);
            this.bar2.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.ZoomIn), new LinkPersistInfo(this.ZoomOut), new LinkPersistInfo(this.FixedZoomIn), new LinkPersistInfo(this.FixedZoomOut), new LinkPersistInfo(BarLinkUserDefines.Width, this.MapScaleSetCommand, "", false, true, true, 0x85), new LinkPersistInfo(this.Pan), new LinkPersistInfo(this.PreviousExtent), new LinkPersistInfo(this.NextExtent), new LinkPersistInfo(this.FullExtent), new LinkPersistInfo(this.ControlToolsMapNavigation_RefreshView), new LinkPersistInfo(this.ElementSelectTool, true), new LinkPersistInfo(this.CPrintSetupCommand, true) });
            this.bar2.Offset = 2;
            this.bar2.OptionsBar.AllowDelete = true;
            this.bar2.Text = "工具";
            this.ZoomIn.Caption = "放大";
            this.ZoomIn.CategoryGuid = new Guid("de6da363-5d66-4195-af3a-49f2e42d8cc0");
            this.ZoomIn.Hint = "放大";
            this.ZoomIn.Id = 0x17;
            this.ZoomIn.ImageIndex = 0x1a;
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomOut.Caption = "缩小";
            this.ZoomOut.CategoryGuid = new Guid("de6da363-5d66-4195-af3a-49f2e42d8cc0");
            this.ZoomOut.Hint = "缩小";
            this.ZoomOut.Id = 0x18;
            this.ZoomOut.ImageIndex = 0x1c;
            this.ZoomOut.Name = "ZoomOut";
            this.FixedZoomIn.Caption = "固定放大";
            this.FixedZoomIn.CategoryGuid = new Guid("de6da363-5d66-4195-af3a-49f2e42d8cc0");
            this.FixedZoomIn.Hint = "固定放大";
            this.FixedZoomIn.Id = 0x1c;
            this.FixedZoomIn.ImageIndex = 0x1b;
            this.FixedZoomIn.Name = "FixedZoomIn";
            this.FixedZoomOut.Caption = "固定缩小";
            this.FixedZoomOut.CategoryGuid = new Guid("de6da363-5d66-4195-af3a-49f2e42d8cc0");
            this.FixedZoomOut.Hint = "固定缩小";
            this.FixedZoomOut.Id = 0x1d;
            this.FixedZoomOut.ImageIndex = 0x13;
            this.FixedZoomOut.Name = "FixedZoomOut";
            this.MapScaleSetCommand.Caption = "比例尺";
            this.MapScaleSetCommand.CategoryGuid = new Guid("de6da363-5d66-4195-af3a-49f2e42d8cc0");
            this.MapScaleSetCommand.Edit = this.repositoryItemComboBox1;
            this.MapScaleSetCommand.Id = 0xbd;
            this.MapScaleSetCommand.Name = "MapScaleSetCommand";
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.Pan.Caption = "平移";
            this.Pan.CategoryGuid = new Guid("de6da363-5d66-4195-af3a-49f2e42d8cc0");
            this.Pan.Hint = "平移";
            this.Pan.Id = 0x1b;
            this.Pan.ImageIndex = 0x17;
            this.Pan.Name = "Pan";
            this.PreviousExtent.Caption = "前一视图";
            this.PreviousExtent.CategoryGuid = new Guid("de6da363-5d66-4195-af3a-49f2e42d8cc0");
            this.PreviousExtent.Hint = "上一视图";
            this.PreviousExtent.Id = 0x1a;
            this.PreviousExtent.ImageIndex = 0x19;
            this.PreviousExtent.Name = "PreviousExtent";
            this.NextExtent.Caption = "后一视图";
            this.NextExtent.CategoryGuid = new Guid("de6da363-5d66-4195-af3a-49f2e42d8cc0");
            this.NextExtent.Hint = "下一视图";
            this.NextExtent.Id = 30;
            this.NextExtent.ImageIndex = 0x18;
            this.NextExtent.Name = "NextExtent";
            this.FullExtent.Caption = "全图";
            this.FullExtent.CategoryGuid = new Guid("de6da363-5d66-4195-af3a-49f2e42d8cc0");
            this.FullExtent.Hint = "全图";
            this.FullExtent.Id = 0x1f;
            this.FullExtent.ImageIndex = 20;
            this.FullExtent.Name = "FullExtent";
            this.ControlToolsMapNavigation_RefreshView.Caption = "刷新视图";
            this.ControlToolsMapNavigation_RefreshView.CategoryGuid = new Guid("e5f39d20-2be7-4874-86cf-bc2c58756ba9");
            this.ControlToolsMapNavigation_RefreshView.Glyph = (Image) resources.GetObject("ControlToolsMapNavigation_RefreshView.Glyph");
            this.ControlToolsMapNavigation_RefreshView.Hint = "刷新视图";
            this.ControlToolsMapNavigation_RefreshView.Id = 0x47;
            this.ControlToolsMapNavigation_RefreshView.Name = "ControlToolsMapNavigation_RefreshView";
            this.ElementSelectTool.Caption = "选择元素";
            this.ElementSelectTool.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ElementSelectTool.Glyph = (Image) resources.GetObject("ElementSelectTool.Glyph");
            this.ElementSelectTool.Hint = "选择元素";
            this.ElementSelectTool.Id = 0x5d;
            this.ElementSelectTool.Name = "ElementSelectTool";
            this.CPrintSetupCommand.Caption = "打印";
            this.CPrintSetupCommand.CategoryGuid = new Guid("1d5a55eb-4a56-4fd3-9818-45cc5ef97b55");
            this.CPrintSetupCommand.Glyph = (Image) resources.GetObject("CPrintSetupCommand.Glyph");
            this.CPrintSetupCommand.Id = 0xa7;
            this.CPrintSetupCommand.Name = "CPrintSetupCommand";
            this.bar4.BarName = "状态栏";
            this.bar4.CanDockStyle = BarCanDockStyle.Bottom;
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = BarDockStyle.Bottom;
            this.bar4.FloatLocation = new System.Drawing.Point(0x146, 0x1af);
            this.bar4.FloatSize = new Size(0x2e, 20);
            this.bar4.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.BarMessageItem), new LinkPersistInfo(this.BarStaticItem1), new LinkPersistInfo(this.BarMousePositionItem), new LinkPersistInfo(this.BarPagePositionItem) });
            this.bar4.OptionsBar.AllowDelete = true;
            this.bar4.OptionsBar.AllowQuickCustomization = false;
            this.bar4.OptionsBar.DrawDragBorder = false;
            this.bar4.OptionsBar.Hidden = true;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.Text = "状态栏";
            this.BarMessageItem.AutoSize = BarStaticItemSize.Spring;
            this.BarMessageItem.CategoryGuid = new Guid("b59fd33d-a36e-479a-a09e-8bc218840424");
            this.BarMessageItem.Id = 0x2a;
            this.BarMessageItem.Name = "BarMessageItem";
            this.BarMessageItem.TextAlignment = StringAlignment.Near;
            this.BarMessageItem.Width = 0x20;
            this.BarStaticItem1.AutoSize = BarStaticItemSize.None;
            this.BarStaticItem1.CategoryGuid = new Guid("b59fd33d-a36e-479a-a09e-8bc218840424");
            this.BarStaticItem1.Id = 0x2b;
            this.BarStaticItem1.Name = "BarStaticItem1";
            this.BarStaticItem1.TextAlignment = StringAlignment.Near;
            this.BarStaticItem1.Width = 40;
            this.BarMousePositionItem.AutoSize = BarStaticItemSize.None;
            this.BarMousePositionItem.CategoryGuid = new Guid("b59fd33d-a36e-479a-a09e-8bc218840424");
            this.BarMousePositionItem.Id = 0x2c;
            this.BarMousePositionItem.Name = "BarMousePositionItem";
            this.BarMousePositionItem.TextAlignment = StringAlignment.Near;
            this.BarMousePositionItem.Width = 160;
            this.BarPagePositionItem.AutoSize = BarStaticItemSize.None;
            this.BarPagePositionItem.CategoryGuid = new Guid("b59fd33d-a36e-479a-a09e-8bc218840424");
            this.BarPagePositionItem.Id = 0x2d;
            this.BarPagePositionItem.Name = "BarPagePositionItem";
            this.BarPagePositionItem.TextAlignment = StringAlignment.Near;
            this.BarPagePositionItem.Width = 120;
            this.bar6.BarName = "布局视图";
            this.bar6.DockCol = 1;
            this.bar6.DockRow = 1;
            this.bar6.DockStyle = BarDockStyle.Top;
            this.bar6.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.ControlToolsPageLayout_PageZoomIn), new LinkPersistInfo(this.ControlToolsPageLayout_PageZoomOut), new LinkPersistInfo(this.ControlToolsPageLayout_PageZoomInFixed), new LinkPersistInfo(this.ControlToolsPageLayout_PageZoomOutFixed), new LinkPersistInfo(this.ControlToolsPageLayout_Zoom100Percent), new LinkPersistInfo(this.ControlToolsPageLayout_ZoomWholePage), new LinkPersistInfo(this.ControlToolsPageLayout_ZoomPageWidth), new LinkPersistInfo(this.ControlToolsPageLayout_PagePan), new LinkPersistInfo(this.ControlToolsPageLayout_ZoomPageToLastExtentBack), new LinkPersistInfo(this.ControlToolsPageLayout_ZoomPageToLastExtentForward) });
            this.bar6.Offset = 0x17b;
            this.bar6.OptionsBar.AllowDelete = true;
            this.bar6.Text = "布局视图";
            this.ControlToolsPageLayout_PageZoomIn.Caption = "放大";
            this.ControlToolsPageLayout_PageZoomIn.CategoryGuid = new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6");
            this.ControlToolsPageLayout_PageZoomIn.Glyph = (Image) resources.GetObject("ControlToolsPageLayout_PageZoomIn.Glyph");
            this.ControlToolsPageLayout_PageZoomIn.Hint = "页面放大";
            this.ControlToolsPageLayout_PageZoomIn.Id = 0x4f;
            this.ControlToolsPageLayout_PageZoomIn.Name = "ControlToolsPageLayout_PageZoomIn";
            this.ControlToolsPageLayout_PageZoomOut.Caption = "缩小";
            this.ControlToolsPageLayout_PageZoomOut.CategoryGuid = new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6");
            this.ControlToolsPageLayout_PageZoomOut.Glyph = (Image) resources.GetObject("ControlToolsPageLayout_PageZoomOut.Glyph");
            this.ControlToolsPageLayout_PageZoomOut.Hint = "页面缩小";
            this.ControlToolsPageLayout_PageZoomOut.Id = 0x51;
            this.ControlToolsPageLayout_PageZoomOut.Name = "ControlToolsPageLayout_PageZoomOut";
            this.ControlToolsPageLayout_PageZoomInFixed.Caption = "中心放大";
            this.ControlToolsPageLayout_PageZoomInFixed.CategoryGuid = new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6");
            this.ControlToolsPageLayout_PageZoomInFixed.Glyph = (Image) resources.GetObject("ControlToolsPageLayout_PageZoomInFixed.Glyph");
            this.ControlToolsPageLayout_PageZoomInFixed.Hint = "页面中心放大";
            this.ControlToolsPageLayout_PageZoomInFixed.Id = 0x4e;
            this.ControlToolsPageLayout_PageZoomInFixed.Name = "ControlToolsPageLayout_PageZoomInFixed";
            this.ControlToolsPageLayout_PageZoomOutFixed.Caption = "中心缩小";
            this.ControlToolsPageLayout_PageZoomOutFixed.CategoryGuid = new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6");
            this.ControlToolsPageLayout_PageZoomOutFixed.Glyph = (Image) resources.GetObject("ControlToolsPageLayout_PageZoomOutFixed.Glyph");
            this.ControlToolsPageLayout_PageZoomOutFixed.Hint = "页面中心缩小";
            this.ControlToolsPageLayout_PageZoomOutFixed.Id = 80;
            this.ControlToolsPageLayout_PageZoomOutFixed.Name = "ControlToolsPageLayout_PageZoomOutFixed";
            this.ControlToolsPageLayout_Zoom100Percent.Caption = "放大到100%";
            this.ControlToolsPageLayout_Zoom100Percent.CategoryGuid = new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6");
            this.ControlToolsPageLayout_Zoom100Percent.Glyph = (Image) resources.GetObject("ControlToolsPageLayout_Zoom100Percent.Glyph");
            this.ControlToolsPageLayout_Zoom100Percent.Hint = "页面放大到100%";
            this.ControlToolsPageLayout_Zoom100Percent.Id = 0x4d;
            this.ControlToolsPageLayout_Zoom100Percent.Name = "ControlToolsPageLayout_Zoom100Percent";
            this.ControlToolsPageLayout_ZoomWholePage.Caption = "缩放到全页面";
            this.ControlToolsPageLayout_ZoomWholePage.CategoryGuid = new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6");
            this.ControlToolsPageLayout_ZoomWholePage.Glyph = (Image) resources.GetObject("ControlToolsPageLayout_ZoomWholePage.Glyph");
            this.ControlToolsPageLayout_ZoomWholePage.Hint = "缩放到全页面";
            this.ControlToolsPageLayout_ZoomWholePage.Id = 0x55;
            this.ControlToolsPageLayout_ZoomWholePage.Name = "ControlToolsPageLayout_ZoomWholePage";
            this.ControlToolsPageLayout_ZoomPageWidth.Caption = "缩放到页面宽度";
            this.ControlToolsPageLayout_ZoomPageWidth.CategoryGuid = new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6");
            this.ControlToolsPageLayout_ZoomPageWidth.Glyph = (Image) resources.GetObject("ControlToolsPageLayout_ZoomPageWidth.Glyph");
            this.ControlToolsPageLayout_ZoomPageWidth.Hint = "缩放到页面宽度";
            this.ControlToolsPageLayout_ZoomPageWidth.Id = 0x54;
            this.ControlToolsPageLayout_ZoomPageWidth.Name = "ControlToolsPageLayout_ZoomPageWidth";
            this.ControlToolsPageLayout_PagePan.Caption = "平移";
            this.ControlToolsPageLayout_PagePan.CategoryGuid = new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6");
            this.ControlToolsPageLayout_PagePan.Glyph = (Image) resources.GetObject("ControlToolsPageLayout_PagePan.Glyph");
            this.ControlToolsPageLayout_PagePan.Hint = "页面平移";
            this.ControlToolsPageLayout_PagePan.Id = 0x4c;
            this.ControlToolsPageLayout_PagePan.Name = "ControlToolsPageLayout_PagePan";
            this.ControlToolsPageLayout_ZoomPageToLastExtentBack.Caption = "上一屏";
            this.ControlToolsPageLayout_ZoomPageToLastExtentBack.CategoryGuid = new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6");
            this.ControlToolsPageLayout_ZoomPageToLastExtentBack.Glyph = (Image) resources.GetObject("ControlToolsPageLayout_ZoomPageToLastExtentBack.Glyph");
            this.ControlToolsPageLayout_ZoomPageToLastExtentBack.Hint = "上一页面";
            this.ControlToolsPageLayout_ZoomPageToLastExtentBack.Id = 0x52;
            this.ControlToolsPageLayout_ZoomPageToLastExtentBack.Name = "ControlToolsPageLayout_ZoomPageToLastExtentBack";
            this.ControlToolsPageLayout_ZoomPageToLastExtentForward.Caption = "下一屏";
            this.ControlToolsPageLayout_ZoomPageToLastExtentForward.CategoryGuid = new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6");
            this.ControlToolsPageLayout_ZoomPageToLastExtentForward.Glyph = (Image) resources.GetObject("ControlToolsPageLayout_ZoomPageToLastExtentForward.Glyph");
            this.ControlToolsPageLayout_ZoomPageToLastExtentForward.Hint = "下一页面";
            this.ControlToolsPageLayout_ZoomPageToLastExtentForward.Id = 0x53;
            this.ControlToolsPageLayout_ZoomPageToLastExtentForward.Name = "ControlToolsPageLayout_ZoomPageToLastExtentForward";
            this.bar1.BarName = "插入";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 2;
            this.bar1.DockStyle = BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.InsertTitle), new LinkPersistInfo(this.InsertText), new LinkPersistInfo(this.InsertNorthArrow), new LinkPersistInfo(this.InsertLegend), new LinkPersistInfo(this.InsertScaleBar), new LinkPersistInfo(this.InsertScaleText), new LinkPersistInfo(this.ChangeLayout), new LinkPersistInfo(this.MapGridProperty), new LinkPersistInfo(this.InsertJTBElement) });
            this.bar1.OptionsBar.AllowDelete = true;
            this.bar1.Text = "插入";
            this.ChangeLayout.Caption = "改变布局";
            this.ChangeLayout.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.ChangeLayout.Glyph = (Image) resources.GetObject("ChangeLayout.Glyph");
            this.ChangeLayout.Id = 0xba;
            this.ChangeLayout.Name = "ChangeLayout";
            this.MapGridProperty.Caption = "格网属性";
            this.MapGridProperty.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.MapGridProperty.Id = 0xbb;
            this.MapGridProperty.Name = "MapGridProperty";
            this.InsertJTBElement.Caption = "插入接图表";
            this.InsertJTBElement.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertJTBElement.Id = 0xbc;
            this.InsertJTBElement.Name = "InsertJTBElement";
            this.bar3.BarName = "标注";
            this.bar3.DockCol = 1;
            this.bar3.DockRow = 2;
            this.bar3.DockStyle = BarDockStyle.Top;
            this.bar3.FloatLocation = new System.Drawing.Point(0x159, 0xe0);
            this.bar3.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.LabelManager), new LinkPersistInfo(this.UnPlacedLabels), new LinkPersistInfo(this.UnLockLabels), new LinkPersistInfo(this.barEditItem1) });
            this.bar3.Offset = 250;
            this.bar3.Text = "标注";
            this.LabelManager.Caption = "标注管理器";
            this.LabelManager.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.LabelManager.Glyph = (Image) resources.GetObject("LabelManager.Glyph");
            this.LabelManager.Id = 0xb7;
            this.LabelManager.Name = "LabelManager";
            this.UnPlacedLabels.Caption = "显示未放置标注";
            this.UnPlacedLabels.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.UnPlacedLabels.Glyph = (Image) resources.GetObject("UnPlacedLabels.Glyph");
            this.UnPlacedLabels.Id = 0xb8;
            this.UnPlacedLabels.Name = "UnPlacedLabels";
            this.UnLockLabels.Caption = "锁定标注";
            this.UnLockLabels.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.UnLockLabels.Glyph = (Image) resources.GetObject("UnLockLabels.Glyph");
            this.UnLockLabels.Id = 0xb9;
            this.UnLockLabels.Name = "UnLockLabels";
            this.barAndDockingController1.PaintStyleName = "Office2003";
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            this.imageList1.Images.SetKeyName(12, "");
            this.imageList1.Images.SetKeyName(13, "");
            this.imageList1.Images.SetKeyName(14, "");
            this.imageList1.Images.SetKeyName(15, "");
            this.imageList1.Images.SetKeyName(0x10, "");
            this.imageList1.Images.SetKeyName(0x11, "");
            this.imageList1.Images.SetKeyName(0x12, "");
            this.imageList1.Images.SetKeyName(0x13, "");
            this.imageList1.Images.SetKeyName(20, "");
            this.imageList1.Images.SetKeyName(0x15, "");
            this.imageList1.Images.SetKeyName(0x16, "");
            this.imageList1.Images.SetKeyName(0x17, "");
            this.imageList1.Images.SetKeyName(0x18, "");
            this.imageList1.Images.SetKeyName(0x19, "");
            this.imageList1.Images.SetKeyName(0x1a, "");
            this.imageList1.Images.SetKeyName(0x1b, "");
            this.imageList1.Images.SetKeyName(0x1c, "");
            this.imageList1.Images.SetKeyName(0x1d, "");
            this.imageList1.Images.SetKeyName(30, "");
            this.ControlToolsGraphicElement_NewCircle.Caption = "新建圆";
            this.ControlToolsGraphicElement_NewCircle.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_NewCircle.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_NewCircle.Glyph");
            this.ControlToolsGraphicElement_NewCircle.Hint = "新建圆";
            this.ControlToolsGraphicElement_NewCircle.Id = 0x3d;
            this.ControlToolsGraphicElement_NewCircle.Name = "ControlToolsGraphicElement_NewCircle";
            this.ControlToolsGraphicElement_NewCurve.Caption = "新建弧形";
            this.ControlToolsGraphicElement_NewCurve.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_NewCurve.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_NewCurve.Glyph");
            this.ControlToolsGraphicElement_NewCurve.Id = 0x3e;
            this.ControlToolsGraphicElement_NewCurve.Name = "ControlToolsGraphicElement_NewCurve";
            this.ControlToolsGraphicElement_NewEllipse.Caption = "新建椭圆";
            this.ControlToolsGraphicElement_NewEllipse.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_NewEllipse.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_NewEllipse.Glyph");
            this.ControlToolsGraphicElement_NewEllipse.Id = 0x3f;
            this.ControlToolsGraphicElement_NewEllipse.Name = "ControlToolsGraphicElement_NewEllipse";
            this.ControlToolsGraphicElement_NewFrame.Caption = "新建框架";
            this.ControlToolsGraphicElement_NewFrame.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_NewFrame.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_NewFrame.Glyph");
            this.ControlToolsGraphicElement_NewFrame.Id = 0x40;
            this.ControlToolsGraphicElement_NewFrame.Name = "ControlToolsGraphicElement_NewFrame";
            this.ControlToolsGraphicElement_NewFreeHand.Caption = "新建自由曲线";
            this.ControlToolsGraphicElement_NewFreeHand.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_NewFreeHand.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_NewFreeHand.Glyph");
            this.ControlToolsGraphicElement_NewFreeHand.Id = 0x41;
            this.ControlToolsGraphicElement_NewFreeHand.Name = "ControlToolsGraphicElement_NewFreeHand";
            this.ControlToolsGraphicElement_NewLine.Caption = "新建线";
            this.ControlToolsGraphicElement_NewLine.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_NewLine.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_NewLine.Glyph");
            this.ControlToolsGraphicElement_NewLine.Id = 0x42;
            this.ControlToolsGraphicElement_NewLine.Name = "ControlToolsGraphicElement_NewLine";
            this.ControlToolsGraphicElement_NewPolygon.Caption = "新建多边形";
            this.ControlToolsGraphicElement_NewPolygon.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_NewPolygon.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_NewPolygon.Glyph");
            this.ControlToolsGraphicElement_NewPolygon.Id = 0x43;
            this.ControlToolsGraphicElement_NewPolygon.Name = "ControlToolsGraphicElement_NewPolygon";
            this.ControlToolsGraphicElement_NewRectangle.Caption = "新建矩形";
            this.ControlToolsGraphicElement_NewRectangle.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_NewRectangle.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_NewRectangle.Glyph");
            this.ControlToolsGraphicElement_NewRectangle.Id = 0x44;
            this.ControlToolsGraphicElement_NewRectangle.Name = "ControlToolsGraphicElement_NewRectangle";
            this.ControlToolsPageLayout_NewMap.Caption = "数据框";
            this.ControlToolsPageLayout_NewMap.CategoryGuid = new Guid("b079cdb4-d010-4ad5-ac66-0ffb90e230d6");
            this.ControlToolsPageLayout_NewMap.Glyph = (Image) resources.GetObject("ControlToolsPageLayout_NewMap.Glyph");
            this.ControlToolsPageLayout_NewMap.Hint = "数据框";
            this.ControlToolsPageLayout_NewMap.Id = 0x45;
            this.ControlToolsPageLayout_NewMap.Name = "ControlToolsPageLayout_NewMap";
            this.ControlToolsMapNavigation_Rotate.Caption = "旋转数据框";
            this.ControlToolsMapNavigation_Rotate.CategoryGuid = new Guid("e5f39d20-2be7-4874-86cf-bc2c58756ba9");
            this.ControlToolsMapNavigation_Rotate.Glyph = (Image) resources.GetObject("ControlToolsMapNavigation_Rotate.Glyph");
            this.ControlToolsMapNavigation_Rotate.Id = 0x4b;
            this.ControlToolsMapNavigation_Rotate.Name = "ControlToolsMapNavigation_Rotate";
            this.ControlToolsGraphicElement_RotateElement.Caption = "旋转";
            this.ControlToolsGraphicElement_RotateElement.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_RotateElement.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_RotateElement.Glyph");
            this.ControlToolsGraphicElement_RotateElement.Id = 0x56;
            this.ControlToolsGraphicElement_RotateElement.Name = "ControlToolsGraphicElement_RotateElement";
            this.ControlToolsGraphicElement_RotateLeft.Caption = "向左旋转";
            this.ControlToolsGraphicElement_RotateLeft.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_RotateLeft.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_RotateLeft.Glyph");
            this.ControlToolsGraphicElement_RotateLeft.Id = 0x57;
            this.ControlToolsGraphicElement_RotateLeft.Name = "ControlToolsGraphicElement_RotateLeft";
            this.ControlToolsGraphicElement_RotateRight.Caption = "向右旋转";
            this.ControlToolsGraphicElement_RotateRight.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_RotateRight.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_RotateRight.Glyph");
            this.ControlToolsGraphicElement_RotateRight.Id = 0x58;
            this.ControlToolsGraphicElement_RotateRight.Name = "ControlToolsGraphicElement_RotateRight";
            this.ControlToolsGraphicElement_Group.Caption = "组合";
            this.ControlToolsGraphicElement_Group.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_Group.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_Group.Glyph");
            this.ControlToolsGraphicElement_Group.Hint = "组合";
            this.ControlToolsGraphicElement_Group.Id = 0x61;
            this.ControlToolsGraphicElement_Group.Name = "ControlToolsGraphicElement_Group";
            this.ControlToolsGraphicElement_Ungroup.Caption = "取消组合";
            this.ControlToolsGraphicElement_Ungroup.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_Ungroup.Glyph = (Image) resources.GetObject("ControlToolsGraphicElement_Ungroup.Glyph");
            this.ControlToolsGraphicElement_Ungroup.Hint = "取消组合";
            this.ControlToolsGraphicElement_Ungroup.Id = 0x62;
            this.ControlToolsGraphicElement_Ungroup.Name = "ControlToolsGraphicElement_Ungroup";
            this.ControlToolsMapNavigation_ClearMapRotation.Caption = "撤销旋转";
            this.ControlToolsMapNavigation_ClearMapRotation.CategoryGuid = new Guid("e5f39d20-2be7-4874-86cf-bc2c58756ba9");
            this.ControlToolsMapNavigation_ClearMapRotation.Glyph = (Image) resources.GetObject("ControlToolsMapNavigation_ClearMapRotation.Glyph");
            this.ControlToolsMapNavigation_ClearMapRotation.Id = 0x63;
            this.ControlToolsMapNavigation_ClearMapRotation.Name = "ControlToolsMapNavigation_ClearMapRotation";
            this.InsertMapFrame.Caption = "数据框";
            this.InsertMapFrame.CategoryGuid = new Guid("a8802271-b0c8-4c17-9504-fd8dec82a088");
            this.InsertMapFrame.Glyph = (Image) resources.GetObject("InsertMapFrame.Glyph");
            this.InsertMapFrame.Id = 0x79;
            this.InsertMapFrame.Name = "InsertMapFrame";
            this.ControlToolsGraphicElement_SelectTool.Caption = "元素选择";
            this.ControlToolsGraphicElement_SelectTool.CategoryGuid = new Guid("d44ec61a-893b-47aa-9704-5a856aa3d08a");
            this.ControlToolsGraphicElement_SelectTool.Id = 0xae;
            this.ControlToolsGraphicElement_SelectTool.Name = "ControlToolsGraphicElement_SelectTool";
            this.axPageLayoutControl1.Dock = DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(0, 0x4b);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = (AxHost.State) resources.GetObject("axPageLayoutControl1.OcxState");
            this.axPageLayoutControl1.Size = new Size(0x2ba, 0x1d7);
            this.axPageLayoutControl1.TabIndex = 0x1f;
            this.barEditItem1.Caption = "barEditItem1";
            this.barEditItem1.Edit = this.repositoryItemComboBox2;
            this.barEditItem1.Id = 190;
            this.barEditItem1.Name = "barEditItem1";
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x2ba, 0x237);
            base.Controls.Add(this.axPageLayoutControl1);
            base.Controls.Add(this.barDockControl3);
            base.Controls.Add(this.barDockControl4);
            base.Controls.Add(this.barDockControl2);
            base.Controls.Add(this.barDockControl1);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "FormMapPrinter";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.Form1_Load);
            base.Closing += new CancelEventHandler(this.Form1_Closing);
            this.barManager1.EndInit();
            this.repositoryItemComboBox1.EndInit();
            this.barAndDockingController1.EndInit();
            this.axPageLayoutControl1.EndInit();
            this.repositoryItemComboBox2.EndInit();
            base.ResumeLayout(false);
        }

        public void LoadTools(IApplication pApp, string FileName)
        {
            LoadComponent component = new LoadComponent();
            ComponentList list = new ComponentList(FileName);
            list.beginRead();
            string str = "";
            string str2 = "";
            int subType = -1;
            for (int i = 0; i < list.GetComponentCount(); i++)
            {
                str2 = list.getClassName(i);
                str = Application.StartupPath + @"\" + list.getfilename(i);
                subType = list.getSubType(i);
                bool flag = component.LoadComponentLibrary(str);
                try
                {
                    ICommand pCommand = null;
                    pCommand = component.LoadClass(str2) as ICommand;
                    if (pCommand == null)
                    {
                       Logger.Current.Error("", null, "无法创建:" + str2);
                    }
                    else
                    {
                        pCommand.OnCreate(pApp);
                        if (subType != -1)
                        {
                            (pCommand as ICommandSubType).SetSubType(subType);
                        }
                        BarItem item = this.barManager1.Items[pCommand.Name];
                        if (item == null)
                        {
                            item = this.CreateBarItem(pCommand);
                        }
                        if ((pCommand is IBarEditItem) && (item is BarEditItem))
                        {
                            (pCommand as IBarEditItem).BarEditItem = new JLKComboxBarItem(item);
                        }
                        if (item != null)
                        {
                            item.Tag = pCommand;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CErrorLog.writeErrorLog(null, exception, "");
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                DocumentManager.UnRegister(this.axPageLayoutControl1.Object);
            }
            catch
            {
            }
            base.OnClosing(e);
        }

        protected override void OnNotifyMessage(Message m)
        {
            base.OnNotifyMessage(m);
            if (m.Msg == 0x232)
            {
                this.axPageLayoutControl1.SuppressResizeDrawing(false, 0);
            }
            else if (m.Msg == 0x231)
            {
                this.axPageLayoutControl1.SuppressResizeDrawing(true, 0);
            }
        }

        private void ReSetCurrentTool()
        {
            ((IPageLayoutControl2) this.axPageLayoutControl1.Object).CurrentTool = null;
        }

        private void SetCommand(ICommand pCommand)
        {
            BarItem item = this.barManager1.Items[pCommand.Name];
            if ((pCommand is IBarEditItem) && (item is BarEditItem))
            {
                (pCommand as IBarEditItem).BarEditItem = item;
            }
            if (item != null)
            {
                item.Tag = pCommand;
            }
        }

        private void SetCurrentTool(ITool pTool)
        {
            ((IPageLayoutControl2) this.axPageLayoutControl1.Object).CurrentTool = pTool;
        }

        private void SetHook(object hook)
        {
            for (int i = 0; i < this.m_esriCommandList.Count; i++)
            {
                (this.m_esriCommandList[i] as ICommand).OnCreate(hook);
            }
        }

        private void StyleManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmStyleManagerDialog dialog = new frmStyleManagerDialog();
            dialog.SetStyleGallery(ApplicationBase.StyleGallery);
            dialog.ShowDialog();
        }

        public void UpdataUI()
        {
            ICommand command = null;
            BarItem item = null;
            ITool currentTool = this.GetCurrentTool();
            this.UpdateUI(currentTool);
            for (int i = 0; i < this.m_esriCommandList.Count; i++)
            {
                command = this.m_esriCommandList[i] as ICommand;
                item = this.barManager1.Items[command.Name];
                if (item != null)
                {
                    item.Enabled = command.Enabled;
                    BarButtonItem item2 = item as BarButtonItem;
                    if (item2 != null)
                    {
                        if (currentTool == command)
                        {
                            item2.ButtonStyle = BarButtonStyle.Check;
                            item2.Down = true;
                        }
                        else
                        {
                            item2.ButtonStyle = BarButtonStyle.Default;
                            item2.Down = false;
                        }
                    }
                }
            }
        }

        public bool UpdateUI(ITool pCurrentTool)
        {
            if (this.barManager1 != null)
            {
                ICommand tag = null;
                string name = "";
                if (pCurrentTool != null)
                {
                    name = (pCurrentTool as ICommand).Name;
                }
                for (int i = 0; i < this.barManager1.Items.Count; i++)
                {
                    BarItem item = this.barManager1.Items[i];
                    if (item.Tag != null)
                    {
                        tag = item.Tag as ICommand;
                    }
                    else
                    {
                        tag = this.FindCommand(item.Name);
                    }
                    if (tag != null)
                    {
                        item.Enabled = tag.Enabled;
                        BarButtonItem item2 = item as BarButtonItem;
                        if (item2 != null)
                        {
                            if (tag.Name == name)
                            {
                                item2.ButtonStyle = BarButtonStyle.Check;
                                item2.Down = true;
                            }
                            else if (tag.Checked)
                            {
                                item2.ButtonStyle = BarButtonStyle.Check;
                                item2.Down = true;
                            }
                            else
                            {
                                item2.ButtonStyle = BarButtonStyle.Default;
                                item2.Down = false;
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public IGeometry ClipGeometry
        {
            set
            {
                this.m_pClipGeometry = value;
            }
        }

        public IMap SourcesMap
        {
            set
            {
                this.m_pSourcesMap = value;
            }
        }

        public IPageLayout SourcesPgeLayout
        {
            set
            {
                this.m_pSourcesPageLayout = value;
                this.m_pSourcesMap = (this.m_pSourcesPageLayout as IActiveView).FocusMap;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                ApplicationBase.StyleGallery = value;
            }
        }
    }
}

