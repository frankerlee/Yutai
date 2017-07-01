namespace Yutai.ArcGIS.Carto.DesignLib
{
    internal class ScaleTextEventsClass
    {
        internal static event ValueChangeHandler ValueChange;

        internal static void ScaleTextChage(object object_0)
        {
            if (ValueChange != null)
            {
                ValueChange(object_0);
            }
        }

        internal delegate void ValueChangeHandler(object object_0);
    }
}