using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Scene.Forms;

namespace Yutai.Plugins.Scene.Commands.Creator
{
    public class CmdCreateBuilding3D : YutaiCommand
    {
        private IAppContext _context;
        private IScenePlugin _plugin;

        public override bool Enabled
        {
            get
            {
                if (_plugin == null) return false;
                return this._plugin.Scene != null && this._plugin.SceneVisible && (this._plugin.Scene as IBasicMap).LayerCount > 0;

            }
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public CmdCreateBuilding3D(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_CreateBuilding3D";
            _itemType = RibbonItemType.Button;
            this.m_caption = "创建建筑物三维";
            this.m_name = "Scene_CreateBuilding3D";
            this.m_toolTip = "创建建筑物三维";
            this.m_category = "三维物体";
            m_bitmap = Properties.Resources.icon_3d_building;
            _needUpdateEvent = true;

        }



        public override void OnClick()
        {
            frmFeaturesToBuildings frmFeaturesToBuilding = new frmFeaturesToBuildings(_plugin)
            {
                Map = this._plugin.Scene as IBasicMap
            };
            frmFeaturesToBuilding.ShowDialog();
            this._plugin.SceneGraph.RefreshViewers();
        }
    }
}
