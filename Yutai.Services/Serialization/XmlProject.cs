using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Services.Serialization
{
     [DataContract(Name = "YutaiGIS")]
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

             Settings = new XmlProjectSettings { SavedAsFilename = filename };

            /* if (!context.Locator.Empty)
             {
                 var service = context.Container.GetInstance<ImageSerializationService>();
                 Locator = new XmlMapLocator(context.Locator, service);
             }*/

             
         }

        /* [DataMember]
         public XmlMap Map { get; set; }
         [DataMember]
         public XmlMapLocator Locator { get; set; }*/
         [DataMember]
         public XmlProjectSettings Settings { get; set; }
      /*   [DataMember]
         public List<XmlGroup> Groups { get; set; }
         [DataMember]
         public List<XmlLayer> Layers { get; set; }
         [DataMember]
         public List<XmlPlugin> Plugins { get; set; }*/

    }
}
