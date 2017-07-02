using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Tools.XPMenus;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Services;
using Yutai.Shared;
using Yutai.UI.Helpers;
using Yutai.UI.Style;
using Yutai.Views.Abstract;

namespace Yutai.Views
{
    public class ConfigPresenter : ComplexPresenter<IConfigView, ConfigCommand, ConfigViewModel>
    {
        private readonly IAppContext _context;
        private readonly IConfigService _configService;
        private readonly IStyleService _styleService;
        private readonly IPluginManager _pluginManger;


        public ConfigPresenter(IAppContext context, IConfigView view, IConfigService configService,
            IStyleService styleService, IPluginManager pluginManger)
            : base(view)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (view == null) throw new ArgumentNullException("view");
            if (configService == null) throw new ArgumentNullException("configService");
            if (styleService == null) throw new ArgumentNullException("styleService");
            if (pluginManger == null) throw new ArgumentNullException("pluginManger");


            _context = context;
            _configService = configService;
            _styleService = styleService;
            _pluginManger = pluginManger;


            view.PageShown += OnPageShown;
        }

        public override void RunCommand(ConfigCommand command)
        {
            switch (command)
            {
                case ConfigCommand.RestoreToolbars:
                    if (MessageService.Current.Ask("Do you want to restore default location of toolbars?"))
                    {
                        var view = _context.Container.Resolve<IMainView>();
                        var manager = view.MenuManager as MainFrameBarManager;
                        if (manager != null)
                        {
                            manager.RestoreLayout(NewMainView.SerializationKey, true);
                        }
                    }
                    break;
                case ConfigCommand.RestorePlugins:
                    RestorePlugins();
                    break;
                case ConfigCommand.Save:
                    ApplySettings();

                    bool result = _configService.SaveConfig();
                    if (result)
                    {
                        MessageService.Current.Info("Configuration was saved successfully.");
                    }
                    break;
                case ConfigCommand.SetDefaults:
                    if (MessageService.Current.Ask("Do you want to reset default value of all settings?"))
                    {
                        _configService.Config.SetDefaults();
                        foreach (var page in Model.Pages)
                        {
                            page.Initialize();
                        }
                    }

                    ApplySettings();

                    break;
                case ConfigCommand.OpenFolder:
                    string path = _configService.ConfigPath;
                    PathHelper.OpenFolderWithExplorer(path);
                    break;
            }
        }

        private void RestorePlugins()
        {
            if (
                !MessageService.Current.Ask(
                    "Do you want to restore default set of plugins and location of their panels?"))
            {
                return;
            }

            try
            {
                var guids = AppConfig.Instance.DefaultApplicationPlugins;
                _pluginManger.RestoreApplicationPlugins(guids, _context);
                Model.ReloadPage(ConfigPageType.Plugins);

                // restoring layout
                var view = _context.Container.Resolve<IMainView>();
                var manager = view.DockingManager as DockingManager;
                manager.RestoreLayout(NewMainView.SerializationKey, true);
            }
            catch (Exception ex)
            {
                Logger.Current.Error("Failed to restore dock panel layout.", ex);
            }
        }

        private void OnPageShown()
        {
            _styleService.ApplyStyle(View as Form);
        }

        private void ApplySettings()
        {
            foreach (var page in Model.Pages)
            {
                page.Save();
            }

            //_context.ApplyConfig(_configService);
        }

        public override bool ViewOkClicked()
        {
            ApplySettings();
            _configService.SaveAll();
            return true;
        }
    }
}