namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class ScaleTextEventsClass
    {
        internal static  event ValueChangeHandler ValueChange;

        internal static void ScaleTextChage(object sender)
        {
            if (ValueChange != null)
            {
                ValueChange(sender);
            }
        }

        internal delegate void ValueChangeHandler(object sender);
    }
}

