using System;
using System.Runtime.Serialization;

namespace Yutai.Services.Serialization
{
    [DataContract(Name = "Plugin", Namespace = "")]
    public class XmlPlugin
    {
        [DataMember(IsRequired = true, Order = 0)]
        public Guid Guid { get; set; }

        [DataMember(IsRequired = true, Order = 1)]
        public string Name { get; set; }

        [DataMember(IsRequired = false, Order = 2)]
        public string MenuXML { get; set; }

        [DataMember(IsRequired = false, Order = 3)]
        public string ConfigXML { get; set; }
    }
}