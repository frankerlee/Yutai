using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Carto.UI;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.MapLegend
{
    class CmdLayerProperties : YutaiCommand
    {

        private IMapLegendView _view;

        public CmdLayerProperties(IAppContext context)
        {
            _context = context;
           
            OnCreate();
        }

        public override bool Enabled
        {
            get
            {
                if (_context == null) return false;
                if (_context.CurrentLayer == null) return false;
                return true;
            }
        }

        private void OnCreate()
        {
            base.m_caption = "图层属性";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "mnuLayerProperties";
            base._key = "mnuLayerProperties";
            base.m_toolTip = "图层属性";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;

        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context=hook as IAppContext;
            
            OnCreate();
        }

       
        public void OnClick()
        {

            if (this.Enabled)
            {
                if ((_context.CurrentLayer is IFeatureLayer || _context.CurrentLayer is IGroupLayer ? true : _context.CurrentLayer is ICadLayer))
                {
                    frmLayerPropertyEx _frmLayerPropertyEx = new frmLayerPropertyEx()
                    {
                        FocusMap = _context.FocusMap as IBasicMap,
                        SelectItem = _context.CurrentLayer,
                        StyleGallery = _context.StyleGallery
                    };
                    if (_frmLayerPropertyEx.ShowDialog() == DialogResult.OK)
                    {
                        (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, _context.CurrentLayer, null);
                        
                        _context.MapDocumentChanged();
                    }
                }
            }
        }
    }
}