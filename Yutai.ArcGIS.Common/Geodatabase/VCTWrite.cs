using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class VCTWrite : IConvertEvent, ESRI.ArcGIS.Geodatabase.IFeatureProgress_Event, IProgressMessage
    {
        private double double_0 = 1;

        private IFields ifields_0 = null;

        private IGeometry igeometry_0 = null;

        private bool bool_0 = false;

        private string string_0 = "MBBSM_VCT";

        private IDataset idataset_0 = null;

        private ISpatialReference ispatialReference_0 = null;

        private IList ilist_0 = new ArrayList();

        private IList ilist_1 = new ArrayList();

        private IEnvelope ienvelope_0 = null;

        private ProgressMessageHandle progressMessageHandle_0;

        private ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler ifeatureProgress_StepEventHandler_0;

        private SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler_0;

        private SetFeatureCountEnventHandler setFeatureCountEnventHandler_0;

        private SetMaxValueHandler setMaxValueHandler_0;

        private SetMinValueHandler setMinValueHandler_0;

        private SetPositionHandler setPositionHandler_0;

        private SetMessageHandler setMessageHandler_0;

        private FinishHander finishHander_0;

        public IGeometry ClipGeometry
        {
            set { this.igeometry_0 = value; }
        }

        public IDataset Dataset
        {
            set
            {
                string name;
                char[] chrArray;
                string[] strArrays;
                this.idataset_0 = value;
                this.ilist_0.Clear();
                this.ilist_1.Clear();
                if (value is IFeatureClass)
                {
                    name = value.Name;
                    chrArray = new char[] {'.'};
                    strArrays = name.Split(chrArray);
                    name = strArrays[(int) strArrays.Length - 1];
                    this.ilist_0.Add(value);
                    this.ilist_1.Add(name);
                }
                else if (value is IFeatureDataset)
                {
                    IEnumDataset subsets = (value as IFeatureDataset).Subsets;
                    subsets.Reset();
                    for (IFeatureClass i = subsets.Next() as IFeatureClass;
                        i != null;
                        i = subsets.Next() as IFeatureClass)
                    {
                        if ((i.FeatureType == esriFeatureType.esriFTSimple
                            ? true
                            : i.FeatureType == esriFeatureType.esriFTAnnotation))
                        {
                            name = (i as IDataset).Name;
                            chrArray = new char[] {'.'};
                            strArrays = name.Split(chrArray);
                            name = strArrays[(int) strArrays.Length - 1];
                            this.ilist_0.Add(i);
                            this.ilist_1.Add(name);
                        }
                    }
                }
            }
        }

        public bool IsClip
        {
            set { this.bool_0 = value; }
        }

        public string MBBSM
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }

        public VCTWrite()
        {
            IObjectClassDescription annotationFeatureClassDescriptionClass = new AnnotationFeatureClassDescription();
            this.ifields_0 = (annotationFeatureClassDescriptionClass.RequiredFields as IClone).Clone() as IFields;
        }

        public bool AddDataset(IDataset idataset_1)
        {
            string name;
            char[] chrArray;
            string[] strArrays;
            if (this.ispatialReference_0 == null)
            {
                this.ispatialReference_0 = (idataset_1 as IGeoDataset).SpatialReference;
            }
            if (idataset_1 is IFeatureClass)
            {
                name = idataset_1.Name;
                chrArray = new char[] {'.'};
                strArrays = name.Split(chrArray);
                name = strArrays[(int) strArrays.Length - 1];
                this.ilist_0.Add(idataset_1);
                this.ilist_1.Add(name);
            }
            else if (idataset_1 is IFeatureDataset)
            {
                IEnumDataset subsets = (idataset_1 as IFeatureDataset).Subsets;
                subsets.Reset();
                for (IDataset i = subsets.Next(); i != null; i = subsets.Next())
                {
                    IFeatureClass featureClass = i as IFeatureClass;
                    if (featureClass != null)
                    {
                        if ((featureClass.FeatureType == esriFeatureType.esriFTSimple
                            ? true
                            : featureClass.FeatureType == esriFeatureType.esriFTAnnotation))
                        {
                            name = (featureClass as IDataset).Name;
                            chrArray = new char[] {'.'};
                            strArrays = name.Split(chrArray);
                            name = strArrays[(int) strArrays.Length - 1];
                            this.ilist_0.Add(featureClass);
                            this.ilist_1.Add(name);
                        }
                    }
                }
            }
            return true;
        }

        private IGeometry method_0(IGeometry igeometry_1)
        {
            IGeometry igeometry1;
            if (this.igeometry_0 == null)
            {
                igeometry1 = igeometry_1;
            }
            else if (this.bool_0)
            {
                bool flag = false;
                try
                {
                    flag = (this.igeometry_0 as IRelationalOperator).Contains(igeometry_1);
                }
                catch
                {
                }
                if (!flag)
                {
                    IGeometry zAware = null;
                    ITopologicalOperator igeometry0 = (ITopologicalOperator) this.igeometry_0;
                    if (igeometry_1.GeometryType == esriGeometryType.esriGeometryMultipoint)
                    {
                        zAware = igeometry0.Intersect(igeometry_1, esriGeometryDimension.esriGeometry0Dimension);
                        (zAware as IZAware).ZAware = (igeometry_1 as IZAware).ZAware;
                        (zAware as IMAware).MAware = (igeometry_1 as IMAware).MAware;
                    }
                    else if (igeometry_1.GeometryType == esriGeometryType.esriGeometryPolygon)
                    {
                        zAware = igeometry0.Intersect(igeometry_1, esriGeometryDimension.esriGeometry2Dimension);
                        (zAware as IZAware).ZAware = (igeometry_1 as IZAware).ZAware;
                        if ((zAware as IZAware).ZAware)
                        {
                            (zAware as IZ).SetConstantZ((igeometry_1 as IZ).ZMin);
                        }
                        (zAware as IMAware).MAware = (igeometry_1 as IMAware).MAware;
                    }
                    else if (igeometry_1.GeometryType != esriGeometryType.esriGeometryPolyline)
                    {
                        zAware = igeometry_1;
                    }
                    else
                    {
                        try
                        {
                            zAware = igeometry0.Intersect(igeometry_1, esriGeometryDimension.esriGeometry1Dimension);
                            (zAware as IZAware).ZAware = (igeometry_1 as IZAware).ZAware;
                            (zAware as IMAware).MAware = (igeometry_1 as IMAware).MAware;
                        }
                        catch
                        {
                            zAware = igeometry_1;
                        }
                    }
                    igeometry1 = zAware;
                }
                else
                {
                    igeometry1 = igeometry_1;
                }
            }
            else
            {
                igeometry1 = igeometry_1;
            }
            return igeometry1;
        }

        private void method_1()
        {
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                IGeoDataset item = this.ilist_0[i] as IGeoDataset;
                if (this.ienvelope_0 != null)
                {
                    this.ienvelope_0.Union(item.Extent);
                }
                else
                {
                    this.ienvelope_0 = item.Extent;
                }
            }
            this.ienvelope_0.Expand(1, 1, false);
        }

        private void method_10(StreamWriter streamWriter_0)
        {
            streamWriter_0.WriteLine("PolygonBegin");
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                IFeatureClass item = this.ilist_0[i] as IFeatureClass;
                ISpatialFilter spatialFilterClass = null;
                if (this.igeometry_0 != null)
                {
                    spatialFilterClass = new SpatialFilter()
                    {
                        GeometryField = item.ShapeFieldName,
                        Geometry = this.igeometry_0
                    };
                    if (item.ShapeType != esriGeometryType.esriGeometryPoint)
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    }
                    else
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    }
                }
                int num = item.FeatureCount(spatialFilterClass);
                if (num != 0 && item.FeatureType == esriFeatureType.esriFTSimple &&
                    item.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    string str = this.ilist_1[i].ToString();
                    string name = (item as IDataset).Name;
                    string[] strArrays = name.Split(new char[] {'.'});
                    string str1 = strArrays[(int) strArrays.Length - 1];
                    if (this.setFeatureClassNameEnventHandler_0 != null)
                    {
                        this.setFeatureClassNameEnventHandler_0(name);
                    }
                    if (this.setFeatureCountEnventHandler_0 != null)
                    {
                        this.setFeatureCountEnventHandler_0(num);
                    }
                    IFeatureCursor featureCursor = item.Search(spatialFilterClass, false);
                    for (IFeature j = featureCursor.NextFeature(); j != null; j = featureCursor.NextFeature())
                    {
                        if (this.ifeatureProgress_StepEventHandler_0 != null)
                        {
                            this.ifeatureProgress_StepEventHandler_0();
                        }
                        IPolygon shape = j.Shape as IPolygon;
                        if ((shape == null ? false : !shape.IsEmpty))
                        {
                            streamWriter_0.WriteLine(j.OID);
                            streamWriter_0.WriteLine(str);
                            streamWriter_0.WriteLine(str1);
                            IPoint labelPoint = (shape as IArea).LabelPoint;
                            double x = (double) (labelPoint.X*this.double_0);
                            string str2 = x.ToString("0.###");
                            x = (double) (labelPoint.Y*this.double_0);
                            string str3 = string.Concat(str2, ",", x.ToString("0.###"));
                            streamWriter_0.WriteLine(str3);
                            for (int k = 0; k < (shape as IGeometryCollection).GeometryCount; k++)
                            {
                                IPointCollection geometry =
                                    (shape as IGeometryCollection).Geometry[k] as IPointCollection;
                                streamWriter_0.WriteLine(geometry.PointCount.ToString());
                                for (int l = 0; l < geometry.PointCount; l++)
                                {
                                    labelPoint = geometry.Point[l];
                                    x = (double) (labelPoint.X*this.double_0);
                                    string str4 = x.ToString("0.###");
                                    x = (double) (labelPoint.Y*this.double_0);
                                    str3 = string.Concat(str4, ",", x.ToString("0.###"));
                                    streamWriter_0.WriteLine(str3);
                                }
                            }
                            streamWriter_0.WriteLine("0");
                        }
                    }
                    Marshal.ReleaseComObject(featureCursor);
                    featureCursor = null;
                }
            }
            streamWriter_0.WriteLine("PolygonEnd");
        }

        private void method_11(StreamWriter streamWriter_0)
        {
            streamWriter_0.WriteLine("AnnotationBegin");
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                IFeatureClass item = this.ilist_0[i] as IFeatureClass;
                ISpatialFilter spatialFilterClass = null;
                if (this.igeometry_0 != null)
                {
                    spatialFilterClass = new SpatialFilter()
                    {
                        GeometryField = item.ShapeFieldName,
                        Geometry = this.igeometry_0
                    };
                    if (item.ShapeType != esriGeometryType.esriGeometryPoint)
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    }
                    else
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    }
                }
                int num = item.FeatureCount(spatialFilterClass);
                if (num != 0 && item.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    string str = this.ilist_1[i].ToString();
                    string name = (item as IDataset).Name;
                    string[] strArrays = name.Split(new char[] {'.'});
                    string str1 = strArrays[(int) strArrays.Length - 1];
                    if (this.setFeatureClassNameEnventHandler_0 != null)
                    {
                        this.setFeatureClassNameEnventHandler_0(name);
                    }
                    if (this.setFeatureCountEnventHandler_0 != null)
                    {
                        this.setFeatureCountEnventHandler_0(num);
                    }
                    IFeatureCursor featureCursor = item.Search(spatialFilterClass, false);
                    for (IFeature j = featureCursor.NextFeature(); j != null; j = featureCursor.NextFeature())
                    {
                        if (this.ifeatureProgress_StepEventHandler_0 != null)
                        {
                            this.ifeatureProgress_StepEventHandler_0();
                        }
                        ITextElement annotation = (j as IAnnotationFeature).Annotation as ITextElement;
                        streamWriter_0.WriteLine(j.OID);
                        streamWriter_0.WriteLine(str);
                        streamWriter_0.WriteLine(str1);
                        streamWriter_0.WriteLine(annotation.Symbol.Font.Name);
                        streamWriter_0.WriteLine(annotation.Symbol.Color.RGB);
                        short weight = annotation.Symbol.Font.Weight;
                        short num1 = 0;
                        if (annotation.Symbol.Font.Italic)
                        {
                            num1 = 1;
                        }
                        string str2 = "F";
                        if (annotation.Symbol.Font.Underline)
                        {
                            str2 = "T";
                        }
                        string[] strArrays1 = new string[] {weight.ToString(), ",", num1.ToString(), ",", str2};
                        streamWriter_0.WriteLine(string.Concat(strArrays1));
                        double size = annotation.Symbol.Size;
                        string str3 = size.ToString();
                        size = annotation.Symbol.Size;
                        string str4 = string.Concat(str3, ",", size.ToString());
                        streamWriter_0.WriteLine(str4);
                        streamWriter_0.WriteLine("4");
                        streamWriter_0.WriteLine(annotation.Text);
                        streamWriter_0.WriteLine("1");
                        IPoint labelPoint = ((annotation as IElement).Geometry.Envelope as IArea).LabelPoint;
                        size = (double) (labelPoint.X*this.double_0);
                        string str5 = size.ToString("0.###");
                        size = (double) (labelPoint.Y*this.double_0);
                        str4 = string.Concat(str5, ",", size.ToString("0.###"));
                        streamWriter_0.WriteLine(str4);
                    }
                    Marshal.ReleaseComObject(featureCursor);
                    featureCursor = null;
                }
            }
            streamWriter_0.WriteLine("AnnotationEnd");
        }

        private void method_12(StreamWriter streamWriter_0, IFeature ifeature_0)
        {
            string str;
            int i;
            IField field;
            object value;
            IFields fields = ifeature_0.Fields;
            if (!(ifeature_0 is IAnnotationFeature))
            {
                str = ifeature_0.OID.ToString();
                for (i = 0; i < fields.FieldCount; i++)
                {
                    field = fields.Field[i];
                    if ((field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeBlob ||
                         field.Type == esriFieldType.esriFieldTypeGeometry ||
                         field.Type == esriFieldType.esriFieldTypeRaster
                            ? false
                            : field.Editable) && !(field.Name == this.string_0))
                    {
                        value = ifeature_0.Value[i];
                        str = (!(value is DBNull) ? string.Concat(str, ",", value.ToString()) : string.Concat(str, ","));
                    }
                }
                streamWriter_0.WriteLine(str);
            }
            else
            {
                str = ifeature_0.OID.ToString();
                for (i = 0; i < fields.FieldCount; i++)
                {
                    field = fields.Field[i];
                    if ((field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeBlob ||
                         field.Type == esriFieldType.esriFieldTypeGeometry ||
                         field.Type == esriFieldType.esriFieldTypeRaster
                            ? false
                            : field.Editable) && !(field.Name == this.string_0) &&
                        this.ifields_0.FindField(field.Name) == -1)
                    {
                        value = ifeature_0.Value[i];
                        str = (!(value is DBNull) ? string.Concat(str, ",", value.ToString()) : string.Concat(str, ","));
                    }
                }
                streamWriter_0.WriteLine(str);
            }
        }

        private void method_13(StreamWriter streamWriter_0)
        {
            streamWriter_0.WriteLine("AttributeBegin");
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                IFeatureClass item = this.ilist_0[i] as IFeatureClass;
                ISpatialFilter spatialFilterClass = null;
                if (this.igeometry_0 != null)
                {
                    spatialFilterClass = new SpatialFilter()
                    {
                        GeometryField = item.ShapeFieldName,
                        Geometry = this.igeometry_0
                    };
                    if (item.ShapeType != esriGeometryType.esriGeometryPoint)
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    }
                    else
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    }
                }
                int num = item.FeatureCount(spatialFilterClass);
                if (num != 0)
                {
                    string name = (item as IDataset).Name;
                    string[] strArrays = name.Split(new char[] {'.'});
                    streamWriter_0.WriteLine(strArrays[(int) strArrays.Length - 1]);
                    if (this.setFeatureClassNameEnventHandler_0 != null)
                    {
                        this.setFeatureClassNameEnventHandler_0(name);
                    }
                    if (this.setFeatureCountEnventHandler_0 != null)
                    {
                        this.setFeatureCountEnventHandler_0(num);
                    }
                    IFeatureCursor featureCursor = item.Search(spatialFilterClass, false);
                    for (IFeature j = featureCursor.NextFeature(); j != null; j = featureCursor.NextFeature())
                    {
                        if (this.ifeatureProgress_StepEventHandler_0 != null)
                        {
                            this.ifeatureProgress_StepEventHandler_0();
                        }
                        this.method_12(streamWriter_0, j);
                    }
                    Marshal.ReleaseComObject(featureCursor);
                    featureCursor = null;
                    streamWriter_0.WriteLine("TableEnd");
                }
            }
            streamWriter_0.WriteLine("AttributeEnd");
        }

        private string method_2(IFeatureClass ifeatureClass_0)
        {
            string str;
            if (ifeatureClass_0.FeatureType != esriFeatureType.esriFTAnnotation)
            {
                switch (ifeatureClass_0.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                    {
                        str = "Point";
                        break;
                    }
                    case esriGeometryType.esriGeometryMultipoint:
                    {
                        str = "";
                        break;
                    }
                    case esriGeometryType.esriGeometryPolyline:
                    {
                        str = "Line";
                        break;
                    }
                    case esriGeometryType.esriGeometryPolygon:
                    {
                        str = "Polygon";
                        break;
                    }
                    default:
                    {
                        goto case esriGeometryType.esriGeometryMultipoint;
                    }
                }
            }
            else
            {
                str = "Annotation";
            }
            return str;
        }

        private void method_3(StreamWriter streamWriter_0)
        {
            streamWriter_0.WriteLine("FeatureCodeBegin");
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                string str = string.Concat(this.ilist_1[i].ToString(), ", ");
                IFeatureClass item = this.ilist_0[i] as IFeatureClass;
                string[] aliasName = new string[] {str, item.AliasName, ", ", this.method_2(item), ", 0, "};
                str = string.Concat(aliasName);
                string name = (item as IDataset).Name;
                string[] strArrays = name.Split(new char[] {'.'});
                str = string.Concat(str, strArrays[(int) strArrays.Length - 1]);
                streamWriter_0.WriteLine(str);
            }
            streamWriter_0.WriteLine("FeatureCodeEnd");
        }

        private int method_4(IFeatureClass ifeatureClass_0)
        {
            int i;
            IField field;
            int num = 0;
            IFields fields = ifeatureClass_0.Fields;
            if (ifeatureClass_0.FeatureType != esriFeatureType.esriFTAnnotation)
            {
                for (i = 0; i < fields.FieldCount; i++)
                {
                    field = fields.Field[i];
                    if ((field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeBlob ||
                         field.Type == esriFieldType.esriFieldTypeGeometry ||
                         field.Type == esriFieldType.esriFieldTypeRaster
                            ? false
                            : field.Editable) && !(field.Name == this.string_0))
                    {
                        num++;
                    }
                }
            }
            else
            {
                for (i = 0; i < fields.FieldCount; i++)
                {
                    field = fields.Field[i];
                    if ((field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeBlob ||
                         field.Type == esriFieldType.esriFieldTypeGeometry ||
                         field.Type == esriFieldType.esriFieldTypeRaster
                            ? false
                            : field.Editable) && !(field.Name == this.string_0) &&
                        this.ifields_0.FindField(field.Name) == -1)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        private string method_5(IField ifield_0)
        {
            int precision;
            string str = "";
            switch (ifield_0.Type)
            {
                case esriFieldType.esriFieldTypeSmallInteger:
                case esriFieldType.esriFieldTypeInteger:
                {
                    str = "Integer";
                    return str;
                }
                case esriFieldType.esriFieldTypeSingle:
                case esriFieldType.esriFieldTypeDouble:
                {
                    string str1 = ifield_0.Length.ToString();
                    precision = ifield_0.Precision;
                    str = string.Concat("Float,", str1, ",", precision.ToString());
                    return str;
                }
                case esriFieldType.esriFieldTypeString:
                {
                    precision = ifield_0.Length;
                    str = string.Concat("Char,", precision.ToString());
                    return str;
                }
                case esriFieldType.esriFieldTypeDate:
                {
                    str = "Date";
                    return str;
                }
                case esriFieldType.esriFieldTypeOID:
                case esriFieldType.esriFieldTypeGeometry:
                case esriFieldType.esriFieldTypeBlob:
                case esriFieldType.esriFieldTypeRaster:
                {
                    return str;
                }
                case esriFieldType.esriFieldTypeGUID:
                {
                    str = "Char,20";
                    return str;
                }
                case esriFieldType.esriFieldTypeGlobalID:
                {
                    str = "Char,20";
                    return str;
                }
                default:
                {
                    return str;
                }
            }
        }

        private void method_6(StreamWriter streamWriter_0, IFeatureClass ifeatureClass_0)
        {
            int i;
            IField field;
            string str;
            IFields fields = ifeatureClass_0.Fields;
            if (ifeatureClass_0.FeatureType != esriFeatureType.esriFTAnnotation)
            {
                for (i = 0; i < fields.FieldCount; i++)
                {
                    field = fields.Field[i];
                    if ((field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeBlob ||
                         field.Type == esriFieldType.esriFieldTypeGeometry ||
                         field.Type == esriFieldType.esriFieldTypeRaster
                            ? false
                            : field.Editable) && !(field.Name == this.string_0))
                    {
                        str = string.Concat(field.Name, ",", this.method_5(field));
                        streamWriter_0.WriteLine(str);
                    }
                }
            }
            else
            {
                for (i = 0; i < fields.FieldCount; i++)
                {
                    field = fields.Field[i];
                    if ((field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeBlob ||
                         field.Type == esriFieldType.esriFieldTypeGeometry ||
                         field.Type == esriFieldType.esriFieldTypeRaster
                            ? false
                            : field.Editable) && !(field.Name == this.string_0) &&
                        this.ifields_0.FindField(field.Name) == -1)
                    {
                        str = string.Concat(field.Name, ",", this.method_5(field));
                        streamWriter_0.WriteLine(str);
                    }
                }
            }
        }

        private void method_7(StreamWriter streamWriter_0)
        {
            streamWriter_0.WriteLine("TableStructureBegin");
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                IFeatureClass item = this.ilist_0[i] as IFeatureClass;
                string name = (item as IDataset).Name;
                string[] strArrays = name.Split(new char[] {'.'});
                string str = strArrays[(int) strArrays.Length - 1];
                int num = this.method_4(item);
                string str1 = string.Concat(str, ",", num.ToString());
                streamWriter_0.WriteLine(str1);
                this.method_6(streamWriter_0, item);
            }
            streamWriter_0.WriteLine("TableStructureEnd");
        }

        private void method_8(StreamWriter streamWriter_0)
        {
            streamWriter_0.WriteLine("PointBegin");
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                IFeatureClass item = this.ilist_0[i] as IFeatureClass;
                ISpatialFilter spatialFilterClass = null;
                if (this.igeometry_0 != null)
                {
                    spatialFilterClass = new SpatialFilter()
                    {
                        GeometryField = item.ShapeFieldName,
                        Geometry = this.igeometry_0
                    };
                    if (item.ShapeType != esriGeometryType.esriGeometryPoint)
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    }
                    else
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    }
                }
                int num = item.FeatureCount(spatialFilterClass);
                if (num != 0 && item.FeatureType == esriFeatureType.esriFTSimple &&
                    item.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    string str = this.ilist_1[i].ToString();
                    string name = (item as IDataset).Name;
                    if (this.setFeatureClassNameEnventHandler_0 != null)
                    {
                        this.setFeatureClassNameEnventHandler_0(name);
                    }
                    string[] strArrays = name.Split(new char[] {'.'});
                    string str1 = strArrays[(int) strArrays.Length - 1];
                    if (this.setFeatureCountEnventHandler_0 != null)
                    {
                        this.setFeatureCountEnventHandler_0(num);
                    }
                    IFeatureCursor featureCursor = item.Search(spatialFilterClass, false);
                    for (IFeature j = featureCursor.NextFeature(); j != null; j = featureCursor.NextFeature())
                    {
                        if (this.ifeatureProgress_StepEventHandler_0 != null)
                        {
                            this.ifeatureProgress_StepEventHandler_0();
                        }
                        IPoint shape = j.Shape as IPoint;
                        if (shape != null)
                        {
                            streamWriter_0.WriteLine(j.OID);
                            streamWriter_0.WriteLine(str);
                            streamWriter_0.WriteLine(str1);
                            streamWriter_0.WriteLine("1");
                            double x = (double) (shape.X*this.double_0);
                            string str2 = x.ToString("0.###");
                            x = (double) (shape.Y*this.double_0);
                            string str3 = string.Concat(str2, ",", x.ToString("0.###"));
                            streamWriter_0.WriteLine(str3);
                        }
                    }
                    Marshal.ReleaseComObject(featureCursor);
                    featureCursor = null;
                }
            }
            streamWriter_0.WriteLine("PointEnd");
        }

        private void method_9(StreamWriter streamWriter_0)
        {
            streamWriter_0.WriteLine("LineBegin");
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                IFeatureClass item = this.ilist_0[i] as IFeatureClass;
                ISpatialFilter spatialFilterClass = null;
                if (this.igeometry_0 != null)
                {
                    spatialFilterClass = new SpatialFilter()
                    {
                        GeometryField = item.ShapeFieldName,
                        Geometry = this.igeometry_0
                    };
                    if (item.ShapeType != esriGeometryType.esriGeometryPoint)
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    }
                    else
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    }
                }
                int num = item.FeatureCount(spatialFilterClass);
                if (num != 0 && item.FeatureType == esriFeatureType.esriFTSimple &&
                    item.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    string str = this.ilist_1[i].ToString();
                    string name = (item as IDataset).Name;
                    string[] strArrays = name.Split(new char[] {'.'});
                    string str1 = strArrays[(int) strArrays.Length - 1];
                    if (this.setFeatureClassNameEnventHandler_0 != null)
                    {
                        this.setFeatureClassNameEnventHandler_0(name);
                    }
                    if (this.setFeatureCountEnventHandler_0 != null)
                    {
                        this.setFeatureCountEnventHandler_0(num);
                    }
                    IFeatureCursor featureCursor = item.Search(spatialFilterClass, false);
                    for (IFeature j = featureCursor.NextFeature(); j != null; j = featureCursor.NextFeature())
                    {
                        if (this.ifeatureProgress_StepEventHandler_0 != null)
                        {
                            this.ifeatureProgress_StepEventHandler_0();
                        }
                        IPolyline shape = j.Shape as IPolyline;
                        if ((shape == null ? false : !shape.IsEmpty))
                        {
                            streamWriter_0.WriteLine(j.OID);
                            streamWriter_0.WriteLine(str);
                            streamWriter_0.WriteLine(str1);
                            streamWriter_0.WriteLine("1");
                            IPointCollection pointCollection = shape as IPointCollection;
                            streamWriter_0.WriteLine(pointCollection.PointCount.ToString());
                            for (int k = 0; k < pointCollection.PointCount; k++)
                            {
                                IPoint point = pointCollection.Point[k];
                                double x = (double) (point.X*this.double_0);
                                string str2 = x.ToString("0.###");
                                x = (double) (point.Y*this.double_0);
                                string str3 = string.Concat(str2, ",", x.ToString("0.###"));
                                streamWriter_0.WriteLine(str3);
                            }
                        }
                    }
                    Marshal.ReleaseComObject(featureCursor);
                    featureCursor = null;
                }
            }
            streamWriter_0.WriteLine("LineEnd");
        }

        public void Write(string string_1)
        {
            StreamWriter streamWriter = new StreamWriter(string_1, false, Encoding.Default);
            if (this.progressMessageHandle_0 != null)
            {
                this.progressMessageHandle_0(this, "写文件头");
            }
            this.WriteHead(streamWriter);
            this.method_3(streamWriter);
            this.method_7(streamWriter);
            if (this.progressMessageHandle_0 != null)
            {
                this.progressMessageHandle_0(this, "写点要素图形");
            }
            this.method_8(streamWriter);
            if (this.progressMessageHandle_0 != null)
            {
                this.progressMessageHandle_0(this, "写线要素图形");
            }
            this.method_9(streamWriter);
            if (this.progressMessageHandle_0 != null)
            {
                this.progressMessageHandle_0(this, "写面要素图形");
            }
            this.method_10(streamWriter);
            if (this.progressMessageHandle_0 != null)
            {
                this.progressMessageHandle_0(this, "写注记");
            }
            this.method_11(streamWriter);
            if (this.progressMessageHandle_0 != null)
            {
                this.progressMessageHandle_0(this, "写要素属性");
            }
            this.method_13(streamWriter);
            streamWriter.Close();
        }

        internal void WriteHead(StreamWriter streamWriter_0)
        {
            ISpheroid spheroid;
            streamWriter_0.WriteLine("HeadBegin");
            streamWriter_0.WriteLine("Datamark: LANDUSE-DAT");
            streamWriter_0.WriteLine("Version: 1.0");
            if (this.ispatialReference_0 is IUnknownCoordinateSystem)
            {
                streamWriter_0.WriteLine("Unit: M");
            }
            else if (this.ispatialReference_0 is IProjectedCoordinateSystem)
            {
                this.double_0 = (this.ispatialReference_0 as IProjectedCoordinateSystem).CoordinateUnit.MetersPerUnit;
                streamWriter_0.WriteLine("Unit: M");
            }
            else if (this.ispatialReference_0 is IGeographicCoordinateSystem)
            {
                IAngularUnit coordinateUnit = (this.ispatialReference_0 as IGeographicCoordinateSystem).CoordinateUnit;
                this.double_0 = 1/coordinateUnit.RadiansPerUnit;
                streamWriter_0.WriteLine("Unit: D");
            }
            streamWriter_0.WriteLine("Dim: 2");
            streamWriter_0.WriteLine("Topo: 0");
            this.method_1();
            double xMin = this.ienvelope_0.XMin;
            string str = string.Concat("MinX: ", xMin.ToString("0.####"));
            streamWriter_0.WriteLine(str);
            xMin = this.ienvelope_0.YMin;
            str = string.Concat("MinY: ", xMin.ToString("0.####"));
            streamWriter_0.WriteLine(str);
            xMin = this.ienvelope_0.XMax;
            str = string.Concat("MaxX: ", xMin.ToString("0.####"));
            streamWriter_0.WriteLine(str);
            xMin = this.ienvelope_0.YMax;
            str = string.Concat("MaxY: ", xMin.ToString("0.####"));
            streamWriter_0.WriteLine(str);
            streamWriter_0.WriteLine("ScaleM: 10000");
            if (this.ispatialReference_0 is IUnknownCoordinateSystem)
            {
                streamWriter_0.WriteLine("Projection: Unknown");
                streamWriter_0.WriteLine("Spheroid: Unknown");
            }
            else if (this.ispatialReference_0 is IProjectedCoordinateSystem)
            {
                str = string.Concat("Projection: ",
                    (this.ispatialReference_0 as IProjectedCoordinateSystem).Projection.Name);
                streamWriter_0.WriteLine(str);
                spheroid =
                    (this.ispatialReference_0 as IProjectedCoordinateSystem).GeographicCoordinateSystem.Datum.Spheroid;
                streamWriter_0.WriteLine(string.Concat("Spheroid: ", spheroid.Name));
                string str1 = spheroid.SemiMajorAxis.ToString();
                xMin = spheroid.SemiMinorAxis;
                str = string.Concat("Parameters: ", str1, ",", xMin.ToString());
                streamWriter_0.WriteLine(str);
            }
            else if (this.ispatialReference_0 is IGeographicCoordinateSystem)
            {
                spheroid = (this.ispatialReference_0 as IGeographicCoordinateSystem).Datum.Spheroid;
                streamWriter_0.WriteLine(string.Concat("Spheroid: ", spheroid.Name));
                string str2 = spheroid.SemiMajorAxis.ToString();
                xMin = spheroid.SemiMinorAxis;
                str = string.Concat("Parameters: ", str2, ",", xMin.ToString());
                streamWriter_0.WriteLine(str);
            }
            DateTime today = DateTime.Today;
            str = string.Concat("Date: ", today.ToString());
            streamWriter_0.WriteLine(str);
            streamWriter_0.WriteLine("Separator: ,");
            if (this.ispatialReference_0 is IProjectedCoordinateSystem)
            {
                xMin = (this.ispatialReference_0 as IProjectedCoordinateSystem).CentralMeridian[false];
                str = string.Concat("Meridian: ", xMin.ToString());
                streamWriter_0.WriteLine(str);
            }
            streamWriter_0.WriteLine("HeadEnd");
        }

        public event FinishHander FinishEvent
        {
            add
            {
                FinishHander finishHander;
                FinishHander finishHander0 = this.finishHander_0;
                do
                {
                    finishHander = finishHander0;
                    FinishHander finishHander1 = (FinishHander) Delegate.Combine(finishHander, value);
                    finishHander0 = Interlocked.CompareExchange<FinishHander>(ref this.finishHander_0, finishHander1,
                        finishHander);
                } while ((object) finishHander0 != (object) finishHander);
            }
            remove
            {
                FinishHander finishHander;
                FinishHander finishHander0 = this.finishHander_0;
                do
                {
                    finishHander = finishHander0;
                    FinishHander finishHander1 = (FinishHander) Delegate.Remove(finishHander, value);
                    finishHander0 = Interlocked.CompareExchange<FinishHander>(ref this.finishHander_0, finishHander1,
                        finishHander);
                } while ((object) finishHander0 != (object) finishHander);
            }
        }

        public event ProgressMessageHandle ProgressMessage
        {
            add
            {
                ProgressMessageHandle progressMessageHandle;
                ProgressMessageHandle progressMessageHandle0 = this.progressMessageHandle_0;
                do
                {
                    progressMessageHandle = progressMessageHandle0;
                    ProgressMessageHandle progressMessageHandle1 =
                        (ProgressMessageHandle) Delegate.Combine(progressMessageHandle, value);
                    progressMessageHandle0 =
                        Interlocked.CompareExchange<ProgressMessageHandle>(ref this.progressMessageHandle_0,
                            progressMessageHandle1, progressMessageHandle);
                } while ((object) progressMessageHandle0 != (object) progressMessageHandle);
            }
            remove
            {
                ProgressMessageHandle progressMessageHandle;
                ProgressMessageHandle progressMessageHandle0 = this.progressMessageHandle_0;
                do
                {
                    progressMessageHandle = progressMessageHandle0;
                    ProgressMessageHandle progressMessageHandle1 =
                        (ProgressMessageHandle) Delegate.Remove(progressMessageHandle, value);
                    progressMessageHandle0 =
                        Interlocked.CompareExchange<ProgressMessageHandle>(ref this.progressMessageHandle_0,
                            progressMessageHandle1, progressMessageHandle);
                } while ((object) progressMessageHandle0 != (object) progressMessageHandle);
            }
        }

        public event SetFeatureClassNameEnventHandler SetFeatureClassNameEnvent
        {
            add
            {
                SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler;
                SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler0 =
                    this.setFeatureClassNameEnventHandler_0;
                do
                {
                    setFeatureClassNameEnventHandler = setFeatureClassNameEnventHandler0;
                    SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler1 =
                        (SetFeatureClassNameEnventHandler) Delegate.Combine(setFeatureClassNameEnventHandler, value);
                    setFeatureClassNameEnventHandler0 =
                        Interlocked.CompareExchange<SetFeatureClassNameEnventHandler>(
                            ref this.setFeatureClassNameEnventHandler_0, setFeatureClassNameEnventHandler1,
                            setFeatureClassNameEnventHandler);
                } while ((object) setFeatureClassNameEnventHandler0 != (object) setFeatureClassNameEnventHandler);
            }
            remove
            {
                SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler;
                SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler0 =
                    this.setFeatureClassNameEnventHandler_0;
                do
                {
                    setFeatureClassNameEnventHandler = setFeatureClassNameEnventHandler0;
                    SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler1 =
                        (SetFeatureClassNameEnventHandler) Delegate.Remove(setFeatureClassNameEnventHandler, value);
                    setFeatureClassNameEnventHandler0 =
                        Interlocked.CompareExchange<SetFeatureClassNameEnventHandler>(
                            ref this.setFeatureClassNameEnventHandler_0, setFeatureClassNameEnventHandler1,
                            setFeatureClassNameEnventHandler);
                } while ((object) setFeatureClassNameEnventHandler0 != (object) setFeatureClassNameEnventHandler);
            }
        }

        public event SetFeatureCountEnventHandler SetFeatureCountEnvent
        {
            add
            {
                SetFeatureCountEnventHandler setFeatureCountEnventHandler;
                SetFeatureCountEnventHandler setFeatureCountEnventHandler0 = this.setFeatureCountEnventHandler_0;
                do
                {
                    setFeatureCountEnventHandler = setFeatureCountEnventHandler0;
                    SetFeatureCountEnventHandler setFeatureCountEnventHandler1 =
                        (SetFeatureCountEnventHandler) Delegate.Combine(setFeatureCountEnventHandler, value);
                    setFeatureCountEnventHandler0 =
                        Interlocked.CompareExchange<SetFeatureCountEnventHandler>(
                            ref this.setFeatureCountEnventHandler_0, setFeatureCountEnventHandler1,
                            setFeatureCountEnventHandler);
                } while ((object) setFeatureCountEnventHandler0 != (object) setFeatureCountEnventHandler);
            }
            remove
            {
                SetFeatureCountEnventHandler setFeatureCountEnventHandler;
                SetFeatureCountEnventHandler setFeatureCountEnventHandler0 = this.setFeatureCountEnventHandler_0;
                do
                {
                    setFeatureCountEnventHandler = setFeatureCountEnventHandler0;
                    SetFeatureCountEnventHandler setFeatureCountEnventHandler1 =
                        (SetFeatureCountEnventHandler) Delegate.Remove(setFeatureCountEnventHandler, value);
                    setFeatureCountEnventHandler0 =
                        Interlocked.CompareExchange<SetFeatureCountEnventHandler>(
                            ref this.setFeatureCountEnventHandler_0, setFeatureCountEnventHandler1,
                            setFeatureCountEnventHandler);
                } while ((object) setFeatureCountEnventHandler0 != (object) setFeatureCountEnventHandler);
            }
        }

        public event SetMaxValueHandler SetMaxValueEvent
        {
            add
            {
                SetMaxValueHandler setMaxValueHandler;
                SetMaxValueHandler setMaxValueHandler0 = this.setMaxValueHandler_0;
                do
                {
                    setMaxValueHandler = setMaxValueHandler0;
                    SetMaxValueHandler setMaxValueHandler1 =
                        (SetMaxValueHandler) Delegate.Combine(setMaxValueHandler, value);
                    setMaxValueHandler0 = Interlocked.CompareExchange<SetMaxValueHandler>(
                        ref this.setMaxValueHandler_0, setMaxValueHandler1, setMaxValueHandler);
                } while ((object) setMaxValueHandler0 != (object) setMaxValueHandler);
            }
            remove
            {
                SetMaxValueHandler setMaxValueHandler;
                SetMaxValueHandler setMaxValueHandler0 = this.setMaxValueHandler_0;
                do
                {
                    setMaxValueHandler = setMaxValueHandler0;
                    SetMaxValueHandler setMaxValueHandler1 =
                        (SetMaxValueHandler) Delegate.Remove(setMaxValueHandler, value);
                    setMaxValueHandler0 = Interlocked.CompareExchange<SetMaxValueHandler>(
                        ref this.setMaxValueHandler_0, setMaxValueHandler1, setMaxValueHandler);
                } while ((object) setMaxValueHandler0 != (object) setMaxValueHandler);
            }
        }

        public event SetMessageHandler SetMessageEvent
        {
            add
            {
                SetMessageHandler setMessageHandler;
                SetMessageHandler setMessageHandler0 = this.setMessageHandler_0;
                do
                {
                    setMessageHandler = setMessageHandler0;
                    SetMessageHandler setMessageHandler1 =
                        (SetMessageHandler) Delegate.Combine(setMessageHandler, value);
                    setMessageHandler0 = Interlocked.CompareExchange<SetMessageHandler>(ref this.setMessageHandler_0,
                        setMessageHandler1, setMessageHandler);
                } while ((object) setMessageHandler0 != (object) setMessageHandler);
            }
            remove
            {
                SetMessageHandler setMessageHandler;
                SetMessageHandler setMessageHandler0 = this.setMessageHandler_0;
                do
                {
                    setMessageHandler = setMessageHandler0;
                    SetMessageHandler setMessageHandler1 = (SetMessageHandler) Delegate.Remove(setMessageHandler, value);
                    setMessageHandler0 = Interlocked.CompareExchange<SetMessageHandler>(ref this.setMessageHandler_0,
                        setMessageHandler1, setMessageHandler);
                } while ((object) setMessageHandler0 != (object) setMessageHandler);
            }
        }

        public event SetMinValueHandler SetMinValueEvent
        {
            add
            {
                SetMinValueHandler setMinValueHandler;
                SetMinValueHandler setMinValueHandler0 = this.setMinValueHandler_0;
                do
                {
                    setMinValueHandler = setMinValueHandler0;
                    SetMinValueHandler setMinValueHandler1 =
                        (SetMinValueHandler) Delegate.Combine(setMinValueHandler, value);
                    setMinValueHandler0 = Interlocked.CompareExchange<SetMinValueHandler>(
                        ref this.setMinValueHandler_0, setMinValueHandler1, setMinValueHandler);
                } while ((object) setMinValueHandler0 != (object) setMinValueHandler);
            }
            remove
            {
                SetMinValueHandler setMinValueHandler;
                SetMinValueHandler setMinValueHandler0 = this.setMinValueHandler_0;
                do
                {
                    setMinValueHandler = setMinValueHandler0;
                    SetMinValueHandler setMinValueHandler1 =
                        (SetMinValueHandler) Delegate.Remove(setMinValueHandler, value);
                    setMinValueHandler0 = Interlocked.CompareExchange<SetMinValueHandler>(
                        ref this.setMinValueHandler_0, setMinValueHandler1, setMinValueHandler);
                } while ((object) setMinValueHandler0 != (object) setMinValueHandler);
            }
        }

        public event SetPositionHandler SetPositionEvent
        {
            add
            {
                SetPositionHandler setPositionHandler;
                SetPositionHandler setPositionHandler0 = this.setPositionHandler_0;
                do
                {
                    setPositionHandler = setPositionHandler0;
                    SetPositionHandler setPositionHandler1 =
                        (SetPositionHandler) Delegate.Combine(setPositionHandler, value);
                    setPositionHandler0 = Interlocked.CompareExchange<SetPositionHandler>(
                        ref this.setPositionHandler_0, setPositionHandler1, setPositionHandler);
                } while ((object) setPositionHandler0 != (object) setPositionHandler);
            }
            remove
            {
                SetPositionHandler setPositionHandler;
                SetPositionHandler setPositionHandler0 = this.setPositionHandler_0;
                do
                {
                    setPositionHandler = setPositionHandler0;
                    SetPositionHandler setPositionHandler1 =
                        (SetPositionHandler) Delegate.Remove(setPositionHandler, value);
                    setPositionHandler0 = Interlocked.CompareExchange<SetPositionHandler>(
                        ref this.setPositionHandler_0, setPositionHandler1, setPositionHandler);
                } while ((object) setPositionHandler0 != (object) setPositionHandler);
            }
        }

        public event ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler Step
        {
            add
            {
                ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
                ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 =
                    this.ifeatureProgress_StepEventHandler_0;
                do
                {
                    featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
                    ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 =
                        (ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler)
                        Delegate.Combine(featureProgressStepEventHandler, value);
                    ifeatureProgressStepEventHandler0 =
                        Interlocked.CompareExchange<ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler>(
                            ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1,
                            featureProgressStepEventHandler);
                } while ((object) ifeatureProgressStepEventHandler0 != (object) featureProgressStepEventHandler);
            }
            remove
            {
                ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
                ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 =
                    this.ifeatureProgress_StepEventHandler_0;
                do
                {
                    featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
                    ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 =
                        (ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler)
                        Delegate.Remove(featureProgressStepEventHandler, value);
                    ifeatureProgressStepEventHandler0 =
                        Interlocked.CompareExchange<ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler>(
                            ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1,
                            featureProgressStepEventHandler);
                } while ((object) ifeatureProgressStepEventHandler0 != (object) featureProgressStepEventHandler);
            }
        }
    }
}