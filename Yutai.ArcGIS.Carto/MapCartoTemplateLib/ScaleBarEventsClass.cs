namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    internal class ScaleBarEventsClass
    {
        internal static event ValueChangeHandler ValueChange;

        internal static void ScaleBarChage(object object_0)
        {
            if (ValueChange != null)
            {
                ValueChange(object_0);
            }
        }

        internal delegate void ValueChangeHandler(object object_0);
    }
}