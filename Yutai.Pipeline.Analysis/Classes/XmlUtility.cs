using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class XmlUtility
    {
        public XmlUtility()
        {
        }

        public static object Deserialize(Type type, string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            XmlTextReader xmlTextReader = new XmlTextReader(fileName);
            object obj = xmlSerializer.Deserialize(xmlTextReader);
            xmlTextReader.Close();
            return obj;
        }

        public static void Serialize(object data, string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
            XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, Encoding.UTF8)
            {
                Formatting = Formatting.Indented
            };
            xmlSerializer.Serialize(xmlTextWriter, data);
            xmlTextWriter.Close();
        }
    }
}