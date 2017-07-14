using System;
using System.Collections;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Scene.Forms;

namespace Yutai.Plugins.Scene.Commands.Common
{
    public class CmdSceneAddData : YutaiCommand
    {
        private IAppContext _context;
        private IScenePlugin _plugin;

        private Add3DDataHelper add3DDataHelper_0 = null;

        private IList ilist_0 = null;
        public override bool Enabled
        {
            get
            {
                if (_plugin == null) return false;
                return this._plugin.Scene != null && this._plugin.SceneVisible;

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

        public CmdSceneAddData(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_AddData";
            _itemType = RibbonItemType.Button;
            this.m_bitmap = Properties.Resources.AddData;
            this.m_caption = "添加数据";
            this.m_category = "文件";
            this.m_message = "添加数据";
            this.m_name = "Scene_AddData";
            this.m_toolTip = "添加数据";
        }

       
        private void method_1()
        {
            this.add3DDataHelper_0.InvokeMethod(this.ilist_0);
        }

        public override void OnClick()
        {
            frmOpenFile frmOpenFile = new frmOpenFile();
            frmOpenFile.Text = "添加数据";
            frmOpenFile.AllowMultiSelect = true;
            frmOpenFile.AddFilter(new MyGxFilterDatasets(), true);
            if (frmOpenFile.DoModalOpen() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                this.add3DDataHelper_0 = new Add3DDataHelper(this.method_0());
                this.ilist_0 = frmOpenFile.SelectedItems;
                this.add3DDataHelper_0.LoadData(this.ilist_0);
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }
    }
}