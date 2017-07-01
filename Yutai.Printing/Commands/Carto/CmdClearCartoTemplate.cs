using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public sealed class CmdClearCartoTemplate : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.FocusMap != null && this._context.FocusMap.LayerCount > 0; }
        }


        public override void OnCreate(object hook)
        {
            this.m_category = "制图工具";
            this.m_caption = "清除模板";
            this.m_message = "清除制图模板";
            this.m_toolTip = "清除制图模板";
            this.m_name = "ClearCartoTemplate";
            base.m_bitmap = Properties.Resources.icon_map_cleae;
            base.m_name = "Printing_ClearCartoTemplate";
            _key = "Printing_ClearCartoTemplate";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdClearCartoTemplate(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        private void ClearElements(IActiveView pActiveView)
        {
            IGraphicsContainer graphicsContainer = pActiveView.GraphicsContainer;
            IMapFrame mapFrame = graphicsContainer.FindFrame(pActiveView.FocusMap) as IMapFrame;
            (mapFrame as IMapGrids).ClearMapGrids();
            mapFrame.ExtentType = esriExtentTypeEnum.esriExtentDefault;
            graphicsContainer.Reset();
            IElement element = graphicsContainer.Next();
            List<IElement> list = new List<IElement>();
            while (element != null)
            {
                if (element != mapFrame)
                {
                    list.Add(element);
                }
                element = graphicsContainer.Next();
            }
            try
            {
                graphicsContainer.DeleteAllElements();
                graphicsContainer.Reset();
                element = graphicsContainer.Next();
                if (element != null)
                {
                    graphicsContainer.DeleteElement(element);
                }
                graphicsContainer.AddElement(mapFrame as IElement, -1);
                pActiveView.FocusMap = mapFrame.Map;
            }
            catch (Exception)
            {
            }
        }

        public override void OnClick()
        {
            if (this._context.ActiveView != null)
            {
                this.ClearElements(this._context.ActiveView as IActiveView);
                (this._context.ActiveView as IActiveView).Refresh();
            }
        }
    }
}