using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Common.Carto
{
    public class ElementChangeEvent
    {
        public static bool IsEdit { get; set; }

        public static bool IsFireElementPropertyEditEvent { get; set; }

        public ElementChangeEvent()
        {
        }

        public static void DeleteElement(IElement ielement_0)
        {
            if (ElementChangeEvent.OnDeleteElement != null)
            {
                ElementChangeEvent.OnDeleteElement(ielement_0);
            }
        }

        public static void EditElementProperty(IElement ielement_0)
        {
            ElementChangeEvent.IsEdit = false;
            if (ElementChangeEvent.OnEditElementProperty != null)
            {
                ElementChangeEvent.OnEditElementProperty(ielement_0);
            }
        }

        public static void ElementPositionChange(IElement ielement_0)
        {
            if (ElementChangeEvent.OnElementPositionChange != null)
            {
                ElementChangeEvent.OnElementPositionChange(ielement_0);
            }
        }

        public static void ElementPropertyChange(IElement ielement_0)
        {
            if (ElementChangeEvent.OnElementPropertyChange != null)
            {
                ElementChangeEvent.OnElementPropertyChange(ielement_0);
            }
        }

        public static void ElementSelectChange()
        {
            if (ElementChangeEvent.OnElementSelectChange != null)
            {
                ElementChangeEvent.OnElementSelectChange();
            }
        }

        public static void InsertElement(ElementType elementType_0)
        {
            if (ElementChangeEvent.OnInsertElement != null)
            {
                ElementChangeEvent.OnInsertElement(elementType_0);
            }
        }

        public static void LoadMapTemplate(string string_0)
        {
            if (ElementChangeEvent.OnLoadMapTemplate != null)
            {
                ElementChangeEvent.OnLoadMapTemplate(string_0);
            }
        }

        public static event OnDeleteElementHandler OnDeleteElement;

        public static event OnEditElementPropertyHandler OnEditElementProperty;

        public static event OnElementPositionChangeHandler OnElementPositionChange;

        public static event OnElementPropertyChangeHandler OnElementPropertyChange;

        public static event OnElementSelectChangeHandler OnElementSelectChange;

        public static event OnInsertElementHandler OnInsertElement;

        public static event OnLoadMapTemplateHandler OnLoadMapTemplate;
    }
}