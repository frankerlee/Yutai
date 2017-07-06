using System;
using ESRI.ArcGIS.Controls;
using Yutai.ArcGIS.Carto.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdPrintSetup : YutaiCommand
    {
        private PrintingPlugin _plugin;
        private IPageLayoutControl _pageLayoutControl;
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
                    if (this._context.MainView.ControlType == GISControlType.PageLayout)
                    {
                        this._pageLayoutControl = _context.MainView.PageLayoutControl;

                        return true;
                    }
                    result = false;
                }
                return result;
            }
        }
        public CmdPrintSetup(IAppContext context, BasePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as PrintingPlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_clip_print;
            this.m_caption = "打印";
            this.m_category = "Layout";
            this.m_message = "打印";
            this.m_name = "Layout_Print";
            this._key = "Layout_Print";
            this.m_toolTip = "打印";
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
            try
            {
                this._pageLayoutControl = _context.MainView.PageLayoutControl;
                FormPrinterSetup formPrinterSetup = new FormPrinterSetup();
                formPrinterSetup.setPageLayout(ref this._pageLayoutControl);
                formPrinterSetup.ShowDialog();
            }
            catch (Exception exception_)
            {
                System.Windows.Forms.MessageBox.Show("无法启动打印!");
                //CErrorLog.writeErrorLog(this, exception_, "");
            }
        }
    }
}