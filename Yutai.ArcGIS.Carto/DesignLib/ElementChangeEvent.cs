using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ElementChangeEvent
    {
        public static  event OnDeleteElementHandler OnDeleteElement;

        public static  event OnEditElementPropertyHandler OnEditElementProperty;

        public static  event OnElementPositionChangeHandler OnElementPositionChange;

        public static  event OnElementPropertyChangeHandler OnElementPropertyChange;

        public static  event OnInsertElementHandler OnInsertElement;

        public static  event OnLoadMapTemplateHandler OnLoadMapTemplate;

        public static void DeleteElement(IElement ielement_0)
        {
            if (OnDeleteElement != null)
            {
                OnDeleteElement(ielement_0);
            }
        }

        public static void EditElementProperty(IElement ielement_0)
        {
            if (OnEditElementProperty != null)
            {
                OnEditElementProperty(ielement_0);
            }
        }

        public static void ElementPositionChange(IElement ielement_0)
        {
            if (OnElementPositionChange != null)
            {
                OnElementPositionChange(ielement_0);
            }
        }

        public static void ElementPropertyChange(IElement ielement_0)
        {
            if (OnElementPropertyChange != null)
            {
                OnElementPropertyChange(ielement_0);
            }
        }

        public static void InsertElement(ElementType elementType_0)
        {
            if (OnInsertElement != null)
            {
                OnInsertElement(elementType_0);
            }
        }

        public static void LoadMapTemplate(string string_0)
        {
            if (OnLoadMapTemplate != null)
            {
                OnLoadMapTemplate(string_0);
            }
        }
    }
}

