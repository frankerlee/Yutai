namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class MapTemplateAssis
    {
        public static  event OnExcuteAddElementHandler OnExcuteAddElement;

        public static void ExcuteAddElement()
        {
            if (OnExcuteAddElement != null)
            {
                OnExcuteAddElement();
            }
        }
    }
}

