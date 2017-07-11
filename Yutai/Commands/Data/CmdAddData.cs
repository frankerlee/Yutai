using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Controls.Controls.Export;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Forms;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;
using Point = System.Drawing.Point;

namespace Yutai.Commands.Data
{
    public class CmdAddData : YutaiCommand
    {
        private AddDataHelper pHelper = null;

        private IList ilist_0 = null;
        public override bool Enabled
        {
            get
            {
                return this._context.FocusMap != null;
            }
        }
        public CmdAddData(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {

           
            frmOpenFile _frmOpenFile = new frmOpenFile()
            {
                Text = "添加数据",
                AllowMultiSelect = true
            };
            _frmOpenFile.AddFilter(new MyGxFilterDatasets(), true);
            if (_frmOpenFile.DoModalOpen() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.pHelper = new AddDataHelper(this.GetMap() as IActiveView);
                this.ilist_0 = _frmOpenFile.SelectedItems;
                this.pHelper.m_pApp = _context;
                this.pHelper.LoadData(this.ilist_0);
                
                Cursor.Current = Cursors.Default;
            }

        }


        private void method_2()
        {
            Cursor.Current = Cursors.Default;
            (this.GetMap() as IActiveView).ScreenDisplay.UpdateWindow();
        }

      


        private IMap GetMap()
        {
            return this._context.FocusMap;
        }

        private void method_1()
        {
            this.pHelper.InvokeMethod(this.ilist_0);
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "添加数据";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_add_data;
            base.m_name = "File_AddData";
            base._key = "File_AddData";
            base.m_toolTip = "添加数据";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }

    public class CmdPrint : YutaiCommand
    {
     
        public override bool Enabled
        {
            get
            {
                return this._context.FocusMap != null;
            }
        }
        public CmdPrint(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            CMapPrinter cMapPrinter;
            cMapPrinter = new CMapPrinter(this._context.FocusMap);
            cMapPrinter.showPrintUI("打印地图");

        }
      
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "打印";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_print;
            base.m_name = "File_Print";
            base._key = "File_Print";
            base.m_toolTip = "打印";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }

    public class CmdPrintPageSetup : YutaiCommand
    {
        private IPageLayoutControl ipageLayoutControl_0 = null;
        public override bool Enabled
        {
            get
            {
                bool flag;
             
                 
                        if (this._context.MainView.ControlType == GISControlType.PageLayout)
                        {
                            this.ipageLayoutControl_0 = this._context.MainView.PageLayoutControl as IPageLayoutControl;
                            flag = true;
                            return flag;
                        }
                 
                flag = false;
                return flag;
            }
        }
        public CmdPrintPageSetup(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
             this.ipageLayoutControl_0 = this._context.Hook as IPageLayoutControl;
            if (this.ipageLayoutControl_0 != null)
            {
                try
                {
                    FormPrinterSetup formPrinterSetup = new FormPrinterSetup();
                    formPrinterSetup.setPageLayout(ref this.ipageLayoutControl_0);
                    formPrinterSetup.ShowDialog();
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    MessageBox.Show("无法启动打印!");
                    CErrorLog.writeErrorLog(this, exception, "");
                }
            }

        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "打印设置";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_pagesetup;
            base.m_name = "File_PrintPageSetup";
            base._key = "File_PrintPageSetup";
            base.m_toolTip = "打印设置";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }

    public class CmdPrintPreview : YutaiCommand
    {
        private PrintDocument printDocument_0 = new PrintDocument();

        internal PrintDialog printDialog1;

        internal PageSetupDialog pageSetupDialog1;

        private ITrackCancel itrackCancel_0 = new CancelTrackerClass();

        internal PrintPreviewDialog printPreviewDialog1;

        private short short_0 = 0;

        public override bool Enabled
        {
            get
            {
                return this._context.FocusMap != null;
            }
        }
        public CmdPrintPreview(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            if (Editor.EditWorkspace != null && this._context.FocusMap.SelectionCount > 0)
            {
                this._context.FocusMap.ClearSelection();
            }
            this.short_0 = 0;
            this.printDocument_0.DocumentName = "打印文档";
            this.printPreviewDialog1.Document = this.printDocument_0;
            this.printPreviewDialog1.ShowDialog();

        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "打印预览";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_preview;
            base.m_name = "File_PrintPreview";
            base._key = "File_PrintPreview";
            base.m_toolTip = "打印预览";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            this.method_0();
            this.printDialog1 = new PrintDialog();
            this.method_1();
        }

        private void method_0()
        {
            this.printPreviewDialog1 = new PrintPreviewDialog()
            {
                ClientSize = new Size(800, 600),
                Location = new Point(29, 29),
                Name = "PrintPreviewDialog1",
                MinimumSize = new Size(375, 250),
                UseAntiAlias = true
            };
            this.printDocument_0.PrintPage += new PrintPageEventHandler(this.printDocument_0_PrintPage);
        }

        private void method_1()
        {
            this.pageSetupDialog1 = new PageSetupDialog()
            {
                PageSettings = new PageSettings(),
                PrinterSettings = new PrinterSettings(),
                ShowNetwork = false
            };
        }

        private void printDocument_0_PrintPage(object sender, PrintPageEventArgs e)
        {
            short dpiX;
            IEnvelope envelopeClass;
            short num;
            double xMin;
            double yMin;
            double xMax;
            double yMax;
            tagRECT deviceFrame = new tagRECT();
            IEnvelope visibleBounds;
            IntPtr hdc;
            WKSEnvelope wKSEnvelope;
            WKSEnvelope wKSEnvelope1;
            if (this._context.ActiveView is IPageLayout)
            {
                IPageLayout2 activeView = this._context.ActiveView as IPageLayout2;
                dpiX = (short)e.Graphics.DpiX;
                envelopeClass = new EnvelopeClass();
                IPage page = activeView.Page;
                activeView.Page.PrinterPageCount(activeView.Printer, 0, out num);
                CmdPrintPreview short0 = this;
                short0.short_0 = (short)(short0.short_0 + 1);
                IPrinter printer = activeView.Printer;
                page.GetDeviceBounds(printer, this.short_0, 0, dpiX, envelopeClass);
                envelopeClass.QueryCoords(out xMin, out yMin, out xMax, out yMax);
                deviceFrame.bottom = (int)yMax;
                deviceFrame.left = (int)xMin;
                deviceFrame.top = (int)yMin;
                deviceFrame.right = (int)xMax;
                visibleBounds = new EnvelopeClass();
                page.GetPageBounds(printer, this.short_0, 0, visibleBounds);
                hdc = e.Graphics.GetHdc();
                (activeView as IActiveView).Output(hdc.ToInt32(), dpiX, ref deviceFrame, visibleBounds, this.itrackCancel_0);
                e.Graphics.ReleaseHdc(hdc);
                if (this.short_0 >= num)
                {
                    e.HasMorePages = false;
                    this.short_0 = 0;
                }
                else
                {
                    e.HasMorePages = true;
                }
            }
            else if (this._context.ActiveView is IMap)
            {
                dpiX = (short)e.Graphics.DpiX;
                envelopeClass = new EnvelopeClass();
                deviceFrame = this._context.ActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                IEnvelope clipEnvelope = this._context.ActiveView.ScreenDisplay.ClipEnvelope;
                visibleBounds = this._context.ActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds;
                visibleBounds.QueryCoords(out xMin, out yMin, out xMax, out yMax);
                hdc = e.Graphics.GetHdc();
                IPrinter emfPrinterClass = new EmfPrinterClass();
                PrintDocument printDocument = new PrintDocument();
                IPaper paperClass = new PaperClass()
                {
                    PrinterName = printDocument.PrinterSettings.PrinterName
                };
                emfPrinterClass.Paper = paperClass;
                emfPrinterClass.PrintableBounds.QueryWKSCoords(out wKSEnvelope);
                emfPrinterClass.PrintableBounds.QueryWKSCoords(out wKSEnvelope1);
                xMin = wKSEnvelope1.XMin * (double)dpiX;
                xMax = wKSEnvelope1.XMax * (double)dpiX;
                yMin = wKSEnvelope1.YMin * (double)dpiX;
                yMax = wKSEnvelope1.YMax * (double)dpiX;
                deviceFrame.left = (int)Math.Round(xMin);
                deviceFrame.top = (int)Math.Round(yMin);
                deviceFrame.right = (int)Math.Round(xMax);
                deviceFrame.bottom = (int)Math.Round(yMax);
                try
                {
                    this._context.ActiveView.Output(hdc.ToInt32(), dpiX, ref deviceFrame, null, this.itrackCancel_0);
                }
                catch
                {
                }
                e.Graphics.ReleaseHdc(hdc);
            }
        }
    
}


    public class CmdExportMap : YutaiCommand
    {

        public override bool Enabled
        {
            get
            {
                return this._context.FocusMap != null;
            }
        }
        public CmdExportMap(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            try
            {
                frmExportMap _frmExportMap = new frmExportMap()
                {
                    ActiveView = this._context.ActiveView
                };
                _frmExportMap.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "导出地图";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_exportmap;
            base.m_name = "File_ExportMap";
            base._key = "File_ExportMap";
            base.m_toolTip = "导出地图";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }


    public class CmdExit : YutaiCommand
    {

        
        public CmdExit(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            _context.Close();

        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "退出系统";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_exit;
            base.m_name = "File_ExitSys";
            base._key = "File_ExitSys";
            base.m_toolTip = "退出系统";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}
