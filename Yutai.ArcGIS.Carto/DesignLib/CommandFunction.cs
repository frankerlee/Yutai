using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class CommandFunction
    {
        public static IElement FindElementByType(IGroupElement igroupElement_0, string string_0)
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

        public static IMapFrame FindFocusMapFrame(IPageLayout ipageLayout_0)
        {
            IActiveView view = ipageLayout_0 as IActiveView;
            IMap focusMap = view.FocusMap;
            IGraphicsContainer container = ipageLayout_0 as IGraphicsContainer;
            container.Reset();
            for (IElement element = container.Next(); element != null; element = container.Next())
            {
                if (element is IMapFrame)
                {
                    if (focusMap == (element as IMapFrame).Map)
                    {
                        return (element as IMapFrame);
                    }
                }
                else if (element is IGroupElement)
                {
                    IMapFrame frame2 = FindFocusMapFrame(element as IGroupElement, focusMap);
                    if (frame2 != null)
                    {
                        return frame2;
                    }
                }
            }
            return null;
        }

        private static IMapFrame FindFocusMapFrame(IGroupElement igroupElement_0, IMap imap_0)
        {
            IEnumElement elements = igroupElement_0.Elements;
            elements.Reset();
            for (IElement element2 = elements.Next(); element2 != null; element2 = elements.Next())
            {
                if (element2 is IMapFrame)
                {
                    if (imap_0 == (element2 as IMapFrame).Map)
                    {
                        return (element2 as IMapFrame);
                    }
                }
                else if (element2 is IGroupElement)
                {
                    IMapFrame frame2 = FindFocusMapFrame(element2 as IGroupElement, imap_0);
                    if (frame2 != null)
                    {
                        return frame2;
                    }
                }
            }
            return null;
        }

        public static IMapFrame FindFocusMapFrameEx(IPageLayout ipageLayout_0)
        {
            IGraphicsContainer container = ipageLayout_0 as IGraphicsContainer;
            return (container.FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame);
        }
    }
}

