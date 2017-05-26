using System;
using System.Collections.Generic;
using System.Drawing;
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

        [DataMember(Name="MapDoc")]
        public  string MapDocumentName { get; set; }
        [DataMember(Name = "SceneDoc")]
        public string SceneDocumentName { get; set; }

        [DataMember(Name = "ViewStyle")]
        public int ViewStyle { get; set; }

        [DataMember(Name = "Envelope")]
        public XmlEnvelope  Envelope { get; set; }

        [DataMember]
        public List<XmlPlugin> Plugins { get; set; }

        [DataMember(Name = "Extensions")]
        public List<XmlExtensions> Extensions { get; set; }
       
        [DataMember]
         public XmlProjectSettings Settings { get; set; }
   

    }

    [DataContract]
    public class XmlExtensions
    {
        [DataMember]
        public List<XmlMapLocator> Locators { get; set; }
    }

    [DataContract]
    public class XmlMapLocator
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public Bitmap Bitmap { get; set; }

        [DataMember]
        public string LayerName { get; set; }

        [DataMember]
        public string Fields { get; set; }

        [DataMember]
        public int Order { get; set; }
    }

    [DataContract]
    public class XmlEnvelope
    {
        public XmlEnvelope(ESRI.ArcGIS.Geometry.IEnvelope e)
        {
            XMin = e.XMin;
            XMax = e.XMax;
            YMin = e.YMin;
            YMax = e.YMax;
        }
        [DataMember]
        public double XMin { get; set; }
        [DataMember]
        public double XMax { get; set; }
        [DataMember]
        public double YMin { get; set; }
        [DataMember]
        public double YMax { get; set; }
    }
}
