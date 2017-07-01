﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;
using Yutai.Plugins.Helpers;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Services;
using Yutai.Shared;

namespace Yutai.Plugins
{
    internal class PluginManager : IPluginManager
    {
        private const string PluginDirectory = "Plugins";

        private readonly HashSet<PluginIdentity> _active = new HashSet<PluginIdentity>();

        private readonly IApplicationContainer _container;

        private readonly List<BasePlugin> _plugins = new List<BasePlugin>(); // all valid plugins

        private readonly MainPlugin _mainPlugin;

        [ImportMany]
#pragma warning disable 649
        private IEnumerable<Lazy<IPlugin, IPluginMetadata>> _mefPlugins; // found by MEF
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginManager"/> class.
        /// </summary>
        public PluginManager(IApplicationContainer container, MainPlugin mainPlugin)
        {
            Logger.Current.Trace("In PluginManager");
            if (container == null) throw new ArgumentNullException("container");
            if (mainPlugin == null) throw new ArgumentNullException("mainPlugin");

            _container = container;
            _mainPlugin = mainPlugin;
        }

        public event EventHandler<PluginEventArgs> PluginUnloaded;

        /// <summary>
        /// Gets list of all plugins both active and not.
        /// </summary>
        public IEnumerable<BasePlugin> AllPlugins
        {
            get { return _plugins; }
        }

        public IEnumerable<BasePlugin> ApplicationPlugins
        {
            get { return _plugins.Where(p => p.IsApplicationPlugin); }
        }

        public IEnumerable<BasePlugin> CustomPlugins
        {
            get { return _plugins.Where(p => !p.IsApplicationPlugin); }
        }

        /// <summary>
        /// Gets list of only active plugins.
        /// </summary>
        public IEnumerable<BasePlugin> ActivePlugins
        {
            get
            {
                // TODO: cache it each time the list of plugins changes to spare the time on search for each event
                return _plugins.Where(p => _active.Contains(p.Identity)).ToList();
            }
        }

        public IEnumerable<BasePlugin> ListeningPlugins
        {
            get { return (new[] {_mainPlugin}).Concat(_plugins.Where(p => _active.Contains(p.Identity))); }
        }

        /// <summary>
        /// Validates the list of plugins loaded by MEF.
        /// </summary>
        public void ValidatePlugins(ISplashView splashView)
        {
            _plugins.Clear();

            var dict = new Dictionary<Guid, BasePlugin>();

            if (_mefPlugins == null)
            {
                return;
            }

            foreach (var item in _mefPlugins)
            {
                var p = item.Value as BasePlugin;
                if (p == null)
                {
                    Logger.Current.Warn("Invalid plugin type: plugin must inherit from BasePlugin type.");
                    continue;
                }

                try
                {
                    p.Identity = PluginIdentityHelper.GetIdentity(p.GetType(), item.Metadata);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to load plugin identity from assembly.", ex);
                }

                // TODO: make sure that application plugins will have priority if duplicate GUIDs are found
                if (dict.ContainsKey(p.Identity.Guid))
                {
                    var p2 = dict[p.Identity.Guid];
                    string msg = string.Format("Plugins have duplicate GUIDs: {0} {1}", p, p2);
                    throw new ApplicationException(msg);
                }
                splashView.ShowStatus("正在检查：" + p.Description);
                dict.Add(p.Identity.Guid, p);

                _container.RegisterInstance(p.GetType(), p);

                _plugins.Add(p);
            }

#if DEBUG
            // I didn't caught anything by this check so far;
            // Perhaps better just to check for particular assemblies in Plugins folder
            // and display warning if they are present
            CheckDuplicatedAssemblies();
#endif
        }

        /// <summary>
        /// Searches plugins in plugins folder with MEF.
        /// </summary>
        public void AssemblePlugins(ISplashView splashView)
        {
            try
            {
                var aggregateCatalog = new AggregateCatalog();

                aggregateCatalog.Catalogs.Add(GetPluginCatalog());

                var container = new CompositionContainer(aggregateCatalog);

                container.ComposeParts(this);

                ValidatePlugins(splashView);
            }
            catch (Exception ex)
            {
                Logger.Current.Error("Failed to initialize plugin manager", ex);
            }
        }

        /// <summary>
        /// Loads a single plugin.
        /// </summary>
        /// <param name="identity">Plugin identity.</param>
        /// <param name="context">Application context.</param>
        public void LoadPlugin(PluginIdentity identity, IAppContext context)
        {
            context.ShowSplashMessage("正在引导:" + identity.Name);
            LoadPlugin(identity.Guid, context);
        }

        public void LoadPlugin(Guid pluginGuid, IAppContext context)
        {
            if (_active.Select(p => p.Guid).Contains(pluginGuid))
            {
                return; // it's already loaded
            }

            var plugin = _plugins.FirstOrDefault(p => p.Identity.Guid == pluginGuid);
            context.ShowSplashMessage("正在引导:" + plugin.Identity.Name);
            if (plugin == null)
            {
                throw new ApplicationException("Plugin which requested for loading isn't present in the list.");
            }

            try
            {
                plugin.DoRegisterServices(context.Container);
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn("Failed to register services for plugin: " + plugin.Identity +
                                            Environment.NewLine + ex.Message);
                return;
            }

            try
            {
                plugin.Initialize(context);
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn("Failed to load plugin: " + plugin.Identity + Environment.NewLine +
                                            ex.Message);
                return;
            }

            _active.Add(plugin.Identity);
        }

        /// <summary>
        /// Unloads single plugin and removes associated menus & toolbars
        /// </summary>
        /// <param name="identity">Plugin identity.</param>
        /// <param name="context">Application context.</param>
        /// <exception cref="System.ApplicationException">Plugin which requested for unloading isn't present in the list.</exception>
        public void UnloadPlugin(PluginIdentity identity, IAppContext context)
        {
            var plugin = _plugins.FirstOrDefault(p => p.Identity == identity);
            if (plugin == null)
            {
                throw new ApplicationException("Plugin which requested for unloading isn't present in the list.");
            }

            plugin.Terminate();
            _active.Remove(identity);

            FirePluginUnloaded(identity);
        }

        public void RestoreApplicationPlugins(IEnumerable<Guid> plugins, IAppContext context)
        {
            var dict = new HashSet<Guid>(plugins);

            foreach (var p in AllPlugins)
            {
                bool active = dict.Contains(p.Identity.Guid);
                p.SetApplicationPlugin(active);

                if (active && !PluginActive(p.Identity))
                {
                    context.ShowSplashMessage("正在引导:" + p.Identity.Name);
                    LoadPlugin(p.Identity, context);
                }
            }
        }

        public bool PluginActive(PluginIdentity identity)
        {
            return _active.Contains(identity);
        }

        /// <summary>
        /// Checks if the same assembly was loaded from different locations in the app domain.
        /// Most likely from Plugins folder if "Copy local" flag wasn't turned off.
        /// </summary>
        private void CheckDuplicatedAssemblies()
        {
            var list = LoadedAssemblyChecker.GetConflictingAssemblies();
            foreach (var info in list)
            {
                string s = string.Format("Detected multiple load of assembly {0} from ", info.Key);

                foreach (string location in info.Value)
                {
                    s += string.Format("\t{0}", location);
                }

                Logger.Current.Warn(s);
            }
        }

        private void FirePluginUnloaded(PluginIdentity identity)
        {
            var handler = PluginUnloaded;
            if (handler != null)
            {
                handler.Invoke(this, new PluginEventArgs(identity));
            }
        }

        /// <summary>
        /// Gets the plugin catalog, i.e. directory to look for plugins and filename mask.
        /// </summary>
        private DirectoryCatalog GetPluginCatalog()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";

            path = Path.Combine(path, PluginDirectory);

            return new DirectoryCatalog(path, "Yutai*.dll");
        }
    }
}