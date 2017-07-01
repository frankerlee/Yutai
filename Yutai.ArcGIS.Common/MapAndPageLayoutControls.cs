using System;
using System.Threading;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Events;

namespace Yutai.ArcGIS.Common
{
    public class MapAndPageLayoutControls
    {
        private OnActiveHookChangedHandler onActiveHookChangedHandler_0;

        private OnMousePostionHandler onMousePostionHandler_0;

        private IMapControl3 imapControl3_0 = null;

        private IPageLayoutControl2 ipageLayoutControl2_0 = null;

        private ITool itool_0 = null;

        private ITool itool_1 = null;

        private bool bool_0 = true;

        private AxMapControl axMapControl_0 = null;

        private AxPageLayoutControl axPageLayoutControl_0 = null;

        private bool bool_1 = true;

        private IActiveViewEvents_Event iactiveViewEvents_Event_0 = null;

        public object ActiveControl
        {
            get
            {
                object obj;
                if ((this.imapControl3_0 == null ? true : this.ipageLayoutControl2_0 == null))
                {
                    throw new Exception(
                        "ControlsSynchronizer::ActiveControl:\r\nEither MapControl or PageLayoutControl are not initialized!");
                }
                obj = (!this.bool_0 ? this.ipageLayoutControl2_0.Object : this.imapControl3_0.Object);
                return obj;
            }
        }

        public IActiveView ActiveView
        {
            get
            {
                IActiveView activeView;
                activeView = (!this.bool_0 ? this.ipageLayoutControl2_0.ActiveView : this.imapControl3_0.ActiveView);
                return activeView;
            }
        }

        public string ActiveViewType
        {
            get { return (!this.bool_0 ? "PageLayoutControl" : "MapControl"); }
        }

        public ITool CurrentTool
        {
            get
            {
                ITool currentTool;
                if (!this.bool_0)
                {
                    if (this.ipageLayoutControl2_0 == null)
                    {
                        currentTool = null;
                        return currentTool;
                    }
                    currentTool = this.ipageLayoutControl2_0.CurrentTool;
                    return currentTool;
                }
                else
                {
                    if (this.imapControl3_0 == null)
                    {
                        currentTool = null;
                        return currentTool;
                    }
                    currentTool = this.imapControl3_0.CurrentTool;
                    return currentTool;
                }
                currentTool = null;
                return currentTool;
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
            get { return this.imapControl3_0; }
            set { this.imapControl3_0 = value; }
        }

        public IPageLayoutControl2 PageLayoutControl
        {
            get { return this.ipageLayoutControl2_0; }
            set { this.ipageLayoutControl2_0 = value; }
        }

        public MapAndPageLayoutControls(AxMapControl axMapControl_1, AxPageLayoutControl axPageLayoutControl_1)
        {
            this.axMapControl_0 = axMapControl_1;
            this.axPageLayoutControl_0 = axPageLayoutControl_1;
            IGraphicSnapEnvironment2 pageLayout = axPageLayoutControl_1.PageLayout as IGraphicSnapEnvironment2;
            this.imapControl3_0 = axMapControl_1.Object as IMapControl3;
            this.ipageLayoutControl2_0 = axPageLayoutControl_1.Object as IPageLayoutControl2;
            (this.ipageLayoutControl2_0 as IPageLayoutControlEvents_Event).OnMouseMove +=
                new IPageLayoutControlEvents_OnMouseMoveEventHandler(this.method_5);
            (this.imapControl3_0 as IMapControlEvents2_Event).OnMouseMove +=
                new IMapControlEvents2_OnMouseMoveEventHandler(this.method_4);
            (this.ipageLayoutControl2_0 as IPageLayoutControlEvents_Event).OnFocusMapChanged +=
                new IPageLayoutControlEvents_OnFocusMapChangedEventHandler(this.method_6);
            (this.ipageLayoutControl2_0 as IPageLayoutControlEvents_Event).OnPageLayoutReplaced +=
                new IPageLayoutControlEvents_OnPageLayoutReplacedEventHandler(this.method_1);
            this.iactiveViewEvents_Event_0 = this.ipageLayoutControl2_0.ActiveView.FocusMap as IActiveViewEvents_Event;
            try
            {
                if (this.iactiveViewEvents_Event_0 != null)
                {
                    this.iactiveViewEvents_Event_0.ItemAdded +=
                        new IActiveViewEvents_ItemAddedEventHandler(this.method_8);
                    this.iactiveViewEvents_Event_0.ItemReordered +=
                        new IActiveViewEvents_ItemReorderedEventHandler(this.method_0);
                    this.iactiveViewEvents_Event_0.ItemDeleted +=
                        new IActiveViewEvents_ItemDeletedEventHandler(this.method_7);
                }
            }
            catch
            {
            }
        }

        public void ActivateMap()
        {
            try
            {
                this.axPageLayoutControl_0.Visible = false;
                this.axMapControl_0.Visible = true;
                if (this.ipageLayoutControl2_0.CurrentTool != null)
                {
                    this.itool_1 = this.ipageLayoutControl2_0.CurrentTool;
                }
                if (this.itool_0 != null)
                {
                    this.imapControl3_0.CurrentTool = this.itool_0;
                }
                this.bool_0 = true;
                if (this.onActiveHookChangedHandler_0 != null)
                {
                    this.onActiveHookChangedHandler_0(this);
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
                this.axPageLayoutControl_0.Visible = true;
                this.axMapControl_0.Visible = false;
                if (this.imapControl3_0.CurrentTool != null)
                {
                    this.itool_0 = this.imapControl3_0.CurrentTool;
                }
                IMapFrame focusMapFrame = this.GetFocusMapFrame(this.ipageLayoutControl2_0.PageLayout);
                (focusMapFrame.Map as IMapClipOptions).ClipType = esriMapClipType.esriMapClipNone;
                if (focusMapFrame.Map is IMapAutoExtentOptions)
                {
                    (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentType = esriExtentTypeEnum.esriExtentDefault;
                }
                (focusMapFrame.Map as IActiveView).Extent = this.axMapControl_0.ActiveView.Extent;
                if (this.itool_1 != null)
                {
                    this.ipageLayoutControl2_0.CurrentTool = this.itool_1;
                }
                this.bool_0 = false;
                if (this.onActiveHookChangedHandler_0 != null)
                {
                    this.onActiveHookChangedHandler_0(this);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("ControlsSynchronizer::ActivatePageLayout:\r\n{0}", exception.Message));
            }
        }

        internal IMapFrame GetFocusMapFrame(IPageLayout ipageLayout_0)
        {
            IMapFrame mapFrame;
            IGraphicsContainer ipageLayout0 = ipageLayout_0 as IGraphicsContainer;
            ipageLayout0.Reset();
            IElement element = ipageLayout0.Next();
            while (true)
            {
                if (element == null)
                {
                    mapFrame = null;
                    break;
                }
                else if (element is IMapFrame)
                {
                    mapFrame = element as IMapFrame;
                    break;
                }
                else
                {
                    element = ipageLayout0.Next();
                }
            }
            return mapFrame;
        }

        private void method_0(object object_0, int int_0)
        {
            if (object_0 is ILayer && this.imapControl3_0 != null)
            {
                int num = this.method_10(this.imapControl3_0.Map as IBasicMap, object_0 as ILayer);
                this.imapControl3_0.MoveLayerTo(num, int_0);
                this.imapControl3_0.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, object_0, null);
            }
        }

        private void method_1(object object_0)
        {
            try
            {
                if (this.iactiveViewEvents_Event_0 != null)
                {
                    this.iactiveViewEvents_Event_0.ItemAdded -=
                        new IActiveViewEvents_ItemAddedEventHandler(this.method_8);
                    this.iactiveViewEvents_Event_0.ItemReordered -=
                        new IActiveViewEvents_ItemReorderedEventHandler(this.method_0);
                    this.iactiveViewEvents_Event_0.ItemDeleted -=
                        new IActiveViewEvents_ItemDeletedEventHandler(this.method_7);
                }
            }
            catch
            {
            }
            this.iactiveViewEvents_Event_0 = this.ipageLayoutControl2_0.ActiveView.FocusMap as IActiveViewEvents_Event;
            try
            {
                if (this.iactiveViewEvents_Event_0 != null)
                {
                    this.iactiveViewEvents_Event_0.ItemAdded +=
                        new IActiveViewEvents_ItemAddedEventHandler(this.method_8);
                    this.iactiveViewEvents_Event_0.ItemReordered +=
                        new IActiveViewEvents_ItemReorderedEventHandler(this.method_0);
                    this.iactiveViewEvents_Event_0.ItemDeleted +=
                        new IActiveViewEvents_ItemDeletedEventHandler(this.method_7);
                }
            }
            catch
            {
            }
        }

        private int method_10(IBasicMap ibasicMap_0, ILayer ilayer_0)
        {
            int num;
            int num1 = 0;
            while (true)
            {
                if (num1 >= ibasicMap_0.LayerCount)
                {
                    num = -1;
                    break;
                }
                else if (ibasicMap_0.Layer[num1] == ilayer_0)
                {
                    num = num1;
                    break;
                }
                else
                {
                    num1++;
                }
            }
            return num;
        }

        private string method_2(double double_0)
        {
            bool flag = false;
            if (double_0 < 0)
            {
                double_0 = -double_0;
                flag = true;
            }
            int double0 = (int) double_0;
            double_0 = (double_0 - (double) double0)*60;
            int num = (int) double_0;
            double num1 = (double) Math.Round((double_0 - (double) num)*60, 2);
            string[] str = new string[] {double0.ToString(), "°", num.ToString("00"), "′", num1.ToString("00.00"), "″"};
            string str1 = string.Concat(str);
            if (flag)
            {
                str1 = string.Concat("-", str1);
            }
            return str1;
        }

        private string method_3(esriUnits esriUnits_0)
        {
            string str;
            switch (esriUnits_0)
            {
                case esriUnits.esriUnknownUnits:
                {
                    str = "未知单位";
                    break;
                }
                case esriUnits.esriInches:
                {
                    str = "英寸";
                    break;
                }
                case esriUnits.esriPoints:
                {
                    str = "点";
                    break;
                }
                case esriUnits.esriFeet:
                {
                    str = "英尺";
                    break;
                }
                case esriUnits.esriYards:
                {
                    str = "码";
                    break;
                }
                case esriUnits.esriMiles:
                {
                    str = "英里";
                    break;
                }
                case esriUnits.esriNauticalMiles:
                {
                    str = "海里";
                    break;
                }
                case esriUnits.esriMillimeters:
                {
                    str = "毫米";
                    break;
                }
                case esriUnits.esriCentimeters:
                {
                    str = "厘米";
                    break;
                }
                case esriUnits.esriMeters:
                {
                    str = "米";
                    break;
                }
                case esriUnits.esriKilometers:
                {
                    str = "公里";
                    break;
                }
                case esriUnits.esriDecimalDegrees:
                {
                    str = "度";
                    break;
                }
                case esriUnits.esriDecimeters:
                {
                    str = "分米";
                    break;
                }
                default:
                {
                    str = "未知单位";
                    break;
                }
            }
            return str;
        }

        private void method_4(int int_0, int int_1, int int_2, int int_3, double double_0, double double_1)
        {
            string str;
            IActiveView activeView = this.imapControl3_0.ActiveView;
            if (activeView.ScreenDisplay.DisplayTransformation.Units != esriUnits.esriDecimalDegrees)
            {
                string[] strArrays = new string[] {"坐标:", null, null, null, null, null};
                double num = (double) Math.Round(double_0, 3);
                strArrays[1] = num.ToString();
                strArrays[2] = ", ";
                num = (double) Math.Round(double_1, 3);
                strArrays[3] = num.ToString();
                strArrays[4] = " ";
                strArrays[5] = this.method_3(activeView.ScreenDisplay.DisplayTransformation.Units);
                str = string.Concat(strArrays);
            }
            else
            {
                string str1 = this.method_2(double_0);
                string str2 = this.method_2(double_1);
                str = string.Concat("经纬度:", str1, ", ", str2);
            }
            if (this.onMousePostionHandler_0 != null)
            {
                this.onMousePostionHandler_0(str, "");
            }
        }

        private void method_5(int int_0, int int_1, int int_2, int int_3, double double_0, double double_1)
        {
            string str;
            double num;
            IActiveView focusMap = this.ipageLayoutControl2_0.ActiveView.FocusMap as IActiveView;
            IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            if (focusMap.ScreenDisplay.DisplayTransformation.Units != esriUnits.esriDecimalDegrees)
            {
                string[] strArrays = new string[5];
                num = (double) Math.Round(mapPoint.X, 3);
                strArrays[0] = num.ToString();
                strArrays[1] = " ";
                num = (double) Math.Round(mapPoint.Y, 3);
                strArrays[2] = num.ToString();
                strArrays[3] = " ";
                strArrays[4] = this.method_3(focusMap.ScreenDisplay.DisplayTransformation.Units);
                str = string.Concat(strArrays);
            }
            else
            {
                string str1 = this.method_2(mapPoint.X);
                string str2 = this.method_2(mapPoint.Y);
                str = string.Concat(str1, "  ", str2);
            }
            num = (double) Math.Round(double_0, 2);
            string str3 = num.ToString();
            num = (double) Math.Round(double_1, 2);
            string str4 = string.Concat(str3, " ", num.ToString(), " 厘米");
            if (this.onMousePostionHandler_0 != null)
            {
                this.onMousePostionHandler_0(str, str4);
            }
        }

        private void method_6()
        {
            int i;
            if (this.bool_1)
            {
                try
                {
                    if (this.iactiveViewEvents_Event_0 != null)
                    {
                        this.iactiveViewEvents_Event_0.ItemAdded -=
                            new IActiveViewEvents_ItemAddedEventHandler(this.method_8);
                        this.iactiveViewEvents_Event_0.ItemReordered -=
                            new IActiveViewEvents_ItemReorderedEventHandler(this.method_0);
                        this.iactiveViewEvents_Event_0.ItemDeleted -=
                            new IActiveViewEvents_ItemDeletedEventHandler(this.method_7);
                    }
                }
                catch
                {
                }
                IMap focusMap = this.ipageLayoutControl2_0.ActiveView.FocusMap;
                this.iactiveViewEvents_Event_0 = focusMap as IActiveViewEvents_Event;
                try
                {
                    if (this.iactiveViewEvents_Event_0 != null)
                    {
                        this.iactiveViewEvents_Event_0.ItemAdded +=
                            new IActiveViewEvents_ItemAddedEventHandler(this.method_8);
                        this.iactiveViewEvents_Event_0.ItemReordered +=
                            new IActiveViewEvents_ItemReorderedEventHandler(this.method_0);
                        this.iactiveViewEvents_Event_0.ItemDeleted +=
                            new IActiveViewEvents_ItemDeletedEventHandler(this.method_7);
                    }
                }
                catch
                {
                }
                this.imapControl3_0.Map.ClearLayers();
                (this.imapControl3_0.Map as IActiveView).ContentsChanged();
                this.imapControl3_0.Map.MapUnits = focusMap.MapUnits;
                this.imapControl3_0.Map.SpatialReferenceLocked = false;
                this.imapControl3_0.Map.SpatialReference = focusMap.SpatialReference;
                this.imapControl3_0.Map.Name = focusMap.Name;
                for (i = 0; i < focusMap.LayerCount; i++)
                {
                    ILayer layer = focusMap.Layer[i];
                    this.imapControl3_0.AddLayer(layer, i);
                }
                (this.imapControl3_0.Map as IGraphicsContainer).DeleteAllElements();
                IGraphicsContainer graphicsContainer = focusMap as IGraphicsContainer;
                graphicsContainer.Reset();
                IElement element = graphicsContainer.Next();
                int num = 0;
                while (element != null)
                {
                    (this.imapControl3_0.Map as IGraphicsContainer).AddElement(element, num);
                    num++;
                    element = graphicsContainer.Next();
                }
                (this.imapControl3_0.Map as ITableCollection).RemoveAllTables();
                ITableCollection tableCollection = focusMap as ITableCollection;
                for (i = 0; i < tableCollection.TableCount; i++)
                {
                    (this.imapControl3_0.Map as ITableCollection).AddTable(tableCollection.Table[i]);
                }
                this.imapControl3_0.ActiveView.Extent = (focusMap as IActiveView).Extent;
                this.imapControl3_0.ActiveView.Refresh();
            }
        }

        private void method_7(object object_0)
        {
            if (object_0 is ILayer && this.imapControl3_0 != null)
            {
                int num = this.method_10(this.imapControl3_0.Map as IBasicMap, object_0 as ILayer);
                this.imapControl3_0.DeleteLayer(num);
                if (this.imapControl3_0.LayerCount == 0)
                {
                    this.imapControl3_0.Map.SpatialReferenceLocked = false;
                    this.imapControl3_0.Map.SpatialReference = new UnknownCoordinateSystem() as ISpatialReference;
                    this.imapControl3_0.Map.MapUnits = esriUnits.esriUnknownUnits;
                    this.imapControl3_0.Map.DistanceUnits = esriUnits.esriUnknownUnits;
                    (this.imapControl3_0.Map as IActiveView).Extent =
                        (this.imapControl3_0.Map as IActiveView).FullExtent;
                }
                this.imapControl3_0.ActiveView.Refresh();
            }
        }

        private void method_8(object object_0)
        {
            if (object_0 is ILayer && this.imapControl3_0 != null)
            {
                int num = this.method_10((this.iactiveViewEvents_Event_0 as IActiveView).FocusMap as IBasicMap,
                    object_0 as ILayer);
                this.imapControl3_0.AddLayer((ILayer) object_0, num);
                this.imapControl3_0.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, object_0, null);
            }
        }

        private int method_9(IGroupLayer igroupLayer_0, ILayer ilayer_0)
        {
            int num;
            int num1 = 0;
            while (true)
            {
                if (num1 >= (igroupLayer_0 as ICompositeLayer).Count)
                {
                    num = -1;
                    break;
                }
                else if ((igroupLayer_0 as ICompositeLayer).Layer[num1] == ilayer_0)
                {
                    num = num1;
                    break;
                }
                else
                {
                    num1++;
                }
            }
            return num;
        }

        public event OnActiveHookChangedHandler OnActiveHookChanged
        {
            add
            {
                OnActiveHookChangedHandler onActiveHookChangedHandler;
                OnActiveHookChangedHandler onActiveHookChangedHandler0 = this.onActiveHookChangedHandler_0;
                do
                {
                    onActiveHookChangedHandler = onActiveHookChangedHandler0;
                    OnActiveHookChangedHandler onActiveHookChangedHandler1 =
                        (OnActiveHookChangedHandler) Delegate.Combine(onActiveHookChangedHandler, value);
                    onActiveHookChangedHandler0 =
                        Interlocked.CompareExchange<OnActiveHookChangedHandler>(ref this.onActiveHookChangedHandler_0,
                            onActiveHookChangedHandler1, onActiveHookChangedHandler);
                } while ((object) onActiveHookChangedHandler0 != (object) onActiveHookChangedHandler);
            }
            remove
            {
                OnActiveHookChangedHandler onActiveHookChangedHandler;
                OnActiveHookChangedHandler onActiveHookChangedHandler0 = this.onActiveHookChangedHandler_0;
                do
                {
                    onActiveHookChangedHandler = onActiveHookChangedHandler0;
                    OnActiveHookChangedHandler onActiveHookChangedHandler1 =
                        (OnActiveHookChangedHandler) Delegate.Remove(onActiveHookChangedHandler, value);
                    onActiveHookChangedHandler0 =
                        Interlocked.CompareExchange<OnActiveHookChangedHandler>(ref this.onActiveHookChangedHandler_0,
                            onActiveHookChangedHandler1, onActiveHookChangedHandler);
                } while ((object) onActiveHookChangedHandler0 != (object) onActiveHookChangedHandler);
            }
        }

        public event OnMousePostionHandler OnMousePostion
        {
            add
            {
                OnMousePostionHandler onMousePostionHandler;
                OnMousePostionHandler onMousePostionHandler0 = this.onMousePostionHandler_0;
                do
                {
                    onMousePostionHandler = onMousePostionHandler0;
                    OnMousePostionHandler onMousePostionHandler1 =
                        (OnMousePostionHandler) Delegate.Combine(onMousePostionHandler, value);
                    onMousePostionHandler0 =
                        Interlocked.CompareExchange<OnMousePostionHandler>(ref this.onMousePostionHandler_0,
                            onMousePostionHandler1, onMousePostionHandler);
                } while ((object) onMousePostionHandler0 != (object) onMousePostionHandler);
            }
            remove
            {
                OnMousePostionHandler onMousePostionHandler;
                OnMousePostionHandler onMousePostionHandler0 = this.onMousePostionHandler_0;
                do
                {
                    onMousePostionHandler = onMousePostionHandler0;
                    OnMousePostionHandler onMousePostionHandler1 =
                        (OnMousePostionHandler) Delegate.Remove(onMousePostionHandler, value);
                    onMousePostionHandler0 =
                        Interlocked.CompareExchange<OnMousePostionHandler>(ref this.onMousePostionHandler_0,
                            onMousePostionHandler1, onMousePostionHandler);
                } while ((object) onMousePostionHandler0 != (object) onMousePostionHandler);
            }
        }
    }
}