using ESRI.ArcGIS.Carto;
using System;
using System.Collections.Generic;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public class CmdGroupElementEx : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                bool result;
                if (MapTemplate.CurrentMapTemplate == null)
                {
                    result = false;
                }
                else if (!(this._context.ActiveView is IPageLayout))
                {
                    result = false;
                }
                else
                {
                    IGraphicsContainerSelect graphicsContainerSelect =
                        this._context.ActiveView.GraphicsContainer as IGraphicsContainerSelect;
                    result = (graphicsContainerSelect.ElementSelectionCount > 1);
                }
                return result;
            }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "组合";
            this.m_toolTip = "组合元素";
            this.m_category = "制图";
            this.m_name = "Printing_GroupElementEx";
            base.m_bitmap = Properties.Resources.icon_group;

            _key = "Printing_GroupElementEx";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdGroupElementEx(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
            IGraphicsContainer graphicsContainer = this._context.ActiveView.GraphicsContainer;
            IGraphicsContainerSelect graphicsContainerSelect = graphicsContainer as IGraphicsContainerSelect;
            IEnumElement selectedElements = graphicsContainerSelect.SelectedElements;
            MapTemplateGroupElement mapTemplateGroupElement = new MapTemplateGroupElement(MapTemplate.CurrentMapTemplate);
            mapTemplateGroupElement.Create(selectedElements);
            mapTemplateGroupElement.Save();
            List<IElement> list = new List<IElement>();
            selectedElements.Reset();
            for (IElement element = selectedElements.Next(); element != null; element = selectedElements.Next())
            {
                list.Add(element);
            }
            foreach (IElement current in list)
            {
                graphicsContainer.DeleteElement(current);
            }
            IGraphicsContainer graphicsContainer2 = this._context.ActiveView.GraphicsContainer;
            IElement element2 = mapTemplateGroupElement.GetElement(this._context.ActiveView as IPageLayout);
            graphicsContainer2.AddElement(element2, -1);
            this._context.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element2, null);
        }
    }
}