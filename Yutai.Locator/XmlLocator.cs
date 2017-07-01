using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace Yutai.Plugins.Locator
{
    [DataContract(Name = "PluginConfig", Namespace = "")]
    public class PluginConfig
    {
        public PluginConfig()
        {
        }

        [DataMember(Name = "Locators", Order = 0)]
        public List<XmlLocator> Locators { get; set; }
    }

    [DataContract(Name = "Locator", Namespace = "")]
    public class XmlLocator
    {
        public XmlLocator()
        {
        }

        [DataMember(IsRequired = true, Order = 0)]
        public string Name { get; set; }

        [DataMember(IsRequired = false, Order = 1)]
        public string Caption { get; set; }

        [DataMember(IsRequired = false, Order = 2)]
        public Bitmap Bitmap { get; set; }

        [DataMember(IsRequired = true, Order = 3)]
        public string Layer { get; set; }

        [DataMember(IsRequired = true, Order = 4)]
        public string SearchFields { get; set; }

        [DataMember(IsRequired = true, Order = 5)]
        public string NameField { get; set; }

        [DataMember(IsRequired = false, Order = 6)]
        public string DescriptionField { get; set; }

        [DataMember(IsRequired = false, Order = 7)]
        public string AddressField { get; set; }

        [DataMember(IsRequired = false, Order = 8)]
        public string TelephoneField { get; set; }

        [DataMember(IsRequired = false, Order = 9)]
        public string PhotoField { get; set; }

        [DataMember(IsRequired = false, Order = 10)]
        public string EmailField { get; set; }

        [DataMember(IsRequired = false, Order = 11)]
        public string OIDField { get; set; }

        [DataMember(IsRequired = false, Order = 12)]
        public string ShapeField { get; set; }

        [DataMember(IsRequired = false, Order = 13)]
        public int DisplayOrder { get; set; }
    }
}