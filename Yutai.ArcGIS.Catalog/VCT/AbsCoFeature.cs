using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Yutai.ArcGIS.Catalog.VCT
{
    internal abstract class AbsCoFeature : IDisposable, ICoFeature
    {
        private CoFeatureType coFeatureType_0 = CoFeatureType.Point;
        private ICoLayer icoLayer_0;
        private int int_0;
        private List<object> list_0 = new List<object>();

        public AbsCoFeature(ICoLayer icoLayer_1, CoFeatureType coFeatureType_1)
        {
            this.icoLayer_0 = icoLayer_1;
            this.coFeatureType_0 = coFeatureType_1;
            if (this.icoLayer_0 != null)
            {
                for (int i = 0; i < this.icoLayer_0.Fields.Count; i++)
                {
                    this.list_0.Add(null);
                }
            }
        }

        public void AppendValue(object object_0)
        {
            this.list_0.Add(object_0);
        }

        public void Dispose()
        {
            this.list_0.Clear();
        }

        ~AbsCoFeature()
        {
            this.list_0.Clear();
            this.list_0 = null;
        }

        public object GetValue(ICoField icoField_0)
        {
            Debug.Assert(icoField_0 != null);
            int fieldIndex = this.icoLayer_0.GetFieldIndex(icoField_0);
            if (fieldIndex != -1)
            {
                return this.GetValue(fieldIndex);
            }
            return "";
        }

        public object GetValue(int int_1)
        {
            return this.list_0[int_1];
        }

        public object GetValue(string string_0)
        {
            return this.GetValue(this.icoLayer_0.GetField(string_0));
        }

        public void SetValue(ICoField icoField_0, object object_0)
        {
            Debug.Assert(this.icoLayer_0 != null);
            this.SetValue(this.icoLayer_0.GetFieldIndex(icoField_0), object_0);
        }

        public void SetValue(int int_1, object object_0)
        {
            Debug.Assert(this.list_0.Count > int_1);
            this.list_0[int_1] = object_0;
        }

        public void SetValue(string string_0, object object_0)
        {
            Debug.Assert(this.icoLayer_0 != null);
            int fieldIndex = this.icoLayer_0.GetFieldIndex(this.icoLayer_0.GetField(string_0));
            this.SetValue(fieldIndex, object_0);
        }

        [Browsable(false)]
        public ICoLayer Layer
        {
            get { return this.icoLayer_0; }
        }

        [DisplayName("标识码"), Browsable(false)]
        public int OID
        {
            get { return this.int_0; }
            set { this.int_0 = value; }
        }

        [DisplayName("要素类型"), Browsable(false)]
        public CoFeatureType Type
        {
            get { return this.coFeatureType_0; }
        }

        [XmlElement(typeof(List<object>)), Browsable(false), DisplayName("属性值集合")]
        public object[] Values
        {
            get { return this.list_0.ToArray(); }
        }
    }
}