using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class GraphicsManage
    {
        public static IElement FindElement(string string_0, IGraphicsContainer igraphicsContainer_0)
        {
            IElement element;
            while ((element = igraphicsContainer_0.Next()) != null)
            {
                if ((element as IElementProperties2).Name == string_0)
                {
                    return element;
                }
            }
            return null;
        }

        private static IElement FindElementByType(IGroupElement igroupElement_0, string string_0)
        {
            IEnumElement elements = igroupElement_0.Elements;
            elements.Reset();
            for (IElement element2 = elements.Next(); element2 != null; element2 = elements.Next())
            {
                if ((element2 is IElementProperties) && ((element2 as IElementProperties).Type == "外框"))
                {
                    return element2;
                }
                if (element2 is IGroupElement)
                {
                    IElement element4 = FindElementByType(element2 as IGroupElement, string_0);
                    if (element4 != null)
                    {
                        return element4;
                    }
                }
            }
            return null;
        }

        public static IElement FindElementByType(IPageLayout ipageLayout_0, string string_0)
        {
            IGraphicsContainer container = ipageLayout_0 as IGraphicsContainer;
            container.Reset();
            for (IElement element = container.Next(); element != null; element = container.Next())
            {
                if ((element is IElementProperties) && ((element as IElementProperties).Type == "外框"))
                {
                    return element;
                }
                if (element is IGroupElement)
                {
                    IElement element3 = FindElementByType(element as IGroupElement, string_0);
                    if (element3 != null)
                    {
                        return element3;
                    }
                }
            }
            return null;
        }
    }
}

