using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        private string _basicName;

        

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.MapControl == null || _context.MapControl.Map == null)
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
            get { return _bufferType; }
            set { _bufferType = value; SetSubType(value);}
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_bitmap = Properties.Resources.icon_select_buffer;

            base.m_toolTip = "缓冲区选择";
            base.m_name = "Query_SelectionTools_BufferSelector";
            base.m_message = "缓冲区选择";
            base.m_caption = "缓冲区选择";
            base.m_category = "Query";
            base._key = "Query_SelectionTools_BufferSelector";
            base._itemType = RibbonItemType.DropDown;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            base.DisplayStyleYT = DisplayStyleYT.ImageAndText;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
        }

        public override void OnClick()
        {

            esriSpatialRelEnum _esriSpatialRelEnum;
            _esriSpatialRelEnum = (this._bufferType == 0 ? esriSpatialRelEnum.esriSpatialRelContains : esriSpatialRelEnum.esriSpatialRelIntersects);
            ((IActiveView)_context.MapControl.Map).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            QueryHelper.SelectByPolygon(_context.MapControl.Map, _context.BufferGeometry as IPolygon, _esriSpatialRelEnum);
            ((IActiveView)_context.MapControl.Map).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            ToolStripItem strip = sender as ToolStripItem;
            if (strip.Name == _basicName) return;
            CmdSelectByBuffer tag = strip.Tag as CmdSelectByBuffer;
           
            _bufferType = tag.SubType;
            SetSubType(_bufferType);
            
            OnClick();
        }

      

        private IMap GetMap()
        {
            return this._context.MapControl.Map;
        }
        public void SetSubType(int subType)
        {
            this._bufferType = subType;

            switch (subType)
            {
                case -1:
                {
                        base.m_bitmap = Properties.Resources.icon_select_buffer;
                        this._basicName = this.m_name;
                        base.m_toolTip = "缓冲区选择";
                        base.m_name = "Query_SelectionTools_BufferSelector";
                        base.m_message = "缓冲区选择";
                        base.m_caption = "缓冲区选择";
                        base.m_category = "Query";
                        base._key = "Query_SelectionTools_BufferSelector";
                        base._itemType = RibbonItemType.DropDown;
                        base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
                        base.DisplayStyleYT = DisplayStyleYT.ImageAndText;
                        base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
                        break;
                    }
                case 0:
                    {
                        base.m_bitmap = Properties.Resources.icon_select_container;
                        
                        base.m_toolTip = "选择包含在缓冲区中的要素";
                        base.m_name = "Query_SelectionTools_SelectFeatureContainers";
                        base.m_message = "选择包含在缓冲区中的要素";
                        base.m_caption = "选择包含在缓冲区中的要素";
                        base.m_category = "Query";
                        base._key = "Query_SelectionTools_SelectFeatureContainers";
                        base._itemType = RibbonItemType.Button;
                        base.DisplayStyleYT = DisplayStyleYT.ImageAndText;
                        base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
                        break;
                    }
                case 1:
                    {
                        base.m_bitmap = Properties.Resources.icon_select_intersect;
                       
                        base.m_toolTip = "选择和缓冲区相交要素";
                        base.m_name = "Query_SelectionTools_SelectFeatureIntersects";
                        base.m_message = "选择和缓冲区相交要素";
                        base.m_caption = "选择和缓冲区相交要素";
                        base.m_category = "Query";
                        base._key = "Query_SelectionTools_SelectFeatureIntersects";
                        base._itemType = RibbonItemType.Button;
                        base.DisplayStyleYT = DisplayStyleYT.ImageAndText;
                        base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
                        break;
                    }
            }

        }
    }
}