using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Metadata.Menu;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Scene;


namespace Yutai.Plugins.Metadata
{
    [YutaiPlugin()]
    public class MetadataPlugin : BasePlugin
    {
        private IAppContext _context;
        private MenuGenerator _menuGenerator;
    
        


        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
        }

        public override void Initialize(IAppContext context)
        {
            _context = context;
            _menuGenerator = context.Container.GetInstance<MenuGenerator>();
          
        }

      
        private void FireEvent<T>(EventHandler<T> handler, T args)
        {
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public override IEnumerable<IConfigPage> ConfigPages
        {
            get {
               yield break;}
        }


      
      
    }

   
   
}