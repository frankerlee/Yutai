using System;
using System.Windows.Forms;
using System.Xml;
using Yutai.ArcGIS.Common.Framework;
using Yutai.ArcGIS.Framework;


namespace Yutai.ArcGIS.Framework
{
    public class CatalogObjectMenuHelper
    {
        private static MenuItemDef GetItemAttribute(XmlNode xmlNode_0)
        {
            MenuItemDef def = new MenuItemDef();
            for (int i = 0; i < xmlNode_0.Attributes.Count; i++)
            {
                XmlAttribute attribute = xmlNode_0.Attributes[i];
                switch (attribute.Name.ToLower())
                {
                    case "name":
                        def.Name = attribute.Value;
                        break;

                    case "caption":
                        def.Caption = attribute.Value;
                        break;

                    case "path":
                        def.Path = attribute.Value;
                        break;

                    case "classname":
                        def.ClassName = attribute.Value;
                        break;

                    case "subtype":
                        def.SubType = attribute.Value;
                        break;

                    case "begingroup":
                        def.BeginGroup = attribute.Value;
                        break;

                    case "bitmap":
                        def.BitmapPath = attribute.Value;
                        break;
                }
            }
            return def;
        }

        public static void InitExtendTools(IBarManager ibarManager_0, string string_0)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(string_0);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                }
                XmlElement documentElement = document.DocumentElement;
                for (int i = 0; i < documentElement.ChildNodes.Count; i++)
                {
                    XmlNode node = documentElement.ChildNodes[i];
                    ReadBarItems(ibarManager_0, node);
                }
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.ToString());
            }
        }

        private static void ReadBarItems(IBarManager ibarManager_0, XmlNode xmlNode_0)
        {
            MenuItemDef itemAttribute = GetItemAttribute(xmlNode_0);
            if (xmlNode_0.ChildNodes.Count == 0)
            {
                ibarManager_0.AddItem(itemAttribute);
            }
            else
            {
                for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
                {
                    ReadBarItems(ibarManager_0, xmlNode_0.ChildNodes[i]);
                }
            }
        }

        private static void ReadBarItems(IPopuMenuWrap ipopuMenuWrap_0, XmlNode xmlNode_0)
        {
            MenuItemDef itemAttribute = GetItemAttribute(xmlNode_0);
            if (xmlNode_0.ChildNodes.Count > 0)
            {
                itemAttribute.HasSubMenu = true;
            }
            ipopuMenuWrap_0.AddItem(itemAttribute);
            for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
            {
                ReadBarItems(ipopuMenuWrap_0, itemAttribute.Name, xmlNode_0.ChildNodes[i]);
            }
        }

        private static void ReadBarItems(IPopuMenuWrap ipopuMenuWrap_0, string string_0, XmlNode xmlNode_0)
        {
            MenuItemDef itemAttribute = GetItemAttribute(xmlNode_0);
            itemAttribute.MainMenuItem = string_0;
            if (xmlNode_0.ChildNodes.Count > 0)
            {
                itemAttribute.HasSubMenu = true;
            }
            ipopuMenuWrap_0.AddItem(itemAttribute);
            for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
            {
                ReadBarItems(ipopuMenuWrap_0, itemAttribute.Name, xmlNode_0.ChildNodes[i]);
            }
        }

        public static void StartCreateBar(IPopuMenuWrap ipopuMenuWrap_0, string string_0)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(string_0);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                }
                XmlElement documentElement = document.DocumentElement;
                for (int i = 0; i < documentElement.ChildNodes.Count; i++)
                {
                    XmlNode node = documentElement.ChildNodes[i];
                    ReadBarItems(ipopuMenuWrap_0, node);
                }
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.ToString());
            }
        }
    }
}

