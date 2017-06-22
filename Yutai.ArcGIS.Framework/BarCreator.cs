using System.Drawing;
using System.Xml;
using DevExpress.XtraBars;

namespace Yutai.ArcGIS.Framework
{
    internal class BarCreator : Creator
    {
        public override void Create(XmlNode xmlNode_0)
        {
            Bar bar = null;
            string str = "";
            for (int i = 0; i < xmlNode_0.Attributes.Count; i++)
            {
                XmlAttribute attribute = xmlNode_0.Attributes[i];
                if (attribute.Name.ToLower() == "name")
                {
                    str = attribute.Value;
                    break;
                }
            }
            if (str != null)
            {
                try
                {
                    bar = base.barManager1.Bars[str];
                    if (bar == null)
                    {
                        bar = new Bar {
                            BarName = base.barManager1.GetNewBarName(),
                            DockCol = 0,
                            DockRow = base.barManager1.Bars.Count,
                            FloatSize = new Size(288, 24)
                        };
                        base.barManager1.Bars.Add(bar);
                        this.method_0(bar, xmlNode_0);
                        if (bar.DockStyle == BarDockStyle.None)
                        {
                            bar.DockStyle = BarDockStyle.Top;
                        }
                        this.method_2(bar, xmlNode_0);
                    }
                }
                catch
                {
                }
            }
        }

        private void method_0(Bar bar_0, XmlNode xmlNode_0)
        {
            int num = 0;
            while (true)
            {
                if (num >= xmlNode_0.Attributes.Count)
                {
                    return;
                }
                XmlAttribute attribute = xmlNode_0.Attributes[num];
                switch (attribute.Name.ToLower())
                {
                    case "name":
                        bar_0.BarName = attribute.Value;
                        break;

                    case "caption":
                        bar_0.Text = attribute.Value;
                        break;

                    case "col":
                        try
                        {
                            bar_0.DockCol = short.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "row":
                        try
                        {
                            bar_0.DockRow = short.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "ismainmenu":
                        try
                        {
                            if (bool.Parse(attribute.Value))
                            {
                                base.barManager1.MainMenu = bar_0;
                            }
                        }
                        catch
                        {
                        }
                        break;

                    case "visible":
                        try
                        {
                            bar_0.Visible = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;
                }
                num++;
            }
        }

        private void method_1(Bar bar_0, XmlNode xmlNode_0)
        {
            int num = 0;
            while (true)
            {
                if (num >= xmlNode_0.Attributes.Count)
                {
                    return;
                }
                XmlAttribute attribute = xmlNode_0.Attributes[num];
                switch (attribute.Name.ToLower())
                {
                    case "disableclose":
                        try
                        {
                            bar_0.OptionsBar.DisableClose = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "allowdelete":
                        try
                        {
                            bar_0.OptionsBar.AllowDelete = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "multiline":
                        try
                        {
                            bar_0.OptionsBar.MultiLine = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "drawdragborder":
                        try
                        {
                            bar_0.OptionsBar.DrawDragBorder = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;
                }
                num++;
            }
        }

        private void method_2(Bar bar_0, XmlNode xmlNode_0)
        {
            for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
            {
                XmlNode node = xmlNode_0.ChildNodes[i];
                if (node.Name.ToLower() == "options")
                {
                    this.method_1(bar_0, node);
                }
                else if (!(node.Name.ToLower() == "baritem"))
                {
                }
            }
        }

        private void method_3(object object_1, XmlNode xmlNode_0, bool bool_0)
        {
        }
    }
}

