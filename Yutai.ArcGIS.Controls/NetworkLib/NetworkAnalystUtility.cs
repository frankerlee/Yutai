using System.Collections;
using System.Collections.Specialized;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalyst;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class NetworkAnalystUtility
    {
        public static bool AddLocation(INALayer layer, string locationClassName, IPoint point, string locationName,
            StringObjectDictionary locationProperties, double tolerance)
        {
            INALocator locator = layer.Context.Locator;
            locator.SnapTolerance = tolerance;
            INALocation location = null;
            IPoint outPoint = null;
            double distanceFromPoint = 0.0;
            locator.QueryLocationByPoint(point, ref location, ref outPoint, ref distanceFromPoint);
            INAClass class2 = layer.Context.NAClasses.get_ItemByName(locationClassName) as INAClass;
            IFeatureClass class3 = class2 as IFeatureClass;
            IFeature feature = class3.CreateFeature();
            feature.Shape = point;
            int num2 = class3.FeatureCount(null);
            int index = -1;
            IClass class4 = class3;
            INALocationObject obj2 = feature as INALocationObject;
            obj2.NALocation = location;
            index = class4.FindField(NetworkConstants.NAME_FIELD);
            if (locationName.Trim().Length > 0)
            {
                feature.set_Value(index, locationName);
            }
            index = class4.FindField(NetworkConstants.SEQUENCE_FIELD);
            if (index >= 0)
            {
                feature.set_Value(index, num2);
            }
            index = class4.FindField(NetworkConstants.STATUS_FIELD);
            if (location.IsLocated)
            {
                feature.set_Value(index, 0);
            }
            else
            {
                feature.set_Value(index, 1);
            }
            if ((locationProperties != null) && (locationProperties.Count > 0))
            {
                IEnumerator enumerator = locationProperties.Keys.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    string current = enumerator.Current as string;
                    if ((current != null) && (current.Length > 0))
                    {
                        index = class4.FindField(current);
                        if (index != -1)
                        {
                            feature.set_Value(index, locationProperties[current]);
                        }
                    }
                }
            }
            feature.Store();
            return location.IsLocated;
        }

        public static void GetClasses(INALayer layer, out NameValueCollection inputClasses,
            out NameValueCollection outputClasses)
        {
            INamedSet nAClasses = layer.Context.NAClasses;
            inputClasses = new NameValueCollection();
            outputClasses = new NameValueCollection();
            for (int i = 0; i < nAClasses.Count; i++)
            {
                INAClass class2 = nAClasses.get_Item(i) as INAClass;
                string name = class2.ClassDefinition.Name;
                string str2 = layer.get_LayerByNAClassName(name).Name;
                if (class2.ClassDefinition.IsInput)
                {
                    inputClasses.Add(str2, name);
                }
                else
                {
                    outputClasses.Add(str2, name);
                }
            }
        }

        public static string GetDirectionsStringType(esriDirectionsStringType type)
        {
            switch (type)
            {
                case esriDirectionsStringType.esriDSTGeneral:
                    return "Directions";

                case esriDirectionsStringType.esriDSTDepart:
                    return "Depart";

                case esriDirectionsStringType.esriDSTArrive:
                    return "Arrive";

                case esriDirectionsStringType.esriDSTLength:
                    return "Length";

                case esriDirectionsStringType.esriDSTTime:
                    return "Time";

                case esriDirectionsStringType.esriDSTSummary:
                    return "Summary";
            }
            return "Default";
        }

        public static Hashtable GetNALayers(IMap map)
        {
            Hashtable hashtable = new Hashtable();
            IEnumLayer layer = map.get_Layers(null, true);
            ILayer layer2 = layer.Next();
            int num = 0;
            while (layer2 != null)
            {
                if (layer2 is INALayer)
                {
                    hashtable[num] = layer2;
                }
                num++;
                layer2 = layer.Next();
            }
            return hashtable;
        }

        public static void GetPermittedAttributes(INALayer layer, out string[] impedences, out string[] restrictions,
            out string[] hierarchies)
        {
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            ArrayList list3 = new ArrayList();
            INetworkDataset networkDataset = layer.Context.NetworkDataset;
            for (int i = 0; i < networkDataset.AttributeCount; i++)
            {
                INetworkAttribute attribute = networkDataset.get_Attribute(i);
                if (attribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost)
                {
                    list.Add(attribute.Name);
                }
                else if (attribute.UsageType == esriNetworkAttributeUsageType.esriNAUTRestriction)
                {
                    list2.Add(attribute.Name);
                }
                else if (attribute.UsageType == esriNetworkAttributeUsageType.esriNAUTHierarchy)
                {
                    list3.Add(attribute.Name);
                }
            }
            impedences = (string[]) list.ToArray(typeof(string));
            restrictions = (string[]) list2.ToArray(typeof(string));
            hierarchies = (string[]) list3.ToArray(typeof(string));
        }

        public static ILayer LayerFromLayerID(IMap map, int layerID)
        {
            IEnumLayer layer = map.get_Layers(null, true);
            ILayer layer2 = layer.Next();
            int num = 0;
            while (layer2 != null)
            {
                if (num == layerID)
                {
                    return layer2;
                }
                num++;
                layer2 = layer.Next();
            }
            return null;
        }

        public static int LayerIDFromLayer(IMap map, ILayer layer)
        {
            IEnumLayer layer2 = map.get_Layers(null, true);
            ILayer layer3 = layer2.Next();
            int num = 0;
            while (layer3 != null)
            {
                if (layer3 == layer)
                {
                    return num;
                }
                num++;
                layer3 = layer2.Next();
            }
            return -1;
        }
    }
}