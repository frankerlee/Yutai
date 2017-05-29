using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Services.Serialization
{
    [DataContract(Name = "YutaiGIS", Namespace = "")]
    public class XmlProject
    {
       public XmlProject(ISecureContext context, string filename)
        {
            /*int selectedHandle = context.Legend.SelectedLayerHandle;

            Layers = context.Legend.Layers.Select(l => new XmlLayer(l, l.Handle == selectedHandle)).ToList();

            Groups = context.Legend.Groups.Select(g => new XmlGroup(g)).ToList();

            Plugins = context.PluginManager.ActivePlugins.Select(p => new XmlPlugin()
            {
                Name = p.Identity.Name,
                Guid = p.Identity.Guid
            }).ToList();*/

            /* Map = new XmlMap
             {
                 Projection = context.Map.Projection.ExportToWkt(),
                 Envelope = new XmlEnvelope(context.Map.Extents),
                 TileProviderId = context.Map.Tiles.ProviderId
             };*/

            Settings = new XmlProjectSettings {SavedAsFilename = filename};

            /* if (!context.Locator.Empty)
             {
                 var service = context.Container.GetInstance<ImageSerializationService>();
                 Locator = new XmlMapLocator(context.Locator, service);
             }*/
        }

        [DataMember(Name = "MapDoc",Order=0)]
        public string MapDocumentName { get; set; }

        [DataMember(Name = "SceneDoc", Order = 1)]
        public string SceneDocumentName { get; set; }

        [DataMember(Name="ViewStyle", Order = 2)]
        public int ViewStyle { get; set; }

        [DataMember(IsRequired = false,Order = 3)]
        public XmlEnvelope Envelope { get; set; }

        [DataMember(IsRequired = false,Order = 4)]
        public List<XmlPlugin> Plugins { get; set; }

        [DataMember(IsRequired = false,Order = 5)]
        public List<XmlLocator> Locators { get; set; }

        [DataMember(IsRequired = false,Order = 6)]
        public XmlProjectSettings Settings { get; set; }

        [DataMember(IsRequired = false, Order = 7)]
        public XmlOverview Overview { get; set; }
    }

    [DataContract(Name = "Overview", Namespace = "")]
    public class XmlOverview
    {
        [DataMember(Name = "OverviewType", Order = 0)]
        public string OverviewType { get; set; }

        [DataMember(Name = "ObjectName", Order = 1)]
        public string ObjectName { get; set; }
    }
}