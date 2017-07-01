using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class ElementLocation
    {
        public ElementLocation()
        {
            this.LocationType = MapCartoTemplateLib.LocationType.UpperrCenter;
        }

        public ElementLocation(string string_0)
        {
            this.method_2(string_0);
        }

        public ElementLocation Clone()
        {
            return new ElementLocation
            {
                LocationType = this.LocationType,
                XOffset = this.XOffset,
                YOffset = this.YOffset
            };
        }

        private string method_0()
        {
            StringBuilder builder = new StringBuilder("<ElementPosition>");
            int locationType = (int) this.LocationType;
            this.method_1(builder, "position", locationType);
            this.method_1(builder, "xoffset", this.XOffset);
            this.method_1(builder, "yoffset", this.YOffset);
            builder.Append("</ElementPosition>");
            return builder.ToString();
        }

        private void method_1(StringBuilder stringBuilder_0, string string_0, object object_0)
        {
            stringBuilder_0.Append("<attribute name=\"");
            stringBuilder_0.Append(string_0);
            stringBuilder_0.Append("\" value=\"");
            stringBuilder_0.Append(object_0 + "\"/>");
        }

        private void method_2(string string_0)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(string_0);
                XmlNode node = document.ChildNodes[0];
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    XmlNode node2 = node.ChildNodes[i];
                    string str = node2.Attributes["name"].Value;
                    string s = node2.Attributes["value"].Value;
                    switch (str)
                    {
                        case "position":
                            try
                            {
                                this.LocationType = (LocationType) int.Parse(s);
                            }
                            catch
                            {
                            }
                            break;

                        case "xoffset":
                            this.XOffset = Convert.ToDouble(s);
                            break;

                        case "yoffset":
                            this.YOffset = Convert.ToDouble(s);
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        public override string ToString()
        {
            return this.method_0();
        }

        public MapCartoTemplateLib.LocationType LocationType { get; set; }

        public double XOffset { get; set; }
        public double YOffset { get; set; }
    }
}