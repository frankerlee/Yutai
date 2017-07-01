using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Properties;
using Yutai.UI.Controls;
using Yutai.Views;

namespace Yutai.Configuration
{
    public partial class GeneralConfigPage : ConfigPageBase, IConfigPage
    {
        private readonly IConfigService _configService;

        public GeneralConfigPage(IConfigService configService)
        {
            if (configService == null) throw new ArgumentNullException("configService");
            _configService = configService;

            InitializeComponent();

            InitControls();

            Initialize();
        }

        private void InitControls()
        {
            //cboSymbologyStorage.AddItemsFromEnum<SymbologyStorage>();
        }

        public void Initialize()
        {
            var config = _configService.Config;
            chkLoadLastProject.Checked = config.LoadLastProject;
            //chkLoadSymbology.Checked = config.LoadSymbology;
            chkShowWelcomeDialog.Checked = config.ShowWelcomeDialog;
            //cboSymbologyStorage.SetValue(config.SymbolobyStorage);
            //chkShowPluginInToolTip.Checked = config.ShowPluginInToolTip;
            chkShowMenuToolTips.Checked = config.ShowMenuToolTips;
            //chkDynamicVisibilityWarnings.Checked = config.DisplayDynamicVisibilityWarnings;
            chkLocalDocumentation.Checked = config.LocalDocumentation;
            //chkNewVersion.Checked = config.UpdaterCheckNewVersion;
            //chkLegendExpanded.Checked = config.LegendExpandLayersOnAdding;
        }

        public string PageName
        {
            get { return "常用配置"; }
        }

        public void Save()
        {
            var config = _configService.Config;
            config.LoadLastProject = chkLoadLastProject.Checked;
            //config.LoadSymbology = chkLoadSymbology.Checked;
            config.ShowWelcomeDialog = chkShowWelcomeDialog.Checked;
            //config.ShowPluginInToolTip = chkShowPluginInToolTip.Checked;
            config.ShowMenuToolTips = chkShowMenuToolTips.Checked;
            //config.DisplayDynamicVisibilityWarnings = chkDynamicVisibilityWarnings.Checked;
            //config.SymbolobyStorage = cboSymbologyStorage.GetValue<SymbologyStorage>();
            config.LocalDocumentation = chkLocalDocumentation.Checked;
            //config.UpdaterCheckNewVersion = chkNewVersion.Checked;
            //config.LegendExpandLayersOnAdding = chkLegendExpanded.Checked;
        }

        public string Key
        {
            get { return "General"; }
        }

        public Bitmap Icon
        {
            get { return Resources.img_component32; }
        }

        public ConfigPageType PageType
        {
            get { return ConfigPageType.General; }
        }

        public string Description
        {
            get { return "系统的一些常用配置"; }
        }

        public bool VariableHeight
        {
            get { return false; }
        }
    }
}