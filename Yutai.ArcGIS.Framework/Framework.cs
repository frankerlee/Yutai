using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.COMSupport;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Framework;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;

namespace Yutai.ArcGIS.Framework
{
    public class Framework : IFramework
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private bool bool_2 = false;
        private Control control_0 = null;
        private Form form_0 = null;
        private IApplication iapplication_0;
        private IBarManager ibarManager_0 = null;
        private ICommandLine icommandLine_0 = null;
        private ICommandLine icommandLine_1 = null;
        private ICommandLineWindows icommandLineWindows_0 = null;
        private IDockManagerWrap idockManagerWrap_0 = null;
        private ILayer ilayer_0 = null;
        private IMapControl2 imapControl2_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private object object_0 = null;
        private object object_1 = null;
        private object object_2 = null;
        private object object_3 = null;
        private Thread thread_0 = null;
        private UpdateUIHelper updateUIHelper_0 = null;
        private IAppContext _appContext;

        public event OnActiveHookChangedHandler OnActiveHookChanged;

        public event OnDockWindowsEventHandler OnDockWindowsEvent;

        public event OnFrameworkClosedHandler OnFrameworkClosed;

        public event OnMapDocumentChangedEventHandler OnMapDocumentChangedEvent;

        public event OnMapDocumentSaveEventHandler OnMapDocumentSaveEvent;

        public Framework(IApplication iapplication_1)
        {
            if (iapplication_1 == null)
            {
                this.Application = null; // new ApplicationBase();
            }
            else
            {
                this.Application = iapplication_1;
            }
        }

        public void CancelCurrentTool()
        {
            if (this.icommandLine_1 != null)
            {
                if (this.icommandLine_1 != null)
                {
                    this.icommandLine_1.Cancel();
                }
                else if (this.icommandLine_1 is ITool)
                {
                    (this.icommandLine_1 as ITool).OnDblClick();
                }
            }
            bool flag = true;
            if (SketchToolAssist.CurrentTask != null)
            {
                flag = false;
            }
            if (flag)
            {
                ITool tool = this.method_12();
                if (tool is ICommandLine)
                {
                    (tool as ICommandLine).Cancel();
                }
                else if (tool == null)
                {
                }
            }
            this.method_10(null);
        }

        public void Excute(string string_0)
        {
            if (this.icommandLine_0 != null)
            {
                this.icommandLine_0.HandleCommandParameter(string_0);
            }
        }

        ~Framework()
        {
            this.iapplication_0 = null;
            AOUninitialize.Shutdown();
        }

        public void HandleCommand(ICommand icommand_0)
        {
            string str;
            try
            {
                if ((this.ibarManager_0 != null) && (icommand_0 is ITool))
                {
                    str = "";
                    if (icommand_0.Message != null)
                    {
                        str = "当前工具:" + icommand_0.Message;
                    }
                    if (str.Length == 0)
                    {
                        str = "当前工具:" + icommand_0.Caption;
                    }
                    this.ibarManager_0.Message(MSGTYPE.MTCurrentTool, str);
                }
                ITool tool = this.method_12();
                if ((tool == null) || ((tool as ICommand).Name != icommand_0.Name))
                {
                    if (this.icommandLineWindows_0 != null)
                    {
                        this.icommandLineWindows_0.LockCommandLine(false);
                    }
                    this.icommandLine_0 = icommand_0 as ICommandLine;
                    if (this.icommandLine_0 != null)
                    {
                        if (this.icommandLine_0 == this.icommandLine_1)
                        {
                            this.RestoreTool(this.icommandLine_1);
                            this.UpdateUI();
                            this.icommandLine_1 = null;
                            return;
                        }
                        this._appContext.ShowCommandString(this.icommandLine_0.CommandName, CommandTipsType.CTTCommandName);
                    }
                    if (icommand_0 is ITool)
                    {
                        if (icommand_0.Enabled)
                        {
                            if (this.icommandLine_0 != null)
                            {
                                if (this.icommandLine_0.CommandType == COMMANDTYPE.STATECOMMAND)
                                {
                                    this.CancelCurrentTool();
                                    this.icommandLine_1 = null;
                                }
                            }
                            else
                            {
                                this._appContext.ShowCommandString("", CommandTipsType.CTTEnd);
                            }
                            this.method_10(icommand_0 as ITool);
                        }
                        else
                        {
                            if (this.icommandLine_0 != null)
                            {
                                icommand_0.OnClick();
                            }
                            this.RestoreCurrentTool();
                        }
                    }
                    else
                    {
                        icommand_0.OnClick();
                        if (icommand_0 is ITask)
                        {
                            ICommand command = this.ibarManager_0.FindCommand((icommand_0 as ITask).DefaultTool);
                            if (command != null)
                            {
                                command.OnClick();
                                this.method_10(command as ITool);
                            }
                        }
                    }
                    this._appContext.UpdateUI();
                }
            }
            catch (Exception exception)
            {
                str = exception.ToString();
                Logger.Current.Error("", exception, "");
            }
        }

        public void HandleCommandLine(string string_0)
        {
            ICommand command = this.method_9(string_0);
            if (command == null)
            {
                if (this.icommandLineWindows_0 != null)
                {
                    this.icommandLineWindows_0.ShowCommandString("无法识别的命令", 5);
                    this.RestoreCurrentTool();
                }
            }
            else
            {
                ITool tool = this.method_12();
                if ((tool == null) || ((tool as ICommand).Name != command.Name))
                {
                    if (this.icommandLineWindows_0 != null)
                    {
                        this.icommandLineWindows_0.LockCommandLine(false);
                    }
                    this.icommandLine_0 = command as ICommandLine;
                    if (command is ITool)
                    {
                        if (command.Enabled)
                        {
                            if (this.icommandLine_0 != null)
                            {
                                if (this.icommandLine_0.CommandType == COMMANDTYPE.STATECOMMAND)
                                {
                                    this.CancelCurrentTool();
                                    this.icommandLine_1 = null;
                                }
                            }
                            else
                            {
                                this._appContext.ShowCommandString("", CommandTipsType.CTTEnd);
                            }
                            this.method_10(command as ITool);
                        }
                        else
                        {
                            if (this.icommandLine_0 != null)
                            {
                                command.OnClick();
                            }
                            this.RestoreCurrentTool();
                        }
                    }
                    else
                    {
                        command.OnClick();
                        this.RestoreCurrentTool();
                    }
                    this.UpdateUI();
                }
            }
        }

        public void Init()
        {
            this.bool_0 = true;
            if (this.ibarManager_0 != null)
            {
                this.ibarManager_0.Init();
                if (this.ibarManager_0 is IBarManagerEvents)
                {
                    (this.ibarManager_0 as IBarManagerEvents).OnItemClickEvent += new OnItemClickEventHandler(this.method_13);
                }
            }
            if (this.icommandLineWindows_0 != null)
            {
                this.icommandLineWindows_0.Init();
            }
            if (this.imapControl2_0 == null)
            {
            }
            if (this.object_2 != null)
            {
                if (this.object_2 is IPageLayoutControl)
                {
                    (this.object_2 as IPageLayoutControlEvents_Event).OnPageLayoutReplaced+=(new IPageLayoutControlEvents_OnPageLayoutReplacedEventHandler(this.method_7));
                }
            }
            else if (this.object_3 == null)
            {
            }
        //    (this.iapplication_0 as IApplicationEvents).OnMapDocumentChangedEvent += new OnMapDocumentChangedEventHandler(this.method_20);
        //    (this.iapplication_0 as IApplicationEvents).OnDockWindowsEvent += new OnDockWindowsEventHandler(this.method_21);
        //    (this.iapplication_0 as IApplicationEvents).OnUpdateUIEvent += new OnUpdateUIEventHandler(this.method_27);
        }

        public void LoadCommand(string string_0)
        {
        }

        private void method_0(int int_0, object object_4)
        {
            if (object_4 != null)
            {
                this.ibarManager_0.Message((MSGTYPE) int_0, object_4.ToString());
            }
        }

        private void method_1(object object_4)
        {
            if (object_4 is IDockContent)
            {
                if (this.idockManagerWrap_0 != null)
                {
                    this.idockManagerWrap_0.HideDockWindow(object_4 as IDockContent);
                }
            }
            else if (object_4 is Form)
            {
                (object_4 as Form).Hide();
            }
        }

        private void method_10(ITool itool_0)
        {
            if (this.object_3 is IMapControl2)
            {
                (this.object_3 as IMapControl2).CurrentTool = itool_0;
            }
            else if (this.object_3 is IPageLayoutControl2)
            {
                (this.object_3 as IPageLayoutControl2).CurrentTool = itool_0;
            }
            else if (this.object_3 is ISceneControlDefault)
            {
                try
                {
                    if (itool_0 != null)
                    {
                        (this.object_3 as ISceneControlDefault).CurrentTool = itool_0;
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (this.object_3 is IGlobeControlDefault)
            {
                (this.object_3 as IGlobeControlDefault).CurrentTool = itool_0;
            }
            else if (this.object_3 is MapAndPageLayoutControls)
            {
                if ((this.object_3 as MapAndPageLayoutControls).ActiveControl is IMapControl2)
                {
                    ((this.object_3 as MapAndPageLayoutControls).ActiveControl as IMapControl2).CurrentTool = itool_0;
                }
                else
                {
                    ((this.object_3 as MapAndPageLayoutControls).ActiveControl as IPageLayoutControl2).CurrentTool = itool_0;
                }
            }
            //else if (this.object_3 is MapAndPageLayoutControlsold)
            //{
            //    (this.object_3 as MapAndPageLayoutControlsold).CurrentTool = itool_0;
            //}
            if ((SketchToolAssist.CurrentTask != null) && (itool_0 != null))
            {
                SketchToolAssist.CurrentTask.CheckTaskStatue(itool_0);
            }
            this.iapplication_0.CurrentTool = null;//itool_0;
            if (this.control_0 != null)
            {
                if (itool_0 is IToolContextMenu)
                {
                    (itool_0 as IToolContextMenu).Init();
                    this.ibarManager_0.SetContextMenu(this.control_0);
                }
                else
                {
                    this.ibarManager_0.SetPopupContextMenu(this.control_0);
                }
            }
        }

        private void method_11()
        {
            if (this.object_3 is IMapControl2)
            {
                (this.object_3 as IMapControl2).CurrentTool = null;
            }
            else if (this.object_3 is IPageLayoutControl2)
            {
                (this.object_3 as IPageLayoutControl2).CurrentTool = null;
            }
            else if (this.object_3 is ISceneControlDefault)
            {
                (this.object_3 as ISceneControlDefault).CurrentTool = null;
            }
            else if (this.object_3 is IGlobeControlDefault)
            {
                (this.object_3 as IGlobeControlDefault).CurrentTool = null;
            }
            else if (this.object_3 is MapAndPageLayoutControls)
            {
                (this.object_3 as MapAndPageLayoutControls).CurrentTool = null;
            }
            this.iapplication_0.CurrentTool = null;
            if (this.control_0 != null)
            {
                this.ibarManager_0.SetPopupContextMenu(this.control_0);
            }
        }

        private ITool method_12()
        {
            try
            {
                if (this.iapplication_0 != null)
                {
                    object hook = this.iapplication_0;
                    if (hook is IMapControl2)
                    {
                        return (hook as IMapControl2).CurrentTool;
                    }
                    if (hook is IPageLayoutControl2)
                    {
                        return (hook as IPageLayoutControl2).CurrentTool;
                    }
                    if (hook is ISceneControlDefault)
                    {
                        return (hook as ISceneControlDefault).CurrentTool;
                    }
                    if (hook is IGlobeControlDefault)
                    {
                        return (hook as IGlobeControlDefault).CurrentTool;
                    }
                    if (hook is MapAndPageLayoutControls)
                    {
                        return (hook as MapAndPageLayoutControls).CurrentTool;
                    }
                
                }
                else
                {
                    if (this.object_3 is IMapControl2)
                    {
                        return (this.object_3 as IMapControl2).CurrentTool;
                    }
                    if (this.object_3 is IPageLayoutControl2)
                    {
                        return (this.object_3 as IPageLayoutControl2).CurrentTool;
                    }
                    if (this.object_3 is ISceneControlDefault)
                    {
                        return (this.object_3 as ISceneControlDefault).CurrentTool;
                    }
                    if (this.object_3 is IGlobeControlDefault)
                    {
                        return (this.object_3 as IGlobeControlDefault).CurrentTool;
                    }
                    if (this.object_3 is MapAndPageLayoutControls)
                    {
                        if ((this.object_3 as MapAndPageLayoutControls).ActiveControl is IMapControl2)
                        {
                            return ((this.object_3 as MapAndPageLayoutControls).ActiveControl as IMapControl2).CurrentTool;
                        }
                        return ((this.object_3 as MapAndPageLayoutControls).ActiveControl as IPageLayoutControl2).CurrentTool;
                    }
                 
                }
            }
            catch
            {
            }
            return null;
        }

        private void method_13(object object_4)
        {
            ICommand command = null;
            if (object_4 is string)
            {
                command = this.method_8(object_4 as string);
            }
            else
            {
                command = object_4 as ICommand;
            }
            if (command != null)
            {
                this.HandleCommand(command);
            }
        }

        private void method_14(object object_4, int int_0)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        private void method_15(object object_4)
        {
            if (object_4 != null)
            {
                this.ibarManager_0.Message(MSGTYPE.MTToolTip, object_4.ToString());
            }
        }

        private string method_16(double double_0)
        {
            bool flag = false;
            if (double_0 < 0.0)
            {
                double_0 = -double_0;
                flag = true;
            }
            int num = (int) double_0;
            double_0 = (double_0 - num) * 60.0;
            int num2 = (int) double_0;
            double num3 = Math.Round((double) ((double_0 - num2) * 60.0), 2);
            string str = string.Format("{0}\x00b0{1:00}′{2:00.00}″", num, num2, num3);
            if (flag)
            {
                str = "-" + str;
            }
            return str;
        }

        private string method_17(esriUnits esriUnits_0)
        {
            switch (esriUnits_0)
            {
                case esriUnits.esriUnknownUnits:
                    return "未知单位";

                case esriUnits.esriInches:
                    return "英寸";

                case esriUnits.esriPoints:
                    return "点";

                case esriUnits.esriFeet:
                    return "英尺";

                case esriUnits.esriYards:
                    return "码";

                case esriUnits.esriMiles:
                    return "英里";

                case esriUnits.esriNauticalMiles:
                    return "海里";

                case esriUnits.esriMillimeters:
                    return "毫米";

                case esriUnits.esriCentimeters:
                    return "厘米";

                case esriUnits.esriMeters:
                    return "米";

                case esriUnits.esriKilometers:
                    return "公里";

                case esriUnits.esriDecimalDegrees:
                    return "度";

                case esriUnits.esriDecimeters:
                    return "分米";
            }
            return "未知单位";
        }

        private void method_18(int int_0, int int_1, int int_2, int int_3, double double_0, double double_1)
        {
            if (this.object_3 is IMapControl2)
            {
                string str3;
                IActiveView activeView = (this.object_3 as IMapControl2).ActiveView;
                if (activeView.ScreenDisplay.DisplayTransformation.Units == esriUnits.esriDecimalDegrees)
                {
                    string str = this.method_16(double_0);
                    string str2 = this.method_16(double_1);
                    str3 = string.Format("经纬度:{0}, {1}", str, str2);
                }
                else
                {
                    string[] strArray = new string[6];
                    strArray[0] = "坐标:";
                    double num = Math.Round(double_0, 3);
                    strArray[1] = num.ToString();
                    strArray[2] = ", ";
                    strArray[3] = Math.Round(double_1, 3).ToString();
                    strArray[4] = " ";
                    strArray[5] = this.method_17(activeView.ScreenDisplay.DisplayTransformation.Units);
                    str3 = string.Concat(strArray);
                }
                this.ibarManager_0.Message(MSGTYPE.MTMapPosition, str3);
            }
        }

        private void method_19(int int_0, int int_1, int int_2, int int_3, double double_0, double double_1)
        {
            string str3;
            double num;
            IActiveView focusMap = (this.object_3 as IPageLayoutControl2).ActiveView.FocusMap as IActiveView;
            IPoint point = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            if (focusMap.ScreenDisplay.DisplayTransformation.Units == esriUnits.esriDecimalDegrees)
            {
                string str = this.method_16(point.X);
                string str2 = this.method_16(point.Y);
                str3 = string.Format("{0}  {1}", str, str2);
            }
            else
            {
                string[] strArray = new string[5];
                num = Math.Round(point.X, 3);
                strArray[0] = num.ToString();
                strArray[1] = " ";
                strArray[2] = Math.Round(point.Y, 3).ToString();
                strArray[3] = " ";
                strArray[4] = this.method_17(focusMap.ScreenDisplay.DisplayTransformation.Units);
                str3 = string.Concat(strArray);
            }
            this.ibarManager_0.Message(MSGTYPE.MTMapPosition, str3);
            num = Math.Round(double_0, 2);
            num = Math.Round(double_1, 2);
            str3 = num.ToString() + " " + num.ToString() + " 厘米";
            this.ibarManager_0.Message(MSGTYPE.MTPagePosition, str3);
        }

        private void method_2()
        {
            if (this.OnFrameworkClosed != null)
            {
                this.OnFrameworkClosed(this);
            }
        }

        private void method_20()
        {
            if (this.OnMapDocumentChangedEvent != null)
            {
                this.OnMapDocumentChangedEvent();
            }
        }

        private void method_21(object object_4, Bitmap bitmap_0)
        {
            if (this.OnDockWindowsEvent == null)
            {
                if (object_4 is IDockContent)
                {
                    if (this.idockManagerWrap_0 != null)
                    {
                        this.idockManagerWrap_0.DockWindows(object_4, this.form_0, bitmap_0);
                    }
                }
                //else if (object_4 is DockContent)
                //{
                //    if (this.idockManagerWrap_0 != null)
                //    {
                //        this.idockManagerWrap_0.DockWindows(object_4, this.form_0, bitmap_0);
                //    }
                //    if (object_4 is IMapWindows)
                //    {
                //        if ((object_4 as IMapWindows).ActiveObject is Control)
                //        {
                //            this.control_0 = (object_4 as IMapWindows).ActiveObject as Control;
                //            if (this.method_12() is IToolContextMenu)
                //            {
                //                this.ibarManager_0.SetContextMenu(this.control_0);
                //            }
                //            else
                //            {
                //                this.ibarManager_0.SetPopupContextMenu((object_4 as IMapWindows).ActiveObject as Control);
                //            }
                //        }
                //    }
                //    else if (object_4 is IMapControlForm)
                //    {
                //        //this.control_0 = (object_4 as IMapControlForm).MapControl;
                //        //if (this.method_12() is IToolContextMenu)
                //        //{
                //        //    this.ibarManager_0.SetContextMenu(this.control_0);
                //        //}
                //        //else
                //        //{
                //        //    this.ibarManager_0.SetPopupContextMenu(this.control_0);
                //        //}
                //    }
                //}
                else if (object_4 is Form)
                {
                    (object_4 as Form).MdiParent = this.form_0;
                    (object_4 as Form).Show();
                    if (object_4 is IMapWindows)
                    {
                        if ((object_4 as IMapWindows).ActiveObject is Control)
                        {
                            this.control_0 = (object_4 as IMapWindows).ActiveObject as Control;
                            if (this.method_12() is IToolContextMenu)
                            {
                                this.ibarManager_0.SetContextMenu(this.control_0);
                            }
                            else
                            {
                                this.ibarManager_0.SetPopupContextMenu((object_4 as IMapWindows).ActiveObject as Control);
                            }
                        }
                    }
                    else if (object_4 is IMapControlForm)
                    {
                        //this.control_0 = (object_4 as IMapControlForm).MapControl;
                        //if (this.method_12() is IToolContextMenu)
                        //{
                        //    this.ibarManager_0.SetContextMenu(this.control_0);
                        //}
                        //else
                        //{
                        //    this.ibarManager_0.SetPopupContextMenu(this.control_0);
                        //}
                    }
                }
            }
        }

        private void method_22(object object_4)
        {
            if (this.OnActiveHookChanged != null)
            {
                this.OnActiveHookChanged(object_4);
            }
        }

        private bool method_23(string string_0, CommandTipsType commandTipsType_0)
        {
            if (this.icommandLineWindows_0 != null)
            {
                this.icommandLineWindows_0.ShowCommandString(string_0, (short) commandTipsType_0);
                if (commandTipsType_0 == CommandTipsType.CTTEnd)
                {
                    if ((this.icommandLine_1 != null) && !(this.icommandLine_1 as ICommand).Enabled)
                    {
                        this.icommandLine_1 = null;
                    }
                }
                else if (commandTipsType_0 == CommandTipsType.CTTUnKnown)
                {
                    this.icommandLine_0 = this.method_12() as ICommandLine;
                    if (this.icommandLine_0 != null)
                    {
                        if ((this.icommandLine_0 as ICommand).Enabled)
                        {
                            this.icommandLineWindows_0.ShowCommandString(string.Format("恢复执行{0}", this.icommandLine_0.CommandName), 2);
                            this.icommandLine_0.ActiveCommand();
                        }
                        else
                        {
                            this.method_10(null);
                        }
                    }
                }
                return true;
            }
            return false;
        }

        private void method_24(object object_4)
        {
            if (this.control_0 != null)
            {
                if (object_4 is IToolContextMenu)
                {
                    this.ibarManager_0.SetContextMenu(this.control_0);
                }
                else
                {
                    this.ibarManager_0.SetPopupContextMenu(this.control_0);
                }
            }
        }

        private void method_25(ILayer ilayer_1)
        {
            if (this.OnMapDocumentChangedEvent != null)
            {
                this.OnMapDocumentChangedEvent();
            }
        }

        private void method_26(string string_0)
        {
            if (this.OnMapDocumentSaveEvent != null)
            {
                this.OnMapDocumentSaveEvent(string_0);
            }
        }

        private void method_27()
        {
            this.UpdateUI();
        }

        private void method_3(string string_0, string string_1)
        {
            this.ibarManager_0.Message(MSGTYPE.MTMapPosition, string_0);
            this.ibarManager_0.Message(MSGTYPE.MTPagePosition, string_1);
        }

        private void method_4(int int_0, int int_1, int int_2, int int_3)
        {
            string text1 = int_2.ToString() + " " + int_3.ToString();
        }

        private void method_5()
        {
         //   this.Application.UpdateClickTool = false;
            this.bool_2 = false;
        }

        private void method_6()
        {
            this.updateUIHelper_0.InvokeMethod();
        }

        private void method_7(object object_4)
        {
            if (this.MainHook is IMapControl2)
            {
                IMapControl2 mainHook = this.MainHook as IMapControl2;
                IMap focusMap = (this.object_2 as IPageLayoutControl).ActiveView.FocusMap;
                mainHook.Map.ClearLayers();
                mainHook.Map.MapScale = focusMap.MapScale;
                mainHook.Map.MapUnits = focusMap.MapUnits;
                mainHook.Map.Name = focusMap.Name;
                mainHook.Map.ReferenceScale = focusMap.ReferenceScale;
                mainHook.Map.SpatialReferenceLocked = false;
                mainHook.Map.SpatialReference = focusMap.SpatialReference;
                mainHook.Map.DistanceUnits = focusMap.DistanceUnits;
                for (int i = 0; i < focusMap.LayerCount; i++)
                {
                    mainHook.AddLayer(focusMap.get_Layer(i), i);
                }
                mainHook.ActiveView.Refresh();
            }
        }

        private ICommand method_8(string string_0)
        {
            return null;
        }

        private ICommand method_9(string string_0)
        {
            ICommand command = null;
            if (this.ibarManager_0 != null)
            {
                command = this.ibarManager_0.FindCommand(string_0);
            }
            return command;
        }

        public void RestoreCurrentTool()
        {
            ITool tool = this.method_12();
            if (tool != null)
            {
                if ((tool as ICommand).Enabled)
                {
                    if ((tool is ICommandLine) && ((tool as ICommandLine).CommandType == COMMANDTYPE.STATECOMMAND))
                    {
                        this.method_23("恢复执行" + (tool as ICommandLine).CommandName, CommandTipsType.CTTInput);
                        (tool as ICommandLine).ActiveCommand();
                    }
                }
                else
                {
                    tool.OnDblClick();
                    this.method_10(null);
                }
            }
        }

        public void RestoreTool(ICommandLine icommandLine_2)
        {
            if (icommandLine_2 != null)
            {
                this.method_23("恢复执行" + icommandLine_2.CommandName, CommandTipsType.CTTInput);
                this.method_10(icommandLine_2 as ITool);
            }
        }

        public void UpdateUI()
        {
            if (!(this.iapplication_0 !=null) && (this.ibarManager_0 != null))
            {
                if ((this.thread_0 != null) && this.bool_2)
                {
                    if (this.thread_0.ThreadState == ThreadState.Running)
                    {
                        try
                        {
                            this.thread_0.Abort();
                        }
                        catch
                        {
                        }
                    }
                    this.thread_0 = null;
                }
                if (this.updateUIHelper_0 == null)
                {
                    this.updateUIHelper_0 = new UpdateUIHelper(this.ibarManager_0, this.method_12());
                    this.updateUIHelper_0.OnUpdateUIComplete += new UpdateUIHelper.OnUpdateUICompleteHandler(this.method_5);
                }
                else
                {
                    this.updateUIHelper_0.pCurrentTool = this.method_12();
                }
                this.updateUIHelper_0.UpdateUI();
                this.bool_2 = true;
            }
        }

        public Control ActiveControl
        {
            get
            {
                return this.control_0;
            }
            set
            {
                if (this.iapplication_0 != null)
                {
                   // this.iapplication_0.ActiveControl = value;
                }
                this.control_0 = value;
            }
        }

        public IApplication Application
        {
            get
            {
                return this.iapplication_0;
            }
            protected set
            {
                this.iapplication_0 = value;
                //ApplicationRef.Application = this.iapplication_0;
                //(this.iapplication_0 as IApplicationEvents).OnMessageEvent += new OnMessageEventHandler(this.method_15);
                //(this.iapplication_0 as IApplicationEvents).OnMessageEventEx += new OnMessageEventHandlerEx(this.method_0);
                //(this.iapplication_0 as IApplicationEvents).OnActiveHookChanged += new OnActiveHookChangedHandler(this.method_22);
                //(this.iapplication_0 as IApplicationEvents).OnShowCommandString += new OnShowCommandStringHandler(this.method_23);
                //(this.iapplication_0 as IApplicationEvents).OnDockWindowsEvent += new OnDockWindowsEventHandler(this.method_21);
                //(this.iapplication_0 as IApplicationEvents).OnCurrentToolChanged += new OnCurrentToolChangedHandler(this.method_24);
                //(this.iapplication_0 as IApplicationEvents).OnLayerDeleted += new OnLayerDeletedHandler(this.method_25);
                //(this.iapplication_0 as IApplicationEvents).OnMapDocumentSaveEvent += new OnMapDocumentSaveEventHandler(this.method_26);
                //(this.iapplication_0 as IApplicationEvents).OnApplicationClosed += new OnApplicationClosedHandler(this.method_2);
                //(this.iapplication_0 as IApplicationEvents).OnHideDockWindowEvent += new OnHideDockWindowEventHandler(this.method_1);
            }
        }

        public IBarManager BarManager
        {
            get
            {
                return this.ibarManager_0;
            }
            set
            {
                this.ibarManager_0 = value;
                UIManagerHelper.BarManager = value;
                this.ibarManager_0.Framework = this;
            //    this.iapplication_0.PaintStyleName = this.ibarManager_0.PaintStyleName;
                ApplicationRef.BarManage = value;
            }
        }

        public string CommandLines
        {
            set
            {
                if (this.icommandLine_0 != null)
                {
                    this.icommandLine_0.CommandLines = value;
                }
            }
        }

        public ICommandLineWindows CommandLineWindows
        {
            get
            {
                return this.icommandLineWindows_0;
            }
            set
            {
                this.icommandLineWindows_0 = value;
                this.icommandLineWindows_0.Framework = this;
            }
        }

        public object ContainerHook
        {
            get
            {
                return this.object_2;
            }
            set
            {
                this.object_2 = value;
              //  this.iapplication_0.ContainerHook = value;
            }
        }

        public ILayer CurrentLayer
        {
            get
            {
                return this.ilayer_0;
            }
            set
            {
                this.ilayer_0 = value;
                if (this.iapplication_0 != null)
                {
                   // this.iapplication_0.CurrentLayer = this.ilayer_0;
                }
            }
        }

        public IDockManagerWrap DockManager
        {
            get
            {
                return this.idockManagerWrap_0;
            }
            set
            {
                this.idockManagerWrap_0 = value;
                UIManagerHelper.DockManagerWrap = value;
            }
        }

        public object Hook
        {
            get
            {
                return this.object_3;
            }
            set
            {
                if (this.object_3 != null)
                {
                    try
                    {
                        if (this.object_3 is IMapControl2)
                        {
                            (this.object_3 as IMapControlEvents2_Event).OnAfterDraw-=(new IMapControlEvents2_OnAfterDrawEventHandler(this.method_14));
                            (this.object_3 as IMapControlEvents2_Event).OnMouseMove-=(new IMapControlEvents2_OnMouseMoveEventHandler(this.method_18));
                        }
                        else if (this.object_3 is IPageLayoutControl2)
                        {
                            (this.object_3 as IPageLayoutControlEvents_Event).OnAfterDraw-=(new IPageLayoutControlEvents_OnAfterDrawEventHandler(this.method_14));
                            (this.object_3 as IPageLayoutControlEvents_Event).OnMouseMove-=(new IPageLayoutControlEvents_OnMouseMoveEventHandler(this.method_19));
                        }
                        else if (this.object_3 is ISceneControlDefault)
                        {
                            (this.object_3 as ISceneControlEvents_Event).OnMouseMove-=(new ISceneControlEvents_OnMouseMoveEventHandler(this.method_4));
                        }
                        else if (!(this.object_3 is IGlobeControlDefault))
                        {
                        }
                    }
                    catch
                    {
                    }
                }
                this.object_3 = value;
                if (this.iapplication_0 != value)
                {
                    this.iapplication_0 = value as IApplication;
                }
                if (this.object_3 is IMapControl2)
                {
                    (this.object_3 as IMapControlEvents2_Event).OnAfterDraw+=(new IMapControlEvents2_OnAfterDrawEventHandler(this.method_14));
                    (this.object_3 as IMapControlEvents2_Event).OnMouseMove+=(new IMapControlEvents2_OnMouseMoveEventHandler(this.method_18));
                }
                else if (this.object_3 is IPageLayoutControl2)
                {
                    (this.object_3 as IPageLayoutControlEvents_Event).OnAfterDraw+=(new IPageLayoutControlEvents_OnAfterDrawEventHandler(this.method_14));
                    (this.object_3 as IPageLayoutControlEvents_Event).OnMouseMove+=(new IPageLayoutControlEvents_OnMouseMoveEventHandler(this.method_19));
                }
                else if (this.object_3 is ISceneControlDefault)
                {
                    (this.object_3 as ISceneControlEvents_Event).OnMouseMove+=(new ISceneControlEvents_OnMouseMoveEventHandler(this.method_4));
                }
                else if (!(this.object_3 is IGlobeControlDefault) && (this.object_3 is MapAndPageLayoutControls))
                {
                    (this.object_3 as MapAndPageLayoutControls).OnMousePostion += new OnMousePostionHandler(this.method_3);
                }
                if (this.ibarManager_0 != null)
                {
                    this.ibarManager_0.ChangeHook(this.object_3);
                    string message = "";
                    ITool tool = this.method_12();
                    if (tool != null)
                    {
                        if ((tool as ICommand).Message != null)
                        {
                            message = (tool as ICommand).Message;
                        }
                        if (message.Length == 0)
                        {
                            message = (tool as ICommand).Caption;
                        }
                    }
                    this.ibarManager_0.Message(MSGTYPE.MTCurrentTool, message);
                }
            }
        }

        public Form MainForm
        {
            get
            {
                return this.form_0;
            }
            set
            {
                this.form_0 = value;
            }
        }

        public object MainHook
        {
            get
            {
                return this.object_1;
            }
            set
            {
                this.object_1 = value;
               // this.iapplication_0.MainHook = value;
            }
        }

        public IMapControl2 NavigationMap
        {
            get
            {
                return this.imapControl2_0;
            }
            set
            {
                this.imapControl2_0 = value;
            }
        }

        public object SecondaryHook
        {
            get
            {
                return this.object_0;
            }
            set
            {
                this.object_0 = value;
               // this.iapplication_0.SecondaryHook = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
                _appContext.StyleGallery = value;
            }
        }

        public IAppContext AppContext
        {
            get { return _appContext; }
            set { _appContext = value; }
        }

        internal enum enumCommandOperatorState
        {
            enumCOSCommandName,
            enumCOSCommandTip,
            enumCOSInput,
            enumCOSEnd,
            enumCOSLog,
            enumCOSUnKnown
        }
    }
}

