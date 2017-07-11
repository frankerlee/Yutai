using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Docking;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;
using Yutai.UI.Docking;

namespace Yutai.Commands.Windows
{
    class CmdOverviewDock : YutaiCommand
    {
        private bool _checked;

        public override bool Checked
        {
            get { return _checked; }
        }

        public CmdOverviewDock(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            ISecureContext sContext = _context as ISecureContext;
            DockPanel dock = _context.DockPanels.GetDockPanel(OverviewDockPanel.DefaultDockName);
            if (dock == null) return;
            if (dock.Visible)
            {
                dock.Hide();
                _checked = false;
                return;
            }
            else
            {
                dock.Show();
                _checked = true;
                return;
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "鹰眼视图";
            base.m_category = "Window";
            base.m_bitmap = Properties.Resources.icon_overview;
            base.m_name = "Window.Common.Overview";
            base._key = "Window_Common_Overview";
            base.m_toolTip = "鹰眼视图";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.CheckBox;
        }
    }
}