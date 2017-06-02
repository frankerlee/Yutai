using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Identifer.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Commands
{
    public class CmdSelectByBuffer:YutaiCommand,ICommandSubType
    {
        private int _bufferType = 0;
        private IAppContext _context;
        private int _subType;

        public override string Caption
        {
            get
            {
                return (this._bufferType == 0 ? "选择包含在缓冲区中的要素" : "选择和缓冲区相交要素");
            }
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.MapControl.Map == null)
                {
                    flag = false;
                }
                else if (_context.MapControl.Map.LayerCount != 0)
                {
                    flag = ((_context==null) || _context.BufferGeometry == null ? false : true);
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }

        public override string Name
        {
            get
            {
                return (this._bufferType == 0 ? "SelectFeatureByBufferContains" : "SelectFeatureByBufferIntersects");
            }
        }

        public CmdSelectByBuffer(IAppContext context)
        {
            OnCreate(context);
        }

        public int GetCount()
        {
            return 2;
        }

        public int SubType
        {
            get { return _subType; }
            set { _subType = value; SetSubType(value);}
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            this.m_caption = "选择包含在缓冲区中的要素";
            this.m_category = "Query";
            this.m_message = "选择包含在缓冲区中的要素";
            this.m_name = "Query.Buffer.SelectFeatureByBuffer";
            base._key = "Query.Buffer.SelectFeatureByBuffer";
            this.m_toolTip = "选择包含在缓冲区中的要素";
            _subType = 0;
        }

        public override void OnClick()
        {
            esriSpatialRelEnum _esriSpatialRelEnum;
            _esriSpatialRelEnum = (this._bufferType == 0 ? esriSpatialRelEnum.esriSpatialRelContains : esriSpatialRelEnum.esriSpatialRelIntersects);
            _context.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            QueryHelper.SelectByPolygon(_context.MapControl.Map, _context.BufferGeometry as IPolygon, _esriSpatialRelEnum);
            _context.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public void SetSubType(int subType)
        {
            this._bufferType = subType;

            switch (subType)
            {
                case 0:
                    {
                        this.m_bitmap = Properties.Resources.SelectFeatures;
                        
                        this.m_toolTip = "选择包含在缓冲区中的要素";
                        this.m_name = "Query.Buffer.SelectFeatureByBuffer";
                        this.m_message = "选择包含在缓冲区中的要素";
                        this.m_caption = "选择包含在缓冲区中的要素";
                        base.m_category = "Query";
                        base._key = "Query.Buffer.SelectFeatureByBuffer";
                        base._itemType = RibbonItemType.NormalItem;
                        break;
                    }
                case 1:
                    {
                        this.m_bitmap = Properties.Resources.PolygonSelectFeatures;
                       
                        this.m_toolTip = "选择和缓冲区相交要素";
                        this.m_name = "Query.Buffer.SelectFeatureByBuffer2";
                        this.m_message = "选择和缓冲区相交要素";
                        this.m_caption = "选择和缓冲区相交要素";
                        base.m_category = "Query";
                        base._key = "Query.Buffer.SelectFeatureByBuffer2";
                        base._itemType = RibbonItemType.NormalItem;
                        break;
                    }
            }

        }
    }
}