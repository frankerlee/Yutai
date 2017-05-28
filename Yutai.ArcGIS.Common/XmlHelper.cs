using System;
using System.Xml;

namespace Yutai.ArcGIS.Common
{
    public class XmlHelper
    {
        public XmlHelper()
        {
        }

        public static string XmlReadValue(XmlDocument doc, string Section, string Key)
        {
            XmlNode xmlNodes = doc.SelectSingleNode(string.Concat("//", Section));
            string innerText = "";
            if (null != xmlNodes)
            {
                innerText = xmlNodes.SelectSingleNode(Key).InnerText;
            }
            return innerText;
        }

        public static string XmlReadValue(XmlDocument doc, string Section, string Section1, string Key)
        {
            string innerText;
            try
            {
                XmlNode xmlNodes = doc.SelectSingleNode(string.Concat("//", Section));
                if (null != xmlNodes)
                {
                    XmlNodeList childNodes = xmlNodes.ChildNodes;
                    int num = 0;
                    while (num < childNodes.Count)
                    {
                        XmlNode itemOf = childNodes[num];
                        if (!itemOf.LocalName.Equals(Section1))
                        {
                            num++;
                        }
                        else
                        {
                            innerText = itemOf.SelectSingleNode(Key).InnerText;
                            return innerText;
                        }
                    }
                }
                innerText = "";
            }
            catch
            {
                innerText = "";
            }
            return innerText;
        }

        public static string XmlReadValue(XmlDocument doc, string Section, string Section1, string Section2, string Key)
        {
            string innerText;
            try
            {
                XmlNode xmlNodes = doc.SelectSingleNode(string.Concat("//", Section));
                if (null != xmlNodes)
                {
                    XmlNodeList childNodes = xmlNodes.ChildNodes;
                    for (int i = 0; i < childNodes.Count; i++)
                    {
                        XmlNode itemOf = childNodes[i];
                        if (itemOf.LocalName.Equals(Section1))
                        {
                            XmlNodeList xmlNodeLists = itemOf.ChildNodes;
                            int num = 0;
                            while (num < xmlNodeLists.Count)
                            {
                                XmlNode itemOf1 = xmlNodeLists[num];
                                if (!itemOf1.LocalName.Equals(Section2))
                                {
                                    num++;
                                }
                                else
                                {
                                    innerText = itemOf1.SelectSingleNode(Key).InnerText;
                                    return innerText;
                                }
                            }
                        }
                    }
                }
                innerText = "";
            }
            catch (Exception exception)
            {
                innerText = "";
            }
            return innerText;
        }

        public static string XmlReadValue(XmlDocument doc, string Section, string Section1, string Section2, string Section3, string Key)
        {
            string innerText;
            try
            {
                XmlNode xmlNodes = doc.SelectSingleNode(string.Concat("//", Section));
                if (null != xmlNodes)
                {
                    XmlNodeList childNodes = xmlNodes.ChildNodes;
                    for (int i = 0; i < childNodes.Count; i++)
                    {
                        XmlNode itemOf = childNodes[i];
                        if (itemOf.LocalName.Equals(Section1))
                        {
                            XmlNodeList xmlNodeLists = itemOf.ChildNodes;
                            for (int j = 0; j < xmlNodeLists.Count; j++)
                            {
                                XmlNode itemOf1 = xmlNodeLists[j];
                                if (itemOf1.LocalName.Equals(Section2))
                                {
                                    XmlNodeList childNodes1 = itemOf1.ChildNodes;
                                    int num = 0;
                                    while (num < childNodes1.Count)
                                    {
                                        XmlNode xmlNodes1 = childNodes1[num];
                                        if (!xmlNodes1.LocalName.Equals(Section3))
                                        {
                                            num++;
                                        }
                                        else
                                        {
                                            innerText = xmlNodes1.SelectSingleNode(Key).InnerText;
                                            return innerText;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                innerText = "";
            }
            catch
            {
                innerText = "";
            }
            return innerText;
        }

        public static void XmlWriteValue(XmlDocument doc, string sXMLPath, string Section, string Key, string Value)
        {
            XmlNode value = doc.SelectSingleNode(string.Concat("//", Section));
            if (null != value)
            {
                if (!(Value == ""))
                {
                    value.SelectSingleNode(Key).InnerText = Value;
                }
                else
                {
                    value.SelectSingleNode(Key).InnerText = "无";
                }
            }
            doc.Save(sXMLPath);
        }

        public static void XmlWriteValue(XmlDocument doc, string sXMLPath, string Section, string sValue1, string Key, string Value)
        {
            XmlNode xmlNodes = doc.SelectSingleNode(string.Concat("//", Section));
            if (null != xmlNodes)
            {
                XmlNodeList childNodes = xmlNodes.ChildNodes;
                for (int i = 0; i < childNodes.Count; i++)
                {
                    XmlNode itemOf = childNodes[i];
                    if (itemOf.LocalName.Equals(sValue1))
                    {
                        if (!(Value == ""))
                        {
                            itemOf.SelectSingleNode(Key).InnerText = Value;
                        }
                        else
                        {
                            itemOf.SelectSingleNode(Key).InnerText = "无";
                        }
                    }
                }
            }
            doc.Save(sXMLPath);
        }

        public static void XmlWriteValue(XmlDocument doc, string sXMLPath, string Section, string sValue1, string sValue2, string Key, string Value)
        {
            XmlNode xmlNodes = doc.SelectSingleNode(string.Concat("//", Section));
            if (null != xmlNodes)
            {
                XmlNodeList childNodes = xmlNodes.ChildNodes;
                for (int i = 0; i < childNodes.Count; i++)
                {
                    XmlNode itemOf = childNodes[i];
                    if (itemOf.LocalName.Equals(sValue1))
                    {
                        XmlNodeList xmlNodeLists = itemOf.ChildNodes;
                        for (int j = 0; j < xmlNodeLists.Count; j++)
                        {
                            XmlNode value = xmlNodeLists[j];
                            if (value.LocalName.Equals(sValue2))
                            {
                                if (!(Value == ""))
                                {
                                    value.SelectSingleNode(Key).InnerText = Value;
                                }
                                else
                                {
                                    value.SelectSingleNode(Key).InnerText = "无";
                                }
                            }
                        }
                    }
                }
            }
            doc.Save(sXMLPath);
        }

        public static void XmlWriteValue(XmlDocument doc, string sXMLPath, string Section, string sValue1, string sValue2, string sValue3, string Key, string Value)
        {
            XmlNode xmlNodes = doc.SelectSingleNode(string.Concat("//", Section));
            if (null != xmlNodes)
            {
                XmlNodeList childNodes = xmlNodes.ChildNodes;
                for (int i = 0; i < childNodes.Count; i++)
                {
                    XmlNode itemOf = childNodes[i];
                    if (itemOf.LocalName.Equals(sValue1))
                    {
                        XmlNodeList xmlNodeLists = itemOf.ChildNodes;
                        for (int j = 0; j < xmlNodeLists.Count; j++)
                        {
                            XmlNode itemOf1 = xmlNodeLists[j];
                            if (itemOf1.LocalName.Equals(sValue2))
                            {
                                XmlNodeList childNodes1 = itemOf1.ChildNodes;
                                for (int k = 0; k < childNodes1.Count; k++)
                                {
                                    XmlNode value = childNodes1[k];
                                    if (value.LocalName.Equals(sValue3))
                                    {
                                        if (!(Value == ""))
                                        {
                                            value.SelectSingleNode(Key).InnerText = Value;
                                        }
                                        else
                                        {
                                            value.SelectSingleNode(Key).InnerText = "无";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            doc.Save(sXMLPath);
        }
    }
}