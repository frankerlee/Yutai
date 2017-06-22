using System.Collections.Generic;
using System.Xml;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IPipeTemplate
    {
        string Name { get; set; }
        string Caption { get; set; }
        List<IYTField> Fields { get; set; }

        void ReadFromXml(XmlNode xmlNode);
        XmlNode ToXml();

    }
}