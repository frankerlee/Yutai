using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class XmlReaderClass
    {
        public const string XmlNamespaceURL = "http://schemas.microsoft.com/.NetConfiguration/v2.0";

        private System.Xml.XmlDocument xmlDocument_0 = new System.Xml.XmlDocument();

        private string string_0;

        public System.Xml.XmlDocument XmlDocument
        {
            get { return this.xmlDocument_0; }
        }

        public XmlReaderClass(string string_1)
        {
            this.string_0 = Path.GetFullPath(string_1);
            if (!File.Exists(this.string_0))
            {
                throw new Exception(string.Concat(string_1, "不是有效的路径。"));
            }
            this.xmlDocument_0.Load(this.string_0);
        }

        public void AppendAttribute(XmlNode xmlNode_0, string string_1, string string_2)
        {
            xmlNode_0.Attributes.Append(this.xmlDocument_0.CreateAttribute(string_1)).Value = string_2;
        }

        public XmlNode CreateNode(string string_1)
        {
            return this.xmlDocument_0.CreateNode(XmlNodeType.Element, string_1,
                "http://schemas.microsoft.com/.NetConfiguration/v2.0");
        }

        public XmlNode GetCustomSetting(string string_1)
        {
            return this.method_0(string_1);
        }

        private XmlNode method_0(string string_1)
        {
            XmlNamespaceManager xmlNamespaceManagers = new XmlNamespaceManager(this.xmlDocument_0.NameTable);
            xmlNamespaceManagers.AddNamespace("Root", "http://schemas.microsoft.com/.NetConfiguration/v2.0");
            XmlNode xmlNodes = this.xmlDocument_0.SelectSingleNode(string.Concat("//Root:", string_1),
                xmlNamespaceManagers);
            return xmlNodes;
        }

        public void SaveChanges()
        {
            this.xmlDocument_0.Save(this.string_0);
        }

        public void SetAppSetting(string string_1, string string_2)
        {
            IEnumerator enumerator = this.method_0("appSettings").ChildNodes.GetEnumerator();
            try
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XmlNode current = (XmlNode) enumerator.Current;
                        if (current.Attributes["key"].Value == string_1)
                        {
                            current.Attributes["value"].Value = string_2;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}