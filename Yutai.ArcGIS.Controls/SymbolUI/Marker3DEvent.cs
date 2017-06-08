namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class Marker3DEvent
    {
        public static  event Marker3DChangedHandler Marker3DChanged;

        public static void Marker3DChangeH(object sender)
        {
            if (Marker3DChanged != null)
            {
                Marker3DChanged(sender);
            }
        }

        public delegate void Marker3DChangedHandler(object sender);
    }
}

