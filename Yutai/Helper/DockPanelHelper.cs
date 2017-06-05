using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Properties;
using Yutai.Services.Serialization;
using Yutai.UI.Docking;

namespace Yutai.Helper
{
    internal static class DockPanelHelper
    {
        private const int PanelSize = 300;

        public static void InitDocking(this ISecureContext context)
        {
            var panels = context.DockPanels;
            panels.Lock();

            InitMapLegend(context);
            InitOverview(context);

            /* InitLocator(context);

             InitToolbox(context);

             InitTasks(context);*/

            context.DockPanels.MapLegend.TabPosition = 0;
            panels.Unlock();
        }

        /*private static void InitTasks(ISecureContext context)
        {
            var presenter = context.Container.Resolve<TasksPresenter>();
            var tasks = context.DockPanels.Add(presenter.View, DockPanelKeys.Tasks, PluginIdentity.Default);
            tasks.Caption = "Tasks";
            var toolbox = context.DockPanels.Toolbox;
            tasks.DockTo(toolbox, DockPanelState.Tabbed, PanelSize);
            tasks.SetIcon(Resources.ico_tasks);
            tasks.TabPosition = toolbox.TabPosition;
        }*/

        public static void SetViewStyle(this ISecureContext context, MapViewStyle viewStyle)
        {
            //TabSplitterContainer tabContainer = context.MainView.MapContainer as TabSplitterContainer;
            //if (viewStyle == MapViewStyle.View2D)
            //{
            //    tabContainer.PrimaryPages[0].Hide = false;
            //    tabContainer.SecondaryPages[0].Hide = true;
            //}
            //if (viewStyle == MapViewStyle.View3D)
            //{
            //    tabContainer.SecondaryPages[0].Hide = false;
            //    tabContainer.PrimaryPages[0].Hide = true;
            //}
            //if (viewStyle == MapViewStyle.ViewAll)
            //{
            //    tabContainer.SecondaryPages[0].Hide = false;
            //    tabContainer.PrimaryPages[0].Hide = false;
            //}
            //tabContainer.Update();
        }
      private static void InitMapLegend(ISecureContext context)
        {
            var legendControl = context.GetDockPanelObject(DefaultDockPanel.MapLegend);
            var legend = context.DockPanels.Add(legendControl, DockPanelKeys.MapLegend, PluginIdentity.Default);
            legend.Caption = "二维图例";
            legend.TabPosition = 1;
            legend.DockTo(null, DockPanelState.Left, PanelSize);
            legend.SetIcon(Resources.ico_legend);
        }

        private static void InitOverview(ISecureContext context)
        {
            var overviewControl = context.GetDockPanelObject(DefaultDockPanel.Overview);
            var overview = context.DockPanels.Add(overviewControl, DockPanelKeys.Overview, PluginIdentity.Default);
            overview.Caption = "鹰眼视图";
            overview.TabPosition = 2;
            overview.DockTo(context.DockPanels.MapLegend, DockPanelState.Bottom, PanelSize);
            overview.SetIcon(Resources.ico_tasks);
        }

        /* private static void InitToolbox(ISecureContext context)
         {
             var toolboxControl = context.GetDockPanelObject(DefaultDockPanel.Toolbox);

             var toolbox = context.DockPanels.Add(toolboxControl, DockPanelKeys.Toolbox, PluginIdentity.Default);
             toolbox.Caption = "Toolbox";
             toolbox.DockTo(null, DockPanelState.Right, PanelSize);
             toolbox.SetIcon(Resources.ico_toolbox24);
         }*/

        /* private static void InitLocator(ISecureContext context)
         {
             var locatorControl = context.GetDockPanelObject(DefaultDockPanel.Locator);
             if (locatorControl == null)
             {
                 return;
             }

             var locator = context.DockPanels.Add(locatorControl, DockPanelKeys.Preview, PluginIdentity.Default);
             locator.Caption = "Overview";
             locator.SetIcon(Resources.ico_zoom_to_layer);
             locator.DockTo(context.DockPanels.Legend, DockPanelState.Bottom, PanelSize);

             var size = locator.Size;
             locator.Size = new Size(size.Width, 250);
         }*/

        public static void ClosePanel(IAppContext context, string dockPanelKey)
        {
            var panel = context.DockPanels.Find(dockPanelKey);
            if (panel != null)
            {
                panel.Visible = false;
            }
        }

        public static void ShowPanel(IAppContext context, string dockPanelKey)
        {
            var panel = context.DockPanels.Find(dockPanelKey);
            if (panel != null)
            {
                panel.Visible = true;
                panel.Activate();
                panel.Focus();
            }
        }

        public static void SerializeDockState(IAppContext context)
        {
            var panels = context.DockPanels;
            panels.Lock();

            foreach (var panel in panels)
            {
                Debug.Print(panel.Caption);
                Debug.Print("Hidden: " + panel.AutoHidden);
                Debug.Print("Visible: " + panel.Visible);
                Debug.Print("Style: " + panel.DockState);

                //bool hidden = panel.Hidden;
                //if (hidden)
                //{
                //    panel.Hidden = false;
                //}

                //bool visible = panel.Visible;
                //if (!visible)
                //{
                //    panel.Visible = true;
                //}

                var host = panel.Control.Parent as DockHost;
                if (host != null)
                {


                    var dhc = host.InternalController as DockHostController;
                    if (dhc != null)
                    {
                        DockInfo di = dhc.GetSerCurrentDI();
                        if (di != null)
                        {

                            Rectangle r;

                            if (dhc.bInAutoHide)
                            {
                                r = dhc.DINew.rcDockArea;
                            }
                            else
                            {
                                r = dhc.LayoutRect;
                            }

                            Debug.Print("Child host count: " + dhc.ChildHostCount);

                            Debug.Print("Controller name: " + di.ControlleName);
                            Debug.Print("Style: " + di.dStyle);
                            Debug.Print("x: {0}; y: {1}; w: {2}; h: {3}", r.X, r.Y, r.Width, r.Height);
                            //Debug.Print("x: {0}; y: {1}; w: {2}; h: {3}", r2.X, r2.Y, r2.Width, r2.Height);
                            Debug.Print("Priority: " + di.nPriority);
                            Debug.Print("DockIndex: " + di.nDockIndex);
                        }
                    }
                }

                //if (!visible)
                //{
                //    panel.Visible = false;
                //}

                //if (hidden)
                //{
                //    panel.Hidden = true;
                //}

                Debug.Print("--------------");
            }

            panels.Unlock();
        }
    }
}
