using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface ICommonConfig
    {
         string Name { get; set; }
        string Value { get; set; }
        void ReadFromXml(XmlNode xml);
 
        XmlNode ToXml(XmlDocument doc);
    }
}
