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
    public partial class FormMapPrinter : Form
    {
        public AxPageLayoutControl axPageLayoutControl1;
        private IList m_CommandList = new ArrayList();
        private IList m_esriCommandList = new ArrayList();
        private IApplication m_pApp = new ApplicationBase();
        private PaintStyleMenuItem m_pApplicationStyle = new PaintStyleMenuItem();
        private IGeometry m_pClipGeometry = null;
        private IMap m_pSourcesMap = null;
        private IPageLayout m_pSourcesPageLayout = null;
        private const int WM_ENTERSIZEMOVE = 561;
        private const int WM_EXITSIZEMOVE = 562;

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
            this.fullPageLayerOut(this.axPageLayoutControl1.ActiveView.FocusMap,
                (this.m_pSourcesMap as IActiveView).Extent);
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
            if (m.Msg == 562)
            {
                this.axPageLayoutControl1.SuppressResizeDrawing(false, 0);
            }
            else if (m.Msg == 561)
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
            set { this.m_pClipGeometry = value; }
        }

        public IMap SourcesMap
        {
            set { this.m_pSourcesMap = value; }
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
            set { ApplicationBase.StyleGallery = value; }
        }
    }
}