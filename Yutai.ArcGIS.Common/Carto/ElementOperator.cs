using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Common.Carto
{
    public class ElementOperator
    {
        public ElementOperator()
        {
        }

        public static void FocusOneElement(IActiveView iactiveView_0, IElement ielement_0)
        {
            try
            {
                IGraphicsContainerSelect iactiveView0 = iactiveView_0 as IGraphicsContainerSelect;
                if (iactiveView0 != null)
                {
                    iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    if (iactiveView0.ElementSelectionCount >= 1)
                    {
                        iactiveView0.UnselectAllElements();
                    }
                    iactiveView0.SelectElement(ielement_0);
                    iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ielement_0, null);
                }
            }
            catch
            {
            }
        }
    }
}