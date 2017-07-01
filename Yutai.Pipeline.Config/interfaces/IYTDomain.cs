using System.Collections.Generic;
using System.Xml;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IYTDomain
    {
        string DomainName { get; set; }
        string DomainValues { get; set; }
        Dictionary<string, string> DomainDirectory { get; set; }
        void ReadFromXml(XmlNode xml);
        XmlNode ToXml(XmlDocument doc);
    }
}