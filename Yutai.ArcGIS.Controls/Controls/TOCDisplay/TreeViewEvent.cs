using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public class TreeViewEvent
    {
        public static  event ChildLayersDeletedHandler ChildLayersDeleted;

        public static  event GroupLayerAddLayerChangedHandler GroupLayerAddLayerChanged;

        public static  event GroupLayerEventHandler GroupLayerEvent;

        public static  event LayerNameChangedHandler LayerNameChanged;

        public static  event LayerOrderChangedHandler LayerOrderChanged;

        public static  event LayerPropertyChangedHandler LayerPropertyChanged;

        public static  event LayerVisibleChangedHandler LayerVisibleChanged;

        public static  event MapNameChangedHandler MapNameChanged;

        public static  event UnGroupLayerEventHandler UnGroupLayerEvent;

        public static void OnChildLayersDeleted(object sender, object pObj)
        {
            if (ChildLayersDeleted != null)
            {
                ChildLayersDeleted(sender, pObj);
            }
        }

        public static void OnGroupLayerAddLayerChanged(object sender, IBasicMap pMap, ILayer pLayer)
        {
            if (GroupLayerAddLayerChanged != null)
            {
                GroupLayerAddLayerChanged(sender, pMap, pLayer);
            }
        }

        public static void OnGroupLayerEvent(object sender)
        {
            if (GroupLayerEvent != null)
            {
                GroupLayerEvent(sender);
            }
        }

        public static void OnLayerNameChanged(object sender, IBasicMap pMap, ILayer pLayer)
        {
            if (LayerNameChanged != null)
            {
                LayerNameChanged(sender, pMap, pLayer);
            }
        }

        public static void OnLayerOrderChanged(object sender)
        {
            if (LayerOrderChanged != null)
            {
                LayerOrderChanged(sender);
            }
        }

        public static void OnLayerPropertyChanged(object sender, IBasicMap pMap, ILayer pLayer)
        {
            if (LayerPropertyChanged != null)
            {
                LayerPropertyChanged(sender, pMap, pLayer);
            }
        }

        public static void OnLayerVisibleChanged(object sender, IBasicMap pMap, ILayer pLayer)
        {
            if (LayerVisibleChanged != null)
            {
                LayerVisibleChanged(sender, pMap, pLayer);
            }
        }

        public static void OnMapNameChanged(object sender, IBasicMap pMap)
        {
            if (MapNameChanged != null)
            {
                MapNameChanged(sender, pMap);
            }
        }

        public static void OnUnGroupLayerEvent(object sender)
        {
            if (UnGroupLayerEvent != null)
            {
                UnGroupLayerEvent(sender);
            }
        }

        public delegate void ChildLayersDeletedHandler(object sender, object pObj);

        public delegate void GroupLayerAddLayerChangedHandler(object sender, IBasicMap pMap, ILayer pLayer);

        public delegate void GroupLayerEventHandler(object sender);

        public delegate void LayerNameChangedHandler(object sender, IBasicMap pMap, ILayer pLayer);

        public delegate void LayerOrderChangedHandler(object sender);

        public delegate void LayerPropertyChangedHandler(object sender, IBasicMap pMap, ILayer pLayer);

        public delegate void LayerVisibleChangedHandler(object sender, IBasicMap pMap, ILayer pLayer);

        public delegate void MapNameChangedHandler(object sender, IBasicMap pMap);

        public delegate void UnGroupLayerEventHandler(object sender);
    }
}

