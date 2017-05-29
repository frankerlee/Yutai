using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;
using Yutai.Shared;

namespace Yutai.Services.Concrete
{
    public class ProjectLoader : ProjectLoaderBase, IProjectLoader
    {
        private readonly ImageSerializationService _imageSerializationService;
        private readonly IBroadcasterService _broadcaster;
        private readonly ISecureContext _context;
        private string _basePath;

        public ProjectLoader(IAppContext context, ImageSerializationService imageSerializationService,
            IBroadcasterService broadcaster)
        {
            if (imageSerializationService == null) throw new ArgumentNullException("imageSerializationService");
            _imageSerializationService = imageSerializationService;
            _broadcaster = broadcaster;

            _context = context as ISecureContext;
            if (_context == null)
            {
                throw new InvalidCastException("Application context must support ISerializable_context interface.");
            }
        }

        /// <summary>
        /// Restores the state of application by populating application _context after project file was deserialized.
        /// </summary>
        public bool Restore(XmlProject project)
        {
            _context.MainView.Lock();
            System.IO.FileInfo fileInfo=new FileInfo(project.Settings.LoadAsFilename);
            _basePath = fileInfo.DirectoryName;

            try
            {
                RestoreMxdDocument(project);
                RestoreSxdDocument(project);

             //   RestorePlugins(project);

                RestoreExtents(project);

                RestoreLocator(project);


                return true;
            }
            finally
            {
                _context.YutaiProject = project;
               _context.MainView.Unlock();
                _broadcaster.BroadcastEvent(p=>p.ProjectOpened_,this,null);
            }
        }

      
        private void RestorePlugins(XmlProject project)
        {
            foreach (var p in project.Plugins)
            {
                _context.PluginManager.LoadPlugin(p.Guid, _context);
            }
        }

        private void RestoreExtents(XmlProject project)
        {
            if (project.Envelope != null)
            {
                IEnvelope  env=new Envelope() as IEnvelope;
                env.XMin = project.Envelope.XMin;
                env.XMax = project.Envelope.XMax;
                env.YMin = project.Envelope.YMin;
                env.YMax = project.Envelope.YMax;
                _context.MapControl.ActiveView.Extent = env;
            }
        }

        private void RestoreLocator(XmlProject project)
        {
            //var locator = project.Locator;
            //if (locator != null)
            //{
            //    var bitmap = _imageSerializationService.ConvertStringToImage(locator.Image, locator.Type);
            //    _context.Locator.RestorePicture(bitmap as System.Drawing.Image, locator.Dx, locator.Dy, locator.XllCenter, locator.YllCenter);
            //}
        }

        private void RestoreSxdDocument(XmlProject project)
        {
            if (string.IsNullOrEmpty(project.MapDocumentName))
            {
                return;
            }
            string sxdPath = _basePath + "\\" + project.SceneDocumentName;
            if (File.Exists(sxdPath))
                _context.SceneControl.LoadSxFile(sxdPath);
            else
            {
                MessageService.Current.Warn("三维文档不存在，"+sxdPath);
            }
        }

        private void RestoreMxdDocument(XmlProject project)
        {
            if (string.IsNullOrEmpty(project.MapDocumentName))
            {
                return;
            }
            string mxdPath = _basePath + "\\" + project.MapDocumentName;
            if(File.Exists(mxdPath))
                _context.MapControl.LoadMxFile(mxdPath, null,null);
            else
            {
                MessageService.Current.Warn("二维文档不存在，" + mxdPath);
            }
        }
    }
}
