using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Scene.Helpers
{
    internal class BulidTinFeatureClassInfo
    {
        private IFeatureClass ifeatureClass_0 = null;

        private IFeatureLayer ifeatureLayer_0 = null;

        private IQueryFilter iqueryFilter_0 = null;

        private string string_0 = "";

        private string string_1 = "";

        private int int_0 = 18;

        private bool bool_0 = true;

        private bool bool_1 = false;

        public bool Is3DGeometry
        {
            get
            {
                return this.bool_1;
            }
        }

        public IFeatureClass FeatureClass
        {
            get
            {
                return this.ifeatureClass_0;
            }
        }

        public IQueryFilter QueryFilter
        {
            get
            {
                return this.iqueryFilter_0;
            }
            set
            {
                this.iqueryFilter_0 = value;
            }
        }

        public string HeightField
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public string TagValueField
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public int TinSurfaceType
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public bool UseShapeZ
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public BulidTinFeatureClassInfo(object object_0)
        {
            if (object_0 is IFeatureLayer)
            {
                this.ifeatureLayer_0 = (object_0 as IFeatureLayer);
                this.ifeatureClass_0 = this.ifeatureLayer_0.FeatureClass;
            }
            else if (object_0 is IFeatureClass)
            {
                this.ifeatureClass_0 = (object_0 as IFeatureClass);
            }
            if (this.ifeatureClass_0 != null)
            {
                switch (this.ifeatureClass_0.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        this.int_0 = 18;
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        this.int_0 = 1;
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        this.int_0 = 10;
                        break;
                }
                int index = this.ifeatureClass_0.FindField(this.ifeatureClass_0.ShapeFieldName);
                IField field = this.ifeatureClass_0.Fields.get_Field(index);
                IGeometryDef geometryDef = field.GeometryDef;
                if (geometryDef.HasZ)
                {
                    this.bool_1 = true;
                    this.string_0 = this.ifeatureClass_0.ShapeFieldName;
                }
                else
                {
                    for (int i = 0; i < this.ifeatureClass_0.Fields.FieldCount; i++)
                    {
                        IField field2 = this.ifeatureClass_0.Fields.get_Field(i);
                        if (field2.Type == esriFieldType.esriFieldTypeDouble || field2.Type == esriFieldType.esriFieldTypeInteger || field2.Type == esriFieldType.esriFieldTypeSingle || field2.Type == esriFieldType.esriFieldTypeSmallInteger)
                        {
                            this.string_0 = field2.Name;
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            string result;
            if (this.ifeatureLayer_0 != null)
            {
                result = this.ifeatureLayer_0.Name;
            }
            else if (this.ifeatureClass_0 != null)
            {
                result = (this.ifeatureClass_0 as IDataset).Name;
            }
            else
            {
                result = "";
            }
            return result;
        }
    }
}