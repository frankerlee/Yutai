using System.Collections.Generic;
using System.Runtime.Serialization;
using Yutai.Plugins.Concrete;

namespace Yutai.Services.Serialization
{
    [DataContract]
    public class XmlConfig
    {
        [DataMember]
        public AppConfig Settings { get; set; }

        [DataMember]
        public List<XmlPlugin> ApplicationPlugins { get; set; }
    }
}