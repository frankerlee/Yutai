using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Output;
using Yutai.ArcGIS.Carto.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdPageSetup : YutaiCommand
    {
        private PrintingPlugin _plugin;
        private IPageLayoutControl2 _pageLayoutControl;
        public override bool Enabled
        {
            get
            {
                bool result;
                if (this._context.FocusMap == null)
                {
                    result = false;
                }
                else
                {
                    if (this._context.MainView.ControlType== GISControlType.PageLayout)
                    {
                        this._pageLayoutControl = _context.MainView.PageLayoutControl;
                       
                        return true;
                    }
                    result = false;
                }
                return result;
            }
        }
        public CmdPageSetup(IAppContext context, BasePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as PrintingPlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_page_setup;
            this.m_caption = "页面设置";
            this.m_category = "Layout";
            this.m_message = "页面设置";
            this.m_name = "Layout_PageSetup";
            this._key = "Layout_PageSetup";
            this.m_toolTip = "页面设置";
            _context = hook as IAppContext;
            this._itemType = RibbonItemType.Button;
            ;
            _needUpdateEvent = true;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
            frmPageAndPrinterSetup frmPageAndPrinterSetup = new frmPageAndPrinterSetup();
            if (this._context.ActiveView is IPageLayout)
            {
                frmPageAndPrinterSetup.setPageLayout(this._context.ActiveView as IPageLayout);
            }
            else
            {
                IPrinter printer = new EmfPrinter() as IPrinter;
                System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                printer.Paper = new Paper
                {
                    PrinterName = printDocument.PrinterSettings.PrinterName
                };
                frmPageAndPrinterSetup.setPrinter(printer);
            }
            frmPageAndPrinterSetup.ShowDialog();
        }
    }
}