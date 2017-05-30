using System.Collections;

namespace Yutai.Catalog.VCT
{
    
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    public class XmlReaderClass
    {
        // Fields
        private string string_0;
        private XmlDocument xmlDocument_0 = new XmlDocument();
        public const string XmlNamespaceURL = "http://schemas.microsoft.com/.NetConfiguration/v2.0";

        // Methods
        public XmlReaderClass(string string_1)
        {
            this.string_0 = Path.GetFullPath(string_1);
            if (!File.Exists(this.string_0))
            {
                throw new Exception(string_1 + "不是有效的路径。");
            }
            this.xmlDocument_0.Load(this.string_0);
        }

        public void AppendAttribute(XmlNode xmlNode_0, string string_1, string string_2)
        {
            xmlNode_0.Attributes.Append(this.xmlDocument_0.CreateAttribute(string_1)).Value = string_2;
        }

        public XmlNode CreateNode(string string_1)
        {
            return this.xmlDocument_0.CreateNode(XmlNodeType.Element, string_1, "http://schemas.microsoft.com/.NetConfiguration/v2.0");
        }

        public XmlNode GetCustomSetting(string string_1)
        {
            return this.method_0(string_1);
        }

        private XmlNode method_0(string string_1)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(this.xmlDocument_0.NameTable);
            nsmgr.AddNamespace("Root", "http://schemas.microsoft.com/.NetConfiguration/v2.0");
            return this.xmlDocument_0.SelectSingleNode("//Root:" + string_1, nsmgr);
        }

        public void SaveChanges()
        {
            this.xmlDocument_0.Save(this.string_0);
        }

        public void SetAppSetting(string string_1, string string_2)
        {
            IEnumerator enumerator = this.method_0("appSettings").ChildNodes.GetEnumerator();
            
                XmlNode current;
                while (enumerator.MoveNext())
                {
                    current = (XmlNode)enumerator.Current;
                    if (current.Attributes["key"].Value == string_1)
                    {
                    current.Attributes["value"].Value = string_2;
                    return;
                    }
                }
                
           
        }

        // Properties
        public XmlDocument XmlDocument
        {
            get
            {
                return this.xmlDocument_0;
            }
        }
    }


    public class CoLayerMapper
    {
        private ICoLayer icoLayer_0 = null;
        private ICoLayer icoLayer_1 = null;
        private List<CoFieldMapper> list_0 = new List<CoFieldMapper>();

        public CoLayerMapper(ICoLayer icoLayer_2, ICoLayer icoLayer_3)
        {
            this.icoLayer_0 = icoLayer_2;
            this.icoLayer_1 = icoLayer_3;
        }

        public ICoField FindDestField(ICoField icoField_0)
        {
            foreach (CoFieldMapper mapper in this.list_0)
            {
                if ((mapper.SourceField != null) && (mapper.SourceField.Name.ToUpper() == icoField_0.Name.ToUpper()))
                {
                    return mapper.DestField;
                }
            }
            return null;
        }

        public ICoField FindSourceField(string string_0)
        {
            if (string_0 != null)
            {
                foreach (CoFieldMapper mapper in this.list_0)
                {
                    if (string_0.Trim().ToLower() == mapper.SourceField.Name.Trim().ToLower())
                    {
                        return mapper.SourceField;
                    }
                }
            }
            return null;
        }

        public static CoLayerMapper[] ReadXml(string string_0)
        {
            List<CoLayerMapper> list = new List<CoLayerMapper>();
            XmlReaderClass class2 = new XmlReaderClass(string_0);
            foreach (XmlNode node2 in class2.GetCustomSetting("configuration").ChildNodes)
            {
                ICoLayer layer = new CoLayerClass {
                    Name = node2.Attributes["sName"].Value,
                    AliasName = node2.Attributes["sAliasName"].Value
                };
                ICoLayer layer2 = new CoLayerClass {
                    Name = node2.Attributes["dName"].Value,
                    AliasName = node2.Attributes["dAliasName"].Value
                };
                CoLayerMapper item = new CoLayerMapper(layer, layer2);
                foreach (XmlNode node3 in node2.ChildNodes)
                {
                    ICoField field = new CoFieldClass {
                        Name = node3.Attributes["sName"].Value,
                        AliasName = node3.Attributes["sAliasName"].Value
                    };
                    ICoField field2 = new CoFieldClass {
                        Name = node3.Attributes["dName"].Value,
                        AliasName = node3.Attributes["dAliasName"].Value
                    };
                    item.FieldRelation.Add(new CoFieldMapper(field, field2));
                }
                list.Add(item);
            }
            return list.ToArray();
        }

        public static void WriteXml(CoLayerMapper[] coLayerMapper_0, string string_0)
        {
            if (File.Exists(string_0))
            {
                File.Delete(string_0);
            }
            using (StreamWriter writer = File.CreateText(string_0))
            {
                writer.WriteLine("<?xml version=\"1.0\" standalone=\"yes\"?>");
                writer.WriteLine("<configuration xmlns=\"http://schemas.microsoft.com/.NetConfiguration/v2.0\">");
                writer.WriteLine("</configuration>");
                writer.Flush();
                writer.Close();
            }
            XmlReaderClass class2 = new XmlReaderClass(string_0);
            foreach (CoLayerMapper mapper in coLayerMapper_0)
            {
                XmlNode node = class2.CreateNode("LayerMapper");
                class2.AppendAttribute(node, "sName", mapper.SourceLayer.Name);
                class2.AppendAttribute(node, "sAliasName", mapper.SourceLayer.AliasName);
                class2.AppendAttribute(node, "dName", mapper.DestLayer.Name);
                class2.AppendAttribute(node, "dAliasName", mapper.DestLayer.AliasName);
                foreach (CoFieldMapper mapper2 in mapper.FieldRelation)
                {
                    XmlNode node2 = class2.CreateNode("FieldMapper");
                    class2.AppendAttribute(node2, "sName", mapper2.SourceField.Name);
                    class2.AppendAttribute(node2, "sAliasName", mapper2.SourceField.AliasName);
                    class2.AppendAttribute(node2, "dName", mapper2.DestField.Name);
                    class2.AppendAttribute(node2, "dAliasName", mapper2.DestField.AliasName);
                    node.AppendChild(node2);
                }
                class2.GetCustomSetting("configuration").AppendChild(node);
            }
            class2.SaveChanges();
        }

        public ICoLayer DestLayer
        {
            get
            {
                return this.icoLayer_1;
            }
            set
            {
                this.icoLayer_1 = value;
            }
        }

        public List<CoFieldMapper> FieldRelation
        {
            get
            {
                return this.list_0;
            }
        }

        public ICoLayer SourceLayer
        {
            get
            {
                return this.icoLayer_0;
            }
            set
            {
                this.icoLayer_0 = value;
            }
        }
    }
}

