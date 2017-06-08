namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class ScaleBarEventsClass
    {
        internal static  event ValueChangeHandler ValueChange;

        internal static void ScaleBarChage(object sender)
        {
            if (ValueChange != null)
            {
                ValueChange(sender);
            }
        }

        internal delegate void ValueChangeHandler(object sender);
    }
}

