using System;
using System.Collections;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Events;

namespace Yutai.ArcGIS.Common
{
    public class MapAndPageLayoutControlsold
    {
        private ArrayList arrayList_0;
        private bool bool_0;
        private bool bool_1;
        private IMapControl3 imapControl3_0;
        private IPageLayoutControl2 ipageLayoutControl2_0;
        private ITool itool_0;
        private ITool itool_1;

        public event OnActiveHookChangedHandler OnActiveHookChanged;

        public event OnMousePostionHandler OnMousePostion;

        public MapAndPageLayoutControlsold()
        {
            this.imapControl3_0 = null;
            this.ipageLayoutControl2_0 = null;
            this.itool_0 = null;
            this.itool_1 = null;
            this.bool_0 = true;
            this.arrayList_0 = null;
            this.bool_1 = true;
            this.arrayList_0 = new ArrayList();
        }

        public MapAndPageLayoutControlsold(IMapControl3 imapControl3_1, IPageLayoutControl2 ipageLayoutControl2_1) : this()
        {
            this.imapControl3_0 = imapControl3_1;
            this.ipageLayoutControl2_0 = ipageLayoutControl2_1;
            (this.ipageLayoutControl2_0 as IPageLayoutControlEvents_Event).OnMouseMove+=(new IPageLayoutControlEvents_OnMouseMoveEventHandler(this.method_3));
            (this.imapControl3_0 as IMapControlEvents2_Event).OnMouseMove+=(new IMapControlEvents2_OnMouseMoveEventHandler(this.method_2));
        }

        public void ActivateMap()
        {
            try
            {
                if ((this.ipageLayoutControl2_0 == null) || (this.imapControl3_0 == null))
                {
                    throw new Exception("ControlsSynchronizer::ActivateMap:\r\nEither MapControl or PageLayoutControl are not initialized!");
                }
                if (this.ipageLayoutControl2_0.CurrentTool != null)
                {
                    this.itool_1 = this.ipageLayoutControl2_0.CurrentTool;
                }
                this.ipageLayoutControl2_0.ActiveView.Deactivate();
                this.imapControl3_0.ActiveView.Activate(this.imapControl3_0.hWnd);
                if (this.itool_0 != null)
                {
                    this.imapControl3_0.CurrentTool = this.itool_0;
                }
                this.bool_0 = true;
                if (this.OnActiveHookChanged != null)
                {
                    this.OnActiveHookChanged(this);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("ControlsSynchronizer::ActivateMap:\r\n{0}", exception.Message));
            }
        }

        public void ActivatePageLayout()
        {
            try
            {
                if ((this.ipageLayoutControl2_0 == null) || (this.imapControl3_0 == null))
                {
                    throw new Exception("ControlsSynchronizer::ActivatePageLayout:\r\nEither MapControl or PageLayoutControl are not initialized!");
                }
                if (this.imapControl3_0.CurrentTool != null)
                {
                    this.itool_0 = this.imapControl3_0.CurrentTool;
                }
                this.imapControl3_0.ActiveView.Deactivate();
                this.ipageLayoutControl2_0.ActiveView.Activate(this.ipageLayoutControl2_0.hWnd);
                if (this.itool_1 != null)
                {
                    this.ipageLayoutControl2_0.CurrentTool = this.itool_1;
                }
                this.bool_0 = false;
                if (this.OnActiveHookChanged != null)
                {
                    this.OnActiveHookChanged(this);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("ControlsSynchronizer::ActivatePageLayout:\r\n{0}", exception.Message));
            }
        }

        public void AddFrameworkControl(object object_0)
        {
            if (object_0 == null)
            {
                throw new Exception("ControlsSynchronizer::AddFrameworkControl:\r\nAdded control is not initialized!");
            }
            this.arrayList_0.Add(object_0);
        }

        public void BindControls(bool bool_2)
        {
            if ((this.ipageLayoutControl2_0 == null) || (this.imapControl3_0 == null))
            {
                throw new Exception("ControlsSynchronizer::BindControls:\r\nEither MapControl or PageLayoutControl are not initialized!");
            }
            IMap map = new Map {
                Name = "Map"
            };
            IMaps maps = new Maps();
            maps.Add(map);
            this.ActivatePageLayout();
            this.ipageLayoutControl2_0.PageLayout.ReplaceMaps(maps);
            this.imapControl3_0.Map = map;
            this.itool_1 = null;
            this.itool_0 = null;
            if (bool_2)
            {
                this.ActivateMap();
            }
            else
            {
                this.ActivatePageLayout();
            }
        }

        public void BindControls(IMapControl3 imapControl3_1, IPageLayoutControl2 ipageLayoutControl2_1, bool bool_2)
        {
            if ((imapControl3_1 == null) || (ipageLayoutControl2_1 == null))
            {
                throw new Exception("ControlsSynchronizer::BindControls:\r\nEither MapControl or PageLayoutControl are not initialized!");
            }
            this.imapControl3_0 = this.MapControl;
            this.ipageLayoutControl2_0 = ipageLayoutControl2_1;
            (this.ipageLayoutControl2_0 as IPageLayoutControlEvents_Event).OnFocusMapChanged+=(new IPageLayoutControlEvents_OnFocusMapChangedEventHandler(this.method_4));
            this.BindControls(bool_2);
        }

        private string method_0(double double_0)
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
            string str = num.ToString() + "\x00b0" + num2.ToString("00") + "′" + num3.ToString("00.00") + "″";
            if (flag)
            {
                str = "-" + str;
            }
            return str;
        }

        private string method_1(esriUnits esriUnits_0)
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

        private void method_2(int int_0, int int_1, int int_2, int int_3, double double_0, double double_1)
        {
            string str3;
            IActiveView activeView = this.imapControl3_0.ActiveView;
            if (activeView.ScreenDisplay.DisplayTransformation.Units == esriUnits.esriDecimalDegrees)
            {
                string str = this.method_0(double_0);
                string str2 = this.method_0(double_1);
                str3 = "经纬度:" + str + ", " + str2;
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
                strArray[5] = this.method_1(activeView.ScreenDisplay.DisplayTransformation.Units);
                str3 = string.Concat(strArray);
            }
            if (this.OnMousePostion != null)
            {
                this.OnMousePostion(str3, "");
            }
        }

        private void method_3(int int_0, int int_1, int int_2, int int_3, double double_0, double double_1)
        {
            string str3;
            double num;
            IActiveView focusMap = this.ipageLayoutControl2_0.ActiveView.FocusMap as IActiveView;
            IPoint point = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            if (focusMap.ScreenDisplay.DisplayTransformation.Units == esriUnits.esriDecimalDegrees)
            {
                string str = this.method_0(point.X);
                string str2 = this.method_0(point.Y);
                str3 = str + "  " + str2;
            }
            else
            {
                string[] strArray = new string[5];
                num = Math.Round(point.X, 3);
                strArray[0] = num.ToString();
                strArray[1] = " ";
                strArray[2] = Math.Round(point.Y, 3).ToString();
                strArray[3] = " ";
                strArray[4] = this.method_1(focusMap.ScreenDisplay.DisplayTransformation.Units);
                str3 = string.Concat(strArray);
            }
            num = Math.Round(double_0, 2);
            num = Math.Round(double_1, 2);
            string str4 = num.ToString() + " " + num.ToString() + " 厘米";
            if (this.OnMousePostion != null)
            {
                this.OnMousePostion(str3, str4);
            }
        }

        private void method_4()
        {
            if (this.bool_1)
            {
                bool flag;
                if (!(flag = this.bool_0))
                {
                    this.imapControl3_0.Map = this.ipageLayoutControl2_0.ActiveView.FocusMap;
                    if (flag)
                    {
                        this.ActivateMap();
                        this.imapControl3_0.ActiveView.Refresh();
                    }
                }
                else if (this.imapControl3_0.Map != this.ipageLayoutControl2_0.ActiveView.FocusMap)
                {
                    this.ActivatePageLayout();
                    this.imapControl3_0.Map = this.ipageLayoutControl2_0.ActiveView.FocusMap;
                    if (flag)
                    {
                        this.ActivateMap();
                        this.imapControl3_0.ActiveView.Refresh();
                    }
                }
            }
        }

        private void method_5(object object_0)
        {
            try
            {
                if (object_0 == null)
                {
                    throw new Exception("ControlsSynchronizer::SetBuddies:\r\nTarget Buddy Control is not initialized!");
                }
                foreach (object obj2 in this.arrayList_0)
                {
                    if (obj2 is IToolbarControl)
                    {
                        ((IToolbarControl) obj2).SetBuddyControl(object_0);
                    }
                    else if (obj2 is ITOCControl)
                    {
                        ((ITOCControl) obj2).SetBuddyControl(object_0);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("ControlsSynchronizer::SetBuddies:\r\n{0}", exception.Message));
            }
        }

        public void RemoveFrameworkControl(object object_0)
        {
            if (object_0 == null)
            {
                throw new Exception("ControlsSynchronizer::RemoveFrameworkControl:\r\nControl to be removed is not initialized!");
            }
            this.arrayList_0.Remove(object_0);
        }

        public void RemoveFrameworkControlAt(int int_0)
        {
            if (this.arrayList_0.Count < int_0)
            {
                throw new Exception("ControlsSynchronizer::RemoveFrameworkControlAt:\r\nIndex is out of range!");
            }
            this.arrayList_0.RemoveAt(int_0);
        }

        public void ReplaceMap(IMap imap_0)
        {
            if (imap_0 == null)
            {
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nNew map for replacement is not initialized!");
            }
            if ((this.ipageLayoutControl2_0 == null) || (this.imapControl3_0 == null))
            {
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nEither MapControl or PageLayoutControl are not initialized!");
            }
            IMaps maps = new Maps();
            maps.Add(imap_0);
            bool flag = this.bool_0;
            this.ActivatePageLayout();
            this.bool_1 = false;
            this.ipageLayoutControl2_0.PageLayout.ReplaceMaps(maps);
            this.bool_1 = true;
            this.imapControl3_0.Map = imap_0;
            this.itool_1 = null;
            this.itool_0 = null;
            if (flag)
            {
                this.ActivateMap();
                this.imapControl3_0.ActiveView.Refresh();
            }
            else
            {
                this.ActivatePageLayout();
                this.ipageLayoutControl2_0.ActiveView.Refresh();
            }
        }

        public void ReplaceMap(IMaps imaps_0, IMap imap_0)
        {
            if ((this.ipageLayoutControl2_0 == null) || (this.imapControl3_0 == null))
            {
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nEither MapControl or PageLayoutControl are not initialized!");
            }
            bool flag = this.bool_0;
            this.ActivatePageLayout();
            this.bool_1 = false;
            this.ipageLayoutControl2_0.PageLayout.ReplaceMaps(imaps_0);
            this.bool_1 = true;
            this.imapControl3_0.Map = imap_0;
            this.itool_1 = null;
            this.itool_0 = null;
            if (flag)
            {
                this.ActivateMap();
                this.imapControl3_0.ActiveView.Refresh();
            }
            else
            {
                this.ActivatePageLayout();
                this.ipageLayoutControl2_0.ActiveView.Refresh();
            }
        }

        public object ActiveControl
        {
            get
            {
                if ((this.imapControl3_0 == null) || (this.ipageLayoutControl2_0 == null))
                {
                    throw new Exception("ControlsSynchronizer::ActiveControl:\r\nEither MapControl or PageLayoutControl are not initialized!");
                }
                if (this.bool_0)
                {
                    return this.imapControl3_0.Object;
                }
                return this.ipageLayoutControl2_0.Object;
            }
        }

        public IActiveView ActiveView
        {
            get
            {
                if (this.bool_0)
                {
                    return this.imapControl3_0.ActiveView;
                }
                return this.ipageLayoutControl2_0.ActiveView;
            }
        }

        public string ActiveViewType
        {
            get
            {
                if (this.bool_0)
                {
                    return "MapControl";
                }
                return "PageLayoutControl";
            }
        }

        public ITool CurrentTool
        {
            get
            {
                if (this.bool_0)
                {
                    if (this.imapControl3_0 != null)
                    {
                        return this.imapControl3_0.CurrentTool;
                    }
                }
                else if (this.ipageLayoutControl2_0 != null)
                {
                    return this.ipageLayoutControl2_0.CurrentTool;
                }
                return null;
            }
            set
            {
                if (this.bool_0)
                {
                    if (this.imapControl3_0 != null)
                    {
                        this.imapControl3_0.CurrentTool = value;
                    }
                }
                else if (this.ipageLayoutControl2_0 != null)
                {
                    this.ipageLayoutControl2_0.CurrentTool = value;
                }
            }
        }

        public IMapControl3 MapControl
        {
            get
            {
                return this.imapControl3_0;
            }
            set
            {
                this.imapControl3_0 = value;
            }
        }

        public IPageLayoutControl2 PageLayoutControl
        {
            get
            {
                return this.ipageLayoutControl2_0;
            }
            set
            {
                this.ipageLayoutControl2_0 = value;
            }
        }
    }
}

