using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.Editor.Properties;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.UI.Controls;

namespace Yutai.Plugins.Editor.Controls
{
    public partial class SnapConfigPage :   ConfigPageBase, IConfigPage
    {
        private readonly IConfigService _configService;

        public SnapConfigPage(IConfigService configService)
        {
            if (configService == null) throw new ArgumentNullException("configService");
            _configService = configService;

            InitializeComponent();

           

            Initialize();
        }

     

        public void Initialize()
        {
            var config = _configService.Config;
            chkSnap.Checked = config.UseSnap;
            chkSnapBoundary.Checked = config.IsSnapBoundary;
            chkSnapEndPoint.Checked = config.IsSnapEndPoint;
            chkSnapIntersectPoint.Checked = config.IsSnapIntersectionPoint;
            chkSnapMidPoint.Checked = config.IsSnapMiddlePoint;
            chkSnapPoint.Checked = config.IsSnapPoint;
            chkSnapSketch.Checked = config.IsSnapSketch;
            chkSnapTargent.Checked = config.IsSnapTangent;
            chkSnapVertexPoint.Checked = config.IsSnapVertexPoint;
           
        }

        public string PageName
        {
            get { return "捕捉配置"; }
        }

        public void Save()
        {
            var config = _configService.Config;
            config.UseSnap = chkSnap.Checked;
            config.IsSnapBoundary = chkSnapBoundary.Checked;
             config.IsSnapEndPoint = chkSnapEndPoint.Checked;
            config.IsSnapIntersectionPoint = chkSnapIntersectPoint.Checked;
             config.IsSnapMiddlePoint = chkSnapMidPoint.Checked;
            config.IsSnapPoint = chkSnapPoint.Checked;
           config.IsSnapSketch = chkSnapSketch.Checked;
            config.IsSnapTangent = chkSnapTargent.Checked;
             config.IsSnapVertexPoint = chkSnapVertexPoint.Checked;
        }

        public string Key { get { return "Snap"; } }

        

        public Bitmap Icon
        {
            get { return Resources.icon_snap; }
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
