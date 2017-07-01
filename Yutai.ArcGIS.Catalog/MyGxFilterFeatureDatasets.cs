using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog
{
    public class MyGxFilterFeatureDatasets : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if (igxObject_0 is IGxDataset)
            {
                if ((igxObject_0 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
                    return true;
                }
            }
            else
            {
                if (igxObject_0 is IGxObjectContainer)
                {
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                    return true;
                }
                if (igxObject_0 is IGxBasicObject)
                {
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRDefault;
                    return true;
                }
            }
            return false;
        }

        public bool CanDisplayObject(IGxObject igxObject_0)
        {
            if (igxObject_0 is IGxDataset)
            {
                if ((igxObject_0 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    return true;
                }
            }
            else
            {
                if (igxObject_0 is IGxObjectContainer)
                {
                    return true;
                }
                if (igxObject_0 is IGxBasicObject)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
        {
            if (igxObject_0 is IGxDatabase)
            {
                bool_0 = this.method_0((igxObject_0 as IGxDatabase).Workspace, string_0.ToLower());
                return true;
            }
            return false;
        }

        private bool method_0(IWorkspace iworkspace_0, string string_0)
        {
            IEnumDatasetName name = iworkspace_0.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
            name.Reset();
            for (IDatasetName name2 = name.Next(); name2 != null; name2 = name.Next())
            {
                string[] strArray = name2.Name.Split(new char[] {'.'});
                string str = strArray[strArray.Length - 1].ToLower();
                if (string_0 == str)
                {
                    return true;
                }
            }
            return false;
        }

        public string Description
        {
            get { return "要素集"; }
        }

        public string Name
        {
            get { return "要素集"; }
        }
    }
}