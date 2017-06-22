using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class GxCoverageDataset : IGxObject, IGxDataset, IGxObjectContainer, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget
    {
        private IDatasetName idatasetName_0 = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

        public IGxObject AddChild(IGxObject igxObject_1)
        {
            string strB = igxObject_1.Name.ToUpper();
            int num = 0;
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                IGxObject obj2 = this.igxObjectArray_0.Item(i);
                num = obj2.Name.ToUpper().CompareTo(strB);
                if (num > 0)
                {
                    this.igxObjectArray_0.Insert(i, igxObject_1);
                    return igxObject_1;
                }
                if (num == 0)
                {
                    return obj2;
                }
            }
            this.igxObjectArray_0.Insert(-1, igxObject_1);
            return igxObject_1;
        }

        public void Attach(IGxObject igxObject_1, IGxCatalog igxCatalog_1)
        {
            this.igxObject_0 = igxObject_1;
            this.igxCatalog_0 = igxCatalog_1;
            if (this.igxObject_0 is IGxObjectContainer)
            {
                (this.igxObject_0 as IGxObjectContainer).AddChild(this);
            }
        }

        public bool CanCopy()
        {
            return true;
        }

        public bool CanDelete()
        {
            IDataset dataset = this.Dataset;
            bool flag = false;
            try
            {
                flag = dataset.CanDelete();
            }
            catch
            {
            }
            dataset = null;
            return flag;
        }

        public bool CanPaste(IEnumName ienumName_0, ref bool bool_0)
        {
            return false;
        }

        public bool CanRename()
        {
            try
            {
                bool flag = this.Dataset.CanRename();
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public void Delete()
        {
            if (this.CanDelete())
            {
                try
                {
                    this.Dataset.Delete();
                    this.Detach();
                }
                catch
                {
                }
            }
        }

        public void DeleteChild(IGxObject igxObject_1)
        {
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                if (this.igxObjectArray_0.Item(i) == igxObject_1)
                {
                    this.igxObjectArray_0.Remove(i);
                    break;
                }
            }
        }

        public void Detach()
        {
            if (this.igxCatalog_0 != null)
            {
                if (this.igxCatalog_0 != null)
                {
                    this.igxCatalog_0.ObjectDeleted(this);
                }
                if (this.igxObject_0 is IGxObjectContainer)
                {
                    (this.igxObject_0 as IGxObjectContainer).DeleteChild(this);
                }
                this.igxCatalog_0 = null;
                this.igxObject_0 = null;
            }
        }

        public void EditProperties(int int_0)
        {
            new frmCoveragePropertySheet { CoverageName = this.idatasetName_0 as ICoverageName }.ShowDialog();
        }

        public void GetPropByIndex(int int_0, ref string string_0, ref object object_0)
        {
        }

        public object GetProperty(string string_0)
        {
            return null;
        }

        private esriCoverageType method_0()
        {
            IEnumGxObject children = this.Children;
            if ((children as IGxObjectArray).Count != 1)
            {
                IGxDataset dataset;
                ICoverageFeatureClassName datasetName;
                if ((children as IGxObjectArray).Count == 2)
                {
                    children.Reset();
                    for (dataset = children.Next() as IGxDataset; dataset != null; dataset = children.Next() as IGxDataset)
                    {
                        datasetName = dataset.DatasetName as ICoverageFeatureClassName;
                        switch (datasetName.FeatureClassType)
                        {
                            case esriCoverageFeatureClassType.esriCFCTPoint:
                                return esriCoverageType.esriPointCoverage;

                            case esriCoverageFeatureClassType.esriCFCTArc:
                                return esriCoverageType.esriLineCoverage;

                            case esriCoverageFeatureClassType.esriCFCTAnnotation:
                                return esriCoverageType.esriAnnotationCoverage;
                        }
                    }
                }
                else
                {
                    if ((children as IGxObjectArray).Count == 3)
                    {
                        return esriCoverageType.esriLineCoverage;
                    }
                    if ((children as IGxObjectArray).Count == 4)
                    {
                        children.Reset();
                        for (dataset = children.Next() as IGxDataset; dataset != null; dataset = children.Next() as IGxDataset)
                        {
                            datasetName = dataset.DatasetName as ICoverageFeatureClassName;
                            switch (datasetName.FeatureClassType)
                            {
                                case esriCoverageFeatureClassType.esriCFCTPolygon:
                                    return esriCoverageType.esriPolygonCoverage;

                                case esriCoverageFeatureClassType.esriCFCTRegion:
                                    return esriCoverageType.esriPreliminaryPolygonCoverage;
                            }
                        }
                    }
                    else if ((children as IGxObjectArray).Count > 4)
                    {
                        return esriCoverageType.esriPolygonCoverage;
                    }
                }
            }
            return esriCoverageType.esriEmptyCoverage;
        }

        private void method_1()
        {
            try
            {
                IEnumDatasetName featureClassNames = (this.idatasetName_0 as IFeatureDatasetName2).FeatureClassNames;
                featureClassNames.Reset();
                IDatasetName name2 = featureClassNames.Next();
                IGxObject obj2 = null;
                while (name2 != null)
                {
                    obj2 = new GxCoverageDataset();
                    if (obj2 != null)
                    {
                        (obj2 as IGxDataset).DatasetName = name2;
                        obj2.Attach(this, this.igxCatalog_0);
                    }
                    name2 = featureClassNames.Next();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误");
            }
        }

        public bool Paste(IEnumName ienumName_0, ref bool bool_0)
        {
            return false;
        }

        public void Refresh()
        {
            if (this.igxCatalog_0 != null)
            {
                if ((this.idatasetName_0.Type == esriDatasetType.esriDTContainer) || (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset))
                {
                    this.igxObjectArray_0.Empty();
                    this.method_1();
                }
                this.igxCatalog_0.ObjectRefreshed(this);
            }
        }

        public void Rename(string string_0)
        {
            try
            {
                if (string_0 != null)
                {
                    this.Dataset.Rename(string_0);
                    this.idatasetName_0.Name = string_0;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误");
            }
        }

        public void SearchChildren(string string_0, IGxObjectArray igxObjectArray_1)
        {
        }

        public void SetProperty(string string_0, object object_0)
        {
        }

        public bool AreChildrenViewable
        {
            get
            {
                return (this.idatasetName_0 is ICoverageName);
            }
        }

        public string BaseName
        {
            get
            {
                return this.idatasetName_0.Name;
            }
        }

        public string Category
        {
            get
            {
                if (this.idatasetName_0 is ICoverageName)
                {
                    switch (this.method_0())
                    {
                        case esriCoverageType.esriEmptyCoverage:
                            return "空Coverage";

                        case esriCoverageType.esriAnnotationCoverage:
                            return "注记Coverage";

                        case esriCoverageType.esriPointCoverage:
                            return "点Coverage";

                        case esriCoverageType.esriLineCoverage:
                            return "线Coverage";

                        case esriCoverageType.esriPolygonCoverage:
                            return "面Coverage";

                        case esriCoverageType.esriPreliminaryPolygonCoverage:
                            return "区域Coverage";
                    }
                }
                else if (this.idatasetName_0 is ICoverageFeatureClassName)
                {
                    switch ((this.idatasetName_0 as ICoverageFeatureClassName).FeatureClassType)
                    {
                        case esriCoverageFeatureClassType.esriCFCTPoint:
                            return "Coverage点要素类";

                        case esriCoverageFeatureClassType.esriCFCTArc:
                            return "Arc要素类";

                        case esriCoverageFeatureClassType.esriCFCTPolygon:
                            return "Coverage面要素类";

                        case esriCoverageFeatureClassType.esriCFCTNode:
                            return "Coverage节点要素类";

                        case esriCoverageFeatureClassType.esriCFCTTic:
                            return "Tic要素类";

                        case esriCoverageFeatureClassType.esriCFCTAnnotation:
                            return "Coverage注记要素类";

                        case esriCoverageFeatureClassType.esriCFCTSection:
                            return "Coverage片要素类";

                        case esriCoverageFeatureClassType.esriCFCTRoute:
                            return "路径要素类";

                        case esriCoverageFeatureClassType.esriCFCTLink:
                            return "Coverage连接要素类";

                        case esriCoverageFeatureClassType.esriCFCTRegion:
                            return "区域要素类";

                        case esriCoverageFeatureClassType.esriCFCTLabel:
                            return "Coverage标注要素类";

                        case esriCoverageFeatureClassType.esriCFCTFile:
                            return "文件要素类";
                    }
                }
                return "Coverage";
            }
        }

        public IEnumGxObject Children
        {
            get
            {
                if (this.HasChildren && (this.igxObjectArray_0.Count == 0))
                {
                    this.method_1();
                }
                return (this.igxObjectArray_0 as IEnumGxObject);
            }
        }

        public UID ClassID
        {
            get
            {
                return null;
            }
        }

        public UID ContextMenu
        {
            get
            {
                return null;
            }
        }

        public IDataset Dataset
        {
            get
            {
                if (this.idatasetName_0 != null)
                {
                    try
                    {
                        return ((this.idatasetName_0 as IName).Open() as IDataset);
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("",exception, "打开");
                    }
                }
                return null;
            }
        }

        public IDatasetName DatasetName
        {
            get
            {
                return this.idatasetName_0;
            }
            set
            {
                this.idatasetName_0 = value;
            }
        }

        public string FullName
        {
            get
            {
                string pathName = this.idatasetName_0.WorkspaceName.PathName;
                if (this.idatasetName_0 is ICoverageFeatureClassName)
                {
                    IDatasetName featureDatasetName = (this.idatasetName_0 as IFeatureClassName).FeatureDatasetName;
                    if (featureDatasetName != null)
                    {
                        pathName = pathName + @"\" + featureDatasetName.Name;
                    }
                }
                return (pathName + @"\" + this.idatasetName_0.Name);
            }
        }

        public bool HasChildren
        {
            get
            {
                return (this.idatasetName_0 is ICoverageName);
            }
        }

        public IName InternalObjectName
        {
            get
            {
                return (this.idatasetName_0 as IName);
            }
        }

        public bool IsValid
        {
            get
            {
                return (this.idatasetName_0 != null);
            }
        }

        IName IGxObjectInternalName.InternalObjectName
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public Bitmap LargeImage
        {
            get
            {
                return null;
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                return null;
            }
        }

        public string Name
        {
            get
            {
                return this.idatasetName_0.Name;
            }
        }

        public UID NewMenu
        {
            get
            {
                return null;
            }
        }

        public IGxObject Parent
        {
            get
            {
                return this.igxObject_0;
            }
        }

        public int PropertyCount
        {
            get
            {
                return 0;
            }
        }

        public Bitmap SmallImage
        {
            get
            {
                if (this.idatasetName_0 is ICoverageName)
                {
                    int num = (int) this.method_0();
                    return ImageLib.GetSmallImage(57 + num);
                }
                if (this.idatasetName_0 is ICoverageFeatureClassName)
                {
                    switch ((this.idatasetName_0 as ICoverageFeatureClassName).FeatureClassType)
                    {
                        case esriCoverageFeatureClassType.esriCFCTPoint:
                            return ImageLib.GetSmallImage(63);

                        case esriCoverageFeatureClassType.esriCFCTArc:
                            return ImageLib.GetSmallImage(64);

                        case esriCoverageFeatureClassType.esriCFCTPolygon:
                            return ImageLib.GetSmallImage(65);

                        case esriCoverageFeatureClassType.esriCFCTNode:
                            return ImageLib.GetSmallImage(66);

                        case esriCoverageFeatureClassType.esriCFCTTic:
                            return ImageLib.GetSmallImage(67);

                        case esriCoverageFeatureClassType.esriCFCTAnnotation:
                            return ImageLib.GetSmallImage(68);

                        case esriCoverageFeatureClassType.esriCFCTSection:
                            return ImageLib.GetSmallImage(72);

                        case esriCoverageFeatureClassType.esriCFCTRoute:
                            return ImageLib.GetSmallImage(69);

                        case esriCoverageFeatureClassType.esriCFCTLink:
                            return ImageLib.GetSmallImage(72);

                        case esriCoverageFeatureClassType.esriCFCTRegion:
                            return ImageLib.GetSmallImage(70);

                        case esriCoverageFeatureClassType.esriCFCTLabel:
                            return ImageLib.GetSmallImage(71);

                        case esriCoverageFeatureClassType.esriCFCTFile:
                            return ImageLib.GetSmallImage(72);
                    }
                }
                return ImageLib.GetSmallImage(57);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                if (this.idatasetName_0 is ICoverageName)
                {
                    int num = (int) this.method_0();
                    return ImageLib.GetSmallImage(57 + num);
                }
                if (this.idatasetName_0 is ICoverageFeatureClassName)
                {
                    switch ((this.idatasetName_0 as ICoverageFeatureClassName).FeatureClassType)
                    {
                        case esriCoverageFeatureClassType.esriCFCTPoint:
                            return ImageLib.GetSmallImage(63);

                        case esriCoverageFeatureClassType.esriCFCTArc:
                            return ImageLib.GetSmallImage(64);

                        case esriCoverageFeatureClassType.esriCFCTPolygon:
                            return ImageLib.GetSmallImage(65);

                        case esriCoverageFeatureClassType.esriCFCTNode:
                            return ImageLib.GetSmallImage(66);

                        case esriCoverageFeatureClassType.esriCFCTTic:
                            return ImageLib.GetSmallImage(67);

                        case esriCoverageFeatureClassType.esriCFCTAnnotation:
                            return ImageLib.GetSmallImage(68);

                        case esriCoverageFeatureClassType.esriCFCTSection:
                            return ImageLib.GetSmallImage(72);

                        case esriCoverageFeatureClassType.esriCFCTRoute:
                            return ImageLib.GetSmallImage(69);

                        case esriCoverageFeatureClassType.esriCFCTLink:
                            return ImageLib.GetSmallImage(72);

                        case esriCoverageFeatureClassType.esriCFCTRegion:
                            return ImageLib.GetSmallImage(70);

                        case esriCoverageFeatureClassType.esriCFCTLabel:
                            return ImageLib.GetSmallImage(71);

                        case esriCoverageFeatureClassType.esriCFCTFile:
                            return ImageLib.GetSmallImage(72);
                    }
                }
                return ImageLib.GetSmallImage(57);
            }
        }

        public esriDatasetType Type
        {
            get
            {
                if (this.idatasetName_0 != null)
                {
                    return this.idatasetName_0.Type;
                }
                return esriDatasetType.esriDTAny;
            }
        }
    }
}

