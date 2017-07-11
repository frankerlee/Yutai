using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;


namespace Yutai.Commands.MapLegend
{
    public class CmdLegendAddData : YutaiCommand
    {
        private IMapLegendView _view;
        private ICommand _command;
        private AddDataHelper pHelper = null;
        private IList ilist_0 = null;

        public CmdLegendAddData(IAppContext context, IMapLegendView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        public override bool Enabled
        {
            get
            {
                if (_view == null) return false;
                if (_view.SelectedMap == null)
                {
                    if (_view.SelectedLayer == null) return false;
                    return _view.SelectedLayer is IGroupLayer ? true : false;
                }
                else
                {
                    return true;
                }
            }
        }

       private void OnCreate()
        {
            base.m_caption = "新增数据源";
            base.m_category = "TOC";
            base.m_bitmap = Properties.Resources.icon_layer_add;
            base.m_name = "mnuAddData";
            base._key = "mnuAddData";
            base.m_toolTip = "新增数据源";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }

      

        public override void OnClick(object sender, EventArgs args)
        {
            if (_command == null)
            {
                _command = new ESRI.ArcGIS.Controls.ControlsAddDataCommandClass();
                _command.OnCreate(_context.MapControl);
            }
            object parentObject = null;

            if (_view.SelectedLayer != null && _view.SelectedLayer is IGroupLayer)
            {
                parentObject = _view.SelectedLayer;
            }
            else
            {
                parentObject = _context.FocusMap as IActiveView;
            }

            frmOpenFile _frmOpenFile = new frmOpenFile()
            {
                Text = "添加数据",
                AllowMultiSelect = true
            };
            _frmOpenFile.AddFilter(new MyGxFilterDatasets(), true);
            if (_frmOpenFile.DoModalOpen() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.pHelper = new AddDataHelper(this.GetMap() as IActiveView);
                this.ilist_0 = _frmOpenFile.SelectedItems;
                this.pHelper.m_pApp = _context;
                if (_view.SelectedLayer != null && _view.SelectedLayer is IGroupLayer)
                {
                    pHelper.ParentLayer = _view.SelectedLayer as IGroupLayer;
                }
                this.pHelper.LoadData(this.ilist_0);
                Cursor.Current = Cursors.Default;
            }

        }


        private void method_2()
        {
            Cursor.Current = Cursors.Default;
            (this.GetMap() as IActiveView).ScreenDisplay.UpdateWindow();
        }




        private IMap GetMap()
        {
            return this._context.FocusMap;
        }

        private void method_1()
        {
            this.pHelper.InvokeMethod(this.ilist_0);
        }
        public override void OnCreate(object hook)
        {
            OnCreate();
        }

        private List<ILayer> GetLayers()
        {
            List<ILayer> layers = new List<ILayer>();

            for (int i = 0; i < _view.SelectedMap.LayerCount; i++)
            {
                layers.Add(_view.SelectedMap.Layer[i]);
            }

            return layers;
        }
    }
}