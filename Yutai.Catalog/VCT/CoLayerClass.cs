namespace Yutai.Catalog.VCT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;

    public class CoLayerClass : IDisposable, ICoClone, ICoLayer, ICoLayer2
    {
        private bool bool_0 = false;
        private CoLayerHead coLayerHead_0 = new CoLayerHead();
        private CoLayerType coLayerType_0 = CoLayerType.Point;
        private CoRuleCollection coRuleCollection_0 = new CoRuleCollection();
        private List<ICoFeature> list_0 = new List<ICoFeature>();
        private List<ICoField> list_1 = new List<ICoField>();
        private object object_0 = null;
        private string string_0 = string.Empty;
        private string string_1 = string.Empty;
        private string string_2 = string.Empty;
        private string string_3 = Guid.NewGuid().ToString();

        public void AppendFeature(ICoFeature icoFeature_0)
        {
            this.list_0.Add(icoFeature_0);
        }

        public void AppendRule(CoRuleClass coRuleClass_0)
        {
            this.coRuleCollection_0.Add(coRuleClass_0);
        }

        public virtual object Clone()
        {
            ICoLayer layer = new CoLayerClass {
                AliasName = this.string_1,
                Name = this.string_0,
                Categorie = this.string_2,
                Enable = this.bool_0,
                LayerType = this.LayerType,
                Parameter = new CoLayerHead()
            };
            layer.Parameter.Coordinate = this.coLayerHead_0.Coordinate;
            layer.Parameter.Dim = this.coLayerHead_0.Dim;
            layer.Parameter.Meridian = this.coLayerHead_0.Meridian;
            layer.Parameter.ScaleM = this.coLayerHead_0.ScaleM;
            layer.Parameter.Spheroid = this.coLayerHead_0.Spheroid;
            layer.Parameter.Unit = this.coLayerHead_0.Unit;
            layer.Parameter.MaxPoint = new CoPointClass(this.coLayerHead_0.MaxPoint.X, this.coLayerHead_0.MaxPoint.Y, this.coLayerHead_0.MaxPoint.Z);
            layer.Parameter.MinPoint = new CoPointClass(this.coLayerHead_0.MinPoint.X, this.coLayerHead_0.MinPoint.Y, this.coLayerHead_0.MinPoint.Z);
            foreach (ICoField field in this.Fields)
            {
                ICoField item = (field as ICoClone).Clone() as ICoField;
                layer.Fields.Add(item);
            }
            return layer;
        }

        public void DeleteRule(CoRuleClass coRuleClass_0)
        {
            if (coRuleClass_0 != null)
            {
                this.DeleteRule(coRuleClass_0.ID);
            }
        }

        public void DeleteRule(string string_4)
        {
            using (List<CoRuleClass>.Enumerator enumerator = this.coRuleCollection_0.GetEnumerator())
            {
                CoRuleClass current;
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    if (current.ID == string_4)
                    {
                        goto Label_0033;
                    }
                }
                return;
            Label_0033:
                this.coRuleCollection_0.Remove(current);
            }
        }

        public void Dispose()
        {
            this.RemoveAllFeature();
        }

        ~CoLayerClass()
        {
            this.RemoveAllFeature();
        }

        public ICoFeature GetFeature(int int_0)
        {
            foreach (ICoFeature feature in this.list_0)
            {
                if (feature.OID == int_0)
                {
                    return feature;
                }
            }
            return null;
        }

        public ICoFeature GetFeatureByIndex(int int_0)
        {
            if (this.FeatureCount > int_0)
            {
                return this.list_0[int_0];
            }
            return null;
        }

        public ICoField GetField(int int_0)
        {
            if ((this.list_1 == null) | (this.list_1.Count <= int_0))
            {
                return null;
            }
            return this.list_1[int_0];
        }

        public ICoField GetField(string string_4)
        {
            foreach (ICoField field in this.list_1)
            {
                if (field.Name.ToUpper() == string_4.ToUpper())
                {
                    return field;
                }
            }
            return null;
        }

        public int GetFieldIndex(ICoField icoField_0)
        {
            for (int i = 0; i < this.list_1.Count; i++)
            {
                if (this.list_1[i].Name.ToUpper() == icoField_0.Name.ToUpper())
                {
                    return i;
                }
            }
            return -1;
        }

        public void RemoveAllFeature()
        {
            for (int i = this.list_0.Count - 1; i >= 0; i--)
            {
                this.list_0[i] = null;
            }
            this.list_0.Clear();
        }

        public void RemoveFeature(ICoFeature icoFeature_0)
        {
        }

        public void RemoveFeature(int int_0)
        {
        }

        public override string ToString()
        {
            return this.string_0;
        }

        [DisplayName("图层别名"), Description("图层别名")]
        public string AliasName
        {
            get
            {
                return this.string_1;
            }
            set
            {
                if (this.string_1 != value)
                {
                    this.string_1 = value;
                }
            }
        }

        [Description("图层分类"), DisplayName("分类")]
        public string Categorie
        {
            get
            {
                return this.string_2;
            }
            set
            {
                if (this.string_2 != value)
                {
                    this.string_2 = value;
                }
            }
        }

        [DisplayName("是否检查"), Description("设置此图层是否参与检查")]
        public bool Enable
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                if (this.bool_0 != value)
                {
                    this.bool_0 = value;
                }
            }
        }

        [Browsable(false)]
        public int FeatureCount
        {
            get
            {
                return this.list_0.Count;
            }
        }

        [XmlIgnore, Browsable(false)]
        public List<ICoField> Fields
        {
            get
            {
                return this.list_1;
            }
        }

        [Browsable(false)]
        public string ID
        {
            get
            {
                return this.string_3;
            }
            set
            {
                if (this.string_3 != value)
                {
                    this.string_3 = value;
                }
            }
        }

        [Description("图层类型"), DisplayName("类型")]
        public CoLayerType LayerType
        {
            get
            {
                return this.coLayerType_0;
            }
            set
            {
                if (this.coLayerType_0 != value)
                {
                    this.coLayerType_0 = value;
                }
            }
        }

        [DisplayName("图层名称"), Description("图层名称")]
        public string Name
        {
            get
            {
                return this.string_0;
            }
            set
            {
                if (this.string_0 != value)
                {
                    this.string_0 = value;
                }
            }
        }

        [Browsable(false)]
        public CoLayerHead Parameter
        {
            get
            {
                return this.coLayerHead_0;
            }
            set
            {
                if (this.coLayerHead_0 != value)
                {
                    this.coLayerHead_0 = value;
                }
            }
        }

        [DisplayName("检查规则"), Browsable(false), Description("检查规则")]
        public CoRuleCollection Rules
        {
            get
            {
                return this.coRuleCollection_0;
            }
        }

        [Browsable(false)]
        public object Tag
        {
            get
            {
                return this.object_0;
            }
            set
            {
                if (this.object_0 != value)
                {
                    this.object_0 = value;
                }
            }
        }
    }
}

