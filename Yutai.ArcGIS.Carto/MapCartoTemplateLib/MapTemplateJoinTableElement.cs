using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ExtendClass;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateJoinTableElement : MapTemplateElement
    {
        private List<string> list_0;

        public MapTemplateJoinTableElement(MapTemplate mapTemplate_1) : base(mapTemplate_1)
        {
            this.list_0 = new List<string>(9);
            base.MapTemplateElementType = MapTemplateElementType.JoinTableElement;
            base.Name = "接图表";
        }

        public MapTemplateJoinTableElement(int int_1, MapTemplate mapTemplate_1) : base(int_1, mapTemplate_1)
        {
            this.list_0 = new List<string>(9);
            base.MapTemplateElementType = MapTemplateElementType.JoinTableElement;
            base.Name = "接图表";
        }

        public override MapTemplateElement Clone(MapTemplate mapTemplate_1)
        {
            MapTemplateJoinTableElement element = new MapTemplateJoinTableElement(mapTemplate_1);
            this.CopyTo(element);
            return element;
        }

        public override IElement CreateElement(IPageLayout ipageLayout_0)
        {
            IPoint position = this.GetPosition(ipageLayout_0);
            JoinTableElement element = new JoinTableElement();
            element.CreateJionTab(ipageLayout_0 as IActiveView, position);
            this.Element = element;
            return this.Element;
        }

        public override void Init()
        {
            JoinTableElement element = this.Element as JoinTableElement;
            for (int i = 0; i < 9; i++)
            {
                if (i != 4)
                {
                    element.SetJTB(this.list_0[i], i);
                }
            }
        }

        public void SetJTBTH(string string_1, int int_1)
        {
            if (this.list_0.Count == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    this.list_0.Add("");
                }
            }
            this.list_0[int_1] = string_1;
        }

        public override void Update(IPageLayout ipageLayout_0)
        {
            if (this.Element != null)
            {
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.Element, null);
                IPoint position = this.GetPosition(ipageLayout_0);
                (this.Element as JoinTableElement).CreateJionTab(ipageLayout_0 as IActiveView, position);
                base.m_pElement.Geometry = position;
                (ipageLayout_0 as IActiveView).GraphicsContainer.UpdateElement(base.m_pElement);
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, base.m_pElement, null);
                this.Save();
            }
        }

        protected override IPropertySet PropertySet
        {
            get
            {
                return new PropertySetClass();
            }
            set
            {
                try
                {
                }
                catch
                {
                }
            }
        }
    }
}

