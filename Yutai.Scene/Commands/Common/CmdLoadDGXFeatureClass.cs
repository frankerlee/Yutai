using System;
using System.Collections;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Scene.Forms;

namespace Yutai.Plugins.Scene.Commands.Common
{
    public class CmdLoadDGXFeatureClass : YutaiCommand
    {
        private IAppContext _context;
        private IScenePlugin _plugin;

        private IList ilist_0 = null;

        public override bool Enabled
        {
            get
            {
                if (_plugin == null) return false;
                return this._plugin.Scene != null && this._plugin.SceneVisible ;

            }
        }
        private IBasicMap method_0()
        {
            return this._plugin.Scene as IBasicMap;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public CmdLoadDGXFeatureClass(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_LoadDGXFeatureClass";
            _itemType = RibbonItemType.Button;
            this.m_caption = "转载等高线";
            this.m_category = "文件";
            this.m_message = "转载等高线";
            this.m_name = "Scene_LoadDGXFeatureClass";
            this.m_toolTip = "转载等高线";
            m_bitmap = Properties.Resources.counter;
        }

      

        public override void OnClick()
        {
            frmOpenFile frmOpenFile = new frmOpenFile();
            frmOpenFile.Text = "添加数据";
            frmOpenFile.AllowMultiSelect = false;
            frmOpenFile.AddFilter(new MyGxFilterPolylineFeatureClasses(), true);
            if (frmOpenFile.DoModalOpen() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                IArray items = frmOpenFile.Items;
                IGxDataset gxDataset = items.get_Element(0) as IGxDataset;
                IFeatureClass featureClass = gxDataset.Dataset as IFeatureClass;
                new frmDGXFeatureClassToTin
                {
                    FeatureClass = featureClass,
                    m_pMap = this._plugin.Scene as IBasicMap
                }.ShowDialog();
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }
    }
}