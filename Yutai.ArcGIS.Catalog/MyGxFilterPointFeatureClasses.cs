using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog
{
    public class MyGxFilterPointFeatureClasses : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if (igxObject_0 is IGxDataset)
            {
                switch ((igxObject_0 as IGxDataset).Type)
                {
                    case esriDatasetType.esriDTFeatureDataset:
                        myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                        return true;

                    case esriDatasetType.esriDTFeatureClass:
                        myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
                        return true;
                }
            }
            else if (igxObject_0 is IGxObjectContainer)
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                return true;
            }
            return false;
        }

        public bool CanDisplayObject(IGxObject igxObject_0)
        {
            try
            {
                if (igxObject_0 is IGxDataset)
                {
                    switch ((igxObject_0 as IGxDataset).Type)
                    {
                        case esriDatasetType.esriDTFeatureDataset:
                            return true;

                        case esriDatasetType.esriDTFeatureClass:
                        {
                            IFeatureClassName datasetName = (igxObject_0 as IGxDataset).DatasetName as IFeatureClassName;
                            if (datasetName.FeatureType == esriFeatureType.esriFTSimple)
                            {
                                if (datasetName.ShapeType == esriGeometryType.esriGeometryNull)
                                {
                                    IFeatureClass dataset = (igxObject_0 as IGxDataset).Dataset as IFeatureClass;
                                    if ((dataset.ShapeType == esriGeometryType.esriGeometryMultipoint) ||
                                        (dataset.ShapeType == esriGeometryType.esriGeometryPoint))
                                    {
                                        return true;
                                    }
                                }
                                else if ((datasetName.ShapeType == esriGeometryType.esriGeometryMultipoint) ||
                                         (datasetName.ShapeType == esriGeometryType.esriGeometryPoint))
                                {
                                    return true;
                                }
                                goto Label_00CB;
                            }
                            break;
                        }
                    }
                }
                else if (igxObject_0 is IGxObjectContainer)
                {
                    return true;
                }
            }
            catch
            {
            }
            Label_00CB:
            return false;
        }

        public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
        {
            try
            {
                if ((igxObject_0 is IGxDiskConnection) || (igxObject_0 is IGxFolder))
                {
                    string path = (igxObject_0 as IGxFile).Path + @"\" + string_0;
                    if (System.IO.Path.GetExtension(path).ToLower() == ".shp")
                    {
                        bool_0 = File.Exists(path);
                    }
                    else
                    {
                        bool_0 = File.Exists(path + ".shp");
                    }
                    return true;
                }
                if (igxObject_0 is IGxDatabase)
                {
                    bool_0 =
                        ((igxObject_0 as IGxDatabase).Workspace as IWorkspace2).get_NameExists(
                            esriDatasetType.esriDTFeatureClass, string_0);
                    return true;
                }
                if (igxObject_0 is IGxDataset)
                {
                    bool_0 =
                        ((igxObject_0 as IGxDataset).Dataset.Workspace as IWorkspace2).get_NameExists(
                            esriDatasetType.esriDTFeatureClass, string_0);
                    if ((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        public string Description
        {
            get { return "点要素类"; }
        }

        public string Name
        {
            get { return "GxFilterPointFeatureClasses"; }
        }
    }
}