using System.Configuration;
using System.Xml;

namespace Yutai.ArcGIS.Common.Overview
{
    public class OverviewWindowsLayerSettingsSectionHandler : IConfigurationSectionHandler
    {
        public OverviewWindowsLayerSettingsSectionHandler()
        {
        }

        public object Create(object object_0, object object_1, XmlNode xmlNode_0)
        {
            OverviewLayerSettings overviewLayerSetting = new OverviewLayerSettings();
            foreach (XmlNode childNode in xmlNode_0.ChildNodes)
            {
                if (childNode.Name != "Layers")
                {
                    continue;
                }
                foreach (XmlNode xmlNodes in childNode.SelectNodes("layer"))
                {
                    string innerText = "";
                    double num = 0;
                    double num1 = 0;
                    if (xmlNodes.Attributes["name"] == null)
                    {
                        continue;
                    }
                    innerText = xmlNodes.Attributes["name"].InnerText;
                    if (xmlNodes.Attributes["minscale"] != null)
                    {
                        try
                        {
                            num = double.Parse(xmlNodes.Attributes["minscale"].InnerText);
                        }
                        catch
                        {
                        }
                    }
                    if (xmlNodes.Attributes["maxscale"] != null)
                    {
                        try
                        {
                            num1 = double.Parse(xmlNodes.Attributes["maxscale"].InnerText);
                        }
                        catch
                        {
                        }
                    }
                    OverviewLayerSetting overviewLayerSetting1 = new OverviewLayerSetting()
                    {
                        LayerName = innerText,
                        MaxScale = num1,
                        MinScale = num
                    };
                    if (!overviewLayerSetting.LayerSettings.Contains(innerText))
                    {
                        overviewLayerSetting.LayerSettings.Add(innerText, overviewLayerSetting1);
                    }
                    else
                    {
                        overviewLayerSetting.LayerSettings[innerText] = overviewLayerSetting1;
                    }
                }
            }
            return overviewLayerSetting;
        }
    }
}