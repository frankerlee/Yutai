using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Controls;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.SymbolUI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands.Common
{
    public class CmdSceneStyleManagerItem : YutaiCommand
    {
        private IAppContext _context;
        private IScenePlugin _plugin;

        public override bool Enabled
        {
            get
            {
                if (_plugin == null) return false;
                return this._plugin.Scene != null && this._plugin.SceneVisible;

            }
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public CmdSceneStyleManagerItem(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_StyleManagerItem";
            _itemType = RibbonItemType.Button;
            this.m_caption = "符号管理器";
            this.m_name = "Scene_StyleManagerItem";
            this.m_toolTip = "符号管理器";
            m_bitmap = Properties.Resources.gallery;
        }

        public override void OnClick()
        {
            frmStyleManagerDialog frmStyleManagerDialog = new frmStyleManagerDialog();
            frmStyleManagerDialog.SetStyleGallery(_context.StyleGallery);
            frmStyleManagerDialog.ShowDialog();
        }
    }
}
