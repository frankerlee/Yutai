using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class GxLayer : IGxObject, IGxLayer, IGxFile, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties,
        IGxObjectUI
    {
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private ILayer ilayer_0 = null;
        private string string_0 = "";

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
            return true;
        }

        public bool CanRename()
        {
            return true;
        }

        public void Close(bool bool_0)
        {
        }

        public void Delete()
        {
            try
            {
                File.Delete(this.string_0);
                this.Detach();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void Detach()
        {
            if (this.igxCatalog_0 != null)
            {
                this.igxCatalog_0.ObjectDeleted(this);
            }
            if (this.igxObject_0 is IGxObjectContainer)
            {
                (this.igxObject_0 as IGxObjectContainer).DeleteChild(this);
            }
            this.igxObject_0 = null;
            this.igxCatalog_0 = null;
        }

        public void Edit()
        {
        }

        public void EditProperties(int int_0)
        {
        }

        public void GetPropByIndex(int int_0, ref string string_1, ref object object_0)
        {
        }

        public object GetProperty(string string_1)
        {
            return null;
        }

        private LayerType method_0(IFeatureClass ifeatureClass_0)
        {
            if (ifeatureClass_0.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                return LayerType.AnnoLayer;
            }
            switch (ifeatureClass_0.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    return LayerType.PointLayer;

                case esriGeometryType.esriGeometryMultipoint:
                    return LayerType.PointLayer;

                case esriGeometryType.esriGeometryPolyline:
                    return LayerType.LineLayer;

                case esriGeometryType.esriGeometryPolygon:
                    return LayerType.PolygonLayer;
            }
            return LayerType.UnknownLayer;
        }

        private ILayer method_1(string string_1)
        {
            IFileName name = new FileNameClass
            {
                Path = string_1
            };
            ILayerFactoryHelper helper = new LayerFactoryHelperClass();
            try
            {
                IEnumLayer layer = helper.CreateLayersFromName(name as IName);
                layer.Reset();
                return layer.Next();
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            return null;
        }

        public void New()
        {
        }

        public void Open()
        {
        }

        public void Refresh()
        {
        }

        public void Rename(string string_1)
        {
            try
            {
                if (string_1 != null)
                {
                    string extension = System.IO.Path.GetExtension(this.string_0);
                    if (extension != null)
                    {
                        string_1 = System.IO.Path.GetFileNameWithoutExtension(string_1) + extension;
                    }
                    string_1 = System.IO.Path.GetDirectoryName(this.string_0) + string_1;
                    File.Move(this.string_0, string_1);
                    this.string_0 = string_1;
                    this.igxCatalog_0.ObjectChanged(this);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void Save()
        {
        }

        public void SetProperty(string string_1, object object_0)
        {
        }

        public override string ToString()
        {
            return this.FullName;
        }

        public string BaseName
        {
            get { return System.IO.Path.GetFileNameWithoutExtension(this.string_0); }
        }

        public string Category
        {
            get { return "图层"; }
        }

        public UID ClassID
        {
            get { return null; }
        }

        public UID ContextMenu
        {
            get { return null; }
        }

        public string FullName
        {
            get { return this.string_0; }
        }

        public IName InternalObjectName
        {
            get
            {
                IFileName name = new FileNameClass
                {
                    Path = this.string_0
                };
                return (name as IName);
            }
        }

        public bool IsValid
        {
            get { return false; }
        }

        IName IGxObjectInternalName.InternalObjectName
        {
            get
            {
                IFileName name = new FileNameClass
                {
                    Path = this.string_0
                };
                return (name as IName);
            }
            set { }
        }

        public Bitmap LargeImage
        {
            get { return null; }
        }

        public Bitmap LargeSelectedImage
        {
            get { return null; }
        }

        public ILayer Layer
        {
            get
            {
                if (this.ilayer_0 != null)
                {
                    return this.ilayer_0;
                }
                return this.method_1(this.string_0);
            }
            set { this.ilayer_0 = value; }
        }

        public LayerType LayerType
        {
            get
            {
                if (this.Layer != null)
                {
                    if (this.Layer is IGroupLayer)
                    {
                        return LayerType.GroupLayer;
                    }
                    if (this.Layer is IRasterLayer)
                    {
                        return LayerType.RasterLayer;
                    }
                    if (this.Layer is IFeatureLayer)
                    {
                        IFeatureClass featureClass = (this.Layer as IFeatureLayer).FeatureClass;
                        if (featureClass == null)
                        {
                            return LayerType.UnknownLayer;
                        }
                        return this.method_0(featureClass);
                    }
                }
                return LayerType.UnknownLayer;
            }
        }

        public string Name
        {
            get { return System.IO.Path.GetFileName(this.string_0); }
        }

        public UID NewMenu
        {
            get { return null; }
        }

        public IGxObject Parent
        {
            get { return this.igxObject_0; }
        }

        public string Path
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }

        public int PropertyCount
        {
            get { return 0; }
        }

        public Bitmap SmallImage
        {
            get
            {
                try
                {
                    IFeatureClass featureClass;
                    if (this.ilayer_0 != null)
                    {
                        if (this.ilayer_0 is IGroupLayer)
                        {
                            return ImageLib.GetSmallImage(35);
                        }
                        if (this.ilayer_0 is IRasterLayer)
                        {
                            return ImageLib.GetSmallImage(36);
                        }
                        if (this.ilayer_0 is IFeatureLayer)
                        {
                            featureClass = (this.ilayer_0 as IFeatureLayer).FeatureClass;
                            if (featureClass != null)
                            {
                                switch (featureClass.ShapeType)
                                {
                                    case esriGeometryType.esriGeometryPoint:
                                    case esriGeometryType.esriGeometryMultipoint:
                                        return ImageLib.GetSmallImage(32);

                                    case esriGeometryType.esriGeometryPolyline:
                                    case esriGeometryType.esriGeometryPath:
                                    case esriGeometryType.esriGeometryRay:
                                        return ImageLib.GetSmallImage(33);

                                    case esriGeometryType.esriGeometryPolygon:
                                        return ImageLib.GetSmallImage(34);
                                }
                            }
                        }
                    }
                    else
                    {
                        ILayer layer = this.method_1(this.string_0);
                        if (layer != null)
                        {
                            if (layer is IGroupLayer)
                            {
                                return ImageLib.GetSmallImage(35);
                            }
                            if (layer is IRasterLayer)
                            {
                                return ImageLib.GetSmallImage(36);
                            }
                            if (layer is IFeatureLayer)
                            {
                                featureClass = (layer as IFeatureLayer).FeatureClass;
                                if (featureClass != null)
                                {
                                    switch (featureClass.ShapeType)
                                    {
                                        case esriGeometryType.esriGeometryPoint:
                                        case esriGeometryType.esriGeometryMultipoint:
                                            return ImageLib.GetSmallImage(32);

                                        case esriGeometryType.esriGeometryPolyline:
                                        case esriGeometryType.esriGeometryPath:
                                        case esriGeometryType.esriGeometryRay:
                                            return ImageLib.GetSmallImage(33);

                                        case esriGeometryType.esriGeometryPolygon:
                                            return ImageLib.GetSmallImage(34);
                                    }
                                }
                                featureClass = null;
                            }
                        }
                    }
                }
                catch
                {
                }
                return ImageLib.GetSmallImage(37);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                try
                {
                    IFeatureClass featureClass;
                    if (this.ilayer_0 != null)
                    {
                        if (this.ilayer_0 is IGroupLayer)
                        {
                            return ImageLib.GetSmallImage(35);
                        }
                        if (this.ilayer_0 is IRasterLayer)
                        {
                            return ImageLib.GetSmallImage(36);
                        }
                        if (this.ilayer_0 is IFeatureLayer)
                        {
                            featureClass = (this.ilayer_0 as IFeatureLayer).FeatureClass;
                            if (featureClass != null)
                            {
                                switch (featureClass.ShapeType)
                                {
                                    case esriGeometryType.esriGeometryPoint:
                                    case esriGeometryType.esriGeometryMultipoint:
                                        return ImageLib.GetSmallImage(32);

                                    case esriGeometryType.esriGeometryPolyline:
                                    case esriGeometryType.esriGeometryPath:
                                    case esriGeometryType.esriGeometryRay:
                                        return ImageLib.GetSmallImage(33);

                                    case esriGeometryType.esriGeometryPolygon:
                                        return ImageLib.GetSmallImage(34);
                                }
                            }
                        }
                    }
                    else
                    {
                        ILayer layer = this.method_1(this.string_0);
                        if (layer != null)
                        {
                            if (layer is IGroupLayer)
                            {
                                return ImageLib.GetSmallImage(35);
                            }
                            if (layer is IRasterLayer)
                            {
                                return ImageLib.GetSmallImage(36);
                            }
                            if (layer is IFeatureLayer)
                            {
                                featureClass = (layer as IFeatureLayer).FeatureClass;
                                if (featureClass != null)
                                {
                                    switch (featureClass.ShapeType)
                                    {
                                        case esriGeometryType.esriGeometryPoint:
                                        case esriGeometryType.esriGeometryMultipoint:
                                            return ImageLib.GetSmallImage(32);

                                        case esriGeometryType.esriGeometryPolyline:
                                        case esriGeometryType.esriGeometryPath:
                                        case esriGeometryType.esriGeometryRay:
                                            return ImageLib.GetSmallImage(33);

                                        case esriGeometryType.esriGeometryPolygon:
                                            return ImageLib.GetSmallImage(34);
                                    }
                                }
                                featureClass = null;
                            }
                        }
                    }
                }
                catch
                {
                }
                return ImageLib.GetSmallImage(37);
            }
        }
    }
}